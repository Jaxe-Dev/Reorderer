using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Reorderer.Engine
{
  public static class DesignationCategoryConfig
  {
    private static OrderGUI<DesignationCategoryDef> _orderer;

    private static Dictionary<DesignationCategoryDef, int> _originals;

    public static void Init()
    {
      CheckDefStates();

      _orderer = new OrderGUI<DesignationCategoryDef>(() => Settings.DesignationCategoryStates, ApplyChanges, Reset);
    }

    public static void OnGUI(Rect rect) => _orderer.OnGUI(rect, false);

    private static void CheckDefStates()
    {
      if (_originals == null)
      {
        _originals = new Dictionary<DesignationCategoryDef, int>();
        foreach (var def in DefDatabase<DesignationCategoryDef>.AllDefs) { _originals[def] = def.order; }
      }

      if (Settings.DesignationCategoryStates == null) { Settings.DesignationCategoryStates = new List<DefState<DesignationCategoryDef>>(); }

      foreach (var def in DefDatabase<DesignationCategoryDef>.AllDefs.OrderByDescending(def => def.order))
      {
        if (Settings.DesignationCategoryStates.Any(p => p.Def == def)) { continue; }
        Settings.DesignationCategoryStates.Add(new DefState<DesignationCategoryDef>(def));
      }

      ApplyChanges();
    }

    private static void Reset()
    {
      foreach (var original in _originals) { original.Key.order = original.Value; }
      Settings.DesignationCategoryStates = new List<DefState<DesignationCategoryDef>>();
      CheckDefStates();
    }

    private static void ApplyChanges()
    {
      var index = Settings.DesignationCategoryStates.Count - 1;
      foreach (var state in Settings.DesignationCategoryStates)
      {
        state.Def.order = index;
        index--;
      }

      Traverse.Create(MainButtonDefOf.Architect.TabWindow).Method("CacheDesPanels").GetValue();
      Settings.Save();
    }
  }
}

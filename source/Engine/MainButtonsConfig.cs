using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Reorderer.Engine
{
  public static class MainButtonsConfig
  {
    private static Traverse _buttonOrder;
    private static OrderGUI<MainButtonDef> _orderer;

    public static bool IsReady { get; private set; }

    public static void Init(MainButtonsRoot instance)
    {
      Settings.Validate();

      DesignationCategoryConfig.Init();

      _buttonOrder = Traverse.Create(instance).Field("allButtonsInOrder");

      CheckDefStates();

      _orderer = new OrderGUI<MainButtonDef>(() => Settings.MainButtonStates, ApplyChanges, Reset);

      IsReady = true;
    }

    public static void OnGUI(Rect rect) => _orderer.OnGUI(rect, true);

    private static void CheckDefStates()
    {
      if (Settings.MainButtonStates == null) { Settings.MainButtonStates = new List<DefState<MainButtonDef>>(); }

      foreach (var def in DefDatabase<MainButtonDef>.AllDefs.OrderBy(def => def.order))
      {
        if (!def.buttonVisible || Settings.MainButtonStates.Any(state => state.Def == def)) { continue; }
        Settings.MainButtonStates.Add(new DefState<MainButtonDef>(def));
      }

      ApplyChanges();
    }

    private static void Reset()
    {
      Settings.MainButtonStates = new List<DefState<MainButtonDef>>();
      CheckDefStates();
    }

    private static void ApplyChanges()
    {
      Settings.MainButtonStates = Settings.MainButtonStates.OrderByDescending(state => state.Enabled).ToList();
      _buttonOrder.SetValue(Settings.MainButtonStates.Where(state => state.Enabled).Select(state => state.Def).ToList());
      Settings.Save();
    }
  }
}

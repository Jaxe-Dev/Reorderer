using System.Collections.Generic;
using System.Linq;
using Reorderer.Engine;
using Reorderer.Patch;
using RimWorld;
using UnityEngine;
using Verse;

namespace Reorderer
{
  public class Settings : ModSettings
  {
    private static Settings _instance;

    private static int _typeSelected;

    public static List<DefState<MainButtonDef>> MainButtonStates;
    public static List<DefState<DesignationCategoryDef>> DesignationCategoryStates;

    public static void Validate()
    {
      if (_instance != null) { return; }

      _instance = Reorderer.Mod.Instance.GetSettings<Settings>();

      MainButtonStates = MainButtonStates?.Where(state => state.Def != null && state.Def.buttonVisible).ToList();
      DesignationCategoryStates = DesignationCategoryStates?.Where(state => state.Def != null).ToList();
      Save();
    }

    public static void Save() => _instance.Write();

    public static void OnGUI(Rect rect)
    {
      var grid = rect.GetHGrid(4f, 200f, -1f);

      var typeListing = new Listing_Standard();
      typeListing.Begin(MainButtonsConfig.IsReady ? grid[1] : rect);

      if (MainButtonsConfig.IsReady)
      {
        if (typeListing.RadioButton("Reorderer.MainButtons".Translate(), _typeSelected == 0)) { _typeSelected = 0; }
        if (typeListing.RadioButton("Reorderer.DesignationCategories".Translate(), _typeSelected == 1)) { _typeSelected = 1; }
      }
      else { typeListing.Label("Reorderer.NotReady".Translate()); }

      typeListing.NewColumn();

      typeListing.End();

      if (!MainButtonsConfig.IsReady) { return; }

      if (_typeSelected == 0) { MainButtonsConfig.OnGUI(grid[2]); }
      else if (_typeSelected == 1) { DesignationCategoryConfig.OnGUI(grid[2]); }
    }

    public override void ExposeData()
    {
      Scribe_Collections.Look(ref MainButtonStates, "MainButtonStates", LookMode.Deep);
      Scribe_Collections.Look(ref DesignationCategoryStates, "DesignationCategoryStates", LookMode.Deep);

      base.ExposeData();
    }
  }
}

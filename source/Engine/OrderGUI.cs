using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Reorderer.Engine
{
  public class OrderGUI<T> where T : Def, new()
  {
    private Vector2 _scrollPosition = Vector2.zero;

    private readonly Func<List<DefState<T>>> _defStates;
    private readonly Action _applyChanges;
    private readonly Action _doReset;

    public OrderGUI(Func<List<DefState<T>>> defStates, Action applyChanges, Action doReset)
    {
      _defStates = defStates;
      _applyChanges = applyChanges;
      _doReset = doReset;
    }

    public void OnGUI(Rect rect, bool isCheckbox)
    {
      var defStates = _defStates.Invoke();
      var height = (defStates.Count * 26f) + 8f;

      var scrollRect = new Rect(0f, 0f, rect.width - 16f, height);
      Widgets.BeginScrollView(rect, ref _scrollPosition, scrollRect);

      var listingRect = scrollRect.ContractedBy(4f);
      var listing = new Listing_Standard { ColumnWidth = 220f };

      listing.Begin(listingRect);

      var reorderableGroup = ReorderableWidget.NewGroup(Reorder, ReorderableDirection.Vertical);

      foreach (var state in defStates) { DoRow(listing, state, reorderableGroup, isCheckbox); }

      if (listing.ButtonText("Reorderer.Reset".Translate())) { _doReset(); }

      listing.End();
      Widgets.EndScrollView();
    }

    private void DoRow(Listing listing, DefState<T> state, int reorderableGroup, bool isCheckbox)
    {
      var rect = listing.GetRect(26f);
      ReorderableWidget.Reorderable(reorderableGroup, rect);

      Widgets.DrawAtlas(rect, Widgets.ButtonSubtleAtlas);

      rect.xMin += 28f;

      if (isCheckbox)
      {
        var wasEnabled = state.Enabled;
        Widgets.CheckboxLabeled(rect, state.Def.LabelCap, ref state.Enabled);

        if (state.Enabled != wasEnabled) { _applyChanges(); }
      }
      else { Widgets.Label(rect, state.Def.LabelCap); }
    }

    private void Reorder(int currentIndex, int newIndex)
    {
      if (currentIndex == newIndex) { return; }

      var defStates = _defStates();

      defStates.Insert(newIndex, defStates[currentIndex]);
      defStates.RemoveAt(currentIndex >= newIndex ? currentIndex + 1 : currentIndex);

      _applyChanges();
    }
  }
}

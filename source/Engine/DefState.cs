using Verse;

namespace Reorderer.Engine
{
  public class DefState<T> : IExposable where T : Def, new()
  {
    private T _def;
    public T Def => _def;

    public bool Enabled;

    public DefState()
    { }

    public DefState(T def, bool enabled = true)
    {
      _def = def;
      Enabled = enabled;
    }

    public void ExposeData()
    {
      try { Scribe_Defs.Look(ref _def, "def"); }
      catch { _def = null; }
      Scribe_Values.Look(ref Enabled, "enabled", true, true);
    }
  }
}

using HarmonyLib;
using UnityEngine;
using Verse;

namespace Reorderer
{
    public class Mod : Verse.Mod
    {
        public const string Id = "Reorderer";
        public const string Name = "Reorderer";
        public const string Version = "1.3";

        public static Mod Instance { get; private set; }

        public Mod(ModContentPack content) : base(content)
        {
            Instance = this;

            new Harmony(Id).PatchAll();
            Log("Initialized");
        }

        public override void DoSettingsWindowContents(Rect inRect) => Settings.OnGUI(inRect);

        public override string SettingsCategory() => Name;

        public static void Log(string message) => Verse.Log.Message(PrefixMessage(message));
        private static string PrefixMessage(string message) => $"[{Name} v{Version}] {message}";
    }
}

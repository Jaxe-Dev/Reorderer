using Harmony;
using Reorderer.Engine;
using RimWorld;

namespace Reorderer.Patch
{
    [HarmonyPatch(typeof(MainButtonsRoot), MethodType.Constructor)]
    internal static class RimWorld_MainButtonsRoot_Ctor
    {
        private static void Postfix(MainButtonsRoot __instance) => MainButtonsConfig.Init(__instance);
    }
}

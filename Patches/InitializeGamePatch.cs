using HarmonyLib;

namespace IntroTweaks.Patches;

[HarmonyPatch(typeof(InitializeGame))]
internal class InitializeGamePatch {
    [HarmonyPrefix]
    [HarmonyPatch("Start")]
    static void DisableBootAnimation(InitializeGame __instance) {
        if (Plugin.Config.SKIP_BOOT_ANIMATION.Value) {
            __instance.runBootUpScreen = false;
            __instance.bootUpAudio = null;
            __instance.bootUpAnimation = null;
        }
    }
}
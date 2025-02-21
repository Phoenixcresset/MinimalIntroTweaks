using HarmonyLib;
using UnityEngine.SceneManagement;

namespace IntroTweaks.Patches;

[HarmonyPatch(typeof(PreInitSceneScript))]
internal class PreInitScenePatch {
    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    static void FinishedFirstLaunch() {
        IngamePlayerSettings.Instance?.SetPlayerFinishedLaunchOptions();
    }

    [HarmonyPostfix]
    [HarmonyPatch("SkipToFinalSetting")]
    internal static void SkipToSelectedMode(PreInitSceneScript __instance, ref bool ___choseLaunchOption) {
        string mode = Plugin.SelectedMode;
        if (mode != "online" && mode != "lan" && mode != "off") {
            Plugin.Logger.LogWarning("Wrong value for AutoSelectMode, defaulting to OFF");
            mode = "off";
        }
        if (mode == "off") return;

        #region Skip panels & play sound
        __instance.LaunchSettingsPanels.Do(panel => panel.SetActive(false));
        __instance.currentLaunchSettingPanel = 0;
        __instance.headerText.text = "";
        __instance.blackTransition.gameObject.SetActive(false);
        __instance.continueButton.gameObject.SetActive(false);

        ___choseLaunchOption = true;
        __instance.mainAudio.PlayOneShot(__instance.selectSFX);
        #endregion

        #region Choose scene and load
        bool online = Plugin.SelectedMode == "online";
        string sceneToLoad = online ? "InitScene" : "InitSceneLANMode";

        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        #endregion
    }
}
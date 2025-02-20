using System.Collections;
using HarmonyLib;
using IntroTweaks.Data;
using UnityEngine;

namespace IntroTweaks.Patches;

[HarmonyPatch(typeof(MenuManager))]
internal class MenuManagerPatch {

    static MenuManager Instance;
    static Config Cfg => Plugin.Config;

    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    static void Init(MenuManager __instance) {
        Instance = __instance;
        Instance.StartCoroutine(PatchMenuDelayed());
    }

    private static IEnumerator PatchMenuDelayed() {
        // Essentially waits until the menu is fully loaded.
        yield return new WaitUntil(() => !GameNetworkManager.Instance.firstTimeInMenu);

        PatchMenu();
    }

    static void PatchMenu() {
        if (Plugin.ModInstalled("Emblem")) {
            Plugin.Logger.LogWarning("\nDetected conflicting mod: Emblem.\nErrors may occurr if similar menu tweaks are enabled in both configs.");
        }
        
        #region Hide UI elements
        if (Cfg.REMOVE_NEWS_PANEL.Value) {
            Instance.NewsPanel?.SetActive(false);
        }
        #endregion

    }
}
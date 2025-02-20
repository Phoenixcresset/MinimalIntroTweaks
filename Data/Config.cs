using BepInEx.Configuration;

namespace IntroTweaks.Data;

public class Config {
    #region Properties
    #region General
    public ConfigEntry<bool> PLUGIN_ENABLED { get; private set; }
    #endregion

    #region Intro Tweaks
    public ConfigEntry<bool> SKIP_SPLASH_SCREENS { get; private set; }
    public ConfigEntry<bool> SKIP_BOOT_ANIMATION { get; private set; }

    public ConfigEntry<string> AUTO_SELECT_MODE { get; private set; }
    public ConfigEntry<bool> AUTO_SELECT_HOST { get; private set; }
    #endregion

    #region Menu Tweaks
    public ConfigEntry<bool> ALIGN_MENU_BUTTONS { get; private set; }
    public ConfigEntry<bool> FIX_MENU_CANVAS { get; private set; }
    public ConfigEntry<bool> FIX_MENU_PANELS { get; private set; }
    public ConfigEntry<bool> FIX_MORE_COMPANY { get; private set; }
    public ConfigEntry<bool> USE_CUSTOM_HEADER { get; private set; }
    public ConfigEntry<string> CUSTOM_HEADER_PATH { get; private set; }
    //public bool IMPROVE_HOST_SCREEN { get; private set; }

    public ConfigEntry<bool> REMOVE_LAN_WARNING { get; private set; }
    public ConfigEntry<bool> REMOVE_LAUNCHED_IN_LAN { get; private set; }
    public ConfigEntry<bool> REMOVE_NEWS_PANEL { get; private set; }
    public ConfigEntry<bool> REMOVE_CREDITS_BUTTON { get; private set; }
    #endregion

    #region Version Text
    public ConfigEntry<bool> CUSTOM_VERSION_TEXT { get; private set; }
    public ConfigEntry<string> VERSION_TEXT { get; private set; }
    public ConfigEntry<float> VERSION_TEXT_SIZE { get; private set; }
    public ConfigEntry<float> VERSION_TEXT_OFFSET { get; private set; }
    public ConfigEntry<bool> ALWAYS_SHORT_VERSION { get; private set; }
    #endregion

    #region Misc
    public ConfigEntry<bool> AUTO_START_GAME { get; private set; }
    public ConfigEntry<float> AUTO_START_GAME_DELAY { get; private set; }
    public ConfigEntry<bool> DISABLE_FIRST_DAY_SFX { get; private set; }
    public ConfigEntry<int> GAME_STARTUP_DISPLAY { get; private set; }
    #endregion

    readonly ConfigFile configFile;
    #endregion

    public Config(ConfigFile cfg) {
        configFile = cfg;

        PLUGIN_ENABLED = NewEntry("Enabled", true, "Enable or disable the plugin globally.");

        SKIP_SPLASH_SCREENS = NewEntry(Category.INTRO_TWEAKS, "SkipSplashScreens", true,
            "Skips those pesky Unity and Zeekers startup logos!"
        );
    }

    private ConfigEntry<T> NewEntry<T>(string key, T defaultVal, string desc) =>
        NewEntry(Category.GENERAL, key, defaultVal, desc);

    private ConfigEntry<T> NewEntry<T>(Category category, string key, T defaultVal, string desc) =>
        configFile.Bind(category.Value, key, defaultVal, desc);

    public void InitBindings() {
        #region Options related to the intro.
        SKIP_BOOT_ANIMATION = NewEntry(Category.INTRO_TWEAKS, "SkipBootAnimation", true,
            "If the loading animation (booting OS) should be skipped."
        );

        AUTO_SELECT_MODE = NewEntry(Category.INTRO_TWEAKS, "AutoSelectMode", "OFF",
            "Which mode to automatically enter into after the splash screen.\n" +
            "Valid options: ONLINE, LAN, OFF"
        );
        #endregion

        #region Tweaks to the main menu
        REMOVE_NEWS_PANEL = NewEntry(Category.MENU_TWEAKS, "RemoveNewsPanel", false,
            "Hides the panel that displays news such as game updates."
        );
        #endregion
    }
}
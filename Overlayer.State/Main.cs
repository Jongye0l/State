using System.Reflection;
using Overlayer.Core;
using Overlayer.Core.Patches;
using Overlayer.Core.Translation;
using Overlayer.Modules;
using UnityEngine;

namespace State {
    public class Main : OverlayerModule {
        public static Main Instance;
        public static StateSettings Settings;
        private static Assembly _assembly;

        public Main() {
            Instance = this;
        }

        public override bool IsEnabled { get; set; }

        public override void OnLoad() {
            _assembly = Assembly.GetExecutingAssembly();
            Settings = StateSettings.CreateInstance();
        }

        public override void OnEnable() {
            LazyPatchManager.Load(_assembly);
            TagManager.Load(typeof(CustomTags));
            TextManager.Refresh();
            IsEnabled = true;
        }

        public override void OnDisable() {
            TagManager.Unload(typeof(CustomTags));
            LazyPatchManager.Unload(_assembly);
            IsEnabled = false;
            MemoryHelper.Clean(CleanOption.All);
        }

        public override void OnUnload() {
            _assembly = null;
        }

        public override void OnGUI() {
            Values values = GetValues();
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.font = FontManager.GetFont("Default").font;
            style.fontSize = 50;
            style.richText = true;
            GUILayout.Label("State", style);
            GUILayout.BeginHorizontal();
            GUILayout.Label(values.Credit_Devloper + " : Jongyeol");
            if(GUILayout.Button(values.Credit_Source)) Application.OpenURL("https://github.com/Jongye0l/State");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Label(values.Credit_BugReport);
            AddSettingToggle(ref Settings.CurrentStart, values.Setting_CurrentStart);
            AddSettingToggle(ref Settings.Auto, values.Setting_Auto);
            AddSettingToggle(ref Settings.AutoTile, values.Setting_AutoTile);
            AddSettingToggle(ref Settings.MidStart, values.Setting_MidStart);
            if(Settings.Auto) AddSettingToggle(ref Settings.HideAuto, values.Setting_HideAuto);
            if(Settings.AutoTile) AddSettingToggle(ref Settings.HideAutoTile, values.Setting_HideAutoTile);
        }

        private void AddSettingToggle(ref bool value, string text) {
            if(GUILayout.Toggle(value, text)) {
                if(!value) {
                    value = true;
                    Settings.Save();
                }
            } else if(value) {
                value = false;
                Settings.Save();
            }
        }

        public static Values GetValues() {
            return Overlayer.Main.Language == Language.Korean ? Values.Korean : Values.English;
        }
    }
}
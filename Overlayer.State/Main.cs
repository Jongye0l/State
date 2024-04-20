using System.Collections.Generic;
using System.Reflection;
using Overlayer.Core;
using Overlayer.Core.Patches;
using Overlayer.Tags;
using UnityEngine;
using UnityModManagerNet;

namespace Overlayer.State {
    public class Main {
        public static UnityModManager.ModEntry.ModLogger Logger => ModEntry.Logger;
        public static UnityModManager.ModEntry ModEntry;
        public static StateSettings Settings;
        private static Assembly _assembly;
        private static List<LazyPatch> lazyPatches;
        private static void Setup(UnityModManager.ModEntry modEntry) {
            ModEntry = modEntry;
            _assembly = Assembly.GetExecutingAssembly();
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            Settings = StateSettings.CreateInstance();
        }
        
        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value) {
            if(value) {
                LazyPatchManager.Load(_assembly);
                TagManager.Load(typeof(CustomTags));
                TextManager.Refresh();
                lazyPatches = LazyPatchManager.PatchAll("StartTile");
                foreach(LazyPatch lazyPatch in lazyPatches) lazyPatch.Locked = true;
            } else {
                foreach(LazyPatch lazyPatch in lazyPatches) lazyPatch.Locked = false;
                TagManager.Unload(typeof(CustomTags));
                LazyPatchManager.Unload(_assembly);
            }
            return true;
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry) {
            Values values = GetValues();
            GUILayout.Label("State", new GUIStyle(GUI.skin.label) {
                font = FontManager.GetFont("Default").font,
                fontSize = 50,
                richText = true
            });
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

        private static void AddSettingToggle(ref bool value, string text) {
            if(GUILayout.Toggle(value, text) == value) return;
            value = !value;
            Settings.Save();
        }

        public static Values GetValues() => RDString.language == SystemLanguage.Korean ? Values.Korean : Values.English;
    }
}
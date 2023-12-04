using System.IO;
using Newtonsoft.Json;

namespace State {
    public class StateSettings {
        private static readonly string SettingPath = Path.Combine(Main.Instance.ModulePath, "Settings.json");
        public static StateSettings Instance;
        public bool CurrentStart = true;
        public bool Auto = true;
        public bool AutoTile = true;
        public bool MidStart = true;
        public bool HideAuto = true;
        public bool HideAutoTile = true;
        
        public static StateSettings CreateInstance() {
            Instance = File.Exists(SettingPath) ? JsonConvert.DeserializeObject<StateSettings>(File.ReadAllText(SettingPath)) : new StateSettings();
            return Instance;
        }

        public void Save() {
            File.WriteAllText(SettingPath, JsonConvert.SerializeObject(Instance, Formatting.Indented));
        }

        public bool GetHideAuto() {
            return Auto && HideAuto;
        }

        public bool GetHideAutoTile() {
            return AutoTile && HideAutoTile;
        }
    }
}
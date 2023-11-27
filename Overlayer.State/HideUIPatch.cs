using Overlayer.Core.Patches;
using UnityEngine.UI;

namespace State {
    [LazyPatch("State.HideDebugUI", "scrShowIfDebug", "Update", Triggers = new[] { "State" })]
    public class HideUIPatch {
        private static StateSettings Settings = StateSettings.Instance;
        private static bool auto;

        public static void Prefix() {
            if(Settings.GetHideAuto() && !Settings.GetHideAutoTile()) {
                auto = RDC.auto;
                RDC.auto = false;
            }
        }
        
        public static void Postfix(Text ___txt) {
            if(Settings.GetHideAutoTile() && (!RDC.auto || Settings.GetHideAuto())) ___txt.text = string.Empty;
            else if(Settings.GetHideAuto()) RDC.auto = auto; 
        }
    }
}
using Overlayer.Core.Patches;
using UnityEngine.UI;

namespace State {
    [LazyPatch("State.HideDebugUI", "scrShowIfDebug", "Update", Triggers = new[] { "State" })]
    public class HideUIPatch {
        private static readonly StateSettings Settings = StateSettings.Instance;
        private static bool _auto;

        public static void Prefix() {
            if(Settings.GetHideAuto() && !Settings.GetHideAutoTile()) {
                _auto = RDC.auto;
                RDC.auto = false;
            }
        }
        
        public static void Postfix(Text ___txt) {
            if(Settings.GetHideAutoTile() && (!RDC.auto || Settings.GetHideAuto())) ___txt.text = string.Empty;
            else if(Settings.GetHideAuto()) RDC.auto = _auto; 
        }
    }
}
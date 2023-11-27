using Overlayer.Core.Patches;
using Overlayer.Patches;
using UnityEngine.UI;

namespace State {
    public class OverlayerStatePatch {

        [LazyPatch("OverlayerStatePatch.UpdateStartProgress", "scrCountdown", "Update",
            Triggers = new[] { "State" })]
        public class ProgressStartPatch {
            public static bool Prefix(scrCountdown __instance, ref Text ___text, ref float ___timeGoTween) {
                return StartProgUpdater.Prefix(__instance, ref ___text, ref ___timeGoTween);
            }
        }

        [LazyPatch("OverlayerStatePatch.HitMarginCounterAndScoreCalculator", "scrMisc", "GetHitMargin",
            Triggers = new[] { "State" })]
        public class HitMarginPatch {
            public static bool Prefix(float hitangle, float refangle, bool isCW, float bpmTimesSpeed,
                float conductorPitch, double marginScale, ref HitMargin __result) {
                return GetHitMarginFixer.Prefix(hitangle, refangle, isCW, bpmTimesSpeed, conductorPitch, marginScale, 
                    ref __result);
            }
        }
    }
}
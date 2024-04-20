using System;
using Overlayer.Tags;
using Overlayer.Tags.Attributes;

namespace Overlayer.State {
    public class CustomTags {

        private static readonly StateSettings Settings = StateSettings.Instance;

        private static int[] GetHitCount() {
            return scrMistakesManager.hitMarginsCount;
        }

        public static bool IsAutoPlayTile() {
            return scrController.instance.currFloor &&
                   scrController.instance.currFloor.nextfloor &&
                   scrController.instance.currFloor.nextfloor.auto;
        }

        private static bool IsPurePerfect(int[] HitCount) {
            return GCS.hitMarginLimit == HitMarginLimit.PurePerfectOnly ||
                   HitCount[(int) HitMargin.VeryEarly] + HitCount[(int) HitMargin.TooEarly] + HitCount[(int) HitMargin.EarlyPerfect] +
                   HitCount[(int) HitMargin.LatePerfect] + HitCount[(int) HitMargin.TooLate] + HitCount[(int) HitMargin.VeryLate] == 0;
        }

        private static bool IsPerfect(int[] HitCount) {
            return GCS.hitMarginLimit == HitMarginLimit.PerfectsOnly ||
                   HitCount[(int) HitMargin.VeryEarly] + HitCount[(int) HitMargin.VeryLate] == 0;
        }

        private static bool IsDeath() {
            scrController instance = scrController.instance;
            return instance && instance.mistakesManager.GetHits(HitMargin.FailOverload) +
                instance.mistakesManager.GetHits(HitMargin.FailMiss) != 0;
        }

        [Tag]
        public static string State() {
            try {
                string value;
                bool colored = false;
                int startTile = Tile.StartTile;
                int currentTile = scrController.instance.currentSeqID + 1;
                int[] hitCount = GetHitCount();
                Values values = Main.GetValues();
                if(Settings.CurrentStart && startTile == 0 || startTile == currentTile) value = values.State_Wait;
                else if(Settings.AutoTile && IsAutoPlayTile()) {
                    value = "<color=#FF7F00>" + values.State_AutoPlayTile;
                    colored = true;
                } else if(RDC.auto) {
                    value = "<color=#1BFF00>" + values.State_AutoPlay;
                    colored = true;
                } else if(IsDeath()) value = values.State_Finish;
                else if(IsPurePerfect(hitCount)) {
                    value = "<color=#FFDA00>" + values.State_PurePerfect;
                    colored = true;
                } else if(IsPerfect(hitCount)) {
                    value = hitCount[(int) HitMargin.VeryEarly] + hitCount[(int) HitMargin.TooEarly] +
                        hitCount[(int) HitMargin.TooLate] + hitCount[(int) HitMargin.VeryLate] == 0 ? values.State_PerfectNoMiss : values.State_Perfect;
                } else if(hitCount[(int) HitMargin.TooEarly] + hitCount[(int) HitMargin.TooLate] == 0) value = values.State_NoMiss;
                else value = values.State_Clear;
                if(values == Values.Korean && currentTile != scrLevelMaker.instance.listFloors.Count) value += " 중";
                if(Settings.MidStart && startTile != 1 || startTile != 0) value += values.State_MidStart;
                if(colored) value += "</color>";
                return value;
            }
            catch (Exception e) {
                Main.Logger.LogException(e);
                return "<color=#FF0000>오류</color>";
            }
        }
    }
}
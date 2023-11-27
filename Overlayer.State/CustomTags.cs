using System;
using System.Collections.Generic;
using Overlayer.Core.Tags;
using Overlayer.Tags;

namespace State {
    public class CustomTags {
        
        private static readonly StateSettings Settings = StateSettings.Instance;
        
        private static int GetStartTile() {
            return Variables.StartTile;
        }

        private static int GetCurrentTile() {
            return scrController.instance.currentSeqID + 1;
        }

        private static Dictionary<HitMargin, int> GetHitCount() {
            Dictionary<HitMargin, int> HitCount = null;
            switch (GCS.difficulty) {
                case Difficulty.Lenient:
                    HitCount = Variables.LenientCounts;
                    break;
                case Difficulty.Normal:
                    HitCount = Variables.NormalCounts;
                    break;
                case Difficulty.Strict:
                    HitCount = Variables.StrictCounts;
                    break;
            }
            return HitCount;
        }

        public static bool IsAutoPlayTile() {
            return scrController.instance.currFloor != null &&
                   scrController.instance.currFloor.nextfloor != null &&
                   scrController.instance.currFloor.nextfloor.auto;
        }

        private static bool IsPurePerfect(Dictionary<HitMargin, int> HitCount) {
            return GCS.hitMarginLimit == HitMarginLimit.PurePerfectOnly ||
                   HitCount[HitMargin.VeryEarly] + HitCount[HitMargin.TooEarly] + HitCount[HitMargin.EarlyPerfect] +
                   HitCount[HitMargin.LatePerfect] + HitCount[HitMargin.TooLate] + HitCount[HitMargin.VeryLate] == 0;
        }

        private static bool IsPerfect(Dictionary<HitMargin, int> HitCount) {
            return GCS.hitMarginLimit == HitMarginLimit.PerfectsOnly ||
                   HitCount[HitMargin.VeryEarly] + HitCount[HitMargin.VeryLate] == 0;
        }

        private static bool IsDeath() {
            scrController instance = scrController.instance;
            return instance != null && instance.mistakesManager.GetHits(HitMargin.FailOverload) +
                instance.mistakesManager.GetHits(HitMargin.FailMiss) != 0;
        }

        [Tag]
        public static string State() {
            try {
                string value;
                bool colored = false;
                int StartTile = GetStartTile();
                int CurrentTile = GetCurrentTile();
                Dictionary<HitMargin, int> HitCount = GetHitCount();
                Values values = global::State.Main.GetValues();
                if(Settings.CurrentStart && StartTile == CurrentTile) value = values.State_Wait;
                else if(Settings.AutoTile && IsAutoPlayTile()) {
                    value = "<color=#FF7F00>" + values.State_AutoPlayTile;
                    colored = true;
                } else if(RDC.auto) {
                    value = "<color=#1BFF00>" + values.State_AutoPlay;
                    colored = true;
                } else if(IsDeath()) value = values.State_Finish;
                else if(IsPurePerfect(HitCount)) {
                    value = "<color=#FFDA00>" + values.State_PurePerfect;
                    colored = true;
                } else if(IsPerfect(HitCount)) {
                    if(HitCount[HitMargin.VeryEarly] + HitCount[HitMargin.TooEarly] +
                       HitCount[HitMargin.TooLate] + HitCount[HitMargin.VeryLate] == 0) value = values.State_PerfectNoMiss;
                    else value = values.State_Perfect;
                } else if(HitCount[HitMargin.TooEarly] + HitCount[HitMargin.TooLate] == 0) value = values.State_NoMiss;
                else value = values.State_Clear;
                if(values == Values.Korean && CurrentTile != scrLevelMaker.instance.listFloors.Count) value += " 중";
                if(Settings.MidStart && StartTile != 1) value += values.State_MidStart;
                if(colored) value += "</color>";
                return value;
            }
            catch (Exception e) {
                global::State.Main.Instance.Log(e);
                return "<color=#FF0000>오류</color>";
            }
        }
    }
}
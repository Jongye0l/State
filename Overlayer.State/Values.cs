namespace Overlayer.State {
    public class Values {
        public static readonly Values Korean = new Values {
            State_Wait = "대기",
            State_AutoPlayTile = "자동 플레이 타일",
            State_AutoPlay = "자동 플레이",
            State_Finish = "완주",
            State_PurePerfect = "완벽한 플레이",
            State_PerfectNoMiss = "완벽주의 노미스",
            State_Perfect = "완벽주의",
            State_NoMiss = "노미스",
            State_Clear = "클리어",
            State_MidStart = "(중간에서 시작)",
            
            Setting_CurrentStart = "현재 타일이 시작 타일과 같을 때 '대기' 표시하기",
            Setting_Auto = "'자동 플레이' 표시하기",
            Setting_AutoTile = "'자동 플레이 타일' 표시하기",
            Setting_MidStart = "중간에서 시작시 (중간에서 시작) 텍스트 추가",
            Setting_HideAuto = "자동 플레이 텍스트 UI 숨기기",
            Setting_HideAutoTile = "자동 플레이 타일 텍스트 UI 숨기기",
            
            Credit_Devloper = "개발자",
            Credit_Source = "소스 코드",
            Credit_BugReport = "이 모듈에 대한 버그는 깃허브나 디스코드 'jongyeol_'에게 연락주세요."
        };

        public static readonly Values English = new Values {
            State_Wait = "Waiting",
            State_AutoPlayTile = "AutoPlay Tile",
            State_AutoPlay = "AutoPlay",
            State_Finish = "Finish",
            State_PurePerfect = "Pure Perfect",
            State_PerfectNoMiss = "Perfect No Miss",
            State_Perfect = "Perfect",
            State_NoMiss = "No Miss",
            State_Clear = "Clear",
            State_MidStart = "(starting from middle)",
            
            Setting_CurrentStart = "Show 'Wait' when the current tile is the same as the starting tile",
            Setting_Auto = "Show 'AutoPlay'",
            Setting_AutoTile = "Show 'AutoPlay Tile'",
            Setting_MidStart = "Add text when starting from middle (starting from middle)",
            Setting_HideAuto = "Hide AutoPlay Text UI",
            Setting_HideAutoTile = "Hide AutoPlay Tile Text UI",
            
            Credit_Devloper = "Developer",
            Credit_Source = "Source Code",
            Credit_BugReport = "Please contact GitHub or Discord 'jongyeol_' for bugs about this module."
        };

        public string State_Wait;
        public string State_AutoPlayTile;
        public string State_AutoPlay;
        public string State_Finish;
        public string State_PurePerfect;
        public string State_PerfectNoMiss;
        public string State_Perfect;
        public string State_NoMiss;
        public string State_Clear;
        public string State_MidStart;
        
        public string Setting_CurrentStart;
        public string Setting_Auto;
        public string Setting_AutoTile;
        public string Setting_MidStart;
        public string Setting_HideAuto;
        public string Setting_HideAutoTile;

        public string Credit_Devloper;
        public string Credit_Source;
        public string Credit_BugReport;
    }
}
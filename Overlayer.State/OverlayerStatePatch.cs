using Overlayer.Core.Patches;

namespace State {
    [LazyPatch("StatePatch.PatchTrigger", "TagManager", "UpdateReference", new[] { "State" })]
    public class OverlayerStatePatch {
        public static void Postfix() {
            LazyPatchManager.PatchAll("CurP");
            LazyPatchManager.PatchAll("StartTile");
        }
    }
}
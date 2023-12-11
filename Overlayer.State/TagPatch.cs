using Overlayer.Core.Patches;

namespace State {
    [LazyPatch("State.PatchTrigger", "TagManager", "UpdateReference", Triggers = new[] { "State" })]
    public class TagPatch {
        public static void Postfix() {
            LazyPatchManager.PatchAll("StartTile");
            LazyPatchManager.PatchAll("CurBpm");
            LazyPatchManager.PatchAll("CurP");
        }
    }
}
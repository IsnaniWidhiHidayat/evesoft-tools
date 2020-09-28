#if ODIN_INSPECTOR 
using Sirenix.OdinInspector;

namespace Evesoft.Editor.Reference
{
    [HideReferenceObjectPicker]
    public class GoogleSignInConfigReference : iRefresh
    {
#if GOOGLE_AUTH
        [ShowInInspector,ShowIf(nameof(config)),InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden)]
        private object config;
#endif

        public void Refresh()
        {
            #if GOOGLE_AUTH
            if(!config.IsNull())
                return;

            config = AssetDatabaseFinder.Find<Evesoft.CloudService.GoogleSignIn.GoogleAuthConfigAsset>();
            #endif
        }   
    }
}
#endif
using Sirenix.OdinInspector;

namespace Evesoft.Editor.Reference
{
    [HideReferenceObjectPicker]
    public class AdmobConfigReference : iRefresh
    {
#if ADMOB
        [ShowInInspector,ShowIf(nameof(config)),InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden)]
        private object config;
#endif

        public void Refresh()
        {
            #if ADMOB
            if(!config.IsNull())
                return;

            config = AssetDatabaseFinder.Find<Evesoft.Ads.Admob.AdmobConfigAsset>();
            #endif
        }   
    }
}
using Sirenix.OdinInspector;

namespace Evesoft.Editor.Reference
{
    [HideReferenceObjectPicker]
    public class UnityAdsConfigReference : iRefresh
    {
#if UNITY_ADS
        [ShowInInspector,ShowIf(nameof(config)),InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden)]
        private object config;
#endif

        public void Refresh()
        {
            #if UNITY_ADS
            if(!config.IsNull())
                return;

            config = AssetDatabaseFinder.Find<Evesoft.Ads.UnityAds.UnityAdsConfigAsset>();
            #endif
        }   
    }
}
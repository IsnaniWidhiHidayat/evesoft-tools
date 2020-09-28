#if ODIN_INSPECTOR 
using Sirenix.OdinInspector;

namespace Evesoft.Editor.Reference
{
    [HideReferenceObjectPicker]
    public class LocalizeReference : iRefresh
    {
#if LOCALIZE
        [ShowInInspector,ShowIf(nameof(config)),InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden)]
        private object config;
#endif

        public void Refresh()
        {
            #if LOCALIZE
            if(!config.IsNull())
                return;

            config = AssetDatabaseFinder.Find<Evesoft.Localize.LocalizeDatabase>();
            #endif
        }   
    }
}
#endif
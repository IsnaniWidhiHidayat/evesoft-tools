#if ODIN_INSPECTOR 
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Evesoft.Editor.Reference
{
    [HideReferenceObjectPicker]
    public class YarnSpinnerReference : iRefresh
    {

#if YARN_SPINNER
        [ShowInInspector,ShowIf("@EditorApplication.isPlaying"),InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden)]
        private IDictionary<string,string> variables
        {
            get
            {
                var dialogue = Evesoft.Dialogue.DialogueFactory.Get(Evesoft.Dialogue.DialogueType.YarnSpinner);
                if(dialogue.IsNull())
                    return null;

                var yarn    = dialogue as Evesoft.Dialogue.YarnSpinner.YarnSpinner;
                if(yarn.IsNull())
                    return null;

                return yarn.variables;
            }
        }

        [ShowInInspector,ShowIf("@EditorApplication.isPlaying"),InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden)]
        private IDictionary<string,string> functions
        {
            get
            {
                var dialogue = DialogueFactory.Get(DialogueType.YarnSpinner);
                if(dialogue.IsNull())
                    return null;

                var yarn    = dialogue as Evesoft.Dialogue.YarnSpinner.YarnSpinner;
                if(yarn.IsNull())
                    return null;

                return yarn.functions;
            }
        }
#endif

        public void Refresh()
        {
           
        }
    }
}
#endif
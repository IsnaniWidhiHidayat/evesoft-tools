#if ODIN_INSPECTOR && YARN_SPINNER
using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Yarn;
using System.Threading.Tasks;

namespace Evesoft.Dialogue.YarnSpinner.Component
{
    [System.Serializable,HideReferenceObjectPicker]
    internal class RegisterFunction
    {
        public enum Type
        {
            Function,
            ReturningFunction
        }

        #region const
        const string grpName = "$"+nameof(name);
        const string grph1  = grpName + "/h1";
        #endregion

        [HideLabel,FoldoutGroup(grpName),HorizontalGroup(grph1)]
        public Type type;

        [Required,HideLabel,HorizontalGroup(grph1),SuffixLabel(nameof(name),true)]
        public string name;

        [HideLabel,HorizontalGroup(grph1,Width = 100),SuffixLabel(nameof(paramCount),true)]
        public int paramCount;
        //[ValidateInput(nameof(ValidateFunction),"Need Some Function")
        [HideLabel,FoldoutGroup(grpName),ShowInInspector,Required,ShowIf(nameof(type),Type.Function,false)]
        public Action<object[]> function;

        [HideLabel,FoldoutGroup(grpName),ShowInInspector,Required,ShowIf(nameof(type),Type.ReturningFunction,false)]
        public Func<object[],object> returnfunction;
    }

    [HideMonoScript]
    [AddComponentMenu(Menu.Dialogue + "/" + nameof(YarnSpinner) + "/" + nameof(YarnSpinnerFunctions))]
    [DisallowMultipleComponent]
    internal class YarnSpinnerFunctions : SerializedMonoBehaviour
    {   
        #region const
        const string grpConfig = "Config";
        #endregion

        #region field
        [OdinSerialize,ListDrawerSettings(Expanded = true,DraggableItems = false)]
        internal List<RegisterFunction> registerFunctions = new List<RegisterFunction>();
        #endregion
    }
}
#endif
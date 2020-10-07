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
            ReturningFunction,
            Commmand,
            BlockingCommand,
        }

        #region const
        const string grpName = "$"+nameof(GetName);
        const string grph1  = grpName + "/h1";
        const int labelWidth = 80;
        #endregion

        private string GetName()
        {
            switch(type){
                case Type.Function:
                {
                    return $"{name}(params object[] param) - F";
                }

                case Type.ReturningFunction:
                {
                    return $"object {name}(params object[] param) - RF";
                }

                case Type.Commmand:
                {
                    return $"{name}(params string[] param) - C";
                }

                case Type.BlockingCommand:
                {
                    return $"{name}(params string[] param, Action onComplete) - BC";
                }
            }
            
            return default(string);
        }

        [LabelWidth(labelWidth),FoldoutGroup(grpName)]
        public Type type;

        [LabelWidth(labelWidth),Required,FoldoutGroup(grpName)]
        public string name;

        private bool ShowParamCount => type == Type.ReturningFunction || type ==Type.Function;
        [LabelWidth(labelWidth),FoldoutGroup(grpName),ShowIf(nameof(ShowParamCount))]
        public int paramCount;
       
        [LabelWidth(labelWidth),LabelText("Function"),FoldoutGroup(grpName),ShowInInspector,ShowIf(nameof(type),Type.Function,false)]
        public Action<object[]> function;

        [LabelWidth(labelWidth),LabelText("Function"),FoldoutGroup(grpName),ShowInInspector,ShowIf(nameof(type),Type.ReturningFunction,false)]
        public Func<object[],object> returnfunction;

        [LabelWidth(labelWidth),LabelText("Function"),FoldoutGroup(grpName),ShowInInspector,ShowIf(nameof(type),Type.Commmand,false)]
        public Action<string[]> command;

        [LabelWidth(labelWidth),LabelText("Function"),FoldoutGroup(grpName),ShowInInspector,ShowIf(nameof(type),Type.BlockingCommand,false)]
        public Action<string[],Action> blockingCommand;
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
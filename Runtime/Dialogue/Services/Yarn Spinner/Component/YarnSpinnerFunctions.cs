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
        const string grpName = "$"+nameof(usage);
        const string grph1  = grpName + "/h1";
        const int labelWidth = 80;
        #endregion

        private string GetName()
        {
            switch(type){
                case Type.Function:
                {
                    return $"{name}(params object[{paramCount}] param)";
                }

                case Type.ReturningFunction:
                {
                    return $"object {name}(params object[{paramCount}] param)";
                }

                case Type.Commmand:
                {
                    return $"{name}(params string[] param)";
                }

                case Type.BlockingCommand:
                {
                    return $"{name}(params string[] param, Action onComplete)";
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

       // [LabelWidth(labelWidth),FoldoutGroup(grpName),ShowInInspector,DisplayAsString]
        internal string usage{
            get
            {
                var result = default(string);
                var param  = default(string[]);
                
                if(paramCount > 0)
                {
                    param  = new string[paramCount];
                    for (int i = 0; i < paramCount; i++)
                    {
                        param[i] = $"p{i+1}";
                    }
                }


                switch(type)
                {
                    case Type.Function:
                    {
                        result = $"<<call {name}({param.Join()})>>";
                        break;
                    }
                    case Type.ReturningFunction:
                    {
                        result = $"<<if {name}({param.Join()})>>";
                        break;
                    }

                    case Type.Commmand:
                    {
                        result = $"<<{name} gameObjectName p1 p2 p3 ...>>";
                        break;
                    }

                    case Type.BlockingCommand:
                    {
                        result = $"<<{name} gameObjectName p1 p2 p3 ...>>";
                        break;
                    }
                }

                return result;
            }
        }
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
        [OdinSerialize,ListDrawerSettings(Expanded = true)]
        internal List<RegisterFunction> registerFunctions = new List<RegisterFunction>();
        #endregion
    }
}
#endif
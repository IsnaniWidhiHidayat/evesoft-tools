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
        private List<RegisterFunction> registerFunctions = new List<RegisterFunction>();
        #endregion

        #region methods
        private void Start()
        {
            Init();
        }
        private async void Init()
        {
            if(registerFunctions.IsNullOrEmpty())
                return;

            var dialogue = await GetDialogue();

            var data = DialogueDataFactory.CreateYarnSpinnerData();
            foreach (var function in registerFunctions)
            {
                switch(function.type){
                    case RegisterFunction.Type.Function:
                    {
                        if(function.function.IsNull())
                            continue;

                        var func = function.function;
                        data.AddFunctions((function.name,function.paramCount,(values)=>
                        {
                            RunFunction(func,values);
                        }));
                        break;
                    }

                    case RegisterFunction.Type.ReturningFunction:
                    {
                        if(function.returnfunction.IsNull())
                            continue;

                        var func = function.returnfunction;
                        data.AddReturnFunctions((function.name,function.paramCount,(values)=>
                        {
                            return RunReturnFunction(func,values);
                        }));
                        break;
                    }
                }
             
            }

            dialogue.Add(data);
        }          
        private void RunFunction(Action<object[]> func,Value[] values)
        {
            if(values.IsNullOrEmpty())
            {
                func?.Invoke(null);
            }
            else
            {
                var result = new object[values.Length];

                for (int i = 0; i < result.Length; i++)
                {
                    switch(values[i].type)
                    {
                        case Value.Type.Bool:
                        {
                            result[i] = values[i].AsBool;
                            break;
                        }

                        case Value.Type.Null:
                        {
                            result[i] = null;
                            break;
                        }

                        case Value.Type.Number:
                        {
                            result[i] = values[i].AsNumber;
                            break;
                        }

                        case Value.Type.String:
                        {
                            result[i] = values[i].AsString;
                            break;
                        }
                    }
                }

                func?.Invoke(result);
            }
        }
        private object RunReturnFunction(Func<object[],object> func,Value[] values)
        {
            if(values.IsNullOrEmpty())
            {
                return func?.Invoke(null);
            }
            else
            {
                var result = new object[values.Length];

                for (int i = 0; i < result.Length; i++)
                {
                    switch(values[i].type)
                    {
                        case Value.Type.Bool:
                        {
                            result[i] = values[i].AsBool;
                            break;
                        }

                        case Value.Type.Null:
                        {
                            result[i] = null;
                            break;
                        }

                        case Value.Type.Number:
                        {
                            result[i] = values[i].AsNumber;
                            break;
                        }

                        case Value.Type.String:
                        {
                            result[i] = values[i].AsString;
                            break;
                        }
                    }
                }

                return func?.Invoke(result);
            }
        }
        private async Task<iDialogue> GetDialogue()
        {
            await new WaitUntil(()=> !DialogueFactory.Get(DialogueType.YarnSpinner).IsNull());
            return DialogueFactory.Get(DialogueType.YarnSpinner);
        }        
        #endregion
    }
}
#endif
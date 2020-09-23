using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace EveSoft.Localize
{
    [Serializable]
    internal class LocalizeParameter
    {
        #region Field
        [TableColumnWidth(120, Resizable = false)]
        [SerializeField]
        internal string key;

        [HideLabel]
        [SerializeField]
        internal LocalizeParameterValue value;
        #endregion
    }

    [HideReferenceObjectPicker]
    [Serializable]
    internal class LocalizeParameterValue
    {
        #region event
        internal event Action<string> OnValueChange;
        #endregion

        #region Field
        [HorizontalGroup("h1", Width = 60)]
        [HideLabel]
        [ShowInInspector]
        internal ParameterType type
        {
            get
            {
                return _type;
            }

            set
            {
                if (_type != value)
                {
                    _type = value;

                    if (OnValueChange != null)
                    {
                        OnValueChange(this.value);
                    }
                }
            }
        }

        [VerticalGroup("h1/v1")]
        [ShowIf("type", ParameterType.Const)]
        [ShowInInspector]
        [HideLabel]
        internal string constValue
        {
            get
            {
                return _constValue;
            }

            set
            {
                if (_constValue != value)
                {
                    _constValue = value;

                    if (OnValueChange != null)
                    {
                        OnValueChange(_constValue);
                    }
                }

            }
        }

        [VerticalGroup("h1/v1")]
        [ShowIf("type", ParameterType.Ref)]
        [ShowInInspector]
        [HideLabel]
        internal UnityEngine.Object refValue
        {
            get
            {
                return _refValue;
            }

            set
            {
                if (_refValue != value)
                {
                    _refValue = value;
                    if (OnValueChange != null)
                    {
                        OnValueChange(_refValue.ToString());
                    }
                }
            }
        }
        #endregion

        #region Property
        public string value
        {
            get
            {
                switch (type)
                {
                    case ParameterType.Const:
                        {

                            return _constValue;

                        }

                    case ParameterType.Ref:
                        {
                            if (_refValue != null)
                            {
                                return _refValue.ToString();
                            }
                            else
                            {
                                return string.Empty;
                            }
                        }

                    default:
                        {
                            return string.Empty;
                        }
                }
            }

            set
            {
                switch (type)
                {
                    case ParameterType.Const:
                        {
                            constValue = value;
                            break;
                        }
                }
            }
        }
        #endregion

        #region Private
        [HideInInspector]
        [SerializeField]
        private ParameterType _type;

        [HideInInspector]
        [SerializeField]
        private string _constValue = "Value";

        [HideInInspector]
        [SerializeField]
        private UnityEngine.Object _refValue;
        #endregion
    }
}
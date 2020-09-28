using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Utils
{
    public abstract class Singleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
    {
        #region Property
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                if (_instance == null)
                {
                    _instance = CreateInstance();
                }

                return _instance;
            }
        }
        protected virtual bool dontDestroy => true;
        protected virtual new HideFlags hideFlags => HideFlags.None;
        #endregion

        #region Protected
        protected static T _instance;
        //protected bool _destroyed;
        #endregion

        protected virtual void Awake()
        {
            if (dontDestroy)
            {
                if (_instance == null)
                {
                    _instance = this as T;
                    DontDestroyOnLoad(gameObject);
                    base.hideFlags = hideFlags;
                }
                else if (_instance != this)
                {
                    "destroy object {0}".LogFormat(gameObject.name);
                    Destroy(gameObject);
                    //_destroyed = true;
                }
            }
            else
            {
                if (_instance == null)
                {
                    _instance = this as T;
                }
            }
        }
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
        public static T CreateInstance()
        {
            if (_instance == null)
            {
                string name = Path.GetExtension(typeof(T).ToString()).Replace(".", "");
                _instance = new GameObject(name).AddComponent<T>();
            }

            return _instance;
        }       
    }
}

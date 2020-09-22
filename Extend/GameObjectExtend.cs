using System.Collections.Generic;
using UnityEngine;

namespace EveSoft
{
    public static class GameObjectExtend
    {
        public static T GetOrAddComponent<T>(this GameObject target, bool forceAddIfNotExist) where T : Component
        {
            var findComponent = target.GetComponent<T>();
            if (findComponent == null)
            {
                findComponent = target.AddComponent<T>();
            }

            return findComponent;
        }
        public static void Destroy(this GameObject target)
        {
            if(target.IsNull())
                return;

            if (Application.isEditor)
                Object.DestroyImmediate(target);
            else
                Object.Destroy(target);

            target = null;
        }
        public static void Destroy(this UnityEngine.Object target)
        {
            if (target.IsNull())
                return;
                
            if (Application.isEditor)
                Object.DestroyImmediate(target);
            else
                Object.Destroy(target);

            target = null;
        }
        public static void Destroy<T>(this IList<T> objs) where T : UnityEngine.Object
        {
            if(objs.IsNullOrEmpty())
                return;

            foreach (var obj in objs)
            {
                if(obj.IsNull())
                    continue;

                obj.Destroy();
            }

            objs.Clear();
        }
        public static IList<GameObject> gameObjects<T>(this IList<T> objs) where T : MonoBehaviour
        {
            if(objs.IsNullOrEmpty())
                return null;

            var result = default(List<GameObject>);
            foreach (var item in objs)
            {
                if(item.IsNull())
                    continue;

                var newObj = item.gameObject;
                if(newObj.IsNull())
                    continue;

                if(result.IsNull())
                    result = new List<GameObject>();

                result.Add(newObj);
            }

            return result;
        }
        
        public static void DestroyImmediate(this UnityEngine.Object target)
        {
            if (target.IsNull())
                return;

            UnityEngine.Object.DestroyImmediate(target);
            target = null;
        }

        public static void DontDestroyOnLoad(this GameObject obj)
        {
            UnityEngine.Object.DontDestroyOnLoad(obj);
        }
        public static void RemoveComponent<T>(this GameObject obj) where T : Component
        {
            if (obj == null)
                return;

            T component = obj.GetComponent<T>();
            if (component != null)
            {
                GameObject.Destroy(component);
            }

        }
        public static void Show(this UnityEngine.GameObject obj)
        {
            if (obj == null)
                return;

            if (!obj.activeSelf)
            {
                obj.SetActive(true);
            }
        }
        public static void Hide(this UnityEngine.GameObject obj)
        {
            if (obj == null)
                return;

            if (obj.activeSelf)
            {
                obj.SetActive(false);
            }
        }
        public static void Clear(this Transform transform)
        {
            if(transform.IsNull())
                return;
                
            if(transform.childCount > 0) 
            {
                foreach (Transform child in transform)
                {
                    if(child.IsNull())
                        continue;
    
                    child.gameObject.Destroy();
                }
            }
        }
        public static void SetObjectDirty(this UnityEngine.Object obj)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(obj);
            #endif
        }
    }
}

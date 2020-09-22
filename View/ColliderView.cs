using UnityEngine;
using Sirenix.OdinInspector;

namespace EveSoft.Views
{
    [RequireComponent(typeof(Collider))]
    [AddComponentMenu(Menu.views + "/" + nameof(ColliderView))]
    public class ColliderView : MonoBehaviour
    {
        #region Field
        [ColorPalette,HideLabel] public Color color;
        [ReadOnly] public Vector3 center;
        [ReadOnly] public Vector3 size;
        #endregion

        #region Private
        private Collider _collider;
        #endregion

        #region methods
        private void Reset()
        {
            color = Color.white;
        }     
        #endregion
        
        #region callback
        private void OnDrawGizmos()
        {
            if (_collider.IsNull())
                _collider = GetComponent<Collider>();

            if (!_collider.IsNull())
            {
                Gizmos.color = color;
                center = _collider.bounds.center;
                size = _collider.bounds.size;
                Gizmos.DrawWireCube(center, size);
            }
        }
        #endregion
    } 
}
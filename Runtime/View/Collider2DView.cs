#if ODIN_INSPECTOR 
using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Views
{
    [HideMonoScript,RequireComponent(typeof(Collider2D))]
    [AddComponentMenu(Menu.views + "/" + nameof(Collider2DView))]
    public class Collider2DView : MonoBehaviour
    {
        #region Field
        [ColorPalette,HideLabel]
        public Color color;
        #endregion

        #region Private
        private Collider2D _collider;
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
            if (_collider == null)
                _collider = GetComponent<Collider2D>();

            if (_collider != null)
            {
                var center   = _collider.bounds.center;
                var size     = _collider.bounds.size;
                Gizmos.color = color;
                Gizmos.DrawWireCube(center, size);
            }
        }  
        #endregion
    } 
}
#endif
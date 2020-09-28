#if ODIN_INSPECTOR 
using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.Views
{
    [HideMonoScript,RequireComponent(typeof(RectTransform))]
    [AddComponentMenu(Menu.views + "/" + nameof(RectTransformView))]
    public class RectTransformView : MonoBehaviour
    {
        #region field
        [ColorPalette,HideLabel]
        public Color _color = Color.white;
        #endregion

        #region private
        private Vector2 _lossyScale;
        private RectTransform _rectTransfrom;
        #endregion
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(_rectTransfrom.IsNull())
                _rectTransfrom = GetComponent<RectTransform>();

            var rect = _rectTransfrom.GetRect();
            Gizmos.color = _color;
            Gizmos.DrawWireCube(rect.Center(),rect.size);
        }  
#endif
    }
}


#endif
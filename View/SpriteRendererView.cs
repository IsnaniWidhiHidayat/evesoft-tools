using Sirenix.OdinInspector;
using UnityEngine;

namespace EveSoft.Views
{
    [HideMonoScript,RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu(Menu.views + "/" + nameof(SpriteRendererView))]
    public class SpriteRendererView : MonoBehaviour
    {
        #region const
        const string grpConfig = "Config";
        #endregion
   
        #region Field
        [ColorPalette,HideLabel]
        public Color color = Color.red;
        #endregion

        #region private
        private SpriteRenderer _renderer;
        #endregion

#if UNITY_EDITOR
        // public Rect rect;
        // public Vector2 boundSize;
        private void OnDrawGizmos()
        {
            if(_renderer.IsNull())
                _renderer = GetComponent<SpriteRenderer>();

            var rect      = _renderer.GetRect();
            //boundSize = _renderer.bounds.size;
            Gizmos.color    = color;
            Gizmos.DrawWireCube(rect.Center(),rect.size);
        }
#endif 
    }
}
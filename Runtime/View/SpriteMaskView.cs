#if ODIN_INSPECTOR 
using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.Views
{
    [HideMonoScript,RequireComponent(typeof(SpriteMask))]
    [AddComponentMenu(Menu.views + "/" + nameof(SpriteMaskView))]
    public class SpriteMaskView : MonoBehaviour
    {
        #region const
        const string grpConfig = "Config";
        #endregion

        #region private
        private SpriteMask _renderer;
        #endregion

        #region Field
        [ColorPalette,HideLabel]
        public Color color = Color.red;
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(_renderer.IsNull())
                _renderer = GetComponent<SpriteMask>();
        
            var bounds      = _renderer.bounds;
            Gizmos.color    = color;
            Gizmos.DrawWireCube(bounds.center,bounds.size);
        }
#endif 
    }
}
#endif
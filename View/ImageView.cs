using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Evesoft.Views
{
    [HideMonoScript,RequireComponent(typeof(Image))]
    [AddComponentMenu(Menu.views + "/" + nameof(ImageView))]
    public class ImageView : MonoBehaviour
    {
        #region const
        const string grpConfig = "Config";
        #endregion

        #region Field
        [ColorPalette,HideLabel]
        public Color color = Color.red;
        #endregion

        #region private
        private Image _image;
        #endregion

        private void OnDrawGizmos() 
        {
            if(_image.IsNull())
                _image = GetComponent<Image>();

            if(_image.IsNull())
                return;

            Gizmos.color = color;
            var worldRect = _image.WorldRect();
            Gizmos.DrawWireCube(worldRect.center,worldRect.size);
        }
    }
}
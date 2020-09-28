#if ODIN_INSPECTOR 
using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Views
{
    [HideMonoScript,RequireComponent(typeof(Camera))]
    [AddComponentMenu(Menu.views + "/" + nameof(CameraView))]
    public class CameraView : MonoBehaviour
    {
        #region Field
        [ColorPalette,HideLabel]
        public Color color = Color.red;
        #endregion

        #region private
        private Camera _camera;
        #endregion

        #region callback
        private void OnDrawGizmos()
        {
            if(_camera.IsNull())
                _camera = GetComponent<Camera>();

            if(_camera.IsNull())
                return;

            Gizmos.matrix = transform.localToWorldMatrix;

            if (_camera.orthographic)
            {
                Gizmos.color = color;
                Vector3 size = _camera.OrthographicSize();
                size.z =  _camera.farClipPlane - _camera.nearClipPlane;

                var position = Vector3.forward * (size.z /2f);
                position.z += _camera.nearClipPlane;
            
                Gizmos.DrawWireCube(position,size);
            }else
            {
                Gizmos.color = color;
                Gizmos.DrawFrustum(Vector3.zero, _camera.fieldOfView, _camera.farClipPlane, _camera.nearClipPlane, _camera.aspect);
            }
        }   
        #endregion
    } 
}
#endif
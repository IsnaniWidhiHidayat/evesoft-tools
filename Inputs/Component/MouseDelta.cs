using UnityEngine;
using Sirenix.OdinInspector;

namespace EveSoft.Input
{
    [HideMonoScript]
    [AddComponentMenu(Menu.input + "/" + nameof(MouseDelta))]
    public class MouseDelta : MonoBehaviour
    {
        #region const
        const string grpRuntime = "Runtime";
        #endregion

        #region Property
        [ShowInInspector,FoldoutGroup(grpRuntime)]
        public Vector2 deltaPosition
        {
            get
            {
                return _deltaPosition;
            }
        }
        #endregion

        #region Private
        private Vector2 _lastMousePos;
        private Vector2 _deltaPosition;
        #endregion

        private void LateUpdate()
        {
            _deltaPosition = (Vector2)UnityEngine.Input.mousePosition - _lastMousePos;
            _deltaPosition = new Vector2(_deltaPosition.x / (float)Screen.width, _deltaPosition.y / (float)Screen.height);
            _lastMousePos = UnityEngine.Input.mousePosition;
        }
    } 
}

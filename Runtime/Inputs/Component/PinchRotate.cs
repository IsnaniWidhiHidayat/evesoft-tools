using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Input
{
    [HideMonoScript]
    [AddComponentMenu(Menu.input + "/" + nameof(PinchRotate))]
    public class PinchRotate : MonoBehaviour
    {
        #region const
        const string grpConfig = "Config";
        const string grpRuntime = "Runtime";

        #endregion
        #region Field

        [SerializeField,FoldoutGroup(grpConfig),Range(0,2f)] 
        private float _turnRatio = Mathf.PI / 2;
        private float _angle;
        private float _angleDelta;
        #endregion

        #region Property
        public float turnRatio {
            get
            {
                return _turnRatio;
            }
        }
        
        [FoldoutGroup(grpRuntime),ShowInInspector,PropertyRange(0,360)]
        public float angle
        {
            get
            {
                return _angle;
            }
        }
        
        [FoldoutGroup(grpRuntime),ShowInInspector,PropertyRange(0,180)]
        public float angleDelta
        {
            get
            {
                return _angleDelta;
            }
        }
        #endregion

        #region Private
        private float _prevAngle;    
        #endregion

        private void LateUpdate()
        {
            if (UnityEngine.Input.touchCount != 2)
                return;

            Touch touch1 = UnityEngine.Input.touches[0];
            Touch touch2 = UnityEngine.Input.touches[1];
            _angle  = Angle(touch1.position, touch2.position);
            _prevAngle = Angle(touch1.position - touch1.deltaPosition,touch2.position - touch2.deltaPosition);
            _angleDelta = Mathf.DeltaAngle(_prevAngle, _angle) * _turnRatio;
        }
        private float Angle(Vector2 pos1, Vector2 pos2)
        {
            Vector2 from = pos2 - pos1;
            Vector2 to = new Vector2(1, 0);

            float result = Vector2.Angle(from, to);
            Vector3 cross = Vector3.Cross(from, to);

            if (cross.z > 0)
            {
                result = 360f - result;
            }

            return result;
        }

        void OnDrawGizmos() { }
    }
}
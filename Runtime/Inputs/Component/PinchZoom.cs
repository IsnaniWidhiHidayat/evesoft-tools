#if ODIN_INSPECTOR 
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Evesoft.Input
{
    [HideMonoScript,RequireComponent(typeof(Camera))]
    [AddComponentMenu(Menu.input + "/" + nameof(PinchZoom))]
    public class PinchZoom : SerializedMonoBehaviour
    {
        public enum Type{
            Pinch,
            Scroll
        }

        #region field
        const string grpConfig = "Config";
        const string grpRuntime = "Runtime";
        #endregion

        #region Field
        // [SerializeField, FoldoutGroup(grpConfig)]
        // private bool _enable = true;

        [SerializeField, FoldoutGroup(grpConfig)]
        private Type _type;

        [SerializeField, FoldoutGroup(grpConfig),ShowIf(nameof(_type),Type.Scroll)]
        private bool _invertScroll;

        [SerializeField,FoldoutGroup(grpConfig),Range(.1f,1f)] 
        private float _zoomSpeed = 0.5f;
        
        private float maxZoomRange {
            get
            {
                if(_camera.IsNull())
                    _camera = GetComponent<Camera>();

                if(_camera.IsNull())
                    _camera = Camera.main;

                return _camera.orthographicSize;
            }
        }
        [SerializeField,FoldoutGroup(grpConfig),MinMaxSlider(0,nameof(maxZoomRange))] 
        private Vector2 _zoomRange = new Vector2(0,1);
        #endregion

        #region Property
        // public bool canZoom {
        //     get => _enable;
        //     set => _enable = value;
        // }
        public new Camera camera
        {
            get
            {
                return _camera;
            }
        }
        public float zoomSpeed
        {
            get => _zoomSpeed;
            set => _zoomSpeed = value;
        }      
        public Vector2 minMaxZoom
        {
            get => _zoomRange;
            set => _zoomRange = value;
        }
        

        private float minZoom => minMaxZoom.x;
        private float maxZoom => minMaxZoom.y;

        [ShowInInspector,DisableInEditorMode,FoldoutGroup(grpRuntime),PropertyRange(nameof(minZoom),nameof(maxZoom))]
        public float zoomValue{
            get
            {
                if(_camera.IsNull())
                    return 0;

                var result = _camera.orthographic? _camera.orthographicSize : _camera.fieldOfView;
                    result = Mathf.Clamp(result,minMaxZoom.x,minMaxZoom.y);

                return  result;
            }

            set
            {
                value = Mathf.Clamp(value,minMaxZoom.x,minMaxZoom.y);
            
                if(_camera.orthographic)
                    _camera.orthographicSize  = value;
                else
                    _camera.fieldOfView = value;
            }
        }
        
        [ShowInInspector,DisableInEditorMode,FoldoutGroup(grpRuntime),PropertyRange(0f,1f)]
        public float zoomScale{
            get
            {
                var result = minMaxZoom.y - minMaxZoom.x;
                    result = (zoomValue - minMaxZoom.x) / result;
                    result = Mathf.Clamp01(result);
                return result; 
            }
            set
            {
                value = Mathf.Clamp01(value);
                var range = _zoomRange.y - _zoomRange.x;
                var zoomValue = _zoomRange.x + (value * range);
                this.zoomValue = zoomValue;
            }
        }
        #endregion
        
        public event Action<float> onZooming;

        
        #region Private
        private float _deltaMagnitudeDiff;
        private Camera _camera;
        private float _prevZoom;
        private float _initZoom;
        #endregion

        private void Start()
        {
            if (_camera == null)
                _camera = GetComponent<Camera>();

            if(_camera != null)
               _prevZoom = _camera.orthographic? _camera.orthographicSize : _camera.fieldOfView;

            _initZoom = _prevZoom;
        }
        private void Reset()
        {
            if (_camera == null)
                _camera = GetComponent<Camera>();

            if (_camera == null)
                return;
        }
        private void Update()
        {
            // if(!_enable)
            //     return;

            var inputed = false;
            // If there are two touches on the device...
            if (UnityEngine.Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = UnityEngine.Input.GetTouch(0);
                Touch touchOne = UnityEngine.Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                _deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                inputed = true;            
            }
            else if (_type == Type.Scroll && UnityEngine.Input.mouseScrollDelta.y != 0)
            {
                var scollInput = UnityEngine.Input.mouseScrollDelta.y;
                if (_invertScroll)
                    scollInput = -scollInput;

                _deltaMagnitudeDiff = scollInput * 10;  
                inputed = true;
            }

            if(!inputed)
                return;

            var zoomSize = _camera.orthographic ? _camera.orthographicSize : _camera.fieldOfView;
            zoomSize += _deltaMagnitudeDiff * _zoomSpeed;
            zoomSize = Mathf.Clamp(zoomSize ,_zoomRange.x, _zoomRange.y);

            if(_prevZoom != zoomSize)
            {
                _prevZoom = zoomSize;

                // if(!canZoom)
                //     return;

                if(_camera.orthographic)
                    _camera.orthographicSize = zoomSize;
                else
                    _camera.fieldOfView = zoomSize;

                onZooming?.Invoke(_prevZoom);
            }
        }
    
        public void ResetZoom(){
            if(_camera ==null)
                return;

            zoomValue = _initZoom;
        }
    }
}
#endif
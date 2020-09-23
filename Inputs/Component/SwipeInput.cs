using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Evesoft.Input
{
    [HideMonoScript]
    [AddComponentMenu(Menu.input + "/" + nameof(SwipeInput))]
    public class SwipeInput : Utils.Singleton<SwipeInput>
    {
        #region const
        const string grpConfig = "Config"; 
        #endregion

        #region Events
        public event Action onSwipeUp,onSwipeDown,onSwipeLeft,onSwipeRight;
        #endregion

        #region Field
        [SerializeField,FoldoutGroup(grpConfig)] private float deadZone = 80;
        #endregion

        protected override bool dontDestroy => true;
        protected override HideFlags hideFlags => HideFlags.HideInHierarchy;

        #region Private
        private bool _tap, _swipeLeft, _swipeRight, _swipeUp, _swipeDown;
        private bool _isDragging = false;
        private Vector2 _startTouch, _swipeDelta; 
        #endregion

        private void Update()
        {
            _tap = _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;

            #region Standalone Input
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _tap = true;
                _isDragging = true;
                _startTouch = UnityEngine.Input.mousePosition;
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                Reset();
            }
            #endregion

            #region Mobile Input
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.touches[0];

                if (touch.phase == TouchPhase.Began)
                {
                    _tap = true;
                    _isDragging = true;
                    _startTouch = UnityEngine.Input.touches[0].position;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _isDragging = false;
                    Reset();
                }
            }
            #endregion

            //Calculate distance
            _swipeDelta = Vector2.zero;
            if (_isDragging)
            {
                if (UnityEngine.Input.touchCount > 0)
                {
                    _swipeDelta = UnityEngine.Input.touches[0].position - _startTouch;
                }
                else
                {
                    _swipeDelta = (Vector2)UnityEngine.Input.mousePosition - _startTouch;
                }
            }

            //Did we cross the deadzone
            if (_swipeDelta.magnitude > deadZone)
            {
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //Left Or Right

                    if (x < 0)
                    {
                        _swipeLeft = true;
                        onSwipeLeft?.Invoke();
                    }
                    else
                    {
                        _swipeRight = true;
                        onSwipeRight?.Invoke();
                    }
                }
                else
                {
                    //Up or down
                    if (y < 0)
                    {
                        _swipeDown = true;
                        onSwipeDown?.Invoke();
                    }
                    else
                    {
                        _swipeUp = true;
                        onSwipeUp?.Invoke();
                    }
                }

                Reset();
            }

        }
        private void Reset()
        {
            _startTouch = _swipeDelta = Vector2.zero;
            _isDragging = false;
        }
    }
}
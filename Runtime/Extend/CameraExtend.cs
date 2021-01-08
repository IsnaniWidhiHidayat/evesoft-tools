#if ODIN_INSPECTOR 
using UnityEngine;

namespace Evesoft
{
    public static class CameraExtend
    {
        public static Rect OrthographicRect(this Camera camera)
        {
            Vector2 camSize = camera.OrthographicSize();
            var pos = camera.transform.position;
            var rect = new Rect(pos.x - (camSize.x/2f), pos.y + (camSize.y/2f), camSize.x, camSize.y);
            return rect;
        }       
        public static Vector2 OrthographicSize(this Camera camera)
        {
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            return new Vector2(width, height);
        }
        public static Vector3 WorldToScreenPoint(this Camera camera, Vector3 worldPosition, Camera targetCam)
        {
            Vector2 screenResolution = new Vector2(Screen.width, Screen.height);
            Vector2 screenPos = camera.WorldToScreenPoint(worldPosition);
            Vector2 screenPosNormalize = new Vector2(screenPos.x / (float)screenResolution.x, screenPos.y / (float)screenResolution.y);

            Vector2 UICamSize = targetCam.OrthographicSize();
            Vector3 targetPosition = targetCam.transform.position;
            targetPosition += new Vector3(UICamSize.x * screenPosNormalize.x, UICamSize.y * screenPosNormalize.y, 1);
            targetPosition -= new Vector3(UICamSize.x / 2f, UICamSize.y / 2f);
            return targetPosition;
        }
    }
}

#endif
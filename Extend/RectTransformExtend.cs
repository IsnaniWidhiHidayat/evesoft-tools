using UnityEngine;

namespace Evesoft
{
    public static class RectTransformExtend
    {
        private static Vector3[] _corners = new Vector3[4]; 

        public static bool IsOverlaps(this RectTransform source,RectTransform target)
        {
            if(target.IsNull())
                return false;

            source.GetWorldCorners(_corners);
            var size = new Vector2(_corners[2].x - _corners[1].x,_corners[2].y - _corners[0].y);
            var center = new Vector2(_corners[2].x - (size.x /2f),_corners[2].y - (size.y/2f));
            var sourceRect = new Rect(center - (size * 0.5f),size); 

            target.GetWorldCorners(_corners);
            size = new Vector2(_corners[2].x - _corners[1].x,_corners[2].y - _corners[0].y);
            center = new Vector2(_corners[2].x - (size.x /2f),_corners[2].y - (size.y/2f));
            var targetRect = new Rect(center - (size * 0.5f),size); 
          
            return sourceRect.Overlaps(targetRect);
        }
        public static Rect GetRect(this RectTransform source, Camera camera = null)
        {
           if(camera.IsNull()) 
           {
                var size = source.rect.size;
                size.x *= source.lossyScale.x;
                size.y *= source.lossyScale.y;
    
                var pos = source.rect.position;
                    pos.x = source.transform.position.x + (pos.x * source.lossyScale.x);
                    pos.y = source.transform.position.y + (pos.y * source.lossyScale.y + size.y);
                return new Rect(pos.x,pos.y,size.x,size.y);
           }
           else
           {
                var uiCamera = source.transform.root.GetComponent<Canvas>().worldCamera;
                var viewRect = source.GetRect();
                var offsetX  = Mathf.Abs(viewRect.Center().x - uiCamera.OrthographicRect().Center().x);
                var scale    = camera.orthographicSize  / uiCamera.orthographicSize;
                    offsetX  = offsetX * scale;

                var width    = viewRect.width * scale;
                var height   = viewRect.height * scale;
                var camRect  = camera.OrthographicRect();
                return new Rect(camRect.x + (offsetX*2),camRect.y + camRect.height,width,height);
           }
        }         
    }
}


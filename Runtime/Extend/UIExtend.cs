using  UnityEngine;
using   UnityEngine.UI;

namespace Evesoft
{
    public static class UIExtend
    {
        public static Rect WorldRect(this Image image)
        {
            if(image.preserveAspect)
            {
                var spriteRect = image.sprite.rect;
                var aspectRatio = spriteRect.width / spriteRect.height; 
                var rootscale = image.transform.root.localScale;
                var oldRect = image.rectTransform.rect;
                var newSize = new Vector2(oldRect.size.x * rootscale.x,oldRect.size.y * rootscale.y);
            
                if(aspectRatio < 1)
                {
                    newSize.x = newSize.y * aspectRatio;
                }
                else
                {
                    newSize.y = newSize.x * aspectRatio;
                }
              
                var newPos = new Vector2(image.transform.position.x - newSize.x/2f,image.transform.position.y - newSize.y/2f);
                var newRect = new Rect(newPos.x,newPos.y,newSize.x,newSize.y);
                return newRect;
            }
            else
            {
                var rootScale = image.transform.root.localScale;
                var oldRect = image.rectTransform.rect;
                var newSize = new Vector2(oldRect.size.x * rootScale.x,oldRect.size.y * rootScale.y);
                var newPos = new Vector2(image.transform.position.x - newSize.x/2f,image.transform.position.y - newSize.y/2f);
                var newRect = new Rect(newPos.x,newPos.y,newSize.x,newSize.y);
                return newRect;
            }
        }
    }
}
using UnityEngine;

namespace EveSoft
{
    public static class RectExtend
    {
        public static Rect ToTextureScale(this Rect vector,int scalePixelPerUnit = 100)
        {
            return new Rect(vector.x * scalePixelPerUnit,vector.y * scalePixelPerUnit,vector.width * scalePixelPerUnit,vector.height * scalePixelPerUnit);
        }
        public static Rect FromTextureScale(this Rect vector,int scalePixelPerUnit = 100)
        {
            return new Rect(vector.x / scalePixelPerUnit,vector.y / scalePixelPerUnit,vector.width / scalePixelPerUnit,vector.height / scalePixelPerUnit);
        }
        public static Vector2 LeftUpCorner(this Rect rect,bool invert = false)
        {
            return new Vector2(rect.x,rect.y);
        }
        public static Vector2 RightUpCorner(this Rect rect,bool invert = false)
        {
            return new Vector2(rect.x + rect.width,rect.y);
        }
        public static Vector2 LeftBottomCorner(this Rect rect,bool invert = false)
        {
            var y = invert ? rect.y + rect.height : rect.y - rect.height;
            return new Vector2(rect.x,y);
        }
        public static Vector2 RightBottomCorner(this Rect rect,bool invert = false)
        {
            var y = invert ? rect.y + rect.height : rect.y - rect.height;
            return new Vector2(rect.x + rect.width,y);
        }
        public static Vector2 Center(this Rect rect)
        {
            return (rect.LeftUpCorner() + rect.RightBottomCorner())/2f;
        }
        public static Vector2 CenterInvert(this Rect rect)
        {
            return rect.center - Vector2.up * rect.height;
        }
        public static Rect InvertY(this Rect rect)
        {
            var result = new Rect(rect);
                result.center = result.center - Vector2.up * result.height;
            return result;
        }       
    }
}
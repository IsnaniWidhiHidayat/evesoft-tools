using UnityEngine;

namespace Evesoft
{
    public static class VectorExtend
    {
        public static Vector2Int ToTextureScale(this Vector2 vector,int scalePixelPerUnit = 100,bool abs = true)
        {
            var x = vector.x * scalePixelPerUnit;
            var y = vector.y * scalePixelPerUnit;

            if(abs) 
            {
                x = Mathf.Abs(x);
                y = Mathf.Abs(y);
            }

            return new Vector2Int(Mathf.RoundToInt(x),Mathf.RoundToInt(y));
        }      
        public static Vector2 FromTextureScale(this Vector2Int vector,int scalePixelPerUnit = 100)
        {
            var x = vector.x / (float)scalePixelPerUnit;
            var y = vector.y / (float)scalePixelPerUnit;
            return new Vector2(x,y);
        }
        public static Vector2 FromTextureScale(this Vector2 vector,int scalePixelPerUnit = 100)
        {
            var x = vector.x / (float)scalePixelPerUnit;
            var y = vector.y / (float)scalePixelPerUnit;
            return new Vector2(x,y);
        }
    }
}
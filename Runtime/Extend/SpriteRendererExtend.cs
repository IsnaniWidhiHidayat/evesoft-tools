using UnityEngine;

namespace Evesoft
{
    public static class SpriteRendererExtend
    {
        public static Rect GetRect(this SpriteRenderer renderer)
        {
            if(renderer.IsNull())
                return default(Rect);

            var size = renderer.bounds.size;
            var pos = renderer.transform.position;
            pos.x -= size.x /2f;
            pos.y += size.y /2f;

            return new Rect(pos,size);
        }

       
    }
}
using UnityEngine;

namespace EveSoft
{
    public static class SpriteMaskExtend
    {
        public static Rect GetRect(this SpriteMask mask)
        {
            if(mask.IsNull())
                return default(Rect);

            var size = mask.bounds.size;
            var pos = mask.transform.position;
            pos.x -= size.x /2f;
            pos.y += size.y /2f;

            return new Rect(pos,size);
        }
    }
}
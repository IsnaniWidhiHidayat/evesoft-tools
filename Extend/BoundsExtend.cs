using UnityEngine;

namespace EveSoft
{
    public static class BoundsExtend
    {
        public static Rect GetRect(this Bounds target)
        {
            return new Rect(target.min.x, target.min.y, target.size.x, target.size.y);
        }

        public static Rect GetRect(this Bounds target, Vector2 offset)
        {
            return new Rect(target.min.x + offset.x, target.min.y + offset.y, target.size.x, target.size.y);
        }
    }
}

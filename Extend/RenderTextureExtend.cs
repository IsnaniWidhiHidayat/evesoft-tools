using UnityEngine;

namespace EveSoft
{
    public static class RenderTextureExtend
    {
        public static Texture2D ToTexture2D(this RenderTexture renderTexture,bool mipmap = false)
        {
            RenderTexture.active = renderTexture;
            var texture = new Texture2D(renderTexture.width,renderTexture.height,TextureFormat.RGBA32,mipmap);
                texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                texture.Apply();

            return texture;
        }
    }
}
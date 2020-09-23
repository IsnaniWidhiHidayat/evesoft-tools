
using UnityEngine;

namespace Evesoft.Utils
{
    public static class GradientFactory 
    {
        public static Gradient Create(params Color[] colors)
        {
            if(colors.IsNullOrEmpty())
                return GradientFactory.Create(Color.clear,Color.clear);

            if(colors.Length == 1)
                return GradientFactory.Create(colors[0],colors[0]);

            var step = 1f/(colors.Length - 1);
            var result = new Gradient();
    
            var colorKeys = new GradientColorKey[colors.Length];
            var alphaKeys = new GradientAlphaKey[colors.Length];
            
            for (int i = 0; i < colorKeys.Length; i++)
            {
                var time = i*step;
                colorKeys[i] = new GradientColorKey(colors[i],time);
                alphaKeys[i] = new GradientAlphaKey(colors[i].a,time);
            }
            
            result.SetKeys(colorKeys,alphaKeys);   
            return result;
        }
    }
}
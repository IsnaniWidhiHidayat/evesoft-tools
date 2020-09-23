using UnityEngine;
using System.Collections.Generic;
using System;

namespace Evesoft.Ads
{
    public interface iAdsConfig
    {
        IDictionary<string,object> configs{get;}
        T GetConfig<T>(string key);
    }
}


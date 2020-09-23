using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Ads
{
    public static class AdConfigFactory
    {
        public static iAdsConfig CreateAdmobConfig()
        {
            return new Admob.AdmobConfig();
        }
    }
}


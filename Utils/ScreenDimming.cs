using UnityEngine;
using Sirenix.OdinInspector;

namespace EveSoft.Utils
{
    [HideMonoScript]
    [AddComponentMenu(Menu.utils + "/ScreenDimming")]
    internal class ScreenDimming : MonoBehaviour
    {
        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        void OnDrawGizmos() { }
    } 
}

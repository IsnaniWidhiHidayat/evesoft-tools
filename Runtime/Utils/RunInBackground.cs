#if ODIN_INSPECTOR 
using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Utils
{
    [HideMonoScript]
    [AddComponentMenu(Menu.utils + "/" + nameof(RunInBackground))]
    internal class RunInBackground : MonoBehaviour
    {
        private void Start()
        {
            Application.runInBackground = true;
        }

        void OnDrawGizmos() { }
    }

}
#endif
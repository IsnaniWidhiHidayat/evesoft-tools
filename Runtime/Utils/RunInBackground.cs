using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Utils
{
    [HideMonoScript]
    [AddComponentMenu(Menu.utils + "/RunInBackground")]
    internal class RunInBackground : MonoBehaviour
    {
        private void Start()
        {
            Application.runInBackground = true;
        }

        void OnDrawGizmos() { }
    }

}
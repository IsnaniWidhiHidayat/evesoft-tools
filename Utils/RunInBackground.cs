using UnityEngine;
using Sirenix.OdinInspector;

namespace EveSoft.Utils
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
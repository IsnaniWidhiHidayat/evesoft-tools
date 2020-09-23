using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Utils
{
    [HideMonoScript]
    [AddComponentMenu(Menu.utils + "/TargetFPS")]
    public class TargetFPS : MonoBehaviour
    {     
        public enum TargetFPSType
        {
            Mobile,
            Console,
        }

        #region Field
        public TargetFPSType targetFps;
        #endregion

        private void Start()
        {
            switch (targetFps)
            {
                case TargetFPSType.Console:
                    {
                        Application.targetFrameRate = int.MaxValue;
                        break;
                    }

                case TargetFPSType.Mobile:
                    {
                        Application.targetFrameRate = int.MaxValue;
                        break;
                    }
            }
        }
    } 
}

using UnityEngine;
using Sirenix.OdinInspector; 

namespace EveSoft.Views
{
    [HideMonoScript]
    [AddComponentMenu(Menu.views + "/" + nameof(FPSView))]
    public class FPSView : MonoBehaviour
    {
        #region const
        const string grpConfig = "Config";
        #endregion

        #region Field
        [FoldoutGroup(grpConfig),ColorPalette,HideLabel] public Color color = Color.red;
        [FoldoutGroup(grpConfig)] public int size = 20;
        #endregion

        #region Private
        private float deltaTime = 0.0f;
        private GUIStyle style = new GUIStyle();
        private Rect rect;
        #endregion

        private void Start()
        {
            int w = Screen.width, h = Screen.height;
            rect = new Rect(0, 0, w, h * 2 / 100);

            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = size;
            style.normal.textColor = color;
        }
        private void Update()
        {
            deltaTime += (UnityEngine.Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }
        private void OnGUI()
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }

        void OnDrawGizmos() { }
    } 
}
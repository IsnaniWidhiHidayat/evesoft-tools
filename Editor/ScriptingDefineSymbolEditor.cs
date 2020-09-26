using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Evesoft.Editor
{
    public class ScriptingDefineSymbolEditor : OdinEditorWindow
    {
        #region const
        const string path  = "Tools/EveSoft/Browser/Scripting Define Symbol";
        #endregion

        #region static
        [MenuItem(path)]
        public static void ShowWindow()
        {
            var window = GetWindow<ScriptingDefineSymbolEditor>();
            window.Refresh();
            window.Show();
        }

        public static string[] GetDefineSymbol()
        {
            var platfrom = EditorUserBuildSettings.selectedBuildTargetGroup;
            var str =  PlayerSettings.GetScriptingDefineSymbolsForGroup(platfrom);
            return str.SplitBy(';');
        }
        public static void AddDefineSymbol(params string[] symbols)
        {
            if(symbols.IsNullOrEmpty())
                return;

            var predefine = GetDefineSymbol();
            foreach (var symbol in symbols)
            {
                if(ArrayUtility.Contains(predefine,symbol))
                    continue;

                ArrayUtility.Add(ref predefine,symbol);
            }

            SaveDefineSymbol(predefine);
        }
        public static void RemoveDefineSymbol(params string[] symbols)
        {
            if(symbols.IsNullOrEmpty())
                return;

            var predefine = GetDefineSymbol();
            foreach (var symbol in symbols)
                ArrayUtility.Remove(ref predefine,symbol);

            SaveDefineSymbol(predefine);
        }
        public static void SaveDefineSymbol(params string[] symbols)
        {
            var platfrom = EditorUserBuildSettings.selectedBuildTargetGroup;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(platfrom,symbols.Join(";"));
        }
        #endregion

        [ShowInInspector,ShowIf(nameof(_symbols))]
        private string[] _symbols;

        [Button,HorizontalGroup,ShowIf(nameof(_symbols))]
        private void Refresh()
        {
            _symbols = GetDefineSymbol();
        }
        
        [Button,HorizontalGroup,ShowIf(nameof(_symbols))]
        private void Save()
        {
            SaveDefineSymbol(_symbols);
        }
    }
}
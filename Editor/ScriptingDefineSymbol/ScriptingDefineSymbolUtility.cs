using System.Collections.Generic;
using UnityEditor;

namespace Evesoft.Editor.ScriptingDefineSymbol
{
    public static class ScriptingDefineSymbolUtility
    {
        public static IList<string> GetDefineSymbol()
        {
            var platfrom = EditorUserBuildSettings.selectedBuildTargetGroup;
            var str =  PlayerSettings.GetScriptingDefineSymbolsForGroup(platfrom);
            return new List<string>(str.SplitBy(';'));
        }
        public static void AddDefineSymbol(IList<string> symbols)
        {
            if(symbols.IsNullOrEmpty())
                return;

            var predefine = GetDefineSymbol();
            foreach (var symbol in symbols)
            {
                if(predefine.Contains(symbol))
                    continue;

                predefine.Add(symbol);
            }

            SaveDefineSymbol(predefine);
        }
        public static void RemoveDefineSymbol(IList<string> symbols)
        {
            if(symbols.IsNullOrEmpty())
                return;

            var predefine = GetDefineSymbol();
            foreach (var symbol in symbols)
                predefine.Remove(symbol);

            SaveDefineSymbol(predefine);
        }
        public static void SaveDefineSymbol(IList<string> symbols)
        {
            var platfrom = EditorUserBuildSettings.selectedBuildTargetGroup;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(platfrom,symbols.Join(";"));
        }
        public static bool ContainSymbol(IList<string> symbols)
        {
            if(symbols.IsNullOrEmpty())
                return false;

            var predefine = GetDefineSymbol();
            var contain = true;
            foreach (var symbol in symbols)
            {
                contain &= predefine.Contains(symbol);

                if(!contain)
                    break;
            }

            return contain;
        }
        
    }
}
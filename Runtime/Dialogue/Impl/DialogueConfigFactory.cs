using System.Collections.Generic;

namespace Evesoft.Dialogue
{
    public static class DialogueConfigFactory
    {
        #if YARN_SPINNER
        public static iDialogueConfig CreateYarnSpinnerConfig(iDialogueUI ui = null,string startNode = null,bool startAuto = false,IList<YarnProgram> yarnScripts = null)
        {
           return new YarnSpinner.YarnSpinnerConfig(ui,startNode,startAuto,yarnScripts);
        }   
        #endif 
    }
}
namespace Evesoft.Dialogue
{
    public static class DialogueDataFactory
    {
        #if YARN_SPINNER
        public static iDialogueData CreateYarnSpinnerData(YarnProgram script,
        (string,Yarn.Unity.DialogueRunner.CommandHandler) commanHandler,
        (string,int,Yarn.Function) function,
        (string,int,Yarn.ReturningFunction) returningFunction)
        {
            return new YarnSpinner.YarnSpinnerData(script,commanHandler,function,returningFunction);
        }   
        
        public static iDialogueData CreateYarnSpinnerData(string removeCommandHandlerName,string removeFunctionName)
        {
            return new YarnSpinner.YarnSpinnerData(removeCommandHandlerName,removeFunctionName);
        }
        #endif 
    }
}
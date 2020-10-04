namespace Evesoft.Dialogue
{
    public static class DialogueDataFactory
    {
        #if YARN_SPINNER
        public static YarnSpinner.YarnSpinnerData CreateYarnSpinnerData()
        {
            return new YarnSpinner.YarnSpinnerData();
        }
        #endif 
    }
}
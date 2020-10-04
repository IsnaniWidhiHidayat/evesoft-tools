#if ODIN_INSPECTOR && YARN_SPINNER
using System;
using Yarn.Unity;

namespace Evesoft.Dialogue.YarnSpinner
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class YarnSpinnerCommandAttribute : YarnCommandAttribute
    {
        public YarnSpinnerCommandAttribute(string commandString) : base(commandString)
        {
        }
    }
}
#endif
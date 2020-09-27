using System;
using System.Reflection;

namespace Evesoft.Editor
{
    public static class NamespaceUtility
    {
        public static bool IsNamespaceExists(string desiredNamespace)
        {
            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(Type type in assembly.GetTypes())
                {
                    if (type.Namespace == desiredNamespace)
                        return true;
                }
            }
            return false;
        }
    }
}
using UnityEngine;

namespace Evesoft
{
    public static class Clipboard
    {
        public static string PasteFromClipboard(this string text)
        {
            return GUIUtility.systemCopyBuffer;
        }
        public static void CopyToClipboard(this string text)
        {
            GUIUtility.systemCopyBuffer = text;
        }
    }
}

#if ODIN_INSPECTOR 
namespace Evesoft
{
    public static class Menu
    {
        private const string separator = "/";
        public const string root = nameof(Evesoft);
        public const string utils = root + separator + nameof(Evesoft.Utils);
        public const string input = root + separator + nameof(Evesoft.Input);
        public const string views = root + separator + nameof(Evesoft.Views);
        #if LOCALIZE
        public const string localize = root + separator + nameof(Evesoft.Localize);
        #endif
        public const string Dialogue = root + separator + nameof(Dialogue);
    }
}
#endif
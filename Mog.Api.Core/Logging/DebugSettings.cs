namespace Mog.Api.Core.Logging
{
    public class DebugSettings
    {
        public static bool IsDebugging =>
#if DEBUG 
            true;
#else
            false;
#endif
    }
}
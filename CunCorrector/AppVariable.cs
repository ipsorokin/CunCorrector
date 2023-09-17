using System.Collections.Generic;

namespace CunCorrector
{
    internal static class AppVariable
    {
        public static Dictionary<string, string> HardwareConstants { get; } = new Dictionary<string, string>
        {
            {"51E9FC49-F2F0-46D2-A243-C5C9C3F83956", "КУН-2Д"},
            {"CBAD15FE-E91A-4127-B6EA-9A3D18AC2CE9", "КУН-2Д1"},
            {"AEACD3B8-A1E7-4666-A5D6-74C0AC2B7BB2", "КУН-IP4"},
            {"57A985E3-40D5-462B-A20E-3E4BF283C88A", "КУН-IP8"},
            {"F7A432E3-6D09-484A-AB44-81AFF91793D5", "КИО"},
            {"13A036F8-BF68-4542-9C7B-57277C9D6B43", "КИО-2М"},
            {"33B80C00-DEB0-41F2-B995-76942AE8FF19", "USB-Пульт"},
        };

        public static Dictionary<string, string> ConcentratorClasses { get; } = new Dictionary<string, string>
        {
            {"51E9FC49-F2F0-46D2-A243-C5C9C3F83956", "КУН-2Д"},
            {"CBAD15FE-E91A-4127-B6EA-9A3D18AC2CE9", "КУН-2Д1"},
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalFundingDev
{
    public static class FileDirectoryHelper
    {
        public static String GetTempDirectory()
        {
#if DEBUG
            return @"\\IGSKIACWVMGS018\dev-siftaroot\Temp\";
#else
            return @"\\IGSKIACWVMGS018\siftaroot\Temp\";
#endif
        }
        public static String GetAgreementDirectory(int AgreementID)
        {
#if DEBUG
            return String.Format(@"\\IGSKIACWVMGS018\dev-siftaroot\Documents\Agreements\{0}\", AgreementID);
#else
            return String.Format(@"\\IGSKIACWVMGS018\siftaroot\Documents\Agreements\{0}\", AgreementID);
#endif
        }
    }
}
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
            return @"\\IGSKIACWVMi03\dev-siftaroot\Temp\";
#else
            return @"\\IGSKIACWVMi01\siftaroot\Temp\";
#endif
        }
        public static String GetAgreementDirectory(int AgreementID)
        {
#if DEBUG
            return String.Format(@"\\IGSKIACWVMi03\dev-siftaroot\Documents\Agreements\{0}\", AgreementID);
#else
            return String.Format(@"\\IGSKIACWVMi01\siftaroot\Documents\Agreements\{0}\", AgreementID);
#endif
        }
    }
}
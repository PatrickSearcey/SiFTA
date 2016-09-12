using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalFundingDev
{
    interface IDocument
    {
        /// <summary>
        /// Uses the current http context to download the document
        /// </summary>
        void DownloadDocument();
        /// <summary>
        /// Saves the Document to a designated path.
        /// </summary>
        /// <param name="DestinationPath">A string of the desired path for the document to be saved too</param>
        void SaveDocument(String DestinationPath);
    }
}

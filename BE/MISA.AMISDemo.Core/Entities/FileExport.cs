using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class FileExport
    {
        #region Fields
        /// <summary>
        /// File Stream
        /// </summary>
        public MemoryStream FileStream { get; set; }

        /// <summary>
        /// Nội dung file
        /// </summary>
        public string FileContent { get; set; }

        /// <summary>
        /// Tên file
        /// </summary>
        public string FileName { get; set; }
        #endregion

        #region Constructor
        public FileExport(MemoryStream fileStream, string fileContent, string fileName)
        {
            FileStream = fileStream;
            FileContent = fileContent;
            FileName = fileName;
        }
        #endregion
    }
}

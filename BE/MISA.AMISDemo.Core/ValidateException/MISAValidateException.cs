using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.ValidateException
{
    public class MISAValidateException : Exception
    {
        #region Field
        string? msgValidateException = null;
        #endregion

        #region Contructor
        public MISAValidateException(string msg)
        {
            this.msgValidateException = msg;
        }
        #endregion

        #region Methods
        //Ghi đè phương thức của Exception
        public override string Message
        {
            get
            {
                return msgValidateException;
            }
        }
        #endregion
    }
}

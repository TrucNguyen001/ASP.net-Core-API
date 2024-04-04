using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.ValidateException
{
    public class MISAValidateExceptionAuth : Exception
    {
        #region Field
        string? msgValidateExceptionUser = null;
        string? msgValidateExceptionDev = null;
        #endregion

        #region Contructor
        public MISAValidateExceptionAuth(string msgUser, string msgDev)
        {
            this.msgValidateExceptionUser = msgUser;
            this.msgValidateExceptionDev = msgDev;
        }
        #endregion

        #region Methods
        //Ghi đè phương thức của Exception
        public override string Message
        {
            get
            {
                return $"{MISAResourceVN.User} {msgValidateExceptionUser}, {MISAResourceVN.Dev} {msgValidateExceptionDev}";
            }
        }
        #endregion
    }
}

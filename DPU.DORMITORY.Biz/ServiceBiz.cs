using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_M_SERVICE
    {
        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public decimal Amount { get; set; }
        #endregion
    }

}

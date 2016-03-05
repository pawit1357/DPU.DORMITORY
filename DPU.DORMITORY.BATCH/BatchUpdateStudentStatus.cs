using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using Gen_Bapizarfi_01_Bapizcmi003;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DPU.DORMITORY.BATCH
{
    public class BatchUpdateStudentStatus
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_CUSTOMER> repCustomer;
        private SAPBiz sapBiz;
        
        public BatchUpdateStudentStatus()
        {
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
            sapBiz = new SAPBiz();
        }

        public void Start()
        {
            List<String> errorList = new List<string>();
            List<TB_CUSTOMER> listCustomer = repCustomer.Table.ToList();
            foreach(TB_CUSTOMER cus in listCustomer)
            {
                ZSTD_INFOTable result = sapBiz.getStudentInfo(cus.CUSTOMER_NUMBER);
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        ZSTD_INFO info = result[0];
                        if (info != null)
                        {
                            TB_CUSTOMER _updateCus = repCustomer.GetById(cus.ID);
                            _updateCus.STD_STATUS = info.Status;
                            repCustomer.Update(_updateCus);
                            cus.STD_STATUS = info.Status;
                        }
                    }
                    else
                    {
                        errorList.Add(String.Format("ไม่พบข้อมูล {0} ใน SLCM", cus.CUSTOMER_NUMBER));
                        continue;
                    }
                }
            }
        }
    }
}

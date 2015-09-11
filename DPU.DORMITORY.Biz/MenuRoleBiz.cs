using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class MENU_ROLE
    {
        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<MENU_ROLE> menuRoleRepo;

        public MENU_ROLE getRoleByUserId(string _userId)
        {
            //using (var ctx = new DORMEntities())
            //{
            //    return (from mr in ctx.MENU_ROLE
            //            join ur in ctx.USERS_ROLE on mr.ROLE_ID equals ur.ROLE_ID
            //            where ur.ROLE_ID 
            //            select mr).FirstOrDefault();

            //}
            return null;
        }

        public List<MENU_ROLE> getRoleListByRoleId(int _roleId)
        {
            menuRoleRepo = unitOfWork.Repository<MENU_ROLE>();
            return menuRoleRepo.Table.Where(x => x.ROLE_ID == _roleId).ToList();

        }



        public List<int> getMenuByRole(int _roleId)
        {
            menuRoleRepo = unitOfWork.Repository<MENU_ROLE>();
            List<int> result = new List<int>();

            IEnumerable<MENU_ROLE> menuRoles = menuRoleRepo.Table.ToList().Where(x => x.ROLE_ID == _roleId);
            foreach (MENU_ROLE _menuRole in menuRoles)
            {
                result.Add(_menuRole.MENU_ID);
            }

            return result;
        }


        public void InsertList(List<MENU_ROLE> _lists)
        {
            foreach (MENU_ROLE tmp in _lists)
            {
                switch (tmp.RowState)
                {
                    case CommandNameEnum.Add:
                        menuRoleRepo.Insert(tmp);
                        break;
                    case CommandNameEnum.Edit:
                      MENU_ROLE mRole =   menuRoleRepo.GetById(tmp.ROLE_ID);
                      if (mRole != null)
                      {
                          mRole.MENU_ID = tmp.MENU_ID;
                          mRole.IS_REQUIRED_ACTION = tmp.IS_REQUIRED_ACTION;
                          mRole.IS_CREATE = tmp.IS_CREATE;
                          mRole.IS_EDIT = tmp.IS_EDIT;
                          mRole.IS_DELETE = tmp.IS_DELETE;
                          menuRoleRepo.Update(mRole);
                      }
                        break;
                    case CommandNameEnum.Delete:
                         MENU_ROLE mRoleDelete =   menuRoleRepo.GetById(tmp.ROLE_ID);
                         if (mRoleDelete != null)
                         {
                             menuRoleRepo.Delete(mRoleDelete);
                         }
                        break;
                }
            }
        }


    }
}

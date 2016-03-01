using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Biz
{
    public partial class MenuBiz
    {
        public List<string> nav = new List<string>();

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<MENU> menuRep;

        public MenuBiz()
        {
            menuRep = unitOfWork.Repository<MENU>();
        }
        public string getMenuByRole(List<int> _menuByRole, string _currentPage)
        {
            StringBuilder result = new StringBuilder();

            _currentPage = Path.GetFileName(_currentPage);

            IEnumerable<MENU> menus = menuRep.Table.ToList().Where(x => _menuByRole.Contains(x.MENU_ID));

            foreach (MENU _menu in menus.Where(x => x.PREVIOUS_MENU_ID == null).OrderBy(x => x.DISPLAY_ORDER))
            {
                if (nav.Count > 0)
                {
                    if (nav[0].Equals(_menu.MENU_NAME))
                    {
                        result.Append("<li class=\"start active open\">");
                    }
                    else
                    {
                        result.Append("<li>");
                    }
                }
                else
                {
                    result.Append("<li>");
                }
                result.Append("<a href=\"javascript:;\" runat=\"server\">");
                result.Append("    <i class=\"" + _menu.MENU_ICON + "\"></i>");
                result.Append("    <span class=\"title\">" + _menu.MENU_NAME + "</span>");
                result.Append("    <span class=\"selected\"></span>");
                result.Append("    <span class=\"arrow\"></span>");
                result.Append("</a>");
                IEnumerable<MENU> menuChilds = menus.Where(x => x.PREVIOUS_MENU_ID == _menu.MENU_ID).OrderBy(x => x.DISPLAY_ORDER);
                if (menuChilds != null)
                {
                    result.Append("<ul class=\"sub-menu\">");
                    foreach (MENU _childmenu in menuChilds)
                    {
                        if (nav.Count > 0)
                        {
                            if (nav[1].Equals(_childmenu.MENU_NAME))
                            {
                                result.Append("<li class=\"active\">");
                            }
                            else
                            {
                                result.Append("<li>");
                            }
                        }
                        else
                        {
                            result.Append("<li>");
                        }
                        result.Append("     <a href=\"" + _childmenu.URL_NAVIGATE + "\">");
                        result.Append("     <i class=\"" + _childmenu.MENU_ICON + "\"></i>");
                        result.Append("" + _childmenu.MENU_NAME + "</a>");
                        result.Append("</li>");
                    }
                    result.Append("</ul>");
                }
                result.Append("</li>");
            }


            return result.ToString();
            //StringBuilder result = new StringBuilder();

            //IEnumerable<MENU> menus = menuRep.Table.ToList().Where(x => _menuByRole.Contains(x.MENU_ID));

            //foreach (MENU _menu in menus.Where(x => x.PREVIOUS_MENU_ID == null).OrderBy(x => x.DISPLAY_ORDER))
            //{
            //    //result.Append("<li class=\"start active open\">");
            //    result.Append("<li>");
            //    result.Append("<a href=\"javascript:;\" runat=\"server\">");
            //    result.Append("    <i class=\"" + _menu.MENU_ICON + "\"></i>");
            //    result.Append("    <span class=\"title\">" + _menu.MENU_NAME + "</span>");
            //    result.Append("    <span class=\"selected\"></span>");
            //    result.Append("    <span class=\"arrow\"></span>");
            //    result.Append("</a>");
            //    IEnumerable<MENU> menuChilds = menus.Where(x => x.PREVIOUS_MENU_ID == _menu.MENU_ID).OrderBy(x => x.DISPLAY_ORDER);
            //    if (menuChilds != null)
            //    {
            //        result.Append("<ul class=\"sub-menu\">");
            //        foreach (MENU _childMenu in menuChilds)
            //        {
            //            //result.Append("<li class=\"active\">");
            //            result.Append("<li>");
            //            result.Append("     <a href=\"" + _childMenu.URL_NAVIGATE + "\">");
            //            result.Append("     <i class=\"" + _childMenu.MENU_ICON + "\"></i>");
            //            result.Append("" + _childMenu.MENU_NAME + "</a>");
            //            result.Append("</li>");
            //        }
            //        result.Append("</ul>");
            //    }
            //    result.Append("</li>");
            //}


            //return result.ToString();
        }

        public void getMenuByTree(ref TreeView tv, List<MENU_ROLE> roles)
        {
            IEnumerable<MENU> menus = menuRep.Table.ToList();

            foreach (MENU _menu in menus.Where(x => x.PREVIOUS_MENU_ID == null).OrderBy(x => x.DISPLAY_ORDER))
            {
                TreeNode root = new TreeNode(_menu.MENU_NAME, _menu.MENU_ID.ToString());
                IEnumerable<MENU> menuChilds = menus.Where(x => x.PREVIOUS_MENU_ID == _menu.MENU_ID).OrderBy(x => x.DISPLAY_ORDER);
                if (menuChilds != null)
                {
                    foreach (MENU _childMenu in menuChilds)
                    {
                        TreeNode child = new TreeNode(_childMenu.MENU_NAME, _childMenu.MENU_ID.ToString());
                        child.ShowCheckBox = true;
                        MENU_ROLE menuRole = roles.Where(x => x.MENU_ID == _childMenu.MENU_ID).FirstOrDefault();
                        if (menuRole != null)
                        {
                            child.Expanded = !((bool)menuRole.IS_CREATE && (bool)menuRole.IS_DELETE && (bool)menuRole.IS_EDIT);
                            child.Checked = true;
                            foreach (MenuRoleActionEnum val in Enum.GetValues(typeof(MenuRoleActionEnum)))
                            {
                                TreeNode childLevel1 = new TreeNode(val.ToString(), menuRole.MENU_ID.ToString());
                                childLevel1.ShowCheckBox = true;
                                switch (val)
                                {
                                    case MenuRoleActionEnum.Add:
                                        childLevel1.Checked = (bool)menuRole.IS_CREATE;
                                        break;
                                    case MenuRoleActionEnum.Delete:
                                        childLevel1.Checked = (bool)menuRole.IS_DELETE;
                                        break;
                                    case MenuRoleActionEnum.Edit:
                                        childLevel1.Checked = (bool)menuRole.IS_EDIT;
                                        break;
                                }
                                child.ChildNodes.Add(childLevel1);
                            }
                        }
                        else
                        {
                            //

                            foreach (MenuRoleActionEnum val in Enum.GetValues(typeof(MenuRoleActionEnum)))
                            {
                                TreeNode childLevel1 = new TreeNode(val.ToString(), _childMenu.MENU_ID.ToString());
                                childLevel1.ShowCheckBox = true;
                                child.ChildNodes.Add(childLevel1);
                            }
                        }
                        root.ChildNodes.Add(child);
                    }
                    tv.Nodes.Add(root);
                }
            }
            Console.WriteLine("");
        }

        public string getNavigator(string _currentPage)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append("<i class=\"fa fa-home\"></i>");
            sb.Append("<a href=\"" + Constants.LINK_SEARCH_CUSTOMER + "\">Home</a>");
            sb.Append("</li>");

            _currentPage = Path.GetFileName(_currentPage);
            MENU child = menuRep.Table.ToList().Where(x => Path.GetFileName(x.URL_NAVIGATE) == _currentPage).FirstOrDefault();
            if (child != null)
            {
                MENU parent = menuRep.Table.ToList().Where(x => x.MENU_ID == child.PREVIOUS_MENU_ID).FirstOrDefault();
                if (parent != null)
                {

                    sb.Append("<li>");
                    sb.Append("<i class=\"fa fa-angle-right\"></i>");
                    sb.Append("<a href=\"#\">" + parent.MENU_NAME + "</a>");
                    sb.Append("</li>");
                    nav.Add(parent.MENU_NAME);
                }
                sb.Append("<li>");
                sb.Append("<i class=\"fa fa-angle-right\"></i>");
                sb.Append("<a href=\"#\">" + child.MENU_NAME + "</a>");
                sb.Append("</li>");
                nav.Add(child.MENU_NAME);
            }

            return sb.ToString();

        }
        public string getCurrentMenuName(string _currentPage)
        {

            String menuName = String.Empty;
            _currentPage = Path.GetFileName(_currentPage);
            MENU child = menuRep.Table.ToList().Where(x => Path.GetFileName(x.URL_NAVIGATE) == _currentPage).FirstOrDefault();
            if (child != null)
            {
                menuName = child.MENU_NAME;
            }

            return menuName;
        }

        public void getmenuByTree(ref TreeView tv, List<MENU_ROLE> roles)
        {
            using (var ctx = new DORMEntities())
            {

                IEnumerable<MENU> menus = menuRep.Table.ToList();

                foreach (MENU _menu in menus.Where(x => x.PREVIOUS_MENU_ID == null).OrderBy(x => x.DISPLAY_ORDER))
                {
                    TreeNode root = new TreeNode(_menu.MENU_NAME, _menu.MENU_ID.ToString());
                    IEnumerable<MENU> menuChilds = menus.Where(x => x.PREVIOUS_MENU_ID == _menu.MENU_ID).OrderBy(x => x.DISPLAY_ORDER);
                    if (menuChilds != null)
                    {
                        foreach (MENU _childmenu in menuChilds)
                        {
                            TreeNode child = new TreeNode(_childmenu.MENU_NAME, _childmenu.MENU_ID.ToString());
                            child.ShowCheckBox = true;
                            MENU_ROLE menuRole = roles.Where(x => x.MENU_ID == _childmenu.MENU_ID).FirstOrDefault();
                            if (menuRole != null)
                            {
                                child.Expanded = !((bool)menuRole.IS_CREATE && (bool)menuRole.IS_DELETE && (bool)menuRole.IS_EDIT);
                                child.Checked = true;
                                foreach (MenuRoleActionEnum val in Enum.GetValues(typeof(MenuRoleActionEnum)))
                                {
                                    TreeNode childLevel1 = new TreeNode(val.ToString(), menuRole.MENU_ID.ToString());
                                    childLevel1.ShowCheckBox = true;
                                    switch (val)
                                    {
                                        case MenuRoleActionEnum.Add:
                                            childLevel1.Checked = (bool)menuRole.IS_CREATE;
                                            break;
                                        case MenuRoleActionEnum.Delete:
                                            childLevel1.Checked = (bool)menuRole.IS_DELETE;
                                            break;
                                        case MenuRoleActionEnum.Edit:
                                            childLevel1.Checked = (bool)menuRole.IS_EDIT;
                                            break;
                                    }
                                    child.ChildNodes.Add(childLevel1);
                                }
                            }
                            else
                            {
                                //

                                foreach (MenuRoleActionEnum val in Enum.GetValues(typeof(MenuRoleActionEnum)))
                                {
                                    TreeNode childLevel1 = new TreeNode(val.ToString(), _childmenu.MENU_ID.ToString());
                                    childLevel1.ShowCheckBox = true;
                                    child.ChildNodes.Add(childLevel1);
                                }
                            }
                            root.ChildNodes.Add(child);
                        }
                        tv.Nodes.Add(root);
                    }
                }
                Console.WriteLine("");
            }
        }


    }
}

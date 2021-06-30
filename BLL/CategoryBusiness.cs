using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public partial class CategoryBusiness : ICategoryBusiness
    {
        private IICategoryRepository _res;
        public CategoryBusiness(IICategoryRepository CategoryRes)
        {
            _res = CategoryRes; 
        }

        public List<CategoryModel> GetData()
        {
            var allCategory = _res.GetData();
            var lstParent = allCategory.Where(ds => ds.parent_category_id == null).OrderBy(s => s.seq_num).ToList();
            foreach (var item in lstParent)
            {
                item.children = GetHiearchyList(allCategory, item);
            }
            return lstParent;
        }
        public List<CategoryModel> GetHiearchyList(List<CategoryModel> lstAll, CategoryModel node)
        {
            var lstChilds = lstAll.Where(ds => ds.parent_category_id == node.category_id).ToList();
            if (lstChilds.Count == 0)
                return null;
            for (int i = 0; i < lstChilds.Count; i++)
            {
                var childs = GetHiearchyList(lstAll, lstChilds[i]);
                lstChilds[i].type = (childs == null || childs.Count == 0) ? "leaf" : "";
                lstChilds[i].children = childs;
            }
            return lstChilds.OrderBy(s => s.category_id).ToList();
        }

        public bool Create(CategoryModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }

        public bool Update(CategoryModel model)
        {
            return _res.Update(model);
        }

        public CategoryModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }

        public List<CategoryModel> TimKiem(int pageIndex, int pageSize, out long total, string category_name)
        {
            return _res.TimKiem(pageIndex, pageSize, out total, category_name);
        }


    }
}

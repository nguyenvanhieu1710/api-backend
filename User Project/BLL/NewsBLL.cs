using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NewsBLL : INewsBLL
    {
        private INewsDAL _INewsDAL;
        public NewsBLL(INewsDAL InterfaceNewsDAL)
        {
            _INewsDAL = InterfaceNewsDAL;
        }

        public NewsModel GetDataById(int id)
        {
            return _INewsDAL.GetDataById(id);
        }

        public List<NewsModel> GetAll()
        {
            return _INewsDAL.GetAll();
        }

        public bool Create(NewsModel newsModel)
        {
            return _INewsDAL.Create(newsModel);
        }

        public bool Update(NewsModel newsModel)
        {
            return _INewsDAL.Update(newsModel);
        }
        public bool Delete(int id)
        {
            return _INewsDAL.Delete(id);
        }

        public List<NewsModel> Search(string name)
        {
            return _INewsDAL.Search(name);
        }
        public List<NewsModel> Pagination(int pageNumber, int pageSize)
        {
            return _INewsDAL.Pagination(pageNumber, pageSize);
        }

        public List<NewsModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _INewsDAL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}

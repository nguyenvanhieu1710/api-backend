using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface INewsBLL
    {
        NewsModel GetDataById(int id);
        List<NewsModel> GetAll();
        bool Create(NewsModel newsModel);
        bool Update(NewsModel newsModel);
        bool Delete(int id);
        List<NewsModel> Search(string name);
        List<NewsModel> Pagination(int pageNumber, int pageSize);
        List<NewsModel> GetDataDeletedPagination(int pageNumber, int pageSize);
        List<NewsModel> SearchAndPagination(int pageNumber, int pageSize, string name);
    }
}

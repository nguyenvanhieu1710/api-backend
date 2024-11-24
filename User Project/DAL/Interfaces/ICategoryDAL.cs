using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL.Interfaces
{
    public interface ICategoryDAL
    {
        CategoryModel GetDataById(int id);
        List<CategoryModel> GetAll();
        List<CategoryModel> Search(string name);
    }
}

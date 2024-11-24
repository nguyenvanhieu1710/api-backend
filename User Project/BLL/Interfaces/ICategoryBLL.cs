using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryBLL
    {
        CategoryModel GetDataById(int id);
        List<CategoryModel> GetAll();
        List<CategoryModel> Search(string name);
    }
}

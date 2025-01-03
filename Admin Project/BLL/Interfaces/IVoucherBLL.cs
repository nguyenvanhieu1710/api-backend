﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IVoucherBLL
    {
        VoucherModel GetDataById(int id);
        List<VoucherModel> GetAll();
        bool Create(VoucherModel voucherModel);
        bool Update(VoucherModel voucherModel);
        bool Delete(int id);
        List<VoucherModel> Search(string name);
        List<VoucherModel> Pagination(int pageNumber, int pageSize);
        List<VoucherModel> GetDataDeletedPagination(int pageNumber, int pageSize);
        List<VoucherModel> SearchAndPagination(int pageNumber, int pageSize, string name);
    }
}

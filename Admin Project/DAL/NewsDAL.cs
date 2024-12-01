using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NewsDAL : INewsDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public NewsDAL(IDatabaseHelper dbhelper)
        {
            _IDatabaseHelper = dbhelper;
        }

        public List<NewsModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_news_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<NewsModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NewsModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_news_get_data_by_id",
                    "@news_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<NewsModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(NewsModel newsModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_news_create",
                    "@news_Name", newsModel.NewsName,
                    "@news_Content", newsModel.Content,
                    "@news_NewsImage", newsModel.NewsImage,
                    "@news_PostingDate", newsModel.PostingDate,
                    "@news_PersonPostingId", newsModel.PersonPostingId);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_news_delete",
                    "@news_Id", id);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(NewsModel newsModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_news_update",
                    "@news_Id", newsModel.NewsId,
                    "@news_Name", newsModel.NewsName,
                    "@news_Content", newsModel.Content,
                    "@news_NewsImage", newsModel.NewsImage,
                    "@news_PostingDate", newsModel.PostingDate,
                    "@news_PersonPostingId", newsModel.PersonPostingId,
                    "@news_Deleted", newsModel.Deleted);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NewsModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_news_search",
                    "@news_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<NewsModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NewsModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_news_pagination",
                    "@news_pageNumber", pageNumber,
                    "@news_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<NewsModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NewsModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_news_deleted_pagination",
                    "@news_pageNumber", pageNumber,
                    "@news_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<NewsModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NewsModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_news_search_pagination",
                    "@news_pageNumber", pageNumber,
                    "@news_pageSize", pageSize,
                    "@news_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<NewsModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

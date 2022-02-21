using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWeb.Models.Interface
{
    public interface IMoviesRepository
    {
        /// <summary>
        /// Index清單列表
        /// </summary>
        /// <param name="movieGenre">電影類別</param>
        /// <param name="searchString">電影名稱-搜尋字串</param>
        /// <param name="sortOrder">排序</param>
        /// <returns></returns>
        IQueryable<Movie> GetAll(string movieGenre, string searchString, string sortOrder); //使用IQueryable會請求資料庫伺服器做篩選後再回傳結果
        
        /// <summary>
        /// 電影類別-下拉選單
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GenreLst();
    }
}

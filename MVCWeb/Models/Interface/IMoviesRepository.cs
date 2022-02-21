using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWeb.Models.Interface
{
    public interface IMoviesRepository: IRepository<Movie>
    {
        IQueryable<Movie> GetAll(string movieGenre, string searchString, string sortOrder); //使用IQueryable會請求資料庫伺服器做篩選後再回傳結果

        IQueryable<string> GenreLst();
    }
}

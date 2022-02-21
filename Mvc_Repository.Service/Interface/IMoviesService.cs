using Mvc_Repository.Service.Misc;
using MVCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc_Repository.Service.Interface
{
    public interface IMoviesService
    {
        IResult Create(Movie instance);

        IResult Update(Movie instance);

        IResult Delete(int ID);

        bool IsExists(int ID);

        Movie GetByID(int categoryID);

        IQueryable<Movie> GetAll(string movieGenre, string searchString, string sortOrder); //使用IQueryable會請求資料庫伺服器做篩選後再回傳結果

        IQueryable<string> GenreLst();
    }
}

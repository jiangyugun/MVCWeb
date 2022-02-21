using MVCWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MVCWeb.Models.Repository
{
    public class MoviesRepository : GenericRepository<Movie>, IMoviesRepository
    {
        /// <summary>
        /// Index清單列表
        /// </summary>
        /// <param name="movieGenre">電影類別</param>
        /// <param name="searchString">電影名稱-搜尋字串</param>
        /// <param name="sortOrder">排序</param>
        /// <returns></returns>
        public IQueryable<Movie> GetAll(string movieGenre, string searchString, string sortOrder)
        {
            var movies = this.GetByAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            switch (sortOrder)
            {
                case "電影名稱":
                    movies = movies.OrderBy(s => s.Title);
                    break;
                case "發布日期":
                    movies = movies.OrderBy(s => s.ReleaseDate);
                    break;
                default:
                    movies = movies.OrderBy(s => s.Title);
                    break;
            }
            return movies;
        }

        /// <summary>
        /// 電影類別-下拉選單
        /// </summary>
        /// <returns></returns>
        public IQueryable<string> GenreLst()
        {
            var GenreQry = this.GetByAll().Select(d => d.Genre).Distinct();
            return GenreQry;
        }
    }
}
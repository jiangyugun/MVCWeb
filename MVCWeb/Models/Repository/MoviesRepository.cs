using MVCWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MVCWeb.Models.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        protected MovieDBContext db { get; private set; }

        public MoviesRepository()
        {
            this.db = new MovieDBContext();
        }

        public IQueryable<Movie> GetAll(string movieGenre, string searchString, string sortOrder)
        {
            var movies = from m in db.Movies select m;

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

        public IQueryable<string> GenreLst()
        {
            var GenreQry = from d in db.Movies orderby d.Genre select d.Genre;
            return GenreQry;
        }
    }
}
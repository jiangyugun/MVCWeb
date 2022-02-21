using MVCWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWeb.Models.Repository
{
    public class MoviesRepository : IMoviesRepository, IDisposable
    {
        protected MovieDBContext db { get; private set; }

        public MoviesRepository()
        {
            this.db = new MovieDBContext();
        }

        public void Create(Movie instance)
        {
            if (instance == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                db.Movies.Add(instance);
                this.SaveChanges();
            }
        }
        public void Update(Movie instance)
        {
            if (instance == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                db.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Delete(Movie instance)
        {
            if (instance == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public Movie Get(int ID)
        {
            //First、FirstOrDefault 運算子
            //回傳來源序列中的第一個元素，或符合條件的第一個元素
            return db.Movies.FirstOrDefault(x => x.ID == ID);
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

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}
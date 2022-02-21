using Mvc_Repository.Service.Interface;
using Mvc_Repository.Service.Misc;
using MVCWeb.Models;
using MVCWeb.Models.Interface;
using MVCWeb.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc_Repository.Service
{
    public class MoviesService : IMoviesService
    {
        private IRepository<Movie> _repository;
        public MoviesService(IRepository<Movie> repository)
        {
            this._repository = repository;
        }
        public IResult Create(Movie instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this._repository.Create(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Update(Movie instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this._repository.Update(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int ID)
        {
            IResult result = new Result(false);

            if (!this.IsExists(ID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(ID);
                this._repository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(int ID)
        {
            return this._repository.GetByAll().Any(x => x.ID == ID);
        }

        public Movie GetByID(int ID)
        {
            return this._repository.Get(x => x.ID == ID);
        }

        /// <summary>
        /// Index清單列表
        /// </summary>
        /// <param name="movieGenre">電影類別</param>
        /// <param name="searchString">電影名稱-搜尋字串</param>
        /// <param name="sortOrder">排序</param>
        /// <returns></returns>
        public IQueryable<Movie> GetAll(string movieGenre, string searchString, string sortOrder)
        {
            var movies = _repository.GetByAll();

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
            var GenreQry = _repository.GetByAll().Select(d => d.Genre).Distinct();
            return GenreQry;
        }
    }
}

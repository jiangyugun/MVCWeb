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

        IEnumerable<Movie> GetAll();
    }
}

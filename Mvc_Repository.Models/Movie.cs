using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MVCWeb.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "電影名稱")]
        public string Title { get; set; }

        [Display(Name = "發布日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        [Display(Name = "電影類型")]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Display(Name = "價錢")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [StringLength(5)]
        [Display(Name = "評分")]
        public string Rating { get; set; }
    }

    public class MovieDBContext : DbContext
    {
        public IDbSet<Movie> Movies { get; set; }
    }
}
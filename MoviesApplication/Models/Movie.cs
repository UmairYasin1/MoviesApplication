using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesApplication.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int GenreId { get; set; }

        public string Duration { get; set; }
        public string Rating { get; set; }
    }
}
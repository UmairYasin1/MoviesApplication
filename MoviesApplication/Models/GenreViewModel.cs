using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesApplication.Models
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
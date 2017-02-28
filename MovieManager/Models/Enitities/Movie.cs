using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MovieManager.Common.Enums;

namespace MovieManager.Models.Entities
{
    public class Movie
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]        
        public int ReleaseYear { get; set; }
        [Required]    
        public MovieRating Rating { get; set; }
    }
}

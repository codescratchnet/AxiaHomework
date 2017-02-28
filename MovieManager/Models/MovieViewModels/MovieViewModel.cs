using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManager.Common.Enums;
using MovieManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Models.MovieViewModels
{
    public class MovieViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public MovieRating CurrentRating { get; set; }

        /// <summary>
        ///This property will hold a movie rating, selected by user
        /// </summary>
        [Required]
        [Display(Name = "Selected Movie Rating")]
        public string SelectedMovieRating { get; set; }

        /// <summary>
        /// This property will hold all available movie rating for selection
        /// </summary>        
        public IEnumerable<SelectListItem> MovieRatingOptions { get; set; }
    }
}

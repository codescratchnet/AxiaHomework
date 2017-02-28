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
    public class CreateMovieViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int ReleaseYear { get; set; } = DateTime.Now.Year;
        
        public MovieRating CurrentMovieRating { get; set; }

        /// <summary>
        ///This property will hold a movie rating, selected by user
        /// </summary>
        [Required]
        public string SelectedMovieRating { get; set; }

        /// <summary>
        /// This property will hold all available movie rating for selection
        /// </summary>            
        public IEnumerable<SelectListItem> MovieRatingOptions { get; set; }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Models.Entities;
using MovieManager.Models.MovieViewModels;
using MovieManager.Common.ListItems;
using MovieManager.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using MovieManager.Common.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace MovieManager.Controllers
{

    /// <summary>
    /// This controller for movie managment
    /// </summary>    
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movie
        /// <summary>
        /// Get All Movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Movie")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: List of Movie
        /// <summary>
        /// Get List of All Movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Movie/List")]
        public async Task<IActionResult> List()
        {
            return new JsonResult(await _context.Movies.ToListAsync());
        }

        // GET: Movie/Details/5
        /// <summary>
        /// Get Movie Detail by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Movie/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        /// <summary>
        /// View of Movie Creation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Movie/Create")]
        public IActionResult Create()
        {
            var model = new CreateMovieViewModel();

            //Get Movie Rating
            var movieRatings = ListItemData.GetMovieRatingItems();
            model.MovieRatingOptions = ListItemData.GetSelectListItems(movieRatings);

            return View(model);
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates a Movie.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model">model</param>
        /// <returns>New Created Movie Item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Movie/Create")]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = null, Description = "Redirict to Movie List")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(CreateMovieViewModel))]
        [Produces("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Create([FromForm]CreateMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                MovieRating selectedMovieRating = (MovieRating)Enum.Parse(typeof(MovieRating), model.SelectedMovieRating);
                Movie movie = new Movie
                {
                    Title = model.Title,
                    ReleaseYear = model.ReleaseYear,
                    Rating = selectedMovieRating
                };

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Movie/Edit/5
        /// <summary>
        /// View of Movie Editing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Movie/Edit/{Id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(MovieViewModel), Description = "Edit view of Movie")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = null, Description = "Movie not found")]
        //[Produces("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            //Get Edit MovieVideModel
            var model = new MovieViewModel();
            model.ID = movie.ID;
            model.Title = movie.Title;
            model.CurrentRating = movie.Rating;
            model.ReleaseYear = movie.ReleaseYear;

            //Get Movie Rating
            var movieRatings = ListItemData.GetMovieRatingItems();
            model.MovieRatingOptions = ListItemData.GetSelectListItems(movieRatings);
            var selectedOption = model.MovieRatingOptions.Single(e => e.Value == ((int)movie.Rating).ToString());
            selectedOption.Selected = true;

            return View(model);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit Movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Movie/Edit/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = null, Description = "Redirect to The Movie List")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = null)]
        [Produces("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Edit(int id, [FromForm] MovieViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
                    if (movie == null)
                    {
                        return NotFound();
                    }

                    MovieRating selectedMovieRating = (MovieRating)Enum.Parse(typeof(MovieRating), model.SelectedMovieRating);

                    movie.Title = model.Title;
                    movie.ReleaseYear = model.ReleaseYear;
                    movie.Rating = selectedMovieRating;

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(model.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Movie/Delete/5
        /// <summary>
        /// View of Movie Deletion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Movie/Delete/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = null, Description = "Delete View of Movie")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = null)]
        [Authorize(Roles = UserRoleConst.ADMIN_ROLE + "," + UserRoleConst.MANAGER_ROLE)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        /// <summary>
        /// Delete Movie by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoleConst.ADMIN_ROLE + "," + UserRoleConst.MANAGER_ROLE)]
        [Route("Movie/DeleteConfirmed/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = null, Description = "Redirect to the movie list")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = null)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}

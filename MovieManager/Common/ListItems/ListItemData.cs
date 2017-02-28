using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieManager.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManager.Common.Enums;

namespace MovieManager.Common.ListItems
{
    public static class ListItemData
    {
        public static Dictionary<string, string> GetUserRoleItems()
        {
            var userRoleItems = new Dictionary<string, string>();
            userRoleItems.Add(((int)UserRole.ADMINISTRATOR).ToString(), UserRole.ADMINISTRATOR.ToString());
            userRoleItems.Add(((int)UserRole.MANAGER).ToString(), UserRole.MANAGER.ToString());
            userRoleItems.Add(((int)UserRole.TEAMLEADER).ToString(), UserRole.TEAMLEADER.ToString());
            userRoleItems.Add(((int)UserRole.FLOORSTAFF).ToString(), UserRole.FLOORSTAFF.ToString());

            return userRoleItems;
        }

        public static Dictionary<string, string> GetMovieRatingItems()
        {
            var movieRatingItems = new Dictionary<string, string>();
            movieRatingItems.Add(((int)MovieRating.G).ToString(), MovieRating.G.ToString());
            movieRatingItems.Add(((int)MovieRating.M).ToString(), MovieRating.M.ToString());
            movieRatingItems.Add(((int)MovieRating.MA).ToString(), MovieRating.MA.ToString());
            movieRatingItems.Add(((int)MovieRating.PG).ToString(), MovieRating.PG.ToString());
            movieRatingItems.Add(((int)MovieRating.R).ToString(), MovieRating.R.ToString());

            return movieRatingItems;
        }

        public static IEnumerable<SelectListItem> GetSelectListItems(Dictionary<string, string> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Key,
                    Text = element.Value
                });
            }
            return selectList;
        }
    }

}

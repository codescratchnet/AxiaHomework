using MovieManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Models.ManageViewModels
{
    public class ManageUserViewModel
    {
        public IList<ManageUser> Users { get; set; }
    }
}

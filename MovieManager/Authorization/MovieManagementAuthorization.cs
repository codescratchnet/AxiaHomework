using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MovieManager.Models;

namespace MovieManager.Authorization
{
    public class MovieManagementAuthorizationRequirement : IAuthorizationRequirement
    {
        //public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }        
    }

    //public class MovieManagementAuthorization : AuthorizationHandler<MovieManagementAuthorizationRequirement>
    //{
    //    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MovieManagementAuthorizationRequirement requirement)
    //    {
    //        Movie movie = context.Resource as Movie;


    //        return Task.CompletedTask;
    //    }
    //}
}

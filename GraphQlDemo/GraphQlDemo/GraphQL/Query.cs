using GraphQlDemo.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlDemo.GraphQL
{
    public class Query
    {
        private IMovieService _moviesService;

        public Query(IMovieService moviesService)
        {
            _moviesService = moviesService;
        }

        [UsePaging(typeof(MovieType))]
        [UseFiltering]
        public IQueryable<Movie> Movies => _moviesService.GetAll();
    }
}
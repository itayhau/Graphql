using GraphQlDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlDemo.GraphQL
{
    // sample use: 
    /*mutation {
        createCompany(inputCompany: {
            name: "Tnuva",
            revenue: 210000
        })
        {
            id
            name
            revenue
        }
    }
    */

    public class Mutation
    {
        private readonly IMovieService _movieService;

        public Mutation(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public Movie CreateMovie(CreateMovieInput inputMovie)
        {
            return _movieService.Create(inputMovie);
        }

        // sample use: 
        /*mutation {
            createCompany(inputCompany: {
                name: "Tnuva",
                revenue: 210000
            })
            {
                id
                name
                revenue
            }
        }
        */
        //test
        public Movie DeleteMovie(DeleteMovieInput inputMovie)
        {
            return _movieService.Delete(inputMovie);
        }

        // sample use:
        /*
          mutation {
              deleteCompany(inputCompany: {
                id: 71
              })
              {
                id
                name
                revenue
              }
            }
         */
    }
}
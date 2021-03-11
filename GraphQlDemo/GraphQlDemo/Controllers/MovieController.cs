using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQlDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphQlDemo.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public async Task<ActionResult> Index()
        {
            var query = @"
                query { 
                    movies {
                        nodes {
                            id
                            title
                            price
                        }
                    }
                }
            ";
            HttpClient client = new HttpClient { BaseAddress = new Uri(BackEndConstants.GraphQLUrl) };
            var response = await client.GetStringAsync($"?query={query}");
            var json = JObject.Parse(response);
            var moviesJson = json["data"]["movies"]["nodes"];
            List<Movie> movies = new List<Movie>();
            foreach (var obj in moviesJson)
            {
                movies.Add(new Movie()
                {
                    Id = int.Parse(obj["id"].ToString()),
                    Title = obj["title"].ToString(),
                    Price = double.Parse(obj["price"].ToString())
                });
            }
            return View(movies);
        }

        [HttpPost]
        public async Task<ActionResult> Runquery(string query)
        {
            /*
            var query = @"
                query { 
                    movies {
                        nodes {
                            id
                            title
                            price
                        }
                    }
                }
            ";
            */
            HttpClient client = new HttpClient { BaseAddress = new Uri(BackEndConstants.GraphQLUrl) };
            var response = await client.GetStringAsync($"?query={query}");
            return Json(response);
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }


        // TODO Implement Create
        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            string mutation = $@"
                mutation {{
                    createMovie(inputMovie: {{
                        title: ""{collection["title"]}"",
                        price: {collection["price"]}
                    }})
                    {{
                        id
                        title
                        price
                    }}
                }}
            ";

            GraphQLHttpClient client = new GraphQLHttpClient(BackEndConstants.GraphQLUrl, new NewtonsoftJsonSerializer());
            GraphQLHttpRequest request = new GraphQLHttpRequest(mutation);
            

            try
            {
                await client.SendMutationAsync<Movie>(request);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await DeletePost(id);
            return RedirectToAction("Index");
        }

        // POST: Company/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int id)
        {
            string mutation = @$"
                        mutation {{
                            deleteMovie(inputMovie: {{
                                id: {id}
                            }})
                            {{
                                id
                                title
                                price
                            }}
                        }}
            ";

            GraphQLHttpClient client = new GraphQLHttpClient(BackEndConstants.GraphQLUrl, new NewtonsoftJsonSerializer());
            GraphQLHttpRequest request = new GraphQLHttpRequest(mutation);
            await client.SendMutationAsync<Movie>(request);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
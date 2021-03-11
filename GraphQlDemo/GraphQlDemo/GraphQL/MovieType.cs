using GraphQlDemo.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlDemo.GraphQL
{
    public class MovieType : ObjectType<Movie>
    {
        protected override void Configure(IObjectTypeDescriptor<Movie> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Title).Type<StringType>();
            descriptor.Field(a => a.Price).Type<DecimalType>();
        }
    }
}
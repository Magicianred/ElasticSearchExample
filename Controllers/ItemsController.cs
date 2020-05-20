using ElasticSearchExample.DAL;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;  
using System.Collections.Generic;

namespace ElasticSearchExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly ConnectionSettings connSettings =
        new ConnectionSettings(new Uri("http://localhost:9200/"))
                        .DefaultIndex("esearchitems")
                        .DefaultMappingFor<Item>(m => m
                        .PropertyName(p => p.Id, "id")
            );

        private static readonly ElasticClient elasticClient = new ElasticClient(connSettings);
        public ItemsController()
        {

            if (!elasticClient.Indices.Exists("esearchitems").Exists)
            {
                elasticClient.Indices.Create("esearchitems",
                     index => index.Map<Item>(
                          x => x
                         .AutoMap()
                  ));

                elasticClient.Bulk(b => b
                  .Index("esearchitems")
                  .IndexMany(ItemService.GetItems())
                   );
            }
        }

        [HttpGet]
        public List<Item> Get()
        {
            var response = elasticClient.Search<Item>(i => i
           .Query(q => q.MatchAll())
           .PostFilter(f => f.Range(r => r.Field(fi => fi.Price).GreaterThan(1)))
            );
            List<Item> items = new List<Item>();
            foreach (var item in response.Documents)
                items.Add(item);
            return items;
        }
    }
}
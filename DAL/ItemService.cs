using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchExample.DAL
{
    public static class ItemService
    {
        public static Item[] GetItems()
        {
            Item[] items = new[]
            {
                new Item
                {
                    Id = 1,
                    Quantity =1,
                    Name="Item 1",
                    Description="Item1 Desc",
                    Price=1
                },
                new Item
                {
                    Id = 2,
                    Quantity =2,
                    Name="Item 2",
                    Description="Item2 Desc",
                    Price=2
                },
                new Item
                {
                    Id = 3,
                    Quantity = 3,
                    Name="Item 3",
                    Description="Item3 Desc",
                    Price=3
                }
            };
            return items;
        }
    }
}

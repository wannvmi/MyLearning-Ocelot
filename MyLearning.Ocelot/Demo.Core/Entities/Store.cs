using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Entities
{
    public class Store : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }

        public DateTime CreateTime { get; set; }

        public int IsDelete { get; set; } = 0;
    }
}

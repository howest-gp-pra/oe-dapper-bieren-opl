using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Pra.Beers.Core.Entities
{
    [Table("BeerTypes")]
    public class BeerType
    {
        private string type;

        [Key]
        public int Id { get; set; }

        public string Type
        {
            get { return type; }
            set
            {
                value = value.Trim();
                if (value.Length > 50)
                    value = value.Substring(0, 50);
                type = value;
            }
        }
        public BeerType(string type)
        {
            Type = type;
        }

        internal BeerType(int id, string type) : this(type)
        {
            Id = id;
        }

        public override string ToString()
        {
            return type;
        }
    }
}

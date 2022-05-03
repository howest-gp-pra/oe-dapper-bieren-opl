using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Pra.Beers.Core.Entities
{
    [Table("Beers")]
    public class Beer
    {
        private string name;

        [Key]
        public int Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                value = value.Trim();
                if (value.Length > 50)
                    value = value.Substring(0, 50);
                name = value;
            }
        }
        public int BeerTypeID{get;set;}
        public float Alcohol { get; set; }
        public int Score { get; set; }

        public Beer(string name, int beerTypeID, float alcohol, int score)
        {
            Name = name;
            BeerTypeID = beerTypeID;
            Alcohol = alcohol;
            Score = score;
        }

        internal Beer(int id, string name, int beerTypeId, float alcohol, int score)
            :this(name, beerTypeId, alcohol, score)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Pra.Bieren.Core.Entities
{
    [Table("Bieren")]
    public class Bier
    {
        private string naam;

        [Key]
        public int Id { get; set; }

        public string Naam
        {
            get { return naam; }
            set
            {
                value = value.Trim();
                if (value.Length > 50)
                    value = value.Substring(0, 50);
                naam = value;
            }
        }
        public int BierSoortId{get;set;}
        public float Alcohol { get; set; }
        public int Score { get; set; }

        public Bier(string naam, int bierSoortId, float alcohol, int score)
        {
            Naam = naam;
            BierSoortId = bierSoortId;
            Alcohol = alcohol;
            Score = score;
        }
        internal Bier(int id, string naam, int bierSoortId, float alcohol, int score)
            :this(naam, bierSoortId, alcohol, score)
        {
            Id = id;
        }
        public override string ToString()
        {
            return Naam;
        }

    }
}

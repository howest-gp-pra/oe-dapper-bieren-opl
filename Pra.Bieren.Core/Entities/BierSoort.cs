using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Pra.Bieren.Core.Entities
{
    [Table("BierSoorten")]
    public class BierSoort
    {
        private string soort;

        [Key]
        public int Id { get; set; }
        public string Soort
        {
            get { return soort; }
            set
            {
                value = value.Trim();
                if (value.Length > 50)
                    value = value.Substring(0, 50);
                soort = value;
            }
        }
        public BierSoort(string soort)
        {
            Soort = soort;
        }
        internal BierSoort(int id, string soort):this(soort)
        {
            Id = id;
        }
        public override string ToString()
        {
            return soort;
        }
    }
}

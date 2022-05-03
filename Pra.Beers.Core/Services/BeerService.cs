using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;
using Pra.Beers.Core.Entities;

namespace Pra.Beers.Core.Services
{
    public class BeerService
    {
        private string connectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=praBeers; Integrated security=true;";
        
        // READ ALL RECORDS
        public List<BeerType> GetBeerTypes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            { 
                try 
                { 
                    connection.Open(); 
                    List<BeerType> beerTypes = connection.GetAll<BeerType>().ToList();
                    beerTypes = beerTypes.OrderBy(beerType => beerType.Type).ToList(); 
                    return beerTypes; 
                } 
                catch 
                { 
                    return null; 
                } 
            }
        }

        public List<Beer> GetBeers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<Beer> beers = connection.GetAll<Beer>().ToList();
                    beers = beers.OrderBy(beer => beer.Name).ToList();
                    return beers;
                }
                catch
                {
                    return null;
                }
            }
        }

        // INSERT
        public int AddBeer(Beer bier)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    int id = (int)connection.Insert(bier);
                    return id;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int AddBeerType(BeerType beerType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    int id = (int)connection.Insert(beerType);
                    return id;
                }
                catch
                {
                    return 0;
                }
            }
        }

        // UPDATE
        public bool UpdateBeer(Beer beer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Update(beer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateBeerType(BeerType beerType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Update(beerType);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // DELETED
        public bool DeleteBeer(Beer beer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Delete(beer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteBeerType(BeerType beerType)
        {
            if (IsBeerTypeInUse(beerType))
                return false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Delete(beerType);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private bool IsBeerTypeInUse(BeerType beerType)
        {
            string sql = "select count(*) from beers where beertypeid = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int count = connection.ExecuteScalar<int>(sql, beerType);
                return count > 0;
            }
        }

    }
}

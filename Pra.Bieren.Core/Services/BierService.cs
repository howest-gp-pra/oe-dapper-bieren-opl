using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;
using Pra.Bieren.Core.Entities;

namespace Pra.Bieren.Core.Services
{
    public class BierService
    {
        private string CS = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=praBieren; Integrated security=true;";
        
        // READ ALL RECORDS
        public List<BierSoort> GetBierSoorten()
        {
            using (SqlConnection connection = new SqlConnection(CS)) 
            { 
                try 
                { 
                    connection.Open(); 
                    List<BierSoort> biersoorten = connection.GetAll<BierSoort>().ToList();
                    biersoorten = biersoorten.OrderBy(p => p.Soort).ToList(); 
                    return biersoorten; 
                } 
                catch 
                { 
                    return null; 
                } 
            }
        }
        public List<Bier> GetBieren()
        {
            using (SqlConnection connection = new SqlConnection(CS))
            {
                try
                {
                    connection.Open();
                    List<Bier> bieren = connection.GetAll<Bier>().ToList();
                    bieren = bieren.OrderBy(p => p.Naam).ToList();
                    return bieren;
                }
                catch
                {
                    return null;
                }
            }
        }

        // INSERT
        public int AddBier(Bier bier)
        {
            using (SqlConnection connection = new SqlConnection(CS))
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
        public int AddBierSoort(BierSoort bierSoort)
        {
            using (SqlConnection connection = new SqlConnection(CS))
            {
                try
                {
                    connection.Open();
                    int id = (int)connection.Insert(bierSoort);
                    return id;
                }
                catch
                {
                    return 0;
                }
            }
        }

        // UPDATE
        public bool UpdateBier(Bier bier)
        {
            using (SqlConnection connection = new SqlConnection(CS))
            {
                try
                {
                    connection.Open();
                    connection.Update(bier);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool UpdateBierSoort(BierSoort bierSoort)
        {
            using (SqlConnection connection = new SqlConnection(CS))
            {
                try
                {
                    connection.Open();
                    connection.Update(bierSoort);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // DELETED
        public bool DeleteBier(Bier bier)
        {
            using (SqlConnection connection = new SqlConnection(CS))
            {
                try
                {
                    connection.Open();
                    connection.Delete(bier);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool DeleteBierSoort(BierSoort bierSoort)
        {
            if (IsBierSoortInUse(bierSoort))
                return false;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                try
                {
                    connection.Open();
                    connection.Delete(bierSoort);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        private bool IsBierSoortInUse(BierSoort bierSoort)
        {
            string sql = "select count(*) from bieren where biersoortid = @id";
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                connection.Open();
                int count = connection.ExecuteScalar<int>(sql, bierSoort);
                if (count == 0)
                    return false;
                else
                    return true;
            }
        }

    }
}

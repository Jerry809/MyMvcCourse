using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Dapper;
using MyMvcCourse.Models;

namespace MyMvcCourse.Controllers
{
    public class CustomerApiController : ApiController
    {
        private string _conn = System.Configuration.ConfigurationManager.ConnectionStrings["Customer"].ConnectionString;

        // GET: api/Customer
        public List<Customer> Get()
        {
            using (var cn = new SqlConnection(_conn))
            {
                var query = "select * from tCustomer";
                var result = cn.Query<Customer>(query);
                return result.ToList();
            }
        }

        // GET: api/Customer/5
        public Customer Get(int fId)
        {
            Customer result;
            using (var cn = new SqlConnection(_conn))
            {
                var query = "select * from tCustomer where fId = @fId";
                result = cn.QueryFirstOrDefault<Customer>(query, new { fId = fId });
            }
            return result;
        }

        // POST: api/Customer
        public int Post(string fname, string fphone, string femail, string faddress)
        {
            int num = 0;

            using (var cn = new SqlConnection(_conn))
            {
                var query = @"insert into tCustomer (fname, fphone, femail, faddress)" +
                    @"values (@fname, @fphone, @femail, @faddress);" +
                    @"SELECT CAST(SCOPE_IDENTITY() as int)";
                try
                {
                    num = cn.QueryFirstOrDefault<int>(query,
                         new { fname = fname, fphone = fphone, femail = femail, faddress = faddress });
                }
                catch (Exception ex)
                {
                    throw ex;
                    //num = 0;
                }
            }
            return num;
        }

        // PUT: api/Customer/5
        //public int Put(int fid, string fname, string fphone, string femail, string faddress)
        public int Put(Customer customer)

        {
            int num = 0;
            using (var cn = new SqlConnection(_conn))
            {
                var query = @" update tCustomer set fname = @fname, fphone = @fphone" +
                            @" , femail = @femail, faddress = @faddress" +
                            @" where fid = @fid";

                try
                {
                    num = cn.Execute(query, customer);
                    //num = cn.Execute(query, new { fid = fid, fname = fname, fphone = fphone, femail = femail, faddress = faddress });
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return num;
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
{
}
    }
}

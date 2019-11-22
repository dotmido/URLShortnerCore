using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortner.DAL
{
    public  class Connection
    {
        public  IConfiguration configuration { get; set; }
        private  readonly string ConnectionString;
        public Connection(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }
        public  SqlConnection Connect()
        {

            return new SqlConnection(new Connection(configuration).ConnectionString);
        }
    }
}

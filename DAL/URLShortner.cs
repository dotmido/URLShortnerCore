using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using URLShortner.Models;

namespace URLShortner.DAL
{
    internal class URLShortner
    {
        internal static ShortURL GetByCode(string shortCode, IConfiguration configuration)
        {
            DataTable dt = new DataTable();
            if (GetShortURLByCode(shortCode, configuration.GetConnectionString("ShortnerConnection"), dt) > 0 && dt.Rows.Count > 0)
            {
                return new ShortURL
                {
                    Original_Url = dt.Rows[0]["Original_Url"].ToString(),
                    ShortCode = dt.Rows[0]["Shortcode"].ToString(),
                    DateAdded = Convert.ToDateTime(dt.Rows[0]["DateAdded"].ToString()),
                    DateUpdated = Convert.ToDateTime(dt.Rows[0]["DateUpdated"].ToString())
                };
            }
            return new ShortURL();
        }
        internal static ShortURL GetByOriginal(string Original, IConfiguration configuration)
        {
            DataTable dt = new DataTable();
            if (GetShortURLByOriginal(Original, configuration.GetConnectionString("ShortnerConnection"), dt) > 0 && dt.Rows.Count > 0)
            {
                return new ShortURL
                {
                    Original_Url = dt.Rows[0]["Original_Url"].ToString(),
                    ShortCode = dt.Rows[0]["Shortcode"].ToString(),
                    DateAdded = Convert.ToDateTime(dt.Rows[0]["DateAdded"].ToString()),
                    DateUpdated = Convert.ToDateTime(dt.Rows[0]["DateUpdated"].ToString())
                };
            }
            return new ShortURL();
        }

        internal static int GetShortURLByCode(string shortcode, string connectionstring, DataTable dt)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Shortner_GetByCode", new SqlConnection(connectionstring));
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@ShortCode", SqlDbType.NVarChar).Value = shortcode;
            try
            {
                return adapter.Fill(dt);
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                adapter.Dispose();
            }
        }
        internal static int GetShortURLByOriginal(string OriginalUrl, string connectionstring, DataTable dt)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("Shortner_GetByOriginal", new SqlConnection(connectionstring));
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OriginalUrl", SqlDbType.NVarChar).Value = OriginalUrl;
            try
            {
                return adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                adapter.Dispose();
            }
        }
    }
}
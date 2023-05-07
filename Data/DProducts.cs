using System.Data.SqlClient;
using System.Data;
using API_STORE.Conexion;
using API_STORE.Models;

namespace API_STORE.Data
{
    public class DProducts
    {
        ConexionDB cs = new ConexionDB();
        public async Task <List<MProducts>> showProducts() 
        {
            var listProducts = new List<MProducts>();
           // using(var sql = SqlConnection(cs.stringSQL()))
            using (SqlConnection sql = new SqlConnection(cs.stringSQL()))
            {
                using (var cmd = new SqlCommand("showProducts", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while(await item.ReadAsync())
                        {
                            var mproducts = new MProducts();
                            mproducts.id = (int)item["id"];
                            mproducts.description = (string)item["description"];
                            mproducts.price = (decimal)item["price"];
                            listProducts.Add(mproducts);
                        }
                    }
                }
            }

            return listProducts;
        }

        public async Task insertProduct(MProducts param)
        {
            using (SqlConnection sql = new SqlConnection(cs.stringSQL()))
            {
                using (var cmd = new SqlCommand("insertProducts", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@description", param.description);
                    cmd.Parameters.AddWithValue("@price", param.price);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task editProduct(MProducts param)
        {
            using (SqlConnection sql = new SqlConnection(cs.stringSQL()))
            {
                using (var cmd = new SqlCommand("editProduct", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", param.id);
                    //cmd.Parameters.AddWithValue("@description", param.description);
                    cmd.Parameters.AddWithValue("@price", param.price);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteProduct(MProducts param)
        {
            using (SqlConnection sql = new SqlConnection(cs.stringSQL()))
            {
                using (var cmd = new SqlCommand("deleteProduct", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", param.id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

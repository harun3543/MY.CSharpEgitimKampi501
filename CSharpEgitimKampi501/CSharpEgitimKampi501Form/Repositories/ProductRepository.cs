using CSharpEgitimKampi501.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi501.Repositories
{
    public class ProductRepository : IProductRepository
    {
        SqlConnection connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=EgitimKampi501Db;Integrated Security=True");
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            string query = "Insert into TblProduct (ProductName,ProductCategory,ProductStock,ProductPrice) values (@productName,@productCategory,@productStock,@productPrice)";

            // yukarıdaki @ ifadesi ile başlayan parametreleri doldurmak için kullandık.
            var parameters = new DynamicParameters();
            parameters.Add("@productName", createProductDto.ProductName);
            parameters.Add("@productCategory", createProductDto.ProductCategory);
            parameters.Add("@productStock", createProductDto.ProductStock);
            parameters.Add("@productPrice", createProductDto.ProductPrice);
            await connection.ExecuteAsync(query, parameters);
            
        }

        public async Task DeleteProductAsync(int id)
        {
            string query = "Delete from TblProduct Where Id=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<ResultProductDto>> GetAllProductsAsync()
        {
            string query = "Select * from TblProduct";
            var values = await connection.QueryAsync<ResultProductDto>(query);
            return values;
        }

        public async Task<IEnumerable<ResultProductDto>> GetByProductId(int id)
        {
            string query = "Select * from TblProduct Where Id=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            var value = await connection.QueryAsync<ResultProductDto>(query, parameters);
            return value;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            string query = "Update TblProduct Set ProductName=@productName,ProductCategory=@productCategory,ProductStock=@productStock,ProductPrice=@productPrice Where Id=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", updateProductDto.Id);
            parameters.Add("@productName", updateProductDto.ProductName);
            parameters.Add("@productCategory", updateProductDto.ProductCategory);
            parameters.Add("@productStock", updateProductDto.ProductStock);
            parameters.Add("@productPrice", updateProductDto.ProductPrice);
            await connection.ExecuteAsync(query, parameters);
        }
    }
}

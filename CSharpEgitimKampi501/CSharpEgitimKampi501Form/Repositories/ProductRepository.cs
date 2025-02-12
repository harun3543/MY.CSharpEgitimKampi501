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
            MessageBox.Show("Başarılı bir şekilde eklendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public async Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ResultProductDto>> GetAllProductsAsync()
        {
            string query = "Select * from TblProduct";
            var values = await connection.QueryAsync<ResultProductDto>(query);
            return values;
        }

        public async Task GetByProductId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}

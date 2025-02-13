using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpEgitimKampi501.Dtos;
using CSharpEgitimKampi501.Repositories;
using Dapper;

namespace CSharpEgitimKampi501Form
{
    public partial class Form1 : Form
    {
        IProductRepository productRepository;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            productRepository = new ProductRepository();
            this.Load += frmLoad;
        }

        private async void frmLoad(object sender, EventArgs e)
        {
            // bu sorgu dapper'da tablo içindeki data sayısını verir.
            string query1 = "Select Count(*) from TblProduct";
            var productTotalCount = await connection.QueryFirstOrDefaultAsync<int>(query1);
            lblProductTotalCount.Text = productTotalCount.ToString();

            // en pahalı product'ı bulmak
            string query2 = "Select ProductName from TblProduct Where ProductPrice=(Select Max(ProductPrice) From TblProduct)";
            var productMaxPrice = await connection.QueryFirstAsync<string>(query2);
            lblProductMaxPrice.Text = productMaxPrice.ToString();

            // kaç tane category çeşidi var
            //
            string query3 = "Select Count(Distinct(ProductCategory)) from TblProduct";
            var productCategoryDistinct = await connection.QueryFirstAsync<int>(query3);
            lblProductCategoryDistinct.Text = productCategoryDistinct.ToString();

        }

        SqlConnection connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=EgitimKampi501Db;Integrated Security=True");

        private async void btnList_Click(object sender, EventArgs e)
        {
            //string query = "Select * from TblProduct";
            //var values = await connection.QueryAsync<ResultProductDto>(query);
            //dataGridView1.DataSource = values;

            dataGridView1.DataSource = await productRepository.GetAllProductsAsync();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "Insert into TblProduct (ProductName,ProductCategory,ProductStock,ProductPrice) values (@productName,@productCategory,@productStock,@productPrice)";

            // yukarıdaki @ ifadesi ile başlayan parametreleri doldurmak için kullandık.
            var parameters = new DynamicParameters();
            parameters.Add("@productName", txtProductName.Text);
            parameters.Add("@productCategory", txtProductCategory.Text);
            parameters.Add("@productStock", txtProductStock.Text);
            parameters.Add("@productPrice", txtProductPrice.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Başarılı bir şekilde eklendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update TblProduct Set ProductName=@productName,ProductCategory=@productCategory,ProductStock=@productStock,ProductPrice=@productPrice Where Id=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", txtProductId.Text);
            parameters.Add("@productName", txtProductName.Text);
            parameters.Add("@productCategory", txtProductCategory.Text);
            parameters.Add("@productStock", txtProductStock.Text);
            parameters.Add("@productPrice", txtProductPrice.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Başarılı bir şekilde güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "Delete from TblProduct Where Id=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", txtProductId.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Başarılı bir şekilde silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

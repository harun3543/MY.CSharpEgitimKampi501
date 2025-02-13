# DAPPER

**QueryAsync()**: 
Dapper'da dataları listelemek için kullanılan komuttur. 

**ExecuteAsync**:
ekleme, silme veya güncelleme işlemleri için kullanılan query ifadesini yürütmek için kullanılır.

**Distict Komutu**:
bir kolonda birden fazla datayı tek olacak şekilde getirir. örneğin projede category alanında aynı kategoriden birden fazla olabilir.
bunun sayısını bulmak için *distinct* ifadesi kullanılır.

**DynamicParameters**:
query içerisinde "@" ifadesi ile belirlenen parametreleri dinamik olarak doldurmak için kullanılır.

örnek kullanımı;

```cs
 string query = "Insert into TblProduct (ProductName,ProductCategory,ProductStock,ProductPrice) 
                 values (@productName,@productCategory,@productStock,@productPrice)";
 var parameters = new DynamicParameters();
 parameters.Add("@productName", txtProductName.Text);
 parameters.Add("@productCategory", txtProductCategory.Text);
 parameters.Add("@productStock", txtProductStock.Text);
 parameters.Add("@productPrice", txtProductPrice.Text);
 await connection.ExecuteAsync(query, parameters);
```

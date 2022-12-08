use eShoppingCodi;

--Based on MAnufacturer name
select * from Manufacturer,Product where Manufacturer.ManufacturerId=Product.ManufacturerId and ManufacturerName='HP';


--Based on catagory name
select * from Product,Catagory where Product.CatagoryId=Catagory.CatagoryId and CatagoryName='fashion';

--Based on City="Pune" 
--select * from Product where ManufacturerId in(select ManufacturerId from Manufacturer where City='pune');
select * from Manufacturer, product where Manufacturer.ManufacturerId=Product.ManufacturerId and Manufacturer.City='pune';

--Products having price between 5000 to 10000
select * from Product where price between 5000 and 10000;

--first 3 and last 3 records
select * from Product order by ProductId offset 0 rows fetch next 3 rows only;
--select * from product order by ProductId offset 57 rows fetch next 3 rows only;
select * from Product order by ProductId offset ((select count(ProductId) from Product)-3) rows fetch next 3 rows only;
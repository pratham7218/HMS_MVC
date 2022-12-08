use eShoppingCodi;


create table SearchTable(
productId int primary key,
productName varchar(100),
manufacturerName varchar(100),
price int,
seller varchar(100),
description_ varchar(200)
)

insert into SearchTable values(1,'Laptop','HP',39999,'Croma','16 GB RAM');
insert into SearchTable values(2,'Laptop','HP',75999,'Reliance','1TB HDD');
insert into SearchTable values(3,'Laptop','Lenovo',45599,'Amazon','8 GB RAM');
insert into SearchTable values(4,'Laptop','Lenovo',82999,'Croma','512GB SSD');

insert into SearchTable values(5,'Mobile','Samsung',32999,'Samsung','256GB Storage');
insert into SearchTable values(6,'Mobile','Apple',63999,'Uni','12MP Camera');
insert into SearchTable values(7,'Mobile','Apple',63999,'Flipkart','8GB Ram');
insert into SearchTable values(8,'Mobile','Xiomi',15999,'Apprio Retails','65W Charger');
insert into SearchTable values(9,'Mobile','Xiomi',19999,'SS Mobiles','256GB Storage');
insert into SearchTable values(10,'Mobile','Samsung',72999,'Samsung','10Bit Super Amoled Display');



select * from SearchTable;







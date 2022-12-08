 create database HMS_Project;

 use HMS_Project;


 create table Roles(
 role_id int primary key,
 roleName varchar(100)
 );
 
 create table Users(
 userId int primary key identity(1,1),
 first_name varchar(25),
 last_name varchar(25),
 email varchar(100),
 password_ varchar(16),
 age int,
 contact_number varchar(15),
 gender varchar(10),
 staff_category varchar(25),
 specialization varchar(50),
 role_id int foreign key references Roles(role_id)
 );
 alter table users drop column first_name;
 alter table users drop column last_name;
 alter table users drop column staff_category;

 alter table users add full_name varchar(50);
 alter table users add userName varchar(50);



 create table Patient(
 patient_id int primary key,
 prev_history varchar(500),
 reports varchar(500),
 userId int foreign key references Users(userId)
 );


 create table Desease_Details(
 detail_id int primary key identity,
 patient_id int foreign key references Patient(patient_id),
 no_of_visits int,
 desease_catagory varchar(100)
 );

 
 create table Consumable(
 consumable_id int primary key,
 consumable_name varchar(50),
 availability_detail varchar(20),
 price int
 );

 create table Treatment(
 treatment_id int primary key,
 quantity int,
 is_admitted binary,
 consumable_id int foreign key references Consumable(consumable_id)
 );

 alter table Treatment add is_admitted bit;

 create table Appointment(
 appointment_id int primary key,
 AdmitDate date,
 patient_id int foreign key references Patient(patient_id),
 treatment_id int foreign key references Treatment(treatment_id),
 userId int foreign key references Users(userId)
 );

 create table Billing(
 bill_number varchar(25) primary key,
 patient_id int foreign key references Patient(patient_id),
 consumable_id int foreign key references Consumable(consumable_id),
 userId int foreign key references Users(userId)
 );

 select * from Users;
 select * from Roles;





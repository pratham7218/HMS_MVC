 create database HMS_Project_new;

 use HMS_Project_new;


 create table Roles(
 role_id int primary key,
 roleName varchar(100)
 );
 
 create table Users(
 userId int primary key identity,
full_name varchar(50),
 email varchar(100),
 userName varchar(50),
 password_ varchar(16),
 age int,
 contact_number varchar(15),
 gender varchar(10),
 --staff_category varchar(25),
 specialization varchar(50),
 role_id int foreign key references Roles(role_id)
 );

 alter table users alter column password_ varchar(80) not null;

 

 create table Patient(
 patient_id varchar(100) primary key,
 prev_history varchar(500),
 reports varchar(500),
 userId int foreign key references Users(userId)
 );


 create table Desease_Details(
 detail_id int primary key identity,
 patient_id varchar(100) foreign key references Patient(patient_id),
 no_of_visits int,
 desease_catagory varchar(100)
 );

 
 create table Consumable(
 consumable_id int primary key identity,
 consumable_name varchar(50),
 availability_detail varchar(20),
 price int
 );

 create table Treatment(
 treatment_id int primary key identity,
 quantity int,
 is_admitted bit,
 consumable_id int foreign key references Consumable(consumable_id)
 );
 alter table treatment add patient_id varchar(100),constraint fk Foreign key(patient_id) references patient(patient_id); 

 alter table treatment drop constraint FK__Treatment__consu__30F848ED;
 alter table treatment drop column consumable_id;

 alter table treatment add consumable_id int foreign key references Consumable(consumable_id);

 alter table treatment add IsActive bit;


 create table Appointment(
 appointment_id int primary key identity,
 AdmitDate date,
 patient_id varchar(100) foreign key references Patient(patient_id),
 treatment_id int foreign key references Treatment(treatment_id),
 userId int foreign key references Users(userId)
 );

 alter table Appointment add appointmentDate date;
 alter table Appointment add appointmentTime time;
 alter table Appointment add isApproved bit;

 create table Billing(
 bill_number int primary key identity,
 patient_id varchar(100) foreign key references Patient(patient_id),
 consumable_id int foreign key references Consumable(consumable_id),
 userId int foreign key references Users(userId)
 );
 alter table billing add is_paid bit;
 
 
create table Payment(
paymentId int primary key identity,
patientId varchar(100) foreign key references  patient(patient_id),
total_bill int,
isRequested bit
);


go
create or alter proc sp_GetAllAppointments 
@userId int
As
Begin
select * from Appointment where Appointment.userId = @userId;
End
go

exec sp_GetAllAppointments @userId =17;





 select * from Users;
 select * from Roles;
 select * from Desease_Details
 select * from Appointment;
select * from Treatment;
select * from Consumable;
select * from payment;
select * from Billing;
select * from Patient;


delete from Users where userId=42;
update treatment set IsActive=1 where treatment_id in(88,89); 
alter table appointment add admitdate date;
alter table treatment add is_admitted bit;



 

delete from Billing;
delete from payment;
delete from Appointment;
delete from Desease_Details;
delete from Treatment;
delete from Patient;










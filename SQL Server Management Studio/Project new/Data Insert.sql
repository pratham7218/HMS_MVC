use hms_project_new;


 insert into roles values(1,'Admin');
 insert into roles values(2,'Doctor');
 insert into roles values(3,'Receptionist');
 insert into roles values(4,'Patient');

 insert into Users values('Admin Supreme','admin@gmail.com','admin01','admin',22,'9876543210','Male',null,1);
 select * from Users;
 select * from Roles;
 select * from Consumable;

 select * from patient;
 select * from Desease_Details;
select * from Treatment;
 select * from Appointment;

 


 
delete from Appointment;
delete from Billing;
delete from Patient;
delete from Desease_Details;
delete from payment;
delete from Treatment;

select * from payment;
select * from Billing;


update billing set is_paid=0 where bill_number=53;
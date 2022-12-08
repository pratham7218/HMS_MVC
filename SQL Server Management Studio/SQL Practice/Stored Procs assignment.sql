use EmployeeBase;

go
create or alter proc sp_getSalSumByDeptName
@DeptNAme varchar(100)
as
begin
	declare @salSum int
	declare @number int;
	if(@DeptNAme!=null)
		begin
			select @number = Department.DeptNo from Department where DeptName=@DeptNAme;
			select @salSum = Sum(salary) from Employee where DeptNo=@number;
		end
	else
		select 'Invalid Department name';

	return @salsum;
end
go


declare @FinalResult int;
Exec @finalResult = sp_getSalSumByDeptName @DeptNAme='projects';
select @finalResult;
go



--------------------------------------------------------
create OR ALTER proc sp_calculateTax
@resSet float output
as
begin 
	declare @sal int,@tax float	
	select @sal = Employee.Salary from Employee;

	if(@sal<300000)
		select @tax = @sal*0.18;
	else if(@sal>300000 and @sal<500000)
		select @tax = @sal*0.21;
	else if(@sal>500000 and @sal<700000)
		select @tax = @sal*0.23;
	else if(@sal>700000 and @sal<900000)
		select @tax = @sal*0.29;

	select @resSet = @tax;
end 
go

declare @tax float;
exec sp_calculateTax @resSet=@tax output;
select @tax as tax;
go

----------------------------------------

create or alter proc sp_groupEmpByDept
@deptName varchar(20)
as
begin
	--select Employee.EmpName,Department.DeptName from Department,Employee where DeptName=@deptName and Employee.DeptNo=Department.DeptNo;
	select Employee.EmpName,Department.DeptName from Employee join Department on Department.DeptNo = Employee.DeptNo where @deptName=Department.DeptName;
end
go

--declare @result 
exec sp_groupEmpByDept @deptname = 'Accounts';
go

-------------------------------------------------------

--select * from department;
--select * from employee;
create or alter proc sp_calculateTax
as
begin
	select Employee.DeptNo,Employee.EmpNo,Employee.EmpName,Employee.Salary,
	case 
		when salary between 300001 and 500000 then salary*0.21
		when salary between 500001 and 700000 then salary*0.23
		when salary between 700001 and 900000 then salary*0.29
		when salary <=300000 then salary*0.18
	end as net_tax
	from employee;
end 
go


exec sp_calculateTax;
go

---------------------------------------------------------------------


select * from Department,Employee where Department.DeptNo= Employee.DeptNo;
go
create or alter proc sp_InsertEmpWithValidations
@EmpNo int,@EmpName varchar(200),@Designation varchar(200),@Salary int,@DeptNo int
as
begin 
	declare @currentEmployees int,@totalCapacity int;
	select @currentEmployees= count(*),@totalCapacity=Capacity  from Employee,Department where Employee.DeptNo=Department.DeptNo  group by Department.Capacity;
	if(@Designation = 'manager' or @Designation = 'Lead' or @Designation = 'assosiate' or @Designation = 'director')
		begin
			if(@currentEmployees<@totalCapacity)
				begin
					if(@Designation = 'assosiate' and @salary <300000)
						begin 
							insert into Employee values(@EmpNo,@EmpName,@Designation,@Salary,@DeptNo);					
						end
					else if(@Designation = 'Lead' and @salary <500000)
						begin 
							insert into Employee values(@EmpNo,@EmpName,@Designation,@Salary,@DeptNo);					
						end
					else if(@Designation = 'manager' and @salary <700000)
						begin 
							insert into Employee values(@EmpNo,@EmpName,@Designation,@Salary,@DeptNo);					
						end
					else if(@Designation = 'director' and @salary <900000)
						begin 
							insert into Employee values(@EmpNo,@EmpName,@Designation,@Salary,@DeptNo);					
						end
				end
				else
					begin
						select ERROR_MESSAGE() as 'Can not add more employees';
					end
		end
end
go


exec sp_InsertEmpWithValidations @EmpNo=1005,@EmpNAme='xyz',@Designation='assosiate',@salary=175000,@DeptNo=30;
go


--select * from department;
--select * from employee;
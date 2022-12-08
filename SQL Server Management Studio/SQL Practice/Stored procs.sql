use EmployeeBase;

go
create proc sp_getAllEmployees
As
Begin
	select Employee.empNo,employee.empName,Employee.Designation,Employee.Salary,Employee.DeptNo from Employee
End 
Go

Exec sp_getAllEmployees;
go

create or alter proc sp_getEmpByDeptNo
@DeptNo int 
as
begin
select  Employee.empNo,employee.empName,Employee.Designation,Employee.Salary,Employee.DeptNo from Employee where @DeptNo=Employee.DeptNo;
end
go

select * from Employee;

Exec sp_getEmpByDeptNo @DeptNo=10;
go


--create SP that returns sum of salary for all emp in deptNo 20
create or alter Proc sp_getSalarySumByDeptNo
@deptNo int 
as
Begin
	--local varibale
	declare @SumSAlary int;
	--chack deptNo
	if(@deptNo<=0)
		select 'Invalid deptNo' as Error_MEssage;
	else
		select @SumSAlary=Sum(salary) from Employee where DeptNo=@deptNo;


	return @sumSalary;
end
go

--Declare a variable to store return value of SP
Declare @sumSal int;

Exec @sumSal = sp_getSalarySumByDeptNo @deptNo =20;
select @sumSal;
go


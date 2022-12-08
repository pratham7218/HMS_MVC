use employeebase;
select * from Employee;
--select Employee.DeptNo,COUNT(*) as myCols from Employee join Department on Employee.DeptNo= Department.DeptNo group by Employee.DeptNo;

--table valued function
go
create function myFunc(
	@deptNo int
)
returns table
as 
return 
select 
 capacity,DeptNo,DeptName from Department where DeptNo=@deptNo; 
go
select * from myFunc(10);


--scalar fn>returns a single value




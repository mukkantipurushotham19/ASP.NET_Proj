select * from employee


create procedure GetAllEmployees
as begin
select  * from Employee
end

CREATE PROCEDURE GetEmployee
@Eno INT
AS BEGIN
SELECT * FROM Employee WHERE @Eno=Eno
END

CREATE PROCEDURE InsertEmployee
(@Eno int,@Ename varchar(32),@Job varchar(27),@Salary money,@Dname varchar(27))
AS BEGIN
INSERT INTO Employee VALUES(@Eno,@Ename,@Job,@Salary,@Dname)
END


CREATE PROCEDURE UpdateEmployee
(@Eno int,@EName varchar(32),@Job varchar(27),@Salary money,@Dname varchar(27))
AS BEGIN
UPDATE Employee  SET ENAME=@EName,JOB=@Job,SALARY=@Salary,DNAME=@Dname WHERE ENO=@Eno
END


CREATE PROCEDURE DeleteEmployee
@Eno INT
AS BEGIN
DELETE FROM Employee WHERE ENO=@Eno
END







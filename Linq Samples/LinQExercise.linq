<Query Kind="Expression">
  <Connection>
    <ID>8a34c70a-1ed6-4a62-9c94-4c0d59e92cbb</ID>
    <Server>.</Server>
    <Database>WorkSchedule</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//1.Show all skills requiring a ticket and which employees have those skills.
from person in Skills
where person.RequiresTicket == true
select new
{
    Description = person.Description,
	Employees = from people in person.EmployeeSkills
	            orderby people.YearsOfExperience descending
	
	select new 
	{
 	Name = people.Employee.FirstName + " " + people.Employee.LastName, 
	Level = people.Level,
	YearsOfExperience = people.YearsOfExperience
	}
}

//2.List all skills, alphabetically, showing only the description of the skill.
from skill in Skills
orderby skill.Description
select skill.Description 

//3.List all the skills for which we do not have any qualfied employees.// with any
from employee in Skills
where employee.EmployeeSkills.Count() == 0
select employee.Description
//4.From the shifts scheduled for NAIT's placement contract, show the number 
//of exmployees needed for each day (ordered by day-of-week). 

Shifts
PlacementContracts

from employee in Shifts
where employee.PlacementContractID == 3
group employee by employee.DayOfWeek into result
select new
{
 Day = result.Key,
 EmployeesNeeded = result.Sum(s => s.NumberOfEmployees)
 }
 
//4 Bonus: display the name of the day of week (first day being Monday).

from employee in Shifts
where employee.PlacementContractID == 3
group employee by employee.DayOfWeek into result
select new
{
 Day = result.Key,

 EmployeesNeeded = result.Sum(s => s.NumberOfEmployees)
 }
 



//5.List all the employees with the most years of experience.

       from employee in EmployeeSkills
       where employee.YearsOfExperience == (from employee1 in EmployeeSkills select employee1.YearsOfExperience).Max()
       select new
       {
	   
	    Name = employee.Employee.FirstName + " " + employee.Employee.LastName
		
       }
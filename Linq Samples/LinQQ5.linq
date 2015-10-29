<Query Kind="Statements">
  <Connection>
    <ID>30fa732e-3a08-45d1-b42f-532b8ab6f20f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WorkSchedule</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//list employees and their years of experience(YOE)
//sum the rows containing the YOE for an employee 
var employeeYOE = from eachEmployeerow in Employees // instead employeeskills, i want to see the data at employee table for each each row
                                                         // remember, employeeskills inside employee. I need dataset.
select new {
				       name = eachEmployeerow.FirstName + " " + eachEmployeerow.LastName,
					   YOE = eachEmployeerow.EmployeeSkills.Sum(
					         eachEmployeeSkillrow => eachEmployeeSkillrow.YearsOfExperience)
				  
};
				  
employeeYOE.Dump();


//from list of employeeYOE find the Max()
var MaxYOE = employeeYOE.Max(eachEYOErow => eachEYOErow.YOE);// for each row, give the YOE
MaxYOE.Dump();

//using empoyee YOE and YOEMax create a final list of most experienced employee
var finalresult = from eachEYOErow in employeeYOE
                  where  employeeYOE= MaxYOE //compare eachEYOErow to the maxvalue
				  select name;
finalresult.Dump();
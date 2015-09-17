<Query Kind="Statements">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// simpliest form for dumpling an entity:
Waiters

// simple query syntax:
from person in Waiters
select person 

//simple method syntax
Waiters.Select (person => person)

//inside our project we will be writting C# statement
var results = from person in Waiters select person;

// to display the contents of a variable in linqPad use the  .Dump() method
results.Dump();

// implemented inside a VS project's class library BLL method
//[DataObjectMehod(DataObjectMethodType.Select,false)]
//public List<Waiters> SomeMethodName()
//{
  // you will need to connect to your DAL object
  // this will be done by using a new xxxxx() contructor 
  // assume your connection variable is called contextvariable
  
  // do your query
  
  //var results = from person in contextvariable.Waiters select person;
  
  //return your results
  //return results.TOList();
  
}
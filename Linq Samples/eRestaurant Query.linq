<Query Kind="Expression">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//where clause

// list all tables that hold more than 3 people
//query syntax
from row in Tables
where row.Capacity > 3
select row

// method syntax
Tables. Where(row => row.Capacity>3)

// list all items with more than 500 calories

from food in Items
where food.Calories > 500
select food

// list all items with more than 500 calories and selling for more than $10.00
from food in Items
where food.Calories > 500 && food.CurrentPrice>10.00m // works for single "&", but && for C#
select food

// list all items with more than 500 calories and are entrees on the menu
//HINT navigational properties on the database are known by Linqpad
 from food in Items
 where food.Calories >500 && food.MenuCategory.Description.Equals("Entree")
 select food


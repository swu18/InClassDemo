<Query Kind="Expression">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Order Clause


from X in Items
orderby X.Description
select X

// also available ascending and  descending
from food in Items
orderby food.CurrentPrice descending, food.Calories ascending
select food

// you can use where and orderby together
// orderby and where can change its order.
from food in Items
orderby food.CurrentPrice descending, food.Calories ascending
where food .MenuCategory.Description.Equals("Entree")
select food

from food in Items
where food .MenuCategory.Description.Equals("Entree")
orderby food.CurrentPrice descending, food.Calories ascending
select food


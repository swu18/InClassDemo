<Query Kind="Expression">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//anoymous data type queries
 from food in Items
 where food.MenuCategory.Description.Equals("Entree") && food.Active 
 orderby food.CurrentPrice descending
 select new   // New for new data set
 {
  Description = food.Description,
  Price = food.CurrentPrice,
  Cost = food.CurrentCost,
  Profi = food.CurrentPrice - food.CurrentCost
 }
 
 //option2
 
 from food in Items
 where food.MenuCategory.Description.Equals("Entree") && food.Active 
 orderby food.CurrentPrice descending
 select new   // New for new data set
 {
  food.Description,
  food.CurrentPrice,
  food.CurrentCost,
 // Profi = food.CurrentPrice - food.CurrentCost
 }
 
 //Option 3: if you know this data type already, not to create annoymous datatypes, but pocoobject (only works at VS)
 
 from food in Items
 where food.MenuCategory.Description.Equals("Entree") && food.Active 
 orderby food.CurrentPrice descending
 select new //POCOObjectName   // New for new data set
 {
  Description = food.Description,
  Price = food.CurrentPrice,
  Cost = food.CurrentCost,
  Profit = food.CurrentPrice - food.CurrentCost
 }
<Query Kind="Expression">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//groupby

from food in Items
group food by food.MenuCategory.Description

// this creates a key with a value and the row collection for that key value
// more than one field
from food in Items
group food by new{food.MenuCategory.Description, food.CurrentPrice}


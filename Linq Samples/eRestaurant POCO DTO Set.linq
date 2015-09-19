<Query Kind="Program">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main ()

   {

////anoymous data type queries
// from food in Items
// where food.MenuCategory.Description.Equals("Entree") && food.Active 
// orderby food.CurrentPrice descending
// select new   // New for new data set
// {
//  Description = food.Description,
//  Price = food.CurrentPrice,
//  Cost = food.CurrentCost,
//  Profi = food.CurrentPrice - food.CurrentCost
// }
// 
// //option2
// 
// from food in Items
// where food.MenuCategory.Description.Equals("Entree") && food.Active 
// orderby food.CurrentPrice descending
// select new   // New for new data set
// {
//  food.Description,
//  food.CurrentPrice,
//  food.CurrentCost,
// // Profi = food.CurrentPrice - food.CurrentCost
// }
// 
// //Option 3: if you know this data type already, not to create annoymous datatypes, but pocoobject (only works at VS)
// 
// from food in Items
// where food.MenuCategory.Description.Equals("Entree") && food.Active 
// orderby food.CurrentPrice descending
// select new POCOObjectName   // New for new data set
// {
//  Description = food.Description,
//  Price = food.CurrentPrice,
//  Cost = food.CurrentCost,
//  Profit = food.CurrentPrice - food.CurrentCost
// }


var results = from food in Items
              where food.MenuCategory.Description.Equals("Entree") && food.Active 
              orderby food.CurrentPrice descending
             select new FoodMargins()
          {
           Description = food.Description,
            Price = food.CurrentPrice,
            Cost = food.CurrentCost,
           Profit = food.CurrentPrice - food.CurrentCost
          };
    results.Dump();
	
	//get all the bills and bill items for waiters in sep of 2014.get
	//only those bills which were paid.
var results2 = from orders in Bills
              where orders.PaidStatus && (orders.BillDate.Month == 9 && orders.BillDate.Year == 2014)
			  orderby orders. Waiter.LastName, orders.Waiter.FirstName
			  select new
		      {
			    BillID= orders.BillID,
				WaiterName = orders.Waiter.LastName + "," + orders.Waiter.FirstName,
				Orders = orders.BillItems
		       };
	results2.Dump();
} //eop
 
 
 //Define other methods and classed here
 //This is POCO class
 public class FoodMargins
 {
   public string Description{get;set;}
   public decimal Price {get;set;}
   public decimal Cost {get;set;}
   public decimal Profit {get;set;}
 
 }
 
 public class BillOrders
 
 {
      public int BillID{get;set;}
	  public string WaiterName{get;set;}
	  public Ienumerable Orders{get;set;} //BillItems are entity class
 
 }



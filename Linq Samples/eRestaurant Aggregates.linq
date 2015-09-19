<Query Kind="Statements">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

(from theBill in BillItems
where theBill.BillID == 104
select theBill.SalePrice * theBill.Quantity).Sum()

(from customer in Bills
where customer.PaidStatus == true
select customer.BillItems.Sum(theBill => theBill.SalePrice * theBill.Quantity)).Min()

//the average paid bill 

(from customer in Bills
where customer.PaidStatus == true
select customer.BillItems.Sum(theBill => theBill.SalePrice * theBill.Quantity)).Average()


//Add10% Tip

//what is average number of items per paid bill 
// we need to get a list of numbers representing the items per bill
// we take an average of the list 

(from customer in Bills
where customer.PaidStatus // == where customer.PaidStatus == true
select customer.BillItems.Count()).Average()

Bills.Any(customer => customer.PaidStatus == false)

from category in MenuCategories
where category.Items.All(item => item.CurrentCost > 2.0m)
select category
<Query Kind="Expression">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

 //Bills.Max( x => x.BillDate ).Dump()
 
 Bills.Max( eachBillrow => eachBillrow.BillDate ).Dump()
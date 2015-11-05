<Query Kind="Statements">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//get the latest one
var date = Bills.Max(thebill => thebill.BillDate);
date.Dump();

//
var justdate = Bills.Max(thebill => thebill.BillDate).Date;
justdate.Dump();
var newtime = Bills.Max(thebill => thebill.BillDate).TimeOfDay.Add(new TimeSpan(0,30,0));
newtime.Dump();

justdate.Add(newtime).Dump();
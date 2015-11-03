<Query Kind="Statements">
  <Connection>
    <ID>9f536fc5-abf6-4d0d-a44e-9b36870cd229</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// Step 1 - Get the table info along with any walk-in bills and reservation bills for the specific time slot
var date = Bills.Max(thebill => thebill.BillDate);
date.Dump();

//
var justdate = Bills.Max(thebill => thebill.BillDate).Date;
justdate.Dump();
var newtime = Bills.Max(thebill => thebill.BillDate).TimeOfDay.Add(new TimeSpan(0,30,0));
newtime.Dump();

justdate.Add(newtime).Dump();
var step1 = from eachTableRow in Tables
             select new
             {
                Table = eachTableRow.TableNumber,
                Seating = eachTableRow.Capacity,//capasity
                // This sub-query gets the bills for walk-in customers
                WalkIns = from walkIn in eachTableRow.Bills
                        where 
                               walkIn.BillDate.Year == date.Year
                            && walkIn.BillDate.Month == date.Month
                            && walkIn.BillDate.Day == date.Day
                            && walkIn.BillDate.TimeOfDay <= newtime
                            && (!walkIn.OrderPaid.HasValue || walkIn.OrderPaid.Value >= newtime)
//                          && (!walkIn.PaidStatus || walkIn.OrderPaid >= time)
                        select walkIn,
                 // This sub-query gets the bills for reservations
                 Reservations = from booking in eachTableRow.ReservationTables
                        from reservationParty in booking.Reservation.Bills
                        where 
                               reservationParty.BillDate.Year == date.Year
                            && reservationParty.BillDate.Month == date.Month
                            && reservationParty.BillDate.Day == date.Day
                            && reservationParty.BillDate.TimeOfDay <= newtime
                            && (!reservationParty.OrderPaid.HasValue || reservationParty.OrderPaid.Value >= newtime)
//                          && (!reservationParty.PaidStatus || reservationParty.OrderPaid >= time)
                        select reservationParty
             };
step1.Dump();
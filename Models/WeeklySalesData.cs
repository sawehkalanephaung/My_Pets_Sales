using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace My_Pets_Sales.Models;

public class WeeklySalesData
{
   public required List<Series> Series { get; set; }
    public required List<string> Categories { get; set; }

}

public class Series
{
    public required string Name { get; set; } 
    public required List<decimal> Data { get; set; } 
}
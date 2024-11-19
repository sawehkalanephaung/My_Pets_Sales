using System;
using System.ComponentModel.DataAnnotations;

namespace My_Pets_Sales.Models;

public class DashboardViewModel
{
    public required List<Pet> DailySales { get; set; } 
    public required WeeklySalesData WeeklySales  { get; set; }

}

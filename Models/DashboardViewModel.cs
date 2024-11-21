using System;
using System.ComponentModel.DataAnnotations;

namespace My_Pets_Sales.Models;

public class DashboardViewModel
{
    public required List<DailySalesData> DailySales { get; set; } 
    public required WeeklySalesData WeeklySales  { get; set; }

}

using System;
using System.Collections.Generic;
using System.Linq;

public class Report
{
    internal string TotalExpense;
    public string Category { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalExpenses { get; set; }
    public IEnumerable<Transaction> Transactions { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSimulator
{
    class GroceryInfo
    {

        public string GroceryName { get; set; }

        public int Amount { get; set; }

        public double PricePerKQ { get; set; }


        public GroceryInfo(string groceryName, int amount, double pricePerKQ)
        {
            GroceryName = groceryName;
            Amount = amount;
            PricePerKQ = pricePerKQ;
        }
    }

    class DailyGroceryReport
    {
        public string Name { get; set; }

        public double DailyThrown { get; set; }

        public double DailySold { get; set; }

        public DailyGroceryReport()
        {
           
        }

        public DailyGroceryReport(string name, double dailyThrown, double dailySold)
        {
            Name = name;
            DailyThrown = dailyThrown;
            DailySold = dailySold;
        }

        public void SetDailyThrownGrs(double amount)
        {
            DailyThrown += amount;
       
        }

        public void SetDailySoldGrs(double amount)
        {
            DailySold += amount;
        }
    }

    class WeeklyGroceryReport
    {
        public string Name { get; set; }

        public double WeeklyThrown { get; set; }

        public double WeeklySold { get; set; }

        public double Price { get; set; }

        public WeeklyGroceryReport()
        {

        }

        public WeeklyGroceryReport(string name, double weeklyThrown, double weeklySold, double price)
        {
            Name = name;
            WeeklyThrown = weeklyThrown;
            WeeklySold = weeklySold;
            Price = price;
        }

        public void SetWeeklyThrownGrs(double amount)
        {
            WeeklyThrown += amount;
        }

        public void SetWeeklySoldGrs(double amount)
        {
            WeeklySold += amount;
        }
    }

    class GroceryDegree
    {
        public bool New { get; set; }

        public bool Normal { get; set; }

        public bool Rotten { get; set; }

        public bool Toxic { get; set; }

        public GroceryDegree(bool @new, bool normal, bool rotten, bool toxic)
        {
            New = @new;
            Normal = normal;
            Rotten = rotten;
            Toxic = toxic;
        }

        //public override string ToString()
        //{
        //    return $"New: {New}\nNormal : {Normal}\nRotten: {Rotten}\nToxic: {Toxic}";
        //}
    }

}

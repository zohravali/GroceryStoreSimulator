using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSimulator
{
    class DailyReport
    {
        public double DailyRatAvg { get; set; }

        public double DailyIncome { get; set; }

        public Dictionary<string, double> NewGrs { get; set; }

        public Dictionary<string, double> ThrownGrs { get; set; }

        public int BuyersAmount { get; set; }

        public DailyReport()
        {

        }

        public DailyReport(double dailyRatAvg, double dailyIncome, Dictionary<string, double> newGrs, Dictionary<string, double> thrownGrs, int buyersAmount)
        {
            DailyRatAvg = dailyRatAvg;
            DailyIncome = dailyIncome;
            NewGrs = newGrs;
            ThrownGrs = thrownGrs;
            BuyersAmount = buyersAmount;
        }

        public void SetBuyerAmount(int amountofBuyer)
        {
            BuyersAmount = amountofBuyer;
            //amountofBuyer = 0;
        }

        public void SetNewGrAmount(List<GroceryInfo> dailyGrDem)
        {
            foreach (var dg in dailyGrDem)
            {
                NewGrs.Add(dg.GroceryName, dg.Amount);
            }
        }

        public void SetThrownGrAmount(List<DailyGroceryReport> dailyThrowngrs)
        {

            foreach (var g in dailyThrowngrs)
            {
                ThrownGrs.Add(g.Name, g.DailyThrown);
            }
        }

        public  void Show()
        {
            Console.WriteLine($"DailyRatingAvg :{DailyRatAvg}\n\nDailyIncome: {DailyIncome}\n\nBuyersAmount: {BuyersAmount}\n\n");
            foreach (var ngr in NewGrs)
            {
                Console.WriteLine($"Name of New Grocery: {ngr.Key}  Amount: {ngr.Value}");
            }
            Console.WriteLine(" ");
            foreach (var tgr in ThrownGrs)
            {
                Console.WriteLine($"Name of Thrown Grocery: {tgr.Key}  Amount: {tgr.Value}");
            }
        }
    }

    class WeeklyReport
    {
        public double WeeklyRatingRes { get; set; }

        public double WeeklyIncome { get; set; }

        public double WeeklyExpense { get; set; }

        public double Budget { get; set; }

        public int WorkerAmount { get; set; }

        public Dictionary<string, double> ThrownGrs { get; set; }

        public WeeklyReport()
        {

        }

        public WeeklyReport(double weeklyRatingRes, double weeklyIncome, double weeklyExpense, double budget, int workerAmount, Dictionary<string, double> thrownGrs)
        {
            WeeklyRatingRes = weeklyRatingRes;
            WeeklyIncome = weeklyIncome;
            WeeklyExpense = weeklyExpense;
            Budget = budget;
            WorkerAmount = workerAmount;
            ThrownGrs = thrownGrs;
        }

        public void SetWeeklyIncome(double dailyIncome)
        {
            WeeklyIncome += dailyIncome;
            Budget += dailyIncome;
        }

        public void SetWeeklyExpense(List<GroceryInfo> grDemandWeekly, int count, int salary)
        {
            foreach (var gr in grDemandWeekly)
            {
                WeeklyExpense += gr.Amount * gr.PricePerKQ;
                WeeklyExpense += count * salary;
            }

            Budget -= WeeklyExpense;
        }

        public void SetThrownGrAmount(List<WeeklyGroceryReport> weeklyThrowngrs)
        {

            foreach (var g in weeklyThrowngrs)
            {
                ThrownGrs.Add(g.Name, g.WeeklyThrown);
            }
        }

        public void SetWorkerAmount(List<Worker> workers) => WorkerAmount = workers.Count;

        public void Show()
        {
            Console.WriteLine($"WeeklyRatingResukt :{WeeklyRatingRes}\n\nWeeklyIncome: {WeeklyIncome}\n\nWeeklyExpense: {WeeklyExpense}\n\nBudget: {Budget}\n\nWorkerAmount: {WorkerAmount}\n\n");
     
            foreach (var tgr in ThrownGrs)
            {
                Console.WriteLine($"Name of Thrown Grocery: {tgr.Key}  Amount: {tgr.Value}");
            }
        }
    }
}

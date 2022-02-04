using GroceryStoreSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSimulator
{
    static class random
    {
        public static int RandomNumber(int min, int max)
        {
            Random _random = new();
            return _random.Next(min, max);
        }

        public static void FillBuyersQueue(ref GroceryMarket grBuyer, Dictionary<string, double> groceriestoBuy, double rating)
        {
            Random rnd = new();
            List<GroceryInfo> ToBuyGrs = new();
            int n = rnd.Next(1, 17); //nece cur terevez almaq isteyir
            int amountofBuyers = 0; ;
            if (rating >= 1 && rating <= 3) amountofBuyers = rnd.Next(1, 15);
            else if (rating == 4 || rating == 5) amountofBuyers = rnd.Next(5, 25);

            for (int i = 0; i < amountofBuyers; i++)
            {
                foreach (var gr in groceriestoBuy)
                {
                    if (ToBuyGrs.Count != n)
                    {
                        int num = rnd.Next(0, 2);
                        if (num == 0)
                        {
                            int amount = rnd.Next(3, 50);
                            ToBuyGrs.Add(new(gr.Key, amount, gr.Value));
                        }
                    }
                    else break;
                }
                grBuyer.Buyers.Enqueue(new(ToBuyGrs, new()));
            }
        }

        public static void AgingGroceries(ref Stends stends)
        {
            foreach (var st in stends.stends)
            {
                foreach (var gr in st.Value)
                {

                    if (gr.New == true)
                    {
                        gr.New = false;
                        gr.Normal = true;
                    }
                    else if (gr.Normal == true)
                    {
                        gr.Normal = false;
                        gr.Rotten = true;
                    }
                    else if (gr.Rotten == true)
                    {
                        gr.Rotten = false;
                        gr.Toxic = true;
                    }
                }

            }
        }

        public static bool InreaseCheck(double expense, double budget)
        {
            if (expense <= budget * 0.5)
                return true;
            return false;
        } 

        public static bool IncreaseVarietyCheck(double rating)
        {
            if (rating >= 4 && rating <= 5) return true;
            return false;
        }
    }


    class Buyer
    {
        public List<GroceryInfo> ToBuygroceries { get; set; }

        public Dictionary<string, double> BougthGr { get; set; }

        public Buyer()
        {
        }

        public Buyer(List<GroceryInfo> toBuygroceries, Dictionary<string, double> bougthGr)
        {
            ToBuygroceries = toBuygroceries;
            BougthGr = bougthGr;
        }
    }

    struct Worker
    {

        public int Salary { get; set; }

        public Worker(int salary)
        {
            Salary = salary;
        }
    }

    class GroceryMarket
    {
        public Stends stends { get; set; }

        public Queue<Buyer> Buyers { get; set; }

        public List<GroceryInfo> DailyGroceryDemand { get; set; }

        public List<DailyGroceryReport> DailyGroceriesReports { get; set; }

        public List<WeeklyGroceryReport> WeeklyGroceriesReports { get; set; }

        public Rating rating { get; set; }

        public int AmountofBuyers { get; set; }

        public List<Worker> Workers { get; set; }

        public GroceryMarket()
        {

        }

        public GroceryMarket(Stends stends, Queue<Buyer> buyers, List<GroceryInfo> dailyGroceryDemand, List<DailyGroceryReport> dailyGroceriesReports, List<WeeklyGroceryReport> weeklyGroceriesReports, Rating rating, int amountofBuyers, List<Worker> workers)
        {
            this.stends = stends;
            Buyers = buyers;
            DailyGroceryDemand = dailyGroceryDemand;
            DailyGroceriesReports = dailyGroceriesReports;
            WeeklyGroceriesReports = weeklyGroceriesReports;
            this.rating = rating;
            AmountofBuyers = amountofBuyers;
            Workers = workers;
        }

        public void SetDailyGrDemand(List<GroceryInfo> depoGroceries)
        {
            bool check = false;

            foreach (var depgr in depoGroceries)
            {
                foreach (var gr in DailyGroceriesReports)
                {
                    if (depgr.GroceryName == gr.Name)
                    {
                        foreach (var dgr in DailyGroceryDemand)
                        {
                            if (gr.Name == dgr.GroceryName)
                            {
                                dgr.Amount = Convert.ToInt32(gr.DailySold + gr.DailyThrown);
                                check = true;
                            }
                        }
                    }
                }
                if (!check) DailyGroceryDemand.Add(new(depgr.GroceryName, depgr.Amount / 7, depgr.PricePerKQ));

            }
        }

        public void SellingResults(ref DailyReport dailyReport, List<GroceryInfo> depoGroceries)
        {
            double soldAmount = 0;
            double thrownAmount = 0;

            foreach (var buyer in Buyers)
            {
                foreach (var gr in buyer.ToBuygroceries)
                {
                    bool dailyCheck = false;
                    bool weeklyCheck = false;
                    foreach (var st in stends.stends)
                    {
                        if (gr.GroceryName == st.Key)
                        {
                            for (int i = 0; i < gr.Amount; i++)
                            {
                                if (st.Value.Peek().New == true || st.Value.Peek().Normal == true)
                                {
                                    st.Value.Pop();
                                    soldAmount++;
                                }
                                else if (st.Value.Peek().Rotten == true || st.Value.Peek().Toxic == true)
                                {
                                    thrownAmount++;
                                    st.Value.Pop();
                                }

                            }
                            buyer.BougthGr.Add(gr.GroceryName, soldAmount);
                            rating.AddRatingInADay(gr.Amount, soldAmount);

                            dailyReport.DailyIncome += gr.PricePerKQ * soldAmount;

                        }
                    }
                    foreach (var grrep in DailyGroceriesReports)
                    {
                        if (gr.GroceryName == grrep.Name)
                        {
                            grrep.SetDailySoldGrs(soldAmount);
                            grrep.SetDailyThrownGrs(thrownAmount);
                            dailyCheck = true;
                        }
                    }
                    if (!dailyCheck) DailyGroceriesReports.Add(new(gr.GroceryName, thrownAmount, soldAmount));

                    foreach (var grrep in WeeklyGroceriesReports)
                    {
                        if (gr.GroceryName == grrep.Name)
                        {
                            grrep.SetWeeklySoldGrs(soldAmount);
                            grrep.SetWeeklySoldGrs(thrownAmount);
                            weeklyCheck = true;
                        }
                    }
                    if (!weeklyCheck)
                    {
                        foreach (var dgr in depoGroceries)
                        {
                            if (gr.GroceryName == dgr.GroceryName)
                                WeeklyGroceriesReports.Add(new(gr.GroceryName, thrownAmount, soldAmount, dgr.PricePerKQ));
                        }
                    };
                }
                Buyers.Dequeue();
                AmountofBuyers++;
            }
        }

        public void SetWorkers( bool check)
        {
            if (check) Workers.Add(new(70));
        }
    }
}






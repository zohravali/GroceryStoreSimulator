using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSimulator
{
    class Depo
    {

        public List<GroceryInfo> depoGroceriesCapacity { get; set; }

        public List<GroceryInfo> depoGroceries { get; set; }

        public List<GroceryInfo> groceryDemandWeekly { get; set; }

        public Depo(List<GroceryInfo> depoGroceriesCapacity, List<GroceryInfo> depoGroceries, List<GroceryInfo> groceryDemandWeekly)
        {
            this.depoGroceriesCapacity = depoGroceriesCapacity;
            this.depoGroceries = depoGroceries;
            this.groceryDemandWeekly = groceryDemandWeekly;
        }

        public void RemainingGroceries(List<GroceryInfo> DailyGroceryDemand)
        {
            for (int i = 0; i < depoGroceries.Count; i++)
            {
                for (int j = 0; j < DailyGroceryDemand.Count; j++)
                {
                    if (depoGroceries[i].GroceryName == DailyGroceryDemand[j].GroceryName)
                        depoGroceries[i].Amount -= DailyGroceryDemand[j].Amount;
                }
            }
        }

        public void WeeklyReset( )
        {
            bool check = false;

            foreach (var grc in depoGroceriesCapacity)
            {
                foreach (var gr in depoGroceries)
                {
                    if (grc.GroceryName == gr.GroceryName)
                    {
                        gr.Amount = grc.Amount;
                        check = true;
                    }
                }
                if (!check) depoGroceries.Add(grc);
            }
        }

        public void SetGroceryDemandWeekly(List<WeeklyGroceryReport> weeklyGrDem)
        {
            foreach (var dgr in weeklyGrDem)
            {
                int amount = Convert.ToInt32( dgr.WeeklySold + dgr.WeeklyThrown);
                groceryDemandWeekly.Add(new(dgr.Name, amount, dgr.Price));
            }
          
        }

        public void IncreaseCapacity( bool inc, List<WeeklyGroceryReport> grReports,  ref WeeklyReport wre, ref GroceryMarket grdaily)
        {
            if (inc)
            {
                foreach (var gr in depoGroceriesCapacity)
                {
                    foreach (var g in grReports)
                    {
                        if (gr.GroceryName == g.Name && g.WeeklySold >= gr.Amount * 0.8 && wre.Budget >= gr.Amount * 0.25 * gr.PricePerKQ)
                        {
                            gr.Amount += Convert.ToInt32(gr.Amount * 0.25) ;
                            wre.WeeklyExpense += gr.Amount * 0.25 * gr.PricePerKQ;
                            wre.Budget -= gr.Amount * 0.25 * gr.PricePerKQ;
                        }
                    }

                    foreach (var dgr in grdaily.DailyGroceryDemand)
                    {
                        if (gr.GroceryName == dgr.GroceryName) dgr.Amount += Convert.ToInt32( dgr.Amount * 0.05);
                    }
                }
            }

        }

        public void AddNewGrocery( bool addgr, List<GroceryInfo> newGr, ref WeeklyReport wre)
        {
            if (addgr)
            {
                foreach (var gr in newGr)
                {
                    if (wre.Budget >= gr.Amount * gr.PricePerKQ)
                    {
                        depoGroceriesCapacity.Add(gr);
                        wre.WeeklyExpense += gr.Amount * gr.PricePerKQ;
                        wre.Budget -= gr.Amount * gr.PricePerKQ;
                    }
                }
            }
        }

        //bir de Isci elave et methodu ve bool vari olacaq, bu uc ehtimal randomly secilecek, metodlar evente salinacaq, hansi true olsa o isleyecek.
    }

}

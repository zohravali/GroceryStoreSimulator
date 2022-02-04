using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSimulator
{

    class Stends
    {
        public Dictionary<string, Stack<GroceryDegree>> stends { get; set; }

        public Dictionary<string, Stack<GroceryDegree>> BoxWithNewGr { get; set; }

        public Dictionary<string, Stack<GroceryDegree>> BoxWithSecNewGr { get; set; }

        public Dictionary<string, Stack<GroceryDegree>> BoxWithNormalGr { get; set; }

        public Stends(Dictionary<string, Stack<GroceryDegree>> stends,  Dictionary<string, Stack<GroceryDegree>> boxWithNewGr, Dictionary<string, Stack<GroceryDegree>> boxWithSecNewGr, Dictionary<string, Stack<GroceryDegree>> boxWithNormalGr)
        {
            this.stends = stends;
            BoxWithNewGr = boxWithNewGr;
            BoxWithSecNewGr = boxWithSecNewGr;
            BoxWithNormalGr = boxWithNormalGr;
        }

        public void FillBoxeswithNewgr(List<GroceryInfo> dailyGrDem)
        {

            foreach (var gr in dailyGrDem)
            {
                Stack<GroceryDegree> boxWithNewGr = new();
                for (int i = 0; i < gr.Amount; i++)
                {
                    int n= random.RandomNumber(0, 2);
                    if (n == 0) boxWithNewGr.Push(new(true, false, false, false));
                     else if (n == 1) boxWithNewGr.Push(new(false, false, true, false));
                }
                BoxWithNewGr.Add(gr.GroceryName, boxWithNewGr);
            }
        }

        public void FillBoxeswithSecnew()
        {
            foreach (var gr in stends)
            {
                Stack<GroceryDegree> boxWithSecNewGr = new();
                foreach (var g in gr.Value)
                {
                    if (g.New == true)
                    {
                        boxWithSecNewGr.Push(new(true, false, false, false));
                    }
                }
                BoxWithSecNewGr.Add(gr.Key, boxWithSecNewGr);
            }
        }

        public void FillBoxesWithNormalGr()
        {
            foreach (var gr in stends)
            {
                Stack<GroceryDegree> boxWithNormalGr = new();
                foreach (var g in gr.Value)
                {
                    if (g.Normal == true)
                    {
                        boxWithNormalGr.Push(new(false, true, false, false));
                      
                    }
                }
                BoxWithNormalGr.Add(gr.Key, boxWithNormalGr);
            }
        }

        public void ThrowRottenandToxicGr(ref GroceryMarket groceryMarket)
        {
            int amount = 0;
            foreach (var gr in stends)
            {
                foreach (var g in gr.Value)
                {
                    if (g.Rotten == true || g.Toxic == true)
                    {
                        gr.Value.Pop();
                        amount++;
                    }
                }
                foreach (var grep in groceryMarket.DailyGroceriesReports)
                {
                    if (gr.Key == grep.Name) grep.SetDailyThrownGrs(amount);
                }
                foreach (var grep in groceryMarket.WeeklyGroceriesReports)
                {
                    if (gr.Key == grep.Name) grep.SetWeeklyThrownGrs(amount);
                }
            }
        }

        public void FillStendsWithNewGr()
        {
            foreach (var stgr in stends)
            {
                stgr.Value.Clear();            
            }

            Stack<GroceryDegree> grocery = new();
            bool check = false;
            foreach (var ngr in BoxWithNewGr)
            {
                foreach (var stgr in stends)
                {
                    if (ngr.Key == stgr.Key)
                    {
                        foreach (var gr in ngr.Value)
                        {
                            if (gr.New == true)
                            {
                                stgr.Value.Push(gr);
                            }
                        }
                        check = true;
                        BoxWithNewGr.Remove(ngr.Key);
                    }
                }
                if (!check)
                {
                    foreach (var gr in ngr.Value)
                    {
                        if (gr.New == true)
                        {
                            grocery.Push(gr);
                        }
                    }
                    stends.Add(ngr.Key, grocery);
                    BoxWithNewGr.Remove(ngr.Key);
                    grocery.Clear();
                }
            }
        }

        public void FillStendsWithSecNewGr()
        {
            foreach (var st in stends)
            {
                foreach (var gr in BoxWithSecNewGr)
                {
                    for (int g = 0; g < gr.Value.Count; g++)
                    {
                        st.Value.Push(new(true, false, false, false));
                    }
                }
            }
            BoxWithSecNewGr.Clear();
        }

        public void FillStendsWithNormalGr()
        {
            foreach (var st in stends)
            {
                foreach (var gr in BoxWithNormalGr)
                {
                    for (int g = 0; g < gr.Value.Count; g++)
                    {
                        st.Value.Push(new(true, false, false, false));
                    }
                }
            }
            BoxWithNormalGr.Clear();
        }

    }
}

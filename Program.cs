using GroceryStoreSimulator;
using System;
using System.Collections.Generic;
using System.Timers;



//Simulator.CreateInstanses();
//int n = 0;

//var hour = new System.Timers.Timer(2000);
//hour.Elapsed += HourlyEvents;
//hour.AutoReset = true;
//hour.Enabled = true;

//var day = new System.Timers.Timer(26000);
//day.Elapsed += DailyEvents;
//day.AutoReset = true;
//day.Enabled = true;


////var week = new System.Timers.Timer(184000);
////w//eek.Elapsed += WeeklyEvents;
////week.AutoReset = true;
////week.Enabled = true;



//void HourlyEvents(Object source, System.Timers.ElapsedEventArgs e){
//    Console.WriteLine(n);
//    n++;
//}

//void DailyEvents(Object source, System.Timers.ElapsedEventArgs e)
//{
//    Console.WriteLine(n);
//    n=0;
//}

public class Example
{

    public static void Main()
    {
        SetTimer();

        Console.WriteLine("\nPress the Enter key to exit the application...\n");
        Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
        Console.ReadLine();


        Console.WriteLine("Terminating the application...");

    }

    private static void SetTimer()
    {

        Dictionary<string, double> groceriestoBuy = new(); //randomly secilib alicinin almaq istediyi liste elave edilcek
        groceriestoBuy.Add("alma", 0.45);
        groceriestoBuy.Add("kok", 0.35);
        groceriestoBuy.Add("portagal", 0.45);
        groceriestoBuy.Add("heyva", 0.45);
        groceriestoBuy.Add("kartof", 0.35);
        groceriestoBuy.Add("sogan", 0.3);
        groceriestoBuy.Add("pomidor", 0.45);
        groceriestoBuy.Add("xiyar", 0.45);
        groceriestoBuy.Add("kelem", 0.4);
        groceriestoBuy.Add("biber", 0.35);
        groceriestoBuy.Add("gulkelem", 35);
        groceriestoBuy.Add("kabacok", 0.4);
        groceriestoBuy.Add("brokoli", 0.4);
        groceriestoBuy.Add("banan", 0.45);
        groceriestoBuy.Add("armud", 0.4);
        groceriestoBuy.Add("kivi", 0.4);

        Queue<Buyer> Buyers = new();

        //DEPO

        List<GroceryInfo> depoGroceries = new();
        List<GroceryInfo> groceryDemandWeekly = new();
        List<GroceryInfo> depoGroceriesCapacity = new(); //eger hemin heftenin geliri xercden 5 defe coxdursa hem getirilen terevlerin sayi artir hem de yeni isci goturulur.
        depoGroceriesCapacity.Add(new("alma", 3500, 0.35));
        depoGroceriesCapacity.Add(new("kok", 3500, 0.3));
        depoGroceriesCapacity.Add(new("portagal", 3500, 0.4));
        depoGroceriesCapacity.Add(new("heyva", 3500, 0.4));
        depoGroceriesCapacity.Add(new("kartof", 4000, 0.3));
        depoGroceriesCapacity.Add(new("sogan", 4000, 0.25));
        depoGroceriesCapacity.Add(new("pomidor", 3000, 0.4));
        depoGroceriesCapacity.Add(new("xiyar", 3000, 0.4));
        depoGroceriesCapacity.Add(new("kelem", 2500, 0.35));
        depoGroceriesCapacity.Add(new("biber", 3500, 0.3));

        List<GroceryInfo> additionalGroceries = new(); // eger rating 5dirse yeni cesid elave edilecek
        additionalGroceries.Add(new("gulkelem", 2000, 0.3));
        additionalGroceries.Add(new("kabacok", 2000, 0.35));
        additionalGroceries.Add(new("brokoli", 2000, 0.35));
        additionalGroceries.Add(new("banan", 2500, 0.4));
        additionalGroceries.Add(new("armud", 2000, 0.35));
        additionalGroceries.Add(new("kivi", 2000, 0.35));

        Depo depo = new(depoGroceriesCapacity, depoGroceries, groceryDemandWeekly);

        //STENDS

        Dictionary<string, Stack<GroceryDegree>> stends = new();
        Dictionary<string, Stack<GroceryDegree>> BoxWithNewGr = new();
        Dictionary<string, Stack<GroceryDegree>> BoxWithSecNewGr = new();
        Dictionary<string, Stack<GroceryDegree>> BoxWithNormalGr = new();
        Stends Stend = new(stends, BoxWithNewGr, BoxWithSecNewGr, BoxWithNormalGr);

        //RATING

        Stack<int> RatingsInaDay = new();
        Stack<double> DailyRatings = new();
        Rating rating = new(RatingsInaDay, DailyRatings);

        Stack<double> WeeklyRatings = new();
        WeeklyRating weeklyRating = new(WeeklyRatings);

        //DailyReport

        double DailyRatAvg = 0;
        double DailyIncome = 0;
        Dictionary<string, double> NewGrs = new();
        Dictionary<string, double> ThrownGrs = new();
        int BuyersAmount = 0;
        DailyReport dailyReport = new(DailyRatAvg, DailyIncome, NewGrs, ThrownGrs, BuyersAmount);

        //WeeklyReport

        double WeeklyRatingRes = 0;
        double WeeklyIncome = 0;
        double WeeklyExpense = 0;
        double Budget = 0;
        int WorkerAmount = 0;
        Dictionary<string, double> WeeklyThrownGrs = new();
        WeeklyReport weeklyReport = new(WeeklyRatingRes, WeeklyIncome, WeeklyExpense, Budget, WorkerAmount, WeeklyThrownGrs);

        //GroceryMarket

        List<GroceryInfo> DailyGroceryDemand = new();
        List<DailyGroceryReport> DailyGroceriesReports = new();
        List<WeeklyGroceryReport> WeeklyGroceriesReports = new();
        int AmountofBuyers = 0;
        List<Worker> Workers = new();
        GroceryMarket grMarket = new(Stend, Buyers, DailyGroceryDemand, DailyGroceriesReports, WeeklyGroceriesReports, rating, AmountofBuyers, Workers);

        //START

        depo.WeeklyReset();
        weeklyReport.WeeklyExpense = 11600;
        dailyReport.DailyRatAvg = 3;
        weeklyReport.Budget = 5000;
        grMarket.SetDailyGrDemand(depo.depoGroceries);
        Stend.FillBoxeswithNewgr(grMarket.DailyGroceryDemand);
        Stend.FillStendsWithNewGr();
        weeklyReport.WeeklyRatingRes = 3;


        var list = new List<WeeklyReport>();
        list.Add(weeklyReport);
        JsonFile.JsonWrite(list);

        var hour = new System.Timers.Timer(2000);
        hour.Elapsed += HourlyEvents;
        hour.AutoReset = true;
        hour.Enabled = true;

        var day = new System.Timers.Timer(26000);
        day.Elapsed += DailyEvents;
        day.AutoReset = true;
        day.Enabled = true;


        var week = new System.Timers.Timer(184000);
        week.Elapsed += WeeklyEvents;
        week.AutoReset = true;
        week.Enabled = true;

        var aging = new System.Timers.Timer(14000);
        week.Elapsed += AgingEvent;
        week.AutoReset = true;
        week.Enabled = true;

        var pandemia = new System.Timers.Timer(368000);
        week.Elapsed += PandemiaEvent;
        week.AutoReset = true;
        week.Enabled = true;

        void HourlyEvents(Object source, System.Timers.ElapsedEventArgs e)
        {
            random.FillBuyersQueue(ref grMarket, groceriestoBuy, dailyReport.DailyRatAvg);
            grMarket.SellingResults(ref dailyReport, depo.depoGroceries);
        }

        void DailyEvents(Object source, System.Timers.ElapsedEventArgs e)
        {
            rating.DailyRatingAvg(ref dailyReport);
            weeklyReport.SetWeeklyIncome(dailyReport.DailyIncome);
            dailyReport.SetNewGrAmount(grMarket.DailyGroceryDemand);
            dailyReport.SetBuyerAmount(grMarket.AmountofBuyers);
            Stend.FillBoxeswithSecnew();
            Stend.FillBoxesWithNormalGr();
            Stend.ThrowRottenandToxicGr(ref grMarket);
            dailyReport.SetThrownGrAmount(grMarket.DailyGroceriesReports);
            dailyReport.Show();
            grMarket.SetDailyGrDemand(depo.depoGroceries);
            Stend.FillBoxeswithNewgr(grMarket.DailyGroceryDemand);
            Stend.FillStendsWithNewGr();
            Stend.FillStendsWithSecNewGr();
            Stend.FillStendsWithNormalGr();
            rating.RatingsInaDay.Clear();
            dailyReport.DailyIncome = 0;
            grMarket.AmountofBuyers = 0;
            grMarket.DailyGroceriesReports.Clear();
            dailyReport.ThrownGrs.Clear();
            dailyReport.NewGrs.Clear();
        }


        void WeeklyEvents(Object source, System.Timers.ElapsedEventArgs e)
        {
            JsonFile.JsonWriteToList(ref list);
            foreach (var item in list)
            {
                weeklyRating.WeeklyRatings.Push(item.WeeklyRatingRes);
            }
            weeklyRating.WeeklyRatingAvg(ref weeklyReport, rating.DailyRatings);
            weeklyReport.SetWorkerAmount(grMarket.Workers);
            weeklyReport.SetThrownGrAmount(grMarket.WeeklyGroceriesReports);
            list.Add(weeklyReport);
            JsonFile.JsonWrite(list);
            JsonFile.JsonRead(ref list);
            depo.SetGroceryDemandWeekly(grMarket.WeeklyGroceriesReports);
            bool i = random.InreaseCheck(weeklyReport.WeeklyExpense, weeklyReport.Budget);
            grMarket.SetWorkers(i);
            weeklyReport.SetWeeklyExpense(depo.groceryDemandWeekly, grMarket.Workers.Count, 70);
            i = random.InreaseCheck(weeklyReport.WeeklyExpense, weeklyReport.Budget);
            depo.IncreaseCapacity(i, grMarket.WeeklyGroceriesReports, ref weeklyReport, ref grMarket);
            i = random.IncreaseVarietyCheck(weeklyReport.WeeklyRatingRes);
            depo.AddNewGrocery(i, additionalGroceries, ref weeklyReport);
            depo.WeeklyReset();
            grMarket.WeeklyGroceriesReports.Clear();
            depo.groceryDemandWeekly.Clear();
            weeklyReport.WeeklyIncome = 0;
            weeklyReport.ThrownGrs.Clear();
        }

        void AgingEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            random.AgingGroceries(ref Stend);
            grMarket.stends = Stend;

        }

        void PandemiaEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            weeklyReport.WeeklyIncome = 0;
            weeklyReport.WeeklyExpense = 0;
            weeklyReport.ThrownGrs.Clear();
            list.Add(weeklyReport);
            JsonFile.JsonWrite(list);
            JsonFile.JsonRead(ref list);

        }
    }

}


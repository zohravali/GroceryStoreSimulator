using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSimulator
{
    class Rating
    {
        public Stack<int> RatingsInaDay { get; set; }

        public Stack<double> DailyRatings { get; set; }

        public Rating()
        {

        }

        public Rating(Stack<int> ratingsInaDay, Stack<double> dailyRatings)
        {
            RatingsInaDay = ratingsInaDay;
            DailyRatings = dailyRatings;
        }

        public void AddRatingInADay(double plannedAmount, double buyedAmount)
        {
            if (buyedAmount >= plannedAmount * 0.9 && buyedAmount <= plannedAmount) RatingsInaDay.Push(5);
            else if (buyedAmount >= plannedAmount * 0.7 && buyedAmount < plannedAmount * 0.9) RatingsInaDay.Push(4);
            else if (buyedAmount >= plannedAmount * 0.5 && buyedAmount < plannedAmount * 0.7) RatingsInaDay.Push(3);
            else if (buyedAmount >= plannedAmount * 0.3 && buyedAmount < plannedAmount * 0.5) RatingsInaDay.Push(2);
            else if (buyedAmount >= plannedAmount * 0.1 && buyedAmount <= plannedAmount * 0.3) RatingsInaDay.Push(1);
            else RatingsInaDay.Push(0);
        }

        public void DailyRatingAvg(ref DailyReport dr)
        {
            dr.DailyRatAvg = RatingsInaDay.Average();

            DailyRatings.Push(dr.DailyRatAvg);
        }


       
    }

    class WeeklyRating
    {
        public Stack<double> WeeklyRatings { get; set; }//bunu filedan goturub elave etmeliyem sonra onun ortalamasini tapib cari heftenin ortalamsni cixartmaliyam

        public WeeklyRating(Stack<double> weeklyRatings)
        {
            WeeklyRatings = weeklyRatings;
        }

        public void WeeklyRatingAvg(ref WeeklyReport wr, Stack<double> DailyRatings)
        {
            wr.WeeklyRatingRes = (WeeklyRatings.Average() + DailyRatings.Average()) / 2;
        }
    }
}

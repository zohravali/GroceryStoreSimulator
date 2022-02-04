using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroceryStoreSimulator
{
   static class JsonFile
    {
        public static void JsonWrite(List<WeeklyReport> weeklyReports)
        {
            var str = JsonConvert.SerializeObject(weeklyReports, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("weeklyReport.json", str);
        }

        public static void JsonWriteToList(ref List<WeeklyReport> weeklyReports)
        {
            var jsonStr = File.ReadAllText("weeklyReport.json");

            weeklyReports = JsonConvert.DeserializeObject<List<WeeklyReport>>(jsonStr);

        }

        public static void JsonRead(ref List<WeeklyReport> weeklyReports)
        {
            var jsonStr = File.ReadAllText("weeklyReport.json");

            weeklyReports = JsonConvert.DeserializeObject<List<WeeklyReport>>(jsonStr);
            weeklyReports.Last().Show();
        }
    }
}

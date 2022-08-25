using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using BLNutrition;
using BONutrition;
using DLNutrition;

namespace BLNutrition
{
    public class NSysServeUnitManager
    {
        public static List<NSysServeUnit> GetList()
        {
            return NSysServeUnitDL.GetList();
        }

        public static NSysServeUnit GetItem(int serveUnitID)
        {
            return NSysServeUnitDL.GetItem(serveUnitID);
        }
    }
}

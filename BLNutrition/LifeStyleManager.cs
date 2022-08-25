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
    public class LifeStyleManager
    {
        public static LifeStyle GetItem(int LanguageID, int LifeStyleID)
        {
            return LifeStyleDL.GetItem(LanguageID, LifeStyleID);
        }
    }
}

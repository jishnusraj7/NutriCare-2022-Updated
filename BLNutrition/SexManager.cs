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
    public class SexManager
    {
        public static Sex GetItem(int LanguageID, int SexID)
        {
            return SexDL.GetItem(LanguageID, SexID);
        }
    }
}

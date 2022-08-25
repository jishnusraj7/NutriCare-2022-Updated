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
    public class BloodGroupManager
    {
        public static BloodGroup GetItem(int LanguageID, int BloodGroupID)
        {
            return BloodGroupDL.GetItem(LanguageID, BloodGroupID);
        }
    }
}

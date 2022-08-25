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
    public class EthnicManager
    {
        public static List<Ethnic> GetEthnic()
        {
            return EthnicDL.GetEthnic();
        }

        public static Ethnic GetItem(int ethnicID)
        {
            return EthnicDL.GetItem(ethnicID);
        }
    }
}

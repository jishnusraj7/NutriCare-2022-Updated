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
    public class NSysNutrientManager
    {
        #region SysNutrient

        public static List<NSysNutrient> GetListNutrient()
        {
            return NSysNutrientDL.GetListNutrient();
        }

        public static List<NSysNutrient> GetListNutrient(byte nutrientGroup)
        {
            return NSysNutrientDL.GetListNutrient(nutrientGroup);
        }

        public static NSysNutrient GetItemNutrient(byte nutrientID, byte nutrientGroup)
        {
            return NSysNutrientDL.GetItemNutrient(nutrientID, nutrientGroup);
        }

        public static List<NSysNutrient> GetListNutrientMain(byte nutrientGroup)
        {
            return NSysNutrientDL.GetListNutrientMain(nutrientGroup, true);
        }

        public static NSysNutrient GetNutrientID(string nutrientParam)
        {
            return NSysNutrientDL.GetNutrientID(nutrientParam);
        }

        #endregion

        #region SysAyurvedic

        public static List<NSysNutrient> GetListAyurvedic()
        {
            return NSysNutrientDL.GetListAyurvedic();
        }       

        #endregion
    }
}

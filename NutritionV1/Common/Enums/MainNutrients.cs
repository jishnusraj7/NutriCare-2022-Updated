using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace NutritionV1.Enums
{
    public enum MainNutrients
    {
        Calorie = 1,
        Protien = 2,
        CarboHydrates = 3,
        FAT = 4,
        Fibre = 5,
        Iron = 13,
        Calcium = 10,
        Phosphorus = 12,
        [Description("VitaminA(Retinol)")]
        VitaminA_Retinol = 16,
        [Description("VitaminA(BetaCarotene)")]
        VitaminA_BetaCarotene = 17,
        Thiamine = 31,
        Riboflavin = 18,
        NicotinicAcid = 122,
        Pyridoxine = 123,
        FolicAcid = 27,
        VitaminB12 = 26,
        VitaminC = 29
    }
}

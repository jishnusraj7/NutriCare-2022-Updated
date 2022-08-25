using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NutritionV1
{
    public enum CalorieType
    {
        CaloriePregnancy = 300,
        CalorieLactation1 = 550,
        CalorieLactation2 = 400, 

        CalorieSimulation = 500,
        CalorieWeightLoss = 500,
        CalorieMaintainWeight = 0,
        CalorieWeightGain = 1000,
        CalorieLowerLimit = 1000,
        CalorieUpperLimit = 5000,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class NSysAdmin
    {
        private byte vpk;
        private byte foodHabitID;
        private byte lifeStyleID;
        private byte sexID;
        private byte religionID;
        private byte countryID;
        private byte stateID;
        private byte cityID;
        private byte bloodGroupID;
        private byte exerciseID;
        private byte bodyTypeID;
        private byte jobNatureID;
        private byte clinicalUnitID;
        private byte thoughtID;
        private byte unitID;
        private float unitValue;
        private int keyWordID;
        private byte infantID;

        private string description;
        private string foodHabitName;
        private string lifeStyleName;
        private string sexName;
        private string religionName;
        private string countryName;
        private string stateName;
        private string cityName;
        private string bloodGroupName;
        private string exerciseName;
        private string bodyTypeName;
        private string jobNatureName;
        private string clinicalUnitName;
        private string thoughtDescription;
        private string thoughtBy;
        private string unitName;
        private string keyWordDescription;
        private string keyWordCode;
        private string infantName;

        private float infantWeight;
        private float infantHeight;

        private byte bmiImpactID;
        private string bmiImpactName;
        private string bmiIMpactDescription;
        private string bmiIMpactComments;
        private string bmiIMpactSuggetions;
        private string bmiIMpactImagePath;

        private bool unitType;
        private float normalLow;
        private float normalHigh;
        private bool isNormal;

        private float clinicalParamNormalValueFrom;
        private float clinicalParamNormalValueTo;
        private string clinicalParamNormalValueDes;

        private string helpItemName;
        private string helpItemDescription;

        private string formFlowName;
        private string formFlowDescription;

        private string privacyLegalName;
        private string privacyLegalDescription;

        /// <summary>
        /// Get or Set a FeatureDescription - nvarchar(150)
        /// </summary>
        public string Description
        {
            set
            {
                description = value;
            }
            get
            {
                return description;

            }

        }

        /// <summary>
        /// Get or Set a FoodHabitName - nvarchar(50)
        /// </summary>
        public string FoodHabitName
        {
            set
            {
                foodHabitName = value;
            }
            get
            {
                return foodHabitName;

            }

        }

        /// <summary>
        /// Get or Set a LifeStyleName - nvarchar(50)
        /// </summary>
        public string LifeStyleName
        {
            set
            {
                lifeStyleName = value;
            }
            get
            {
                return lifeStyleName;

            }

        }

        /// <summary>
        /// Get or Set a SexName - nvarchar(50)
        /// </summary>
        public string SexName
        {
            set
            {
                sexName = value;
            }
            get
            {
                return sexName;

            }

        }

        /// <summary>
        /// Get or Set a ReligionName - nvarchar(50)
        /// </summary>
        public string ReligionName
        {
            set
            {
                religionName = value;
            }
            get
            {
                return religionName;

            }

        }

        /// <summary>
        /// Get or Set a CountryName - nvarchar(50)
        /// </summary>
        public string CountryName
        {
            set
            {
                countryName = value;
            }
            get
            {
                return countryName;

            }

        }

        /// <summary>
        /// Get or Set a StateName - nvarchar(50)
        /// </summary>
        public string StateName
        {
            set
            {
                stateName = value;
            }
            get
            {
                return stateName;

            }

        }

        /// <summary>
        /// Get or Set a CityName - nvarchar(50)
        /// </summary>
        public string CityName
        {
            set
            {
                cityName = value;
            }
            get
            {
                return cityName;

            }

        }

        /// <summary>
        /// Get or Set a BloodGroupName - nvarchar(50)
        /// </summary>
        public string BloodGroupName
        {
            set
            {
                bloodGroupName = value;
            }
            get
            {
                return bloodGroupName;

            }

        }

        /// <summary>
        /// Get or Set a ExerciseName - nvarchar(50)
        /// </summary>
        public string ExerciseName
        {
            set
            {
                exerciseName = value;
            }
            get
            {
                return exerciseName;

            }

        }

        /// <summary>
        /// Get or Set a BodyTypeName - nvarchar(50)
        /// </summary>
        public string BodyTypeName
        {
            set
            {
                bodyTypeName = value;
            }
            get
            {
                return bodyTypeName;

            }

        }

        /// <summary>
        /// Get or Set a JobNatureName - nvarchar(50)
        /// </summary>
        public string JobNatureName
        {
            set
            {
                jobNatureName = value;
            }
            get
            {
                return jobNatureName;

            }

        }

        /// <summary>
        /// Get or Set a ClinicalUnitName - nvarchar(50)
        /// </summary>
        public string ClinicalUnitName
        {
            set
            {
                clinicalUnitName = value;
            }
            get
            {
                return clinicalUnitName;

            }

        }

        /// <summary>
        /// Get or Set a ThoughtDescription - nvarchar(150)
        /// </summary>
        public string ThoughtDescription
        {
            set
            {
                thoughtDescription = value;
            }
            get
            {
                return thoughtDescription;

            }

        }

        /// <summary>
        /// Get or Set a ThoughtBy - nvarchar(50)
        /// </summary>
        public string ThoughtBy
        {
            set
            {
                thoughtBy = value;
            }
            get
            {
                return thoughtBy;

            }

        }

        public string KeyWordDescription
        {
            set
            {
                keyWordDescription = value;
            }
            get
            {
                return keyWordDescription;

            }

        }

        public string KeyWordCode
        {
            set
            {
                keyWordCode = value;
            }
            get
            {
                return keyWordCode;

            }

        }

        public string InfantName
        {
            set
            {
                infantName = value;
            }
            get
            {
                return infantName;

            }

        }

        /// <summary>
        /// Get or Set a VPK - tinyint
        /// </summary>
        public byte VPK
        {
            set
            {
                vpk = value;
            }
            get
            {
                return vpk;

            }

        }

        /// <summary>
        /// Get or Set a UnitName - nvarchar(50)
        /// </summary>
        public string UnitName
        {
            set
            {
                unitName = value;
            }
            get
            {
                return unitName;

            }

        }

        /// <summary>
        /// Get or Set a FoodHabitID - tinyint
        /// </summary>
        public byte FoodHabitID
        {
            set
            {
                foodHabitID = value;
            }
            get
            {
                return foodHabitID;

            }

        }

        /// <summary>
        /// Get or Set a LifeStyleID - tinyint
        /// </summary>
        public byte LifeStyleID
        {
            set
            {
                lifeStyleID = value;
            }
            get
            {
                return lifeStyleID;

            }

        }

        /// <summary>
        /// Get or Set a SexID - tinyint
        /// </summary>
        public byte SexID
        {
            set
            {
                sexID = value;
            }
            get
            {
                return sexID;

            }

        }

        /// <summary>
        /// Get or Set a ReligionID - tinyint
        /// </summary>
        public byte ReligionID
        {
            set
            {
                religionID = value;
            }
            get
            {
                return religionID;

            }

        }

        /// <summary>
        /// Get or Set a CountryID - tinyint
        /// </summary>
        public byte CountryID
        {
            set
            {
                countryID = value;
            }
            get
            {
                return countryID;

            }

        }

        /// <summary>
        /// Get or Set a StateID - tinyint
        /// </summary>
        public byte StateID
        {
            set
            {
                stateID = value;
            }
            get
            {
                return stateID;

            }

        }

        /// <summary>
        /// Get or Set a CityID - tinyint
        /// </summary>
        public byte CityID
        {
            set
            {
                cityID = value;
            }
            get
            {
                return cityID;

            }

        }

        /// <summary>
        /// Get or Set a BloodGroupID - tinyint
        /// </summary>
        public byte BloodGroupID
        {
            set
            {
                bloodGroupID = value;
            }
            get
            {
                return bloodGroupID;

            }

        }

        /// <summary>
        /// Get or Set a ExerciseID - tinyint
        /// </summary>
        public byte ExerciseID
        {
            set
            {
                exerciseID = value;
            }
            get
            {
                return exerciseID;

            }

        }

        /// <summary>
        /// Get or Set a BodyTypeID - tinyint
        /// </summary>
        public byte BodyTypeID
        {
            set
            {
                bodyTypeID = value;
            }
            get
            {
                return bodyTypeID;

            }

        }

        /// <summary>
        /// Get or Set a JobNatureID - tinyint
        /// </summary>
        public byte JobNatureID
        {
            set
            {
                jobNatureID = value;
            }
            get
            {
                return jobNatureID;

            }

        }

        /// <summary>
        /// Get or Set a ClinicalUnitID - tinyint
        /// </summary>
        public byte ClinicalUnitID
        {
            set
            {
                clinicalUnitID = value;
            }
            get
            {
                return clinicalUnitID;

            }

        }

        /// <summary>
        /// Get or Set a ThoughtID - tinyint
        /// </summary>
        public byte ThoughtID
        {
            set
            {
                thoughtID = value;
            }
            get
            {
                return thoughtID;

            }

        }

        /// <summary>
        /// Get or Set a UnitID - tinyint
        /// </summary>
        public byte UnitID
        {
            set
            {
                unitID = value;
            }
            get
            {
                return unitID;

            }

        }

        public float UnitValue
        {
            set
            {
                unitValue = value;
            }
            get
            {
                return unitValue;

            }

        }

        public int KeyWordID
        {
            set
            {
                keyWordID = value;
            }
            get
            {
                return keyWordID;

            }

        }

        public byte InfantID
        {
            set
            {
                infantID = value;
            }
            get
            {
                return infantID;

            }

        }

        public float InfantHeight
        {
            set
            {
                infantHeight = value;
            }
            get
            {
                return infantHeight;

            }

        }

        public float InfantWeight
        {
            set
            {
                infantWeight = value;
            }
            get
            {
                return infantWeight;

            }

        }

        public byte BMIImpactID
        {
            get
            {
                return bmiImpactID;
            }
            set
            {
                bmiImpactID = value;
            }
        }

        public string BMIImpactName
        {
            get
            {
                return bmiImpactName;
            }
            set
            {
                bmiImpactName = value;
            }
        }

        public string BMIIMpactDescription
        {
            get
            {
                return bmiIMpactDescription;
            }
            set
            {
                bmiIMpactDescription = value;
            }
        }

        public string BMIIMpactComments
        {
            get
            {
                return bmiIMpactComments;
            }
            set
            {
                bmiIMpactComments = value;
            }
        }

        public string BMIIMpactSuggetions
        {
            get
            {
                return bmiIMpactSuggetions;
            }
            set
            {
                bmiIMpactSuggetions = value;
            }
        }

        public string BMIIMpactImagePath
        {
            get
            {
                return bmiIMpactImagePath;
            }
            set
            {
                bmiIMpactImagePath = value;
            }
        }


        public bool UnitType
        {
            get
            {
                return unitType;
            }
            set
            {
                unitType = value;
            }
        }

        public float NormalLow
        {
            get
            {
                return normalLow;
            }
            set
            {
                normalLow = value;
            }
        }

        public float NormalHigh
        {
            get
            {
                return normalHigh;
            }
            set
            {
                normalHigh = value;
            }
        }

        public bool IsNormal
        {
            get
            {
                return isNormal;
            }
            set
            {
                isNormal = value;
            }
        }

        public float ClinicalParamNormalValueFrom
        {
            get
            {
                return clinicalParamNormalValueFrom;
            }
            set
            {
                clinicalParamNormalValueFrom = value;
            }
        }

        public float ClinicalParamNormalValueTo
        {
            get
            {
                return clinicalParamNormalValueTo;
            }
            set
            {
                clinicalParamNormalValueTo = value;
            }
        }

        public string ClinicalParamNormalValueDes
        {
            get
            {
                return clinicalParamNormalValueDes;
            }
            set
            {
                clinicalParamNormalValueDes = value;
            }
        }

        public string HelpItemName
        {
            get
            {
                return helpItemName;
            }
            set
            {
                helpItemName = value;
            }
        }

        public string HelpItemDescription
        {
            get
            {
                return helpItemDescription;
            }
            set
            {
                helpItemDescription = value;
            }
        }

        public string FormFlowName
        {
            get
            {
                return formFlowName;
            }
            set
            {
                formFlowName = value;
            }
        }

        public string FormFlowDescription
        {
            get
            {
                return formFlowDescription;
            }
            set
            {
                formFlowDescription = value;
            }
        }

        public string PrivacyLegalName
        {
            get
            {
                return privacyLegalName;
            }
            set
            {
                privacyLegalName = value;
            }
        }

        public string PrivacyLegalDescription
        {
            get
            {
                return privacyLegalDescription;
            }
            set
            {
                privacyLegalDescription = value;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class Member
    {
        private int familyID;
        private int memberID;
        private string memberName;
        private byte sexID;
        private DateTime dob;
        private byte lifeStyleID;
        private byte bloodGroupID;
        private bool pregnancy;
        private bool lactation;
        private byte lactationType;
        private byte displayOrder;

        private byte exerciseID;
        private int exerciseDuration;

        private int featureID;
        private int vpk;

        private float height;
        private float weight;
        private int bodyTypeID;
        private int jobNatureID;
        private string jobDuration;
        private string sleepTime;
        private bool smoking;
        private bool drinking;
        private bool diabetes;
        private bool cholosterol;
        private float parameterValue;
        private string parameterName;

        private int clinicalParamID;
        private int clinicalAgeGroupID;
        private DateTime modifiedDate;
        private bool isHigh;
        private bool unitType;
        private byte clinicalUnitID;
        private float currentValue;
        private bool isNormal;
        private string keyWordCode;
        private string imagePath;

        private Sex sexItem;
        private BloodGroup bloodGroupItem;
        private FoodHabit foodHabitItem;
        private LifeStyle lifeStyleItem;

        private int age;
        private string sexName;
        private string bloodGroupName;
        private string lifeStyleName;
        private string bodyTypeName;
        private float waist;
        private byte foodHabitID;

        #region FamilyMember

        /// <summary>
        /// Get or Set a familyID - int
        /// </summary>
        public int FamilyID
        {
            set
            {
                familyID = value;
            }
            get
            {
                return familyID;

            }

        }

        /// <summary>
        /// Get or Set a MemberID - int
        /// </summary>
        public int MemberID
        {
            set
            {
                memberID = value;
            }
            get
            {
                return memberID;

            }

        }

        /// <summary>
        /// Get or Set a MemberName - nvarchar(50)
        /// </summary>
        public string MemberName
        {
            set
            {
                memberName = value;
            }
            get
            {
                return memberName;

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
        /// Get or Set a DOB - datetime
        /// </summary>
        public DateTime DOB
        {
            set
            {
                dob = value;
            }
            get
            {
                return dob;

            }

        }

        /// <summary>
        /// Get or Set a Age - int
        /// </summary>
        public int Age
        {
            set
            {
                age = value;
            }
            get
            {
                return age;

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

        public bool Pregnancy
        {
            get
            {
                return pregnancy;
            }
            set
            {
                pregnancy = value;
            }

        }

        public bool Lactation
        {
            get
            {
                return lactation;
            }
            set
            {
                lactation = value;
            }

        }

        public byte LactationType
        {
            set
            {
                lactationType = value;
            }
            get
            {
                return lactationType;

            }
        }

        public byte DisplayOrder
        {
            set
            {
                displayOrder = value;
            }
            get
            {
                return displayOrder;

            }
        }
        public string ImagePath
        {
            set
            {
                imagePath = value;
            }
            get
            {
                return imagePath;

            }
        }


        #endregion

        #region FamilyExercise

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
        /// Get or Set a ExerciseDuration - int
        /// </summary>
        public int ExerciseDuration
        {
            set
            {
                exerciseDuration = value;
            }
            get
            {
                return exerciseDuration;

            }

        }

        #endregion

        #region FamilyAyurvedic

        /// <summary>
        /// Get or Set a FeatureID - int
        /// </summary>
        public int FeatureID
        {
            set
            {
                featureID = value;
            }
            get
            {
                return featureID;

            }

        }

        /// <summary>
        /// Get or Set a VPK - int
        /// </summary>
        public int VPK
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

        #endregion

        #region FamilyGeneral

        public float Height
        {
            get 
            { 
                return height; 
            }
            set 
            { 
                height = value; 
            }

        }

        public float Weight
        {
            get 
            { 
                return weight; 
            }
            set 
            { 
                weight = value; 
            }

        }

        public int BodyTypeID
        {
            get 
            { 
                return bodyTypeID; 
            }
            set 
            { 
                bodyTypeID = value; 
            }

        }

        public int JobNatureID
        {
            get 
            { 
                return jobNatureID; 
            }
            set 
            { 
                jobNatureID = value; 
            }

        }

        public string JobDuration
        {
            get 
            { 
                return jobDuration; 
            }
            set 
            { 
                jobDuration = value; 
            }

        }

        public string SleepTime
        {
            get 
            { 
                return sleepTime; 
            }
            set 
            { 
                sleepTime = value; 
            }

        }

        public bool Smoking
        {
            get 
            { 
                return smoking; 
            }
            set 
            { 
                smoking = value; 
            }

        }

        public bool Drinking
        {
            get 
            { 
                return drinking; 
            }
            set 
            { 
                drinking = value; 
            }

        }

        public bool Diabetes
        {
            get
            {
                return diabetes;
            }
            set
            {
                diabetes = value;
            }

        }

        public bool Cholosterol
        {
            get
            {
                return cholosterol;
            }
            set
            {
                cholosterol = value;
            }

        }

        public float ParameterValue
        {
            get
            {
                return parameterValue;
            }
            set
            {
                parameterValue = value;
            }

        }

        public string ParameterName
        {
            get
            {
                return parameterName;
            }
            set
            {
                parameterName = value;
            }

        }

        #endregion

        #region FamilyClinical

        public int ClinicalParamID
        {
            get 
            { 
                return clinicalParamID; 
            }
            set 
            { 
                clinicalParamID = value; 
            }
        }

        public int ClinicalAgeGroupID
        {
            get 
            { 
                return clinicalAgeGroupID; 
            }
            set 
            { 
                clinicalAgeGroupID = value; 
            }
        }

        public DateTime ModifiedDate
        {
            get 
            { 
                return modifiedDate; 
            }
            set 
            { 
                modifiedDate = value; 
            }
        }

        public bool IsHigh
        {
            get 
            { 
                return isHigh; 
            }
            set
            { 
                isHigh = value;
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

        public byte ClinicalUnitID
        {
            get 
            { 
                return clinicalUnitID; 
            }
            set 
            { 
                clinicalUnitID = value; 
            }
        }

        public float CurrentValue
        {
            get 
            { 
                return currentValue; 
            }
            set 
            { 
                currentValue = value; 
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

        public string KeyWordCode
        {
            get
            {
                return keyWordCode;
            }
            set
            {
                keyWordCode = value;
            }
        }

        #endregion

        #region MemberList

        public Sex SexItem
        {
            set
            {
                sexItem = value;
            }
            get
            {
                return sexItem;
            }
        }

        public BloodGroup BloodGroupItem
        {
            set
            {
                bloodGroupItem = value;
            }
            get
            {
                return bloodGroupItem;
            }
        }

        public FoodHabit FoodHabitItem
        {
            set
            {
                foodHabitItem = value;
            }
            get
            {
                return foodHabitItem;
            }
        }

        public LifeStyle LifeStyleItem
        {
            set
            {
                lifeStyleItem = value;
            }
            get
            {
                return lifeStyleItem;
            }
        }

        /// <summary>
        /// Get or Set a SexName - string
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
        /// Get or Set a BloodGroupName - string
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
        /// Get or Set a LifeStyleName - string
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
        /// Get or Set a BodyTypeName - string
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
        /// Get or Set a Waist - float
        /// </summary>
        public float Waist
        {
            set
            {
                waist = value;
            }
            get
            {
                return waist;

            }
        }
        

        #endregion
    }
}

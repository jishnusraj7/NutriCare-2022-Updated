using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class Ingredient
    {
        #region Varaibles

        // Ingredient Table
        private int id;
        private int ethnicID;
        private byte foodHabitID;
        private string ayurvedicFavourable;
        private string ayurvedicUnFavourable;
        private string ayurvedicBalanced;
        private string medicalFavourable;
        private string medicalUnFavourable;
        private string medicalBalanced;
        private string keywords;
        private float scrappageRate;
        private float weightChangeRate;
        private string displayImage;
        private bool isAllergic;
        private bool isSystemIngredient;
        private int frozenLife;
        private int refrigeratedLife;
        private int shelfLife;
        private bool isFavourites;
        private byte displayOrder;
        private string generalHealthValue;
        private string ayurHealthValue;

        // Ingredient_LAN Table
        private string name;
        private string displayName;
        private string iconName;
        private string comments;

        // Ingredient Favourites
        private int favouriteID;
        private int ingredientFavID;
        private int familyID;

        // Ingredient Summary
        private float calorie;
        private float carboHydrate;
        private float fat;
        private float protien;
        private float fibre;
        private float iron;
        private float calcium;
        private float phosphorus;
        private float vitaminARetinol;
        private float vitaminABetaCarotene;
        private float thiamine;
        private float riboflavin;
        private float nicotinicAcid;
        private float pyridoxine;
        private float folicAcid;
        private float vitaminB12;
        private float vitaminC;
        private float moisture;
        private float sodium;
        private float pottasium;
        private float zinc;

        private float calories;
        private float proteinGrams;
        private float proteinCalories;
        private float proteinPercentage;
        private float carboHydrateGrams;
        private float carboHydrateCalories;
        private float carboHydratePercentage;
        private float fATGrams;
        private float fATCalories;
        private float fATPercentage;
        private float fiberGrams;
        private float fiberCalories;
        private float fiberPercentage;

        //Ingredient Nutrient List
        private List<IngredientAminoAcid> ingredientAminoAcidList;
        private List<IngredientFattyAcid> ingredientFattyAcidList;
        private List<IngredientNutrients> ingredientNutrientsList;
        private List<IngredientAyurvedic> ingredientAyurvedicList;
        private List<IngredientStandardUnit> ingredientStandardUnitList;

        private Ethnic ethnicItem;
        private NSysFoodCategory foodCategory;

        #endregion

        #region Properties - Ingredient

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int EthnicID
        {
            get { return this.ethnicID; }
            set { this.ethnicID = value; }
        }

        public byte FoodHabitID
        {
            get { return this.foodHabitID; }
            set { this.foodHabitID = value; }
        }

        public string AyurvedicFavourable
        {
            get { return this.ayurvedicFavourable; }
            set { this.ayurvedicFavourable = value; }
        }

        public string AyurvedicUnFavourable
        {
            get { return this.ayurvedicUnFavourable; }
            set { this.ayurvedicUnFavourable = value; }
        }

        public string AyurvedicBalanced
        {
            get { return this.ayurvedicBalanced; }
            set { this.ayurvedicBalanced = value; }
        }

        public string MedicalFavourable
        {
            get { return this.medicalFavourable; }
            set { this.medicalFavourable = value; }
        }

        public string MedicalUnFavourable
        {
            get { return this.medicalUnFavourable; }
            set { this.medicalUnFavourable = value; }
        }

        public string MedicalBalanced
        {
            get { return this.medicalBalanced; }
            set { this.medicalBalanced = value; }
        }

        public string Keywords
        {
            get { return this.keywords; }
            set { this.keywords = value; }
        }

        public float ScrappageRate
        {
            get { return this.scrappageRate; }
            set { this.scrappageRate = value; }
        }

        public float WeightChangeRate
        {
            get { return this.weightChangeRate; }
            set { this.weightChangeRate = value; }
        }

        public string DisplayImage
        {
            get { return this.displayImage; }
            set { this.displayImage = value; }
        }

        public bool IsAllergic
        {
            get { return this.isAllergic; }
            set { this.isAllergic = value; }
        }

        public bool IsSystemIngredient
        {
            get { return this.isSystemIngredient; }
            set { this.isSystemIngredient = value; }
        }

        public int FrozenLife
        {
            get { return this.frozenLife; }
            set { this.frozenLife = value; }
        }

        public int RefrigeratedLife
        {
            get { return this.refrigeratedLife; }
            set { this.refrigeratedLife = value; }
        }

        public int ShelfLife
        {
            get { return this.shelfLife; }
            set { this.shelfLife = value; }
        }

        public bool IsFavourites
        {
            get { return this.isFavourites; }
            set { this.isFavourites = value; }
        }

        public byte DisplayOrder
        {
            get { return this.displayOrder; }
            set { this.displayOrder = value; }
        }

        public string GeneralHealthValue
        {
            get { return this.generalHealthValue; }
            set { this.generalHealthValue = value; }
        }

        public string AyurHealthValue
        {
            get { return this.ayurHealthValue; }
            set { this.ayurHealthValue = value; }
        }

        #endregion

        #region Properties - Ingredient_LAN

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string DisplayName
        {
            get { return this.displayName; }
            set { this.displayName = value; }
        }

        public string IconName
        {
            get { return this.iconName; }
            set { this.iconName = value; }
        }

        public String Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        #endregion

        #region Properties - Ingredient Favourites

        public int FavouriteID
        {
            get { return this.favouriteID; }
            set { this.favouriteID = value; }
        }

        public int IngredientFavID
        {
            get { return this.ingredientFavID; }
            set { this.ingredientFavID = value; }
        }

        public int FamilyID
        {
            get { return this.familyID; }
            set { this.familyID = value; }
        }

        #endregion

        #region Properties - Ingredient Summary

        public float Calorie
        {
            get { return this.calorie; }
            set { this.calorie = value; }
        }

        public float CarboHydrate
        {
            get { return this.carboHydrate; }
            set { this.carboHydrate = value; }
        }

        public float Fat
        {
            get { return this.fat; }
            set { this.fat = value; }
        }

        public float Protien
        {
            get { return this.protien; }
            set { this.protien = value; }
        }

        public float Fibre
        {
            get { return this.fibre; }
            set { this.fibre = value; }
        }

        public float Calories
        {
            get { return this.calories; }
            set { this.calories = value; }
        }

        public float Iron
        {
            get { return this.iron; }
            set { this.iron = value; }
        }
        public float Calcium
        {
            get { return this.calcium; }
            set { this.calcium = value; }
        }
        public float Phosphorus
        {
            get { return this.phosphorus; }
            set { this.phosphorus = value; }
        }
        public float VitaminARetinol
        {
            get { return this.vitaminARetinol; }
            set { this.vitaminARetinol = value; }
        }
        public float VitaminABetaCarotene
        {
            get { return this.vitaminABetaCarotene; }
            set { this.vitaminABetaCarotene = value; }
        }
        public float Thiamine
        {
            get { return this.thiamine; }
            set { this.thiamine = value; }
        }
        public float Riboflavin
        {
            get { return this.riboflavin; }
            set { this.riboflavin = value; }
        }
        public float NicotinicAcid
        {
            get { return this.nicotinicAcid; }
            set { this.nicotinicAcid = value; }
        }
        public float Pyridoxine
        {
            get { return this.pyridoxine; }
            set { this.pyridoxine = value; }
        }
        public float FolicAcid
        {
            get { return this.folicAcid; }
            set { this.folicAcid = value; }
        }
        public float VitaminB12
        {
            get { return this.vitaminB12; }
            set { this.vitaminB12 = value; }
        }
        public float VitaminC
        {
            get { return this.vitaminC; }
            set { this.vitaminC = value; }
        }
        public float Moisture
        {
            get { return this.moisture; }
            set { this.moisture = value; }
        }
        public float Sodium
        {
            get { return this.sodium; }
            set { this.sodium = value; }
        }
        public float Pottasium
        {
            get { return this.pottasium; }
            set { this.pottasium = value; }
        }
        public float Zinc
        {
            get { return this.zinc; }
            set { this.zinc = value; }
        }

        public float ProteinGrams
        {
            get { return this.proteinGrams; }
            set { this.proteinGrams = value; }
        }

        public float ProteinCalories
        {
            get { return this.proteinCalories; }
            set { this.proteinCalories = value; }
        }

        public float ProteinPercentage
        {
            get { return this.proteinPercentage; }
            set { this.proteinPercentage = value; }
        }

        public float CarboHydrateGrams
        {
            get { return this.carboHydrateGrams; }
            set { this.carboHydrateGrams = value; }
        }

        public float CarboHydrateCalories
        {
            get { return this.carboHydrateCalories; }
            set { this.carboHydrateCalories = value; }
        }

        public float CarboHydratePercentage
        {
            get { return this.carboHydratePercentage; }
            set { this.carboHydratePercentage = value; }
        }

        public float FATGrams
        {
            get { return this.fATGrams; }
            set { this.fATGrams = value; }
        }

        public float FATCalories
        {
            get { return this.fATCalories; }
            set { this.fATCalories = value; }
        }

        public float FATPercentage
        {
            get { return this.fATPercentage; }
            set { this.fATPercentage = value; }
        }

        public float FiberGrams
        {
            get { return this.fiberGrams; }
            set { this.fiberGrams = value; }
        }

        public float FiberCalories
        {
            get { return this.fiberCalories; }
            set { this.fiberCalories = value; }
        }

        public float FiberPercentage
        {
            get { return this.fiberPercentage; }
            set { this.fiberPercentage = value; }
        }

        #endregion

        #region Properties - Ingredient Nutrients

        public List<IngredientAminoAcid> IngredientAminoAcidList
        {
            get
            {
                return ingredientAminoAcidList;
            }
            set
            {
                ingredientAminoAcidList = value;
            }
        }
        public List<IngredientFattyAcid> IngredientFattyAcidList
        {
            get
            {
                return ingredientFattyAcidList;
            }
            set
            {
                ingredientFattyAcidList = value;
            }
        }
        public List<IngredientNutrients> IngredientNutrientsList
        {
            get
            {
                return ingredientNutrientsList;
            }
            set
            {
                ingredientNutrientsList = value;
            }
        }
        public List<IngredientAyurvedic> IngredientAyurvedicList
        {
            get
            {
                return ingredientAyurvedicList;
            }
            set
            {
                ingredientAyurvedicList = value;
            }
        }
        public List<IngredientStandardUnit> IngredientStandardUnitList
        {
            get
            {
                return ingredientStandardUnitList;
            }
            set
            {
                ingredientStandardUnitList = value;
            }
        }

        #endregion

        #region Properties - Others

        public Ethnic EthnicItem
        {
            get { return this.ethnicItem; }
            set { this.ethnicItem = value; }
        }

        public NSysFoodCategory FoodCategory
        {
            get { return this.foodCategory; }
            set { this.foodCategory = value; }
        }

        #endregion
    }
}

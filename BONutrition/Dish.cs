using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class Dish
    {
        #region VARIABLES

        // Dish Table
        private int id;
        private int ethnicID;
        private byte foodHabitID;
        private byte dishCategoryID;
        private string ayurvedicFavourable;
        private string ayurvedicUnFavourable;
        private string medicalFavourable;
        private string medicalUnFavourable;
        private string keywords;

        private float calorie;
        private float protien;
        private float carboHydrates;
        private float fAT;
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

        private DateTime createdDate;
        private string displayImage;
        private bool isSystemDish;
        private bool isAyurvedic;
        private bool isCountable;
        private byte itemCount;
        private float serveCount;
        private float serveCount1;
        private float serveCount2;
        private string cookingTime;
        private int frozenLife;
        private int refrigeratedLife;
        private int shelfLife;
		private float standardWeight;
        private float standardWeight1;
        private float standardWeight2;
        private float planWeight;
        private float planWeight1;
        private float planWeight2;
        private float dishWeight;
        private byte serveUnit;
        private string unitName;
        private byte authorID;        
        private string name;
        private string displayName;
        private string iconName;
        private string regionalname;
        private string dishRecipe;
        private string comments;
        private string dishAyurFeatures;

        // DishFavourite Table
        private int favouriteID;
        private int dishFavoutiteID;
        private int familyID;
        private bool isFavourites;

		private List<DishIngredient> dishIngredientList;

        private Ethnic ethnicItem;
        private NSysAdmin sysAdmin;
        private NSysDishCategory sysDishCategory;

        private List<MealPlan> mealPlanList;

        #endregion

        #region PROPERTIES - Dish

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

        public byte DishCategoryID
        {
            get { return this.dishCategoryID; }
            set { this.dishCategoryID = value; }
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

        public string Keywords
        {
            get { return this.keywords; }
            set { this.keywords = value; }
        }

        public float Calorie
        {
            get { return this.calorie; }
            set { this.calorie = value; }
        }

        public float Protien
        {
            get { return this.protien; }
            set { this.protien = value; }
        }

        public float CarboHydrates
        {
            get { return this.carboHydrates; }
            set { this.carboHydrates = value; }
        }

        public float FAT
        {
            get { return this.fAT; }
            set { this.fAT = value; }
        }

        public float Fibre
        {
            get { return this.fibre; }
            set { this.fibre = value; }
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

        public DateTime CreatedDate
        {
            get { return this.createdDate; }
            set { this.createdDate = value; }
        }

        public string DisplayImage
        {
            get { return this.displayImage; }
            set { this.displayImage = value; }
        }
       
        public bool IsSystemDish
        {
            get { return this.isSystemDish; }
            set { this.isSystemDish = value; }
        }

        public bool IsAyurvedic
        {
            get { return this.isAyurvedic; }
            set { this.isAyurvedic = value; }
        }

        public bool IsCountable
        {
            get { return this.isCountable; }
            set { this.isCountable = value; }
        }

        public byte ItemCount
        {
            get { return this.itemCount; }
            set { this.itemCount = value; }
        }

        public float ServeCount
        {
            get { return this.serveCount; }
            set { this.serveCount = value; }
        }

        public float ServeCount1
        {
            get { return this.serveCount1; }
            set { this.serveCount1 = value; }
        }

        public float ServeCount2
        {
            get { return this.serveCount2; }
            set { this.serveCount2 = value; }
        }

        public string CookingTime
        {
            get { return this.cookingTime; }
            set { this.cookingTime = value; }
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

		public float StandardWeight
		{
			get { return this.standardWeight; }
			set { this.standardWeight = value; }
		}

        public float StandardWeight1
        {
            get { return this.standardWeight1; }
            set { this.standardWeight1 = value; }
        }

        public float StandardWeight2
        {
            get { return this.standardWeight2; }
            set { this.standardWeight2 = value; }
        }

        public float PlanWeight
        {
            get { return this.planWeight; }
            set { this.planWeight = value; }
        }

        public float PlanWeight1
        {
            get { return this.planWeight1; }
            set { this.planWeight1 = value; }
        }

        public float PlanWeight2
        {
            get { return this.planWeight2; }
            set { this.planWeight2 = value; }
        }

        public float DishWeight
        {
            get { return this.dishWeight; }
            set { this.dishWeight = value; }
        }

        public byte ServeUnit
        {
            get { return this.serveUnit; }
            set { this.serveUnit = value; }
        }

        public string UnitName
        {
            get { return this.unitName; }
            set { this.unitName = value; }
        }

        public byte AuthorID
        {
            get { return this.authorID; }
            set { this.authorID = value; }
        }

        #endregion

        #region PROPERTIES - Dish_LAN
       
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

        public string RegionalName
        {
            get { return this.regionalname; }
            set { this.regionalname = value; }
        }

        public string DishRecipe
        {
            get { return this.dishRecipe; }
            set { this.dishRecipe = value; }
        }

        public string Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
		
        public string DishAyurFeatures
        {
            get { return this.dishAyurFeatures; }
            set { this.dishAyurFeatures = value; }
        }
       
		public List<DishIngredient> DishIngredientList
		{
			get { return this.dishIngredientList; }
			set { this.dishIngredientList = value; }
		}

        #endregion		

		#region PROPERTIES - Dish Favourite

		public int FavouriteID
        {
            get { return this.favouriteID; }
            set { this.favouriteID = value; }
        }

        public int DishFavoutiteID
        {
            get { return this.dishFavoutiteID; }
            set { this.dishFavoutiteID = value; }
        }

        public int FamilyID
        {
            get { return this.familyID; }
            set { this.familyID = value; }
        }

        public bool IsFavourites
        {
            get
            {
                return isFavourites;
            }
            set
            {
                isFavourites = value;
            }
        }        

        #endregion
       
        #region PROPERTIES - Others

        public Ethnic EthnicItem
        {
            get { return this.ethnicItem; }
            set { this.ethnicItem = value; }
        }

        public NSysAdmin SysAdmin
        {
            get { return this.sysAdmin; }
            set { this.sysAdmin = value; }
        }

        public NSysDishCategory SysDishCategory
        {
            get { return this.sysDishCategory; }
            set { this.sysDishCategory = value; }
        }

        public List<MealPlan> MealPlanList
        {
            set
            {
                mealPlanList = value;
            }
            get
            {
                return mealPlanList;
            }
        }



        #endregion

    }
}

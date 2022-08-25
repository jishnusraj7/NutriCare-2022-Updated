using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using CodeReason.Reports;

using BONutrition;
using BLNutrition;
using NutritionV1.Enums;
using NutritionV1.Classes;
using NutritionV1.Common.Classes;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window
    {

        #region Variables

        private int reportType;
        private int dishID;
        private int ingredientID;
        private bool isRegional;

        private float plan;        

        private ItemType displayitem;
        private int itemID;

        private int familyID;
        private int memberID;
        private string memberName;

        private string menuDate;
        private DataTable breakFast;
        private DataTable lunch;
        private DataTable dinner;
        private DataTable snacks;
        private DataTable breakFastCalorie;
        private DataTable lunchCalorie;
        private DataTable dinnerCalorie;
        private DataTable snacksCalorie;
        private DataTable totalCalorie;
        
        private DataSet dsDish;
        private DataSet dsIngredient;

        #endregion

        #region Properties

        public int ReportType
        {
            get
            {
                return reportType;
            }
            set
            {
                reportType = value;
            }
        }

        public ItemType DisplayItem
        {
            set
            {
                displayitem = value;
            }
            get
            {
                return displayitem;
            }
        }

        public int ItemID
        {
            set
            {
                itemID = value;

            }
            get
            {
                return itemID;
            }

        }

        public int DishID
        {
            set
            {
                dishID = value;
            }
            get
            {
                return dishID;
            }

        }

        public int IngredientID
        {
            set
            {
                ingredientID = value;
            }
            get
            {
                return ingredientID;
            }

        }
      
        public bool IsRegional
        {
            set
            {
                isRegional = value;
            }
            get
            {
                return isRegional;
            }

        }

        public float Plan
        {
            get
            {
                return plan;
            }
            set
            {
                plan = value;
            }
        }

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

        public string MenuDate
        {
            set
            {
                menuDate = value;
            }
            get
            {
                return menuDate;
            }
        }

        public DataTable BreakFast
        {
            set
            {
                breakFast = value;
            }
            get
            {
                return breakFast;
            }
        }

        public DataTable Lunch
        {
            set
            {
                lunch = value;
            }
            get
            {
                return lunch;
            }
        }

        public DataTable Dinner
        {
            set
            {
                dinner = value;
            }
            get
            {
                return dinner;
            }
        }

        public DataTable Snacks
        {
            set
            {
                snacks = value;
            }
            get
            {
                return snacks;
            }
        }

        public DataTable BreakFastCalorie
        {
            set
            {
                breakFastCalorie = value;
            }
            get
            {
                return breakFastCalorie;
            }
        }

        public DataTable LunchCalorie
        {
            set
            {
                lunchCalorie = value;
            }
            get
            {
                return lunchCalorie;
            }
        }

        public DataTable DinnerCalorie
        {
            set
            {
                dinnerCalorie = value;
            }
            get
            {
                return dinnerCalorie;
            }
        }

        public DataTable SnacksCalorie
        {
            set
            {
                snacksCalorie = value;
            }
            get
            {
                return snacksCalorie;
            }
        }

        public DataTable TotalCalorie
        {
            set
            {
                totalCalorie = value;
            }
            get
            {
                return totalCalorie;
            }
        }

        #endregion

        #region Constructor

        public ReportViewer()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (reportType)
            {
                case (int)ReportItem.Dish:
                    GetItemHeader();
                    LoadDishList();
                    break;
                case (int)ReportItem.Ingredient:
                    GetItemHeader();
                    LoadIngredientList();
                    break;
                case (int)ReportItem.Menu:
                    LoadMenuList();
                    break;
            }
        }

        private void GetImage(string ImageSource, int ID)
        {
            string ImageDestination = string.Empty;
            if (ID > 0)
            {
                ImageDestination = Path.Combine(Environment.CurrentDirectory, Environment.CurrentDirectory + "\\Temp\\Display.jpg");
                CopyFile(ImageSource, ImageDestination);
            }
        }

        private string GetImagePath(string FolderName)
        {
            string filePath = string.Empty;
            try
            {
                filePath = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\" + FolderName;
                if (Directory.Exists(filePath))
                {
                    return filePath;
                }
                else
                {
                    Directory.CreateDirectory(filePath);
                    return filePath;
                }
            }
            catch
            {
                return filePath;
            }
        }

        private string CopyFile(string sourceFile, string destFile)
        {
            try
            {
                bool isReadOnly = ((File.GetAttributes(destFile) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
                if (isReadOnly)
                    File.SetAttributes(destFile, FileAttributes.Normal);

                File.Copy(sourceFile, destFile, true);
                //File.Move(sourceFile, destFile);                
                return destFile;
            }
            catch
            {
                return "";
            }
        }

        private void LoadDishList()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();

                StreamReader reader = new StreamReader(new FileStream(Environment.CurrentDirectory + "\\Templates\\DishList.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, Environment.CurrentDirectory + "\\Temp\\");
                reportDocument.XamlData = reader.ReadToEnd();
                reader.Close();

                DateTime dateTimeStart = DateTime.Now;

                List<ReportData> listData = new List<ReportData>();

                ReportData data = new ReportData();

                data.ReportDocumentValues.Add("PrintDate", dateTimeStart);
                data.ReportDocumentValues.Add("NutrientWeight", "Nutrient Details Per " + Plan + " gm");

                dsDish = CalorieCalculatorManager.GetDish(dishID, Plan);

                if (dsDish != null)
                {
                    string ImageSource = string.Empty;
                    if (Convert.ToString((dsDish.Tables[0].Rows[0]["DisplayImage"])) == string.Empty)
                    {
                        ImageSource = Path.Combine(Environment.CurrentDirectory, Environment.CurrentDirectory + "\\Images\\Nutrients.jpg");
                    }
                    else
                    {
                        ImageSource = GetImagePath("Dishes") + "\\" + Convert.ToInt32((dsDish.Tables[0].Rows[0]["DishID"])) + ".jpg";
                        if (!File.Exists(ImageSource))
                        {
                            ImageSource = Path.Combine(Environment.CurrentDirectory, Environment.CurrentDirectory + "\\Images\\Nutrients.jpg");
                        }
                    }

                    GetImage(ImageSource, Convert.ToInt32((dsDish.Tables[0].Rows[0]["DishID"])));

                    DataTable dtDish = new DataTable("DishDetails");
                    dtDish.Columns.Add("DishName", typeof(string));
                    for (int i = 1; i <= dsDish.Tables.Count; i++)
                    {
                        if (IsRegional == true)
                        {
                            dtDish.Rows.Add(Convert.ToString((dsDish.Tables[0].Rows[0]["DisplayName"])));
                        }
                        else
                        {
                            dtDish.Rows.Add(Convert.ToString((dsDish.Tables[0].Rows[0]["DishName"])));
                        }
                    }

                    data.DataTables.Add(dtDish);

                    DataTable dtServing = new DataTable("ServingDetails");
                    dtServing.Columns.Add("PlanNumber", typeof(string));
                    dtServing.Columns.Add("ServingSize", typeof(string));
                    dtServing.Columns.Add("StandardWeight", typeof(string));
                    for (int i = 1; i <= dsDish.Tables.Count; i++)
                    {
                        dtServing.Rows.Add("Plan1", Convert.ToString((dsDish.Tables[0].Rows[0]["ServeCount"])) + " " + GetUnitName(Classes.CommonFunctions.Convert2Int(Convert.ToString((dsDish.Tables[0].Rows[0]["ServeUnit"])))), Convert.ToString((dsDish.Tables[0].Rows[0]["StandardWeight"])) + " " + Enum.GetName(typeof(UnitType), 1));
                        dtServing.Rows.Add("Plan2", Convert.ToString((dsDish.Tables[0].Rows[0]["ServeCount1"])) + " " + GetUnitName(Classes.CommonFunctions.Convert2Int(Convert.ToString((dsDish.Tables[0].Rows[0]["ServeUnit"])))), Convert.ToString((dsDish.Tables[0].Rows[0]["StandardWeight1"])) + " " + Enum.GetName(typeof(UnitType), 1));
                        dtServing.Rows.Add("Plan3", Convert.ToString((dsDish.Tables[0].Rows[0]["ServeCount2"])) + " " + GetUnitName(Classes.CommonFunctions.Convert2Int(Convert.ToString((dsDish.Tables[0].Rows[0]["ServeUnit"])))), Convert.ToString((dsDish.Tables[0].Rows[0]["StandardWeight2"])) + " " + Enum.GetName(typeof(UnitType), 1));
                    }

                    data.DataTables.Add(dtServing);

                    dsIngredient = CalorieCalculatorManager.GetIngredient(dishID);

                    if (dsIngredient != null)
                    {
                        DataTable dtIngredient = new DataTable("IngredientDetails");
                        dtIngredient.Columns.Add("Ingredients", typeof(string));
                        dtIngredient.Columns.Add("Quantity", typeof(double));
                        dtIngredient.Columns.Add("Unit", typeof(string));
                        for (int i = 1; i <= dsIngredient.Tables.Count; i++)
                        {
                            for (int j = 1; j <= dsIngredient.Tables[0].Rows.Count; j++)
                            {
                                if (IsRegional == true)
                                {
                                    dtIngredient.Rows.Add(Convert.ToString((dsIngredient.Tables[0].Rows[j - 1]["DisplayName"])), Convert.ToString((dsIngredient.Tables[0].Rows[j - 1]["Quantity"])), Convert.ToString((dsIngredient.Tables[0].Rows[j - 1]["StandardUnitName"])));
                                }
                                else
                                {
                                    dtIngredient.Rows.Add(Convert.ToString((dsIngredient.Tables[0].Rows[j - 1]["IngredientName"])), Convert.ToString((dsIngredient.Tables[0].Rows[j - 1]["Quantity"])), Convert.ToString((dsIngredient.Tables[0].Rows[j - 1]["StandardUnitName"])));
                                }
                            }
                        }

                        data.DataTables.Add(dtIngredient);
                    }

                    DataTable dtRecipe = new DataTable("RecipeDetails");
                    dtRecipe.Columns.Add("DishRecipe", typeof(string));
                    for (int i = 1; i <= dsDish.Tables.Count; i++)
                    {
                        dtRecipe.Rows.Add(Convert.ToString((dsDish.Tables[0].Rows[0]["DishRecipe"])));
                    }
                    data.DataTables.Add(dtRecipe);

                    DataTable dtNutrient = new DataTable("NutrientDetails");
                    dtNutrient.Columns.Add("NutrientName", typeof(string));
                    dtNutrient.Columns.Add("NutrientValue", typeof(double));
                    dtNutrient.Columns.Add("NutrientUnit", typeof(string));
                    for (int i = 1; i <= dsDish.Tables.Count; i++)
                    {
                        dtNutrient.Rows.Add("Calorie", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Calorie"]))), "calorie");
                        dtNutrient.Rows.Add("Protien", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Protien"]))), Enum.GetName(typeof(UnitType), 1));
                        dtNutrient.Rows.Add("FAT", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["FAT"]))), Enum.GetName(typeof(UnitType), 1));
                        dtNutrient.Rows.Add("Fibre", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Fibre"]))), Enum.GetName(typeof(UnitType), 1));
                        dtNutrient.Rows.Add("CarboHydrates", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["CarboHydrates"]))), Enum.GetName(typeof(UnitType), 1));
                        dtNutrient.Rows.Add("Iron", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Iron"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("Calcium", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Calcium"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("Phosphorus", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Phosphorus"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("VitaminA Retinol", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["VitaminARetinol"]))), Enum.GetName(typeof(UnitType), 3));
                        dtNutrient.Rows.Add("VitaminA BetaCarotene", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["VitaminABetaCarotene"]))), Enum.GetName(typeof(UnitType), 3));
                        dtNutrient.Rows.Add("Thiamine", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Thiamine"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("Riboflavin", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Riboflavin"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("Nicotinic Acid", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["NicotinicAcid"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("Pyridoxine", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["Pyridoxine"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("FolicAcid", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["FolicAcid"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("VitaminB12", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["VitaminB12"]))), Enum.GetName(typeof(UnitType), 2));
                        dtNutrient.Rows.Add("VitaminC", Classes.CommonFunctions.Convert2Double(Convert.ToString((dsDish.Tables[0].Rows[0]["VitaminC"]))), Enum.GetName(typeof(UnitType), 2));
                    }

                    data.DataTables.Add(dtNutrient);
                }

                listData.Add(data);

                XpsDocument xps = reportDocument.CreateXpsDocument(listData);
                dvNutrition.Document = xps.GetFixedDocumentSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.GetType() + System.Environment.NewLine + ex.StackTrace, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {

            }
        }

        private void LoadIngredientList()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();

                StreamReader reader = new StreamReader(new FileStream(Environment.CurrentDirectory + "\\Templates\\IngredientList.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, Environment.CurrentDirectory + "\\Temp\\");
                reportDocument.XamlData = reader.ReadToEnd();
                reader.Close();

                DateTime dateTimeStart = DateTime.Now;

                Ingredient ingredientItem = new Ingredient();
                IngredientNutrients ingredientNutrientsItem = new IngredientNutrients();
                List<ReportData> listData = new List<ReportData>();

                ReportData data = new ReportData();

                data.ReportDocumentValues.Add("PrintDate", dateTimeStart);

                ingredientItem = IngredientManager.GetItem(IngredientID);

                if (ingredientItem != null)
                {
                    string ImageSource = string.Empty;
                    if (ingredientItem.DisplayImage == string.Empty)
                    {
                        ImageSource = Path.Combine(Environment.CurrentDirectory, Environment.CurrentDirectory + "\\Images\\Nutrients.jpg");
                    }
                    else
                    {
                        ImageSource = GetImagePath("Ingredients") + "\\" + IngredientID + ".jpg";
                        if (!File.Exists(ImageSource))
                        {
                            ImageSource = Path.Combine(Environment.CurrentDirectory, Environment.CurrentDirectory + "\\Images\\Nutrients.jpg");
                        }
                    }
                    GetImage(ImageSource, IngredientID);

                    DataTable dtIngredient1 = new DataTable("IngredientDetails1");
                    dtIngredient1.Columns.Add("IngredientName", typeof(string));

                    if (IsRegional == true)
                    {
                        dtIngredient1.Rows.Add(Convert.ToString(ingredientItem.DisplayName));
                    }
                    else
                    {
                        dtIngredient1.Rows.Add(Convert.ToString(ingredientItem.Name));
                    }

                    data.DataTables.Add(dtIngredient1);

                    DataTable dtIngredient2 = new DataTable("IngredientDetails2");
                    dtIngredient2.Columns.Add("Item1", typeof(string));
                    dtIngredient2.Columns.Add("Item2", typeof(string));

                    NSysFoodCategory foodCategoryItem = new NSysFoodCategory();
                    foodCategoryItem = NSysFoodCategoryManager.GetItem(ingredientItem.FoodHabitID);
                    if (foodCategoryItem != null)
                    {
                        dtIngredient2.Rows.Add("Food Type", Convert.ToString(foodCategoryItem.FoodCategoryName));
                    }
                    dtIngredient2.Rows.Add("Scrap Rate", Convert.ToString(ingredientItem.ScrappageRate) + "%");
                    dtIngredient2.Rows.Add("Weight Change Rate", Convert.ToString(ingredientItem.WeightChangeRate + "%"));

                    data.DataTables.Add(dtIngredient2);

                    DataTable dtIngredient3 = new DataTable("IngredientDetails3");
                    dtIngredient3.Columns.Add("Description", typeof(string));

                    dtIngredient3.Rows.Add(Convert.ToString(ingredientItem.Comments));

                    data.DataTables.Add(dtIngredient3);

                    DataTable dtNutrient = new DataTable("NutrientDetails");
                    dtNutrient.Columns.Add("NutrientName", typeof(string));
                    dtNutrient.Columns.Add("NutrientValue", typeof(double));
                    dtNutrient.Columns.Add("NutrientUnit", typeof(string));

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Calorie);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Calorie", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), "calorie");
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Calorie", 0, "calorie");
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Protien);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Protien", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 1));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Protien", 0, Enum.GetName(typeof(UnitType), 1));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.FAT);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("FAT", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 1));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("FAT", 0, Enum.GetName(typeof(UnitType), 1));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Fibre);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Fibre", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 1));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Fibre", 0, Enum.GetName(typeof(UnitType), 1));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.CarboHydrates);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("CarboHydrates", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 1));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("CarboHydrates", 0, Enum.GetName(typeof(UnitType), 1));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Iron);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Iron", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Iron", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Calcium);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Calcium", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Calcium", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Phosphorus);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Phosphorus", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Phosphorus", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.VitaminA_Retinol);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("VitaminARetinol", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 3));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("VitaminARetinol", 0, Enum.GetName(typeof(UnitType), 3));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.VitaminA_BetaCarotene);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("VitaminABetaCarotene", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 3));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("VitaminABetaCarotene", 0, Enum.GetName(typeof(UnitType), 3));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Thiamine);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Thiamine", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Thiamine", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Riboflavin);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Riboflavin", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Riboflavin", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.NicotinicAcid);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("NicotinicAcid", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("NicotinicAcid", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.Pyridoxine);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("Pyridoxine", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("Pyridoxine", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.FolicAcid);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("FolicAcid", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("FolicAcid", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.VitaminB12);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("VitaminB12", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("VitaminB12", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    ingredientNutrientsItem = new IngredientNutrients();
                    ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(IngredientID, (int)MainNutrients.VitaminC);
                    if (ingredientNutrientsItem != null)
                    {
                        dtNutrient.Rows.Add("VitaminC", Classes.CommonFunctions.Convert2Double(Convert.ToString(ingredientNutrientsItem.NutrientValue * 100)), Enum.GetName(typeof(UnitType), 2));
                    }
                    else
                    {
                        dtNutrient.Rows.Add("VitaminC", 0, Enum.GetName(typeof(UnitType), 2));
                    }

                    data.DataTables.Add(dtNutrient);
                }

                listData.Add(data);

                XpsDocument xps = reportDocument.CreateXpsDocument(listData);
                dvNutrition.Document = xps.GetFixedDocumentSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.GetType() + System.Environment.NewLine + ex.StackTrace, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {

            }
        }

        private void LoadMenuList()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();

                StreamReader reader = new StreamReader(new FileStream(Environment.CurrentDirectory + "\\Templates\\MenuList.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlData = reader.ReadToEnd();
                reader.Close();

                DateTime dateTimeStart = DateTime.Now;

                List<Member> memberList = new List<Member>();

                List<ReportData> listData = new List<ReportData>();

                ReportData data = new ReportData();

                data.ReportDocumentValues.Add("PrintDate", dateTimeStart);

                DataTable dtMenuDateDetails = new DataTable("MenuDateDetails");
                dtMenuDateDetails.Columns.Add("MenuDate", typeof(string));
                dtMenuDateDetails.Columns.Add("MemberName", typeof(string));
                dtMenuDateDetails.Rows.Add(Convert.ToString(MenuDate) + " for " + Convert.ToString(MemberName));
                data.DataTables.Add(dtMenuDateDetails);                

                if (BreakFast != null)
                {
                    DataTable dtBreakFastDetails = new DataTable("BreakFastDetails");
                    dtBreakFastDetails.Columns.Add("DishName", typeof(string));
                    dtBreakFastDetails.Columns.Add("PlanWeight", typeof(string));
                    dtBreakFastDetails.Columns.Add("Quantity", typeof(double));
                    for (int i = 0; i < BreakFast.Rows.Count; i++)
                    {
                        if (IsRegional == true)
                        {
                            dtBreakFastDetails.Rows.Add(BreakFast.Rows[i]["DisplayName"], Convert.ToString((BreakFast.Rows[i]["PlanWeight"])) + " " + Enum.GetName(typeof(UnitType), 1), BreakFast.Rows[i]["DishCount"]);
                        }
                        else
                        {
                            dtBreakFastDetails.Rows.Add(BreakFast.Rows[i]["DishName"], Convert.ToString((BreakFast.Rows[i]["PlanWeight"])) + " " + Enum.GetName(typeof(UnitType), 1), BreakFast.Rows[i]["DishCount"]);
                        }
                    }

                    data.DataTables.Add(dtBreakFastDetails);
                }

                if (BreakFastCalorie != null)
                {
                    DataTable dtBreakFastCalorie = new DataTable("BreakFastCalorie");
                    dtBreakFastCalorie.Columns.Add("Quantity", typeof(double));
                    
                    for (int i = 0; i < BreakFastCalorie.Rows.Count; i++)
                    {
                        dtBreakFastCalorie.Rows.Add(BreakFastCalorie.Rows[i]["DishCount"]);
                    }

                    data.DataTables.Add(dtBreakFastCalorie);
                }

                if (Lunch != null)
                {
                    DataTable dtLunchDetails = new DataTable("LunchDetails");
                    dtLunchDetails.Columns.Add("DishName", typeof(string));
                    dtLunchDetails.Columns.Add("PlanWeight", typeof(string));
                    dtLunchDetails.Columns.Add("Quantity", typeof(double));
                    
                    for (int i = 0; i < Lunch.Rows.Count; i++)
                    {
                        if (IsRegional == true)
                        {
                            dtLunchDetails.Rows.Add(Lunch.Rows[i]["DisplayName"], Convert.ToString((Lunch.Rows[i]["PlanWeight"])) + " " + Enum.GetName(typeof(UnitType), 1), Lunch.Rows[i]["DishCount"]);
                        }
                        else
                        {
                            dtLunchDetails.Rows.Add(Lunch.Rows[i]["DishName"], Convert.ToString((Lunch.Rows[i]["PlanWeight"])) + " " + Enum.GetName(typeof(UnitType), 1), Lunch.Rows[i]["DishCount"]);
                        }
                    }

                    data.DataTables.Add(dtLunchDetails);
                }

                if (LunchCalorie != null)
                {
                    DataTable dtLunchCalorie = new DataTable("LunchCalorie");
                    dtLunchCalorie.Columns.Add("Quantity", typeof(double));
                    for (int i = 0; i < LunchCalorie.Rows.Count; i++)
                    {
                        dtLunchCalorie.Rows.Add(LunchCalorie.Rows[i]["DishCount"]);
                    }

                    data.DataTables.Add(dtLunchCalorie);
                }

                if (Dinner != null)
                {
                    DataTable dtDinnerDetails = new DataTable("DinnerDetails");
                    dtDinnerDetails.Columns.Add("DishName", typeof(string));
                    dtDinnerDetails.Columns.Add("PlanWeight", typeof(string));
                    dtDinnerDetails.Columns.Add("Quantity", typeof(double));                    
                    for (int i = 0; i < Dinner.Rows.Count; i++)
                    {
                        if (IsRegional == true)
                        {
                            dtDinnerDetails.Rows.Add(Dinner.Rows[i]["DisplayName"], Convert.ToString((Dinner.Rows[i]["PlanWeight"])) + " " + Enum.GetName(typeof(UnitType), 1), Dinner.Rows[i]["DishCount"]);
                        }
                        else
                        {
                            dtDinnerDetails.Rows.Add(Dinner.Rows[i]["DishName"], Convert.ToString((Dinner.Rows[i]["PlanWeight"])) + " " + Enum.GetName(typeof(UnitType), 1), Dinner.Rows[i]["DishCount"]);
                        }
                    }

                    data.DataTables.Add(dtDinnerDetails);
                }

                if (DinnerCalorie != null)
                {
                    DataTable dtDinnerCalorie = new DataTable("DinnerCalorie");
                    dtDinnerCalorie.Columns.Add("Quantity", typeof(double));
                    
                    for (int i = 0; i < DinnerCalorie.Rows.Count; i++)
                    {
                        dtDinnerCalorie.Rows.Add(DinnerCalorie.Rows[i]["DishCount"]);
                    }

                    data.DataTables.Add(dtDinnerCalorie);
                }

                if (Snacks != null)
                {
                    DataTable dtSnacksDetails = new DataTable("SnacksDetails");
                    dtSnacksDetails.Columns.Add("DishName", typeof(string));
                    dtSnacksDetails.Columns.Add("PlanWeight", typeof(string));
                    dtSnacksDetails.Columns.Add("Quantity", typeof(double));                    
                    for (int i = 0; i < Snacks.Rows.Count; i++)
                    {
                        if (IsRegional == true)
                        {
                            dtSnacksDetails.Rows.Add(Snacks.Rows[i]["DisplayName"], Convert.ToString((Snacks.Rows[i]["PlanWeight"])) + " " + Enum.GetName(typeof(UnitType), 1), Snacks.Rows[i]["DishCount"]);
                        }
                        else
                        {
                            dtSnacksDetails.Rows.Add(Snacks.Rows[i]["DishName"], Convert.ToString((Snacks.Rows[i]["PlanWeight"])) + " " + Enum.GetName(typeof(UnitType), 1), Snacks.Rows[i]["DishCount"]);
                        }
                    }

                    data.DataTables.Add(dtSnacksDetails);
                }

                if (SnacksCalorie != null)
                {
                    DataTable dtSnacksCalorie = new DataTable("SnacksCalorie");
                    dtSnacksCalorie.Columns.Add("Quantity", typeof(double));                   
                    for (int i = 0; i < SnacksCalorie.Rows.Count; i++)
                    {
                        dtSnacksCalorie.Rows.Add(SnacksCalorie.Rows[i]["DishCount"]);
                    }

                    data.DataTables.Add(dtSnacksCalorie);
                }

                if (TotalCalorie != null)
                {
                    DataTable dtTotalCalorie = new DataTable("TotalCalorie");
                    dtTotalCalorie.Columns.Add("Quantity", typeof(double));                   

                    for (int i = 0; i < TotalCalorie.Rows.Count; i++)
                    {
                        dtTotalCalorie.Rows.Add(TotalCalorie.Rows[i]["DishCount"]);
                    }

                    data.DataTables.Add(dtTotalCalorie);
                }

                listData.Add(data);

                XpsDocument xps = reportDocument.CreateXpsDocument(listData);
                dvNutrition.Document = xps.GetFixedDocumentSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.GetType() + System.Environment.NewLine + ex.StackTrace, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {

            }
        }

        private void GetItemHeader()
        {
            string Header = string.Empty;

            if (DisplayItem == ItemType.Dish)
            {
                Dish Dish = new Dish();
                Dish = DishManager.GetItem(ItemID, " 1=1 ");
                if (Dish != null)
                {
                    if (IsRegional == true)
                    {
                        Header = Dish.DisplayName;
                    }
                    else
                    {
                        Header = Dish.Name;
                    }
                }
            }
            else
            {
                Ingredient Ingredient = new Ingredient();
                Ingredient = IngredientManager.GetItem(ItemID);
                if (Ingredient != null)
                {
                    if (IsRegional == true)
                    {
                        Header = Ingredient.DisplayName;
                    }
                    else
                    {
                        Header = Ingredient.Name;
                    }
                }
            }

            this.Title = "    " + Header;
        }

        private string GetUnitName(int unitID)
        {
            string strunit = string.Empty;
            DataTable dtableUnit = new DataTable();

            dtableUnit = XMLServices.GetXMLData(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\ServeUnit.xml", 3);

            if (dtableUnit != null)
            {
                if (unitID > 0)
                {
                    DataRow[] rowcol = dtableUnit.Select("ID=" + unitID);
                    strunit = Convert.ToString(rowcol[0].ItemArray[1]);
                }
                else
                {
                    strunit = string.Empty;
                }
            }
            return strunit;
        }

        #endregion
        
    }
}

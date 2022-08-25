using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Collections;
using BONutrition;
using BLNutrition;
using NutritionV1.Common.Classes;
using NutritionV1.Classes;
using NutritionV1.Enums;
using Visifire.Charts;
using Visifire.Commons;
using Microsoft.Win32;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for DishCompare.xaml
    /// </summary>
    public partial class DishCompare : Page
    {

        #region Variables

        List<Dish> dishList = new List<Dish>();

        string DishName1 = string.Empty;
        string DishName2 = string.Empty;
        string DishName3 = string.Empty;
        string DishName4 = string.Empty;
        string DishName5 = string.Empty;

        string DisplayName1 = string.Empty;
        string DisplayName2 = string.Empty;
        string DisplayName3 = string.Empty;
        string DisplayName4 = string.Empty;
        string DisplayName5 = string.Empty;

        ArrayList clickIndex = new ArrayList();
        string imagePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Images\\";

        Visifire.Charts.Chart chart;
        SaveFileDialog SaveDlg = new SaveFileDialog();

        double[] Calorie = new double[5];
        double[] Carbo = new double[5];
        double[] Protein = new double[5];
        double[] FAT = new double[5];
        double[] Fibre = new double[5];
        double[] Moisture = new double[5];

        double[] Iron = new double[5];
        double[] Calcium = new double[5];
        double[] Phosphorus = new double[5];
        double[] Sodium = new double[5];
        double[] Pottasuim = new double[5];
        double[] Zinc = new double[5];

        double[] Retinol = new double[5];
        double[] BetaCarotine = new double[5];
        double[] Thiamine = new double[5];
        double[] Ribiflavin = new double[5];
        double[] Naicin = new double[5];
        double[] Pyridoxine = new double[5];
        double[] FolicAcid = new double[5];
        double[] VitamineC = new double[5];

        string[] DishName = new string[5];
        int Result;
        int[] Results = new int[3];
        int GraphIndex;
        int GraphType;

        private ListViewItem SelectedItem;

        #endregion

        #region Constructor

        public DishCompare()
        {
            InitializeComponent();

            InitailizeGraph();
        }

        #endregion

        #region Methods

        private void WriteRegistry(int themeIndex)
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini.IniWriteValue("SWTHEME", "THEME", themeIndex.ToString());
            ini = null;
        }

        private int ReadRegistry()
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            Result = CommonFunctions.Convert2Int(ini.IniReadValue("SWTHEME", "THEME"));
            ini = null;
            return Result;
        }

        private void WriteGraphRegistry(int themeIndex)
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini.IniWriteValue("SWGRAPH", "GRAPH", themeIndex.ToString());
            ini = null;
        }

        private int ReadGraphRegistry()
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            Result = CommonFunctions.Convert2Int(ini.IniReadValue("SWGRAPH", "GRAPH"));
            ini = null;
            return Result;
        }

        private void WriteCompareRegistry(int Result)
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini.IniWriteValue("SWCOMPARECATEGORY", "COMPARECATEGORY", Result.ToString());
            ini = null;
        }

        private int ReadCompareRegistry()
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            Result = CommonFunctions.Convert2Int(ini.IniReadValue("SWCOMPARECATEGORY", "COMPARECATEGORY"));
            return Result;
        }

        private void RepopulateData()
        {
            int Result = ReadCompareRegistry();
            if (Result != 0)
            {
                cboDishCategory.SelectedIndex = Result;
                FillSearchList();
            }
        }

        private void GridAnimation(string animationname)
        {

        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;

            ImgDish1.SetThemes = true;
            ImgDish2.SetThemes = true;
            ImgDish3.SetThemes = true;
            ImgDish4.SetThemes = true;
            ImgDish5.SetThemes = true;

            ColumnGraph.Style = (Style)apps.SetStyle["HomeBarStyle"];
            BarGraph.Style = (Style)apps.SetStyle["HomeBarStyle"];
            ((NutritionV1.MasterPage)(Window.GetWindow(this))).mnuTop.Visibility = Visibility.Visible;
        }

        private void SetThemeOnClick(System.Windows.Shapes.Rectangle SelRectangle)
        {
            App apps = (App)Application.Current;

            ColumnGraph.Style = (Style)apps.SetStyle["HomeBarStyle"];
            BarGraph.Style = (Style)apps.SetStyle["HomeBarStyle"];

            SelRectangle.Style = (Style)apps.SetStyle["HomeBarSelectStyle"];
        }

        private void ShowMessages(string message)
        {
            AlertBox.Show(message);
        }

        private void ChangeView(int viewIndex)
        {
            switch (viewIndex)
            {
                case (int)NutrientGraphType.BarGraph:
                    CreateGraph((int)NutrientGraphType.BarGraph, GraphIndex);
                    SetThemeOnClick(BarGraph);
                    break;
                case (int)NutrientGraphType.ColumnGraph:
                    CreateGraph((int)NutrientGraphType.ColumnGraph, GraphIndex);
                    SetThemeOnClick(ColumnGraph);
                    break;
                default:
                    CreateGraph((int)NutrientGraphType.BarGraph, GraphIndex);
                    SetThemeOnClick(BarGraph);
                    break;
            }
        }

        private void LoadImages()
        {
            if (File.Exists(imagePath + "Nutrients.png"))
            {
                imgBar.Source = new BitmapImage(new Uri(imagePath + "BarGraph.png"));
            }
            else
            {
                imgBar.Source = new BitmapImage(new Uri(imagePath + "Vista.png"));
            }

            if (File.Exists(imagePath + "Vista.png"))
            {
                imgColumn.Source = new BitmapImage(new Uri(imagePath + "ColumnGraph.png"));
            }
            else
            {
                imgColumn.Source = new BitmapImage(new Uri(imagePath + "Vista.png"));
            }
        }

        private void ShowMessage()
        {
            AlertBox.Show("Select DishCategory or Type Search Criteria", "", AlertType.Information, AlertButtons.OK);
        }

        private void FillSearchList()
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                List<Dish> dishSearchList = new List<Dish>();
                string searchString = string.Empty;

                searchString = " 1=1 ";

                if (txtSearch.Text.Trim() != string.Empty)
                {
                    searchString = searchString + " AND (DishName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%' ";
                    searchString = searchString + " OR DisplayName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%') ";
                }

                if (cboDishCategory.SelectedIndex > 0)
                {
                    searchString = searchString + " And DishCategoryID=" + cboDishCategory.SelectedIndex + " ";
                }

                searchString = "Where " + searchString;
                dishSearchList = DishManager.GetList(searchString);

                foreach (Dish dish in dishSearchList)
                {
                    if (dish.DisplayImage != string.Empty)
                        dish.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Dishes" + "\\" + dish.Id + ".jpg";
                    else
                        dish.DisplayImage = null;

                }

                if (dishSearchList != null)
                {
                    SearchList.ItemsSource = null;
                    SearchList.Items.Refresh();

                    SearchList.ItemsSource = dishSearchList;
                    SearchList.SelectedIndex = 0;
                    SearchList.ScrollIntoView(SearchList.SelectedItem);
                }

            }
        }

        private void FillGraph(Dish dish, int dishNo)
        {
            if (dishNo == 1)
            {
                if (dish != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(dish.Id);

                    if (ingredientList != null && ingredientList.Count > 0)
                    {
                        Calorie[0] = ingredientList[0].NutrientValue;
                        Carbo[0] = ingredientList[2].NutrientValue;
                        Protein[0] = ingredientList[1].NutrientValue;
                        FAT[0] = ingredientList[3].NutrientValue;
                        Fibre[0] = ingredientList[4].NutrientValue;
                        Iron[0] = ingredientList[9].NutrientValue;
                        Calcium[0] = ingredientList[7].NutrientValue;
                        Phosphorus[0] = ingredientList[8].NutrientValue;
                        Moisture[0] = ingredientList[18].NutrientValue;
                        Retinol[0] = ingredientList[11].NutrientValue;
                        BetaCarotine[0] = ingredientList[12].NutrientValue;
                        Thiamine[0] = ingredientList[17].NutrientValue;
                        Ribiflavin[0] = ingredientList[13].NutrientValue;
                        Naicin[0] = ingredientList[19].NutrientValue;
                        Pyridoxine[0] = ingredientList[20].NutrientValue;
                        FolicAcid[0] = ingredientList[15].NutrientValue;
                        VitamineC[0] = ingredientList[16].NutrientValue;
                        Sodium[0] = ingredientList[5].NutrientValue;
                        Pottasuim[0] = ingredientList[6].NutrientValue;
                        Zinc[0] = ingredientList[10].NutrientValue;

                        DishName[0] = Classes.CommonFunctions.Convert2String(Convert.ToString(dish.Name));

                        GraphType = ReadGraphRegistry();
                        CreateGraph(GraphType, GraphIndex);
                    }

                }
                else
                {
                    Calorie[0] = 0;
                    Carbo[0] = 0;
                    Protein[0] = 0;
                    FAT[0] = 0;
                    Fibre[0] = 0;
                    Iron[0] = 0;
                    Calcium[0] = 0;
                    Phosphorus[0] = 0;
                    Moisture[0] = 0;
                    Retinol[0] = 0;
                    BetaCarotine[0] = 0;
                    Thiamine[0] = 0;
                    Ribiflavin[0] = 0;
                    Naicin[0] = 0;
                    Pyridoxine[0] = 0;
                    FolicAcid[0] = 0;
                    VitamineC[0] = 0;
                    Sodium[0] = 0;
                    Pottasuim[0] = 0;
                    Zinc[0] = 0;
                    DishName[0] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }
            }
            if (dishNo == 2)
            {
                if (dish != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(dish.Id);

                    if (ingredientList != null && ingredientList.Count > 0)
                    {
                        Calorie[1] = ingredientList[0].NutrientValue;
                        Carbo[1] = ingredientList[2].NutrientValue;
                        Protein[1] = ingredientList[1].NutrientValue;
                        FAT[1] = ingredientList[3].NutrientValue;
                        Fibre[1] = ingredientList[4].NutrientValue;
                        Iron[1] = ingredientList[9].NutrientValue;
                        Calcium[1] = ingredientList[7].NutrientValue;
                        Phosphorus[1] = ingredientList[8].NutrientValue;
                        Moisture[1] = ingredientList[18].NutrientValue;
                        Retinol[1] = ingredientList[11].NutrientValue;
                        BetaCarotine[1] = ingredientList[12].NutrientValue;
                        Thiamine[1] = ingredientList[17].NutrientValue;
                        Ribiflavin[1] = ingredientList[13].NutrientValue;
                        Naicin[1] = ingredientList[19].NutrientValue;
                        Pyridoxine[1] = ingredientList[20].NutrientValue;
                        FolicAcid[1] = ingredientList[15].NutrientValue;
                        VitamineC[1] = ingredientList[16].NutrientValue;
                        Sodium[1] = ingredientList[5].NutrientValue;
                        Pottasuim[1] = ingredientList[6].NutrientValue;
                        Zinc[1] = ingredientList[10].NutrientValue;

                        DishName[1] = Classes.CommonFunctions.Convert2String(Convert.ToString(dish.Name));

                        GraphType = ReadGraphRegistry();
                        CreateGraph(GraphType, GraphIndex);
                    }

                }
                else
                {
                    Calorie[1] = 0;
                    Carbo[1] = 0;
                    Protein[1] = 0;
                    FAT[1] = 0;
                    Fibre[1] = 0;
                    Iron[1] = 0;
                    Calcium[1] = 0;
                    Phosphorus[1] = 0;
                    Moisture[1] = 0;
                    Retinol[1] = 0;
                    BetaCarotine[1] = 0;
                    Thiamine[1] = 0;
                    Ribiflavin[1] = 0;
                    Naicin[1] = 0;
                    Pyridoxine[1] = 0;
                    FolicAcid[1] = 0;
                    VitamineC[1] = 0;
                    Sodium[1] = 0;
                    Pottasuim[1] = 0;
                    Zinc[1] = 0;
                    DishName[1] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }

            }
            if (dishNo == 3)
            {
                if (dish != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(dish.Id);

                    if (ingredientList != null && ingredientList.Count > 0)
                    {
                        Calorie[2] = ingredientList[0].NutrientValue;
                        Carbo[2] = ingredientList[2].NutrientValue;
                        Protein[2] = ingredientList[1].NutrientValue;
                        FAT[2] = ingredientList[3].NutrientValue;
                        Fibre[2] = ingredientList[4].NutrientValue;
                        Iron[2] = ingredientList[9].NutrientValue;
                        Calcium[2] = ingredientList[7].NutrientValue;
                        Phosphorus[2] = ingredientList[8].NutrientValue;
                        Moisture[2] = ingredientList[18].NutrientValue;
                        Retinol[2] = ingredientList[11].NutrientValue;
                        BetaCarotine[2] = ingredientList[12].NutrientValue;
                        Thiamine[2] = ingredientList[17].NutrientValue;
                        Ribiflavin[2] = ingredientList[13].NutrientValue;
                        Naicin[2] = ingredientList[19].NutrientValue;
                        Pyridoxine[2] = ingredientList[20].NutrientValue;
                        FolicAcid[2] = ingredientList[15].NutrientValue;
                        VitamineC[2] = ingredientList[16].NutrientValue;
                        Sodium[2] = ingredientList[5].NutrientValue;
                        Pottasuim[2] = ingredientList[6].NutrientValue;
                        Zinc[2] = ingredientList[10].NutrientValue;

                        DishName[2] = Classes.CommonFunctions.Convert2String(Convert.ToString(dish.Name));

                        GraphType = ReadGraphRegistry();
                        CreateGraph(GraphType, GraphIndex);
                    }

                }
                else
                {
                    Calorie[2] = 0;
                    Carbo[2] = 0;
                    Protein[2] = 0;
                    FAT[2] = 0;
                    Fibre[2] = 0;
                    Iron[2] = 0;
                    Calcium[2] = 0;
                    Phosphorus[2] = 0;
                    Moisture[2] = 0;
                    Retinol[2] = 0;
                    BetaCarotine[2] = 0;
                    Thiamine[2] = 0;
                    Ribiflavin[2] = 0;
                    Naicin[2] = 0;
                    Pyridoxine[2] = 0;
                    FolicAcid[2] = 0;
                    VitamineC[2] = 0;
                    Sodium[2] = 0;
                    Pottasuim[2] = 0;
                    Zinc[2] = 0;
                    DishName[2] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }

            }
            if (dishNo == 4)
            {
                if (dish != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(dish.Id);

                    if (ingredientList != null && ingredientList.Count > 0)
                    {
                        Calorie[3] = ingredientList[0].NutrientValue;
                        Carbo[3] = ingredientList[2].NutrientValue;
                        Protein[3] = ingredientList[1].NutrientValue;
                        FAT[3] = ingredientList[3].NutrientValue;
                        Fibre[3] = ingredientList[4].NutrientValue;
                        Iron[3] = ingredientList[9].NutrientValue;
                        Calcium[3] = ingredientList[7].NutrientValue;
                        Phosphorus[3] = ingredientList[8].NutrientValue;
                        Moisture[3] = ingredientList[18].NutrientValue;
                        Retinol[3] = ingredientList[11].NutrientValue;
                        BetaCarotine[3] = ingredientList[12].NutrientValue;
                        Thiamine[3] = ingredientList[17].NutrientValue;
                        Ribiflavin[3] = ingredientList[13].NutrientValue;
                        Naicin[3] = ingredientList[19].NutrientValue;
                        Pyridoxine[3] = ingredientList[20].NutrientValue;
                        FolicAcid[3] = ingredientList[15].NutrientValue;
                        VitamineC[3] = ingredientList[16].NutrientValue;
                        Sodium[3] = ingredientList[5].NutrientValue;
                        Pottasuim[3] = ingredientList[6].NutrientValue;
                        Zinc[3] = ingredientList[10].NutrientValue;

                        DishName[3] = Classes.CommonFunctions.Convert2String(Convert.ToString(dish.Name));

                        GraphType = ReadGraphRegistry();
                        CreateGraph(GraphType, GraphIndex);
                    }

                }
                else
                {
                    Calorie[3] = 0;
                    Carbo[3] = 0;
                    Protein[3] = 0;
                    FAT[3] = 0;
                    Fibre[3] = 0;
                    Iron[3] = 0;
                    Calcium[3] = 0;
                    Phosphorus[3] = 0;
                    Moisture[3] = 0;
                    Retinol[3] = 0;
                    BetaCarotine[3] = 0;
                    Thiamine[3] = 0;
                    Ribiflavin[3] = 0;
                    Naicin[3] = 0;
                    Pyridoxine[3] = 0;
                    FolicAcid[3] = 0;
                    VitamineC[3] = 0;
                    Sodium[3] = 0;
                    Pottasuim[3] = 0;
                    Zinc[3] = 0;
                    DishName[3] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }

            }
            if (dishNo == 5)
            {
                if (dish != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(dish.Id);

                    if (ingredientList != null && ingredientList.Count > 0)
                    {
                        Calorie[4] = ingredientList[0].NutrientValue;
                        Carbo[4] = ingredientList[2].NutrientValue;
                        Protein[4] = ingredientList[1].NutrientValue;
                        FAT[4] = ingredientList[3].NutrientValue;
                        Fibre[4] = ingredientList[4].NutrientValue;
                        Iron[4] = ingredientList[9].NutrientValue;
                        Calcium[4] = ingredientList[7].NutrientValue;
                        Phosphorus[4] = ingredientList[8].NutrientValue;
                        Moisture[4] = ingredientList[18].NutrientValue;
                        Retinol[4] = ingredientList[11].NutrientValue;
                        BetaCarotine[4] = ingredientList[12].NutrientValue;
                        Thiamine[4] = ingredientList[17].NutrientValue;
                        Ribiflavin[4] = ingredientList[13].NutrientValue;
                        Naicin[4] = ingredientList[19].NutrientValue;
                        Pyridoxine[4] = ingredientList[20].NutrientValue;
                        FolicAcid[4] = ingredientList[15].NutrientValue;
                        VitamineC[4] = ingredientList[16].NutrientValue;
                        Sodium[4] = ingredientList[5].NutrientValue;
                        Pottasuim[4] = ingredientList[6].NutrientValue;
                        Zinc[4] = ingredientList[10].NutrientValue;

                        DishName[4] = Classes.CommonFunctions.Convert2String(Convert.ToString(dish.Name));

                        GraphType = ReadGraphRegistry();
                        CreateGraph(GraphType, GraphIndex);
                    }

                }
                else
                {
                    Calorie[4] = 0;
                    Carbo[4] = 0;
                    Protein[4] = 0;
                    FAT[4] = 0;
                    Fibre[4] = 0;
                    Iron[4] = 0;
                    Calcium[4] = 0;
                    Phosphorus[4] = 0;
                    Moisture[4] = 0;
                    Retinol[4] = 0;
                    BetaCarotine[4] = 0;
                    Thiamine[4] = 0;
                    Ribiflavin[4] = 0;
                    Naicin[4] = 0;
                    Pyridoxine[4] = 0;
                    FolicAcid[4] = 0;
                    VitamineC[4] = 0;
                    Sodium[4] = 0;
                    Pottasuim[4] = 0;
                    Zinc[4] = 0;
                    DishName[4] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }

            }
        }

        private void InitailizeGraph()
        {
            for (int i = 0; i < 5; i++)
            {
                Calorie[i] = 0;
                Carbo[i] = 0;
                Protein[i] = 0;
                FAT[i] = 0;
                Fibre[i] = 0;
                Iron[i] = 0;
                Calcium[i] = 0;
                Phosphorus[i] = 0;
                Moisture[i] = 0;
                Retinol[i] = 0;
                BetaCarotine[i] = 0;
                Thiamine[i] = 0;
                Ribiflavin[i] = 0;
                Naicin[i] = 0;
                Pyridoxine[i] = 0;
                FolicAcid[i] = 0;
                VitamineC[i] = 0;
                Sodium[i] = 0;
                Pottasuim[i] = 0;
                Zinc[i] = 0;
                DishName[i] = string.Empty;
            }

            CreateGraph((int)NutrientGraphType.BarGraph, (int)NutritionType.Calorie);
        }

        private void FillDish(int ingrId, string ingrName, Label calories, Label proteins, Label carbohydrates, Label fats, Label fibers, Label irons, Label calciums,
                                                Label phosphorus, Label vitaminARetinol, Label vitaminABetaCarotene, Label thiamine, Label riboflavin, Label nicotinicAcid, Label pyridoxine,
                                                Label folicAcid, Label vitaminB12, Label vitaminC, Label moisture, Label sodium, Label pottasium, Label zinc)
        {
            List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
            ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(ingrId);

            if (ingredientList != null && ingredientList.Count > 0)
            {
                calories.Content = Convert.ToString(ingredientList[0].NutrientValue);
                carbohydrates.Content = Convert.ToString(ingredientList[2].NutrientValue);
                proteins.Content = Convert.ToString(ingredientList[1].NutrientValue);
                fats.Content = Convert.ToString(ingredientList[3].NutrientValue);
                fibers.Content = Convert.ToString(ingredientList[4].NutrientValue);
                irons.Content = Convert.ToString(ingredientList[9].NutrientValue);
                calciums.Content = Convert.ToString(ingredientList[7].NutrientValue);

                phosphorus.Content = Convert.ToString(ingredientList[8].NutrientValue);
                vitaminARetinol.Content = Convert.ToString(ingredientList[11].NutrientValue);
                vitaminABetaCarotene.Content = Convert.ToString(ingredientList[12].NutrientValue);
                thiamine.Content = Convert.ToString(ingredientList[17].NutrientValue);
                riboflavin.Content = Convert.ToString(ingredientList[13].NutrientValue);
                nicotinicAcid.Content = Convert.ToString(ingredientList[19].NutrientValue);
                pyridoxine.Content = Convert.ToString(ingredientList[20].NutrientValue);
                folicAcid.Content = Convert.ToString(ingredientList[15].NutrientValue);
                vitaminB12.Content = Convert.ToString(ingredientList[14].NutrientValue);
                vitaminC.Content = Convert.ToString(ingredientList[16].NutrientValue);

                sodium.Content = Convert.ToString(ingredientList[5].NutrientValue);
                pottasium.Content = Convert.ToString(ingredientList[6].NutrientValue);
                zinc.Content = Convert.ToString(ingredientList[10].NutrientValue);
                moisture.Content = Convert.ToString(ingredientList[18].NutrientValue);
            }
        }

        private void GetDish(int dishID)
        {
            try
            {
                int ingredientCount = 0;
                string imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                Dish recipeDish = new Dish();
                recipeDish = DishManager.GetItem(dishID);
                for (int i = 0; i < 5; i++)
                {
                    if (dishList.Count >= i + 1)
                    {
                        if (dishList[i] != null)
                        {
                            ingredientCount++;
                        }
                    }
                }
                if (ingredientCount < 5)
                {
                    if (DishSearch(recipeDish) == false)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (dishList.Count >= (i + 1))
                            {
                                if (dishList[i] == null)
                                {
                                    dishList[i] = recipeDish;
                                    FillDishList(i + 1);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        ShowMessages("You cannot Compare Same Dish.");
                    }
                }
                else
                {
                    ShowMessages("Maximum 5 Dish(s) can be Added for Comparison.");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);

            }
            finally
            {

            }
        }

        private void GetDish(int dishID, int dishNo)
        {
            try
            {
                Dish recipeDish = new Dish();
                recipeDish = DishManager.GetItem(dishID);

                if (DishSearch(recipeDish) == false)
                {
                    FillDishList(dishID, dishNo);
                }
                else
                {
                    ShowMessages("You cannot Compare Same Dish.");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);
            }
            finally
            {

            }
        }

        private bool DishSearch(Dish dish)
        {
            bool result = false;
            foreach (Dish dishItem in dishList)
            {
                if (dishItem != null)
                {
                    if (dishItem.Id == dish.Id)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }


        private void FillDishList(int dishno)
        {
            string imgSource = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string imgPath1 = string.Empty;
            string imgPath2 = string.Empty;
            string imgPath3 = string.Empty;
            string imgPath4 = string.Empty;
            string imgPath5 = string.Empty;

            Dish dish1 = new Dish();
            Dish dish2 = new Dish();
            Dish dish3 = new Dish();
            Dish dish4 = new Dish();
            Dish dish5 = new Dish();

            if (dishno == 1)
            {
                dish1 = dishList[0];
                DishName1 = Convert.ToString(dish1.Name);
                DisplayName1 = Convert.ToString(dish1.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName1.Length > 13)
                        lblDishName1.Text = DisplayName1.Substring(0, 13) + "..";
                    else
                        lblDishName1.Text = DisplayName1;
                }
                else
                {
                    if (DishName1.Length > 13)
                        lblDishName1.Text = DishName1.Substring(0, 13) + "..";
                    else
                        lblDishName1.Text = DishName1;
                }

                FillDish(dish1.Id, dish1.Name, lblDish1Calorie, lblDish1Protein, lblDish1Carbohydrates, lblDish1Fat, lblDish1Fiber, lblDish1Iron, lblDish1Calcium, lblDish1Phosphorus, lblDish1VitaminAReti, lblDish1VitaminABeta, lblDish1Thiamine, lblDish1Riboflavin, lblDish1NicotinicAcid, lblDish1Pyridoxyne, lblDish1FolicAcid, lblDish1VitaminB12, lblDish1VitaminC, lblDish1Moisture, lblDish1Sodium, lblDish1Pottasium, lblDish1Zinc);

                if (dish1.DisplayImage != string.Empty)
                {
                    imgPath1 = GetImagePath("Dishes") + "\\" + dish1.Id + ".jpg";
                }
                else
                {
                    imgPath1 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish1.ImageSource = imgPath1;
                ImgDish1.ImageName = lblDishName1.Text;
                ImgDish1.ItemID = dish1.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish1.ToolTip = DisplayName1;
                }
                else
                {
                    ImgDish1.ToolTip = DishName1;
                }
                chkClear1.Visibility = Visibility.Visible;

                FillGraph(dish1, dishno);

            }
            if (dishno == 2)
            {
                dish2 = dishList[1];
                DishName2 = Convert.ToString(dish2.Name);
                DisplayName2 = Convert.ToString(dish2.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName2.Length > 13)
                        lblDishName2.Text = DisplayName2.Substring(0, 13) + "..";
                    else
                        lblDishName2.Text = DisplayName2;
                }
                else
                {
                    if (DishName2.Length > 13)
                        lblDishName2.Text = DishName2.Substring(0, 13) + "..";
                    else
                        lblDishName2.Text = DishName2;
                }

                FillDish(dish2.Id, dish2.Name, lblDish2Calorie, lblDish2Protein, lblDish2Carbohydrates, lblDish2Fat, lblDish2Fiber, lblDish2Iron, lblDish2Calcium, lblDish2Phosphorus, lblDish2VitaminAReti, lblDish2VitaminABeta, lblDish2Thiamine, lblDish2Riboflavin, lblDish2NicotinicAcid, lblDish2Pyridoxyne, lblDish2FolicAcid, lblDish2VitaminB12, lblDish2VitaminC, lblDish2Moisture, lblDish2Sodium, lblDish2Pottasium, lblDish2Zinc);

                if (dish2.DisplayImage != string.Empty)
                {
                    imgPath2 = GetImagePath("Dishes") + "\\" + dish2.Id + ".jpg";
                }
                else
                {
                    imgPath2 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish2.ImageSource = imgPath2;
                ImgDish2.ImageName = lblDishName2.Text;
                ImgDish2.ItemID = dish2.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish2.ToolTip = DisplayName2;
                }
                else
                {
                    ImgDish2.ToolTip = DishName2;
                }
                chkClear2.Visibility = Visibility.Visible;

                FillGraph(dish2, dishno);

            }
            if (dishno == 3)
            {
                dish3 = dishList[2];
                DishName3 = Convert.ToString(dish3.Name);
                DisplayName3 = Convert.ToString(dish3.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName3.Length > 13)
                        lblDishName3.Text = DisplayName3.Substring(0, 13) + "..";
                    else
                        lblDishName3.Text = DisplayName3;
                }
                else
                {
                    if (DishName3.Length > 13)
                        lblDishName3.Text = DishName3.Substring(0, 13) + "..";
                    else
                        lblDishName3.Text = DishName3;
                }

                FillDish(dish3.Id, dish3.Name, lblDish3Calorie, lblDish3Protein, lblDish3Carbohydrates, lblDish3Fat, lblDish3Fiber, lblDish3Iron, lblDish3Calcium, lblDish3Phosphorus, lblDish3VitaminAReti, lblDish3VitaminABeta, lblDish3Thiamine, lblDish3Riboflavin, lblDish3NicotinicAcid, lblDish3Pyridoxyne, lblDish3FolicAcid, lblDish3VitaminB12, lblDish3VitaminC, lblDish3Moisture, lblDish3Sodium, lblDish3Pottasium, lblDish3Zinc);

                if (dish3.DisplayImage != string.Empty)
                {
                    imgPath3 = GetImagePath("Dishes") + "\\" + dish3.Id + ".jpg";
                }
                else
                {
                    imgPath3 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish3.ImageSource = imgPath3;
                ImgDish3.ImageName = lblDishName3.Text;
                ImgDish3.ItemID = dish3.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish3.ToolTip = DisplayName3;
                }
                else
                {
                    ImgDish3.ToolTip = DishName3;
                }
                chkClear3.Visibility = Visibility.Visible;

                FillGraph(dish3, dishno);

            }
            if (dishno == 4)
            {
                dish4 = dishList[3];
                DishName4 = Convert.ToString(dish4.Name);
                DisplayName4 = Convert.ToString(dish4.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName4.Length > 13)
                        lblDishName4.Text = DisplayName4.Substring(0, 13) + "..";
                    else
                        lblDishName4.Text = DisplayName4;
                }
                else
                {
                    if (DishName4.Length > 13)
                        lblDishName4.Text = DishName4.Substring(0, 13) + "..";
                    else
                        lblDishName4.Text = DishName4;
                }

                FillDish(dish4.Id, dish4.Name, lblDish4Calorie, lblDish4Protein, lblDish4Carbohydrates, lblDish4Fat, lblDish4Fiber, lblDish4Iron, lblDish4Calcium, lblDish4Phosphorus, lblDish4VitaminAReti, lblDish4VitaminABeta, lblDish4Thiamine, lblDish4Riboflavin, lblDish4NicotinicAcid, lblDish4Pyridoxyne, lblDish4FolicAcid, lblDish4VitaminB12, lblDish4VitaminC, lblDish4Moisture, lblDish4Sodium, lblDish4Pottasium, lblDish4Zinc);

                if (dish4.DisplayImage != string.Empty)
                {
                    imgPath4 = GetImagePath("Dishes") + "\\" + dish4.Id + ".jpg";
                }
                else
                {
                    imgPath4 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish4.ImageSource = imgPath4;
                ImgDish4.ImageName = lblDishName4.Text;
                ImgDish4.ItemID = dish4.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish4.ToolTip = DisplayName4;
                }
                else
                {
                    ImgDish4.ToolTip = DishName4;
                }
                chkClear4.Visibility = Visibility.Visible;

                FillGraph(dish4, dishno);

            }
            if (dishno == 5)
            {
                dish5 = dishList[4];
                DishName5 = Convert.ToString(dish5.Name);
                DisplayName5 = Convert.ToString(dish5.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName5.Length > 13)
                        lblDishName5.Text = DisplayName5.Substring(0, 13) + "..";
                    else
                        lblDishName5.Text = DisplayName5;
                }
                else
                {
                    if (DishName5.Length > 13)
                        lblDishName5.Text = DishName5.Substring(0, 13) + "..";
                    else
                        lblDishName5.Text = DishName5;
                }

                FillDish(dish5.Id, dish5.Name, lblDish5Calorie, lblDish5Protein, lblDish5Carbohydrates, lblDish5Fat, lblDish5Fiber, lblDish5Iron, lblDish5Calcium, lblDish5Phosphorus, lblDish5VitaminAReti, lblDish5VitaminABeta, lblDish5Thiamine, lblDish5Riboflavin, lblDish5NicotinicAcid, lblDish5Pyridoxyne, lblDish5FolicAcid, lblDish5VitaminB12, lblDish5VitaminC, lblDish5Moisture, lblDish5Sodium, lblDish5Pottasium, lblDish5Zinc);

                if (dish5.DisplayImage != string.Empty)
                {
                    imgPath5 = GetImagePath("Dishes") + "\\" + dish5.Id + ".jpg";
                }
                else
                {
                    imgPath5 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish5.ImageSource = imgPath5;
                ImgDish5.ImageName = lblDishName5.Text;
                ImgDish5.ItemID = dish5.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish5.ToolTip = DisplayName5;
                }
                else
                {
                    ImgDish5.ToolTip = DishName5;
                }
                chkClear5.Visibility = Visibility.Visible;

                FillGraph(dish5, dishno);
            }
        }

        private void FillDishList(int dishID, int dishNo)
        {
            string imgSource = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string imgPath1 = string.Empty;
            string imgPath2 = string.Empty;
            string imgPath3 = string.Empty;
            string imgPath4 = string.Empty;
            string imgPath5 = string.Empty;

            Dish dish1 = new Dish();
            Dish dish2 = new Dish();
            Dish dish3 = new Dish();
            Dish dish4 = new Dish();
            Dish dish5 = new Dish();

            if (dishNo == 1)
            {
                dish1 = DishManager.GetItem(dishID);
                dishList[0] = dish1;
                DishName1 = Convert.ToString(dish1.Name);
                DisplayName1 = Convert.ToString(dish1.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName1.Length > 13)
                        lblDishName1.Text = DisplayName1.Substring(0, 13) + "..";
                    else
                        lblDishName1.Text = DisplayName1;
                }
                else
                {
                    if (DishName1.Length > 13)
                        lblDishName1.Text = DishName1.Substring(0, 13) + "..";
                    else
                        lblDishName1.Text = DishName1;
                }


                FillDish(dish1.Id, dish1.Name, lblDish1Calorie, lblDish1Protein, lblDish1Carbohydrates, lblDish1Fat, lblDish1Fiber, lblDish1Iron, lblDish1Calcium, lblDish1Phosphorus, lblDish1VitaminAReti, lblDish1VitaminABeta, lblDish1Thiamine, lblDish1Riboflavin, lblDish1NicotinicAcid, lblDish1Pyridoxyne, lblDish1FolicAcid, lblDish1VitaminB12, lblDish1VitaminC, lblDish1Moisture, lblDish1Sodium, lblDish1Pottasium, lblDish1Zinc);

                if (dish1.DisplayImage != string.Empty)
                {
                    imgPath1 = GetImagePath("Dishes") + "\\" + dish1.Id + ".jpg";
                }
                else
                {
                    imgPath1 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish1.ImageSource = imgPath1;
                ImgDish1.ImageName = lblDishName1.Text;
                ImgDish1.ItemID = dish1.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish1.ToolTip = DisplayName1;
                }
                else
                {
                    ImgDish1.ToolTip = DishName1;
                }
                chkClear1.Visibility = Visibility.Visible;

                FillGraph(dish1, dishNo);

            }
            if (dishNo == 2)
            {
                dish2 = DishManager.GetItem(dishID);
                dishList[1] = dish2;
                DishName2 = Convert.ToString(dish2.Name);
                DisplayName2 = Convert.ToString(dish2.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName2.Length > 13)
                        lblDishName2.Text = DisplayName2.Substring(0, 13) + "..";
                    else
                        lblDishName2.Text = DisplayName2;
                }
                else
                {
                    if (DishName2.Length > 13)
                        lblDishName2.Text = DishName2.Substring(0, 13) + "..";
                    else
                        lblDishName2.Text = DishName2;
                }

                FillDish(dish2.Id, dish2.Name, lblDish2Calorie, lblDish2Protein, lblDish2Carbohydrates, lblDish2Fat, lblDish2Fiber, lblDish2Iron, lblDish2Calcium, lblDish2Phosphorus, lblDish2VitaminAReti, lblDish2VitaminABeta, lblDish2Thiamine, lblDish2Riboflavin, lblDish2NicotinicAcid, lblDish2Pyridoxyne, lblDish2FolicAcid, lblDish2VitaminB12, lblDish2VitaminC, lblDish2Moisture, lblDish2Sodium, lblDish2Pottasium, lblDish2Zinc);

                if (dish2.DisplayImage != string.Empty)
                {
                    imgPath2 = GetImagePath("Dishes") + "\\" + dish2.Id + ".jpg";
                }
                else
                {
                    imgPath2 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish2.ImageSource = imgPath2;
                ImgDish2.ImageName = lblDishName2.Text;
                ImgDish2.ItemID = dish2.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish2.ToolTip = DisplayName2;
                }
                else
                {
                    ImgDish2.ToolTip = DishName2;
                }
                chkClear2.Visibility = Visibility.Visible;

                FillGraph(dish2, dishNo);

            }
            if (dishNo == 3)
            {
                dish3 = DishManager.GetItem(dishID);
                dishList[2] = dish3;
                DishName3 = Convert.ToString(dish3.Name);
                DisplayName3 = Convert.ToString(dish3.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName3.Length > 13)
                        lblDishName3.Text = DisplayName3.Substring(0, 13) + "..";
                    else
                        lblDishName3.Text = DisplayName3;
                }
                else
                {
                    if (DishName3.Length > 13)
                        lblDishName3.Text = DishName3.Substring(0, 13) + "..";
                    else
                        lblDishName3.Text = DishName3;
                }

                FillDish(dish3.Id, dish3.Name, lblDish3Calorie, lblDish3Protein, lblDish3Carbohydrates, lblDish3Fat, lblDish3Fiber, lblDish3Iron, lblDish3Calcium, lblDish3Phosphorus, lblDish3VitaminAReti, lblDish3VitaminABeta, lblDish3Thiamine, lblDish3Riboflavin, lblDish3NicotinicAcid, lblDish3Pyridoxyne, lblDish3FolicAcid, lblDish3VitaminB12, lblDish3VitaminC, lblDish3Moisture, lblDish3Sodium, lblDish3Pottasium, lblDish3Zinc);

                if (dish3.DisplayImage != string.Empty)
                {
                    imgPath3 = GetImagePath("Dishes") + "\\" + dish3.Id + ".jpg";
                }
                else
                {
                    imgPath3 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish3.ImageSource = imgPath3;
                ImgDish3.ImageName = lblDishName3.Text;
                ImgDish3.ItemID = dish3.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish3.ToolTip = DisplayName3;
                }
                else
                {
                    ImgDish3.ToolTip = DishName3;
                }
                chkClear3.Visibility = Visibility.Visible;

                FillGraph(dish3, dishNo);

            }
            if (dishNo == 4)
            {
                dish4 = DishManager.GetItem(dishID);
                dishList[3] = dish4;
                DishName4 = Convert.ToString(dish4.Name);
                DisplayName4 = Convert.ToString(dish4.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName4.Length > 13)
                        lblDishName4.Text = DisplayName4.Substring(0, 13) + "..";
                    else
                        lblDishName4.Text = DisplayName4;
                }
                else
                {
                    if (DishName4.Length > 13)
                        lblDishName4.Text = DishName4.Substring(0, 13) + "..";
                    else
                        lblDishName4.Text = DishName4;
                }

                FillDish(dish4.Id, dish4.Name, lblDish4Calorie, lblDish4Protein, lblDish4Carbohydrates, lblDish4Fat, lblDish4Fiber, lblDish4Iron, lblDish4Calcium, lblDish4Phosphorus, lblDish4VitaminAReti, lblDish4VitaminABeta, lblDish4Thiamine, lblDish4Riboflavin, lblDish4NicotinicAcid, lblDish4Pyridoxyne, lblDish4FolicAcid, lblDish4VitaminB12, lblDish4VitaminC, lblDish4Moisture, lblDish4Sodium, lblDish4Pottasium, lblDish4Zinc);

                if (dish4.DisplayImage != string.Empty)
                {
                    imgPath4 = GetImagePath("Dishes") + "\\" + dish4.Id + ".jpg";
                }
                else
                {
                    imgPath4 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish4.ImageSource = imgPath4;
                ImgDish4.ImageName = lblDishName4.Text;
                ImgDish4.ItemID = dish4.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish4.ToolTip = DisplayName4;
                }
                else
                {
                    ImgDish4.ToolTip = DishName4;
                }
                chkClear4.Visibility = Visibility.Visible;

                FillGraph(dish4, dishNo);

            }
            if (dishNo == 5)
            {
                dish5 = DishManager.GetItem(dishID);
                dishList[4] = dish5;
                DishName5 = Convert.ToString(dish5.Name);
                DisplayName5 = Convert.ToString(dish5.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName5.Length > 13)
                        lblDishName5.Text = DisplayName5.Substring(0, 13) + "..";
                    else
                        lblDishName5.Text = DisplayName5;
                }
                else
                {
                    if (DishName5.Length > 13)
                        lblDishName5.Text = DishName5.Substring(0, 13) + "..";
                    else
                        lblDishName5.Text = DishName5;
                }

                FillDish(dish5.Id, dish5.Name, lblDish5Calorie, lblDish5Protein, lblDish5Carbohydrates, lblDish5Fat, lblDish5Fiber, lblDish5Iron, lblDish5Calcium, lblDish5Phosphorus, lblDish5VitaminAReti, lblDish5VitaminABeta, lblDish5Thiamine, lblDish5Riboflavin, lblDish5NicotinicAcid, lblDish5Pyridoxyne, lblDish5FolicAcid, lblDish5VitaminB12, lblDish5VitaminC, lblDish5Moisture, lblDish5Sodium, lblDish5Pottasium, lblDish5Zinc);

                if (dish5.DisplayImage != string.Empty)
                {
                    imgPath5 = GetImagePath("Dishes") + "\\" + dish5.Id + ".jpg";
                }
                else
                {
                    imgPath5 = imgSource + "Images\\NoPreview.png";
                }
                ImgDish5.ImageSource = imgPath5;
                ImgDish5.ImageName = lblDishName5.Text;
                ImgDish5.ItemID = dish5.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgDish5.ToolTip = DisplayName5;
                }
                else
                {
                    ImgDish5.ToolTip = DishName5;
                }
                chkClear5.Visibility = Visibility.Visible;

                FillGraph(dish5, dishNo);
            }
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

        private void ClearAll()
        {
            if (chkClear1.IsChecked == true)
            {
                dishList[0] = null;
                DishName1 = string.Empty;
                DisplayName2 = string.Empty;
                lblDishName1.Text = string.Empty;
                lblDish1Calorie.Content = string.Empty;
                lblDish1Protein.Content = string.Empty;
                lblDish1Carbohydrates.Content = string.Empty;
                lblDish1Fat.Content = string.Empty;
                lblDish1Fiber.Content = string.Empty;
                lblDish1Iron.Content = string.Empty;
                lblDish1Calcium.Content = string.Empty;

                lblDish1Phosphorus.Content = string.Empty;
                lblDish1VitaminAReti.Content = string.Empty;
                lblDish1VitaminABeta.Content = string.Empty;
                lblDish1Thiamine.Content = string.Empty;
                lblDish1Riboflavin.Content = string.Empty;
                lblDish1NicotinicAcid.Content = string.Empty;
                lblDish1Pyridoxyne.Content = string.Empty;
                lblDish1FolicAcid.Content = string.Empty;
                lblDish1VitaminB12.Content = string.Empty;
                lblDish1VitaminC.Content = string.Empty;

                lblDish1Moisture.Content = string.Empty;
                lblDish1Sodium.Content = string.Empty;
                lblDish1Pottasium.Content = string.Empty;
                lblDish1Zinc.Content = string.Empty;

                ImgDish1.ImageSource = string.Empty;
                ImgDish1.ImageName = string.Empty;
                ImgDish1.ItemID = 0;
                chkClear1.IsChecked = false;
                chkClear1.Visibility = Visibility.Hidden;

                FillGraph(null, 1);
            }
            if (chkClear2.IsChecked == true)
            {
                dishList[1] = null;
                DishName2 = string.Empty;
                DisplayName2 = string.Empty;
                lblDishName2.Text = string.Empty;
                lblDish2Calorie.Content = string.Empty;
                lblDish2Protein.Content = string.Empty;
                lblDish2Carbohydrates.Content = string.Empty;
                lblDish2Fat.Content = string.Empty;
                lblDish2Fiber.Content = string.Empty;
                lblDish2Iron.Content = string.Empty;
                lblDish2Calcium.Content = string.Empty;

                lblDish2Phosphorus.Content = string.Empty;
                lblDish2VitaminAReti.Content = string.Empty;
                lblDish2VitaminABeta.Content = string.Empty;
                lblDish2Thiamine.Content = string.Empty;
                lblDish2Riboflavin.Content = string.Empty;
                lblDish2NicotinicAcid.Content = string.Empty;
                lblDish2Pyridoxyne.Content = string.Empty;
                lblDish2FolicAcid.Content = string.Empty;
                lblDish2VitaminB12.Content = string.Empty;
                lblDish2VitaminC.Content = string.Empty;

                lblDish2Moisture.Content = string.Empty;
                lblDish2Sodium.Content = string.Empty;
                lblDish2Pottasium.Content = string.Empty;
                lblDish2Zinc.Content = string.Empty;

                ImgDish2.ImageSource = string.Empty;
                ImgDish2.ImageName = string.Empty;
                ImgDish2.ItemID = 0;
                chkClear2.IsChecked = false;
                chkClear2.Visibility = Visibility.Hidden;

                FillGraph(null, 2);
            }
            if (chkClear3.IsChecked == true)
            {
                dishList[2] = null;
                DishName3 = string.Empty;
                DisplayName3 = string.Empty;
                lblDishName3.Text = string.Empty;
                lblDish3Calorie.Content = string.Empty;
                lblDish3Protein.Content = string.Empty;
                lblDish3Carbohydrates.Content = string.Empty;
                lblDish3Fat.Content = string.Empty;
                lblDish3Fiber.Content = string.Empty;
                lblDish3Iron.Content = string.Empty;
                lblDish3Calcium.Content = string.Empty;

                lblDish3Phosphorus.Content = string.Empty;
                lblDish3VitaminAReti.Content = string.Empty;
                lblDish3VitaminABeta.Content = string.Empty;
                lblDish3Thiamine.Content = string.Empty;
                lblDish3Riboflavin.Content = string.Empty;
                lblDish3NicotinicAcid.Content = string.Empty;
                lblDish3Pyridoxyne.Content = string.Empty;
                lblDish3FolicAcid.Content = string.Empty;
                lblDish3VitaminB12.Content = string.Empty;
                lblDish3VitaminC.Content = string.Empty;

                lblDish3Moisture.Content = string.Empty;
                lblDish3Sodium.Content = string.Empty;
                lblDish3Pottasium.Content = string.Empty;
                lblDish3Zinc.Content = string.Empty;

                ImgDish3.ImageSource = string.Empty;
                ImgDish3.ImageName = string.Empty;
                ImgDish3.ItemID = 0;
                chkClear3.IsChecked = false;
                chkClear3.Visibility = Visibility.Hidden;

                FillGraph(null, 3);
            }
            if (chkClear4.IsChecked == true)
            {
                dishList[3] = null;
                DishName4 = string.Empty;
                DisplayName4 = string.Empty;
                lblDishName4.Text = string.Empty;
                lblDish4Calorie.Content = string.Empty;
                lblDish4Protein.Content = string.Empty;
                lblDish4Carbohydrates.Content = string.Empty;
                lblDish4Fat.Content = string.Empty;
                lblDish4Fiber.Content = string.Empty;
                lblDish4Iron.Content = string.Empty;
                lblDish4Calcium.Content = string.Empty;

                lblDish4Phosphorus.Content = string.Empty;
                lblDish4VitaminAReti.Content = string.Empty;
                lblDish4VitaminABeta.Content = string.Empty;
                lblDish4Thiamine.Content = string.Empty;
                lblDish4Riboflavin.Content = string.Empty;
                lblDish4NicotinicAcid.Content = string.Empty;
                lblDish4Pyridoxyne.Content = string.Empty;
                lblDish4FolicAcid.Content = string.Empty;
                lblDish4VitaminB12.Content = string.Empty;
                lblDish4VitaminC.Content = string.Empty;

                lblDish4Moisture.Content = string.Empty;
                lblDish4Sodium.Content = string.Empty;
                lblDish4Pottasium.Content = string.Empty;
                lblDish4Zinc.Content = string.Empty;

                ImgDish4.ImageSource = string.Empty;
                ImgDish4.ImageName = string.Empty;
                ImgDish4.ItemID = 0;
                chkClear4.IsChecked = false;
                chkClear4.Visibility = Visibility.Hidden;

                FillGraph(null, 4);
            }
            if (chkClear5.IsChecked == true)
            {
                dishList[4] = null;
                DishName5 = string.Empty;
                DisplayName5 = string.Empty;
                lblDishName5.Text = string.Empty;
                lblDish5Calorie.Content = string.Empty;
                lblDish5Protein.Content = string.Empty;
                lblDish5Carbohydrates.Content = string.Empty;
                lblDish5Fat.Content = string.Empty;
                lblDish5Fiber.Content = string.Empty;
                lblDish5Iron.Content = string.Empty;
                lblDish5Calcium.Content = string.Empty;

                lblDish5Phosphorus.Content = string.Empty;
                lblDish5VitaminAReti.Content = string.Empty;
                lblDish5VitaminABeta.Content = string.Empty;
                lblDish5Thiamine.Content = string.Empty;
                lblDish5Riboflavin.Content = string.Empty;
                lblDish5NicotinicAcid.Content = string.Empty;
                lblDish5Pyridoxyne.Content = string.Empty;
                lblDish5FolicAcid.Content = string.Empty;
                lblDish5VitaminB12.Content = string.Empty;
                lblDish5VitaminC.Content = string.Empty;

                lblDish5Moisture.Content = string.Empty;
                lblDish5Sodium.Content = string.Empty;
                lblDish5Pottasium.Content = string.Empty;
                lblDish5Zinc.Content = string.Empty;

                ImgDish5.ImageSource = string.Empty;
                ImgDish5.ImageName = string.Empty;
                ImgDish5.ItemID = 0;
                chkClear5.IsChecked = false;
                chkClear5.Visibility = Visibility.Hidden;

                FillGraph(null, 5);
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

        public void DisableControls()
        {

        }

        public void EnableControls()
        {

        }

        public void InitailizeControls()
        {
            CalorieGraphLayout.Visibility = Visibility.Hidden;
            ProteinGraphLayout.Visibility = Visibility.Hidden;
            FATGraphLayout.Visibility = Visibility.Hidden;
            FibreGraphLayout.Visibility = Visibility.Hidden;
            CarboHydratesGraphLayout.Visibility = Visibility.Hidden;
            IronGraphLayout.Visibility = Visibility.Hidden;
            CalciumGraphLayout.Visibility = Visibility.Hidden;
            PhosphrousGraphLayout.Visibility = Visibility.Hidden;
            SodiumGraphLayout.Visibility = Visibility.Hidden;
            PottasiumGraphLayout.Visibility = Visibility.Hidden;
            ZincGraphLayout.Visibility = Visibility.Hidden;
            RetinolGraphLayout.Visibility = Visibility.Hidden;
            BetaGraphLayout.Visibility = Visibility.Hidden;
            ThaimineGraphLayout.Visibility = Visibility.Hidden;
            RiboflavinGraphLayout.Visibility = Visibility.Hidden;
            NaicinGraphLayout.Visibility = Visibility.Hidden;
            PyrodoxineGraphLayout.Visibility = Visibility.Hidden;
            FolicAcidGraphLayout.Visibility = Visibility.Hidden;
            VitaminCGraphLayout.Visibility = Visibility.Hidden;
            MoistureGraphLayout.Visibility = Visibility.Hidden;
        }

        private void VisibleGraph(System.Windows.Controls.Grid GraphLayout)
        {
            CalorieGraphLayout.Visibility = Visibility.Hidden;
            ProteinGraphLayout.Visibility = Visibility.Hidden;
            FATGraphLayout.Visibility = Visibility.Hidden;
            FibreGraphLayout.Visibility = Visibility.Hidden;
            CarboHydratesGraphLayout.Visibility = Visibility.Hidden;
            IronGraphLayout.Visibility = Visibility.Hidden;
            CalciumGraphLayout.Visibility = Visibility.Hidden;
            PhosphrousGraphLayout.Visibility = Visibility.Hidden;
            SodiumGraphLayout.Visibility = Visibility.Hidden;
            PottasiumGraphLayout.Visibility = Visibility.Hidden;
            ZincGraphLayout.Visibility = Visibility.Hidden;
            RetinolGraphLayout.Visibility = Visibility.Hidden;
            BetaGraphLayout.Visibility = Visibility.Hidden;
            ThaimineGraphLayout.Visibility = Visibility.Hidden;
            RiboflavinGraphLayout.Visibility = Visibility.Hidden;
            NaicinGraphLayout.Visibility = Visibility.Hidden;
            PyrodoxineGraphLayout.Visibility = Visibility.Hidden;
            FolicAcidGraphLayout.Visibility = Visibility.Hidden;
            VitaminCGraphLayout.Visibility = Visibility.Hidden;
            MoistureGraphLayout.Visibility = Visibility.Hidden;

            GraphLayout.Visibility = Visibility.Visible;
        }

        private void CreateGraph(int GraphType, int GraphIndex)
        {
            switch (GraphIndex)
            {
                case (int)NutritionType.Calorie:
                    CreateNutrientGraph(Calorie, "Calorie", DishName, GraphType, CalorieGraphLayout);
                    break;
                case (int)NutritionType.Protein:
                    CreateNutrientGraph(Protein, "Protein", DishName, GraphType, ProteinGraphLayout);
                    break;
                case (int)NutritionType.Carbohydrates:
                    CreateNutrientGraph(Carbo, "CarboHydrates", DishName, GraphType, CarboHydratesGraphLayout);
                    break;
                case (int)NutritionType.Fat:
                    CreateNutrientGraph(FAT, "Fat", DishName, GraphType, FATGraphLayout);
                    break;
                case (int)NutritionType.Fiber:
                    CreateNutrientGraph(Fibre, "Fibre", DishName, GraphType, FibreGraphLayout);
                    break;
                case (int)NutritionType.Moisture:
                    CreateNutrientGraph(Moisture, "Moisture", DishName, GraphType, MoistureGraphLayout);
                    break;
                case (int)NutritionType.Iron:
                    CreateNutrientGraph(Iron, "Iron", DishName, GraphType, IronGraphLayout);
                    break;
                case (int)NutritionType.Calcium:
                    CreateNutrientGraph(Calcium, "Calcium", DishName, GraphType, CalciumGraphLayout);
                    break;
                case (int)NutritionType.Phosphorus:
                    CreateNutrientGraph(Phosphorus, "Phosphorus", DishName, GraphType, PhosphrousGraphLayout);
                    break;
                case (int)NutritionType.Sodium:
                    CreateNutrientGraph(Sodium, "Sodium", DishName, GraphType, SodiumGraphLayout);
                    break;
                case (int)NutritionType.Pottasuim:
                    CreateNutrientGraph(Pottasuim, "Pottasuim", DishName, GraphType, PottasiumGraphLayout);
                    break;
                case (int)NutritionType.Zinc:
                    CreateNutrientGraph(Zinc, "Zinc", DishName, GraphType, ZincGraphLayout);
                    break;
                case (int)NutritionType.Retinol:
                    CreateNutrientGraph(Retinol, "Retinol", DishName, GraphType, RetinolGraphLayout);
                    break;
                case (int)NutritionType.BetaCarotene:
                    CreateNutrientGraph(BetaCarotine, "BetaCarotine", DishName, GraphType, BetaGraphLayout);
                    break;
                case (int)NutritionType.Thiamine:
                    CreateNutrientGraph(Thiamine, "Thiamine", DishName, GraphType, ThaimineGraphLayout);
                    break;
                case (int)NutritionType.Riboflavin:
                    CreateNutrientGraph(Ribiflavin, "Ribiflavin", DishName, GraphType, RiboflavinGraphLayout);
                    break;
                case (int)NutritionType.NicotinicAcid:
                    CreateNutrientGraph(Naicin, "Nicotinic Acid", DishName, GraphType, NaicinGraphLayout);
                    break;
                case (int)NutritionType.Pyridoxine:
                    CreateNutrientGraph(Pyridoxine, "Pyridoxine", DishName, GraphType, PyrodoxineGraphLayout);
                    break;
                case (int)NutritionType.FolicAcid:
                    CreateNutrientGraph(FolicAcid, "Folic Acid", DishName, GraphType, FolicAcidGraphLayout);
                    break;
                case (int)NutritionType.VitaminC:
                    CreateNutrientGraph(VitamineC, "VitamineC", DishName, GraphType, VitaminCGraphLayout);
                    break;
                default:
                    CreateNutrientGraph(Calorie, "Calorie", DishName, GraphType, CalorieGraphLayout);
                    break;
            }
        }

        public void InitailizeList()
        {
            for (int i = 0; i < 5; i++)
            {
                dishList.Insert(i, null);
            }
        }

        #endregion

        #region Graph

        public void CreateNutrientGraph(double[] NutrientValue, string NutrienName, string[] DishName, int Mode, System.Windows.Controls.Grid GraphLayout)
        {
            chart = new Visifire.Charts.Chart();
            chart.Cursor = Cursors.Hand;
            chart.ScrollingEnabled = false;
            chart.BorderThickness = new Thickness(4, 4, 4, 4);
            chart.BorderBrush = new SolidColorBrush(Colors.Gray);

            Title header = new Title();
            header.FontSize = 12;
            header.FontWeight = FontWeights.Bold;
            header.FontStyle = FontStyles.Italic;
            header.VerticalAlignment = VerticalAlignment.Top;
            header.HorizontalAlignment = HorizontalAlignment.Left;
            header.Text = NutrienName;
            chart.Titles.Add(header);

            Visifire.Charts.Axis axisX = new Visifire.Charts.Axis();
            chart.AxesX.Add(axisX);

            Visifire.Charts.Axis axisY = new Visifire.Charts.Axis();
            chart.AxesY.Add(axisY);

            DataSeries dataSeries = new DataSeries();
            if (Mode == 1)
                dataSeries.RenderAs = RenderAs.Bar;
            else
                dataSeries.RenderAs = RenderAs.Column;

            if (NutrientValue.Length > 0)
            {
                for (int i = 0; i < NutrientValue.Length; i++)
                {
                    DataPoint dataPoint = new Visifire.Charts.DataPoint();
                    string AxisXLabel = DishName[i].ToString();
                    if (AxisXLabel.Length > 15)
                        AxisXLabel = AxisXLabel.Substring(0, 15) + "..";
                    dataPoint.AxisXLabel = AxisXLabel;
                    dataPoint.YValue = NutrientValue[i];
                    dataSeries.DataPoints.Add(dataPoint);
                }
            }

            chart.Series.Add(dataSeries);
            GraphLayout.Children.Add(chart);

        }

        #endregion

        #region Events

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            int View = ReadGraphRegistry();
            ChangeView(View);

            Keyboard.Focus(cboDishCategory);
            Classes.CommonFunctions.FillDishCategory(cboDishCategory);

            LoadImages();
            InitailizeControls();
            RepopulateData();
            InitailizeList();

            CalorieGraphLayout.Visibility = Visibility.Visible;
        }

        private void spSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtSearch.Text.Trim() != string.Empty || cboDishCategory.SelectedIndex > 0)
            {
                FillSearchList();

                int Result = Classes.CommonFunctions.Convert2Int(Convert.ToString(cboDishCategory.SelectedIndex)); ;
                WriteCompareRegistry(Result);
            }
            else
            {
                ShowMessage();
            }
        }

        private void cboDishCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                {
                    SearchList.SelectedIndex = SearchList.ItemContainerGenerator.IndexFromContainer(lvi);

                    if (SearchList.SelectedIndex >= 0)
                    {
                        int CurrentDishID = ((Dish)SearchList.Items[SearchList.SelectedIndex]).Id;
                        GetDish(CurrentDishID);
                    }
                    else
                    {
                        ShowMessages("Please Select an Dish to Add");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);

            }
        }

        private void ImgDish1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dishList.Count >= 1)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Dish;
                if (dishList[0] != null)
                {
                    imagePreview.ItemID = dishList[0].Id;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        imagePreview.IsRegional = true;
                    }
                    else
                    {
                        imagePreview.IsRegional = false;
                    }
                    imagePreview.Owner = Application.Current.MainWindow;
                    imagePreview.ShowDialog();
                }
            }
        }

        private void ImgDish2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dishList.Count >= 2)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Dish;
                if (dishList[1] != null)
                {
                    imagePreview.ItemID = dishList[1].Id;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        imagePreview.IsRegional = true;
                    }
                    else
                    {
                        imagePreview.IsRegional = false;
                    }
                    imagePreview.Owner = Application.Current.MainWindow;
                    imagePreview.ShowDialog();
                }
            }
        }

        private void ImgDish3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dishList.Count >= 3)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Dish;
                if (dishList[2] != null)
                {
                    imagePreview.ItemID = dishList[2].Id;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        imagePreview.IsRegional = true;
                    }
                    else
                    {
                        imagePreview.IsRegional = false;
                    }
                    imagePreview.Owner = Application.Current.MainWindow;
                    imagePreview.ShowDialog();
                }
            }
        }

        private void ImgDish4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dishList.Count >= 4)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Dish;
                if (dishList[3] != null)
                {
                    imagePreview.ItemID = dishList[3].Id;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        imagePreview.IsRegional = true;
                    }
                    else
                    {
                        imagePreview.IsRegional = false;
                    }
                    imagePreview.Owner = Application.Current.MainWindow;
                    imagePreview.ShowDialog();
                }
            }
        }

        private void ImgDish5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dishList.Count >= 5)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Dish;
                if (dishList[4] != null)
                {
                    imagePreview.ItemID = dishList[4].Id;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        imagePreview.IsRegional = true;
                    }
                    else
                    {
                        imagePreview.IsRegional = false;
                    }
                    imagePreview.Owner = Application.Current.MainWindow;
                    imagePreview.ShowDialog();
                }
            }
        }

        private void lblClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkClear1.IsChecked == false && chkClear2.IsChecked == false && chkClear3.IsChecked == false && chkClear4.IsChecked == false && chkClear5.IsChecked == false)
                {
                    AlertBox.Show("Please Select an Dish to Clear", "", AlertType.Exclamation, AlertButtons.OK);
                    return;
                }
                if (dishList.Count != 0)
                {
                    bool result = AlertBox.Show("Do You Want to Clear the Selected Dish", "", AlertType.Exclamation, AlertButtons.YESNO);
                    if (result == true)
                    {
                        ClearAll();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SearchList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);

            }
        }

        private void rbVeg_Checked(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void rbNonVeg_Checked(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void rbAll_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded == true)
                FillSearchList();
        }

        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {

            }
        }

        private void txtSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FillSearchList();
            }
        }
        private void SearchList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (SearchList.SelectedIndex >= 0)
                    {
                        int CurrentDishID = ((Dish)SearchList.Items[SearchList.SelectedIndex]).Id;
                        GetDish(CurrentDishID);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessages(ex.Message);

                }
            }
        }

        private void chkRegionalNames_Checked(object sender, RoutedEventArgs e)
        {
            if (chkRegionalNames.IsChecked == true)
            {
                lblDishName1.Text = DisplayName1;
                lblDishName2.Text = DisplayName2;
                lblDishName3.Text = DisplayName3;
                lblDishName4.Text = DisplayName4;
                lblDishName5.Text = DisplayName5;

                ImgDish1.ImageName = DisplayName1;
                ImgDish2.ImageName = DisplayName2;
                ImgDish3.ImageName = DisplayName3;
                ImgDish4.ImageName = DisplayName4;
                ImgDish5.ImageName = DisplayName5;

                gvCol2.CellTemplate = this.FindResource("displayNameTemplate") as DataTemplate;
            }
            else
            {
                lblDishName1.Text = DishName1;
                lblDishName2.Text = DishName2;
                lblDishName3.Text = DishName3;
                lblDishName4.Text = DishName4;

                ImgDish1.ImageName = DishName1;
                ImgDish2.ImageName = DishName2;
                ImgDish3.ImageName = DishName3;
                ImgDish4.ImageName = DishName4;
                ImgDish5.ImageName = DishName5;

                gvCol2.CellTemplate = this.FindResource("nameTemplate") as DataTemplate;
            }
        }

        private void chkRegionalNames_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkRegionalNames.IsChecked == true)
            {
                lblDishName1.Text = DisplayName1;
                lblDishName2.Text = DisplayName2;
                lblDishName3.Text = DisplayName3;
                lblDishName4.Text = DisplayName4;

                ImgDish1.ImageName = DisplayName1;
                ImgDish2.ImageName = DisplayName2;
                ImgDish3.ImageName = DisplayName3;
                ImgDish4.ImageName = DisplayName4;
                ImgDish5.ImageName = DisplayName5;

                gvCol2.CellTemplate = this.FindResource("displayNameTemplate") as DataTemplate;
            }
            else
            {
                lblDishName1.Text = DishName1;
                lblDishName2.Text = DishName2;
                lblDishName3.Text = DishName3;
                lblDishName4.Text = DishName4;

                ImgDish1.ImageName = DishName1;
                ImgDish2.ImageName = DishName2;
                ImgDish3.ImageName = DishName3;
                ImgDish4.ImageName = DishName4;
                ImgDish5.ImageName = DishName5;

                gvCol2.CellTemplate = this.FindResource("nameTemplate") as DataTemplate;
            }
        }

        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            SetThemeOnClick(ColumnGraph);
            WriteGraphRegistry((int)NutrientGraphType.ColumnGraph);
            CreateGraph((int)NutrientGraphType.ColumnGraph, GraphIndex);
        }

        private void btnIcon_Click(object sender, RoutedEventArgs e)
        {
            SetThemeOnClick(BarGraph);
            WriteGraphRegistry((int)NutrientGraphType.BarGraph);
            CreateGraph((int)NutrientGraphType.BarGraph, GraphIndex);
        }

        private void tvCalorie_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Calorie;
            GraphType = ReadGraphRegistry();
            VisibleGraph(CalorieGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvProtein_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Protein;
            GraphType = ReadGraphRegistry();
            VisibleGraph(ProteinGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvCarboHydrate_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Carbohydrates;
            GraphType = ReadGraphRegistry();
            VisibleGraph(CarboHydratesGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvFat_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Fat;
            GraphType = ReadGraphRegistry();
            VisibleGraph(FATGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvFibre_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Fiber;
            GraphType = ReadGraphRegistry();
            VisibleGraph(FibreGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvMoisture_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Moisture;
            GraphType = ReadGraphRegistry();
            VisibleGraph(MoistureGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvIron_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Iron;
            GraphType = ReadGraphRegistry();
            VisibleGraph(IronGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvCalcium_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Calcium;
            GraphType = ReadGraphRegistry();
            VisibleGraph(CalciumGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvPhosphrous_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Phosphorus;
            GraphType = ReadGraphRegistry();
            VisibleGraph(PhosphrousGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvSodium_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Sodium;
            GraphType = ReadGraphRegistry();
            VisibleGraph(SodiumGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvPottasium_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Pottasuim;
            GraphType = ReadGraphRegistry();
            VisibleGraph(PottasiumGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvZinc_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Zinc;
            GraphType = ReadGraphRegistry();
            VisibleGraph(ZincGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvRetinol_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Retinol;
            GraphType = ReadGraphRegistry();
            VisibleGraph(RetinolGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvBeta_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.BetaCarotene;
            GraphType = ReadGraphRegistry();
            VisibleGraph(BetaGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvThaimine_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Thiamine;
            GraphType = ReadGraphRegistry();
            VisibleGraph(ThaimineGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvRiboflavin_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Riboflavin;
            GraphType = ReadGraphRegistry();
            VisibleGraph(RiboflavinGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvNaicine_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.NicotinicAcid;
            GraphType = ReadGraphRegistry();
            VisibleGraph(NaicinGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvPyridoxine_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.Pyridoxine;
            GraphType = ReadGraphRegistry();
            VisibleGraph(PyrodoxineGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvFolicAcid_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.FolicAcid;
            GraphType = ReadGraphRegistry();
            VisibleGraph(FolicAcidGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void tvVitaminC_Selected(object sender, RoutedEventArgs e)
        {
            GraphIndex = (int)NutritionType.VitaminC;
            GraphType = ReadGraphRegistry();
            VisibleGraph(VitaminCGraphLayout);
            CreateGraph(GraphType, GraphIndex);
        }

        private void expMacroNutrients_Expanded(object sender, RoutedEventArgs e)
        {
            GridLength len = new GridLength(200);
            dgItem.RowDefinitions[0].Height = len;
        }

        private void expMacroNutrients_Collapsed(object sender, RoutedEventArgs e)
        {
            GridLength len = new GridLength(30);
            dgItem.RowDefinitions[0].Height = len;
        }

        private void expMinerals_Expanded(object sender, RoutedEventArgs e)
        {
            GridLength len = new GridLength(200);
            dgItem.RowDefinitions[1].Height = len;
        }

        private void expMinerals_Collapsed(object sender, RoutedEventArgs e)
        {
            GridLength len = new GridLength(30);
            dgItem.RowDefinitions[1].Height = len;
        }

        private void expVitamins_Expanded(object sender, RoutedEventArgs e)
        {
            GridLength len = new GridLength(250);
            dgItem.RowDefinitions[2].Height = len;
        }

        private void expVitamins_Collapsed(object sender, RoutedEventArgs e)
        {
            GridLength len = new GridLength(30);
            dgItem.RowDefinitions[2].Height = len;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region DragDrop

        private void HandlerPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedItem = sender as ListViewItem;
        }

        private void HandlerPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(SelectedItem, SelectedItem.Content, DragDropEffects.Copy);
            }
        }

        private void SearchList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void SearchList_Drop(object sender, DragEventArgs e)
        {

        }

        private void ImgDish1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ImgDish1_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentDishID = ImgDish1.ItemID;

                    if (CurrentDishID == 0)
                    {
                        int DishID = ((BONutrition.Dish)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (DishID > 0)
                        {
                            GetDish(DishID, 1);
                        }
                    }
                    else
                    {
                        ShowMessages("Dish Exists , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Dish to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);

            }
        }

        private void ImgDish2_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ImgDish2_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentDishID = ImgDish2.ItemID;

                    if (CurrentDishID == 0)
                    {
                        int DishID = ((BONutrition.Dish)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (DishID > 0)
                        {
                            GetDish(DishID, 2);
                        }
                    }
                    else
                    {
                        ShowMessages("Cannot Add Dish , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Dish to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);

            }
        }

        private void ImgDish3_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ImgDish3_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentDishID = ImgDish3.ItemID;

                    if (CurrentDishID == 0)
                    {
                        int DishID = ((BONutrition.Dish)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (DishID > 0)
                        {
                            GetDish(DishID, 3);
                        }
                    }
                    else
                    {
                        ShowMessages("Cannot Add Dish , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Dish to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);

            }
        }

        private void ImgDish4_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ImgDish4_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentDishID = ImgDish4.ItemID;

                    if (CurrentDishID == 0)
                    {
                        int DishID = ((BONutrition.Dish)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (DishID > 0)
                        {
                            GetDish(DishID, 4);
                        }
                    }
                    else
                    {
                        ShowMessages("Cannot Add Dish , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Dish to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);

            }
        }

        private void ImgDish5_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ImgDish5_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentDishID = ImgDish5.ItemID;

                    if (CurrentDishID == 0)
                    {
                        int DishID = ((BONutrition.Dish)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;
                        if (DishID > 0)
                        {
                            GetDish(DishID, 5);
                        }
                    }
                    else
                    {
                        ShowMessages("Cannot Add Dish , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Dish to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);

            }
        }

        #endregion



    }
}

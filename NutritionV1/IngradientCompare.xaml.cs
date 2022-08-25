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
    /// Interaction logic for IngradientCompare.xaml
    /// </summary>
    public partial class IngradientCompare : Page
    {

        #region Variables

        List<Ingredient> ingredientList = new List<Ingredient>();

        string IngredientName1 = string.Empty;
        string IngredientName2 = string.Empty;
        string IngredientName3 = string.Empty;
        string IngredientName4 = string.Empty;
        string IngredientName5 = string.Empty;

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
        
        string[] IngredientName = new string[5];
        int Result;
        int[] Results = new int[3];
        int GraphIndex;
        int GraphType;

        private ListViewItem SelectedItem;

        #endregion

        #region Constructor

        public IngradientCompare()
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
                cboIngredientCategory.SelectedIndex = Result;
                FillSearchList();
            }
        }

        private void GridAnimation(string animationname)
        {
            
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;

            ImgIngredient1.SetThemes = true;
            ImgIngredient2.SetThemes = true;
            ImgIngredient3.SetThemes = true;
            ImgIngredient4.SetThemes = true;
            ImgIngredient5.SetThemes = true;

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
            AlertBox.Show("Select IngredientCategory or Type Search Criteria", "", AlertType.Information, AlertButtons.OK);
        } 

        private void FillSearchList()
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                List<Ingredient> ingrSearchList = new List<Ingredient>();
                string searchString = string.Empty;

                searchString = "1=1 ";

                if (txtSearch.Text.Trim() != string.Empty)
                {
                    searchString = searchString + " AND (IngredientName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%' ";
                    searchString = searchString + " OR DisplayName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%') ";
                }

                if (cboIngredientCategory.SelectedIndex > 0)
                {
                    searchString = searchString + " And FoodHabitID=" + cboIngredientCategory.SelectedIndex + " ";
                }
                
                searchString = "Where " + searchString;
                ingrSearchList = IngredientManager.GetList(searchString);

                foreach (Ingredient ingr in ingrSearchList)
                {
                    if (ingr.DisplayImage != string.Empty)
                        ingr.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Ingredients" + "\\" + ingr.Id + ".jpg";
                    else
                        ingr.DisplayImage = null;

                }

                if (ingrSearchList != null)
                {
                    SearchList.ItemsSource = null;
                    SearchList.Items.Refresh();

                    SearchList.ItemsSource = ingrSearchList;
                    SearchList.SelectedIndex = 0;
                    SearchList.ScrollIntoView(SearchList.SelectedItem);
                }

            }
        }

        private void FillGraph(Ingredient ingredient, int ingredientno)
        {
            if (ingredientno == 1)
            {
                if (ingredient != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(ingredient.Id);

                    if (ingredientList != null)
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

                        IngredientName[0] = Classes.CommonFunctions.Convert2String(Convert.ToString(ingredient.Name));

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
                    IngredientName[0] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }
            }
            if (ingredientno == 2)
            {
                if (ingredient != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(ingredient.Id);

                    if (ingredientList != null)
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

                        IngredientName[1] = Classes.CommonFunctions.Convert2String(Convert.ToString(ingredient.Name));

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
                    IngredientName[1] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }

            }
            if (ingredientno == 3)
            {
                if (ingredient != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(ingredient.Id);

                    if (ingredientList != null)
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

                        IngredientName[2] = Classes.CommonFunctions.Convert2String(Convert.ToString(ingredient.Name));

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
                    IngredientName[2] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }

            }
            if (ingredientno == 4)
            {
                if (ingredient != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(ingredient.Id);

                    if (ingredientList != null)
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

                        IngredientName[3] = Classes.CommonFunctions.Convert2String(Convert.ToString(ingredient.Name));

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
                    IngredientName[3] = string.Empty;

                    GraphType = ReadGraphRegistry();
                    CreateGraph(GraphType, GraphIndex);
                }

            }
            if (ingredientno == 5)
            {
                if (ingredient != null)
                {
                    List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
                    ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(ingredient.Id);

                    if (ingredientList != null)
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

                        IngredientName[4] = Classes.CommonFunctions.Convert2String(Convert.ToString(ingredient.Name));

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
                    IngredientName[4] = string.Empty;

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
                IngredientName[i] = string.Empty;
            }

            CreateGraph((int)NutrientGraphType.BarGraph, (int)NutritionType.Calorie);
        }

        private void FillIngredient(int ingrId, string ingrName, Label calories, Label proteins, Label carbohydrates, Label fats, Label fibers, Label irons, Label calciums,
                                                Label phosphorus, Label vitaminARetinol, Label vitaminABetaCarotene, Label thiamine, Label riboflavin, Label nicotinicAcid, Label pyridoxine,
                                                Label folicAcid, Label vitaminB12, Label vitaminC, Label moisture, Label sodium, Label pottasium, Label zinc)
        {
            List<IngredientNutrients> ingredientList = new List<IngredientNutrients>();
            ingredientList = IngredientNutrientsManager.GetItemNutrientsMain(ingrId);

            if (ingredientList != null)
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

        private void GetIngredient(int ingredientID)
        {
            try
            {
                int ingredientCount = 0;
                string imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                Ingredient recipeDish = new Ingredient();
                recipeDish = IngredientManager.GetItem(ingredientID);
                for (int i = 0; i < 5; i++)
                {
                    if (ingredientList.Count >= i + 1)
                    {
                        if (ingredientList[i] != null)
                        {
                            ingredientCount++;
                        }
                    }
                }
                if (ingredientCount < 5)
                {
                    if (IngredientSearch(recipeDish) == false)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (ingredientList.Count >= (i + 1))
                            {
                                if (ingredientList[i] == null)
                                {
                                    ingredientList[i] = recipeDish;
                                    FillIngredientList(i + 1);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        ShowMessages("You cannot Compare Same Ingredient.");
                    }
                }
                else
                {
                    ShowMessages("Maximum 5 Ingredient(s) can be Added for Comparison.");
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

        private void GetIngredient(int IngredientID, int IngredientNo)
        {
            try
            {
                Ingredient recipeDish = new Ingredient();
                recipeDish = IngredientManager.GetItem(IngredientID);
                               
                if (IngredientSearch(recipeDish) == false)
                {
                    FillIngredientList(IngredientID, IngredientNo);
                }
                else
                {
                    ShowMessages("You cannot Compare Same Ingredient.");
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

        private bool IngredientSearch(Ingredient ingredient)
        {
            bool result = false;
            foreach (Ingredient ingredientItem in ingredientList)
            {
                if (ingredientItem != null)
                {
                    if (ingredientItem.Id == ingredient.Id)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        
        private void FillIngredientList(int ingredientno)
        {
            string imgSource = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string imgPath1 = string.Empty;
            string imgPath2 = string.Empty;
            string imgPath3 = string.Empty;
            string imgPath4 = string.Empty;
            string imgPath5 = string.Empty;

            Ingredient ingredient1 = new Ingredient();
            Ingredient ingredient2 = new Ingredient();
            Ingredient ingredient3 = new Ingredient();
            Ingredient ingredient4 = new Ingredient();
            Ingredient ingredient5 = new Ingredient();

            if (ingredientno == 1)
            {
                ingredient1 = ingredientList[0];
                IngredientName1 = Convert.ToString(ingredient1.Name);
                DisplayName1 = Convert.ToString(ingredient1.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName1.Length > 13)
                        lblIngredientName1.Text = DisplayName1.Substring(0, 13) + "..";
                    else
                        lblIngredientName1.Text = DisplayName1;
                }
                else
                {
                    if (IngredientName1.Length > 13)
                        lblIngredientName1.Text = IngredientName1.Substring(0, 13) + "..";
                    else
                        lblIngredientName1.Text = IngredientName1;
                }

                FillIngredient(ingredient1.Id, ingredient1.Name, lblIngredient1Calorie, lblIngredient1Protein, lblIngredient1Carbohydrates, lblIngredient1Fat, lblIngredient1Fiber, lblIngredient1Iron, lblIngredient1Calcium, lblIngredient1Phosphorus, lblIngredient1VitaminAReti, lblIngredient1VitaminABeta, lblIngredient1Thiamine, lblIngredient1Riboflavin, lblIngredient1NicotinicAcid, lblIngredient1Pyridoxyne, lblIngredient1FolicAcid, lblIngredient1VitaminB12, lblIngredient1VitaminC, lblIngredient1Moisture, lblIngredient1Sodium, lblIngredient1Pottasium, lblIngredient1Zinc);

                if (ingredient1.DisplayImage != string.Empty)
                {
                    imgPath1 = GetImagePath("Ingredients") + "\\" + ingredient1.Id + ".jpg";
                }
                else
                {
                    imgPath1 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient1.ImageSource = imgPath1;
                ImgIngredient1.ImageName = lblIngredientName1.Text;
                ImgIngredient1.ItemID = ingredient1.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient1.ToolTip = DisplayName1;
                }
                else
                {
                    ImgIngredient1.ToolTip = IngredientName1;
                }
                chkClear1.Visibility = Visibility.Visible;

                FillGraph(ingredient1, ingredientno);

            }
            if (ingredientno == 2)
            {
                ingredient2 = ingredientList[1];
                IngredientName2 = Convert.ToString(ingredient2.Name);
                DisplayName2 = Convert.ToString(ingredient2.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName2.Length > 13)
                        lblIngredientName2.Text = DisplayName2.Substring(0, 13) + "..";
                    else
                        lblIngredientName2.Text = DisplayName2;
                }
                else
                {
                    if (IngredientName2.Length > 13)
                        lblIngredientName2.Text = IngredientName2.Substring(0, 13) + "..";
                    else
                        lblIngredientName2.Text = IngredientName2;
                }

                FillIngredient(ingredient2.Id, ingredient2.Name, lblIngredient2Calorie, lblIngredient2Protein, lblIngredient2Carbohydrates, lblIngredient2Fat, lblIngredient2Fiber, lblIngredient2Iron, lblIngredient2Calcium, lblIngredient2Phosphorus, lblIngredient2VitaminAReti, lblIngredient2VitaminABeta, lblIngredient2Thiamine, lblIngredient2Riboflavin, lblIngredient2NicotinicAcid, lblIngredient2Pyridoxyne, lblIngredient2FolicAcid, lblIngredient2VitaminB12, lblIngredient2VitaminC, lblIngredient2Moisture, lblIngredient2Sodium, lblIngredient2Pottasium, lblIngredient2Zinc);

                if (ingredient2.DisplayImage != string.Empty)
                {
                    imgPath2 = GetImagePath("Ingredients") + "\\" + ingredient2.Id + ".jpg";
                }
                else
                {
                    imgPath2 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient2.ImageSource = imgPath2;
                ImgIngredient2.ImageName = lblIngredientName2.Text;
                ImgIngredient2.ItemID = ingredient2.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient2.ToolTip = DisplayName2;
                }
                else
                {
                    ImgIngredient2.ToolTip = IngredientName2;
                }
                chkClear2.Visibility = Visibility.Visible;

                FillGraph(ingredient2, ingredientno);

            }
            if (ingredientno == 3)
            {
                ingredient3 = ingredientList[2];
                IngredientName3 = Convert.ToString(ingredient3.Name);
                DisplayName3 = Convert.ToString(ingredient3.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName3.Length > 13)
                        lblIngredientName3.Text = DisplayName3.Substring(0, 13) + "..";
                    else
                        lblIngredientName3.Text = DisplayName3;
                }
                else
                {
                    if (IngredientName3.Length > 13)
                        lblIngredientName3.Text = IngredientName3.Substring(0, 13) + "..";
                    else
                        lblIngredientName3.Text = IngredientName3;
                }

                FillIngredient(ingredient3.Id, ingredient3.Name, lblIngredient3Calorie, lblIngredient3Protein, lblIngredient3Carbohydrates, lblIngredient3Fat, lblIngredient3Fiber, lblIngredient3Iron, lblIngredient3Calcium, lblIngredient3Phosphorus, lblIngredient3VitaminAReti, lblIngredient3VitaminABeta, lblIngredient3Thiamine, lblIngredient3Riboflavin, lblIngredient3NicotinicAcid, lblIngredient3Pyridoxyne, lblIngredient3FolicAcid, lblIngredient3VitaminB12, lblIngredient3VitaminC, lblIngredient3Moisture, lblIngredient3Sodium, lblIngredient3Pottasium, lblIngredient3Zinc);

                if (ingredient3.DisplayImage != string.Empty)
                {
                    imgPath3 = GetImagePath("Ingredients") + "\\" + ingredient3.Id + ".jpg";
                }
                else
                {
                    imgPath3 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient3.ImageSource = imgPath3;
                ImgIngredient3.ImageName = lblIngredientName3.Text;
                ImgIngredient3.ItemID = ingredient3.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient3.ToolTip = DisplayName3;
                }
                else
                {
                    ImgIngredient3.ToolTip = IngredientName3;
                }
                chkClear3.Visibility = Visibility.Visible;

                FillGraph(ingredient3, ingredientno);

            }
            if (ingredientno == 4)
            {
                ingredient4 = ingredientList[3];
                IngredientName4 = Convert.ToString(ingredient4.Name);
                DisplayName4 = Convert.ToString(ingredient4.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName4.Length > 13)
                        lblIngredientName4.Text = DisplayName4.Substring(0, 13) + "..";
                    else
                        lblIngredientName4.Text = DisplayName4;
                }
                else
                {
                    if (IngredientName4.Length > 13)
                        lblIngredientName4.Text = IngredientName4.Substring(0, 13) + "..";
                    else
                        lblIngredientName4.Text = IngredientName4;
                }

                FillIngredient(ingredient4.Id, ingredient4.Name, lblIngredient4Calorie, lblIngredient4Protein, lblIngredient4Carbohydrates, lblIngredient4Fat, lblIngredient4Fiber, lblIngredient4Iron, lblIngredient4Calcium, lblIngredient4Phosphorus, lblIngredient4VitaminAReti, lblIngredient4VitaminABeta, lblIngredient4Thiamine, lblIngredient4Riboflavin, lblIngredient4NicotinicAcid, lblIngredient4Pyridoxyne, lblIngredient4FolicAcid, lblIngredient4VitaminB12, lblIngredient4VitaminC, lblIngredient4Moisture, lblIngredient4Sodium, lblIngredient4Pottasium, lblIngredient4Zinc);

                if (ingredient4.DisplayImage != string.Empty)
                {
                    imgPath4 = GetImagePath("Ingredients") + "\\" + ingredient4.Id + ".jpg";
                }
                else
                {
                    imgPath4 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient4.ImageSource = imgPath4;
                ImgIngredient4.ImageName = lblIngredientName4.Text;
                ImgIngredient4.ItemID = ingredient4.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient4.ToolTip = DisplayName4;
                }
                else
                {
                    ImgIngredient4.ToolTip = IngredientName4;
                }
                chkClear4.Visibility = Visibility.Visible;

                FillGraph(ingredient4, ingredientno);

            }
            if (ingredientno == 5)
            {
                ingredient5 = ingredientList[4];
                IngredientName5 = Convert.ToString(ingredient5.Name);
                DisplayName5 = Convert.ToString(ingredient5.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName5.Length > 13)
                        lblIngredientName5.Text = DisplayName5.Substring(0, 13) + "..";
                    else
                        lblIngredientName5.Text = DisplayName5;
                }
                else
                {
                    if (IngredientName5.Length > 13)
                        lblIngredientName5.Text = IngredientName5.Substring(0, 13) + "..";
                    else
                        lblIngredientName5.Text = IngredientName5;
                }

                FillIngredient(ingredient5.Id, ingredient5.Name, lblIngredient5Calorie, lblIngredient5Protein, lblIngredient5Carbohydrates, lblIngredient5Fat, lblIngredient5Fiber, lblIngredient5Iron, lblIngredient5Calcium, lblIngredient5Phosphorus, lblIngredient5VitaminAReti, lblIngredient5VitaminABeta, lblIngredient5Thiamine, lblIngredient5Riboflavin, lblIngredient5NicotinicAcid, lblIngredient5Pyridoxyne, lblIngredient5FolicAcid, lblIngredient5VitaminB12, lblIngredient5VitaminC, lblIngredient5Moisture, lblIngredient5Sodium, lblIngredient5Pottasium, lblIngredient5Zinc);

                if (ingredient5.DisplayImage != string.Empty)
                {
                    imgPath5 = GetImagePath("Ingredients") + "\\" + ingredient5.Id + ".jpg";
                }
                else
                {
                    imgPath5 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient5.ImageSource = imgPath5;
                ImgIngredient5.ImageName = lblIngredientName5.Text;
                ImgIngredient5.ItemID = ingredient5.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient5.ToolTip = DisplayName5;
                }
                else
                {
                    ImgIngredient5.ToolTip = IngredientName5;
                }
                chkClear5.Visibility = Visibility.Visible;

                FillGraph(ingredient5, ingredientno);
            }
        }

        private void FillIngredientList(int IngredientID,int IngredientNo)
        {
            string imgSource = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string imgPath1 = string.Empty;
            string imgPath2 = string.Empty;
            string imgPath3 = string.Empty;
            string imgPath4 = string.Empty;
            string imgPath5 = string.Empty;

            Ingredient ingredient1 = new Ingredient();
            Ingredient ingredient2 = new Ingredient();
            Ingredient ingredient3 = new Ingredient();
            Ingredient ingredient4 = new Ingredient();
            Ingredient ingredient5 = new Ingredient();

            if (IngredientNo == 1)
            {
                ingredient1 = IngredientManager.GetItem(IngredientID);
                ingredientList[0] = ingredient1;
                IngredientName1 = Convert.ToString(ingredient1.Name);
                DisplayName1 = Convert.ToString(ingredient1.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName1.Length > 13)
                        lblIngredientName1.Text = DisplayName1.Substring(0, 13) + "..";
                    else
                        lblIngredientName1.Text = DisplayName1;
                }
                else
                {
                    if (IngredientName1.Length > 13)
                        lblIngredientName1.Text = IngredientName1.Substring(0, 13) + "..";
                    else
                        lblIngredientName1.Text = IngredientName1;
                }


                FillIngredient(ingredient1.Id, ingredient1.Name, lblIngredient1Calorie, lblIngredient1Protein, lblIngredient1Carbohydrates, lblIngredient1Fat, lblIngredient1Fiber, lblIngredient1Iron, lblIngredient1Calcium, lblIngredient1Phosphorus, lblIngredient1VitaminAReti, lblIngredient1VitaminABeta, lblIngredient1Thiamine, lblIngredient1Riboflavin, lblIngredient1NicotinicAcid, lblIngredient1Pyridoxyne, lblIngredient1FolicAcid, lblIngredient1VitaminB12, lblIngredient1VitaminC, lblIngredient1Moisture, lblIngredient1Sodium, lblIngredient1Pottasium, lblIngredient1Zinc);

                if (ingredient1.DisplayImage != string.Empty)
                {
                    imgPath1 = GetImagePath("Ingredients") + "\\" + ingredient1.Id + ".jpg";
                }
                else
                {
                    imgPath1 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient1.ImageSource = imgPath1;
                ImgIngredient1.ImageName = lblIngredientName1.Text;
                ImgIngredient1.ItemID = ingredient1.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient1.ToolTip = DisplayName1;
                }
                else
                {
                    ImgIngredient1.ToolTip = IngredientName1;
                }
                chkClear1.Visibility = Visibility.Visible;

                FillGraph(ingredient1, IngredientNo);

            }
            if (IngredientNo == 2)
            {
                ingredient2 = IngredientManager.GetItem(IngredientID);
                ingredientList[1] = ingredient2;
                IngredientName2 = Convert.ToString(ingredient2.Name);
                DisplayName2 = Convert.ToString(ingredient2.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName2.Length > 13)
                        lblIngredientName2.Text = DisplayName2.Substring(0, 13) + "..";
                    else
                        lblIngredientName2.Text = DisplayName2;
                }
                else
                {
                    if (IngredientName2.Length > 13)
                        lblIngredientName2.Text = IngredientName2.Substring(0, 13) + "..";
                    else
                        lblIngredientName2.Text = IngredientName2;
                }

                FillIngredient(ingredient2.Id, ingredient2.Name, lblIngredient2Calorie, lblIngredient2Protein, lblIngredient2Carbohydrates, lblIngredient2Fat, lblIngredient2Fiber, lblIngredient2Iron, lblIngredient2Calcium, lblIngredient2Phosphorus, lblIngredient2VitaminAReti, lblIngredient2VitaminABeta, lblIngredient2Thiamine, lblIngredient2Riboflavin, lblIngredient2NicotinicAcid, lblIngredient2Pyridoxyne, lblIngredient2FolicAcid, lblIngredient2VitaminB12, lblIngredient2VitaminC, lblIngredient2Moisture, lblIngredient2Sodium, lblIngredient2Pottasium, lblIngredient2Zinc);

                if (ingredient2.DisplayImage != string.Empty)
                {
                    imgPath2 = GetImagePath("Ingredients") + "\\" + ingredient2.Id + ".jpg";
                }
                else
                {
                    imgPath2 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient2.ImageSource = imgPath2;
                ImgIngredient2.ImageName = lblIngredientName2.Text;
                ImgIngredient2.ItemID = ingredient2.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient2.ToolTip = DisplayName2;
                }
                else
                {
                    ImgIngredient2.ToolTip = IngredientName2;
                }
                chkClear2.Visibility = Visibility.Visible;

                FillGraph(ingredient2, IngredientNo);

            }
            if (IngredientNo == 3)
            {
                ingredient3 = IngredientManager.GetItem(IngredientID);
                ingredientList[2] = ingredient3;
                IngredientName3 = Convert.ToString(ingredient3.Name);
                DisplayName3 = Convert.ToString(ingredient3.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName3.Length > 13)
                        lblIngredientName3.Text = DisplayName3.Substring(0, 13) + "..";
                    else
                        lblIngredientName3.Text = DisplayName3;
                }
                else
                {
                    if (IngredientName3.Length > 13)
                        lblIngredientName3.Text = IngredientName3.Substring(0, 13) + "..";
                    else
                        lblIngredientName3.Text = IngredientName3;
                }

                FillIngredient(ingredient3.Id, ingredient3.Name, lblIngredient3Calorie, lblIngredient3Protein, lblIngredient3Carbohydrates, lblIngredient3Fat, lblIngredient3Fiber, lblIngredient3Iron, lblIngredient3Calcium, lblIngredient3Phosphorus, lblIngredient3VitaminAReti, lblIngredient3VitaminABeta, lblIngredient3Thiamine, lblIngredient3Riboflavin, lblIngredient3NicotinicAcid, lblIngredient3Pyridoxyne, lblIngredient3FolicAcid, lblIngredient3VitaminB12, lblIngredient3VitaminC, lblIngredient3Moisture, lblIngredient3Sodium, lblIngredient3Pottasium, lblIngredient3Zinc);

                if (ingredient3.DisplayImage != string.Empty)
                {
                    imgPath3 = GetImagePath("Ingredients") + "\\" + ingredient3.Id + ".jpg";
                }
                else
                {
                    imgPath3 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient3.ImageSource = imgPath3;
                ImgIngredient3.ImageName = lblIngredientName3.Text;
                ImgIngredient3.ItemID = ingredient3.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient3.ToolTip = DisplayName3;
                }
                else
                {
                    ImgIngredient3.ToolTip = IngredientName3;
                }
                chkClear3.Visibility = Visibility.Visible;

                FillGraph(ingredient3, IngredientNo);

            }
            if (IngredientNo == 4)
            {
                ingredient4 = IngredientManager.GetItem(IngredientID);
                ingredientList[3] = ingredient4;
                IngredientName4 = Convert.ToString(ingredient4.Name);
                DisplayName4 = Convert.ToString(ingredient4.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName4.Length > 13)
                        lblIngredientName4.Text = DisplayName4.Substring(0, 13) + "..";
                    else
                        lblIngredientName4.Text = DisplayName4;
                }
                else
                {
                    if (IngredientName4.Length > 13)
                        lblIngredientName4.Text = IngredientName4.Substring(0, 13) + "..";
                    else
                        lblIngredientName4.Text = IngredientName4;
                }

                FillIngredient(ingredient4.Id, ingredient4.Name, lblIngredient4Calorie, lblIngredient4Protein, lblIngredient4Carbohydrates, lblIngredient4Fat, lblIngredient4Fiber, lblIngredient4Iron, lblIngredient4Calcium, lblIngredient4Phosphorus, lblIngredient4VitaminAReti, lblIngredient4VitaminABeta, lblIngredient4Thiamine, lblIngredient4Riboflavin, lblIngredient4NicotinicAcid, lblIngredient4Pyridoxyne, lblIngredient4FolicAcid, lblIngredient4VitaminB12, lblIngredient4VitaminC, lblIngredient4Moisture, lblIngredient4Sodium, lblIngredient4Pottasium, lblIngredient4Zinc);

                if (ingredient4.DisplayImage != string.Empty)
                {
                    imgPath4 = GetImagePath("Ingredients") + "\\" + ingredient4.Id + ".jpg";
                }
                else
                {
                    imgPath4 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient4.ImageSource = imgPath4;
                ImgIngredient4.ImageName = lblIngredientName4.Text;
                ImgIngredient4.ItemID = ingredient4.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient4.ToolTip = DisplayName4;
                }
                else
                {
                    ImgIngredient4.ToolTip = IngredientName4;
                }
                chkClear4.Visibility = Visibility.Visible;

                FillGraph(ingredient4, IngredientNo);

            }
            if (IngredientNo == 5)
            {
                ingredient5 = IngredientManager.GetItem(IngredientID);
                ingredientList[4] = ingredient5;
                IngredientName5 = Convert.ToString(ingredient5.Name);
                DisplayName5 = Convert.ToString(ingredient5.DisplayName);
                if (chkRegionalNames.IsChecked == true)
                {
                    if (DisplayName5.Length > 13)
                        lblIngredientName5.Text = DisplayName5.Substring(0, 13) + "..";
                    else
                        lblIngredientName5.Text = DisplayName5;
                }
                else
                {
                    if (IngredientName5.Length > 13)
                        lblIngredientName5.Text = IngredientName5.Substring(0, 13) + "..";
                    else
                        lblIngredientName5.Text = IngredientName5;
                }

                FillIngredient(ingredient5.Id, ingredient5.Name, lblIngredient5Calorie, lblIngredient5Protein, lblIngredient5Carbohydrates, lblIngredient5Fat, lblIngredient5Fiber, lblIngredient5Iron, lblIngredient5Calcium, lblIngredient5Phosphorus, lblIngredient5VitaminAReti, lblIngredient5VitaminABeta, lblIngredient5Thiamine, lblIngredient5Riboflavin, lblIngredient5NicotinicAcid, lblIngredient5Pyridoxyne, lblIngredient5FolicAcid, lblIngredient5VitaminB12, lblIngredient5VitaminC, lblIngredient5Moisture, lblIngredient5Sodium, lblIngredient5Pottasium, lblIngredient5Zinc);

                if (ingredient5.DisplayImage != string.Empty)
                {
                    imgPath5 = GetImagePath("Ingredients") + "\\" + ingredient5.Id + ".jpg";
                }
                else
                {
                    imgPath5 = imgSource + "Images\\NoPreview.png";
                }
                ImgIngredient5.ImageSource = imgPath5;
                ImgIngredient5.ImageName = lblIngredientName5.Text;
                ImgIngredient5.ItemID = ingredient5.Id;
                if (chkRegionalNames.IsChecked == true)
                {
                    ImgIngredient5.ToolTip = DisplayName5;
                }
                else
                {
                    ImgIngredient5.ToolTip = IngredientName5;
                }
                chkClear5.Visibility = Visibility.Visible;

                FillGraph(ingredient5, IngredientNo);
            }
        }

        private string GetUnitName(int unitID)
        {
            string strunit = string.Empty;
            DataTable dtableUnit = new DataTable();

            dtableUnit = XMLServices.GetXMLData(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\ServeUnit.xml",  3);

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
                ingredientList[0] = null;
                IngredientName1 = string.Empty;
                DisplayName2 = string.Empty;
                lblIngredientName1.Text = string.Empty;
                lblIngredient1Calorie.Content = string.Empty;
                lblIngredient1Protein.Content = string.Empty;
                lblIngredient1Carbohydrates.Content = string.Empty;
                lblIngredient1Fat.Content = string.Empty;
                lblIngredient1Fiber.Content = string.Empty;
                lblIngredient1Iron.Content = string.Empty;
                lblIngredient1Calcium.Content = string.Empty;

                lblIngredient1Phosphorus.Content = string.Empty;
                lblIngredient1VitaminAReti.Content = string.Empty;
                lblIngredient1VitaminABeta.Content = string.Empty;
                lblIngredient1Thiamine.Content = string.Empty;
                lblIngredient1Riboflavin.Content = string.Empty;
                lblIngredient1NicotinicAcid.Content = string.Empty;
                lblIngredient1Pyridoxyne.Content = string.Empty;
                lblIngredient1FolicAcid.Content = string.Empty;
                lblIngredient1VitaminB12.Content = string.Empty;
                lblIngredient1VitaminC.Content = string.Empty;

                lblIngredient1Moisture.Content = string.Empty;
                lblIngredient1Sodium.Content = string.Empty;
                lblIngredient1Pottasium.Content = string.Empty;
                lblIngredient1Zinc.Content = string.Empty;

                ImgIngredient1.ImageSource = string.Empty;
                ImgIngredient1.ImageName = string.Empty;
                ImgIngredient1.ItemID = 0;
                chkClear1.IsChecked = false;
                chkClear1.Visibility = Visibility.Hidden;

                FillGraph(null, 1);
            }
            if (chkClear2.IsChecked == true)
            {
                ingredientList[1] = null;
                IngredientName2 = string.Empty;
                DisplayName2 = string.Empty;
                lblIngredientName2.Text = string.Empty;
                lblIngredient2Calorie.Content = string.Empty;
                lblIngredient2Protein.Content = string.Empty;
                lblIngredient2Carbohydrates.Content = string.Empty;
                lblIngredient2Fat.Content = string.Empty;
                lblIngredient2Fiber.Content = string.Empty;
                lblIngredient2Iron.Content = string.Empty;
                lblIngredient2Calcium.Content = string.Empty;

                lblIngredient2Phosphorus.Content = string.Empty;
                lblIngredient2VitaminAReti.Content = string.Empty;
                lblIngredient2VitaminABeta.Content = string.Empty;
                lblIngredient2Thiamine.Content = string.Empty;
                lblIngredient2Riboflavin.Content = string.Empty;
                lblIngredient2NicotinicAcid.Content = string.Empty;
                lblIngredient2Pyridoxyne.Content = string.Empty;
                lblIngredient2FolicAcid.Content = string.Empty;
                lblIngredient2VitaminB12.Content = string.Empty;
                lblIngredient2VitaminC.Content = string.Empty;

                lblIngredient2Moisture.Content = string.Empty;
                lblIngredient2Sodium.Content = string.Empty;
                lblIngredient2Pottasium.Content = string.Empty;
                lblIngredient2Zinc.Content = string.Empty;

                ImgIngredient2.ImageSource = string.Empty;
                ImgIngredient2.ImageName = string.Empty;
                ImgIngredient2.ItemID = 0;
                chkClear2.IsChecked = false;
                chkClear2.Visibility = Visibility.Hidden;

                FillGraph(null, 2);
            }
            if (chkClear3.IsChecked == true)
            {
                ingredientList[2] = null;
                IngredientName3 = string.Empty;
                DisplayName3 = string.Empty;
                lblIngredientName3.Text = string.Empty;
                lblIngredient3Calorie.Content = string.Empty;
                lblIngredient3Protein.Content = string.Empty;
                lblIngredient3Carbohydrates.Content = string.Empty;
                lblIngredient3Fat.Content = string.Empty;
                lblIngredient3Fiber.Content = string.Empty;
                lblIngredient3Iron.Content = string.Empty;
                lblIngredient3Calcium.Content = string.Empty;

                lblIngredient3Phosphorus.Content = string.Empty;
                lblIngredient3VitaminAReti.Content = string.Empty;
                lblIngredient3VitaminABeta.Content = string.Empty;
                lblIngredient3Thiamine.Content = string.Empty;
                lblIngredient3Riboflavin.Content = string.Empty;
                lblIngredient3NicotinicAcid.Content = string.Empty;
                lblIngredient3Pyridoxyne.Content = string.Empty;
                lblIngredient3FolicAcid.Content = string.Empty;
                lblIngredient3VitaminB12.Content = string.Empty;
                lblIngredient3VitaminC.Content = string.Empty;

                lblIngredient3Moisture.Content = string.Empty;
                lblIngredient3Sodium.Content = string.Empty;
                lblIngredient3Pottasium.Content = string.Empty;
                lblIngredient3Zinc.Content = string.Empty;

                ImgIngredient3.ImageSource = string.Empty;
                ImgIngredient3.ImageName = string.Empty;
                ImgIngredient3.ItemID = 0;
                chkClear3.IsChecked = false;
                chkClear3.Visibility = Visibility.Hidden;

                FillGraph(null, 3);
            }
            if (chkClear4.IsChecked == true)
            {
                ingredientList[3] = null;
                IngredientName4 = string.Empty;
                DisplayName4 = string.Empty;
                lblIngredientName4.Text = string.Empty;
                lblIngredient4Calorie.Content = string.Empty;
                lblIngredient4Protein.Content = string.Empty;
                lblIngredient4Carbohydrates.Content = string.Empty;
                lblIngredient4Fat.Content = string.Empty;
                lblIngredient4Fiber.Content = string.Empty;
                lblIngredient4Iron.Content = string.Empty;
                lblIngredient4Calcium.Content = string.Empty;

                lblIngredient4Phosphorus.Content = string.Empty;
                lblIngredient4VitaminAReti.Content = string.Empty;
                lblIngredient4VitaminABeta.Content = string.Empty;
                lblIngredient4Thiamine.Content = string.Empty;
                lblIngredient4Riboflavin.Content = string.Empty;
                lblIngredient4NicotinicAcid.Content = string.Empty;
                lblIngredient4Pyridoxyne.Content = string.Empty;
                lblIngredient4FolicAcid.Content = string.Empty;
                lblIngredient4VitaminB12.Content = string.Empty;
                lblIngredient4VitaminC.Content = string.Empty;

                lblIngredient4Moisture.Content = string.Empty;
                lblIngredient4Sodium.Content = string.Empty;
                lblIngredient4Pottasium.Content = string.Empty;
                lblIngredient4Zinc.Content = string.Empty;

                ImgIngredient4.ImageSource = string.Empty;
                ImgIngredient4.ImageName = string.Empty;
                ImgIngredient4.ItemID = 0;
                chkClear4.IsChecked = false;
                chkClear4.Visibility = Visibility.Hidden;

                FillGraph(null, 4);
            }
            if (chkClear5.IsChecked == true)
            {
                ingredientList[4] = null;
                IngredientName5 = string.Empty;
                DisplayName5 = string.Empty;
                lblIngredientName5.Text = string.Empty;
                lblIngredient5Calorie.Content = string.Empty;
                lblIngredient5Protein.Content = string.Empty;
                lblIngredient5Carbohydrates.Content = string.Empty;
                lblIngredient5Fat.Content = string.Empty;
                lblIngredient5Fiber.Content = string.Empty;
                lblIngredient5Iron.Content = string.Empty;
                lblIngredient5Calcium.Content = string.Empty;

                lblIngredient5Phosphorus.Content = string.Empty;
                lblIngredient5VitaminAReti.Content = string.Empty;
                lblIngredient5VitaminABeta.Content = string.Empty;
                lblIngredient5Thiamine.Content = string.Empty;
                lblIngredient5Riboflavin.Content = string.Empty;
                lblIngredient5NicotinicAcid.Content = string.Empty;
                lblIngredient5Pyridoxyne.Content = string.Empty;
                lblIngredient5FolicAcid.Content = string.Empty;
                lblIngredient5VitaminB12.Content = string.Empty;
                lblIngredient5VitaminC.Content = string.Empty;

                lblIngredient5Moisture.Content = string.Empty;
                lblIngredient5Sodium.Content = string.Empty;
                lblIngredient5Pottasium.Content = string.Empty;
                lblIngredient5Zinc.Content = string.Empty;

                ImgIngredient5.ImageSource = string.Empty;
                ImgIngredient5.ImageName = string.Empty;
                ImgIngredient5.ItemID = 0;
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
                    CreateNutrientGraph(Calorie, "Calorie", IngredientName, GraphType, CalorieGraphLayout);
                    break;
                case (int)NutritionType.Protein:
                    CreateNutrientGraph(Protein, "Protein", IngredientName, GraphType, ProteinGraphLayout);
                    break;
                case (int)NutritionType.Carbohydrates:
                    CreateNutrientGraph(Carbo, "CarboHydrates", IngredientName, GraphType, CarboHydratesGraphLayout);
                    break;
                case (int)NutritionType.Fat:
                    CreateNutrientGraph(FAT, "Fat", IngredientName, GraphType, FATGraphLayout);
                    break;
                case (int)NutritionType.Fiber:
                    CreateNutrientGraph(Fibre, "Fibre", IngredientName, GraphType, FibreGraphLayout);
                    break;
                case (int)NutritionType.Moisture:
                    CreateNutrientGraph(Moisture, "Moisture", IngredientName, GraphType, MoistureGraphLayout);
                    break;
                case (int)NutritionType.Iron:
                    CreateNutrientGraph(Iron, "Iron", IngredientName, GraphType, IronGraphLayout);
                    break;
                case (int)NutritionType.Calcium:
                    CreateNutrientGraph(Calcium, "Calcium", IngredientName, GraphType, CalciumGraphLayout);
                    break;
                case (int)NutritionType.Phosphorus:
                    CreateNutrientGraph(Phosphorus, "Phosphorus", IngredientName, GraphType, PhosphrousGraphLayout);
                    break;
                case (int)NutritionType.Sodium:
                    CreateNutrientGraph(Sodium, "Sodium", IngredientName, GraphType, SodiumGraphLayout);
                    break;
                case (int)NutritionType.Pottasuim:
                    CreateNutrientGraph(Pottasuim, "Pottasuim", IngredientName, GraphType, PottasiumGraphLayout);
                    break;
                case (int)NutritionType.Zinc:
                    CreateNutrientGraph(Zinc, "Zinc", IngredientName, GraphType, ZincGraphLayout);
                    break;
                case (int)NutritionType.Retinol:
                    CreateNutrientGraph(Retinol, "Retinol", IngredientName, GraphType, RetinolGraphLayout);
                    break;
                case (int)NutritionType.BetaCarotene:
                    CreateNutrientGraph(BetaCarotine, "BetaCarotine", IngredientName, GraphType, BetaGraphLayout);
                    break;
                case (int)NutritionType.Thiamine:
                    CreateNutrientGraph(Thiamine, "Thiamine", IngredientName, GraphType, ThaimineGraphLayout);
                    break;
                case (int)NutritionType.Riboflavin:
                    CreateNutrientGraph(Ribiflavin, "Ribiflavin", IngredientName, GraphType, RiboflavinGraphLayout);
                    break;
                case (int)NutritionType.NicotinicAcid:
                    CreateNutrientGraph(Naicin, "Nicotinic Acid", IngredientName, GraphType, NaicinGraphLayout);
                    break;
                case (int)NutritionType.Pyridoxine:
                    CreateNutrientGraph(Pyridoxine, "Pyridoxine", IngredientName, GraphType, PyrodoxineGraphLayout);
                    break;
                case (int)NutritionType.FolicAcid:
                    CreateNutrientGraph(FolicAcid, "Folic Acid", IngredientName, GraphType, FolicAcidGraphLayout);
                    break;
                case (int)NutritionType.VitaminC:
                    CreateNutrientGraph(VitamineC, "VitamineC", IngredientName, GraphType, VitaminCGraphLayout);
                    break;
                default:
                    CreateNutrientGraph(Calorie, "Calorie", IngredientName, GraphType, CalorieGraphLayout);
                    break;
            }
        }

        public void InitailizeList()
        {
            for (int i = 0; i < 5; i++)
            {
                ingredientList.Insert(i, null);
            }
        }

        #endregion

        #region Graph

        public void CreateNutrientGraph(double[] NutrientValue, string NutrienName, string[] IngredientName, int Mode, System.Windows.Controls.Grid GraphLayout)
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
                    string AxisXLabel = IngredientName[i].ToString();
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

            Keyboard.Focus(cboIngredientCategory);
            Classes.CommonFunctions.FillFoodCategory(cboIngredientCategory);

            LoadImages();
            InitailizeControls();
            RepopulateData();
            InitailizeList();

            CalorieGraphLayout.Visibility = Visibility.Visible;
        }

        private void spSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtSearch.Text.Trim() != string.Empty || cboIngredientCategory.SelectedIndex > 0)
            {
                FillSearchList();

                int Result = Classes.CommonFunctions.Convert2Int(Convert.ToString(cboIngredientCategory.SelectedIndex)); ;
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
                        int CurrentIngredientID = ((Ingredient)SearchList.Items[SearchList.SelectedIndex]).Id;
                        GetIngredient(CurrentIngredientID);
                    }
                    else
                    {
                        ShowMessages("Please Select an Ingredient to Add");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);
                
            }
        }

        private void ImgIngredient1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ingredientList.Count >= 1)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Ingredient;
                if (ingredientList[0] != null)
                {
                    imagePreview.ItemID = ingredientList[0].Id;
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

        private void ImgIngredient2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ingredientList.Count >= 2)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Ingredient;
                if (ingredientList[1] != null)
                {
                    imagePreview.ItemID = ingredientList[1].Id;
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

        private void ImgIngredient3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ingredientList.Count >= 3)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Ingredient;
                if (ingredientList[2] != null)
                {
                    imagePreview.ItemID = ingredientList[2].Id;
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

        private void ImgIngredient4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ingredientList.Count >= 4)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Ingredient;
                if (ingredientList[3] != null)
                {
                    imagePreview.ItemID = ingredientList[3].Id;
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

        private void ImgIngredient5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ingredientList.Count >= 5)
            {
                ImagePreview imagePreview = new ImagePreview();
                imagePreview.DisplayItem = ItemType.Ingredient;
                if (ingredientList[4] != null)
                {
                    imagePreview.ItemID = ingredientList[4].Id;
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
                    AlertBox.Show("Please Select an Ingredient to Clear", "", AlertType.Exclamation, AlertButtons.OK);
                    return;
                }
                if (ingredientList.Count != 0)
                {
                    bool result = AlertBox.Show("Do You Want to Clear the Selected Ingredient", "", AlertType.Exclamation, AlertButtons.YESNO);
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
                        int CurrentIngredientID = ((Ingredient)SearchList.Items[SearchList.SelectedIndex]).Id;
                        GetIngredient(CurrentIngredientID);
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
                lblIngredientName1.Text = DisplayName1;
                lblIngredientName2.Text = DisplayName2;
                lblIngredientName3.Text = DisplayName3;
                lblIngredientName4.Text = DisplayName4;
                lblIngredientName5.Text = DisplayName5;

                ImgIngredient1.ImageName = DisplayName1;
                ImgIngredient2.ImageName = DisplayName2;
                ImgIngredient3.ImageName = DisplayName3;
                ImgIngredient4.ImageName = DisplayName4;
                ImgIngredient5.ImageName = DisplayName5;

                gvCol2.CellTemplate = this.FindResource("displayNameTemplate") as DataTemplate;
            }
            else
            {
                lblIngredientName1.Text = IngredientName1;
                lblIngredientName2.Text = IngredientName2;
                lblIngredientName3.Text = IngredientName3;
                lblIngredientName4.Text = IngredientName4;

                ImgIngredient1.ImageName = IngredientName1;
                ImgIngredient2.ImageName = IngredientName2;
                ImgIngredient3.ImageName = IngredientName3;
                ImgIngredient4.ImageName = IngredientName4;
                ImgIngredient5.ImageName = IngredientName5;

                gvCol2.CellTemplate = this.FindResource("nameTemplate") as DataTemplate;
            }
        }

        private void chkRegionalNames_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkRegionalNames.IsChecked == true)
            {
                lblIngredientName1.Text = DisplayName1;
                lblIngredientName2.Text = DisplayName2;
                lblIngredientName3.Text = DisplayName3;
                lblIngredientName4.Text = DisplayName4;

                ImgIngredient1.ImageName = DisplayName1;
                ImgIngredient2.ImageName = DisplayName2;
                ImgIngredient3.ImageName = DisplayName3;
                ImgIngredient4.ImageName = DisplayName4;
                ImgIngredient5.ImageName = DisplayName5;

                gvCol2.CellTemplate = this.FindResource("displayNameTemplate") as DataTemplate;
            }
            else
            {
                lblIngredientName1.Text = IngredientName1;
                lblIngredientName2.Text = IngredientName2;
                lblIngredientName3.Text = IngredientName3;
                lblIngredientName4.Text = IngredientName4;

                ImgIngredient1.ImageName = IngredientName1;
                ImgIngredient2.ImageName = IngredientName2;
                ImgIngredient3.ImageName = IngredientName3;
                ImgIngredient4.ImageName = IngredientName4;
                ImgIngredient5.ImageName = IngredientName5;

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

        private void ImgIngredient1_DragEnter(object sender, DragEventArgs e)
        {
           
        }

        private void ImgIngredient1_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentIngredientID = ImgIngredient1.ItemID;

                    if (CurrentIngredientID == 0)
                    {
                        int IngredientID = ((BONutrition.Ingredient)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (IngredientID > 0)
                        {
                            GetIngredient(IngredientID, 1);
                        }
                    }
                    else
                    {
                        ShowMessages("Ingredient Exists , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Ingredient to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);
                
            }
        }

        private void ImgIngredient2_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void ImgIngredient2_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentIngredientID = ImgIngredient2.ItemID;

                    if (CurrentIngredientID == 0)
                    {
                        int IngredientID = ((BONutrition.Ingredient)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (IngredientID > 0)
                        {
                            GetIngredient(IngredientID, 2);
                        }
                    }
                    else
                    {
                        ShowMessages("Cannot Add Ingredient , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Ingredient to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);
                
            }
        }

        private void ImgIngredient3_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void ImgIngredient3_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentIngredientID = ImgIngredient3.ItemID;

                    if (CurrentIngredientID == 0)
                    {
                        int IngredientID = ((BONutrition.Ingredient)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (IngredientID > 0)
                        {
                            GetIngredient(IngredientID, 3);
                        }
                    }
                    else
                    {
                        ShowMessages("Cannot Add Ingredient , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Ingredient to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);
                
            }
        }

        private void ImgIngredient4_DragEnter(object sender, DragEventArgs e)
        {
           
        }

        private void ImgIngredient4_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentIngredientID = ImgIngredient4.ItemID;

                    if (CurrentIngredientID == 0)
                    {
                        int IngredientID = ((BONutrition.Ingredient)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (IngredientID > 0)
                        {
                            GetIngredient(IngredientID, 4);
                        }
                    }
                    else
                    {
                        ShowMessages("Cannot Add Ingredient , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Ingredient to Add");
                }
            }
            catch (Exception ex)
            {
                ShowMessages(ex.Message);
                
            }
        }

        private void ImgIngredient5_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void ImgIngredient5_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int CurrentIngredientID = ImgIngredient5.ItemID;

                    if (CurrentIngredientID == 0)
                    {
                        int IngredientID = ((BONutrition.Ingredient)(((System.Windows.Controls.ContentControl)(SelectedItem)).Content)).Id;

                        if (IngredientID > 0)
                        {
                            GetIngredient(IngredientID, 5);
                        }
                    }
                    else
                    {
                        ShowMessages("Cannot Add Ingredient , Please Clear and Add");
                    }
                }
                else
                {
                    ShowMessages("Please Select an Ingredient to Add");
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

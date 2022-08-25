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
using Microsoft.Win32;
using BONutrition;
using BLNutrition;
using NutritionV1.Common.Classes;
using NutritionV1.Classes;
using NutritionV1.Enums;
using System.Configuration;
using System.Collections;
using Visifire.Charts;
using Visifire.Commons;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for DishSearch.xaml
    /// </summary>
    public partial class DishSearch : Page
    {

        #region Varaibles

        int DISHID;
        Dish dish = new Dish();
        List<Dish> dishList = new List<Dish>();
        string SearchString = string.Empty;        

        Visifire.Charts.Chart chart;
        SaveFileDialog SaveDlg = new SaveFileDialog();
        string imagePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Images\\";

        private bool IsMyGridVisible = false;
        private bool IsYourGridVisible = false;
        private bool IsSearch = false;

        int Result;
        int[] Results = new int[4];
        double planWeight = 0;

        #endregion

        #region Constructor

        public DishSearch()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region Methods

        private void WriteRegistry(int viewIndex)
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini.IniWriteValue("SWVIEW", "VIEW", viewIndex.ToString());
            ini = null;
        }

        private int ReadRegistry()
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            Result = CommonFunctions.Convert2Int(ini.IniReadValue("SWVIEW", "VIEW"));
            ini = null;
            return Result;
        }

        private void WriteSearchRegistry(int[] Result)
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini.IniWriteValue("SWREGIONAL", "REGIONAL", Result[0].ToString());
            ini.IniWriteValue("SWDCATEGORY", "DCATEGORY", Result[1].ToString());
            ini.IniWriteValue("SWHNUTRIENT", "HNUTRIENT", Result[2].ToString());
            ini.IniWriteValue("SWLNUTRIENT", "LNUTRIENT", Result[3].ToString());
            ini = null;
        }

        private int[] ReadSearchRegistry()
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            Results[0] = CommonFunctions.Convert2Int(ini.IniReadValue("SWREGIONAL", "REGIONAL"));
            Results[1] = CommonFunctions.Convert2Int(ini.IniReadValue("SWDCATEGORY", "DCATEGORY"));
            Results[2] = CommonFunctions.Convert2Int(ini.IniReadValue("SWHNUTRIENT", "HNUTRIENT"));
            Results[3] = CommonFunctions.Convert2Int(ini.IniReadValue("SWLNUTRIENT", "LNUTRIENT"));
            ini = null;
            return Results;
        }

        private void WriteRegionalRegistry(int regionalIndex)
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini.IniWriteValue("SWREGIONAL", "REGIONAL", regionalIndex.ToString());
            ini = null;
        }

        private int ReadRegionalRegistry()
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini = null;
            Result = CommonFunctions.Convert2Int(ini.IniReadValue("SWREGIONAL", "REGIONAL"));
            return Result;
        }

        private void ChangeView(int viewIndex)
        {
            switch (viewIndex)
            {
                case 1:
                    //IconGrid1.Visibility = Visibility.Visible;
                    //IconGrid2.Visibility = Visibility.Hidden;
                    IconGrid.Visibility = Visibility.Visible;
                    DataGrid.Visibility = Visibility.Hidden;
                    SetThemeOnClick(IconView);
                    break;
                case 2:
                    //IconGrid1.Visibility = Visibility.Hidden;
                    //IconGrid2.Visibility = Visibility.Hidden;
                    IconGrid.Visibility = Visibility.Hidden;
                    DataGrid.Visibility = Visibility.Visible;
                    SetThemeOnClick(GridView);
                    break;
                default:
                    //IconGrid1.Visibility = Visibility.Visible;
                    //IconGrid2.Visibility = Visibility.Hidden;
                    IconGrid.Visibility = Visibility.Visible;
                    DataGrid.Visibility = Visibility.Hidden;
                    SetThemeOnClick(IconView);
                    break;
            }
        }

        private void MyGridAnimation(string AnimatioName)
        {            
            //Storyboard gridAnimation = (Storyboard)FindResource(AnimatioName);
            //gridAnimation.Begin(this);

            if (AnimatioName == "ExpandMyGrid")
            {
                IsMyGridVisible = true;
                MyGrid.Visibility = Visibility.Visible;
            }
            else if (AnimatioName == "CollapseMyGrid")
            {
                IsMyGridVisible = false;
                MyGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void YourGridAnimation(string AnimatioName)
        {            
            //Storyboard gridAnimation = (Storyboard)FindResource(AnimatioName);
            //gridAnimation.Begin(this);
            if (AnimatioName == "ExpandYourGrid")
            {
                IsYourGridVisible = true;
                YourGrid.Visibility = Visibility.Visible;
            }
            else if (AnimatioName == "CollapseYourGrid")
            {
                IsYourGridVisible = false;
                YourGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            GridView.Style = (Style)apps.SetStyle["HomeBarStyle"];
            IconView.Style = (Style)apps.SetStyle["HomeBarStyle"];            
            ((NutritionV1.MasterPage)(Window.GetWindow(this))).mnuTop.Visibility = Visibility.Visible;
            chkRegionalNames.Visibility = Visibility.Collapsed;
        }

        private void SetThemeOnClick(System.Windows.Shapes.Rectangle SelRectangle)
        {
            App apps = (App)Application.Current;

            GridView.Style = (Style)apps.SetStyle["HomeBarStyle"];
            IconView.Style = (Style)apps.SetStyle["HomeBarStyle"];
            SelRectangle.Style = (Style)apps.SetStyle["HomeBarSelectStyle"];
        }        
        
        private void RepopulateData()
        {
            if (Results.Length > 0)
            {
                if (Results[0].ToString() != "0")
                {
                    chkRegionalNames.IsChecked = true;
                }
                if (Results[1].ToString() != "0")
                {
                    cboDishCategory.SelectedIndex = Results[1];
                    IsSearch = true;
                }

                if (Results[2].ToString() != "0")
                {
                    rdHighestin.IsChecked = true;
                    cbHighestin.SelectedIndex = Results[2];
                    IsSearch = true;
                }
                if (Results[3].ToString() != "0")
                {
                    rdLowestin.IsChecked = true;
                    cbLowestin.SelectedIndex = Results[3];
                    IsSearch = true;
                }

                if (IsSearch == true)
                {
                    FillSearchList();
                }
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

        private void ShowMessage()
        {
            AlertBox.Show("Select a Dish Category or Type Search Criteria", "", AlertType.Information, AlertButtons.OK);
        }

        //private void FillData(ImageDisplay imgDisplay, int ControlID, string ControlName, string dishPath, string dishName, int dishID)
        //{
        //    string imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
        //    imgPath = dishPath;
        //    imgDisplay.Visibility = Visibility.Visible;
        //    imgDisplay.SetThemes = true;
        //    imgDisplay.ImageSource = imgPath;
        //    if (chkRegionalNames.IsChecked == true)
        //    {
        //        if (dishName.Length > 13)
        //            imgDisplay.ImageName = dishName.Substring(0, 13) + "..";
        //        else
        //            imgDisplay.ImageName = dishName;
        //    }
        //    else
        //    {
        //        if (dishName.Length > 13)
        //            imgDisplay.ImageName = dishName.Substring(0, 13) + "..";
        //        else
        //            imgDisplay.ImageName = dishName;
        //    }
        //    imgDisplay.ImageID = dishID;
        //    imgDisplay.ItemName = dishName;
        //    imgDisplay.ItemID = dishID;
        //    imgDisplay.ControlName = ControlName;
        //    imgDisplay.ControlID = ControlID;
        //    imgDisplay.ToolTip = dishName;
        //    imgDisplay.Cursor = Cursors.Hand;
        //}
        
        private void LoadDishNutrients(int dishID)
        {
            List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
            List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
            List<IngredientNutrients> ingredientNutriList = new List<IngredientNutrients>();
            try
            {
                lblNutrients.Content = "Main Nutrient Values per "+ planWeight + " gms";
                ingredientNutriList = IngredientNutrientsManager.GetNutrientsList(dishID, 1);
                if (ingredientNutriList != null)
                {
                    lvNutrient.ItemsSource = ingredientNutriList;
                    lvNutrient.Items.Refresh();
                }

                lblFattyAcids.Content = "Fatty Acid Values per " + planWeight + " gms";
                ingredientAminoList = IngredientAminoAcidManager.GetAminoAcidsList(dishID, 1);
                if (ingredientAminoList != null)
                {
                    lvAmino.ItemsSource = ingredientAminoList;
                    lvAmino.Items.Refresh();
                }

                lblAminoAcids.Content = "Amino Acid Values per " + planWeight + " gms";
                ingredientFattyList = IngredientFattyAcidManager.GetFattyAcidsList(dishID, 1);
                if (ingredientFattyList != null)
                {
                    lvFattyAcid.ItemsSource = ingredientFattyList;
                    lvFattyAcid.Items.Refresh();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                ingredientAminoList = null;
                ingredientFattyList = null;
                ingredientNutriList = null;
            }
        }

        private void FillDishDetails(int dishID, string dishName, int ControlID, string ControlName)
        {
            dish = DishManager.GetItem(dishID);
            if (dish != null)
            {
                txtStandardWeight.Text = Convert.ToString(dish.StandardWeight);
                txtNutrientHeader.Text = "Nutrient Information per " + dish.StandardWeight + " gms";
                //string[] servingPlan = new string[3];
                //if (dish.StandardWeight > 0)
                //{
                //    servingPlan[0] = Convert.ToString(dish.StandardWeight);
                //    if (dish.StandardWeight1 > 0)
                //    {
                //        servingPlan[1] = Convert.ToString(dish.StandardWeight1);
                //        if (dish.StandardWeight2 > 0)
                //        {
                //            servingPlan[2] = Convert.ToString(dish.StandardWeight2);
                //        }
                //    }
                //}

                //if (servingPlan.Length > 0)
                //{
                //    cboPlan.ItemsSource = servingPlan;
                //    cboPlan.SelectedIndex = 0;
                //}
        
                imgDishDisplay.SetThemes = true;

                txtDishID.Text = Convert.ToString(dish.Id);
                txtDishName.Text = dish.Name;

                if (dish.DisplayImage != "")
                {
                    string imgPath = GetImagePath("Dishes") + "\\" + dish.Id + ".jpg";

                    if (File.Exists(imgPath))
                    {
                        imgDishDisplay.ImageSource = imgPath;
                    }
                    else
                    {
                        imgDishDisplay.ImageSource = string.Empty;
                    }
                }
                else
                {
                    imgDishDisplay.ImageSource = string.Empty;
                }

                if (chkRegionalNames.IsChecked == true)
                {
                    if (dish.DisplayName.Length > 30)
                        //imgDishDisplay.ImageName = dish.DisplayName.Substring(0, 21) + "..";
                        txtName.Text = dish.DisplayName.Substring(0, 30) + "..";
                    else
                        //imgDishDisplay.ImageName = dish.DisplayName;
                        txtName.Text =  dish.DisplayName;
                }
                else
                {
                    if (dish.Name.Length > 30)
                        //imgDishDisplay.ImageName = dish.Name.Substring(0, 21) + "..";
                        txtName.Text = dish.Name.Substring(0, 30) + "..";
                    else
                        //imgDishDisplay.ImageName = dish.Name;
                        txtName.Text = dish.Name;
                }

                txtComments.Text = Convert.ToString(dish.Comments);
                txtControlID.Text = Convert.ToString(ControlID);
                txtControlName.Text = ControlName;

                planWeight = dish.StandardWeight;
                txtCalorie.Text = Convert.ToString(Math.Round((dish.Calorie / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Calorie);                
                txtCalorieUnit.Text = "Kcal";
                txtProtein.Text = Convert.ToString(Math.Round((dish.Protien / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Protien);                
                txtProteinUnit.Text = "gm";
                txtFAT.Text = Convert.ToString(Math.Round((dish.FAT / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.FAT);                
                txtFATUnit.Text = "gm";
                txtFibre.Text = Convert.ToString(Math.Round((dish.Fibre / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Fibre);                
                txtFibreUnit.Text = "gm";
                txtCarboHydrates.Text = Convert.ToString(Math.Round((dish.CarboHydrates / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.CarboHydrates);                
                txtCarboHydratesUnit.Text = "gm";
                //txtMoisture.Text = "0";
                //txtMoistureUnit.Text = "gm";
                txtIron.Text = Convert.ToString(Math.Round((dish.Iron / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Iron);                
                txtIronUnit.Text = "mg";
                txtCalcium.Text = Convert.ToString(Math.Round((dish.Calcium / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Calcium);                
                txtCalciumUnit.Text = "mg";
                txtPhosphorus.Text = Convert.ToString(Math.Round((dish.Phosphorus / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Phosphorus);                
                txtPhosphorusUnit.Text = "mg";
                txtVitaminARetinol.Text = Convert.ToString(Math.Round((dish.VitaminARetinol / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.VitaminARetinol);                
                txtVitaminARetinolUnit.Text = "mcg";
                txtVitaminABetaCarotene.Text = Convert.ToString(Math.Round((dish.VitaminABetaCarotene / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.VitaminABetaCarotene);                
                txtVitaminABetaCaroteneUnit.Text = "mcg";
                txtThiamine.Text = Convert.ToString(Math.Round((dish.Thiamine / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Thiamine);                
                txtThiamineUnit.Text = "mg";
                txtRiboflavin.Text = Convert.ToString(Math.Round((dish.Riboflavin / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Riboflavin);                
                txtRiboflavinUnit.Text = "mg";
                txtNicotinicAcid.Text = Convert.ToString(Math.Round((dish.NicotinicAcid / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.NicotinicAcid);                
                txtNicotinicAcidUnit.Text = "mg";
                txtPyridoxine.Text = Convert.ToString(Math.Round((dish.Pyridoxine / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.Pyridoxine);                
                txtPyridoxineUnit.Text = "mg";
                txtFolicAcid.Text = Convert.ToString(Math.Round((dish.FolicAcid / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.FolicAcid);                
                txtFolicAcidUnit.Text = "mcg";
                txtVitaminC.Text = Convert.ToString(Math.Round((dish.VitaminC / 100) * dish.StandardWeight, 2)); //Convert.ToString(dish.VitaminC);
                txtVitaminCUnit.Text = "mg";
                txtPreparation.Text = Convert.ToString(dish.DishRecipe);

                txtCalorie100.Text = Convert.ToString(dish.Calorie);
                txtProtein100.Text = Convert.ToString(dish.Protien);
                txtFAT100.Text = Convert.ToString(dish.FAT);
                txtFibre100.Text = Convert.ToString(dish.Fibre);
                txtCarboHydrates100.Text = Convert.ToString(dish.CarboHydrates);
                txtIron100.Text = Convert.ToString(dish.Iron);
                txtCalcium100.Text = Convert.ToString(dish.Calcium);
                txtPhosphorus100.Text = Convert.ToString(dish.Phosphorus);
                txtVitaminARetinol100.Text = Convert.ToString(dish.VitaminARetinol);
                txtVitaminABetaCarotene100.Text = Convert.ToString(dish.VitaminABetaCarotene);
                txtThiamine100.Text = Convert.ToString(dish.Thiamine);
                txtRiboflavin100.Text = Convert.ToString(dish.Riboflavin);
                txtNicotinicAcid100.Text = Convert.ToString(dish.NicotinicAcid);
                txtPyridoxine100.Text = Convert.ToString(dish.Pyridoxine);
                txtFolicAcid100.Text = Convert.ToString(dish.FolicAcid);
                txtVitaminC100.Text = Convert.ToString(dish.VitaminC);

                FillIngredientsList(dish.Id);
                CreateGraph(dish.Id);
            }
        }

        private void FillIngredientsList(int dishID)
        {
            List<DishIngredient> dishIngredientList = new List<DishIngredient>();
            dishIngredientList = DishIngredientManager.GetIngredientDisplayList(dishID);
            if (dishIngredientList != null)
            {                
                lvIngredientList.ItemsSource = dishIngredientList;                
            }
        }

        public void FillDropDownFromXML()
        {
            try
            {
                XMLServices.GetXMLDataByLanguage(cbHighestin, AppDomain.CurrentDomain.BaseDirectory + "\\XML\\Nutrition.xml",  1);
                XMLServices.GetXMLDataByLanguage(cbLowestin, AppDomain.CurrentDomain.BaseDirectory + "\\XML\\Nutrition.xml",  1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void DisableControls()
        {
            MyGrid.Visibility = Visibility.Hidden;
            YourGrid.Visibility = Visibility.Hidden;            
            //txtPrevious1.Visibility = Visibility.Hidden;
            //txtPrevious2.Visibility = Visibility.Hidden;
            //txtNext1.Visibility = Visibility.Hidden;
            //txtNext2.Visibility = Visibility.Hidden;

            //imgPrevious1.Visibility = Visibility.Hidden;
            //imgPrevious2.Visibility = Visibility.Hidden;
            //imgNext1.Visibility = Visibility.Hidden;
            //imgNext2.Visibility = Visibility.Hidden;
        }

        public void EnableControls()
        {
            //txtPrevious1.Visibility = Visibility.Visible;
            //txtPrevious2.Visibility = Visibility.Visible;
            //txtNext1.Visibility = Visibility.Visible;
            //txtNext2.Visibility = Visibility.Visible;

            //imgPrevious1.Visibility = Visibility.Visible;
            //imgPrevious2.Visibility = Visibility.Visible;
            //imgNext1.Visibility = Visibility.Visible;
            //imgNext2.Visibility = Visibility.Visible;
        }

        public void InitailizeControls()
        { 
            imgDishDisplay.ImageSource = string.Empty;
            imgDishDisplay.ImageName = string.Empty;
            txtName.Text = string.Empty;
            txtComments.Text = string.Empty;

            txtCalorie.Text = string.Empty;
            txtProtein.Text = string.Empty;
            txtFAT.Text = string.Empty;
            txtFibre.Text = string.Empty;
            txtCarboHydrates.Text = string.Empty;
            txtIron.Text = string.Empty;
            txtCalcium.Text = string.Empty;
            txtPhosphorus.Text = string.Empty;
            txtVitaminARetinol.Text = string.Empty;
            txtVitaminABetaCarotene.Text = string.Empty;
            txtThiamine.Text = string.Empty;
            txtRiboflavin.Text = string.Empty;
            txtNicotinicAcid.Text = string.Empty;
            txtPyridoxine.Text = string.Empty;
            txtFolicAcid.Text = string.Empty;
            txtVitaminC.Text = string.Empty;

            txtPreparation.Text = string.Empty;

            lvNutrient.ItemsSource = null;
            lvFattyAcid.ItemsSource = null;
            lvAmino.ItemsSource = null;
        }

        private void LoadImages()
        {
            if (File.Exists(imagePath + "Nutrients.png"))
            {
                imgNormal.Source = new BitmapImage(new Uri(imagePath + "GridView.png"));
            }
            else
            {
                imgNormal.Source = new BitmapImage(new Uri(imagePath + "Vista.png"));
            }

            if (File.Exists(imagePath + "Vista.png"))
            {
                imgIcon.Source = new BitmapImage(new Uri(imagePath + "IconView.png"));
            }
            else
            {
                imgIcon.Source = new BitmapImage(new Uri(imagePath + "Vista.png"));
            }            
        }

        private void FillSearchList()
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                SearchString = string.Empty;

                SearchString = "Where 1=1 ";

                if (cboDishCategory.SelectedIndex > 0)
                {
                    SearchString = SearchString + "And DishCategoryID =" + cboDishCategory.SelectedIndex + " ";
                }

                if (txtSearch.Text != string.Empty)
                {
                    if (chkRegionalNames.IsChecked == true)
                        SearchString = SearchString + "And DisplayName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                    else
                        SearchString = SearchString + "And DishName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                }

                if (cbHighestin.SelectedIndex > 0)
                {
                    switch (cbHighestin.SelectedIndex)
                    {
                        case (int)NutritionType.Calorie:
                            SearchString = SearchString + " And Calorie <> 0 order by Calorie desc";
                            break;
                        case (int)NutritionType.Protein:
                            SearchString = SearchString + " And Protien <> 0 order by Protien desc";
                            break;
                        case (int)NutritionType.Carbohydrates:
                            SearchString = SearchString + " And Carbohydrates <> 0 order by Carbohydrates desc";
                            break;
                        case (int)NutritionType.Fat:
                            SearchString = SearchString + " And Fat <> 0 order by Fat desc";
                            break;
                        case (int)NutritionType.Fiber:
                            SearchString = SearchString + " And Fibre <>0 order by Fibre desc";
                            break;
                        case (int)NutritionType.Moisture:
                            SearchString = SearchString + " And Moisture <>0 order by Moisture desc";
                            break;
                        case (int)NutritionType.Iron:
                            SearchString = SearchString + " And Iron <> 0 order by Iron desc";
                            break;
                        case (int)NutritionType.Calcium:
                            SearchString = SearchString + " And Calcium <> 0 order by Calcium desc";
                            break;
                        case (int)NutritionType.Phosphorus:
                            SearchString = SearchString + " And Phosphorus <> 0 order by Phosphorus desc";
                            break;
                        case (int)NutritionType.Sodium:
                            SearchString = SearchString + " And Sodium <> 0 order by Sodium desc";
                            break;
                        case (int)NutritionType.Pottasuim:
                            SearchString = SearchString + " And Pottasium <> 0 order by Pottasium desc";
                            break;
                        case (int)NutritionType.Zinc:
                            SearchString = SearchString + " And Zinc <> 0 order by Zinc desc";
                            break;
                        case (int)NutritionType.Retinol:
                            SearchString = SearchString + " And VitaminARetinol <> 0 order by VitaminARetinol desc";
                            break;
                        case (int)NutritionType.BetaCarotene:
                            SearchString = SearchString + " And VitaminABetaCarotene <> 0 order by VitaminABetaCarotene desc";
                            break;
                        case (int)NutritionType.Thiamine:
                            SearchString = SearchString + " And Thiamine <> 0 order by Thiamine desc";
                            break;
                        case (int)NutritionType.Riboflavin:
                            SearchString = SearchString + " And Riboflavin <>0 order by Riboflavin desc";
                            break;
                        case (int)NutritionType.NicotinicAcid:
                            SearchString = SearchString + " And NicotinicAcid <> 0 order by NicotinicAcid desc";
                            break;
                        case (int)NutritionType.Pyridoxine:
                            SearchString = SearchString + " And Pyridoxine <> 0 order by Pyridoxine desc";
                            break;
                        case (int)NutritionType.FolicAcid:
                            SearchString = SearchString + " And FolicAcid <>0 order by FolicAcid desc";
                            break;
                        case (int)NutritionType.VitaminC:
                            SearchString = SearchString + " And VitaminC <> 0 order by VitaminC desc";
                            break;
                    }
                }

                if (cbLowestin.SelectedIndex >= 0)
                {
                    switch (cbLowestin.SelectedIndex)
                    {
                        case (int)NutritionType.Calorie:
                            SearchString = SearchString + " And Calorie <> 0  order by Calorie asc";
                            break;
                        case (int)NutritionType.Protein:
                            SearchString = SearchString + " And Protien <> 0  order by Protien asc";
                            break;
                        case (int)NutritionType.Carbohydrates:
                            SearchString = SearchString + " And CarboHydrates <> 0  order by CarboHydrates asc";
                            break;
                        case (int)NutritionType.Fat:
                            SearchString = SearchString + " And Fat <> 0  order by Fat asc";
                            break;
                        case (int)NutritionType.Fiber:
                            SearchString = SearchString + " And Fibre <> 0  order by Fibre asc";
                            break;
                        case (int)NutritionType.Moisture:
                            SearchString = SearchString + " And Moisture <> 0  order by Moisture asc";
                            break;
                        case (int)NutritionType.Iron:
                            SearchString = SearchString + " And Iron <> 0  order by Iron asc";
                            break;
                        case (int)NutritionType.Calcium:
                            SearchString = SearchString + " And Calcium <> 0 order by Calcium asc";
                            break;
                        case (int)NutritionType.Phosphorus:
                            SearchString = SearchString + " And Phosphorus <> 0 order by Phosphorus asc";
                            break;
                        case (int)NutritionType.Sodium:
                            SearchString = SearchString + " And Sodium <> 0  order by Sodium asc";
                            break;
                        case (int)NutritionType.Pottasuim:
                            SearchString = SearchString + " And Pottasium <> 0 order by Pottasium asc";
                            break;
                        case (int)NutritionType.Zinc:
                            SearchString = SearchString + " And Zinc <> 0 order by Zinc asc";
                            break;
                        case (int)NutritionType.Retinol:
                            SearchString = SearchString + " And VitaminARetinol <> 0 order by VitaminARetinol asc";
                            break;
                        case (int)NutritionType.BetaCarotene:
                            SearchString = SearchString + " And VitaminABetaCarotene <> 0 order by VitaminABetaCarotene asc";
                            break;
                        case (int)NutritionType.Thiamine:
                            SearchString = SearchString + " And Thiamine <> 0 order by Thiamine asc";
                            break;
                        case (int)NutritionType.Riboflavin:
                            SearchString = SearchString + " And Riboflavin <>0 order by Riboflavin asc";
                            break;
                        case (int)NutritionType.NicotinicAcid:
                            SearchString = SearchString + " And NicotinicAcid <> 0 order by NicotinicAcid asc";
                            break;
                        case (int)NutritionType.Pyridoxine:
                            SearchString = SearchString + " And Pyridoxine <> 0 order by Pyridoxine asc";
                            break;
                        case (int)NutritionType.FolicAcid:
                            SearchString = SearchString + " And FolicAcid <>0 order by FolicAcid asc";
                            break;
                        case (int)NutritionType.VitaminC:
                            SearchString = SearchString + " And VitaminC <> 0 order by VitaminC asc";
                            break;
                    }
                }
                
                dishList = DishManager.GetList(SearchString);
                FillData();
            }
        }

        private void FillIconData()
        {
            //if (dishList.Count > 0)
            //{
            //    //IconGrid1.Visibility = Visibility.Visible;
            //    //IconGrid2.Visibility = Visibility.Hidden;
            //    //lblTotalItems1.Text = "Total Items : " + Convert.ToString(dishList.Count);
            //    //lblTotalItems2.Text = "Total Items : " + Convert.ToString(dishList.Count);

            //    if (dishList.Count > 50)
            //    {
            //        EnableControls();
            //    }

            //    for (int i = 1; i <= dishList.Count; i++)
            //    {
            //        ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

            //        if (chkRegionalNames.IsChecked == true)
            //        {
            //            FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].DisplayName, dishList[i - 1].Id);
            //        }
            //        else
            //        {
            //            FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].Name, dishList[i - 1].Id);
            //        }
            //    }

            //    //if (chkRegionalNames.IsChecked == true)
            //    //{
            //    //    SetRegionalName();
            //    //}
            //    //else
            //    //{
            //    //    SetEnglishName();
            //    //}
            //}

            if (dishList.Count > 0)
            {                
                lblTotalItemsList.Text = "Total Items : " + Convert.ToString(dishList.Count);
                lvDishes.ItemsSource = dishList;                
            }
        }

        private void FillGridData()
        {
            if (dishList.Count > 0)
            {                
                lblTotalItemsGrid.Text = "Total Items : " + Convert.ToString(dishList.Count);
                lvDataGrid.ItemsSource = dishList;
                lvDataGrid.SelectedIndex = 0;
                lvDataGrid.ScrollIntoView(lvDataGrid.SelectedItem);
            }
        }

        private void FillData()
        {
            int View = ReadRegistry();

            switch (View)
            {
                case 1:
                    FillIconData();
                    break;
                case 2:
                    FillGridData();
                    break;
                default:
                    FillIconData();
                    break;
            }
        }

        private void IncludeAyurveda()
        {
            if (ConfigurationManager.AppSettings["IncludeAyurvedic"] == "0")
            {
                if (ConfigurationManager.AppSettings["IncludeAyurvedic"] == "0")
                {
                    
                }
            }
        }                
                
        public void PrintDish(ListView lv)
        {
            try
            {
                if (lv.SelectedIndex >= 0)
                {
                    DISHID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Ingredient)lv.Items[lv.SelectedIndex]).Id));
                    ReportViewer dishReport = new ReportViewer();
                    dishReport.DisplayItem = ItemType.Dish;
                    dishReport.ReportType = (int)ReportItem.Dish;
                    dishReport.ItemID = DISHID;
                    dishReport.DishID = DISHID;
                    dishReport.Plan = 100;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        dishReport.IsRegional = true;
                    }
                    else
                    {
                        dishReport.IsRegional = false;
                    }
                    dishReport.Owner = Application.Current.MainWindow;
                    dishReport.ShowDialog();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PrintDish(int dishID)
        {
            try
            {
                ReportViewer dishReport = new ReportViewer();
                dishReport.DisplayItem = ItemType.Dish;
                dishReport.ReportType = (int)ReportItem.Dish;
                dishReport.ItemID = dishID;
                dishReport.DishID = dishID;
                dishReport.Plan = (float)planWeight;
                if (chkRegionalNames.IsChecked == true)
                {
                    dishReport.IsRegional = true;
                }
                else
                {
                    dishReport.IsRegional = false;
                }
                dishReport.Owner = Application.Current.MainWindow;
                dishReport.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DisplayDetails(int dishID)
        {
            try
            {
                MyGridAnimation("ExpandMyGrid");

                InitailizeControls();
                FillDishDetails(dishID, string.Empty, 0, string.Empty);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetRegionalName()
        {
            int View = ReadRegistry();

            switch (View)
            {
                case 1:
                    if (dishList.Count > 0)
                    {
                        if (dishList.Count > 50)
                        {
                            EnableControls();
                        }

                        for (int i = 1; i <= dishList.Count; i++)
                        {
                            ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                            if (chkRegionalNames.IsChecked == true)
                            {
                                //FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].DisplayName, dishList[i - 1].Id);
                            }
                            else
                            {
                                //FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].Name, dishList[i - 1].Id);
                            }
                        }
                    }
                    break;
                case 2:
                    lvGeneralDataCol2.CellTemplate = this.FindResource("DisplayNameTemplate") as DataTemplate;
                    break;
                default:
                    if (dishList.Count > 0)
                    {
                        if (dishList.Count > 50)
                        {
                            EnableControls();
                        }

                        for (int i = 1; i <= dishList.Count; i++)
                        {
                            ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                            if (chkRegionalNames.IsChecked == true)
                            {
                                //FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].DisplayName, dishList[i - 1].Id);
                            }
                            else
                            {
                                //FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].Name, dishList[i - 1].Id);
                            }
                        }
                    }
                    break;
            }

            WriteRegionalRegistry(1);
        }

        private void SetEnglishName()
        {
            int View = ReadRegistry();

            switch (View)
            {
                case 1:
                    if (dishList.Count > 0)
                    {
                        if (dishList.Count > 50)
                        {
                            EnableControls();
                        }

                        for (int i = 1; i <= dishList.Count; i++)
                        {
                            ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                            if (chkRegionalNames.IsChecked == true)
                            {
                                //FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].DisplayName, dishList[i - 1].Id);
                            }
                            else
                            {
                                //FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].Name, dishList[i - 1].Id);
                            }
                        }
                    }
                    break;
                case 2:
                    lvGeneralDataCol2.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
                    break;
                default:
                    if (dishList.Count > 0)
                    {
                        if (dishList.Count > 50)
                        {
                            EnableControls();
                        }

                        for (int i = 1; i <= dishList.Count; i++)
                        {
                            ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                            if (chkRegionalNames.IsChecked == true)
                            {
                                //FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].DisplayName, dishList[i - 1].Id);
                            }
                            else
                            {
                                //FillData(img, i, "imgDisplay" + i, dishList[i - 1].DisplayImage, dishList[i - 1].Name, dishList[i - 1].Id);
                            }
                        }
                    }
                    break;
            }

            WriteRegionalRegistry(0);
        }

        #endregion

        #region Graph

        public void CreateGraph(int dishID)
        {
            try
            {
                CreateGramGraph(dishID);
                CreateMilliGramGraph(dishID);
                CreateMicroGramGraph(dishID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void CreateGramGraph(int dishID)
        {
            chart = new Visifire.Charts.Chart();
            chart.Cursor = Cursors.Hand;
            chart.ScrollingEnabled = false;

            Visifire.Charts.Axis axisX = new Visifire.Charts.Axis();
            chart.AxesX.Add(axisX);

            Visifire.Charts.Axis axisY = new Visifire.Charts.Axis();
            chart.AxesY.Add(axisY);

            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = RenderAs.Bar;

            Visifire.Charts.DataPoint dataPoint;

            DataSet dsDish = new DataSet();
            dsDish = DishManager.GetGramItemSearch(dishID);
            if (dsDish.Tables.Count > 0)
            {
                if (dsDish.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        dataPoint = new Visifire.Charts.DataPoint();
                        string AxisXLabel = Convert.ToString(dsDish.Tables[0].Columns[i].ColumnName);
                        if (AxisXLabel.Length > 7)
                            AxisXLabel = AxisXLabel.Substring(0, 7) + "..";
                        dataPoint.AxisXLabel = AxisXLabel;
                        dataPoint.YValue = CommonFunctions.Convert2Double(Convert.ToString(dsDish.Tables[0].Rows[0][i]));
                        dataSeries.DataPoints.Add(dataPoint);
                    }
                }
            }

            chart.Series.Add(dataSeries);
            GramGraphLayout.Children.Add(chart);

        }

        public void CreateMilliGramGraph(int dishID)
        {
            chart = new Visifire.Charts.Chart();
            chart.Cursor = Cursors.Hand;
            chart.ScrollingEnabled = false;

            Visifire.Charts.Axis axisX = new Visifire.Charts.Axis();
            chart.AxesX.Add(axisX);

            Visifire.Charts.Axis axisY = new Visifire.Charts.Axis();
            chart.AxesY.Add(axisY);

            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = RenderAs.Bar;

            Visifire.Charts.DataPoint dataPoint;

            DataSet dsDish = new DataSet();
            dsDish = DishManager.GetMilliItemSearch(dishID);
            if (dsDish.Tables.Count > 0)
            {
                if (dsDish.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        dataPoint = new Visifire.Charts.DataPoint();
                        string AxisXLabel = Convert.ToString(dsDish.Tables[0].Columns[i].ColumnName);
                        if (AxisXLabel.Length > 7)
                            AxisXLabel = AxisXLabel.Substring(0, 7) + "..";
                        dataPoint.AxisXLabel = AxisXLabel;
                        dataPoint.YValue = CommonFunctions.Convert2Double(Convert.ToString(dsDish.Tables[0].Rows[0][i]));
                        dataSeries.DataPoints.Add(dataPoint);
                    }
                }
            }

            chart.Series.Add(dataSeries);
            MilliGramGraphLayout.Children.Add(chart);

        }

        public void CreateMicroGramGraph(int dishID)
        {
            chart = new Visifire.Charts.Chart();
            chart.Cursor = Cursors.Hand;
            chart.ScrollingEnabled = false;

            Visifire.Charts.Axis axisX = new Visifire.Charts.Axis();
            chart.AxesX.Add(axisX);

            Visifire.Charts.Axis axisY = new Visifire.Charts.Axis();
            chart.AxesY.Add(axisY);

            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = RenderAs.Bar;

            Visifire.Charts.DataPoint dataPoint;

            DataSet dsDish = new DataSet();
            dsDish = DishManager.GetMicroItemSearch(dishID);

            if (dsDish.Tables.Count > 0)
            {
                if (dsDish.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        dataPoint = new Visifire.Charts.DataPoint();
                        string AxisXLabel = Convert.ToString(dsDish.Tables[0].Columns[i].ColumnName);
                        if (AxisXLabel.Length > 7)
                            AxisXLabel = AxisXLabel.Substring(0, 7) + "..";
                        dataPoint.AxisXLabel = AxisXLabel;
                        dataPoint.YValue = CommonFunctions.Convert2Double(Convert.ToString(dsDish.Tables[0].Rows[0][i]));
                        dataSeries.DataPoints.Add(dataPoint);
                    }
                }
            }

            chart.Series.Add(dataSeries);
            MicroGramGraphLayout.Children.Add(chart);

        }
        
        #endregion

        #region Events
       
        private void lblMyClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyGridAnimation("CollapseMyGrid");
        }

        private void lblYourClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            YourGridAnimation("CollapseYourGrid");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            int View = ReadRegistry();
            ChangeView(View);

            Keyboard.Focus(cboDishCategory);
            FillDropDownFromXML();
            Classes.CommonFunctions.FillDishCategory(cboDishCategory);

            rdHighestin.IsChecked = true;
            cbHighestin.IsEnabled = true;
            rdLowestin.IsChecked = false;
            cbLowestin.IsEnabled = false;

            LoadImages();
            DisableControls();
            IncludeAyurveda();
            
            ReadSearchRegistry();
            RepopulateData();
        }

        private void lblMyPrevious_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                bool isStart = false;
                if (lvDishes.SelectedIndex > 0)
                {
                    lvDishes.SelectedItem = lvDishes.Items[lvDishes.SelectedIndex - 1];
                }
                else
                {
                    isStart = true;
                }

                if (lvDishes.SelectedItem != null && isStart == false)
                {
                    DISHID = ((BONutrition.Dish)(lvDishes.SelectedItem)).Id;
                    if (DISHID > 0)
                    {
                        DisplayDetails(DISHID);
                    }
                }
            }
        }

        private void lblMyNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                bool isEnd = false;
                if ((lvDishes.Items.Count - 1) > lvDishes.SelectedIndex)
                {
                    lvDishes.SelectedItem = lvDishes.Items[lvDishes.SelectedIndex + 1];
                }
                else
                {
                    isEnd = true;
                }

                if (lvDishes.SelectedItem != null && isEnd == false)
                {
                    DISHID = ((BONutrition.Dish)(lvDishes.SelectedItem)).Id;
                    if (DISHID > 0)
                    {
                        DisplayDetails(DISHID);
                    }
                }
            }
        }
       
        private void rdHighestin_Checked(object sender, RoutedEventArgs e)
        {
            cbHighestin.IsEnabled = true;
            cbLowestin.IsEnabled = false;
            cbLowestin.SelectedIndex = 0;
        }

        private void rdLowestin_Checked(object sender, RoutedEventArgs e)
        {
            cbHighestin.IsEnabled = false;
            cbLowestin.IsEnabled = true;
            cbHighestin.SelectedIndex = 0;
        }

        private void cbHighestin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                SearchString = string.Empty;

                if (cbHighestin.SelectedIndex > 0)
                {
                    switch (cbHighestin.SelectedIndex)
                    {
                        case (int)NutritionType.Calorie:
                            SearchString = "Where Calorie <> 0  order by Calorie desc";
                            break;
                        case (int)NutritionType.Protein:
                            SearchString = "Where Protien <> 0  order by Protien desc";
                            break;
                        case (int)NutritionType.Carbohydrates:
                            SearchString = "Where CarboHydrates <> 0  order by CarboHydrates desc";
                            break;
                        case (int)NutritionType.Fat:
                            SearchString = "Where Fat <> 0  order by Fat desc";
                            break;
                        case (int)NutritionType.Fiber:
                            SearchString = "Where Fibre <> 0  order by Fibre desc";
                            break;
                        case (int)NutritionType.Moisture:
                            SearchString = "Where Moisture <> 0  order by Moisture desc";
                            break;
                        case (int)NutritionType.Iron:
                            SearchString = "Where Iron <> 0  order by Iron desc";
                            break;
                        case (int)NutritionType.Calcium:
                            SearchString = "Where Calcium <> 0 order by Calcium desc";
                            break;
                        case (int)NutritionType.Phosphorus:
                            SearchString = "Where Phosphorus <> 0 order by Phosphorus desc";
                            break;
                        case (int)NutritionType.Sodium:
                            SearchString = "Where Sodium <> 0 order by Sodium desc";
                            break;
                        case (int)NutritionType.Pottasuim:
                            SearchString = "Where Pottasium <> 0 order by Pottasium desc";
                            break;
                        case (int)NutritionType.Zinc:
                            SearchString = "Where Zinc <> 0 order by Zinc desc";
                            break;
                        case (int)NutritionType.Retinol:
                            SearchString = "Where VitaminARetinol <> 0 order by VitaminARetinol desc";
                            break;
                        case (int)NutritionType.BetaCarotene:
                            SearchString = "Where VitaminABetaCarotene <> 0 order by VitaminABetaCarotene desc";
                            break;
                        case (int)NutritionType.Thiamine:
                            SearchString = "Where Thiamine <> 0 order by Thiamine desc";
                            break;
                        case (int)NutritionType.Riboflavin:
                            SearchString = "Where Riboflavin <> 0 order by Riboflavin desc";
                            break;
                        case (int)NutritionType.NicotinicAcid:
                            SearchString = "Where NicotinicAcid <> 0 order by NicotinicAcid desc";
                            break;
                        case (int)NutritionType.Pyridoxine:
                            SearchString = "Where Pyridoxine <> 0 order by Pyridoxine desc";
                            break;
                        case (int)NutritionType.FolicAcid:
                            SearchString = "Where FolicAcid <> 0 order by FolicAcid desc";
                            break;
                        case (int)NutritionType.VitaminC:
                            SearchString = "Where VitaminC <> 0 order by VitaminC desc";
                            break;
                    }
                }
                else
                {

                }
            }
        }

        private void cbLowestin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                SearchString = string.Empty;

                if (cbLowestin.SelectedIndex > 0)
                {
                    switch (cbLowestin.SelectedIndex)
                    {
                        case (int)NutritionType.Calorie:
                            SearchString = "Where Calorie <> 0  order by Calorie asc";
                            break;
                        case (int)NutritionType.Protein:
                            SearchString = "Where Protien <> 0  order by Protien asc";
                            break;
                        case (int)NutritionType.Carbohydrates:
                            SearchString = "Where CarboHydrates <> 0  order by CarboHydrates asc";
                            break;
                        case (int)NutritionType.Fat:
                            SearchString = "Where Fat <> 0  order by Fat asc";
                            break;
                        case (int)NutritionType.Fiber:
                            SearchString = "Where Fibre <> 0  order by Fibre asc";
                            break;
                        case (int)NutritionType.Moisture:
                            SearchString = "Where Moisture <> 0  order by Moisture asc";
                            break;
                        case (int)NutritionType.Iron:
                            SearchString = "Where Iron <> 0  order by Iron asc";
                            break;
                        case (int)NutritionType.Calcium:
                            SearchString = "Where Calcium <> 0 order by Calcium asc";
                            break;
                        case (int)NutritionType.Phosphorus:
                            SearchString = "Where Phosphorus <> 0  order by Phosphorus asc";
                            break;
                        case (int)NutritionType.Sodium:
                            SearchString = "Where Sodium <> 0  order by Sodium asc";
                            break;
                        case (int)NutritionType.Pottasuim:
                            SearchString = "Where Pottasium <> 0  order by Pottasium asc";
                            break;
                        case (int)NutritionType.Zinc:
                            SearchString = "Where Zinc <> 0  order by Zinc asc";
                            break;
                        case (int)NutritionType.Retinol:
                            SearchString = "Where Fat <> 0  order by Fat asc";
                            break;
                        case (int)NutritionType.BetaCarotene:
                            SearchString = "Where Fat <> 0  order by Fat asc";
                            break;
                        case (int)NutritionType.Thiamine:
                            SearchString = "Where Thiamine <> 0  order by Thiamine asc";
                            break;
                        case (int)NutritionType.Riboflavin:
                            SearchString = "Where Riboflavin <> 0  order by Riboflavin asc";
                            break;
                        case (int)NutritionType.NicotinicAcid:
                            SearchString = "Where NicotinicAcid <> 0  order by NicotinicAcid asc";
                            break;
                        case (int)NutritionType.Pyridoxine:
                            SearchString = "Where Pyridoxine <> 0  order by Pyridoxine asc";
                            break;
                        case (int)NutritionType.FolicAcid:
                            SearchString = "Where Fat <> 0  order by Fat asc";
                            break;
                        case (int)NutritionType.VitaminC:
                            SearchString = "Where VitaminC <> 0  order by VitaminC asc";
                            break;
                    }
                }
                else
                {

                }
            }
        }

        private void spSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtSearch.Text.Trim() != string.Empty || cboDishCategory.SelectedIndex > 0)
            {
                using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                {
                    SearchString = string.Empty;

                    SearchString = "Where 1=1 ";
                    if (cboDishCategory.SelectedIndex > 0)
                    {
                        SearchString = SearchString + "And DishCategoryID =" + cboDishCategory.SelectedIndex + " ";
                    }
                    if (txtSearch.Text != string.Empty)
                    {
                        SearchString = SearchString + "And DisplayName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                    }


                    DisableControls();

                    //InitailizeData();                    

                    FillSearchList();

                    int[] Result = new int[4];
                    Result[0] = Classes.CommonFunctions.Convert2Int(Convert.ToString(chkRegionalNames.IsChecked));
                    Result[1] = Classes.CommonFunctions.Convert2Int(Convert.ToString(cboDishCategory.SelectedIndex));
                    Result[2] = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbHighestin.SelectedIndex));
                    Result[3] = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbLowestin.SelectedIndex));
                    WriteSearchRegistry(Result);
                }
            }
            else
            {
                ShowMessage();
            }
        }

        private void cboDishCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                SearchString = string.Empty;

                SearchString = "Where 1=1 ";
                if (cboDishCategory.SelectedIndex > 0)
                {
                    SearchString = SearchString + "And DishCategoryID =" + cboDishCategory.SelectedIndex + " ";

                    if (txtSearch.Text != string.Empty)
                    {
                        SearchString = SearchString + "And DisplayName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                    }
                }
                else
                {

                }
            }
        }

        private void chkRegionalNames_Checked(object sender, RoutedEventArgs e)
        {
            SetRegionalName();
        }

        private void chkRegionalNames_Unchecked(object sender, RoutedEventArgs e)
        {
            SetEnglishName();
        }

        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            IconGrid.Visibility = Visibility.Hidden;
            DataGrid.Visibility = Visibility.Visible;

            SetThemeOnClick(GridView);
            WriteRegistry(2);
            FillGridData();
        }

        private void btnIcon_Click(object sender, RoutedEventArgs e)
        {
            IconGrid.Visibility = Visibility.Visible;
            DataGrid.Visibility = Visibility.Hidden;

            SetThemeOnClick(IconView);
            WriteRegistry(1);
            FillIconData();
        }

        private void txtPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i = Classes.CommonFunctions.Convert2Int(Convert.ToString(txtControlID.Text));
            if (i > 0)
            {
                ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                int dishid = img.ItemID;

                PrintDish(dishid);
            }
            else
            {
                PrintDish(Classes.CommonFunctions.Convert2Int(Convert.ToString(txtDishID.Text)));
            }
        }       

        private void txtViewMore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                LoadDishNutrients(DISHID);
                YourGridAnimation("ExpandYourGrid");
            }
        }

        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (IsMyGridVisible == true)
                {
                    MyGridAnimation("CollapseMyGrid");
                }
                if (IsYourGridVisible == true)
                {
                    YourGridAnimation("CollapseYourGrid");
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DISHID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Ingredient)lvDataGrid.Items[lvDataGrid.SelectedIndex]).Id));
            if (DISHID > 0)
            {
                imgMyPrevious.Visibility = Visibility.Hidden;
                imgMyNext.Visibility = Visibility.Hidden;

                DisplayDetails(DISHID);
            }           
        }

        private void spDish_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ListBoxItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListBoxItem)) as ListBoxItem;
                if (lvi != null)
                {
                    DISHID = ((BONutrition.Dish)(((System.Windows.Controls.ContentControl)(lvi)).Content)).Id;
                    if (DISHID > 0)
                    {
                        DisplayDetails(DISHID);
                    }
                }
            }
        }
                    
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void spAddNew_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                AddDish addDish = new AddDish();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(addDish);
            }
        }        

        #endregion                
               
    }
}

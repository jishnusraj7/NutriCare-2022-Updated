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
    /// Interaction logic for IngradientSearch.xaml
    /// </summary>
    public partial class IngradientSearch : Page
    {

        #region Varaibles

        int IngredientID;
        Ingredient ingredient = new Ingredient();
        List<Ingredient> ingredientList = new List<Ingredient>();
        string SearchString = string.Empty;                

        Visifire.Charts.Chart chart;
        SaveFileDialog SaveDlg = new SaveFileDialog();
        string imagePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Images\\";

        private bool IsMyGridVisible = false;
        private bool IsYourGridVisible = false;
        private bool IsSearch = false;

        int Result;
        int[] Results = new int[4];

        #endregion

        #region Constructor

        public IngradientSearch()
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
            ini.IniWriteValue("SWCATEGORY", "CATEGORY", Result[1].ToString());
            ini.IniWriteValue("SWHNUTRIENT", "HNUTRIENT", Result[2].ToString());
            ini.IniWriteValue("SWLNUTRIENT", "LNUTRIENT", Result[3].ToString());
            ini = null;
        }

        private int[] ReadSearchRegistry()
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            Results[0] = CommonFunctions.Convert2Int(ini.IniReadValue("SWREGIONAL", "REGIONAL"));
            Results[1] = CommonFunctions.Convert2Int(ini.IniReadValue("SWCATEGORY", "CATEGORY"));
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
                    IconGrid.Visibility = Visibility.Visible;                    
                    DataGrid.Visibility = Visibility.Hidden;
                    SetThemeOnClick(IconView);
                    break;
                case 2:
                    IconGrid.Visibility = Visibility.Hidden;                    
                    DataGrid.Visibility = Visibility.Visible;
                    SetThemeOnClick(GridView);
                    break;
                default:
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
                MyGrid.Visibility = Visibility.Visible;
                IsMyGridVisible = true;
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
                    cboIngredientCategory.SelectedIndex = Results[1];
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
            AlertBox.Show("Select IngredientCategory or Type Search Criteria", "", AlertType.Information, AlertButtons.OK);
        } 

        //private void FillData(ImageDisplay imgDisplay, int ControlID, string ControlName, string IngradientPath, string IngradientName, int IngradientID)
        //{
        //    string imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
        //    imgPath = IngradientPath;
        //    imgDisplay.Visibility = Visibility.Visible;
        //    imgDisplay.SetThemes = true;
        //    imgDisplay.ImageSource = imgPath;
        //    if (chkRegionalNames.IsChecked == true)
        //    {
        //        if (IngradientName.Length > 13)
        //            imgDisplay.ImageName = IngradientName.Substring(0, 13) + "..";
        //        else
        //            imgDisplay.ImageName = IngradientName;
        //    }
        //    else
        //    {
        //        if (IngradientName.Length > 13)
        //            imgDisplay.ImageName = IngradientName.Substring(0, 13) + "..";
        //        else
        //            imgDisplay.ImageName = IngradientName;
        //    }
        //    imgDisplay.ImageID = IngradientID;
        //    imgDisplay.itemName = IngradientName;
        //    imgDisplay.ItemID = IngradientID;
        //    imgDisplay.ControlName = ControlName;
        //    imgDisplay.ControlID = ControlID;
        //    imgDisplay.ToolTip = IngradientName;
        //    imgDisplay.Cursor = Cursors.Hand;
        //}        

        private void LoadIngredientNutrients(int IngredientID)
        {
            List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
            List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
            List<IngredientNutrients> ingredientNutriList = new List<IngredientNutrients>();
            try
            {
                ingredientAminoList = IngredientAminoAcidManager.GetListNutrientValues(IngredientID, (byte)NutrientGroup.AminoAcid);
                if (ingredientAminoList != null)
                {
                    lvAmino.ItemsSource = ingredientAminoList;
                }

                ingredientFattyList = IngredientFattyAcidManager.GetListNutrientValues(IngredientID, (byte)NutrientGroup.FattyAcid);
                if (ingredientFattyList != null)
                {
                    lvFattyAcid.ItemsSource = ingredientFattyList;
                }

                ingredientNutriList = IngredientNutrientsManager.GetListNutrientValues(IngredientID, (byte)NutrientGroup.Nutrients);
                if (ingredientNutriList != null)
                {
                    lvNutrient.ItemsSource = ingredientNutriList;
                }
            }
            catch (Exception ex)
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

        private void FillIngredientDetails(int IngredientID, string IngredientName, int ControlID, string ControlName)
        {
            try
            {
                ingredient = IngredientManager.GetItem(IngredientID);

                if (ingredient != null)
                {
                    imgIngredientDisplay.SetThemes = true;

                    txtIngredientID.Text = Convert.ToString(ingredient.Id);
                    txtIngredientName.Text = ingredient.Name;

                    if (ingredient.DisplayImage != "")
                    {
                        string imgPath = GetImagePath("Ingredients") + "\\" + ingredient.Id + ".jpg";

                        if (File.Exists(imgPath))
                        {
                            imgIngredientDisplay.ImageSource = imgPath;
                        }
                        else
                        {
                            imgIngredientDisplay.ImageSource = string.Empty;
                        }
                    }
                    else
                    {
                        imgIngredientDisplay.ImageSource = string.Empty;
                    }

                    if (chkRegionalNames.IsChecked == true)
                    {
                        if (ingredient.DisplayName.Length > 30)
                            //imgIngredientDisplay.ImageName = ingredient.DisplayName.Substring(0, 21) + "..";
                            txtName.Text = ingredient.DisplayName.Substring(0, 30) + "..";
                        else
                            //imgIngredientDisplay.ImageName = ingredient.DisplayName;
                            txtName.Text = ingredient.DisplayName;
                    }
                    else
                    {
                        if (ingredient.Name.Length > 30)
                            //imgIngredientDisplay.ImageName = ingredient.Name.Substring(0, 21) + "..";
                            txtName.Text = ingredient.Name.Substring(0, 30) + "..";
                        else
                            //imgIngredientDisplay.ImageName = ingredient.Name;
                            txtName.Text = ingredient.Name;
                    }

                    txtComments.Text = Convert.ToString(ingredient.Comments);
                    txtControlID.Text = Convert.ToString(ControlID);
                    txtControlName.Text = ControlName;

                    txtCalorie.Text = Convert.ToString(ingredient.Calorie);
                    txtCalorieUnit.Text = "Kcal";
                    txtProtein.Text = Convert.ToString(ingredient.Protien);
                    txtProteinUnit.Text = "gm";
                    txtFAT.Text = Convert.ToString(ingredient.Fat);
                    txtFATUnit.Text = "gm";
                    txtFibre.Text = Convert.ToString(ingredient.Fibre);
                    txtFibreUnit.Text = "gm";
                    txtCarboHydrates.Text = Convert.ToString(ingredient.CarboHydrate);
                    txtCarboHydratesUnit.Text = "gm";
                    txtMoisture.Text = Convert.ToString(ingredient.Moisture);
                    txtMoistureUnit.Text = "gm";
                    txtIron.Text = Convert.ToString(ingredient.Iron);
                    txtIronUnit.Text = "mg";
                    txtCalcium.Text = Convert.ToString(ingredient.Calcium);
                    txtCalciumUnit.Text = "mg";
                    txtPhosphorus.Text = Convert.ToString(ingredient.Phosphorus);
                    txtPhosphorusUnit.Text = "mg";
                    txtSodium.Text = Convert.ToString(ingredient.Sodium);
                    txtSodiumUnit.Text = "mg";
                    txtPottasium.Text = Convert.ToString(ingredient.Pottasium);
                    txtPottasiumUnit.Text = "mg";
                    txtZinc.Text = Convert.ToString(ingredient.Zinc);
                    txtZincUnit.Text = "mg";
                    txtVitaminARetinol.Text = Convert.ToString(ingredient.VitaminARetinol);
                    txtVitaminARetinolUnit.Text = "mcg";
                    txtVitaminABetaCarotene.Text = Convert.ToString(ingredient.VitaminABetaCarotene);
                    txtVitaminABetaCaroteneUnit.Text = "mcg";
                    txtThiamine.Text = Convert.ToString(ingredient.Thiamine);
                    txtThiamineUnit.Text = "mg";
                    txtRiboflavin.Text = Convert.ToString(ingredient.Riboflavin);
                    txtRiboflavinUnit.Text = "mg";
                    txtNicotinicAcid.Text = Convert.ToString(ingredient.NicotinicAcid);
                    txtNicotinicAcidUnit.Text = "mg";
                    txtPyridoxine.Text = Convert.ToString(ingredient.Pyridoxine);
                    txtPyridoxineUnit.Text = "mg";
                    txtFolicAcid.Text = Convert.ToString(ingredient.FolicAcid);
                    txtFolicAcidUnit.Text = "mcg";
                    txtVitaminC.Text = Convert.ToString(ingredient.VitaminC);
                    txtVitaminCUnit.Text = "mg";

                    if (Convert.ToString(ingredient.GeneralHealthValue) != "")
                    {
                        txtHealthValue.Text = Convert.ToString(ingredient.GeneralHealthValue) + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(ingredient.AyurHealthValue);
                    }
                    else
                    {
                        txtHealthValue.Text = Convert.ToString(ingredient.AyurHealthValue);
                    }

                    //LoadIngredientNutrients(ingredient.Id);
                    CreateGraph(ingredient.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ingredient = null;
            }
        }

        public void FillDropDownFromXML()
        {
            try
            {
                XMLServices.GetXMLDataByLanguage(cbHighestin, AppDomain.CurrentDomain.BaseDirectory + "\\XML\\Nutrition.xml", 1);
                XMLServices.GetXMLDataByLanguage(cbLowestin, AppDomain.CurrentDomain.BaseDirectory + "\\XML\\Nutrition.xml", 1);
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
        }

        public void EnableControls()
        {
            //txtPrevious1.Visibility = Visibility.Visible;
            //txtPrevious2.Visibility = Visibility.Visible;
            //txtNext1.Visibility = Visibility.Visible;
            //txtNext2.Visibility = Visibility.Visible;            
        }

        public void InitailizeControls()
        { 
            imgIngredientDisplay.ImageSource = string.Empty;
            imgIngredientDisplay.ImageName = string.Empty;
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

            txtHealthValue.Text = string.Empty;

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

                if (cboIngredientCategory.SelectedIndex > 0)
                {
                    SearchString = SearchString + "And FoodHabitID =" + cboIngredientCategory.SelectedIndex + " ";
                }

                if (txtSearch.Text != string.Empty)
                {
                    if (chkRegionalNames.IsChecked == true)
                        SearchString = SearchString + "And DisplayName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                    else
                        SearchString = SearchString + "And IngredientName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
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
                
                ingredientList = IngredientManager.GetList(SearchString);                
                FillData();
            }
        }

        private void FillIconData()
        {
            if (ingredientList.Count > 0)
            {
                lblTotalItemsList.Text = "Total Items : " + Convert.ToString(ingredientList.Count);
                lvIngredients.ItemsSource = ingredientList;

                //IconGrid.Visibility = Visibility.Visible;                
                //lblTotalItemsList.Text = "Total Items : " + Convert.ToString(ingredientList.Count);
                //if (ingredientList.Count > 50)
                //{
                //    EnableControls();
                //}

                //for (int i = 1; i <= ingredientList.Count; i++)
                //{
                //    ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                //    if (chkRegionalNames.IsChecked == true)
                //    {
                //        FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].DisplayName, ingredientList[i - 1].Id);
                //    }
                //    else
                //    {
                //        FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].Name, ingredientList[i - 1].Id);
                //    }
                //}

                ////if (chkRegionalNames.IsChecked == true)
                ////{
                ////    SetRegionalName();
                ////}
                ////else
                ////{
                ////    SetEnglishName();
                ////}
            }
        }

        private void FillGridData()
        {
            if (ingredientList.Count > 0)
            {
                //NormalGrid.ItemsSource = ingredientList;
                //NormalGrid.SelectedIndex = 0;
                //NormalGrid.ScrollIntoView(NormalGrid.SelectedItem);

                lblTotalItemsGrid.Text = "Total Items : " + Convert.ToString(ingredientList.Count);

                lvDataGrid.ItemsSource = ingredientList;
                lvDataGrid.SelectedIndex = 0;
                lvDataGrid.ScrollIntoView(lvDataGrid.SelectedItem);
                //if (chkRegionalNames.IsChecked == true)
                //{
                //    SetRegionalName();
                //}
                //else
                //{
                //    SetEnglishName();
                //}
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
                
        public void PrintIngredient(ListView lv)
        {
            try
            {
                if (lv.SelectedIndex >= 0)
                {
                    IngredientID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Ingredient)lv.Items[lv.SelectedIndex]).Id));
                    ReportViewer ingredientReport = new ReportViewer();
                    ingredientReport.DisplayItem = ItemType.Ingredient;
                    ingredientReport.ReportType = (int)ReportItem.Ingredient;
                    ingredientReport.ItemID = IngredientID;
                    ingredientReport.IngredientID = IngredientID;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        ingredientReport.IsRegional = true;
                    }
                    else
                    {
                        ingredientReport.IsRegional = false;
                    }
                    ingredientReport.Owner = Application.Current.MainWindow;
                    ingredientReport.ShowDialog();
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

        public void PrintIngredient(int IngredientID)
        {
            try
            {
                ReportViewer ingredientReport = new ReportViewer();
                ingredientReport.DisplayItem = ItemType.Ingredient;
                ingredientReport.ReportType = (int)ReportItem.Ingredient;
                ingredientReport.ItemID = IngredientID;
                ingredientReport.IngredientID = IngredientID;
                if (chkRegionalNames.IsChecked == true)
                {
                    ingredientReport.IsRegional = true;
                }
                else
                {
                    ingredientReport.IsRegional = false;
                }
                ingredientReport.Owner = Application.Current.MainWindow;
                ingredientReport.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DisplayDetails(int IngredientID)
        {
            try
            {
                MyGridAnimation("ExpandMyGrid");

                InitailizeControls();
                FillIngredientDetails(IngredientID, string.Empty, 0, string.Empty);

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
                    if (ingredientList.Count > 0)
                    {
                        if (ingredientList.Count > 50)
                        {
                            EnableControls();
                        }

                        for (int i = 1; i <= ingredientList.Count; i++)
                        {
                            ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                            if (chkRegionalNames.IsChecked == true)
                            {
                                //FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].DisplayName, ingredientList[i - 1].Id);
                            }
                            else
                            {
                                //FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].Name, ingredientList[i - 1].Id);
                            }
                        }
                    }
                    break;
                case 2:
                    lvGeneralDataCol2.CellTemplate = this.FindResource("DisplayNameTemplate") as DataTemplate;
                    break;
                default:
                    if (ingredientList.Count > 0)
                    {
                        if (ingredientList.Count > 50)
                        {
                            EnableControls();
                        }

                        for (int i = 1; i <= ingredientList.Count; i++)
                        {
                            ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                            if (chkRegionalNames.IsChecked == true)
                            {
                                //FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].DisplayName, ingredientList[i - 1].Id);
                            }
                            else
                            {
                                //FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].Name, ingredientList[i - 1].Id);
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
                    if (ingredientList.Count > 0)
                    {
                        if (ingredientList.Count > 50)
                        {
                            EnableControls();
                        }

                        for (int i = 1; i <= ingredientList.Count; i++)
                        {
                            ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                            if (chkRegionalNames.IsChecked == true)
                            {
                                //FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].DisplayName, ingredientList[i - 1].Id);
                            }
                            else
                            {
                                //FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].Name, ingredientList[i - 1].Id);
                            }
                        }
                    }
                    break;
                case 2:
                    lvGeneralDataCol2.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
                    break;
                default:
                    if (ingredientList.Count > 0)
                    {
                        if (ingredientList.Count > 50)
                        {
                            EnableControls();
                        }

                        for (int i = 1; i <= ingredientList.Count; i++)
                        {
                            ImageDisplay img = (ImageDisplay)this.GetType().InvokeMember("imgDisplay" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                            if (chkRegionalNames.IsChecked == true)
                            {
                                //FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].DisplayName, ingredientList[i - 1].Id);
                            }
                            else
                            {
                                //FillData(img, i, "imgDisplay" + i, ingredientList[i - 1].DisplayImage, ingredientList[i - 1].Name, ingredientList[i - 1].Id);
                            }
                        }
                    }
                    break;
            }

            WriteRegionalRegistry(0);
        }

        #endregion

        #region Graph

        public void CreateGraph(int IngredientID)
        {
            try
            {
                CreateGramGraph(IngredientID);
                CreateMilliGramGraph(IngredientID);
                CreateMicroGramGraph(IngredientID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void CreateGramGraph(int IngredientID)
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

            DataSet dsIngredient = new DataSet();
            dsIngredient = IngredientManager.GetGramItemSearch(IngredientID);

            if (dsIngredient.Tables.Count > 0)
            {
                if (dsIngredient.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        dataPoint = new Visifire.Charts.DataPoint();
                        string AxisXLabel = Convert.ToString(dsIngredient.Tables[0].Columns[i].ColumnName.ToString());
                        if (AxisXLabel.Length > 7)
                            AxisXLabel = AxisXLabel.Substring(0, 7) + "..";
                        dataPoint.AxisXLabel = AxisXLabel;
                        dataPoint.YValue = CommonFunctions.Convert2Double(Convert.ToString(dsIngredient.Tables[0].Rows[0][i]));
                        dataSeries.DataPoints.Add(dataPoint);
                    }
                }
            }

            chart.Series.Add(dataSeries);
            GramGraphLayout.Children.Add(chart);

        }

        public void CreateMilliGramGraph(int IngredientID)
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

            DataSet dsIngredient = new DataSet();
            dsIngredient = IngredientManager.GetMilliItemSearch(IngredientID);
            if (dsIngredient.Tables.Count > 0)
            {
                if (dsIngredient.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        dataPoint = new Visifire.Charts.DataPoint();
                        string AxisXLabel = Convert.ToString(dsIngredient.Tables[0].Columns[i].ColumnName.ToString());
                        if (AxisXLabel.Length > 7)
                            AxisXLabel = AxisXLabel.Substring(0, 7) + "..";
                        dataPoint.AxisXLabel = AxisXLabel;
                        dataPoint.YValue = CommonFunctions.Convert2Double(Convert.ToString(dsIngredient.Tables[0].Rows[0][i]));
                        dataSeries.DataPoints.Add(dataPoint);
                    }
                }
            }

            chart.Series.Add(dataSeries);
            MilliGramGraphLayout.Children.Add(chart);

        }

        public void CreateMicroGramGraph(int IngredientID)
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

            DataSet dsIngredient = new DataSet();
            dsIngredient = IngredientManager.GetMicroItemSearch(IngredientID);

            if (dsIngredient.Tables.Count > 0)
            {
                if (dsIngredient.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        dataPoint = new Visifire.Charts.DataPoint();
                        string AxisXLabel = Convert.ToString(dsIngredient.Tables[0].Columns[i].ColumnName.ToString());
                        if (AxisXLabel.Length > 7)
                            AxisXLabel = AxisXLabel.Substring(0, 7) + "..";
                        dataPoint.AxisXLabel = AxisXLabel;
                        dataPoint.YValue = CommonFunctions.Convert2Double(Convert.ToString(dsIngredient.Tables[0].Rows[0][i]));
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

            Keyboard.Focus(cboIngredientCategory);
            FillDropDownFromXML();
            Classes.CommonFunctions.FillFoodCategory(cboIngredientCategory);

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
                if (lvIngredients.SelectedIndex > 0)
                {
                    lvIngredients.SelectedItem = lvIngredients.Items[lvIngredients.SelectedIndex - 1];
                }
                else
                {
                    isStart = true;
                }

                if (lvIngredients.SelectedItem != null && isStart == false)
                {
                    IngredientID = ((BONutrition.Ingredient)(lvIngredients.SelectedItem)).Id;
                    if (IngredientID > 0)
                    {
                        DisplayDetails(IngredientID);
                    }
                }
            }
        }

        private void lblMyNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                bool isEnd = false;
                if ((lvIngredients.Items.Count - 1) > lvIngredients.SelectedIndex)
                {
                    lvIngredients.SelectedItem = lvIngredients.Items[lvIngredients.SelectedIndex + 1];
                }
                else
                {
                    isEnd = true;
                }

                if (lvIngredients.SelectedItem != null && isEnd == false)
                {
                    IngredientID = ((BONutrition.Ingredient)(lvIngredients.SelectedItem)).Id;
                    if (IngredientID > 0)
                    {
                        DisplayDetails(IngredientID);
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
            if (txtSearch.Text.Trim() != string.Empty || cboIngredientCategory.SelectedIndex > 0)
            {
                using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                {
                    SearchString = string.Empty;

                    SearchString = "Where 1=1 ";
                    if (cboIngredientCategory.SelectedIndex > 0)
                    {
                        SearchString = SearchString + "And FoodHabitID =" + cboIngredientCategory.SelectedIndex + " ";
                    }
                    if (txtSearch.Text != string.Empty)
                    {
                        SearchString = SearchString + "And DisplayName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                    }


                    DisableControls();
                    FillSearchList();

                    int[] Result = new int[4];
                    Result[0] = Classes.CommonFunctions.Convert2Int(Convert.ToString(chkRegionalNames.IsChecked));
                    Result[1] = Classes.CommonFunctions.Convert2Int(Convert.ToString(cboIngredientCategory.SelectedIndex));
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
       
        private void cboIngredientCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                SearchString = string.Empty;

                SearchString = "Where 1=1 ";
                if (cboIngredientCategory.SelectedIndex > 0)
                {
                    SearchString = SearchString + "And FoodHabitID =" + cboIngredientCategory.SelectedIndex + " ";

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
                int IngredientID = img.ItemID;

                PrintIngredient(IngredientID);
            }
            else
            {
                PrintIngredient(Classes.CommonFunctions.Convert2Int(Convert.ToString(txtIngredientID.Text)));
            }
        }
       
        private void txtViewMore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                LoadIngredientNutrients(IngredientID);
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
            IngredientID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Ingredient)lvDataGrid.Items[lvDataGrid.SelectedIndex]).Id));

            if (IngredientID > 0)
            {
                imgMyPrevious.Visibility = Visibility.Hidden;
                imgMyNext.Visibility = Visibility.Hidden;

                DisplayDetails(IngredientID);
            }
           
        }
       
        private void spIngredient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ListBoxItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListBoxItem)) as ListBoxItem;
                if (lvi != null)
                {
                    IngredientID = ((BONutrition.Ingredient)(((System.Windows.Controls.ContentControl)(lvi)).Content)).Id;
                    if (IngredientID > 0)
                    {
                        DisplayDetails(IngredientID);
                    }
                }
            }
        }

        private void spAddNew_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                AddIngredient addIngredient = new AddIngredient();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(addIngredient);
            }
        }

        #endregion                              
    }
}

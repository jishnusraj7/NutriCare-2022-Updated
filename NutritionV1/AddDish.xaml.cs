using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.Xml;
using System.Resources;
using System.Threading;
using System.Configuration;

using BONutrition;
using BLNutrition;
using NutritionV1.Enums;
using NutritionV1.Classes;
using NutritionV1.Common.Classes;
using Microsoft.Win32;
using Indocosmo.Framework.ExceptionManagement;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for AddDish.xaml
    /// </summary>
    public partial class AddDish : Page
    {
        #region DECLARATIONS

        private enum ListViewIndex
        {
            Quantity = 3,
            Unit = 4,
            GramWeight = 5,
        }

        private static ArrayList ingredientID = new ArrayList();
        private static int dishID;
        private bool isDisplayMode;
        private bool isSelectionChanged;
        private string imageDefaultPath = string.Empty;
        private string imageFileName = string.Empty;
        private const string specialChar = " ● ";

        private float dishCalorie;
        private float dishProtien;
        private float dishCarboHydrates;
        private float dishFAT;
        private float dishFibre;
        private float dishIron;
        private float dishCalcium;
        private float dishPhosphorus;
        private float dishVitaminARetinol;
        private float dishVitaminABetaCarotene;
        private float dishThiamine;
        private float dishRiboflavin;
        private float dishNicotinicAcid;
        private float dishPyridoxine;
        private float dishFolicAcid;
        private float dishVitaminB12;
        private float dishVitaminC;

        private float planWeight = 0;
        private float planWeight1 = 0;
        private float planWeight2 = 0;

        private float calorieSum = 0;
        private float carbohydratesSum = 0;
        private float fatSum = 0;
        private float proteinSum = 0;
        private float fiberSum = 0;
        private float ironSum = 0;
        private float calciumSum = 0;

        private string medicalFavourable = string.Empty;
        private string medicalUnFavourable = string.Empty;
        private string ayurvedicFavourable = string.Empty;
        private string ayurvedicUnFavourable = string.Empty;
        private string keyWords = string.Empty;
        
        private bool isIngredientsLoad;
        private bool isDishValueChanged;
        private bool isIngredientValueChanged;
        private bool IsImageAdd;

        private bool isPlan1Loaded;
        private bool isPlan2Loaded;
        private bool isPlan3Loaded;
        private double TotalWeightvalue = 0;
        private float serveSize = 0;
        private float serveWeight = 0;
        private int visibleFlag = 0;
        private string searchString = string.Empty;

        private string uploadCaption = string.Empty;
        private string removeCaption = string.Empty;

		private Dish dish = new Dish();
		private DishIngredient dishIngredient = new DishIngredient();
        private IngredientStandardUnit ingredientUnitItem = new IngredientStandardUnit();

		private List<DishIngredient> dishIngredientList = new List<DishIngredient>();        
        private List<DishIngredient> ingredientDeleteList = new List<DishIngredient>();

        private List<DishIngredient> dishIngredientNutrientsList = new List<DishIngredient>();
        private List<IngredientStandardUnit> ingredientUnitList = new List<IngredientStandardUnit>();

        private List<IngredientNutrients> ingredientNutrientsList = new List<IngredientNutrients>();
        private List<IngredientAminoAcid> ingredientAminoAcidList = new List<IngredientAminoAcid>();
        private List<IngredientFattyAcid> ingredientFattyAcidList = new List<IngredientFattyAcid>();

        private List<IngredientNutrients> ingredientNutrientsPlan1List = new List<IngredientNutrients>();
        private List<IngredientAminoAcid> ingredientAminoAcidPlan1List = new List<IngredientAminoAcid>();
        private List<IngredientFattyAcid> ingredientFattyAcidPlan1List = new List<IngredientFattyAcid>();

        private List<IngredientNutrients> ingredientNutrientsPlan2List = new List<IngredientNutrients>();
        private List<IngredientAminoAcid> ingredientAminoAcidPlan2List = new List<IngredientAminoAcid>();
        private List<IngredientFattyAcid> ingredientFattyAcidPlan2List = new List<IngredientFattyAcid>();

        private List<IngredientNutrients> ingredientNutrientsPlan3List = new List<IngredientNutrients>();
        private List<IngredientAminoAcid> ingredientAminoAcidPlan3List = new List<IngredientAminoAcid>();
        private List<IngredientFattyAcid> ingredientFattyAcidPlan3List = new List<IngredientFattyAcid>();

        private List<NSysNutrient> sysNutrientList = new List<NSysNutrient>();

        DataTable dtIngNutritiveValues = new DataTable();
        OpenFileDialog openDlg = new OpenFileDialog();

       #endregion

		#region PROPERTIES

		public static int DishID
		{
			get
			{
				return dishID;
			}
			set
			{
				dishID = value;
			}
		}

        public static ArrayList IngredientID
        {
            get
            {
                return ingredientID;
            }
            set
            {
                ingredientID = value;
            }
        }

        public bool IsDisplayMode
        {
            get
            {
                return isDisplayMode;
            }
            set
            {
                isDisplayMode = value;
            }
        }

		#endregion

		#region CONSTRUCTOR

		public AddDish()
        {
            InitializeComponent();
		}
      
		#endregion

		#region EVENTS

		private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            SetCulture();

            FillCombo();
            LoadAminoAcidsParameters();
            LoadFattyAcidsParameters();
            LoadNutrientsParameters();

            SetDisplayStyle();
            txtHelp.Text = specialChar + " Recipes are collected from various authentic sources " + System.Environment.NewLine + specialChar + " Total weights are approximaltley calculated " + System.Environment.NewLine + specialChar + " Nutritive values of dishes are calculated based on raw ingredients";
        }

        private void btnAddImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsImageAdd)
                {
                    imageFileName = OpenDialog("Get your image", "Image Files (*.jpg)|*.jpg", "");
                    if (imageFileName == string.Empty)
                    {
                        IsImageAdd = true;
                        imageFileName = imageDefaultPath;
                    }
                    else
                    {
                        IsImageAdd = false;
                        btnAddImage.Content = removeCaption;
                        imgDisplay.ImagePath = imageFileName;
                    }
                }
                else
                {
                    bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1221"), "", AlertType.Exclamation, AlertButtons.YESNO);
                    if (result == true)
                    {
                        imgDisplay.ImagePath = string.Empty;
                        imageFileName = string.Empty;
                        btnAddImage.Content = uploadCaption;
                        IsImageAdd = true;
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
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {           
            SaveDish();
        }

        private void btnSelectIngredient_Click(object sender, RoutedEventArgs e)
        {
            UpdateIngredientList();
        }

        private void btnSelectIngredient_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateIngredientList();
            }
            else if (e.Key == Key.Tab)
            {
                if (lvDishIngredient.Items.Count > 0)
                {
                    lvDishIngredient.SelectedIndex = 0;
                    ItemContainerGenerator generator = this.lvDishIngredient.ItemContainerGenerator;
                    ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(lvDishIngredient.SelectedIndex);
                    TextBox tbFind = ListViewHelper.GetDescendantByType(selectedItem, typeof(TextBox), "txtQuantity") as TextBox;
                    if (tbFind != null)
                    {
                        CommonFunctions.SetControlFocus(tbFind);
                    }
                }
            }
        }

        private void btnEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchDish searchDish = new SearchDish();
            searchDish.Owner = Application.Current.MainWindow;
            searchDish.ShowDialog();
            if (DishID > 0)
            {
                using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                {
                    DisplayDishDetails();
                    FillComboPlan();
                    SetButtonVisibility();
                }
            }
        }
        
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PlanSelector planSelector = new PlanSelector();
            planSelector.DishID = dishID;
            planSelector.Owner = Application.Current.MainWindow;
            planSelector.ShowDialog();
        }
       
        private void imgDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvDishIngredient.SelectedIndex = lvDishIngredient.ItemContainerGenerator.IndexFromContainer(lvi);
                bool result = AlertBox.Show("Do you want to remove " + ((DishIngredient)lvDishIngredient.Items[lvDishIngredient.SelectedIndex]).IngredientName + " from this Recipe?", "Confirmation", AlertType.Exclamation, AlertButtons.YESNO);
                if (result == true)
                {
                    RemoveIngredientItem();
                }
            }
        }

        private void btnNutriDetails_Click(object sender, RoutedEventArgs e)
        {
            ShowPlanNutrients();
            GridAnimation("ExpandGrid");
            visibleFlag = 1;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteDish();
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridAnimation("CollapseGrid");
            visibleFlag = 0;
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (visibleFlag == 1)
                {
                    visibleFlag = 0;
                    GridAnimation("CollapseGrid");
                }
            }
        }

        private void txtNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Classes.CommonFunctions.FilterDecimal(sender, e);
                var uie = e.OriginalSource as UIElement;
                if (e.Key == Key.Enter)
                {
                    e.Handled = true;
                    uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtNumber_PreviewKeyDownDecimal(object sender, KeyEventArgs e)
        {
            try
            {
                Classes.CommonFunctions.FilterNumeric(sender, e);
                var uie = e.OriginalSource as UIElement;
                if (e.Key == Key.Enter)
                {
                    e.Handled = true;
                    uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtGeneral_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtGeneral_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(txt.Text.Length, txt.Text.Length);
        }

        private void txtGeneral_GotMouseCapture(object sender, MouseEventArgs e)
        {
            //TextBox txt = (TextBox)sender;
        }

        private void txtQuantity_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvDishIngredient.SelectedIndex = lvDishIngredient.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            isDishValueChanged = true;
            isIngredientValueChanged = true;
            CalculateGramWeight();            
        }

        private void txtQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Classes.CommonFunctions.FilterNumeric(sender, e);
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                if (lvDishIngredient.SelectedIndex == 0)
                {
                    CommonFunctions.SetControlFocus(btnSelectIngredient);
                }
            }            
            else if (e.Key == Key.Enter)
            {
                var uie = e.OriginalSource as UIElement;
                e.Handled = true;
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
        
        private void cboUnit_GotFocus(object sender, RoutedEventArgs e)
        {
            isSelectionChanged = true;
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvDishIngredient.SelectedIndex = lvDishIngredient.ItemContainerGenerator.IndexFromContainer(lvi);
            }            
        }

        private void cboUnit_LostFocus(object sender, RoutedEventArgs e)
        {
            isSelectionChanged = false;
        }
        
        private void cboUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isSelectionChanged == true)
            {
                isDishValueChanged = true;
                isIngredientValueChanged = true;
                CalculateGramWeight();
            }
        }

        private void txtOrder_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvDishIngredient.SelectedIndex = lvDishIngredient.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }       

        private void tbDish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabDisplay();
                Thread.Sleep(50);
            }
        }
     
        private void cboServeUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtServeUnit1.Text = string.Empty;
            txtServeUnit2.Text = string.Empty;
            if (cboServeUnit.SelectedIndex > 0)
            {
                txtServeUnit1.Text = ((NSysServeUnit)(cboServeUnit.SelectedItem)).ServeUnitName;
                txtServeUnit2.Text = ((NSysServeUnit)(cboServeUnit.SelectedItem)).ServeUnitName;
            }
        }

        private void cboPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double servingSize = 0;
            double servingWeight = 0;
            lblNutritionValuesServe.Content = "Total Nutritive Values";
            switch (cboPlan.SelectedIndex)
            { 
                case 0:
                    servingWeight = CommonFunctions.Convert2Float(txtStandardWeight.Text);
                    servingSize = CommonFunctions.Convert2Float(txtServingSize.Text) * servingWeight;
                    lblNutritionValuesServe.Content = "Total Nutritive Value for PLAN I (" + servingWeight + " gm)";
                    break;
                case 1:
                    servingWeight = CommonFunctions.Convert2Float(txtStandardWeight1.Text);
                    servingSize = CommonFunctions.Convert2Float(txtServingSize1.Text) * servingWeight;
                    lblNutritionValuesServe.Content = "Total Nutritive Value for PLAN II (" + servingWeight + " gm)";
                    break;
                case 2:
                    servingWeight = CommonFunctions.Convert2Float(txtStandardWeight2.Text);
                    servingSize = CommonFunctions.Convert2Float(txtServingSize2.Text) * servingWeight;
                    lblNutritionValuesServe.Content = "Total Nutritive Value for PLAN III (" + servingWeight + " gm)";
                    break;
            }

            lblSCalorie.Content = Convert.ToString(Math.Round((calorieSum / servingSize) * servingWeight, 2));
            lblSProtein.Content = Convert.ToString(Math.Round((proteinSum / servingSize) * servingWeight, 2));
            lblSCarbo.Content = Convert.ToString(Math.Round((carbohydratesSum / servingSize) * servingWeight, 2));
            lblSFat.Content = Convert.ToString(Math.Round((fatSum / servingSize) * servingWeight, 2));
            lblSFiber.Content = Convert.ToString(Math.Round((fiberSum / servingSize) * servingWeight, 2));
            lblSIron.Content = Convert.ToString(Math.Round((ironSum / servingSize) * servingWeight, 2));
            lblSCalcium.Content = Convert.ToString(Math.Round((calciumSum / servingSize) * servingWeight, 2));

            if (visibleFlag == 1)
            {
                ShowPlanNutrients();
            }
        }

        private void cboPlan_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                tbDish.SelectedIndex = 2;
                CommonFunctions.SetControlFocus(txtProcessENG);
            }
        }

        private void lvList_LostFocus(object sender, RoutedEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv != null)
            {
                lv.SelectedItems.Clear();
            }
        }

        private void txtStandardWeight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CommonFunctions.Convert2Float(txtStandardWeight.Text) != CommonFunctions.Convert2Float(txtPreWeight.Text))
            {                
                isDishValueChanged = true;
                FillComboPlan();
            }
        }

        private void txtStandardWeight1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CommonFunctions.Convert2Float(txtStandardWeight1.Text) != CommonFunctions.Convert2Float(txtPreWeight1.Text))
            {
                isDishValueChanged = true;
                FillComboPlan();
            }
        }

        private void txtStandardWeight2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CommonFunctions.Convert2Float(txtStandardWeight2.Text) != CommonFunctions.Convert2Float(txtPreWeight2.Text))
            {
                isDishValueChanged = true;
                FillComboPlan();
            }
        }

        private void txtStandardWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CommonFunctions.Convert2Int(txtStandardWeight.Text) != 0)
            {
                if (txtTotalWeightvalue.Text != "")
                {
                    if (CommonFunctions.Convert2Int(txtStandardWeight.Text) <= 400)
                    {
                        txtServingSize.Text = CommonFunctions.Convert2Double(txtTotalWeightvalue.Text) == 0 ? "0" : Convert.ToString(CommonFunctions.RoundDown(((CommonFunctions.Convert2Double(txtTotalWeightvalue.Text) / CommonFunctions.Convert2Double(txtStandardWeight.Text)) / .5), 0) * .5);
                        isIngredientValueChanged = true;
                    }
                    else
                    {
                        AlertBox.Show("Standard Weight should be less than 400 gm");
                        txtStandardWeight.Text = string.Empty;
                        Keyboard.Focus(txtStandardWeight);
                    }
                }
            }
            else
            {
                txtServingSize.Text = string.Empty;
            }
        }

        private void txtStandardWeight1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CommonFunctions.Convert2Int(txtStandardWeight1.Text) != 0)
            {
                if (txtTotalWeightvalue.Text != "")
                {
                    if (CommonFunctions.Convert2Int(txtStandardWeight1.Text) <= 400)
                    {
                        txtServingSize1.Text = CommonFunctions.Convert2Double(txtTotalWeightvalue.Text) == 0 ? "0" : Convert.ToString(CommonFunctions.RoundDown(((CommonFunctions.Convert2Double(txtTotalWeightvalue.Text) / CommonFunctions.Convert2Double(txtStandardWeight1.Text)) / .5), 0) * .5);
                        isIngredientValueChanged = true;
                    }
                    else
                    {
                        AlertBox.Show("Standard Weight should be less than 400 gm");
                        txtStandardWeight1.Text = string.Empty;
                        Keyboard.Focus(txtStandardWeight1);
                    }
                }
            }
            else
            {
                txtServingSize1.Text = string.Empty;
            }
        }

        private void txtStandardWeight2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CommonFunctions.Convert2Int(txtStandardWeight2.Text) != 0)
            {
                if (txtTotalWeightvalue.Text != "")
                {
                    if (CommonFunctions.Convert2Int(txtStandardWeight1.Text) <= 400)
                    {
                        txtServingSize2.Text = CommonFunctions.Convert2Double(txtTotalWeightvalue.Text) == 0 ? "0" : Convert.ToString(CommonFunctions.RoundDown(((CommonFunctions.Convert2Double(txtTotalWeightvalue.Text) / CommonFunctions.Convert2Double(txtStandardWeight2.Text)) / .5), 0) * .5);
                        isIngredientValueChanged = true;
                    }
                    else
                    {
                        AlertBox.Show("Standard Weight should be less than 400 gm");
                        txtStandardWeight2.Text = string.Empty;
                        Keyboard.Focus(txtStandardWeight2);
                    }
                }
            }
            else
            {
                txtServingSize2.Text = string.Empty;
            }
        }

        private void txtRemarks_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                
            }
            else if (e.Key == Key.Tab)
            {
                tbDish.SelectedIndex = 1;
            }
        }

        private void txtStandardWeight2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Classes.CommonFunctions.FilterNumeric(sender, e);
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                CommonFunctions.SetControlFocus(txtStandardWeight1);
            }
            else if (e.Key == Key.Tab)
            {
                tbDish.SelectedIndex = 2;
            }
        }

        private void txtProcessENG_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                tbDish.SelectedIndex = 1;
                CommonFunctions.SetControlFocus(txtStandardWeight2);
            }
            //else if (e.Key == Key.Tab)
            //{
            //    tbDish.SelectedIndex = 3;
            //}
        }

        private void txtOrder_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Classes.CommonFunctions.FilterNumeric(sender, e);
            if (e.Key == Key.Tab)
            {
                if (lvDishIngredient.SelectedIndex == (lvDishIngredient.Items.Count - 1))
                {
                    CommonFunctions.SetControlFocus(txtStandardWeight);
                }
            }
        }

        private void txtTitleENG_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTitleDisplay.Text = txtTitleENG.Text;
        }
       
        private void imgPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PlanSelector planSelector = new PlanSelector();
            planSelector.DishID = dishID;
            planSelector.Owner = Application.Current.MainWindow;
            planSelector.ShowDialog();
        }

        private void btnSetPlan_Click(object sender, RoutedEventArgs e)
        {
            if (dishID == 0)
            {
                AlertBox.Show("Please select a dish", "", AlertType.Information, AlertButtons.OK);
                txtStandardWeight2.Focus();
                return;
            }

            if (CommonFunctions.Convert2Float(txtStandardWeight2.Text) <= 0)
            {
                AlertBox.Show("Please enter the Standard Weight", "", AlertType.Information, AlertButtons.OK);
                txtStandardWeight2.Focus();
                return;
            }

            if (CommonFunctions.Convert2Float(txtServingSize2.Text) <= 0)
            {
                AlertBox.Show("Please enter the Serving Size", "", AlertType.Information, AlertButtons.OK);
                txtServingSize2.Focus();
                return;
            }

            DishManager.UpdateDishPlan(dishID,CommonFunctions.Convert2Float(txtServingSize2.Text), CommonFunctions.Convert2Float(txtStandardWeight2.Text));
            AlertBox.Show("New Plan updated successfully", "", AlertType.Information, AlertButtons.OK);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                DishSearch dishSearch = new DishSearch();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(dishSearch);
            }
        }

        private void imgAyurvedic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void imgHealthValue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void imgNutrition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void imgDisplayImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

            #region METHODS

            /// <summary>
            /// Apply the theme style to individual controls
            /// </summary>
        private void SetTheme()
        {
            App apps = (App)Application.Current;

            imgDisplay.SetThemes = true;
            lblProcessENG.Style = (Style)apps.SetStyle["LabelStyle"];
            lblRemarks.Style = (Style)apps.SetStyle["LabelStyle"];
            lblTitleENG.Style = (Style)apps.SetStyle["LabelStyle"];
            lblTitleREG.Style = (Style)apps.SetStyle["LabelStyle"];
            lblServingSize.Style = (Style)apps.SetStyle["LabelStyle"];
            lblCookTime.Style = (Style)apps.SetStyle["LabelStyle"];
            lblStandardWeight.Style = (Style)apps.SetStyle["LabelStyle"];
            lblTimeUnit.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWeightUnit.Style = (Style)apps.SetStyle["LabelStyle"];
            lblShelf.Style = (Style)apps.SetStyle["LabelStyle"];
            lblRefrigerated.Style = (Style)apps.SetStyle["LabelStyle"];
            lblFrozen.Style = (Style)apps.SetStyle["LabelStyle"];
            lblDaysFrozen.Style = (Style)apps.SetStyle["LabelStyle"];
            lblDaysRefrigerated.Style = (Style)apps.SetStyle["LabelStyle"];
            lblDaysShelf.Style = (Style)apps.SetStyle["LabelStyle"];
            lblEthnic.Style = (Style)apps.SetStyle["LabelStyle"];
            lblFoodType.Style = (Style)apps.SetStyle["LabelStyle"];
            lblIngNutritiveValues.Style = (Style)apps.SetStyle["LabelStyle"];
        }

        /// <summary>
        /// Apply language to Individual Captions
        /// </summary>
        private void SetCulture()
        {            
            uploadCaption = "Upload Image";
            removeCaption = "Remove Image";

            lblTitleENG.Content = "Dish Name";
            lblTitleREG.Content = "Regional Name";
            lblEthnic.Content = "Ethnicity";
            lblFoodType.Content = "Food Type";
            lblCookTime.Content = "Cooking Time";
            lblTimeUnit.Content = "Minute";
            lblDishCategory.Content = "Category";
            lblShelf.Content = "Shelf Life";
            lblDaysShelf.Content = "days";
            lblDaysRefrigerated.Content = "days";
            lblDaysFrozen.Content = "days";
            lblRefrigerated.Content = "Refrigerate Life";
            lblFrozen.Content = "Frozen Life";
            lblWeightUnit.Content = "gm";

            gvNameCol.Header = "Ingredient Name";
            gvQuantityCol.Header = "Quantity";
            gvWeightCol.Header = "GramWeight";
            gvUnitCol.Header = "Unit";
            gvSectionCol.Header = "Section";
            lblNutrients.Content = "Main Nutrients";
            gvNutrientColName.Header = "Nutrients";
            lblFattyAcids.Content = "Fatty Acids";
            gvFattyAcidColName.Header = "Components";
            lblAminoAcids.Content = "Amino Acids";
            gvAminoAcidColName.Header = "Components";
            lblPeriod.Content = "Period of Expiry";
            lblProcessENG.Content = "Processing Method";            
        }

        /// <summary>
        /// Clear all Values
        /// </summary>
        private void ClearNutrientValues()
        {
            dishCalorie = 0;
            dishCarboHydrates = 0;
            dishProtien = 0;
            dishFAT = 0;
            dishFibre = 0;
            dishIron = 0;
            dishCalcium = 0;
            dishPhosphorus = 0;
            dishVitaminARetinol = 0;
            dishVitaminABetaCarotene = 0;
            dishThiamine = 0;
            dishRiboflavin = 0;
            dishNicotinicAcid = 0;
            dishPyridoxine = 0;
            dishFolicAcid = 0;
            dishVitaminB12 = 0;
            dishVitaminC = 0;
        }

        /// <summary>
        /// Fill the Dropdown from DB
        /// </summary>
		private void FillCombo()
		{
            try
            {
                Classes.CommonFunctions.FillFoodHabit(cboFoodType);
                Classes.CommonFunctions.FillEthnic(cboEthnic);
                Classes.CommonFunctions.FillDishCategory(cboDishCategory);
                Classes.CommonFunctions.FillServeUnit(cboServeUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
		}

        /// <summary>
        /// Fill the Dropdown from XML
        /// </summary>
        public void FillComboFromXML()
        {
            try
            {
                XMLServices.GetXMLDataByLanguage(cboServeUnit, AppDomain.CurrentDomain.BaseDirectory + "\\XML\\ServeUnit.xml", 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillComboPlan()
        {
            string[] servingPlan = new string[3];
            if (CommonFunctions.Convert2Float(txtStandardWeight.Text) > 0)
            {
                servingPlan[0] = txtStandardWeight.Text;
                if (CommonFunctions.Convert2Float(txtStandardWeight1.Text) > 0)
                {
                    servingPlan[1] = txtStandardWeight1.Text;
                    if (CommonFunctions.Convert2Float(txtStandardWeight2.Text) > 0)
                    {
                        servingPlan[2] = txtStandardWeight2.Text;
                    }
                }
            }

            if (servingPlan.Length > 0)
            {
                cboPlan.ItemsSource = servingPlan;
                cboPlan.SelectedIndex = 0;
            }
        }
     
        /// <summary>
        /// Load Display mode OR Addition mode
        /// </summary>
        public void SetDisplayStyle()
        {
            isIngredientsLoad = false;
            isPlan1Loaded = false;
            isPlan2Loaded = false;
            isPlan3Loaded = false;
            isDishValueChanged = false;
            isIngredientValueChanged = false;
            isSelectionChanged = false;            

            lblDishName.Visibility = Visibility.Hidden;
            imgDisplay.ImageSource = string.Empty;
            IsImageAdd = true;

            CreateMode();
        }       

        /// <summary>
        /// Set the templates as entry mode
        /// </summary>
        private void CreateMode()
        {
            App apps = (App)Application.Current;
            ControlsDefaultStyle();

            imgStar1.Visibility = Visibility.Visible;
            imgStar2.Visibility = Visibility.Visible;
            imgStar3.Visibility = Visibility.Visible;
            imgStar4.Visibility = Visibility.Visible;
            imgStar5.Visibility = Visibility.Visible;
            imgStar6.Visibility = Visibility.Visible;
            imgStar7.Visibility = Visibility.Visible;
            imgStarMain.Visibility = Visibility.Visible;
            lblMandatory.Visibility = Visibility.Visible;

            btnAddImage.Visibility = Visibility.Visible;            
            btnAddImage.Content = uploadCaption;
            btnSelectIngredient.Visibility = Visibility.Visible;            
            btnSave.Visibility = Visibility.Visible;
            btnBack.Visibility = Visibility.Visible;
            btnSetPlan.Visibility = Visibility.Hidden;

            gvNameCol.FixedWidth = (int)GridColumnWidth.CreateDishNameWidthII;
            gvDisplayOrderCol.FixedWidth = (int)GridColumnWidth.CreateDishDisplayOrderII;

            gvIconCol.CellTemplate = this.FindResource("DeleteTemplate") as DataTemplate;
            gvQuantityCol.CellTemplate = this.FindResource("QuantityTemplate") as DataTemplate;
            gvWeightCol.CellTemplate = this.FindResource("GramWeightTemplate") as DataTemplate;
            gvUnitCol.CellTemplate = this.FindResource("UnitTemplate") as DataTemplate;
            gvDisplayOrderCol.CellTemplate = this.FindResource("OrderTemplate") as DataTemplate;
            gvSectionCol.CellTemplate = this.FindResource("SectionTemplate") as DataTemplate;

            SetButtonVisibility();
        }

        private void SetButtonVisibility()
        {
            if (dishID > 0)
            {
                btnPrint.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btnNutriDetails.Visibility = Visibility.Visible;
            }
            else
            {
                btnPrint.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnNutriDetails.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Set the controls in entry mode
        /// </summary>
        private void ControlsDefaultStyle()
        {
            App apps = (App)Application.Current;

            cboDishCategory.Visibility = Visibility.Visible;
            cboEthnic.Visibility = Visibility.Visible;
            cboFoodType.Visibility = Visibility.Visible;
            cboServeUnit.Visibility = Visibility.Visible;

            txtDishCategory.Visibility = Visibility.Hidden;
            txtEthnic.Visibility = Visibility.Hidden;
            txtFoodType.Visibility = Visibility.Hidden;
            txtServeUnit.Visibility = Visibility.Hidden;

            txtTitleENG.Style = (Style)apps.SetStyle["TextStyle"];
            txtTitleREG.Style = (Style)apps.SetStyle["TextStyle"];
            txtTitleDisplay.Style = (Style)apps.SetStyle["TextStyle"];
            txtShelfLife.Style = (Style)apps.SetStyle["TextStyle"];
            txtRefrigeratedLife.Style = (Style)apps.SetStyle["TextStyle"];
            txtFrozenLife.Style = (Style)apps.SetStyle["TextStyle"];
            txtCookTime.Style = (Style)apps.SetStyle["TextStyle"];            
            txtProcessENG.Style = (Style)apps.SetStyle["TextStyle"];
            txtProcessREG.Style = (Style)apps.SetStyle["TextStyle"];
            txtRemarks.Style = (Style)apps.SetStyle["TextStyle"];
            txtRemarksREG.Style = (Style)apps.SetStyle["TextStyle"];
            txtServingSize.Style = (Style)apps.SetStyle["TextDisableStyle"];
            txtServingSize1.Style = (Style)apps.SetStyle["TextDisableStyle"];
            txtServingSize2.Style = (Style)apps.SetStyle["TextDisableStyle"];
            txtStandardWeight.Style = (Style)apps.SetStyle["TextStyle"];
            txtStandardWeight1.Style = (Style)apps.SetStyle["TextStyle"];
            txtStandardWeight2.Style = (Style)apps.SetStyle["TextStyle"];
        }

        /// <summary>
        /// Load the existing Ingredient List
        /// </summary>
        public void LoadIngredientList()
        {
            try
            {
                if (dishID > 0)
                {
                    double totalWeight = 0;
                    bool isCalculate = true;
                    if ((Convert.ToInt32(cboDishCategory.SelectedValue) == (int)DishCategoryList.ConvenientFoods) || Convert.ToInt32(cboDishCategory.SelectedValue) == (int)DishCategoryList.FreshFoods)
                    {
                        isCalculate = false;
                    }

                    dishIngredientList = DishIngredientManager.GetList(dishID);
                    if (dishIngredientList != null)
                    {
                        foreach (DishIngredient dishIngredientItem in dishIngredientList)
                        {
                            if (IsDisplayMode == false || (bool)Application.Current.Properties["SYSTEM"] == false)
                            {
                                ingredientUnitList = IngredientStandardUnitManager.GetUnitList(dishIngredientItem.IngredientID);
                                if (ingredientUnitList != null)
                                {
                                    dishIngredientItem.IngredientUnitList = ingredientUnitList;
                                    dishIngredientItem.GramWeight = dishIngredientItem.Quantity * dishIngredientItem.StandardWeight;

                                    if (dishIngredientItem.WeightChangeRate == 0)
                                        dishIngredientItem.WeightChangeRate = 100;

                                    if (!isCalculate)
                                    {
                                        totalWeight = totalWeight + dishIngredientItem.GramWeight;
                                    }
                                    else
                                    {
                                        totalWeight = totalWeight + (dishIngredientItem.GramWeight * dishIngredientItem.WeightChangeRate / 100);
                                    }
                                }
                            }
                            else
                            {
                                ingredientUnitItem = IngredientStandardUnitManager.GetItem(dishIngredientItem.IngredientID, dishIngredientItem.StandardUnitID);
                                if (ingredientUnitItem != null)
                                {
                                    dishIngredientItem.StandardUnitDisplay = ingredientUnitItem.StandardUnitName;
                                    dishIngredientItem.GramWeight = dishIngredientItem.Quantity * ingredientUnitItem.StandardWeight;

                                    if (dishIngredientItem.WeightChangeRate == 0)
                                        dishIngredientItem.WeightChangeRate = 100;

                                    if (!isCalculate)
                                    {
                                        totalWeight = totalWeight + dishIngredientItem.GramWeight;
                                    }
                                    else
                                    {
                                        totalWeight = totalWeight + (dishIngredientItem.GramWeight * dishIngredientItem.WeightChangeRate / 100);
                                    }
                                }
                            }
                        }
                        lvDishIngredient.ItemsSource = dishIngredientList;

                        if (!isCalculate)
                        {
                            txtTotalWeightvalue.Text = Convert.ToString(Math.Round(totalWeight, 0));
                        }
                        else
                        {
                            totalWeight = CommonFunctions.RoundUp(((CommonFunctions.Convert2Double(Convert.ToString(totalWeight)) == 0 ? 0 : CommonFunctions.Convert2Double(Convert.ToString(totalWeight)) / 50)), 0) * 50;
                            txtTotalWeightvalue.Text = Convert.ToString(totalWeight);
                        }

                        if (CommonFunctions.Convert2Float(txtStandardWeight.Text) > 0)
                        {
                            if (CommonFunctions.Convert2Float(txtTotalWeightvalue.Text) > 0)
                            {
                                txtServingSize.Text = Convert.ToString(CommonFunctions.RoundDown(((CommonFunctions.Convert2Float(txtTotalWeightvalue.Text) / CommonFunctions.Convert2Float(txtStandardWeight.Text)) / .5), 0) * .5);
                            }
                        }
                        if (CommonFunctions.Convert2Float(txtStandardWeight1.Text) > 0)
                        {
                            if (CommonFunctions.Convert2Float(txtTotalWeightvalue.Text) > 0)
                            {
                                txtServingSize1.Text = Convert.ToString(CommonFunctions.RoundDown(((CommonFunctions.Convert2Float(txtTotalWeightvalue.Text) / CommonFunctions.Convert2Float(txtStandardWeight1.Text)) / .5), 0) * .5);
                            }
                        }
                        if (CommonFunctions.Convert2Float(txtStandardWeight2.Text) > 0)
                        {
                            if (CommonFunctions.Convert2Float(txtTotalWeightvalue.Text) > 0)
                            {
                                txtServingSize2.Text = Convert.ToString(CommonFunctions.RoundDown(((CommonFunctions.Convert2Float(txtTotalWeightvalue.Text) / CommonFunctions.Convert2Float(txtStandardWeight2.Text)) / .5), 0) * .5);
                            }
                        }

                        TotalWeightvalue = totalWeight;
                    }
                }
                else
                {
                    dishIngredientList.Clear();
                    lvDishIngredient.ItemsSource = dishIngredientList;
                    lvDishIngredient.Items.Refresh();

                    dishIngredientNutrientsList.Clear();
                    lvIngNutrients.ItemsSource = dishIngredientNutrientsList;
                    lvIngNutrients.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void LoadDishNutrients(int plan)
        {
            if (dishID > 0)
            {
                ingredientNutrientsList = IngredientNutrientsManager.GetNutrientsList(dishID, plan);
                if (ingredientNutrientsList != null)
                {
                    lvNutrientValues.ItemsSource = ingredientNutrientsList;
                    lvNutrientValues.Items.Refresh();
                }

                ingredientAminoAcidList = IngredientAminoAcidManager.GetAminoAcidsList(dishID, plan);
                if (ingredientAminoAcidList != null)
                {
                    lvAmino.ItemsSource = ingredientAminoAcidList;
                    lvAmino.Items.Refresh();
                }

                ingredientFattyAcidList = IngredientFattyAcidManager.GetFattyAcidsList(dishID, plan);
                if (ingredientFattyAcidList != null)
                {
                    lvFattyAcid.ItemsSource = ingredientFattyAcidList;
                    lvFattyAcid.Items.Refresh();
                }
            }
            else
            {
                LoadNutrientsParameters();
                LoadAminoAcidsParameters();
                LoadFattyAcidsParameters();
            }
        }

        private void LoadIngredientNutrients()
        {
            FillComboPlan();
            double servingSize = 0;

            lblSCarbo.Content = string.Empty;
            lblSCalorie.Content = string.Empty;
            lblSProtein.Content = string.Empty;
            lblSFat.Content = string.Empty;
            lblSFiber.Content = string.Empty;
            lblSIron.Content = string.Empty;
            lblSCalcium.Content = string.Empty;

            calorieSum = 0;
            carbohydratesSum = 0;
            fatSum = 0;
            proteinSum = 0;
            fiberSum = 0;
            ironSum = 0;
            calciumSum = 0;

            if (dishID > 0)
            {
                servingSize = CommonFunctions.Convert2Double(txtServingSize.Text) * CommonFunctions.Convert2Double(txtStandardWeight.Text);
                dishIngredientNutrientsList = DishIngredientManager.GetIngredientNutrientList(dishID);
                if (dishIngredientNutrientsList != null)
                {
                    lvIngNutrients.ItemsSource = dishIngredientNutrientsList;
                    lvIngNutrients.Items.Refresh();
                    for (int i = 0; i < dishIngredientNutrientsList.Count; i++)
                    {
                        calorieSum = calorieSum + dishIngredientNutrientsList[i].Calorie;
                        carbohydratesSum = carbohydratesSum + dishIngredientNutrientsList[i].CarboHydrate;
                        fatSum = fatSum + dishIngredientNutrientsList[i].Fat;
                        proteinSum = proteinSum + dishIngredientNutrientsList[i].Protien;
                        fiberSum = fiberSum + dishIngredientNutrientsList[i].Fibre;
                        ironSum = ironSum + dishIngredientNutrientsList[i].Iron;
                        calciumSum = calciumSum + dishIngredientNutrientsList[i].Calcium;
                    }

                    lblSCalorie.Content = Convert.ToString(Math.Round((calorieSum / servingSize) * CommonFunctions.Convert2Double(txtStandardWeight.Text), 2));
                    lblSProtein.Content = Convert.ToString(Math.Round((proteinSum / servingSize) * CommonFunctions.Convert2Double(txtStandardWeight.Text), 2));                    
                    lblSCarbo.Content = Convert.ToString(Math.Round((carbohydratesSum / servingSize) * CommonFunctions.Convert2Double(txtStandardWeight.Text), 2));
                    lblSFat.Content = Convert.ToString(Math.Round((fatSum / servingSize) * CommonFunctions.Convert2Double(txtStandardWeight.Text), 2));
                    lblSFiber.Content = Convert.ToString(Math.Round((fiberSum / servingSize) * CommonFunctions.Convert2Double(txtStandardWeight.Text), 2));
                    lblSIron.Content = Convert.ToString(Math.Round((ironSum / servingSize) * CommonFunctions.Convert2Double(txtStandardWeight.Text), 2));
                    lblSCalcium.Content = Convert.ToString(Math.Round((calciumSum / servingSize) * CommonFunctions.Convert2Double(txtStandardWeight.Text), 2));
                }
            }
        }

        /// <summary>
        /// Update the Ingredient List with new Ingredients
        /// </summary>
        public void UpdateIngredientList()
        {
            try
            {
                int newListStartIndex = -1;
                int dishIngredientID = 0;
                ingredientID.Clear();
                IngredientStandardUnit ingredientStandardUnit = new IngredientStandardUnit();
                DetailSearch detailSearch = new DetailSearch();
                detailSearch.SearchType = 0;
                detailSearch.Owner = Application.Current.MainWindow;
                detailSearch.ShowDialog();
                if (ingredientID.Count > 0)
                {
                    for (int i = 0; i < ingredientID.Count; i++)
                    {
                        Ingredient ingredientItem = new Ingredient();
                        DishIngredient dishIngredient = new DishIngredient();

                        dishIngredientID = CommonFunctions.Convert2Int(Convert.ToString(ingredientID[i]));
                        ingredientItem = IngredientManager.GetItem(dishIngredientID);
                        if (ingredientItem != null)
                        {
                            //if (!CommonFunctions.IsIngredientExists(dishIngredientList, dishIngredientID))
                            //{
                                dishIngredient.DishId = dishID;
                                dishIngredient.IngredientID = dishIngredientID;
                                dishIngredient.IngredientName = ingredientItem.Name;
                                dishIngredient.DisplayName = ingredientItem.DisplayName;
                                dishIngredient.Quantity = 0;
                                dishIngredient.SectionName = string.Empty;
                                dishIngredientList.Add(dishIngredient);
                                if (newListStartIndex < 0)
                                {
                                    if (dishIngredientList.Count > 0)
                                    {
                                        newListStartIndex = dishIngredientList.Count - 1;
                                    }
                                }
                            //}
                            //else
                            //{
                            //    AlertBox.Show(Convert.ToString(ingredientItem.Name) + " already added", "Information", AlertType.Information, AlertButtons.OK);
                            //}
                        }
                    }

                    foreach (DishIngredient dishIngredientItem in dishIngredientList)
                    {
                        dishIngredientItem.IngredientUnitList = null;
                        ingredientUnitList = IngredientStandardUnitManager.GetUnitList(dishIngredientItem.IngredientID);
                        if (ingredientUnitList != null)
                        {
                            dishIngredientItem.IngredientUnitList = ingredientUnitList;
                        }
                    }

                    if (dishIngredientList != null)
                    {
                        lvDishIngredient.ItemsSource = dishIngredientList;
                        lvDishIngredient.Items.Refresh();

                        if (lvDishIngredient.Items.Count > 0)
                        {
                            lvDishIngredient.SelectedIndex = newListStartIndex;
                            if (lvDishIngredient.SelectedItem != null)
                            {
                                lvDishIngredient.ScrollIntoView(lvDishIngredient.SelectedItem);
                                lvDishIngredient.Focus();

                                ItemContainerGenerator generator = this.lvDishIngredient.ItemContainerGenerator;
                                ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(lvDishIngredient.SelectedIndex);
                                TextBox tbFind = ListViewHelper.GetDescendantByType(selectedItem, typeof(TextBox), "txtQuantity") as TextBox;
                                if (tbFind != null)
                                {
                                    CommonFunctions.SetControlFocus(tbFind);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
       
        /// <summary>
        /// Delete the selected Ingredient from the List
        /// </summary>
        private void RemoveIngredientItem()
        {
            try
            {
                isDishValueChanged = true;
                isIngredientValueChanged = true;
                DishIngredient dishIngredientItem = new DishIngredient();                
                dishIngredientItem = ((DishIngredient)lvDishIngredient.Items[lvDishIngredient.SelectedIndex]);
                ingredientDeleteList.Add(dishIngredientItem);
        
                lvDishIngredient.ItemsSource = string.Empty;
                dishIngredientList.Remove(dishIngredientItem);

                foreach (DishIngredient dishIngrItem in dishIngredientList)
                {
                    ingredientUnitList = IngredientStandardUnitManager.GetUnitList(dishIngrItem.IngredientID);
                    if (ingredientUnitList != null)
                    {
                        dishIngrItem.IngredientUnitList = ingredientUnitList;
                    }
                }

                if (dishIngredientList != null)
                {
                    lvDishIngredient.ItemsSource = dishIngredientList;
                    lvDishIngredient.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Delete the selected Ingredients from DB
        /// </summary>
        private void DeleteIngredientList()
        {
            foreach (DishIngredient dishIngredientItem in ingredientDeleteList)
            {
                DishIngredientManager.Delete(dishIngredientItem);
            }
        }

        private void DeleteDish()
        {
            if (dishID > 0)
            {
                string dishName = string.Empty;
                if (dish != null)
                {
                    dishName = dish.Name;
                    if (DishManager.GetDishCount(dish) <= 0)
                    {
                        bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1163") + dishName, "", AlertType.Exclamation, AlertButtons.YESNO);
                        if (result == true)
                        {
                            if (DishManager.DeleteDish(dish))
                            {
                                ClearAllValues();
                                AlertBox.Show(dishName + XMLServices.GetXmlMessage("E1164"), "", AlertType.Information, AlertButtons.OK);                                
                            }
                            else
                            {
                                AlertBox.Show(XMLServices.GetXmlMessage("E1165") + dishName, "", AlertType.Information, AlertButtons.OK);
                            }
                        }
                    }
                    else
                    {
                        AlertBox.Show(dishName + XMLServices.GetXmlMessage("E1166"), "", AlertType.Information, AlertButtons.OK);
                    }
                }
            }
        }
        
        /// <summary>
        /// Get the Dish details for DB Updation
        /// </summary>
        private void GetDishList()
		{
			try
			{
                string imageDestination;
				dish = new Dish();
				if (dishID > 0)
				{
					dish.Id = dishID;
				}
				else
				{
					dish.Id = DishManager.GetID();
				}
                
                dish.EthnicID = CommonFunctions.Convert2Byte(Convert.ToString(cboEthnic.SelectedValue));
                dish.FoodHabitID = CommonFunctions.Convert2Byte(Convert.ToString(cboFoodType.SelectedValue));
                dish.DishCategoryID = CommonFunctions.Convert2Byte(Convert.ToString(cboDishCategory.SelectedValue));
                
                dish.MedicalFavourable = string.Empty;
                dish.MedicalUnFavourable = string.Empty;
                dish.AyurvedicFavourable = string.Empty;
                dish.AyurvedicUnFavourable = string.Empty;

                dish.Keywords = string.Empty;
                dish.CreatedDate = DateTime.Now;

				dish.IsSystemDish = false;
                dish.IsAyurvedic = false;

                if (Classes.CommonFunctions.Convert2Int(txtServingSize.Text) > 0)
				{
					dish.IsCountable = true;
				}
				else
				{
					dish.IsCountable = false;
				}

                dish.ItemCount = 0;
				dish.ServeCount = CommonFunctions.Convert2Float(txtServingSize.Text.Trim());
                dish.ServeCount1 = CommonFunctions.Convert2Float(txtServingSize1.Text.Trim());
                dish.ServeCount2 = CommonFunctions.Convert2Float(txtServingSize2.Text.Trim());
                dish.ServeUnit = CommonFunctions.Convert2Byte(Convert.ToString(cboServeUnit.SelectedValue));
                int cooktime = 0;
                int.TryParse(txtCookTime.Text.Trim(), out cooktime);
                dish.CookingTime = CommonFunctions.Convert2String(cooktime.ToString());

				dish.FrozenLife = CommonFunctions.Convert2Int(txtFrozenLife.Text.Trim());
				dish.RefrigeratedLife = CommonFunctions.Convert2Int(txtRefrigeratedLife.Text.Trim());
				dish.ShelfLife = CommonFunctions.Convert2Int(txtShelfLife.Text.Trim());
				dish.StandardWeight = CommonFunctions.Convert2Float(txtStandardWeight.Text.Trim());
                dish.StandardWeight1 = CommonFunctions.Convert2Float(txtStandardWeight1.Text.Trim());
                dish.StandardWeight2 = CommonFunctions.Convert2Float(txtStandardWeight2.Text.Trim());
                dish.DishWeight = CommonFunctions.Convert2Float(txtTotalWeightvalue.Text.Trim());

                dish.PlanWeight = planWeight;
                dish.PlanWeight1 = planWeight1;
                dish.PlanWeight2 = planWeight2;                

                dish.AuthorID = (byte)RecipeSource.Users;

				// Dish Lan (Primary)
				dish.Name = CommonFunctions.Convert2String(txtTitleENG.Text.Trim());
                if (txtTitleDisplay.Text.Trim() != string.Empty)
                {
                    dish.DisplayName = CommonFunctions.Convert2String(txtTitleDisplay.Text.Trim());
                }
                else
                {
                    dish.DisplayName = CommonFunctions.Convert2String(txtTitleENG.Text.Trim());
                }
				dish.DishRecipe = CommonFunctions.Convert2String(txtProcessENG.Text.Trim());
				dish.Comments = CommonFunctions.Convert2String(txtRemarks.Text.Trim());
                dish.DishAyurFeatures = string.Empty;				
				dish.RegionalName = CommonFunctions.Convert2String(txtTitleREG.Text.Trim());

                // Dish Ingredient List
                GetIngredientsList();

                dish.Calorie = (float)Math.Round(Convert.ToDecimal(dishCalorie),2);
                dish.Protien = (float)Math.Round(Convert.ToDecimal(dishProtien),2);
                dish.CarboHydrates = (float)Math.Round(Convert.ToDecimal(dishCarboHydrates),2);
                dish.FAT = (float)Math.Round(Convert.ToDecimal(dishFAT),2);
                dish.Fibre = (float)Math.Round(Convert.ToDecimal(dishFibre),2);
                dish.Iron = (float)Math.Round(Convert.ToDecimal(dishIron),2);
                dish.Calcium = (float)Math.Round(Convert.ToDecimal(dishCalcium),2);
                dish.Phosphorus = (float)Math.Round(Convert.ToDecimal(dishPhosphorus),2);
                dish.VitaminARetinol = (float)Math.Round(Convert.ToDecimal(dishVitaminARetinol),2);
                dish.VitaminABetaCarotene = (float)Math.Round(Convert.ToDecimal(dishVitaminABetaCarotene),2);
                dish.Thiamine = (float)Math.Round(Convert.ToDecimal(dishThiamine),2);
                dish.Riboflavin = (float)Math.Round(Convert.ToDecimal(dishRiboflavin),2);
                dish.NicotinicAcid = (float)Math.Round(Convert.ToDecimal(dishNicotinicAcid),2);
                dish.Pyridoxine = (float)Math.Round(Convert.ToDecimal(dishPyridoxine),2);
                dish.FolicAcid = (float)Math.Round(Convert.ToDecimal(dishFolicAcid),2);
                dish.VitaminB12 = (float)Math.Round(Convert.ToDecimal(dishVitaminB12),2);
                dish.VitaminC = (float)Math.Round(Convert.ToDecimal(dishVitaminC), 2);

                if (imageFileName != string.Empty)
                {
                    //imageDestination = GetImagePath("Dishes") + "\\" + CommonFunctions.EncryptString(Convert.ToString(dish.Id)) + ".jpg";
                    imageDestination = GetImagePath("Dishes") + "\\" + dish.Id + ".jpg";
                    if (imageFileName.Trim() != imageDestination.Trim())
                    {
                        if (File.Exists(imageFileName))
                        {
                            dish.DisplayImage = CopyFile(imageFileName, imageDestination);
                        }
                        else
                        {
                            dish.DisplayImage = imageFileName;
                        }
                    }
                    else
                    {
                        dish.DisplayImage = imageFileName;
                    }
                }
                else
                {
                    dish.DisplayImage = string.Empty;
                    imgDisplay.ImageSource = "";

                }
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
			finally
			{

			}
		}
                
        /// <summary>
        /// Get the Ingredients List for DB Updation
        /// </summary>
		private void GetIngredientsList()
		{
            string nutriName = string.Empty;
            float totalYieldWeight = CommonFunctions.Convert2Float(txtServingSize.Text) * CommonFunctions.Convert2Float(txtStandardWeight.Text);
            NSysNutrient sysNutrientItem = new NSysNutrient();
            List<DishIngredient> dishIngredientUpdateList = new List<DishIngredient>();
            IngredientStandardUnit ingredientStandardUnitItem = new IngredientStandardUnit();
            IngredientNutrients ingredientNutrientsItem = new IngredientNutrients();
			try
			{
                ClearNutrientValues();
                for (int i = 0; i < lvDishIngredient.Items.Count; i++)
                {
                    dishIngredient = new DishIngredient();
                    dishIngredient.DishId = dish.Id;
                    dishIngredient.DisplayOrder = ((DishIngredient)lvDishIngredient.Items[i]).DisplayOrder;
                    dishIngredient.IngredientID = ((DishIngredient)lvDishIngredient.Items[i]).IngredientID;
                    dishIngredient.IngredientName = ((DishIngredient)lvDishIngredient.Items[i]).IngredientName;
                    dishIngredient.Quantity = ((DishIngredient)lvDishIngredient.Items[i]).Quantity;
                    dishIngredient.StandardUnitID = ((DishIngredient)lvDishIngredient.Items[i]).StandardUnitID;

                    ingredientStandardUnitItem = IngredientStandardUnitManager.GetItem(dishIngredient.IngredientID, dishIngredient.StandardUnitID);
                    if (ingredientStandardUnitItem != null)
                    {
                        //if (ingredientStandardUnitItem.StandardUnitType != (int)StandardUnitType.TypeII)
                        //{
                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Calorie));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Calorie);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishCalorie = dishCalorie + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishCalorie = dishCalorie + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Protien));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Protien);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishProtien = dishProtien + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishProtien = dishProtien + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.CarboHydrates));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.CarboHydrates);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishCarboHydrates = dishCarboHydrates + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishCarboHydrates = dishCarboHydrates + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.FAT));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.FAT);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishFAT = dishFAT + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishFAT = dishFAT + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Fibre));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Fibre);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishFibre = dishFibre + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishFibre = dishFibre + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Iron));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Iron);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishIron = dishIron + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishIron = dishIron + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Calcium));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Calcium);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishCalcium = dishCalcium + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishCalcium = dishCalcium + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Phosphorus));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Phosphorus);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishPhosphorus = dishPhosphorus + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishPhosphorus = dishPhosphorus + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //nutriName = Classes.CommonFunctions.GetDescription(MainNutrients.VitaminA_Retinol);
                        //sysNutrientItem = SysNutrientManager.GetNutrientID(nutriName);
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.VitaminA_Retinol);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishVitaminARetinol = dishVitaminARetinol + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishVitaminARetinol = dishVitaminARetinol + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //nutriName = Classes.CommonFunctions.GetDescription(MainNutrients.VitaminA_BetaCarotene);
                        //sysNutrientItem = SysNutrientManager.GetNutrientID(nutriName);
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.VitaminA_BetaCarotene);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishVitaminABetaCarotene = dishVitaminABetaCarotene + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishVitaminABetaCarotene = dishVitaminABetaCarotene + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Thiamine));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Thiamine);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishThiamine = dishThiamine + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishThiamine = dishThiamine + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Riboflavin));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Riboflavin);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishRiboflavin = dishRiboflavin + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishRiboflavin = dishRiboflavin + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.NicotinicAcid));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.NicotinicAcid);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishNicotinicAcid = dishNicotinicAcid + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishNicotinicAcid = dishNicotinicAcid + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Pyridoxine));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.Pyridoxine);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishPyridoxine = dishPyridoxine + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishPyridoxine = dishPyridoxine + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.FolicAcid));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.FolicAcid);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishFolicAcid = dishFolicAcid + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishFolicAcid = dishFolicAcid + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.VitaminB12));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.VitaminB12);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishVitaminB12 = dishVitaminB12 + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishVitaminB12 = dishVitaminB12 + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}

                        //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.VitaminC));
                        //if (sysNutrientItem != null)
                        //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(dishIngredient.IngredientID, (int)MainNutrients.VitaminC);
                            if (ingredientNutrientsItem != null)
                            {
                                //dishVitaminC = dishVitaminC + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                dishVitaminC = dishVitaminC + ((dishIngredient.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight);
                            }
                        //}
                        //}
                    }

                    dishIngredient.SectionName = CommonFunctions.Convert2String(((DishIngredient)lvDishIngredient.Items[i]).SectionName);
                    dishIngredientUpdateList.Add(dishIngredient);
                }

                if (dishIngredientUpdateList != null)
                {
                    dish.DishIngredientList = dishIngredientUpdateList;
                }
                else
                {
                    if (dishIngredientList != null)
                    {
                        dish.DishIngredientList = dishIngredientList;
                    }
                }
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
			finally
			{

			}
		}

        private void ShowPlanNutrients()
        {
            int plan = 0;
            switch (cboPlan.SelectedIndex)
            {
                case 0:
                    plan = (int)MenuPlan.PLANI;
                    break;
                case 1:
                    plan = (int)MenuPlan.PLANII;
                    break;
                case 2:
                    plan = (int)MenuPlan.PLANIII;
                    break;
            }

            if (isDishValueChanged == true)
            {
                btnSave.Focus();

                if (plan == (int)MenuPlan.PLANI)
                {
                    if (ingredientNutrientsPlan1List != null)
                    {
                        if (ingredientNutrientsPlan1List.Count > 0)
                        {
                            isPlan1Loaded = true;
                            lvNutrientValues.ItemsSource = ingredientNutrientsPlan1List;
                        }
                    }
                    if (ingredientAminoAcidPlan1List != null)
                    {
                        if (ingredientAminoAcidPlan1List.Count > 0)
                        {
                            isPlan1Loaded = true;
                            lvAmino.ItemsSource = ingredientAminoAcidPlan1List;
                        }
                    }
                    if (ingredientFattyAcidPlan1List != null)
                    {
                        if (ingredientFattyAcidPlan1List.Count > 0)
                        {
                            isPlan1Loaded = true;
                            lvFattyAcid.ItemsSource = ingredientFattyAcidPlan1List;
                        }
                    }

                    if (isPlan1Loaded == false)
                    {
                        using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                        {
                            CalculateNutrientValues();
                        }
                    }
                }
                else if (plan == (int)MenuPlan.PLANII)
                {
                    if (ingredientNutrientsPlan2List != null)
                    {
                        if (ingredientNutrientsPlan2List.Count > 0)
                        {
                            isPlan2Loaded = true;
                            lvNutrientValues.ItemsSource = ingredientNutrientsPlan2List;
                        }
                    }
                    if (ingredientAminoAcidPlan2List != null)
                    {
                        if (ingredientAminoAcidPlan2List.Count > 0)
                        {
                            isPlan2Loaded = true;
                            lvAmino.ItemsSource = ingredientAminoAcidPlan2List;
                        }
                    }
                    if (ingredientFattyAcidPlan2List != null)
                    {
                        if (ingredientFattyAcidPlan2List.Count > 0)
                        {
                            isPlan2Loaded = true;
                            lvFattyAcid.ItemsSource = ingredientFattyAcidPlan2List;
                        }
                    }

                    if (isPlan2Loaded == false)
                    {
                        using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                        {
                            CalculateNutrientValues();
                        }
                    }
                }
                else if (plan == (int)MenuPlan.PLANIII)
                {
                    if (ingredientNutrientsPlan3List != null)
                    {
                        if (ingredientNutrientsPlan3List.Count > 0)
                        {
                            isPlan3Loaded = true;
                            lvNutrientValues.ItemsSource = ingredientNutrientsPlan3List;
                        }
                    }
                    if (ingredientAminoAcidPlan3List != null)
                    {
                        if (ingredientAminoAcidPlan3List.Count > 0)
                        {
                            isPlan3Loaded = true;
                            lvAmino.ItemsSource = ingredientAminoAcidPlan3List;
                        }
                    }
                    if (ingredientFattyAcidPlan3List != null)
                    {
                        if (ingredientFattyAcidPlan3List.Count > 0)
                        {
                            isPlan3Loaded = true;
                            lvFattyAcid.ItemsSource = ingredientFattyAcidPlan3List;
                        }
                    }

                    if (isPlan3Loaded == false)
                    {
                        using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                        {
                            CalculateNutrientValues();
                        }
                    }
                }

                if (isPlan1Loaded == true && isPlan2Loaded == true && isPlan3Loaded == true)
                {
                    isDishValueChanged = false;
                }
            }
            else
            {
                using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                {
                    LoadDishNutrients(plan);
                }
            }
        }

        /// <summary>
        /// Display the details of Individual tab On Click
        /// </summary>
        private void TabDisplay()
        {
            switch (tbDish.SelectedIndex)
            {
                case 0:
                    CommonFunctions.SetControlFocus(txtTitleENG);
                    break;
                case 1:
                    CommonFunctions.SetControlFocus(btnSelectIngredient);
                    if (isIngredientsLoad == false)
                    {
                        LoadIngredientList();
                        isIngredientsLoad = true;
                    }                    
                    break;
                case 2:
                    CommonFunctions.SetControlFocus(txtProcessENG);
                    break;
                case 3:
                    
                    CommonFunctions.SetControlFocus(cboPlan);
                    if (isIngredientValueChanged == true)
                    {
                        using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                        {
                            CalculateIngredientNutrients(CommonFunctions.Convert2Float(txtServingSize.Text) * CommonFunctions.Convert2Float(txtStandardWeight.Text), CommonFunctions.Convert2Float(txtStandardWeight.Text));
                            isIngredientValueChanged = false;
                        }
                    }                    
                    break;                
            }
        }

        /// <summary>
        /// Validations before DB Updation
        /// </summary>
        /// <returns></returns>
        private bool ValidateDish()
        {
            bool isValidate = false;
            if (txtTitleENG.Text.Trim() == string.Empty)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1040"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 0;
                txtTitleENG.Focus();
                return isValidate;
            }
            else if (txtTitleDisplay.Text == string.Empty)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1276"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 0;
                txtTitleDisplay.Focus();
                return isValidate;
            }
            else if (cboEthnic.SelectedIndex <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1155"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 0;
                cboEthnic.Focus();
                return isValidate;
            }
            else if (cboFoodType.SelectedIndex <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1156"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 0;
                cboFoodType.Focus();
                return isValidate;
            }
            else if (cboDishCategory.SelectedIndex <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1157"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 0;
                cboDishCategory.Focus();
                return isValidate;
            }
            else if (lvDishIngredient.Items.Count == 0)
            {
                //AlertBox.Show(XMLServices.GetXmlMessage("E1073"), "", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1161"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 1;
                btnSelectIngredient.Focus();
                return isValidate;
            }
            else if (lvDishIngredient.Items.Count < 1)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1162"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 1;
                btnSelectIngredient.Focus();
                return isValidate;
            }

            for (int i = 0; i < lvDishIngredient.Items.Count; i++)
            {
                if (((DishIngredient)lvDishIngredient.Items[i]).StandardUnitType != (int)StandardUnitType.TypeII)
                {
                    if (((DishIngredient)lvDishIngredient.Items[i]).Quantity == 0.0)
                    {
                        AlertBox.Show(XMLServices.GetXmlMessage("E1167"), "", AlertType.Information, AlertButtons.OK);
                        tbDish.SelectedIndex = 1;
                        return isValidate;
                    }
                    if (((DishIngredient)lvDishIngredient.Items[i]).StandardUnitID == 0)
                    {
                        AlertBox.Show(XMLServices.GetXmlMessage("E1168"), "", AlertType.Information, AlertButtons.OK);
                        tbDish.SelectedIndex = 1;
                        return isValidate;
                    }
                }
            }
           
            if (cboServeUnit.SelectedIndex <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1159"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 1;
                cboServeUnit.Focus();
                return isValidate;
            }
            else if (CommonFunctions.Convert2Int(txtStandardWeight.Text) <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1160"), "", AlertType.Information, AlertButtons.OK);
                tbDish.SelectedIndex = 1;
                txtServingSize.Focus();
                return isValidate;
            }
            else if (CommonFunctions.Convert2Int(txtStandardWeight.Text) > 0 && CommonFunctions.Convert2Double(txtServingSize.Text) == 0)
            {
                AlertBox.Show("Enter a Valid Standard Weight for Plan 1", "", AlertType.Information, AlertButtons.OK);
                Keyboard.Focus(txtStandardWeight);
                return isValidate;
            }
            else if (CommonFunctions.Convert2Int(txtStandardWeight1.Text) > 0 && CommonFunctions.Convert2Double(txtServingSize1.Text) == 0)
            {
                AlertBox.Show("Enter a Valid Standard Weight for Plan 2", "", AlertType.Information, AlertButtons.OK);
                Keyboard.Focus(txtStandardWeight1);
                return isValidate;
            }
            else if (CommonFunctions.Convert2Int(txtStandardWeight2.Text) > 0 && CommonFunctions.Convert2Double(txtServingSize2.Text) == 0)
            {
                AlertBox.Show("Enter a Valid Standard Weight for Plan 3", "", AlertType.Information, AlertButtons.OK);
                Keyboard.Focus(txtStandardWeight2);
                return isValidate;
            }
            return true;
        }
        
        /// <summary>
        /// Load the Dish details for edit Purpose 
        /// </summary>
        public void DisplayDishDetails()
        {
            try
            {
                txtEthnic.Text = string.Empty;
                txtFoodType.Text = string.Empty;
                txtDishCategory.Text = string.Empty;

                medicalFavourable = string.Empty;
                medicalUnFavourable = string.Empty;
                ayurvedicFavourable = string.Empty;
                ayurvedicUnFavourable = string.Empty;
                keyWords = string.Empty;

                txtStandardWeight.Text = string.Empty;
                txtStandardWeight1.Text = string.Empty;
                txtStandardWeight2.Text = string.Empty;

                using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                {
                    dish = DishManager.GetItem(dishID);
                    if (dish != null)
                    {
                        lblDishName.Visibility = Visibility.Visible;
                        if (dish.DisplayName != dish.Name)
                        {
                            lblDishName.Content = dish.Name + " / " + dish.DisplayName + " ( " + DishManager.GetDishAuthor(dish.AuthorID) + " Recipe )";
                        }
                        else
                        {
                            lblDishName.Content = dish.Name + " ( " + DishManager.GetDishAuthor(dish.AuthorID) + " Recipe )";
                        }                        

                        txtTitleENG.Text = dish.Name;                        
                        txtTitleREG.Text = dish.RegionalName;
                        txtShelfLife.Text = Convert.ToString(dish.ShelfLife);
                        txtRefrigeratedLife.Text = Convert.ToString(dish.RefrigeratedLife);
                        txtFrozenLife.Text = Convert.ToString(dish.FrozenLife);

                        txtProcessENG.Text = Convert.ToString(dish.DishRecipe);                        

                        txtRemarks.Text = Convert.ToString(dish.Comments);                        

                        btnAddImage.Content = uploadCaption;
                        txtCookTime.Text = Convert.ToString(dish.CookingTime);
                        planWeight = dish.StandardWeight;
                        planWeight1 = dish.StandardWeight1;
                        planWeight2 = dish.StandardWeight2;

                        txtPreWeight.Text = Convert.ToString(dish.StandardWeight);
                        txtPreWeight1.Text = Convert.ToString(dish.StandardWeight1);
                        txtPreWeight2.Text = Convert.ToString(dish.StandardWeight2);

                        txtStandardWeight.Text = Convert.ToString(dish.StandardWeight);
                        txtStandardWeight1.Text = Convert.ToString(dish.StandardWeight1);
                        txtStandardWeight2.Text = Convert.ToString(dish.StandardWeight2);

                        if (dish.DisplayImage != string.Empty)
                        {
                            //if (File.Exists(dish.DisplayImage))
                            string path = GetImagePath("Dishes") + "\\" + dish.Id + ".jpg";
                            //string path = GetImagePath("Dishes") + "\\" + CommonFunctions.EncryptString(Convert.ToString(dish.Id)) + ".jpg";
                            if (File.Exists(path))
                            {
                                imageFileName = path;
                                imageDefaultPath = path;
                                imgDisplay.ImagePath = path;
                                btnAddImage.Content = removeCaption;
                                IsImageAdd = false;
                            }
                            else
                            {
                                imageFileName = string.Empty;
                                imageDefaultPath = string.Empty;
                                imgDisplay.ImagePath = string.Empty;
                                btnAddImage.Content = uploadCaption;
                                IsImageAdd = true;
                            }
                        }
                        else
                        {
                            imageFileName = string.Empty;
                            imageDefaultPath = string.Empty;
                            imgDisplay.ImagePath = string.Empty;
                            btnAddImage.Content = uploadCaption;
                            IsImageAdd = true;
                        }
                        medicalFavourable = dish.MedicalFavourable;
                        medicalUnFavourable = dish.MedicalUnFavourable;
                        ayurvedicFavourable = dish.AyurvedicFavourable;
                        ayurvedicUnFavourable = dish.AyurvedicUnFavourable;
                        keyWords = dish.Keywords;
                        
                        isIngredientsLoad = true;

                        cboFoodType.SelectedValue =  Convert.ToInt16(dish.FoodHabitID);
                        cboEthnic.SelectedValue = Convert.ToInt16(dish.EthnicID);
                        cboDishCategory.SelectedValue = Convert.ToInt16(dish.DishCategoryID);
                        cboServeUnit.SelectedValue = Convert.ToInt16(dish.ServeUnit);

                        NSysServeUnit serveUnitItem = new NSysServeUnit();
                        serveUnitItem = NSysServeUnitManager.GetItem(dish.ServeUnit);
                        if (serveUnitItem != null)
                        {
                            txtServeUnit1.Text = serveUnitItem.ServeUnitName;
                            txtServeUnit2.Text = serveUnitItem.ServeUnitName;
                        }

                        LoadIngredientList();
                        LoadIngredientNutrients();
                        
                    }
                    else
                    {
                        ClearAllValues();
                        IsImageAdd = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void SaveDish()
        {
            try
            {
                if (ValidateDish() == true)
                {
                    using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                    {
                        DeleteIngredientList();
                        GetDishList();
                        DishManager.Save(dish);
                    }

                    AlertBox.Show(XMLServices.GetXmlMessage("E1002"), "", AlertType.Information, AlertButtons.OK);
                    ClearAllValues();
                    txtTitleENG.Focus();
                    SetButtonVisibility();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
       
        /// <summary>
        /// Clear all the values
        /// </summary>
        private void ClearAllValues()
        {
            try
            {
                dishID = 0;

                lblDishName.Content = string.Empty;
                lblDishName.Visibility = Visibility.Hidden;
                txtTitleENG.Text = string.Empty;
                txtTitleREG.Text = string.Empty;
                txtTitleDisplay.Text = string.Empty;
                cboEthnic.SelectedIndex = 0;
                cboFoodType.SelectedIndex = 0;
                cboDishCategory.SelectedIndex = 0;
                txtCookTime.Text = string.Empty;
                txtShelfLife.Text = string.Empty;
                txtRefrigeratedLife.Text = string.Empty;
                txtFrozenLife.Text = string.Empty;            
                txtProcessENG.Text = string.Empty;
                txtProcessREG.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtRemarksREG.Text = string.Empty;
                txtServingSize.Text = string.Empty;
                txtServingSize1.Text = string.Empty;
                txtServingSize2.Text = string.Empty;
                txtStandardWeight.Text = string.Empty;
                txtStandardWeight1.Text = string.Empty;
                txtStandardWeight2.Text = string.Empty;
                txtTotalWeightvalue.Text = string.Empty;

                planWeight = 0;
                planWeight1 = 0;
                planWeight2 = 0;

                txtPreWeight.Text = string.Empty;
                txtPreWeight1.Text = string.Empty;
                txtPreWeight2.Text = string.Empty;                
                imgDisplay.ImageSource = string.Empty;

                LoadIngredientList();
                tbDish.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
    
        private void CalculateNutrientValues()
        {
            try
            {
                if (dishID > 0)
                {
                    ProgressDialog dlg = new ProgressDialog();
                    dlg.isFocusable = true;
                    int startValue = 0;
                    int selectPlan = 0;
                    switch (cboPlan.SelectedIndex)
                    {
                        case 0:
                            ingredientNutrientsPlan1List = new List<IngredientNutrients>();
                            ingredientAminoAcidPlan1List = new List<IngredientAminoAcid>();
                            ingredientFattyAcidPlan1List = new List<IngredientFattyAcid>();

                            selectPlan = (int)MenuPlan.PLANI;
                            serveWeight = CommonFunctions.Convert2Float(txtStandardWeight.Text);
                            serveSize = CommonFunctions.Convert2Float(txtServingSize.Text) * serveWeight;
                            break;
                        case 1:
                            ingredientNutrientsPlan2List = new List<IngredientNutrients>();
                            ingredientAminoAcidPlan2List = new List<IngredientAminoAcid>();
                            ingredientFattyAcidPlan2List = new List<IngredientFattyAcid>();

                            selectPlan = (int)MenuPlan.PLANII;
                            serveWeight = CommonFunctions.Convert2Float(txtStandardWeight1.Text);
                            serveSize = CommonFunctions.Convert2Float(txtServingSize1.Text) * serveWeight;
                            break;
                        case 2:
                            ingredientNutrientsPlan3List = new List<IngredientNutrients>();
                            ingredientAminoAcidPlan3List = new List<IngredientAminoAcid>();
                            ingredientFattyAcidPlan3List = new List<IngredientFattyAcid>();

                            selectPlan = (int)MenuPlan.PLANIII;
                            serveWeight = CommonFunctions.Convert2Float(txtStandardWeight2.Text);
                            serveSize = CommonFunctions.Convert2Float(txtServingSize2.Text) * serveWeight;
                            break;
                    }

                    dlg.RunWorkerThread(startValue, ShowProgress);
                    
                    if (ingredientNutrientsList != null)
                    {
                        lvNutrientValues.ItemsSource = ingredientNutrientsList;
                        lvNutrientValues.Items.Refresh();
                    }

                    if (ingredientAminoAcidList != null)
                    {
                        lvAmino.ItemsSource = ingredientAminoAcidList;
                        lvAmino.Items.Refresh();
                    }

                    if (ingredientFattyAcidList != null)
                    {
                        lvFattyAcid.ItemsSource = ingredientFattyAcidList;
                        lvFattyAcid.Items.Refresh();
                    }

                    switch (selectPlan)
                    {
                        case (int)MenuPlan.PLANI:
                            ingredientNutrientsPlan1List = ingredientNutrientsList;
                            ingredientAminoAcidPlan1List = ingredientAminoAcidList;
                            ingredientFattyAcidPlan1List = ingredientFattyAcidList;
                            break;
                        case (int)MenuPlan.PLANII:
                            ingredientNutrientsPlan2List = ingredientNutrientsList;
                            ingredientAminoAcidPlan2List = ingredientAminoAcidList;
                            ingredientFattyAcidPlan2List = ingredientFattyAcidList;
                            break;
                        case (int)MenuPlan.PLANIII:
                            ingredientNutrientsPlan3List = ingredientNutrientsList;
                            ingredientAminoAcidPlan3List = ingredientAminoAcidList;
                            ingredientFattyAcidPlan3List = ingredientFattyAcidList;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowProgress(object sender, DoWorkEventArgs e)
        {
            //the sender property is a reference to the dialog's BackgroundWorker
            //component
            BackgroundWorker worker = (BackgroundWorker)sender;
            
            int rCount = 0;
            byte standardUnitID = 0;
            int dishIngredientID = 0;
            float Quantity = 0;
            float StandardWeight = 0;
            double nutrientValue = 0;            

            ArrayList nutrientValues = new ArrayList();
            ArrayList aminoValues = new ArrayList();
            ArrayList fattyValues = new ArrayList();

            IngredientNutrients ingredientNutriItem = new IngredientNutrients();
            IngredientAminoAcid ingredientAminoItem = new IngredientAminoAcid();
            IngredientFattyAcid ingredientFattyItem = new IngredientFattyAcid();

            IngredientStandardUnit ingredientStandardUnitItem = new IngredientStandardUnit();

            for (int i = 0; i < lvDishIngredient.Items.Count; i++)
            {
                standardUnitID = ((DishIngredient)lvDishIngredient.Items[i]).StandardUnitID;
                dishIngredientID = ((DishIngredient)lvDishIngredient.Items[i]).IngredientID;
                Quantity = ((DishIngredient)lvDishIngredient.Items[i]).Quantity;
                //StandardWeight = ((DishIngredient)lvDishIngredient.Items[i]).StandardWeight;                

                ingredientStandardUnitItem = IngredientStandardUnitManager.GetItem(dishIngredientID, standardUnitID);
                if (ingredientStandardUnitItem != null)
                {
                    StandardWeight = ingredientStandardUnitItem.StandardWeight;
                    //if (ingredientStandardUnitItem.StandardUnitType != (int)StandardUnitType.TypeII)
                    //{
                    ingredientNutrientsList = IngredientNutrientsManager.GetListNutrientValues(dishIngredientID, (Byte)NutrientGroup.Nutrients);
                    if (ingredientNutrientsList != null)
                    {
                        //make a short break
                        Thread.Sleep(50);

                        rCount = 0;
                        foreach (IngredientNutrients ingredientNutrientItem in ingredientNutrientsList)
                        {
                            ingredientNutriItem = IngredientNutrientsManager.GetItemNutrients(dishIngredientID, ingredientNutrientItem.NutrientID);
                            if (nutrientValues.Count > rCount)
                            {
                                nutrientValues[rCount] = CommonFunctions.Convert2Float(Convert.ToString(nutrientValues[rCount])) + CommonFunctions.Convert2Float(Convert.ToString(Math.Round(Convert.ToDecimal(Quantity * StandardWeight * ingredientNutriItem.NutrientValue), 2)));
                            }
                            else
                            {
                                nutrientValues.Add(CommonFunctions.Convert2Float(Convert.ToString(Math.Round(Convert.ToDecimal(Quantity * StandardWeight * ingredientNutriItem.NutrientValue), 2))));
                            }
                            nutrientValue = Math.Round((CommonFunctions.Convert2Double(Convert.ToString(nutrientValues[rCount])) / serveSize) * serveWeight, 2);
                            ingredientNutrientItem.NutrientValue = CommonFunctions.Convert2Float(Convert.ToString(nutrientValue));
                            rCount = rCount + 1;
                        }
                    }

                    rCount = 0;
                    ingredientAminoAcidList = IngredientAminoAcidManager.GetListNutrientValues(dishIngredientID, (Byte)NutrientGroup.AminoAcid);
                    if (ingredientAminoAcidList != null)
                    {

                        //make a short break
                        Thread.Sleep(50);

                        rCount = 0;
                        foreach (IngredientAminoAcid ingredientAminoAcidItem in ingredientAminoAcidList)
                        {
                            ingredientAminoItem = IngredientAminoAcidManager.GetItemNutrients(dishIngredientID, ingredientAminoAcidItem.NutrientID);
                            if (aminoValues.Count > rCount)
                            {
                                aminoValues[rCount] = CommonFunctions.Convert2Float(Convert.ToString(aminoValues[rCount])) + CommonFunctions.Convert2Float(Convert.ToString(Math.Round(Convert.ToDecimal(Quantity * StandardWeight * ingredientAminoItem.NutrientValue), 2)));
                            }
                            else
                            {
                                aminoValues.Add(CommonFunctions.Convert2Float(Convert.ToString(Math.Round(Convert.ToDecimal(Quantity * StandardWeight * ingredientAminoItem.NutrientValue), 2))));
                            }
                            nutrientValue = Math.Round((CommonFunctions.Convert2Float(Convert.ToString(aminoValues[rCount])) / serveSize) * serveWeight, 2);
                            ingredientAminoAcidItem.NutrientValue = CommonFunctions.Convert2Float(Convert.ToString(nutrientValue));
                            rCount = rCount + 1;
                        }
                    }

                    rCount = 0;
                    ingredientFattyAcidList = IngredientFattyAcidManager.GetListNutrientValues(dishIngredientID, (Byte)NutrientGroup.FattyAcid);
                    if (ingredientFattyAcidList != null)
                    {
                        //make a short break
                        Thread.Sleep(50);

                        rCount = 0;
                        foreach (IngredientFattyAcid ingredientFattyAcidItem in ingredientFattyAcidList)
                        {
                            ingredientFattyItem = IngredientFattyAcidManager.GetItemNutrients(dishIngredientID, ingredientFattyAcidItem.NutrientID);
                            if (fattyValues.Count > rCount)
                            {
                                fattyValues[rCount] = CommonFunctions.Convert2Float(Convert.ToString(fattyValues[rCount])) + CommonFunctions.Convert2Float(Convert.ToString(Math.Round(Convert.ToDecimal(Quantity * StandardWeight * ingredientFattyItem.NutrientValue), 2)));
                            }
                            else
                            {
                                fattyValues.Add(CommonFunctions.Convert2Float(Convert.ToString(Math.Round(Convert.ToDecimal(Quantity * StandardWeight * ingredientFattyItem.NutrientValue), 2))));
                            }
                            nutrientValue = Math.Round((CommonFunctions.Convert2Double(Convert.ToString(fattyValues[rCount])) / serveSize) * serveWeight, 2);
                            ingredientFattyAcidItem.NutrientValue = CommonFunctions.Convert2Float(Convert.ToString(nutrientValue));
                            rCount = rCount + 1;
                        }
                    }
                    //}
                }
                //if the user cancelled, break
                if (worker.CancellationPending) break;
            }
        }        

        private void CreateIngredientDatatable()
        {
            dtIngNutritiveValues = new DataTable();
            dtIngNutritiveValues.Columns.Add("IngredientID");
            dtIngNutritiveValues.Columns.Add("IngredientName");
            dtIngNutritiveValues.Columns.Add("DisplayName");
            dtIngNutritiveValues.Columns.Add("Quantity");
            dtIngNutritiveValues.Columns.Add("StandardUnitName");
            dtIngNutritiveValues.Columns.Add("Calorie");
            dtIngNutritiveValues.Columns.Add("Protien");
            dtIngNutritiveValues.Columns.Add("CarboHydrate");
            dtIngNutritiveValues.Columns.Add("Fat");
            dtIngNutritiveValues.Columns.Add("Fibre");
            dtIngNutritiveValues.Columns.Add("Iron");
            dtIngNutritiveValues.Columns.Add("Calcium");            
        }
       
        /// <summary>
        /// Calculate Nutrient Values of each Ingredients
        /// </summary>
        private void CalculateIngredientNutrients(float servingSize,float standardWeight)
        {
            CreateIngredientDatatable();            
          
            lblSCarbo.Content = string.Empty;
            lblSCalorie.Content = string.Empty;
            lblSProtein.Content = string.Empty;
            lblSFat.Content = string.Empty;
            lblSFiber.Content = string.Empty;
            lblSIron.Content = string.Empty;
            lblSCalcium.Content = string.Empty;

            calorieSum = 0;
            carbohydratesSum = 0;
            fatSum = 0;
            proteinSum = 0;
            fiberSum = 0;
            ironSum = 0;
            calciumSum = 0;
            
            int dishingredientID = 0;
            byte dishstandardUnitID = 0;
            float ingQuantity = 0;
            NSysNutrient sysNutrientItem = new NSysNutrient();
            List<DishIngredient> dishIngredientUpdateList = new List<DishIngredient>();
            IngredientStandardUnit ingredientStandardUnitItem = new IngredientStandardUnit();
            List<IngredientNutrients> ingredientNutrientsList = new List<IngredientNutrients>();
            try
            {
                for (int i = 0; i < lvDishIngredient.Items.Count; i++)
                {
                    DataRow row = dtIngNutritiveValues.NewRow();
                    
                    dishingredientID = ((DishIngredient)lvDishIngredient.Items[i]).IngredientID;
                    dishstandardUnitID = ((DishIngredient)lvDishIngredient.Items[i]).StandardUnitID;
                    ingQuantity = ((DishIngredient)lvDishIngredient.Items[i]).Quantity;
                    row[0] = dishingredientID;
                    row[1] = ((DishIngredient)lvDishIngredient.Items[i]).IngredientName;
                    row[2] = ((DishIngredient)lvDishIngredient.Items[i]).DisplayName;
                    row[3] = ingQuantity;
                    ingredientStandardUnitItem = IngredientStandardUnitManager.GetItem(dishingredientID, dishstandardUnitID);
                    if (ingredientStandardUnitItem != null)
                    {
                        double nutrientValue = 0;
                        row[4] = ingredientStandardUnitItem.StandardUnitName;                        
                        ingredientNutrientsList = IngredientNutrientsManager.GetListNutrientMain(dishingredientID);
                        foreach(IngredientNutrients ingredientNutrientsItem in ingredientNutrientsList)
                        {
                            if (ingredientNutrientsItem.NutrientID == (int)MainNutrients.Calorie)
                            {
                                nutrientValue = Math.Round((ingQuantity * ingredientStandardUnitItem.StandardWeight * (ingredientNutrientsItem.NutrientValue / 100)), 2);
                                row[5] = nutrientValue;
                                calorieSum = calorieSum + (float)nutrientValue;
                            }
                            else if (ingredientNutrientsItem.NutrientID == (int)MainNutrients.Protien)
                            {
                                nutrientValue = Math.Round((ingQuantity * ingredientStandardUnitItem.StandardWeight * (ingredientNutrientsItem.NutrientValue / 100)), 2);
                                row[6] = nutrientValue;
                                proteinSum = proteinSum + (float)nutrientValue;
                            }
                            else if (ingredientNutrientsItem.NutrientID == (int)MainNutrients.CarboHydrates)
                            {
                                nutrientValue = Math.Round((ingQuantity * ingredientStandardUnitItem.StandardWeight * (ingredientNutrientsItem.NutrientValue / 100)), 2);
                                row[7] = nutrientValue;
                                carbohydratesSum = carbohydratesSum + (float)nutrientValue;
                            }
                            else if (ingredientNutrientsItem.NutrientID == (int)MainNutrients.FAT)
                            {
                                nutrientValue = Math.Round((ingQuantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue), 2);
                                row[8] = nutrientValue;
                                fatSum = fatSum + (float)nutrientValue;
                            }
                            else if (ingredientNutrientsItem.NutrientID == (int)MainNutrients.Fibre)
                            {
                                nutrientValue = Math.Round((ingQuantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue), 2);
                                row[9] = nutrientValue;
                                fiberSum = fiberSum + (float)nutrientValue;
                            }
                            else if (ingredientNutrientsItem.NutrientID == (int)MainNutrients.Iron)
                            {
                                nutrientValue = Math.Round((ingQuantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue), 2);
                                row[10] = nutrientValue;
                                ironSum = ironSum + (float)nutrientValue;
                            }
                            else if (ingredientNutrientsItem.NutrientID == (int)MainNutrients.Calcium)
                            {
                                nutrientValue = Math.Round((ingQuantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue), 2);
                                row[11] = nutrientValue;
                                calciumSum = calciumSum + (float)nutrientValue;
                            }
                        }
                    }
                    dtIngNutritiveValues.Rows.Add(row);
                }

                if (dtIngNutritiveValues != null)
                {
                    lvIngNutrients.ItemsSource = dtIngNutritiveValues.DefaultView;
                }
            
                lblSCalorie.Content = Convert.ToString(Math.Round((calorieSum / servingSize) * standardWeight, 2));
                lblSProtein.Content = Convert.ToString(Math.Round((proteinSum / servingSize) * standardWeight, 2));
                lblSCarbo.Content = Convert.ToString(Math.Round((carbohydratesSum / servingSize) * standardWeight, 2));
                lblSFat.Content = Convert.ToString(Math.Round((fatSum / servingSize) * standardWeight, 2));
                lblSFiber.Content = Convert.ToString(Math.Round((fiberSum / servingSize) * standardWeight, 2));
                lblSIron.Content = Convert.ToString(Math.Round((ironSum / servingSize) * standardWeight, 2));
                lblSCalcium.Content = Convert.ToString(Math.Round((calciumSum / servingSize) * standardWeight, 2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void CalculateGramWeightOld()
        {
            if (lvDishIngredient.SelectedIndex >= 0)
            {
                int rowIndex = lvDishIngredient.SelectedIndex;

                if (((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Quantity, rowIndex, "txtQuantity"))) != null)
                {
                    if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Unit, rowIndex, "cboUnit"))) != null)
                    {
                        int ingID = ((DishIngredient)lvDishIngredient.Items[rowIndex]).IngredientID;
                        double gramWeight = 0, newTotalWeight = 0, oldTotalWeight = 0;

                        byte standardWeight = CommonFunctions.Convert2Byte(Convert.ToString(((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Unit, rowIndex, "cboUnit"))).SelectedValue));
                        ingredientUnitItem = IngredientStandardUnitManager.GetItem(ingID, standardWeight);
                        if (ingredientUnitItem != null)
                        {
                            ((DishIngredient)lvDishIngredient.Items[rowIndex]).StandardUnitType = ingredientUnitItem.StandardUnitType;
                            if (ingredientUnitItem.StandardUnitType != (int)StandardUnitType.TypeII)
                            {
                                TotalWeightvalue = CommonFunctions.Convert2Double(txtTotalWeightvalue.Text);
                                if (ingredientUnitItem.WeightChangerate == 0)
                                    ingredientUnitItem.WeightChangerate = 100;                                
                                if (TotalWeightvalue > 0)
                                {
                                    oldTotalWeight = TotalWeightvalue - (ingredientUnitItem.StandardWeight * (float)((DishIngredient)lvDishIngredient.Items[lvDishIngredient.SelectedIndex]).Quantity) * (ingredientUnitItem.WeightChangerate / 100);
                                }

                                gramWeight = CommonFunctions.Convert2Float((string)((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Quantity, rowIndex, "txtQuantity"))).Text) * ingredientUnitItem.StandardWeight;
                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.GramWeight, rowIndex, "txtGramWeight"))).Text = Convert.ToString(Math.Round(gramWeight,1));
                                
                                newTotalWeight = oldTotalWeight + gramWeight * (ingredientUnitItem.WeightChangerate /100);
                                newTotalWeight = CommonFunctions.RoundUp(((CommonFunctions.Convert2Double(Convert.ToString(newTotalWeight)) == 0 ? 0 :CommonFunctions.Convert2Double(Convert.ToString(newTotalWeight))/ 50)), 0) * 50;
                                txtTotalWeightvalue.Text = Convert.ToString(newTotalWeight);
                            }
                            else
                            {
                                ((DishIngredient)lvDishIngredient.Items[rowIndex]).Quantity = 0;
                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Quantity, rowIndex, "txtQuantity"))).Text = "0";
                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Quantity, rowIndex, "txtQuantity"))).IsHitTestVisible = true;

                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.GramWeight, rowIndex, "txtGramWeight"))).Text = "0";
                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.GramWeight, rowIndex, "txtGramWeight"))).IsHitTestVisible = true;
                            }
                        }
                    }
                }
            }
        }

        private void CalculateGramWeight()
        {
            double newTotalWeight = 0;
            for (int rowIndex = 0; rowIndex < lvDishIngredient.Items.Count; rowIndex++)
            {
                if (((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Quantity, rowIndex, "txtQuantity"))) != null)
                {
                    if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Unit, rowIndex, "cboUnit"))) != null)
                    {
                        int ingID = ((DishIngredient)lvDishIngredient.Items[rowIndex]).IngredientID;
                        double gramWeight = 0;

                        byte standardWeight = CommonFunctions.Convert2Byte(Convert.ToString(((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Unit, rowIndex, "cboUnit"))).SelectedValue));
                        ingredientUnitItem = IngredientStandardUnitManager.GetItem(ingID, standardWeight);
                        if (ingredientUnitItem != null)
                        {
                            ((DishIngredient)lvDishIngredient.Items[rowIndex]).StandardUnitType = ingredientUnitItem.StandardUnitType;
                            if (ingredientUnitItem.StandardUnitType != (int)StandardUnitType.TypeII)
                            {
                                if (ingredientUnitItem.WeightChangerate == 0)
                                    ingredientUnitItem.WeightChangerate = 100;

                                gramWeight = CommonFunctions.Convert2Float((string)((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Quantity, rowIndex, "txtQuantity"))).Text) * ingredientUnitItem.StandardWeight;
                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.GramWeight, rowIndex, "txtGramWeight"))).Text = Convert.ToString(Math.Round(gramWeight, 1));

                                newTotalWeight = newTotalWeight + (gramWeight * (ingredientUnitItem.WeightChangerate / 100));
                            }
                            else
                            {
                                ((DishIngredient)lvDishIngredient.Items[rowIndex]).Quantity = 0;
                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Quantity, rowIndex, "txtQuantity"))).Text = "0";
                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.Quantity, rowIndex, "txtQuantity"))).IsHitTestVisible = true;

                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.GramWeight, rowIndex, "txtGramWeight"))).Text = "0";
                                ((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvDishIngredient, (int)ListViewIndex.GramWeight, rowIndex, "txtGramWeight"))).IsHitTestVisible = true;
                            }
                        }
                    }
                }
            }
            newTotalWeight = CommonFunctions.RoundUp(((CommonFunctions.Convert2Double(Convert.ToString(newTotalWeight)) == 0 ? 0 : CommonFunctions.Convert2Double(Convert.ToString(newTotalWeight)) / 50)), 0) * 50;
            txtTotalWeightvalue.Text = Convert.ToString(newTotalWeight);

            if (CommonFunctions.Convert2Float(txtStandardWeight.Text) > 0)
            {
                if (newTotalWeight > 0)
                {
                    txtServingSize.Text = Convert.ToString(CommonFunctions.RoundDown(((newTotalWeight / CommonFunctions.Convert2Float(txtStandardWeight.Text)) / .5), 0) * .5);
                }
            }
            if (CommonFunctions.Convert2Float(txtStandardWeight1.Text) > 0)
            {
                if (newTotalWeight > 0)
                {
                    txtServingSize1.Text = Convert.ToString(CommonFunctions.RoundDown(((newTotalWeight / CommonFunctions.Convert2Float(txtStandardWeight1.Text)) / .5), 0) * .5);
                }
            }
            if (CommonFunctions.Convert2Float(txtStandardWeight2.Text) > 0)
            {
                if (newTotalWeight > 0)
                {
                    txtServingSize2.Text = Convert.ToString(CommonFunctions.RoundDown(((newTotalWeight / CommonFunctions.Convert2Float(txtStandardWeight2.Text)) / .5), 0) * .5);
                }
            }
        }
        
        /// <summary>
        /// Load Amino Acid Parameters
        /// </summary>
        private void LoadAminoAcidsParameters()
        {
            sysNutrientList = NSysNutrientManager.GetListNutrient((byte)NutrientGroup.AminoAcid);
            if (sysNutrientList != null)
            {
                lvAmino.ItemsSource = sysNutrientList;
            }
        }

        /// <summary>
        /// Load Fatty Acid Parameters
        /// </summary>
        private void LoadFattyAcidsParameters()
        {
            sysNutrientList = NSysNutrientManager.GetListNutrient((byte)NutrientGroup.FattyAcid);
            if (sysNutrientList != null)
            {
                lvFattyAcid.ItemsSource = sysNutrientList;
            }
        }

        /// <summary>
        /// Load Nutrient Parameters
        /// </summary>
        private void LoadNutrientsParameters()
        {
            sysNutrientList = NSysNutrientManager.GetListNutrient((byte)NutrientGroup.Nutrients);
            if (sysNutrientList != null)
            {
                lvNutrientValues.ItemsSource = sysNutrientList;
            }
        }

        /// <summary>
        /// Browse the dish image
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Filter"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private string OpenDialog(string Title, string Filter, string FileName)
        {
            string filename = string.Empty;
            openDlg.Title = Title;
            openDlg.FileName = FileName; // Default file name
            openDlg.Filter = Filter; // Filter files by extension
            // Show open file dialog box
            Nullable<bool> result = openDlg.ShowDialog();
            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = openDlg.FileName;
            }
            return filename;
        }

        /// <summary>
        /// Get the destination path of dish image
        /// </summary>
        /// <param name="FolderName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Copy dish Image from source to destination
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        /// <returns></returns>
        private string CopyFile(string sourceFile, string destFile)
        {
            try
            {
                if (File.Exists(destFile))
                {
                    bool isReadOnly = ((File.GetAttributes(destFile) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
                    if (isReadOnly)
                        File.SetAttributes(destFile, FileAttributes.Normal);
                }
                File.Copy(sourceFile, destFile, true);
                return destFile;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }
   
        private void GridAnimation(string animationname)
        {
            Storyboard gridAnimation = (Storyboard)FindResource(animationname);
            gridAnimation.Begin(this);
        }

		#endregion       

        #region UpdateDish

        private void UpdateDishWeight()
        {
            List<Dish> objDishList = new List<Dish>();            

            objDishList = DishManager.GetList(" ");
            foreach (Dish objDish in objDishList)
            {
                lblDishName.Content = objDish.Name;
                List<DishIngredient> objDishIngredientList = new List<DishIngredient>();

                objDishIngredientList = DishIngredientManager.GetList(objDish.Id);
                double newTotalWeight = 0;
                foreach (DishIngredient objDishIngr in objDishIngredientList)
                {
                    int ingID = objDishIngr.IngredientID;
                    double gramWeight = 0;

                    byte standardWeight = objDishIngr.StandardUnitID;
                    ingredientUnitItem = IngredientStandardUnitManager.GetItem(ingID, standardWeight);
                    if (ingredientUnitItem != null)
                    {
                        objDishIngr.StandardUnitType = ingredientUnitItem.StandardUnitType;
                        if (ingredientUnitItem.StandardUnitType != (int)StandardUnitType.TypeII)
                        {
                            if (ingredientUnitItem.WeightChangerate == 0)
                                ingredientUnitItem.WeightChangerate = 100;

                            gramWeight = objDishIngr.Quantity * ingredientUnitItem.StandardWeight;
                            newTotalWeight = newTotalWeight + (gramWeight * (ingredientUnitItem.WeightChangerate / 100));
                        }
                    }                    
                }

                newTotalWeight = CommonFunctions.RoundUp(((CommonFunctions.Convert2Double(Convert.ToString(newTotalWeight)) == 0 ? 0 : CommonFunctions.Convert2Double(Convert.ToString(newTotalWeight)) / 50)), 0) * 50;

                if (objDish.StandardWeight > 0)
                {
                    if (newTotalWeight > 0)
                    {
                        objDish.ServeCount = CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.RoundDown(((newTotalWeight / objDish.StandardWeight) / .5), 0) * .5));
                    }
                }

                if (objDish.StandardWeight1 > 0)
                {
                    if (newTotalWeight > 0)
                    {
                        objDish.ServeCount1 = CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.RoundDown(((newTotalWeight / objDish.StandardWeight1) / .5), 0) * .5));
                    }
                }

                if (objDish.StandardWeight2 > 0)
                {
                    if (newTotalWeight > 0)
                    {
                        objDish.ServeCount2 = CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.RoundDown(((newTotalWeight / objDish.StandardWeight2) / .5), 0) * .5));
                    }
                }
                DishManager.UpdateDishWeight(objDish.Id, newTotalWeight,objDish);
            }
            MessageBox.Show("Dishweight updation completed");
        }
       
        private void UpdateDishNutrients()
        {
            List<Dish> objDishList = new List<Dish>();            

            objDishList = DishManager.GetList(" ");
            foreach (Dish objDish in objDishList)
            {
                string nutriName = string.Empty;
                float totalYieldWeight = objDish.ServeCount * objDish.StandardWeight;
                NSysNutrient sysNutrientItem = new NSysNutrient();
                List<DishIngredient> dishIngredientUpdateList = new List<DishIngredient>();
                IngredientStandardUnit ingredientStandardUnitItem = new IngredientStandardUnit();
                IngredientNutrients ingredientNutrientsItem = new IngredientNutrients();
                try
                {
                    ClearNutrientValues();
                    List<DishIngredient> objDishIngredientList = new List<DishIngredient>();
                    objDishIngredientList = DishIngredientManager.GetList(objDish.Id);
                    foreach (DishIngredient objDishIngr in objDishIngredientList)
                    {
                        ingredientStandardUnitItem = IngredientStandardUnitManager.GetItem(objDishIngr.IngredientID, objDishIngr.StandardUnitID);
                        if (ingredientStandardUnitItem != null)
                        {
                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Calorie));
                            //if (sysNutrientItem != null)
                            //{
                            ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Calorie);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishCalorie = dishCalorie + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Protien));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Protien);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishProtien = dishProtien + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.CarboHydrates));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.CarboHydrates);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishCarboHydrates = dishCarboHydrates + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.FAT));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.FAT);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishFAT = dishFAT + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Fibre));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Fibre);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishFibre = dishFibre + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Iron));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Iron);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishIron = dishIron + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Calcium));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Calcium);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishCalcium = dishCalcium + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Phosphorus));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Phosphorus);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishPhosphorus = dishPhosphorus + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //nutriName = Classes.CommonFunctions.GetDescription(MainNutrients.VitaminA_Retinol);
                            //sysNutrientItem = SysNutrientManager.GetNutrientID(nutriName);
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.VitaminA_Retinol);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishVitaminARetinol = dishVitaminARetinol + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //nutriName = Classes.CommonFunctions.GetDescription(MainNutrients.VitaminA_BetaCarotene);
                            //sysNutrientItem = SysNutrientManager.GetNutrientID(nutriName);
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.VitaminA_BetaCarotene);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishVitaminABetaCarotene = dishVitaminABetaCarotene + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Thiamine));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Thiamine);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishThiamine = dishThiamine + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Riboflavin));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Riboflavin);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishRiboflavin = dishRiboflavin + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.NicotinicAcid));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.NicotinicAcid);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishNicotinicAcid = dishNicotinicAcid + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.Pyridoxine));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.Pyridoxine);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishPyridoxine = dishPyridoxine + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.FolicAcid));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.FolicAcid);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishFolicAcid = dishFolicAcid + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.VitaminB12));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.VitaminB12);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishVitaminB12 = dishVitaminB12 + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}

                            //sysNutrientItem = SysNutrientManager.GetNutrientID(Enum.GetName(typeof(MainNutrients), (int)MainNutrients.VitaminC));
                            //if (sysNutrientItem != null)
                            //{
                                ingredientNutrientsItem = IngredientNutrientsManager.GetItemNutrients(objDishIngr.IngredientID, (int)MainNutrients.VitaminC);
                                if (ingredientNutrientsItem != null)
                                {
                                    dishVitaminC = dishVitaminC + ((objDishIngr.Quantity * ingredientStandardUnitItem.StandardWeight * ingredientNutrientsItem.NutrientValue) / totalYieldWeight) * 100;
                                }
                            //}
                        }
                    }

                    objDish.Calorie = dishCalorie;
                    objDish.CarboHydrates = dishCarboHydrates;
                    objDish.Protien = dishProtien;
                    objDish.FAT = dishFAT;
                    objDish.Fibre = dishFibre;
                    objDish.Iron = dishIron;
                    objDish.Calcium = dishCalcium;
                    objDish.Phosphorus = dishPhosphorus;
                    objDish.VitaminARetinol = dishVitaminARetinol;
                    objDish.VitaminABetaCarotene = dishVitaminABetaCarotene;
                    objDish.Thiamine = dishThiamine;
                    objDish.Riboflavin = dishRiboflavin;
                    objDish.NicotinicAcid = dishNicotinicAcid;
                    objDish.Pyridoxine = dishPyridoxine;
                    objDish.FolicAcid = dishFolicAcid;
                    objDish.VitaminB12 = dishVitaminB12;
                    objDish.VitaminC = dishVitaminC;
                    //Update
                    DishManager.UpdateDishNutrients(objDish);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
            }
            MessageBox.Show("DishNutrients updation completed");
        }

        #endregion                
                                        
    }
}

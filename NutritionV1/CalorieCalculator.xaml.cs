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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Transactions;
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Common.Classes;
using NutritionV1.Enums;
using NutritionV1.Constants;
using System.Resources;
using NutritionV1.Classes;
using System.Windows.Media.Animation;
using System.IO;
using System.Collections;
using System.Configuration;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for .xaml
    /// </summary>
    public partial class CalorieCalculator : Page
    {
        #region Declarations

        private enum ListViewIndex
        {
            Plan = 3,
        }

        int DishID;
        private static ArrayList searchDishID = new ArrayList();

        DataTable dtUnit = new DataTable();

        Dish dish = new Dish();
        List<Dish> dishListSearch = new List<Dish>();
        List<Dish> dishListAdd = new List<Dish>();
        Dish dishItem = new Dish();

        #endregion

        public static ArrayList SearchDishID
        {
            get
            {
                return searchDishID;
            }
            set
            {
                searchDishID = value;
            }
        }

        public CalorieCalculator()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #region Methods

        private void IncludeAyurveda()
        {
            if (ConfigurationManager.AppSettings["IncludeAyurvedic"] == "0")
            {
                
            }
        }

        private void SetCulture()
        {
            App apps = (App)Application.Current;
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;
        }

        private void Initialize()
        {
            try
            {
                dtUnit = XMLServices.GetXMLData(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\ServeUnit.xml",  3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }

        private void GridAnimation(string animationname)
        {
            Storyboard gridAnimation = (Storyboard)FindResource(animationname);
            gridAnimation.Begin(this);
            Keyboard.Focus(cboDishCategory);
            cboDishCategory.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            lvSearchList.DataContext = null;
        }

        private void LoadTemplate()
        {
            gvColPlan.CellTemplate = this.FindResource("planDisplayTemplate") as DataTemplate;
        }

        private void FillSearchList()
        {
            string SearchString = string.Empty;

            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (txtSearch.Text.Trim() != string.Empty || cboDishCategory.SelectedIndex > 0)
                {
                    SearchString = " DishName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";

                    if (cboDishCategory.SelectedIndex > 0)
                    {
                        SearchString = SearchString + " And DishCategoryID=" + cboDishCategory.SelectedIndex + " ";
                    }

                    SearchString = " Where " + SearchString + "Order By DishName";
                    dishListSearch = DishManager.GetList(SearchString);
                    lvSearchList.DataContext = dishListSearch;
                    lvSearchList.SelectedIndex = 0;
                    lvSearchList.ScrollIntoView(lvSearchList.SelectedItem);
                }
                else
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1083"), "", AlertType.Information, AlertButtons.OK);
                }
            }
        }

        private bool GetDishDetails(int GridID,int DishID)
        {
            Dish dish = new Dish();

            try
            {
                dish = DishManager.GetItem(DishID);
                
                if (dish != null)
                {
                    foreach (Dish dishItem in dishListAdd)
                    {
                        if (dishItem.Id == DishID)
                        {
                            return false;
                        }
                    }

                    dish.UnitName = GetUnitName(dish.ServeUnit);
                    dishListAdd.Add(dish);
                }

                FillGrid();
                LoadTemplate();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
                return false;
            }
            finally
            {

            }
        }       

        private string GetUnitName(int UnitID)
        {
            string strUnit = string.Empty;

            if (dtUnit != null)
            {
                if (UnitID > 0)
                {
                    DataRow[] rowcol = dtUnit.Select("ID=" + UnitID);
                    strUnit = Convert.ToString(rowcol[0].ItemArray[1]);
                }
                else
                {
                    strUnit = string.Empty;
                }
            }

            return strUnit;
        }

        private void FillGrid()
        {
            if (dishListAdd.Count > 0)
            {
                lvsetting1.ItemsSource = dishListAdd;
                lvsetting1.Items.Refresh();
            }
        }
        
        public void DisplayNutrientValues()
        {
            int DishID;
            int StandardWeight = 0;
            double DishCount = 0;
            double Calorie = 0, DishCalorie = 0;
            double Protein = 0, DishProtein = 0;
            double CarboHydrates = 0, DishCarboHydrates = 0;
            double Fat = 0, DishFat = 0;
            double Fiber = 0, DishFiber = 0;
            double Iron = 0, DishIron = 0;
            double Calcium = 0, DishCalcium = 0;

            Dish dishNutrients = new Dish();

            try
            {
                for (int i = 0; i < lvsetting1.Items.Count; i++)
                {                    
                    DishID = ((Dish)lvsetting1.Items[i]).Id;

                    if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "cboPlan"))) != null)
                    {
                        StandardWeight = CommonFunctions.Convert2Int(Convert.ToString(((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "cboPlan"))).SelectedValue));
                    }
                    else
                    {
                        StandardWeight = CommonFunctions.Convert2Int(Convert.ToString(((Dish)lvsetting1.Items[i]).StandardWeight));
                    }

                    if (((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "txtDishCount"))) != null)
                    {
                        DishCount = CommonFunctions.Convert2Double(((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "txtDishCount"))).Text);
                    }
                    else
                    {
                        DishCount = ((Dish)lvsetting1.Items[i]).ItemCount;
                    }

                    dishNutrients = DishManager.GetItemDishNutrients(DishID);

                    DishCalorie = dishNutrients.Calorie * DishCount * StandardWeight;
                    DishProtein = dishNutrients.Protien * DishCount * StandardWeight;
                    DishCarboHydrates = dishNutrients.CarboHydrates * DishCount * StandardWeight;
                    DishFat = dishNutrients.FAT * DishCount * StandardWeight;
                    DishFiber = dishNutrients.Fibre * DishCount * StandardWeight;
                    DishIron = dishNutrients.Iron * DishCount * StandardWeight;
                    DishCalcium = dishNutrients.Calcium * DishCount * StandardWeight;

                    Calorie = Calorie + DishCalorie;
                    Protein = Protein + DishProtein;
                    CarboHydrates = CarboHydrates + DishCarboHydrates;
                    Fat = Fat + DishFat;
                    Fiber = Fiber + DishFiber;
                    Iron = Iron + DishIron;
                    Calcium = Calcium + DishCalcium;
                }

                lblTotalCalorie.Content = Convert.ToString(Math.Round(Calorie, 0)) + " gm";
                lblTotalProtein.Content = Convert.ToString(Math.Round(Protein, 0)) + " gm";
                lblTotalCarboHydrates.Content = Convert.ToString(Math.Round(CarboHydrates, 0)) + " gm";
                lblTotalFat.Content = Convert.ToString(Math.Round(Fat, 0)) + " gm";
                lblTotalFiber.Content = Convert.ToString(Math.Round(Fiber, 0)) + " gm";
                lblTotalIron.Content = Convert.ToString(Math.Round(Iron, 0)) + " gm";
                lblTotalCalcium.Content = Convert.ToString(Math.Round(Calcium, 0)) + " gm";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void DeleteDish()
        {
            try
            {
                Dish DishItem = new Dish();
                DishItem = ((Dish)lvsetting1.Items[lvsetting1.SelectedIndex]);
                lvsetting1.ItemsSource = string.Empty;
                dishListAdd.Remove(DishItem);
                lvsetting1.ItemsSource = dishListAdd;
                lvsetting1.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void ClearGrid()
        {
            try
            {
                lvsetting1.ItemsSource = null;
                dishListAdd.Clear();
                DisplayNutrientValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void AddToFavourite(int CheckedValue, int DishID)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FillComments()
        {
            try
            {
                
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void SetTemplate()
        {
            if (chkRegionalNames.IsChecked == true)
            {
                lvNutritionCol3.CellTemplate = this.FindResource("displayNameTemplate") as DataTemplate;
                gvCol2.CellTemplate = this.FindResource("displayNameTemplate") as DataTemplate;
            }
            else
            {
                lvNutritionCol3.CellTemplate = this.FindResource("actualNameTemplate") as DataTemplate;
                gvCol2.CellTemplate = this.FindResource("nameTemplate") as DataTemplate;
            }
        }

        public void UpdateDishList()
        {
            try
            {
                if (searchDishID.Count > 0)
                {
                    for (int i = 0; i < searchDishID.Count; i++)
                    {
                        GetDishDetails(1,Classes.CommonFunctions.Convert2Int(Convert.ToString(searchDishID[i])));
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

        private void ShowReport(string filePath)
        {
            ExcelOutput excelOut = new ExcelOutput();
            try
            {                
                int rowNum = 0;
                int serialNo = 0;
                if (excelOut.CreateExcel("CalorieCalculator",filePath))
                {
                    excelOut.SetSheet(1);
                    rowNum = 6;
                    serialNo = 1;
                    foreach (Dish dishItem in dishListAdd)
                    {
                        excelOut.SetCellValue(rowNum, 2, serialNo);
                        excelOut.SetCellValue(rowNum, 3, dishItem.Name);
                        excelOut.SetCellValue(rowNum, 4, dishItem.StandardWeight);
                        excelOut.SetCellValue(rowNum, 5, dishItem.ItemCount);
                        excelOut.SetCellValue(rowNum, 6, dishItem.UnitName);
                        rowNum = rowNum + 1;
                    }

                    excelOut.SetCellValue(6, 9, Convert.ToString(lblTotalCalorie.Content));
                    excelOut.SetCellValue(7, 9, Convert.ToString(lblTotalProtein.Content));
                    excelOut.SetCellValue(8, 9, Convert.ToString(lblTotalCarboHydrates.Content));
                    excelOut.SetCellValue(9, 9, Convert.ToString(lblTotalFat.Content));
                    excelOut.SetCellValue(10, 9, Convert.ToString(lblTotalFiber.Content));
                    excelOut.SetCellValue(11, 9, Convert.ToString(lblTotalIron.Content));
                    excelOut.SetCellValue(12, 9, Convert.ToString(lblTotalCalcium.Content));

                    excelOut.Save();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                excelOut.Dispose();
                excelOut = null;
            }
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            SetCulture();
            SetTemplate();
            FillComments();
            Initialize();
            Classes.CommonFunctions.FillDishCategory(cboDishCategory);
            LoadTemplate();
            //IncludeAyurveda();
        }

        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Escape)
            //    Close();
        }

        private void txtNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Classes.CommonFunctions.FilterNumeric(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            searchDishID.Clear();

            SelectDish objSelectDish = new SelectDish();
            objSelectDish.FormType = (int)SearchDishType.FoodCalorieCalculator;
            objSelectDish.Owner = Application.Current.MainWindow;
            objSelectDish.ShowDialog();

            UpdateDishList();
        }

        private void imgDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            
            if (lvi != null)
            {
                lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);

                DeleteDish();
                LoadTemplate();
                DisplayNutrientValues();
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1089"), "", AlertType.Information, AlertButtons.OK);
                return;
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dishListAdd.Count != 0)
                {
                    bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1191"), "", AlertType.Exclamation, AlertButtons.YESNO);
                    if (result == true)
                    {
                        ClearGrid();
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void chkFavourites_Checked(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void chkFavourites_Unchecked(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridAnimation("CollapseGrid");
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
                if (lvi != null)
                {
                    lvSearchList.SelectedIndex = lvSearchList.ItemContainerGenerator.IndexFromContainer(lvi);
                    int CurrentDishID = ((Dish)lvSearchList.Items[lvSearchList.SelectedIndex]).Id;

                    if (GetDishDetails(1,CurrentDishID) == false)
                    {
                        AlertBox.Show(XMLServices.GetXmlMessage("E1090"), "", AlertType.Error, AlertButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }
        private void SearchList_Keypress(object sender, KeyEventArgs e)
        {
           
        }
       

        private void chkProfileFilter_Checked(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void chkProfileFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FillSearchList();
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            DisplayNutrientValues();
        }

        private void imgDispalyImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                {
                    lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);

                    if (lvsetting1.SelectedIndex >= 0)
                    {
                        DishID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Dish)lvsetting1.Items[lvsetting1.SelectedIndex]).Id));
                        ImagePreview imagePreview = new ImagePreview();
                        imagePreview.ItemID = DishID;
                        imagePreview.DisplayItem = ItemType.Dish;
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
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }

        private void imgNutritionValue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int PlanID = 0;
                int StandardWeight = 0;
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                {
                    lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);
                    if (lvsetting1.SelectedIndex >= 0)
                    {
                        NutritionDetails ingredientDetails = new NutritionDetails();
                        DishID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Dish)lvsetting1.Items[lvsetting1.SelectedIndex]).Id));                        
                        if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, lvsetting1.SelectedIndex, "cboPlan"))) != null)
                        {
                            StandardWeight = CommonFunctions.Convert2Int(Convert.ToString(((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, lvsetting1.SelectedIndex, "cboPlan"))).SelectedValue));
                            PlanID = CommonFunctions.Convert2Int(Convert.ToString(((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, lvsetting1.SelectedIndex, "cboPlan"))).SelectedIndex));
                        }
                        ingredientDetails.ItemID = DishID;
                        ingredientDetails.DisplayItem = ItemType.Dish;
                        ingredientDetails.PlanID = PlanID;
                        ingredientDetails.StandardWeight = StandardWeight;
                        ingredientDetails.DishID = DishID;
                        if (chkRegionalNames.IsChecked == true)
                        {
                            ingredientDetails.IsRegional = true;
                        }
                        else
                        {
                            ingredientDetails.IsRegional = false;
                        }
                        ingredientDetails.Owner = Application.Current.MainWindow;
                        ingredientDetails.ShowDialog();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }

        private void imgAyurvedicValue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                {
                    lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);

                    if (lvsetting1.SelectedIndex >= 0)
                    {
                        
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }

        private void chkAddtoFavorates_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                {
                    lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);

                    if (lvsetting1.SelectedIndex >= 0)
                    {
                        DishID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Dish)lvsetting1.Items[lvsetting1.SelectedIndex]).Id));
                        AddToFavourite(0, DishID);
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

        private void chkAddtoFavorates_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                {
                    lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);

                    if (lvsetting1.SelectedIndex >= 0)
                    {
                        DishID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Dish)lvsetting1.Items[lvsetting1.SelectedIndex]).Id));
                        AddToFavourite(0, DishID);
                    }
                    else
                    {

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

        private void imgPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int PlanID = 0;
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                {
                    lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);

                    if (lvsetting1.SelectedIndex >= 0)
                    {
                        DishID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Dish)lvsetting1.Items[lvsetting1.SelectedIndex]).Id));
                        if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, lvsetting1.SelectedIndex, "cboPlan"))) != null)
                        {
                            PlanID = CommonFunctions.Convert2Int(Convert.ToString(((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, lvsetting1.SelectedIndex, "cboPlan"))).SelectedIndex));
                        }
                        PlanSelector planSelector = new PlanSelector();
                        planSelector.DishID = DishID;
                        planSelector.PlanID = PlanID;
                        if (chkRegionalNames.IsChecked == true)
                        {
                            planSelector.IsRegional = true;
                        }
                        else
                        {
                            planSelector.IsRegional = false;
                        }
                        planSelector.Owner = Application.Current.MainWindow;
                        planSelector.ShowDialog();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void chkRegionalNames_Checked(object sender, RoutedEventArgs e)
        {
            SetTemplate();
        }

        private void chkRegionalNames_Unchecked(object sender, RoutedEventArgs e)
        {
            SetTemplate();
        }

        private void lblHelp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void cboPlan_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void txtDishCount_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void lvsetting1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvsetting1.SelectedIndex >= 0)
            {
                for (int i = 0; i < lvsetting1.Items.Count; i++)
                {
                    gvColPlan.CellTemplate = this.FindResource("planTemplate") as DataTemplate;
                    if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "cboPlan"))) != null)
                    {
                        if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "cboPlan"))).Items.Count <= 0)
                        {
                            int dishID = CommonFunctions.Convert2Int(Convert.ToString(((Dish)lvsetting1.Items[i]).Id));
                            dishItem.MealPlanList = MealPlanManager.GetFoodPlanList(dishID);
                            if (dishItem.MealPlanList != null)
                            {
                                ((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "cboPlan"))).ItemsSource = dishItem.MealPlanList;
                                if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "cboPlan"))).SelectedIndex < 0)
                                {
                                    ((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvsetting1, (int)ListViewIndex.Plan, i, "cboPlan"))).SelectedIndex = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SearchGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int CurrentDishID = ((Dish)lvSearchList.Items[lvSearchList.SelectedIndex]).Id;
                    if (GetDishDetails(1, CurrentDishID) == false)
                    {
                        AlertBox.Show(XMLServices.GetXmlMessage("E1090"), "", AlertType.Information, AlertButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            DisplayNutrientValues();

            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Filter = "Excel Files | *.xls";
            dialog.DefaultExt = "xls";
            dialog.InitialDirectory = @"C:\";

            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (Convert.ToString(result) == "OK")
            {
                ShowReport(dialog.FileName);
            }
        }

        #endregion    
        
                      
    }
}

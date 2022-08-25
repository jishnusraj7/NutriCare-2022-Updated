using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BONutrition;
using BLNutrition;
using NutritionV1.Classes;
using System.Resources;
using System.Data;
using System.Configuration;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for SearchDish.xaml
    /// </summary>
    /// 
    public partial class SelectDish : Window
    {

        #region Declarations

        Dish dish = new Dish();
        List<Dish> DishList = new List<Dish>();
        List<Dish> dishListSearch = new List<Dish>();
        List<Dish> dishListAdd = new List<Dish>();
        List<Dish> dishSelectList = new List<Dish>();
        private int formType;

        #endregion

        #region Enums

        private enum CaloireType
        {
            Add = 1,
            Delete = 2,
        }

        #endregion

        #region Properties

        public int FormType
        {
            get
            {
                return formType;
            }
            set
            {
                formType = value;
            }
        }

        #endregion

        #region Constructor

        public SelectDish()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region Methods

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            this.Style = (Style)apps.SetStyle["WinStyle"];
        }

        private void SetCulture()
        {
            App apps = (App)Application.Current;
        }

        private void IncludeAyurveda()
        {
            if (ConfigurationManager.AppSettings["IncludeAyurvedic"] == "0")
            {
                
            }
        }

        private void FillCombo()
        {
            try
            {
                Classes.CommonFunctions.FillDishCategory(cboDishCategory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void FillSearchList()
        {
            string SearchString = string.Empty;

            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                SearchString = " DishName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";

                if (cboDishCategory.SelectedIndex > 0)
                {
                    SearchString = SearchString + " And DishCategoryID=" + cboDishCategory.SelectedIndex + " ";
                }
                if (chkYourOwn.IsChecked == true)
                {
                    SearchString = SearchString + " AND IsSystemDish = false ";
                }

                SearchString = " Where " + SearchString + "Order By DishName";
                dishListSearch = DishManager.GetListAddDish(SearchString);
                lvDish.DataContext = dishListSearch;
                lvDish.SelectedIndex = 0;
                lvDish.ScrollIntoView(lvDish.SelectedItem);
            }
        }

        private void DeleteDish()
        {
            try
            {
                txtTotalCalorie.Text = Convert.ToString(CommonFunctions.Convert2Double(txtTotalCalorie.Text) - CalculateCalorie((int)CaloireType.Delete));

                Dish DishItem = new Dish();
                DishItem = ((Dish)lvSelectedList.Items[lvSelectedList.SelectedIndex]);
                lvSelectedList.ItemsSource = string.Empty;
                dishSelectList.Remove(DishItem);
                lvSelectedList.ItemsSource = dishSelectList;
                lvSelectedList.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void SelectDishList()
        {
            if (lvDish.SelectedIndex >= 0)
            {
                Dish dishItem = new Dish();
                dishItem = DishManager.GetItem(((Dish)lvDish.Items[lvDish.SelectedIndex]).Id);

                if (dishItem != null)
                {
                    if (!CommonFunctions.IsDishExists(dishSelectList, CommonFunctions.Convert2Int(Convert.ToString(dishItem.Id))))
                    {
                        dishSelectList.Add(dishItem);
                        lvSelectedList.ItemsSource = dishSelectList;
                        lvSelectedList.Items.Refresh();

                        txtTotalCalorie.Text = Convert.ToString(CommonFunctions.Convert2Double(txtTotalCalorie.Text) + CalculateCalorie((int)CaloireType.Add));
                    }
                    else
                    {
                        AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1071"), "", AlertType.Information, AlertButtons.OK);
                    }
                }
            }
        }

        private void AddAndExit()
        {
            if (lvSelectedList.Items.Count == 0)
            {
                bool result = AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1077"), "", AlertType.Information, AlertButtons.YESNO);
                if (result == true)
                {
                    this.Close();
                }
            }
            else
            {
                switch (FormType)
                {
                    case (int)SearchDishType.FoodCalorieCalculator:

                        HealthCalculator.SearchDishID.Clear();
                        for (int i = 0; i < lvSelectedList.Items.Count; i++)
                        {
                            HealthCalculator.SearchDishID.Add(((Dish)lvSelectedList.Items[i]).Id);
                        }
                        break;

                    case (int)SearchDishType.MealPlanner:
                        break;
                }

                this.Close();
            }
        }

        private double CalculateCalorie(int CalorieType)
        {
            double DishCalorie = 0;
            try
            {
                switch (CalorieType)
                {
                    case (int)CaloireType.Add:

                        if (lvDish.SelectedIndex >= 0)
                        {
                            Dish dishItem = new Dish();
                            dishItem = DishManager.GetItem(((Dish)lvDish.Items[lvDish.SelectedIndex]).Id);
                            if (dishItem != null)
                            {
                                DishCalorie = DishCalorie + CommonFunctions.Convert2Double(Convert.ToString(((Dish)lvDish.Items[lvDish.SelectedIndex]).Calorie));
                            }
                        }
                        break;
                    case (int)CaloireType.Delete:

                        if (lvSelectedList.SelectedIndex >= 0)
                        {
                            Dish dishItem = new Dish();
                            dishItem = DishManager.GetItem(((Dish)lvSelectedList.Items[lvSelectedList.SelectedIndex]).Id);
                            if (dishItem != null)
                            {
                                DishCalorie = DishCalorie + (CommonFunctions.Convert2Double(Convert.ToString(((Dish)lvSelectedList.Items[lvSelectedList.SelectedIndex]).Calorie)) / 100) * CommonFunctions.Convert2Double(Convert.ToString(((Dish)lvSelectedList.Items[lvSelectedList.SelectedIndex]).StandardWeight));
                            }
                        }
                        break;
                }

                return DishCalorie;
            }
            catch
            {
                return DishCalorie;
            }
            finally
            {

            }
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            SetCulture();
            Keyboard.Focus(cboDishCategory);
            FillCombo();
            //chkRegionalNames.IsChecked = true;
            //IncludeAyurveda();
            grdImages.Visibility = Visibility.Hidden;
        }

        private void imgAyurvedic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
                if (lvi != null)
                {
                    lvDish.SelectedIndex = lvDish.ItemContainerGenerator.IndexFromContainer(lvi);
                    Dish dish = (Dish)lvDish.SelectedItem;
                    AyurvedicDetails ayurvedicDetails = new AyurvedicDetails();
                    ayurvedicDetails.DishID = dish.Id;
                    ayurvedicDetails.ItemID = dish.Id;
                    ayurvedicDetails.DisplayItem = ItemType.Dish;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        ayurvedicDetails.IsRegional = true;
                    }
                    else
                    {
                        ayurvedicDetails.IsRegional = false;
                    }
                    ayurvedicDetails.Owner = Application.Current.MainWindow;
                    ayurvedicDetails.ShowDialog();
                }
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
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
                if (lvi != null)
                {
                    lvDish.SelectedIndex = lvDish.ItemContainerGenerator.IndexFromContainer(lvi);
                    Dish dish = (Dish)lvDish.SelectedItem;
                    NutritionDetails dishDetails = new NutritionDetails();
                    dishDetails.DishID = dish.Id;
                    dishDetails.ItemID = dish.Id;
                    dishDetails.DisplayItem = ItemType.Dish;
                    if (chkRegionalNames.IsChecked == true)
                    {
                        dishDetails.IsRegional = true;
                    }
                    else
                    {
                        dishDetails.IsRegional = false;
                    }
                    dishDetails.Owner = Application.Current.MainWindow;
                    dishDetails.ShowDialog();
                }
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
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
                if (lvi != null)
                {
                    lvDish.SelectedIndex = lvDish.ItemContainerGenerator.IndexFromContainer(lvi);
                    Dish dish = (Dish)lvDish.SelectedItem;
                    ImagePreview imagePreview = new ImagePreview();
                    imagePreview.DisplayItem = ItemType.Dish;
                    imagePreview.ItemID = dish.Id;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkFavourite_Checked(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvDish.SelectedIndex = lvDish.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void chkFavourite_UnChecked(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvDish.SelectedIndex = lvDish.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void chkFavourites_Checked(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                FillSearchList();
            }
        }

        private void chkFavourites_Unchecked(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                FillSearchList();
            }
        }

        private void chkProfileFilter_Checked(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                FillSearchList();
            }
        }

        private void chkProfileFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                FillSearchList();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                FillSearchList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvDish.SelectedIndex = lvDish.ItemContainerGenerator.IndexFromContainer(lvi);

                SelectDishList();
            }
        }

        private void lvDish_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void txtSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FillSearchList();
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            AddAndExit();
        }

        private void imgDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvSelectedList.SelectedIndex = lvSelectedList.ItemContainerGenerator.IndexFromContainer(lvi);
                DeleteDish();
            }
        }

        private void btnAddDish_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SelectDishList();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FillSearchList();
            }
        }

        private void chkRegionalNames_Checked(object sender, RoutedEventArgs e)
        {
            gColDishName.CellTemplate = this.FindResource("DisplayNameTemplate") as DataTemplate;
            gColSelectedItems.CellTemplate = this.FindResource("DisplayNameTemplate") as DataTemplate;
        }

        private void chkRegionalNames_Unchecked(object sender, RoutedEventArgs e)
        {
            gColDishName.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
            gColSelectedItems.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
        }

        private void lvSelectedList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (lvSelectedList.SelectedIndex > -1)
            {
                if (e.Key == Key.Delete)
                {
                    DeleteDish();
                }
            }
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion

    }
}

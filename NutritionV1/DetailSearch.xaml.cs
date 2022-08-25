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
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Classes;
using System.Resources;
using System.Data;
using System.Configuration;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for DetailSearch.xaml
    /// </summary>
    public partial class DetailSearch : Window
    {
        #region DECLARATIONS

        Ingredient ingredient = new Ingredient();
        List<Ingredient> ingredientList = new List<Ingredient>();
        List<Ingredient> ingredientSelectList = new List<Ingredient>();
        private static int ingredientID;
        private int searchType;

        #endregion

        #region PROPERTIES

        public static int IngredientID
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

        public int SearchType
        {
            get
            {
                return searchType;
            }
            set
            {
                searchType = value;
            }
        }

        #endregion

        public DetailSearch()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
            //FillSearchList();
        }

        #region EVENTS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            SetTheme();
            SetCulture();
            Keyboard.Focus(cboFoodGroup);
            FillCombo();
            IncludeAyurveda();
            //chkRegionalNames.IsChecked = true;
            grdImages.Visibility = Visibility.Hidden;
        }       

        private void imgAyurvedic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
                if (lvi != null)
                {
                    lvIngradient.SelectedIndex = lvIngradient.ItemContainerGenerator.IndexFromContainer(lvi);
                    Ingredient ingredient = (Ingredient)lvIngradient.SelectedItem;
                    AyurvedicDetails ayurvedicDetails = new AyurvedicDetails();
                    ayurvedicDetails.IngredientID = ingredient.Id;
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
                    lvIngradient.SelectedIndex = lvIngradient.ItemContainerGenerator.IndexFromContainer(lvi);
                    Ingredient ingredient = (Ingredient)lvIngradient.SelectedItem;
                    NutritionDetails ingredientDetails = new NutritionDetails();
                    ingredientDetails.IngredientID = ingredient.Id;
                    ingredientDetails.Owner = Application.Current.MainWindow;
                    ingredientDetails.ShowDialog();
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
                    lvIngradient.SelectedIndex = lvIngradient.ItemContainerGenerator.IndexFromContainer(lvi);
                    Ingredient ingredient = (Ingredient)lvIngradient.SelectedItem;
                    ImagePreview imagePreview = new ImagePreview();
                    imagePreview.DisplayItem = ItemType.Ingredient;
                    imagePreview.ItemID = ingredient.Id;
                    imagePreview.IsRegional = false;
                    imagePreview.Owner = Application.Current.MainWindow;
                    imagePreview.ShowDialog();
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }

        private void imgHealthValue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

            if (lvi != null)
            {
                lvIngradient.SelectedIndex = lvIngradient.ItemContainerGenerator.IndexFromContainer(lvi);
                Ingredient ingredient = (Ingredient)lvIngradient.SelectedItem;
                HealthDetails healthDetails = new HealthDetails();
                healthDetails.IngredientID = ingredient.Id;
                healthDetails.Owner = Application.Current.MainWindow;
                healthDetails.ShowDialog();
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
                lvIngradient.SelectedIndex = lvIngradient.ItemContainerGenerator.IndexFromContainer(lvi);
            }
            SelectIngredients();
        }

        private void lvIngradient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //SelectIngredients();
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
                Close();
        }

        private void btnAddList_Click(object sender, RoutedEventArgs e)
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
                if (searchType == 0)
                {
                    AddDish.IngredientID.Clear();
                    for (int i = 0; i < lvSelectedList.Items.Count; i++)
                    {
                        AddDish.IngredientID.Add(((Ingredient)lvSelectedList.Items[i]).Id);
                    }
                }
                else if (searchType ==1)
                {
                    AddDish.IngredientID.Clear();
                    for (int i = 0; i < lvSelectedList.Items.Count; i++)
                    {
                        AddDish.IngredientID.Add(((Ingredient)lvSelectedList.Items[i]).Id);
                    }
                }
                this.Close();
            }
        }

        private void imgDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvSelectedList.SelectedIndex = lvSelectedList.ItemContainerGenerator.IndexFromContainer(lvi);
                DeleteIngredient();
            }
        }

        private void cboFoodGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientList();
        }

        private void btnAddIngredient_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SelectIngredients();
            }
        }

        private void chkRegionalNames_Checked(object sender, RoutedEventArgs e)
        {
            gColIngrName.CellTemplate = this.FindResource("DisplayNameTemplate") as DataTemplate;
            gColSelectedItems.CellTemplate = this.FindResource("DisplayNameTemplate") as DataTemplate;
        }

        private void chkRegionalNames_Unchecked(object sender, RoutedEventArgs e)
        {
            gColIngrName.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
            gColSelectedItems.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
        }

        #endregion

        #region METHODS

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            this.Style = (Style)apps.SetStyle["WinStyle"];
        }

        private void SetCulture()
        {
            lblMain1.Content = "IngredientName";
            lblFoodGroup.Content = "FoodGroup";
            gColIngrName.Header = "IngredientName";
            gColCalorie.Header = "Calorie (KCal)";
            gColSelectedItems.Header = "Ingredients";
            btnAddList.Content = "Add to List ";
        }

        private void IncludeAyurveda()
        {
            if (ConfigurationManager.AppSettings["IncludeAyurvedic"] == "0")
            {
                gColIngrName.FixedWidth = gColIngrName.FixedWidth + gColAyurveda.FixedWidth;
                gColAyurveda.FixedWidth = 0;
            }
        }

        private void FillCombo()
        {
            try
            {
                Classes.CommonFunctions.FillFoodCategory(cboFoodGroup);
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
            string searchOrderBy = string.Empty;
            grdImages.Visibility = Visibility.Hidden;

            gColIngrName.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
            gColSelectedItems.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
            searchOrderBy = " IngredientName";

            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                lvIngradient.ItemsSource = null;
                lvIngradient.Items.Refresh();
                if (txtSearch.Text.Trim() != string.Empty || cboFoodGroup.SelectedIndex > 0)
                {
                    string searchString = string.Empty;
                    searchString = " 1=1 ";
                    if (txtSearch.Text.Trim() != string.Empty)
                    {                        
                        searchString = searchString + " AND IngredientName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%' ";
                    }

                    if (cboFoodGroup.SelectedIndex > 0)
                    {
                        searchString = searchString + " AND FoodHabitID = " + cboFoodGroup.SelectedValue;
                    }
                    
                    ingredientList = IngredientManager.GetDetailedSearchList(searchString, searchOrderBy);
                    lvIngradient.ItemsSource = ingredientList;
                    lvIngradient.SelectedIndex = 0;
                    lvIngradient.ScrollIntoView(lvIngradient.SelectedItem);
                    lvIngradient.Items.Refresh();
                    lvIngradient.Focus();

                    grdImages.Visibility = Visibility.Visible;
                    //}
                }
                else
                {
                    AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1083"), "", AlertType.Information, AlertButtons.OK);
                }
            }
        }
      
        private void DeleteIngredient()
        {
            try
            {
                Ingredient IngredientItem = new Ingredient();
                IngredientItem = ((Ingredient)lvSelectedList.Items[lvSelectedList.SelectedIndex]);
                lvSelectedList.ItemsSource = string.Empty;
                ingredientSelectList.Remove(IngredientItem);
                lvSelectedList.ItemsSource = ingredientSelectList;
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

        private void SelectIngredients()
        {
            if (lvIngradient.SelectedIndex >= 0)
            {
                Ingredient ingredientItem = new Ingredient();
                ingredientItem = IngredientManager.GetItem(((Ingredient)lvIngradient.Items[lvIngradient.SelectedIndex]).Id);
                if (ingredientItem != null)
                {
                    if (!CommonFunctions.IsIngredientExists(ingredientSelectList, CommonFunctions.Convert2Int(Convert.ToString(ingredientItem.Id))))
                    {
                        ingredientSelectList.Add(ingredientItem);
                        lvSelectedList.ItemsSource = ingredientSelectList;
                        lvSelectedList.Items.Refresh();
                    }
                    else
                    {
                        AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1071"), "", AlertType.Information, AlertButtons.OK);
                    }
                }
            }
        }

        public void AddIngredientList()
        {
            try
            {
                ingredientID = 0;
                NewIngredient newIngredient = new NewIngredient();
                newIngredient.Owner = Application.Current.MainWindow;
                newIngredient.ShowDialog();
                if (ingredientID > 0)
                {
                    Ingredient ingredientItem = new Ingredient();
                    ingredientItem = IngredientManager.GetItem(ingredientID);
                    if (ingredientItem != null)
                    {
                        ingredientSelectList.Add(ingredientItem);
                    }

                    if (ingredientSelectList != null)
                    {
                        lvSelectedList.ItemsSource = ingredientSelectList;
                        lvSelectedList.Items.Refresh();
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

        #endregion

        private void lvSelectedList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (lvSelectedList.SelectedIndex > -1)
            {
                if (e.Key == Key.Delete)
                {
                    DeleteIngredient();
                }
            }
        }
        
    }
}

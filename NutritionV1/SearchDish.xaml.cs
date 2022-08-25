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
    /// Interaction logic for SearchDish.xaml
    /// </summary>
    public partial class SearchDish : Window
    {
        #region DECLARATIONS

        Dish dish = new Dish();
        List<Dish> dishList = new List<Dish>();
        private static int dishID;

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
        
        #endregion

        public SearchDish()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
            FillSearchList();
        }

        #region EVENTS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            SetTheme();
            SetCulture();
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
                AddDish.DishID = ((Dish)lvDish.Items[lvDish.SelectedIndex]).Id;
                this.Close();
            }
        }

        private void lvDish_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvDish.SelectedIndex > 0)
            {
                AddDish.DishID = ((Dish)lvDish.Items[lvDish.SelectedIndex]).Id;
                this.Close();
            }
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

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
            App apps = (App)Application.Current;
            ResourceManager rm = apps.getLanguageList;
        }          

        private void FillSearchList()
        {
            string searchOrderBy = string.Empty;
            gColDishName.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
            searchOrderBy = " DishName";

            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                lvDish.ItemsSource = null;
                lvDish.Items.Refresh();
               
                string searchString = string.Empty;
                searchString = " Where IsSystemDish = false";
                if (txtSearch.Text.Trim() != string.Empty)
                {                        
                    searchString = searchString + " AND DishName LIKE '" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                }

                dishList = DishManager.GetDishList(searchString);
                if (dishList != null)
                {
                    lvDish.ItemsSource = dishList;
                    lvDish.SelectedIndex = 0;
                    lvDish.ScrollIntoView(lvDish.SelectedItem);
                    lvDish.Items.Refresh();
                    lvDish.Focus();
                }
            }
        }
        
        #endregion
       
    }
}

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
    public partial class SearchIngredient : Window
    {
        #region DECLARATIONS

        Ingredient ingredient = new Ingredient();
        List<Ingredient> ingredientList = new List<Ingredient>();
        private static int ingredientID;

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
        
        #endregion

        public SearchIngredient()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
            FillSearchList();
        }

        #region EVENTS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
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
                AddIngredient.IngredientID = ((Ingredient)lvIngradient.Items[lvIngradient.SelectedIndex]).Id;
                this.Close();
            }
        }

        private void lvIngradient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(lvIngradient.SelectedIndex > 0)
            {
                AddIngredient.IngredientID = ((Ingredient)lvIngradient.Items[lvIngradient.SelectedIndex]).Id;
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
            gColIngrName.CellTemplate = this.FindResource("NameTemplate") as DataTemplate;
            searchOrderBy = " IngredientName";

            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                lvIngradient.ItemsSource = null;
                lvIngradient.Items.Refresh();
               
                string searchString = string.Empty;
                searchString = " Where IsSystemIngredient = FALSE";
                if (txtSearch.Text.Trim() != string.Empty)
                {
                    searchString = searchString + " AND IngredientName LIKE '" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                }

                ingredientList = IngredientManager.GetIngredientList(searchString);
                if (ingredientList != null)
                {
                    lvIngradient.ItemsSource = ingredientList;
                    lvIngradient.SelectedIndex = 0;
                    lvIngradient.ScrollIntoView(lvIngradient.SelectedItem);
                    lvIngradient.Items.Refresh();
                    lvIngradient.Focus();
                }
            }
        }
        
        #endregion
       
    }
}

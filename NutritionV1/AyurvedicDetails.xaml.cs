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
using System.Windows.Shapes;
using BONutrition;
using BLNutrition;
using NutritionV1;
using System.Resources;
using System.Data; 

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for AyurvedicDetails.xaml
    /// </summary>
    public partial class AyurvedicDetails : Window
    {

        #region Declarations

        private ItemType displayitem;
        private int itemID;
        private bool isRegional;

        #endregion

        #region Properties

        public ItemType DisplayItem
        {
            set
            {
                displayitem = value;
            }
            get
            {
                return displayitem;
            }
        }

        public int ItemID
        {
            set
            {
                itemID = value;

            }
            get
            {
                return itemID;
            }

        }

        public int IngredientID
        {
            set
            {
                SetTheme();
                GetIngredientAyurvedic(value);
            }

        }

        public int DishID
        {
            set
            {
                SetTheme();
                GetDishAyurvedic(value);
            }

        }

        public bool IsRegional
        {
            set
            {
                isRegional = value;

            }
            get
            {
                return isRegional;
            }

        }

        #endregion

        #region Methods

        private void SetTheme()
        {
            App apps = (App)Application.Current;

            this.Style = (Style)apps.SetStyle["WinStyle"];
            LayoutRoot.Style = (Style)apps.SetStyle["WindowStyle"];
        }

        private void GetIngredientAyurvedic(int ingredintid)
        {
            try
            {
                List<BONutrition.IngredientAyurvedic> ingredientAyurList = new List<BONutrition.IngredientAyurvedic>();
                ingredientAyurList = IngredientAyurvedicManager.GetListAyurvedic(ingredintid);
                if (ingredientAyurList != null)
                {
                    lvAyurvedic.DataContext = ingredientAyurList;
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

        private void GetDishAyurvedic(int DishID)
        {
            try
            {
                List<BONutrition.IngredientAyurvedic> ingredientAyurList = new List<BONutrition.IngredientAyurvedic>();
                ingredientAyurList = IngredientAyurvedicManager.GetListAyurvedicDish(DishID);
                if (ingredientAyurList != null)
                {
                    lvAyurvedic.DataContext = ingredientAyurList;
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

        private void GetItemHeader()
        {
            string Header = string.Empty;

            if (DisplayItem == ItemType.Dish)
            {
                
            }
            else
            {
                Ingredient Ingredient = new Ingredient();
                Ingredient = IngredientManager.GetItem(ItemID);
                if (Ingredient != null)
                {
                    if (IsRegional == true)
                    {
                        Header = Ingredient.DisplayName;
                    }
                    else
                    {
                        Header = Ingredient.Name;
                    }
                }
            }

            this.Title = "    " + Header;
        }

        #endregion

        # region Constructor

        public AyurvedicDetails()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region Events

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetItemHeader();
        }

        #endregion

    }
}

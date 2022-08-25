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
using System.Resources;
using BONutrition;
using BLNutrition;
using NutritionV1.Enums;
using NutritionV1.Classes;
using NutritionV1.Common.Classes;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for IngredientDetails.xaml
    /// </summary>
    public partial class NutritionDetails : Window
    {
        #region Declarations

        private int dishIngredientID = 0;
        private ItemType displayitem;
        private int itemID;
        private int planID;
        private int standardWeight;
        private bool isRegional;

        #endregion

        #region PROPERTIES

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
                GetIngredient(value);
                GetIngredientAmino(value);
                GetIngredientFatty(value);
            }

        }

        public int IngredientDishID
        {
            set
            {
                SetTheme();
                GetIngredientDish(value);
                GetIngredientDishAmino(value);
                GetIngredientDishFatty(value);
            }

        }

        public int DishIngredientID
        {
            set
            {
                dishIngredientID = value;
            }
            get
            {
                return dishIngredientID;
            }

        }

        public int PlanID
        {
            set
            {
                planID = value;
            }
            get
            {
                return planID;
            }
        }

        public int StandardWeight
        {
            set
            {
                standardWeight = value;
            }
            get
            {
                return standardWeight;
            }
        }

        public int DishID
        {
            set
            {
                SetTheme();
                GetDish(value);
                GetDishAmino(value);
                GetDishFatty(value);
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

        #region METHODS

        private void SetTheme()
        {
            App apps = (App)Application.Current;

            this.Style = (Style)apps.SetStyle["WinStyle"];
            LayoutRoot.Style = (Style)apps.SetStyle["WindowStyle"];
        }

        private void SetCulture()
        {
            App apps = (App)Application.Current;            
            tbNutrient.Header = "Nutrients";
            gvNutrientName.Header = "Total Nutrient Value";
            gvNutValues.Header = "Nutrient Value (BreakFast)";
            gvNutUnit.Header = "Unit";
        }

        private void GetIngredient(int IngredintID)
        {
            try
            {
                List<BONutrition.IngredientNutrients> ingredientNutriList = new List<BONutrition.IngredientNutrients>();
                ingredientNutriList = IngredientNutrientsManager.GetListNutrientValues(IngredintID, (byte)NutrientGroup.Nutrients);
                lvNutrients.DataContext = ingredientNutriList;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void GetIngredientAmino(int IngredintID)
        {
            try
            {
                List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
                ingredientAminoList = IngredientAminoAcidManager.GetListNutrientValues(IngredintID, (byte)NutrientGroup.AminoAcid);
                lvAmino.DataContext = ingredientAminoList;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        private void GetIngredientFatty(int IngredintID)
        {
            try
            {
                List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
                ingredientFattyList = IngredientFattyAcidManager.GetListNutrientValues(IngredintID, (byte)NutrientGroup.FattyAcid);
                lvFatty.DataContext = ingredientFattyList;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void GetIngredientDish(int ingredintid)
        {
            try
            {
                List<BONutrition.IngredientNutrients> ingredientNutriList = new List<BONutrition.IngredientNutrients>();
                ingredientNutriList = IngredientNutrientsManager.GetIngredientNutrients(ingredintid, (byte)NutrientGroup.Nutrients, DishIngredientID);
                lvNutrients.DataContext = ingredientNutriList;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void GetIngredientDishAmino(int ingredintid)
        {
            try
            {
                List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
                ingredientAminoList = IngredientAminoAcidManager.GetAminoAcidList(ingredintid, (byte)NutrientGroup.AminoAcid, DishIngredientID);
                lvAmino.DataContext = ingredientAminoList;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        private void GetIngredientDishFatty(int ingredintid)
        {
            try
            {
                List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
                ingredientFattyList = IngredientFattyAcidManager.GetFattyAcidList(ingredintid, (byte)NutrientGroup.FattyAcid, DishIngredientID);
                lvFatty.DataContext = ingredientFattyList;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void GetDish(int DishID)
        {
            try
            {
                try
                {
                    List<BONutrition.IngredientNutrients> ingredientNutriList = new List<BONutrition.IngredientNutrients>();
                    ingredientNutriList = IngredientNutrientsManager.GetListNutrientsDish(DishID, PlanID, (byte)NutrientGroup.Nutrients);
                    lvNutrients.DataContext = ingredientNutriList;
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.Message);
                }
                finally
                {

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

        private void GetDishAmino(int DishID)
        {
            try
            {
                try
                {
                    List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
                    ingredientAminoList = IngredientAminoAcidManager.GetListNutrientsDish(DishID,PlanID,(byte)NutrientGroup.AminoAcid);
                    lvAmino.DataContext = ingredientAminoList;
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.Message);
                }
                finally
                {

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

        private void GetDishFatty(int DishID)
        {
            try
            {
                try
                {
                    List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
                    ingredientFattyList = IngredientFattyAcidManager.GetListNutrientsDish(DishID,PlanID,(byte)NutrientGroup.FattyAcid);
                    lvFatty.DataContext = ingredientFattyList;
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.Message);
                }
                finally
                {

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
                Dish Dish = new Dish();
                Dish = DishManager.GetItem(ItemID, " 1=1 ");
                if (Dish != null)
                {
                    lblHeader.Content = "Nutrient Details Per " + StandardWeight + " gm";
                }
            }
            else
            {
                Ingredient Ingredient = new Ingredient();
                Ingredient = IngredientManager.GetItem(ItemID);
                if (Ingredient != null)
                {
                    
                }
            }
        }

        #endregion

        #region CONSTRUCTOR

        public NutritionDetails()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region EVENTS

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

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion
    }
}

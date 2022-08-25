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
using NutritionV1.Classes;
using NutritionV1.Enums;
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for NewIngredient.xaml
    /// </summary>
    public partial class NewIngredient : Window
    {
        #region DECLARATIONS

        Ingredient ingredient = new Ingredient();
        private List<StandardUnit> standardUnitList = new List<StandardUnit>();
        private int ingredientID;

        #endregion

        #region CONSTRUCTOR

        public NewIngredient()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion      
        
        #region METHODS

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            //grdNewIngredient.Style = (Style)apps.SetStyle["WindowStyle"];
            this.Style = (Style)apps.SetStyle["WinStyle"];
        }

        private void GetIngredientList()
        {
            try
            {                
                ingredient = new Ingredient();                
                ingredient.Id = IngredientManager.GetID();
                ingredientID = ingredient.Id;

                ingredient.EthnicID = Convert.ToInt32(cboEthnic.SelectedValue);
                ingredient.FoodHabitID = Convert.ToByte(cboIngredientType.SelectedValue);
                
                ingredient.MedicalFavourable = string.Empty;
                ingredient.MedicalUnFavourable = string.Empty;
                ingredient.MedicalBalanced = string.Empty;
                                               
                ingredient.AyurvedicFavourable = string.Empty;
                ingredient.AyurvedicUnFavourable = string.Empty;
                ingredient.AyurvedicBalanced = string.Empty;

                ingredient.Keywords = string.Empty;
                ingredient.ScrappageRate = 0;
                ingredient.WeightChangeRate = 0;
                ingredient.DisplayImage = string.Empty;                
                ingredient.IsAllergic = false;

                ingredient.IsSystemIngredient = false;
                ingredient.FrozenLife = 0;
                ingredient.RefrigeratedLife = 0;
                ingredient.ShelfLife = 0;
                ingredient.DisplayOrder = 0;

                ingredient.ScrappageRate = CommonFunctions.Convert2Float(txtScrapRate.Text.Trim());
                ingredient.WeightChangeRate = CommonFunctions.Convert2Float(txtWeightChange.Text.Trim());

                // Ingredient Lan (Primary)
                ingredient.Name = CommonFunctions.Convert2String(txtIngredientNameENG.Text.Trim());
                if (txtIngredientNameDisplay.Text.Trim() != string.Empty)
                {
                    ingredient.DisplayName = CommonFunctions.Convert2String(txtIngredientNameDisplay.Text.Trim());
                }
                else
                {
                    ingredient.DisplayName = CommonFunctions.Convert2String(txtIngredientNameENG.Text.Trim());
                }

                ingredient.Comments = string.Empty;
                ingredient.GeneralHealthValue = "";
                ingredient.AyurHealthValue = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void GetNutrientsList()
        {
            try
            {
                List<NSysNutrient> sysNutrientList = new List<NSysNutrient>();

                IngredientAminoAcid ingredientAmino = new IngredientAminoAcid();
                List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();

                IngredientFattyAcid ingredientFatty = new IngredientFattyAcid();
                List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();

                IngredientNutrients ingredientNutrients = new IngredientNutrients();
                List<IngredientNutrients> ingredientNutrientsList = new List<IngredientNutrients>();

                IngredientAyurvedic ingredientAyurvedic = new IngredientAyurvedic();
                List<IngredientAyurvedic> ingredientAyurvedicList = new List<IngredientAyurvedic>();

                sysNutrientList = NSysNutrientManager.GetListNutrient((byte)NutrientGroup.AminoAcid);
                if (sysNutrientList != null)
                {
                    for (int i = 0; i < sysNutrientList.Count; i++)
                    {
                        ingredientAmino = new IngredientAminoAcid();
                        ingredientAmino.IngredientId = ingredientID;
                        ingredientAmino.NutrientID = sysNutrientList[i].NutrientID;                        
                        ingredientAmino.NutrientValue = 0;
                        ingredientAminoList.Add(ingredientAmino);
                    }
                }
                ingredient.IngredientAminoAcidList = ingredientAminoList;

                sysNutrientList = NSysNutrientManager.GetListNutrient((byte)NutrientGroup.FattyAcid);
                if (sysNutrientList != null)
                {
                    for (int i = 0; i < sysNutrientList.Count; i++)
                    {
                        ingredientFatty = new IngredientFattyAcid();
                        ingredientFatty.IngredientId = ingredientID;
                        ingredientFatty.NutrientID = sysNutrientList[i].NutrientID;
                        ingredientFatty.NutrientValue = 0;
                        ingredientFattyList.Add(ingredientFatty);
                    }
                }
                ingredient.IngredientFattyAcidList = ingredientFattyList;

                sysNutrientList = NSysNutrientManager.GetListNutrient((byte)NutrientGroup.Nutrients);
                if (sysNutrientList != null)
                {
                    for (int i = 0; i < sysNutrientList.Count; i++)
                    {
                        ingredientNutrients = new IngredientNutrients();
                        ingredientNutrients.IngredientId = ingredientID;
                        ingredientNutrients.NutrientID = sysNutrientList[i].NutrientID;
                        ingredientNutrients.NutrientValue = 0;
                        ingredientNutrientsList.Add(ingredientNutrients);
                    }
                }
                ingredient.IngredientNutrientsList = ingredientNutrientsList;

                sysNutrientList = NSysNutrientManager.GetListAyurvedic();
                if (sysNutrientList != null)
                {
                    for (int i = 0; i < sysNutrientList.Count; i++)
                    {
                        ingredientAyurvedic = new IngredientAyurvedic();
                        ingredientAyurvedic.IngredientId = ingredientID;
                        ingredientAyurvedic.AyurID = sysNutrientList[i].AyurID;
                        ingredientAyurvedic.AyurValue = string.Empty;
                        ingredientAyurvedic.AyurValueREG = string.Empty;
                        ingredientAyurvedicList.Add(ingredientAyurvedic);
                    }
                }
                ingredient.IngredientAyurvedicList = ingredientAyurvedicList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void GetStandardUnitList()
        {
            try
            {
                IngredientStandardUnit ingredientStandardUnit = new IngredientStandardUnit();
                List<IngredientStandardUnit> ingredientUnitUpdateList = new List<IngredientStandardUnit>();
                for (int i = 0; i < lvStandardUnit.Items.Count; i++)
                {
                    if (standardUnitList.Count > 0)
                    {
                        if (((StandardUnit)lvStandardUnit.Items[i]).IsApplicable == true)
                        {
                            ingredientStandardUnit = new IngredientStandardUnit();
                            ingredientStandardUnit.IngredientID = ingredientID;
                            ingredientStandardUnit.StandardUnitID = ((StandardUnit)lvStandardUnit.Items[i]).StandardUnitID;
                            ingredientStandardUnit.StandardWeight = ((StandardUnit)lvStandardUnit.Items[i]).StandardWeight;
                            ingredientUnitUpdateList.Add(ingredientStandardUnit);
                        }
                    }
                }
                ingredient.IngredientStandardUnitList = ingredientUnitUpdateList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private bool ValidateIngredient()
        {
            if (txtIngredientNameENG.Text == string.Empty)
            {
                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1243"), "", AlertType.Information, AlertButtons.OK);
                txtIngredientNameENG.Focus();
                return false;
            }
            else if (txtIngredientNameDisplay.Text == string.Empty)
            {
                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1276"), "", AlertType.Information, AlertButtons.OK);
                txtIngredientNameDisplay.Focus();
                return false;
            }
            else if (cboEthnic.SelectedIndex <= 0)
            {
                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1244"), "", AlertType.Information, AlertButtons.OK);
                cboEthnic.Focus();
                return false;
            }
            else if (cboIngredientType.SelectedIndex <= 0)
            {
                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1245"), "", AlertType.Information, AlertButtons.OK);
                cboIngredientType.Focus();
                return false;
            }           

            for (int i = 0; i < lvStandardUnit.Items.Count; i++)
            {
                if (standardUnitList.Count > 0)
                {
                    if (((StandardUnit)lvStandardUnit.Items[i]).StandardUnitType != (int)StandardUnitType.TypeII)
                    {
                        if (((StandardUnit)lvStandardUnit.Items[i]).IsApplicable == true)
                        {
                            if (((StandardUnit)lvStandardUnit.Items[i]).StandardWeight <= 0)
                            {
                                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1246") + ((StandardUnit)lvStandardUnit.Items[i]).StandardUnitName, "", AlertType.Information, AlertButtons.OK);
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void LoadStandardUnits()
        {
            standardUnitList = StandardUnitManager.GetList(0);
            if (standardUnitList != null)
            {
                lvStandardUnit.ItemsSource = standardUnitList;
            }
        }

        private void FillCombo()
        {
            Classes.CommonFunctions.FillFoodCategory(cboIngredientType);
            Classes.CommonFunctions.FillEthnic(cboEthnic);
        }

        #endregion

        #region EVENTS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            FillCombo();
            LoadStandardUnits();
            if(CommonFunctions.Convert2Float(txtScrapRate.Text.Trim()) <= 0)
                txtScrapRate.Text = "100";

            if (CommonFunctions.Convert2Float(txtWeightChange.Text.Trim()) <= 0)
                txtWeightChange.Text = "100";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateIngredient() == true)
                {
                    GetIngredientList();
                    GetNutrientsList();
                    GetStandardUnitList();                    
                    IngredientManager.Save(ingredient);
                    AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1002"), "", AlertType.Information, AlertButtons.OK);

                    DetailSearch.IngredientID = 0;
                    DetailSearch.IngredientID = ingredientID;
                    this.Close();
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

        private void chkStandardUnit_Checked(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvStandardUnit.SelectedIndex = lvStandardUnit.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void chkStandardUnit_Unchecked(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvStandardUnit.SelectedIndex = 0;
            }
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

        private void lvList_LostFocus(object sender, RoutedEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv != null)
            {
                lv.SelectedItems.Clear();
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void txtIngredientNameENG_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtIngredientNameDisplay.Text = txtIngredientNameENG.Text;
        }

        #endregion
    }
}

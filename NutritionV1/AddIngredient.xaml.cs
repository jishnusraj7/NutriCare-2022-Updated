using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Resources;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Configuration;
using System.Collections.ObjectModel;

using BONutrition;  
using BLNutrition;
using NutritionV1.Enums;
using NutritionV1.Classes;
using NutritionV1.Common.Classes;
using Indocosmo.Framework.ExceptionManagement;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for AddIngredient.xaml
    /// </summary>
    public partial class AddIngredient : Page
    {
        #region DECLARATIONS

        private enum ListViewIndex
        {
            AminoAcids = 2,
            Nutrients = 2,
            FattyAcids = 2,
        }

        private bool isSystemIngredient;
        private static int ingredientID;
        private bool isDisplayMode;
        private string imageDefaultPath = string.Empty;
        private string imageFileName = string.Empty;                
        private string searchString = string.Empty;

        private bool isNutrientsLoad;        
        private bool isPropertiesLoad;
        private bool IsImageAdd;

        private string uploadCaption = string.Empty;
        private string removeCaption = string.Empty;
        private const string specialChar = " ● ";

        private Ingredient ingredient = new Ingredient();
        private IngredientAminoAcid ingredientAmino = new IngredientAminoAcid();
        private IngredientFattyAcid ingredientFatty = new IngredientFattyAcid();
        private IngredientNutrients ingredientNutri = new IngredientNutrients();
        private IngredientStandardUnit ingredientStandardUnit = new IngredientStandardUnit();
        private StandardUnit standardUnitItem = new StandardUnit();

        private List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
        private List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
        private List<IngredientNutrients> ingredientNutriList = new List<IngredientNutrients>();
        private List<IngredientStandardUnit> ingredientStandardUnitList = new List<IngredientStandardUnit>();

        private List<Ingredient> ingredientList = new List<Ingredient>();
        private List<NSysNutrient> sysNutrientList = new List<NSysNutrient>();
        private List<StandardUnit> standardUnitList = new List<StandardUnit>();
        
        Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog();

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

        public bool IsSystemIngredient
        {
            get
            {
                return isSystemIngredient;
            }
            set
            {
                isSystemIngredient = value;
            }
        }

        #endregion

        #region CONSTRUCTOR

        public AddIngredient()
        {
            InitializeComponent();
        }

        #endregion

        #region EVENTS

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SetTheme();
                SetCulture();
                Classes.CommonFunctions.FillNutrients(cboSearch);
                FillCombo();
                LoadDefaults();
                ClearAllValues();
                SetButtonVisibility();
                txtHelp.Text = specialChar + " Source :  Datas on RTS and RTC : (USDA hand book of agriculture No:102) " + System.Environment.NewLine + specialChar + " Ready to serve datas may vary depending upon cooking methods";
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message);
            }
        }      

        private void txtNumber_PreviewKeyDown(object sender, KeyEventArgs e)
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
                AlertBox.Show(ex.Message);
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
      
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
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
                AlertBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SearchIngredient searchIngredient = new SearchIngredient();
            searchIngredient.Owner = Application.Current.MainWindow;
            searchIngredient.ShowDialog();
            if (ingredientID > 0)
            {
                using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                {
                    LoadIngredient();
                    SetButtonVisibility();
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateIngredient() == true)
                {
                    GetIngredientList();
                    GetAminoAcidList();
                    GetNutrientsList();
                    GetFattyAcidList();
                    GetStandardUnitList();

                    if (IngredientStandardUnitManager.DeleteStandardUnit(ingredientID))
                    {
                        using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                        {
                            IngredientManager.Save(ingredient);
                        }
                        AlertBox.Show(XMLServices.GetXmlMessage("E1002"), "", AlertType.Information, AlertButtons.OK);
                        //cboIngredientName.Focus();
                        ClearAllValues();
                        txtIngredientNameENG.Focus();
                    }
                    else
                    {
                        AlertBox.Show("Updation Failed", "", AlertType.Error,AlertButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message);
            }
            finally
            {

            }
        }
              
        private void lvList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListView view = sender as ListView;
                GridView gridView = view.View as GridView;
                if (view.ActualWidth > 0)
                    gridView.Columns[0].Width = view.ActualWidth - 5;
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message);
            }
        }
        
        private void tbAddIngredient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                {
                    if (ingredientID > 0)
                    {
                        TabDisplay();
                    }
                }
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
            CheckBox chk = (CheckBox)sender;
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvStandardUnit.SelectedIndex = lvStandardUnit.ItemContainerGenerator.IndexFromContainer(lvi);
            }

            if (lvStandardUnit.SelectedIndex >= 0)
            {
                int rowIndex = lvStandardUnit.SelectedIndex;
                int standardUnit = ((StandardUnit)lvStandardUnit.Items[rowIndex]).StandardUnitID;
                if (ingredient != null)
                {
                    if (IngredientManager.GetStandardUnitCount(ingredient, standardUnit) > 0)
                    {
                        AlertBox.Show(((StandardUnit)lvStandardUnit.Items[rowIndex]).StandardUnitName + XMLServices.GetXmlMessage("E1274"), "Alert", AlertType.Error, AlertButtons.OK);
                        chk.IsChecked = true;
                    }
                }
            }
        }

        private void chkStandardUnit_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                if (lvStandardUnit.SelectedIndex <= 0)
                {
                    
                }
            }
        }    
       
        private void cboSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboSearch.SelectedItem != null)
                {
                    SearchNutrients(lvNutrient,((NSysNutrient)(cboSearch.SelectedItem)).NutrientParam, (int)NutrientGroup.Nutrients);
                    SearchNutrients(lvFattyAcid,((NSysNutrient)(cboSearch.SelectedItem)).NutrientParam, (int)NutrientGroup.FattyAcid);
                    SearchNutrients(lvAmino,((NSysNutrient)(cboSearch.SelectedItem)).NutrientParam, (int)NutrientGroup.AminoAcid);
                }
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message);
            }
        }

        private void txtNutriValue_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvNutrient.SelectedIndex = lvNutrient.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void txtAminoValue_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvAmino.SelectedIndex = lvAmino.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void txtFattyValue_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvFattyAcid.SelectedIndex = lvFattyAcid.ItemContainerGenerator.IndexFromContainer(lvi);
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteIngredient();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            ReportViewer ingReport = new ReportViewer();
            ingReport.ReportType = (int)ReportItem.Ingredient;
            ingReport.IngredientID = ingredientID;
            ingReport.IsRegional = false;            
            ingReport.Owner = Application.Current.MainWindow;
            ingReport.ShowDialog();
        }

        private void txtDescriptionENG_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                tbAddIngredient.SelectedIndex = 0;
                CommonFunctions.SetControlFocus(txtWeightChange);
            }
            else if (e.Key == Key.Tab)
            {
                tbAddIngredient.SelectedIndex = 1;
                CommonFunctions.SetControlFocus(cboSearch);
            }
        }

        private void cboSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                tbAddIngredient.SelectedIndex = 0;
                CommonFunctions.SetControlFocus(txtDescriptionENG);
            }
            else if (e.Key == Key.Tab)
            {
                lvNutrient.SelectedIndex = 0;
                ItemContainerGenerator generator = this.lvNutrient.ItemContainerGenerator;
                ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(lvNutrient.SelectedIndex);
                TextBox txtValue = ListViewHelper.GetDescendantByType(selectedItem, typeof(TextBox), "txtNutriValue") as TextBox;
                if (txtValue != null)
                {
                    CommonFunctions.SetControlFocus(txtValue);
                }
            }
        }

        private void txtNutriValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Classes.CommonFunctions.FilterNumeric(sender, e);            
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                if (lvNutrient.SelectedIndex == 0)
                {
                    CommonFunctions.SetControlFocus(cboSearch);
                }
            }
            else if (e.Key == Key.Tab)
            {
                if (lvNutrient.SelectedIndex == (lvNutrient.Items.Count - 1))
                {
                    lvFattyAcid.SelectedIndex = 0;
                    ItemContainerGenerator generator = this.lvFattyAcid.ItemContainerGenerator;
                    ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(lvFattyAcid.SelectedIndex);
                    TextBox txtValue = ListViewHelper.GetDescendantByType(selectedItem, typeof(TextBox), "txtFattyValue") as TextBox;
                    if (txtValue != null)
                    {
                        CommonFunctions.SetControlFocus(txtValue);
                    }
                }
            }
        }

        private void txtFattyValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Classes.CommonFunctions.FilterNumeric(sender, e);
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                if (lvFattyAcid.SelectedIndex == 0)
                {
                    lvNutrient.SelectedIndex = lvNutrient.Items.Count - 1;
                    ItemContainerGenerator generator = this.lvNutrient.ItemContainerGenerator;
                    ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(lvNutrient.SelectedIndex);
                    TextBox txtValue = ListViewHelper.GetDescendantByType(selectedItem, typeof(TextBox), "txtNutriValue") as TextBox;
                    if (txtValue != null)
                    {
                        CommonFunctions.SetControlFocus(txtValue);
                    }
                }
            }
            else if (e.Key == Key.Tab)
            {
                if (lvFattyAcid.SelectedIndex == (lvFattyAcid.Items.Count - 1))
                {
                    lvAmino.SelectedIndex = 0;
                    ItemContainerGenerator generator = this.lvAmino.ItemContainerGenerator;
                    ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(lvAmino.SelectedIndex);
                    TextBox txtValue = ListViewHelper.GetDescendantByType(selectedItem, typeof(TextBox), "txtAminoValue") as TextBox;
                    if (txtValue != null)
                    {
                        CommonFunctions.SetControlFocus(txtValue);
                    }
                }
            }
        }

        private void txtAminoValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Classes.CommonFunctions.FilterNumeric(sender, e);
            if (e.Key == Key.Tab && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                if (lvAmino.SelectedIndex == 0)
                {
                    lvFattyAcid.SelectedIndex = lvFattyAcid.Items.Count - 1;
                    ItemContainerGenerator generator = this.lvFattyAcid.ItemContainerGenerator;
                    ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(lvFattyAcid.SelectedIndex);
                    TextBox txtValue = ListViewHelper.GetDescendantByType(selectedItem, typeof(TextBox), "txtFattyValue") as TextBox;
                    if (txtValue != null)
                    {
                        CommonFunctions.SetControlFocus(txtValue);
                    }
                }
            }
            else if (e.Key == Key.Tab)
            {
                if (lvAmino.SelectedIndex == (lvAmino.Items.Count - 1))
                {
                    tbAddIngredient.SelectedIndex = 3;
                    CommonFunctions.SetControlFocus(txtHealthValueAYUR);
                }
            }
        }

        private void txtHealthValueGENR_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                tbAddIngredient.SelectedIndex = 4;
            }
        }
               
        private void txtQuantity_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvStandardUnit.SelectedIndex = lvStandardUnit.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void txtQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Classes.CommonFunctions.FilterNumeric(sender, e);
            if (e.Key == Key.Tab)
            {
                if (lvStandardUnit.SelectedIndex == (lvStandardUnit.Items.Count - 1))
                {                                        
                    CommonFunctions.SetControlFocus(btnSave);
                }
            }
        }

        private void txtIngredientNameENG_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtIngredientNameDisplay.Text = txtIngredientNameENG.Text;
        }

        private void chkStandardUnit_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvStandardUnit.SelectedIndex = lvStandardUnit.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                IngradientSearch ingrSearch = new IngradientSearch();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(ingrSearch);
            }
        }
               
        //protected void cboIngredientName_PatternChanged(object sender, NutritionV1.AutoComplete.AutoCompleteArgs args)
        //{
        //    if (string.IsNullOrEmpty(args.Pattern))
        //    {
        //        args.CancelBinding = true;
        //    }
        //    else
        //    {
        //        args.DataSource = GetIngredients(args.Pattern);
        //    }
        //}

        //private static ObservableCollection<Ingredient> GetIngredients(string Pattern)
        //{
        //    return new ObservableCollection<Ingredient>(
        //        IngredientManager.GetIngredientList(" WHERE IsSystemIngredient = 0").
        //        Where((Ingredient, match) => Ingredient.Name.ToLower().StartsWith(Pattern.ToLower())));
        //}

        #endregion

        #region METHODS

        /// <summary>
        /// Apply the theme style to individual controls
        /// </summary>
        private void SetTheme()
        {
            App apps = (App)Application.Current;
            lblDaysFrozen.Style = (Style)apps.SetStyle["LabelStyle"];
            lblDaysRefrigerated.Style = (Style)apps.SetStyle["LabelStyle"];
            lblDaysShelf.Style = (Style)apps.SetStyle["LabelStyle"];
            lblDescriptionENG.Style = (Style)apps.SetStyle["LabelStyle"];
            lblDescriptionREG.Style = (Style)apps.SetStyle["LabelStyle"];
            lblFrozen.Style = (Style)apps.SetStyle["LabelStyle"];
            lblIngredientNameENG.Style = (Style)apps.SetStyle["LabelStyle"];
            lblIngredientNameREG.Style = (Style)apps.SetStyle["LabelStyle"];
            lblIngredientType.Style = (Style)apps.SetStyle["LabelStyle"];
            lblRefrigerated.Style = (Style)apps.SetStyle["LabelStyle"];
            lblShelf.Style = (Style)apps.SetStyle["LabelStyle"];
            lblEthnic.Style = (Style)apps.SetStyle["LabelStyle"];
            lblScrapRate.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWeightChange.Style = (Style)apps.SetStyle["LabelStyle"];            
            imgDisplay.SetThemes = true;
        }

        /// <summary>
        /// Apply language to Individual Captions
        /// </summary>
        private void SetCulture()
        {            
            uploadCaption = "Upload Image";
            removeCaption = "Remove Image";

            lblIngredientNameENG.Content = "Ingredient Name";
            lblIngredientNameREG.Content = "Regional Name";
            lblEthnic.Content = "Ethnic";
            lblIngredientType.Content = "Food Type";
            chkAllergic.Content = "Allergy";
            lblPeriod.Content = "Period of Expiry";
            lblShelf.Content = "Shelf Life";
            lblRefrigerated.Content = "Refrigerate Life";
            lblFrozen.Content = "Frozen Life";
            lblDaysShelf.Content = "days";
            lblDaysRefrigerated.Content = "days";
            lblDaysFrozen.Content = "days";
            
            lblStandardUnit.Content = "Standard Unit";
            gvUnitNameCol.Header = "Standard Unit";
            gvGramCol.Header = "Unit";              
        }

        /// <summary>
        /// Fill the Dropdown from DB
        /// </summary>
        private void FillCombo()
        {
            //Classes.CommonFunctions.FillIngredients(cboIngredientName);
            Classes.CommonFunctions.FillFoodCategory(cboIngredientType);
            Classes.CommonFunctions.FillEthnic(cboEthnic);
        }
        
        /// <summary>
        /// Fill the Nutrients Parameters
        /// </summary>
        private void FillNutrientParameters()
        {
            LoadAmino();
            LoadFatty();
            LoadNutrients();            
            LoadStandardUnits();
        }

        /// <summary>
        /// Load Display mode OR Addition mode
        /// </summary>
        public void LoadDefaults()
        {
            imgDisplay.ImageSource = string.Empty;
            IsImageAdd = true;

            CreateMode();                
            isNutrientsLoad = false;
            isPropertiesLoad = false;
        }

        private void SetButtonVisibility()
        {
            if (ingredientID > 0)
            {
                btnDelete.Visibility = Visibility.Visible;
                btnPrint.Visibility = Visibility.Visible;
            }
            else
            {
                btnDelete.Visibility = Visibility.Collapsed;
                btnPrint.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Clear all Values
        /// </summary>
        private void ClearAllValues()
        {
            txtIngredientNameENG.Text = string.Empty;
            txtIngredientNameDisplay.Text = string.Empty;
            txtIngredientNameREG.Text = string.Empty;
            cboEthnic.SelectedIndex = 0;
            cboIngredientType.SelectedIndex = 0;
            txtScrapRate.Text = string.Empty;
            txtWeightChange.Text = string.Empty;
            chkAllergic.IsChecked = false;
            txtShelfLife.Text = string.Empty;
            txtRefrigeratedLife.Text = string.Empty;
            txtFrozenLife.Text = string.Empty;
            btnAddImage.Content = uploadCaption;
            imgDisplay.ImageSource = string.Empty;
            IsImageAdd = true;

            txtDescriptionENG.Text = string.Empty;
            txtDescriptionREG.Text = string.Empty;
            txtHealthValueAYUR.Text = string.Empty;
            txtHealthValueGENR.Text = string.Empty;

            FillNutrientParameters();
            //FillCategory();            
            tbAddIngredient.SelectedIndex = 0;
            ingredientID = 0;
        }

        /// <summary>
        /// Get Ingredient List for DB Updation
        /// </summary>
        private void GetIngredientList()
        {
            try
            {
                string imageDestination;
                string keywordsIncreases = string.Empty;
                string keywordsDecreases = string.Empty;
                string keywordsBalances = string.Empty;
                ingredient = new Ingredient();
                if (ingredientID > 0)
                {
                    ingredient.Id = ingredientID;                    
                }
                else
                {
                    ingredient.Id = IngredientManager.GetID();
                }

                ingredient.EthnicID =  CommonFunctions.Convert2Int(Convert.ToString(cboEthnic.SelectedValue));
                ingredient.FoodHabitID = Convert.ToByte(cboIngredientType.SelectedValue);             

                ingredient.Keywords = string.Empty;
                ingredient.ScrappageRate = CommonFunctions.Convert2Float(txtScrapRate.Text.Trim());
                ingredient.WeightChangeRate = CommonFunctions.Convert2Float(txtWeightChange.Text.Trim());

                if (Convert.ToString(imageFileName).Trim() != string.Empty)
                {
                    imageDestination = GetImagePath("Ingredients") + "\\" + ingredient.Id + ".jpg";
                    if (Convert.ToString(imageFileName).Trim() != Convert.ToString(imageDestination).Trim())
                    {
                        if (File.Exists(imageFileName))
                        {
                            ingredient.DisplayImage = CopyFile(imageFileName, imageDestination);
                        }
                        else
                        {
                            ingredient.DisplayImage = imageFileName;
                        }
                    }
                    else
                    {
                        ingredient.DisplayImage = imageFileName;
                    }
                }
                else
                {
                    ingredient.DisplayImage = string.Empty;
                }

                if (chkAllergic.IsChecked == true)
                {
                    ingredient.IsAllergic = true;
                }
                else
                {
                    ingredient.IsAllergic = false;
                }
                ingredient.IsSystemIngredient = false;
                ingredient.FrozenLife = CommonFunctions.Convert2Int(txtFrozenLife.Text.Trim());
                ingredient.RefrigeratedLife = CommonFunctions.Convert2Int(txtRefrigeratedLife.Text.Trim());
                ingredient.ShelfLife = CommonFunctions.Convert2Int(txtShelfLife.Text.Trim());
                ingredient.DisplayOrder = 0;
                ingredient.GeneralHealthValue = CommonFunctions.Convert2String(txtHealthValueGENR.Text);
                ingredient.AyurHealthValue = CommonFunctions.Convert2String(txtHealthValueAYUR.Text);

                // Ingredient Lan (Primary)
                ingredient.Name = CommonFunctions.Convert2String(txtIngredientNameENG.Text.Trim()); //CommonFunctions.Convert2String(cboIngredientName.Text.Trim());
                if (txtIngredientNameDisplay.Text.Trim() != string.Empty)
                {
                    ingredient.DisplayName = CommonFunctions.Convert2String(txtIngredientNameDisplay.Text.Trim());
                }
                else
                {
                    ingredient.DisplayName = CommonFunctions.Convert2String(txtIngredientNameENG.Text.Trim()); //CommonFunctions.Convert2String(cboIngredientName.Text.Trim());
                }
                ingredient.Comments = CommonFunctions.Convert2String(txtDescriptionENG.Text.Trim());
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        
        /// <summary>
        /// Get AminoAcid Values List for DB Updation
        /// </summary>
        private void GetAminoAcidList()
        {
            try
            {
                List<IngredientAminoAcid> ingredientAminoUpdateList = new List<IngredientAminoAcid>();            
                for (int i = 0; i < lvAmino.Items.Count; i++)
                {
                    ingredientAmino = new IngredientAminoAcid();
                    ingredientAmino.IngredientId = ingredient.Id;
                    if (Convert.ToString(lvAmino.Items[i]) == "BONutrition.IngredientAminoAcid")
                    {
                        ingredientAmino.NutrientID = ((IngredientAminoAcid)lvAmino.Items[i]).NutrientID;
                        ingredientAmino.NutrientValue = ((IngredientAminoAcid)lvAmino.Items[i]).NutrientValue;
                    }
                    else
                    {
                        if (sysNutrientList.Count > 0)
                        {
                            ingredientAmino.NutrientID = ((NSysNutrient)lvAmino.Items[i]).NutrientID;
                            if (((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvAmino, (int)ListViewIndex.AminoAcids, i, "txtAminoValue"))) != null)
                            {
                                ingredientAmino.NutrientValue = CommonFunctions.Convert2Float(((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvAmino, (int)ListViewIndex.AminoAcids, i, "txtAminoValue"))).Text);
                            }
                        }
                    }
                    ingredientAminoUpdateList.Add(ingredientAmino);
                }
                ingredient.IngredientAminoAcidList = ingredientAminoUpdateList;
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Get Nutrient Value List for DB Updation
        /// </summary>
        private void GetNutrientsList()
        {            
            try
            {
                List<IngredientNutrients> ingredientNutriUpdateList = new List<IngredientNutrients>();
                for (int i = 0; i < lvNutrient.Items.Count; i++)
                {
                    ingredientNutri = new IngredientNutrients();
                    ingredientNutri.IngredientId = ingredient.Id;
                    if (Convert.ToString(lvNutrient.Items[i]) == "BONutrition.IngredientNutrients")
                    {
                        ingredientNutri.NutrientID = ((IngredientNutrients)lvNutrient.Items[i]).NutrientID;
                        ingredientNutri.NutrientValue = ((IngredientNutrients)lvNutrient.Items[i]).NutrientValue;
                    }
                    else
                    {
                        if (sysNutrientList.Count > 0)
                        {
                            ingredientNutri.NutrientID = ((NSysNutrient)lvNutrient.Items[i]).NutrientID;
                            if (((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvNutrient, (int)ListViewIndex.Nutrients, i, "txtNutriValue"))) != null)
                            {
                                ingredientNutri.NutrientValue = CommonFunctions.Convert2Float(((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvNutrient, (int)ListViewIndex.Nutrients, i, "txtNutriValue"))).Text);
                            }
                        }
                    }
                    ingredientNutriUpdateList.Add(ingredientNutri);
                }
                ingredient.IngredientNutrientsList = ingredientNutriUpdateList;
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Get Fatty Acid List for DB Updation
        /// </summary>
        private void GetFattyAcidList()
        {
            try
            {
                List<IngredientFattyAcid> ingredientFattyUpdateList = new List<IngredientFattyAcid>();
                for (int i = 0; i < lvFattyAcid.Items.Count; i++)
                {
                    ingredientFatty = new IngredientFattyAcid();
                    ingredientFatty.IngredientId = ingredient.Id;
                    if (Convert.ToString(lvFattyAcid.Items[i]) == "BONutrition.IngredientFattyAcid")
                    {
                        ingredientFatty.NutrientID = ((IngredientFattyAcid)lvFattyAcid.Items[i]).NutrientID;
                        ingredientFatty.NutrientValue = ((IngredientFattyAcid)lvFattyAcid.Items[i]).NutrientValue;
                    }
                    else
                    {
                        if (sysNutrientList.Count > 0)
                        {
                            ingredientFatty.NutrientID = ((NSysNutrient)lvFattyAcid.Items[i]).NutrientID;
                            if (((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvFattyAcid, (int)ListViewIndex.FattyAcids, i, "txtFattyValue"))) != null)
                            {
                                ingredientFatty.NutrientValue = CommonFunctions.Convert2Float(((TextBox)(ListViewHelper.GetElementFromCellTemplate(lvFattyAcid, (int)ListViewIndex.FattyAcids, i, "txtFattyValue"))).Text);
                            }
                        }
                    }              
                    ingredientFattyUpdateList.Add(ingredientFatty);
                }
                ingredient.IngredientFattyAcidList = ingredientFattyUpdateList;
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message);
            }
            finally
            {

            }
        }
      
        /// <summary>
        /// Get Standard Unit List for DB Updation
        /// </summary>
        private void GetStandardUnitList()
        {
            try
            {
                List<IngredientStandardUnit> ingredientUnitUpdateList = new List<IngredientStandardUnit>();
                for (int i = 0; i < lvStandardUnit.Items.Count; i++)
                {                    
                    if (standardUnitList.Count > 0)
                    {
                        if (((StandardUnit)lvStandardUnit.Items[i]).IsApplicable == true)
                        {
                            ingredientStandardUnit = new IngredientStandardUnit();
                            ingredientStandardUnit.IngredientID = ingredient.Id;
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
                AlertBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Validations before DB Updation
        /// </summary>
        /// <returns></returns>
        private bool ValidateIngredient()
        {
            //if (cboIngredientName.Text == string.Empty)
            if (txtIngredientNameENG.Text == string.Empty)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1029"), "", AlertType.Information, AlertButtons.OK);
                //AlertBox.Show(XMLServices.GetXmlMessage("E1029"), "", AlertType.Error, AlertButtons.OK);
                tbAddIngredient.SelectedIndex = 0;
                txtIngredientNameENG.Focus();
                //cboIngredientName.Focus();
                return false;
            }
            else if (txtIngredientNameDisplay.Text == string.Empty)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1276"), "", AlertType.Information, AlertButtons.OK);
                //AlertBox.Show(XMLServices.GetXmlMessage("E1029"), "", AlertType.Error, AlertButtons.OK);
                tbAddIngredient.SelectedIndex = 0;
                txtIngredientNameDisplay.Focus();
                return false;
            }
            else if (cboEthnic.SelectedIndex <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1101"), "", AlertType.Information, AlertButtons.OK);
                //AlertBox.Show(XMLServices.GetXmlMessage("E1030"), "Ethnic", AlertType.Error, AlertButtons.OK);
                tbAddIngredient.SelectedIndex = 0;
                cboEthnic.Focus();
                return false;
            }
            else if (cboIngredientType.SelectedIndex <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1102"), "", AlertType.Information, AlertButtons.OK);
                //AlertBox.Show(XMLServices.GetXmlMessage("E1031"), "Food Type", AlertType.Error, AlertButtons.OK);
                tbAddIngredient.SelectedIndex = 0;
                cboIngredientType.Focus();
                return false;
            }

            //if (txtShelfLife.Text.Trim() != string.Empty)
            //{
            //    if (CommonFunctions.Convert2Int(txtShelfLife.Text) > 365)
            //    {
            //        //AlertBox.Show(XMLServices.GetXmlMessage("E1047"), "Expiry period", AlertType.Error, AlertButtons.OK);
            //        txtShelfLife.Focus();
            //        return false;
            //    }
            //}
            //if (txtRefrigeratedLife.Text.Trim() != string.Empty)
            //{
            //    if (CommonFunctions.Convert2Int(txtRefrigeratedLife.Text) > 365)
            //    {
            //        //AlertBox.Show(XMLServices.GetXmlMessage("E1047"), "Expiry period", AlertType.Error, AlertButtons.OK);
            //        txtRefrigeratedLife.Focus();
            //        return false;
            //    }
            //}
            //if (txtFrozenLife.Text.Trim() != string.Empty)
            //{
            //    if (CommonFunctions.Convert2Int(txtFrozenLife.Text) > 365)
            //    {
            //        //AlertBox.Show(XMLServices.GetXmlMessage("E1047"), "Expiry period", AlertType.Error, AlertButtons.OK);
            //        txtFrozenLife.Focus();
            //        return false;
            //    }
            //}

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
                                AlertBox.Show(XMLServices.GetXmlMessage("E1103") + ((StandardUnit)lvStandardUnit.Items[i]).StandardUnitName, "", AlertType.Information, AlertButtons.OK);
                                tbAddIngredient.SelectedIndex = 3;
                                return false;
                            }
                        }
                    }
                }
            }
                    
            return true;
        }

        /// <summary>
        /// Load Ingredient details in Edit Mode
        /// </summary>
        public void LoadIngredient()
        {            
            ingredientAminoList = new List<IngredientAminoAcid>();
            ingredientFattyList = new List<IngredientFattyAcid>();
            ingredientNutriList = new List<IngredientNutrients>();
            ingredient = IngredientManager.GetItem(ingredientID);
            if (ingredient != null)
            {
                //cboIngredientName.Text = ingredient.Name;
                txtIngredientNameENG.Text = ingredient.Name;

                txtScrapRate.Text = Convert.ToString(ingredient.ScrappageRate);
                txtWeightChange.Text = Convert.ToString(ingredient.WeightChangeRate);
                txtShelfLife.Text = Convert.ToString(ingredient.ShelfLife);
                txtRefrigeratedLife.Text = Convert.ToString(ingredient.RefrigeratedLife);
                txtFrozenLife.Text = Convert.ToString(ingredient.FrozenLife);

                txtDescriptionENG.Text = Convert.ToString(ingredient.Comments);
                chkAllergic.IsChecked = Convert.ToBoolean(ingredient.IsAllergic);

                txtHealthValueGENR.Text = Convert.ToString(ingredient.GeneralHealthValue);
                txtHealthValueAYUR.Text = Convert.ToString(ingredient.AyurHealthValue);
                                
                if (ingredient.DisplayImage != "")
                {
                    string path = GetImagePath("Ingredients") + "\\" + ingredient.Id + ".jpg";
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
                
                isNutrientsLoad = true;
                isPropertiesLoad = true;
                cboIngredientType.SelectedValue = Convert.ToInt16(ingredient.FoodHabitID);
                cboEthnic.SelectedValue = Convert.ToInt16(ingredient.EthnicID);

                LoadIngredientNutrients(ingredient.Id);
                LoadStandardUnits();                

                if (CommonFunctions.Convert2Float(txtScrapRate.Text.Trim()) <= 0)
                    txtScrapRate.Text = "100";

                if (CommonFunctions.Convert2Float(txtWeightChange.Text.Trim()) <= 0)
                    txtWeightChange.Text = "100";
            }
            else
            {
                ClearAllValues();
                IsImageAdd = true;
            }
        }       

        /// <summary>
        /// Load Amino Acid Parameters
        /// </summary>
        private void LoadAmino()
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
        private void LoadFatty()
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
        private void LoadNutrients()
        {            
            sysNutrientList = NSysNutrientManager.GetListNutrient((byte)NutrientGroup.Nutrients);
            if (sysNutrientList != null)
            {
                lvNutrient.ItemsSource = sysNutrientList;
            }
        }       

        /// <summary>
        /// Load Nutrients, Amino Acid & Fatty Acid Values of Ingredient
        /// </summary>
        /// <param name="IngrID"></param>
        private void LoadIngredientNutrients(int IngrID)
        {
            ingredientAminoList = IngredientAminoAcidManager.GetListNutrientValues(IngrID, (byte)NutrientGroup.AminoAcid);
            if (ingredientAminoList != null)
            {
                lvAmino.ItemsSource = ingredientAminoList;
            }

            ingredientFattyList = IngredientFattyAcidManager.GetListNutrientValues(IngrID, (byte)NutrientGroup.FattyAcid);
            if (ingredientFattyList != null)
            {
                lvFattyAcid.ItemsSource = ingredientFattyList;
            }

            ingredientNutriList = IngredientNutrientsManager.GetListNutrientValues(IngrID, (byte)NutrientGroup.Nutrients);
            if (ingredientNutriList != null)
            {
                lvNutrient.ItemsSource = ingredientNutriList;
            }
        }

        /// <summary>
        /// Load the Standard Units of the Ingredient
        /// </summary>
        private void LoadStandardUnits()
        {
            standardUnitList = StandardUnitManager.GetList(ingredientID);
            if (standardUnitList != null)
            {
                lvStandardUnit.ItemsSource = standardUnitList;
            }
        }

        /// <summary>
        /// Delete the selected Ingredient
        /// </summary>
        private void DeleteIngredient()
        {
            if (ingredientID > 0)
            {
                string ingredientName = string.Empty;
                if (ingredient != null)
                {
                    ingredientName = ingredient.Name;
                    if (IngredientManager.GetIngredientCount(ingredient) <= 0)
                    {
                        bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1104") + ingredientName, "", AlertType.Exclamation, AlertButtons.YESNO);
                        if (result == true)
                        {
                            if (IngredientManager.DeleteIngredient(ingredient))
                            {
                                ClearAllValues();
                                AlertBox.Show(ingredientName + XMLServices.GetXmlMessage("E1164"), "", AlertType.Information, AlertButtons.OK);
                            }
                            else
                            {
                                AlertBox.Show(XMLServices.GetXmlMessage("E1105") + ingredientName, "", AlertType.Error, AlertButtons.OK);
                            }
                        }
                    }
                    else
                    {
                        AlertBox.Show(ingredientName + XMLServices.GetXmlMessage("E1166"), "", AlertType.Error, AlertButtons.OK);
                    }
                }
            }
        }
       
        /// <summary>
        /// Set the templates as entry mode
        /// </summary>
        private void CreateMode()
        {
            ControlsDefaultStyle();
            imgStar1.Visibility = Visibility.Visible;
            imgStar2.Visibility = Visibility.Visible;
            imgStar3.Visibility = Visibility.Visible;
            imgStar4.Visibility = Visibility.Visible;
            imgStar5.Visibility = Visibility.Visible;
            imgStarMain.Visibility = Visibility.Visible;
            lblMandatory.Visibility = Visibility.Visible;

            chkAllergic.IsHitTestVisible = true;

            btnAddImage.Visibility = Visibility.Visible;
            btnAddImage.Content = uploadCaption;
            btnSave.Visibility = Visibility.Visible;

            gvNutrientColNameValue.CellTemplate = this.FindResource("nutrientvalueTemplate") as DataTemplate;
            gvFattyAcidColNameValue.CellTemplate = this.FindResource("fattyvalueTemplate") as DataTemplate;
            gvAminoAcidColNameValue.CellTemplate = this.FindResource("aminovalueTemplate") as DataTemplate;
            
            gvQuantityCol.CellTemplate = this.FindResource("quantityTemplate") as DataTemplate;
            gvUnitNameCol.CellTemplate = this.FindResource("unitNameTemplate") as DataTemplate;
        }
        
        /// <summary>
        /// Set the controls in entry mode
        /// </summary>
        private void ControlsDefaultStyle()
        {
            App apps = (App)Application.Current;

            cboEthnic.Visibility = Visibility.Visible;
            cboIngredientType.Visibility = Visibility.Visible;

            txtIngredientNameENG.Style = (Style)apps.SetStyle["TextStyle"];
            txtIngredientNameDisplay.Style = (Style)apps.SetStyle["TextStyle"];
            txtIngredientNameREG.Style = (Style)apps.SetStyle["TextStyle"];
            txtScrapRate.Style = (Style)apps.SetStyle["TextStyle"];
            txtWeightChange.Style = (Style)apps.SetStyle["TextStyle"];
            txtShelfLife.Style = (Style)apps.SetStyle["TextStyle"];
            txtRefrigeratedLife.Style = (Style)apps.SetStyle["TextStyle"];
            txtFrozenLife.Style = (Style)apps.SetStyle["TextStyle"];
            txtDescriptionENG.Style = (Style)apps.SetStyle["TextStyle"];
            txtDescriptionREG.Style = (Style)apps.SetStyle["TextStyle"];
            txtHealthValueGENR.Style = (Style)apps.SetStyle["TextStyle"];
            txtHealthValueAYUR.Style = (Style)apps.SetStyle["TextStyle"];

            //cboIngredientName.Style = (Style)apps.SetStyle["ComboStyle"];
            cboIngredientType.Style = (Style)apps.SetStyle["ComboStyle"];
            cboEthnic.Style = (Style)apps.SetStyle["ComboStyle"];
        }

        /// <summary>
        /// Display the details of Individual tab On Click
        /// </summary>
        private void TabDisplay()
        {
            switch (tbAddIngredient.SelectedIndex)
            {
                case 1:
                    if (isNutrientsLoad == false)
                    {
                        LoadIngredientNutrients(ingredientID);
                        isNutrientsLoad = true;
                    }
                    break;                
                case 2:
                    if (isPropertiesLoad == false)
                    {
                        LoadStandardUnits();
                        isPropertiesLoad = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Search for specific Nutrient Value
        /// </summary>
        /// <param name="lvList"></param>
        /// <param name="strSearch"></param>
        /// <param name="nutrientGroup"></param>
        private void SearchNutrients(ListView lvList, string strSearch, int nutrientGroup)
        {
            lvList.SelectedIndex = -1;
            if (nutrientGroup == (int)NutrientGroup.Nutrients)
            {
                for (int i = 0; i < lvList.Items.Count; i++)
                {
                    if (Convert.ToString(lvList.Items[i]) == "BONutrition.IngredientNutrients")
                    {
                        if (((IngredientNutrients)lvList.Items[i]).NutrientParam.ToUpper() == strSearch.ToUpper())
                        {
                            lvList.SelectedIndex = i;
                            lvList.ScrollIntoView(lvList.SelectedItem);
                            break;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(lvList.Items[i]) == "BONutrition.NSysNutrient")
                        {
                            if (((NSysNutrient)lvList.Items[i]).NutrientParam.ToUpper() == strSearch.ToUpper())
                            {
                                lvList.SelectedIndex = i;
                                lvList.ScrollIntoView(lvList.SelectedItem);
                                break;
                            }
                        }
                    }
                }
            }
            else if (nutrientGroup == (int)NutrientGroup.AminoAcid)
            {
                for (int i = 0; i < lvList.Items.Count; i++)
                {
                    if (Convert.ToString(lvList.Items[i]) == "BONutrition.IngredientAminoAcid")
                    {
                        if (((IngredientAminoAcid)lvList.Items[i]).NutrientParam.ToUpper() == strSearch.ToUpper())
                        {
                            lvList.SelectedIndex = i;
                            lvList.ScrollIntoView(lvList.SelectedItem);
                            break;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(lvList.Items[i]) == "BONutrition.NSysNutrient")
                        {
                            if (((NSysNutrient)lvList.Items[i]).NutrientParam.ToUpper() == strSearch.ToUpper())
                            {
                                lvList.SelectedIndex = i;
                                lvList.ScrollIntoView(lvList.SelectedItem);
                                break;
                            }
                        }
                    }
                }
            }
            else if (nutrientGroup == (int)NutrientGroup.FattyAcid)
            {
                for (int i = 0; i < lvList.Items.Count; i++)
                {
                    if (Convert.ToString(lvList.Items[i]) == "BONutrition.IngredientFattyAcid")
                    {
                        if (((IngredientFattyAcid)lvList.Items[i]).NutrientParam.ToUpper() == strSearch.ToUpper())
                        {
                            lvList.SelectedIndex = i;
                            lvList.ScrollIntoView(lvList.SelectedItem);
                            break;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(lvList.Items[i]) == "BONutrition.NSysNutrient")
                        {
                            if (((NSysNutrient)lvList.Items[i]).NutrientParam.ToUpper() == strSearch.ToUpper())
                            {
                                lvList.SelectedIndex = i;
                                lvList.ScrollIntoView(lvList.SelectedItem);
                                break;
                            }
                        }
                    }
                }
            }
        }        
       
        /// <summary>
        /// Browse the ingredient image
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
        /// Get the destination path of Ingredient image
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
        /// Copy Ingredient Image from source to destination
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        /// <returns></returns>
		private string CopyFile(string sourceFile, string destFile)
		{
			try
			{                
                //if (File.Exists(destFile))
                //    File.Delete(destFile);
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
                AlertBox.Show(ex.Message);
				return "";
			}
		}
        
        #endregion               
        
    }
}
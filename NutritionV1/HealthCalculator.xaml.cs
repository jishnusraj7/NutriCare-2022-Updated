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
using System.Resources;
using System.Transactions;
using System.Collections;
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Common.Classes;
using NutritionV1.Enums;
using NutritionV1.Constants;
using Visifire.Charts;
using Visifire.Commons;
using NutritionV1.Classes;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Animation;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for HealthCalculator.xaml
    /// </summary>
    public partial class HealthCalculator : Page
    {

        #region Declarations

        SaveFileDialog SaveDlg = new SaveFileDialog();

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

        #region Constructor

        public HealthCalculator()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        private void SetTheme()
        {
            App apps = (App)Application.Current;

            //BodyMassIndex
            lblWeight1.Style = (Style)apps.SetStyle["LabelStyle"];
            lblHeight1.Style = (Style)apps.SetStyle["LabelStyle"];
            lbBMI.Style = (Style)apps.SetStyle["LabelStyle"];
            txtBMIComments.Style = (Style)apps.SetStyle["TextBlockStyle"];

            //Calorie Needs
            lblCalorieNeeds2.Style = (Style)apps.SetStyle["LabelStyle"];
            lblSex2.Style = (Style)apps.SetStyle["LabelStyle"];
            lblAge2.Style = (Style)apps.SetStyle["LabelStyle"];
            lblHeight2.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWeight2.Style = (Style)apps.SetStyle["LabelStyle"];
            lblLifeStyle2.Style = (Style)apps.SetStyle["LabelStyle"];
            lblCalorie.Style = (Style)apps.SetStyle["LabelStyle"];

            //WaistHips Ratio
            lblWaistHipsRatio3.Style = (Style)apps.SetStyle["LabelStyle"];
            lblHips3.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWaist3.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWHRatio.Style = (Style)apps.SetStyle["LabelStyle"];

            //Ideal Weight
            lblSex5.Style = (Style)apps.SetStyle["LabelStyle"];
            lblHeight5.Style = (Style)apps.SetStyle["LabelStyle"];
            lblFewFacts5.Style = (Style)apps.SetStyle["LabelStyle"];
            lblIdealWeight.Style = (Style)apps.SetStyle["LabelStyle"];
            lblMinHealthyWeight.Style = (Style)apps.SetStyle["LabelStyle"];
            lblMaxHealthyWeight.Style = (Style)apps.SetStyle["LabelStyle"];

            //BodyFat
            lblSex7.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWeight7.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWaist7.Style = (Style)apps.SetStyle["LabelStyle"];
            lblHips7.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWrist7.Style = (Style)apps.SetStyle["LabelStyle"];
            lblForeArm7.Style = (Style)apps.SetStyle["LabelStyle"];
            lblBFRatio.Style = (Style)apps.SetStyle["LabelStyle"];

            ((NutritionV1.MasterPage)(Window.GetWindow(this))).mnuTop.Visibility = Visibility.Visible;
        }

        private void SetCulture()
        {
            App apps = (App)Application.Current;
            ResourceManager rm = apps.getLanguageList;

            tbBodyMassIndex.Header = "Body Mass Index";
            lblWeight1.Content = "Weight (Kg)";
            lblHeight1.Content = "Height (Cm)";
            btnCalculate1.Content = "Calculate";
            lbBMI.Content = "Your Body Mass Index is :";
            tbCalorieNeeds.Header = "Calorie Needs";

            lblSex2.Content = "Sex";
            lblAge2.Content = "Age";
            lblHeight2.Content = "Height (Cm)";
            lblWeight2.Content = "Weight (Kg)";
            lblLifeStyle2.Content = "Lifestyle";
            btnCalculate2.Content = "Calculate";
            lblCalorie.Content = "Required Calorie per Day :";

            tbWaistHipsRatio.Header = "Waist-Hip Ratio";
            lblHips3.Content = "Hips (Cm)";
            lblWaist3.Content = "Waist (Cm)";
            btnCalculate3.Content = "Calculate";
            lblWHRatio.Content = "Your Waist-Hip ratio is";

            tbIdealWeight.Header = "Ideal Weight";
            lblSex5.Content = "Sex";
            lblHeight5.Content = "Height (Cm)";
            btnCalculate5.Content = "Calculate";

        }

        public void SetMaxLength()
        {
            txtHeight1.MaxLength = 3;
            txtWeight1.MaxLength = 6;

            txtAge2.MaxLength = 3;
            txtHeight2.MaxLength = 3;
            txtWeight2.MaxLength = 6;

            txtWaist3.MaxLength = 3;
            txtHips3.MaxLength = 3;

            txtHeight5.MaxLength = 3;

            txtWeight7.MaxLength = 6;
            txtWaist7.MaxLength = 3;
            txtHips7.MaxLength = 3;
            txtWrist7.MaxLength = 3;
            txtForeArm7.MaxLength = 3;
        }

        private void Initialize()
        {
            try
            {
                dtUnit = XMLServices.GetXMLData(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\ServeUnit.xml", 3);
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

        public void FillTextBlocks()
        {
            App apps = (App)Application.Current;
            ResourceManager rm = apps.getLanguageList;

            //txtBodyMassIndex.Text = "The Body Mass Index (BMI) calculator measures your weight relative to your height and provides a reasonable estimate of your total body fat. " +
            //                        "BMI is helpful in determining when excess mass translates to excess health risk.";

            //txtCalorieNeeds.Text = "Our daily caloric needs are determined by our activity  levels and the amount of energy we expend.The more active you are,  " +
            //                       "the more calories you need. ";

            txtBodyMassIndex.Text = "The Body Mass Index (BMI) calculator measures your weight relative to your height and provides a reasonable estimate of your total body fat. " + System.Environment.NewLine +
                                    "BMI is helpful in determining when excess mass translates to excess health risk.";

            txtCalorieNeeds.Text = "Our daily calorie needs are determined by our activity levels and the amount of energy we expend. The more active you are, the more calorie you need.";

            txtWaistHipsRatio.Text = "Your waist to hip ratio can be used to predict your risk towards obesity-related diseases. By dividing your waist measurement with " + System.Environment.NewLine +
                                     "your hip measurement, you can find out the distribution of fat in your body and better prepare for correlated health risks. ";

            txtIdealWeight.Text = "Use this tool to determine your healthiest weight. The ideal weight calculator can help you determine whether you should consider a diet or not.";

            txtBodyFatRatio.Text = "Your body fat percentage is simply the percentage of fat your body contains. If you weigh 140 pounds and are 10% fat," + System.Environment.NewLine +
                                    " it means that your body consists of 14 pounds fat and 126 pounds lean body mass (bone, muscle, organ tissue, blood etc.).";

        }

        private void FillComments()
        {
            //txtBMIComments.Text = "* Under 18.5 Under Weight." + System.Environment.NewLine + System.Environment.NewLine +
            //"* 18.5-24.9 Normal." + System.Environment.NewLine + System.Environment.NewLine +
            //"* 25-29.9 Over Weight. " + System.Environment.NewLine + System.Environment.NewLine +
            //"* 25-29.9 Over Weight. " + System.Environment.NewLine + System.Environment.NewLine +
            //"* 30+ Obese.";

            txtBMIComments.Text = "                   BMI Categories" + "\n\n" +
                                  "     < 18.5           Underweight" + "\n\n" +
                                  "     18.5-22.9       Normal" + "\n\n" +
                                  "     23.0-27.5       Overweight" + "\n\n" +
                                  "     27.6 >              Obese";

            txtBMICommentsOther.Text = System.Environment.NewLine + "NOTICE : " + System.Environment.NewLine + "● BMI caculation may over estimate the body fat for muscular build and athletics." + System.Environment.NewLine +
                                       "● It may under estimate the body fat for older persons and those who have lost muscular mass." + System.Environment.NewLine +
                                       "● It should not be used for those who are < 19 and > 70 years old." + System.Environment.NewLine +
                                       "● It should not be used for pregnant women.";

            txtWaistHipsComments.Text = "                   Waist-Hip Ratio Categories" + "\n\n" +
                                        "         Excellent      Good           Average       High" + "\n\n" +
                                        "MALE    < 0.85        0.85-0.90    0.90-1        > 1 " + "\n\n" +
                                        "FEMALE < 0.75       0.75-0.80    0.80-0.85   > 0.85 " + "\n\n\n" +
                                        "● A waist to hip ratio more than 1.0 for men & more than 0.85" + "\n" +
                                        "   in women denotes risk for development of diabetes.";

            txtIdealWeightComments.Text = "● Body-growth in men occurs till the age of 25." + "\n" +
                                          "   After which the weight normally remains constant. " + System.Environment.NewLine + System.Environment.NewLine +
                                          "● 60% of body weight is water among males and 50% among females.";

            //txtCalorieNeedsComments.Text = "* The total calories consumed each day by men varies from 1500-4000.       " + System.Environment.NewLine + System.Environment.NewLine +                              
            //"* Total for females varies from 900-2500.";

            txtCalorieNeedsComments.Text = "● The Calorie requirement for Men ranges from 1900-4000. " + System.Environment.NewLine + System.Environment.NewLine +
                                         "● The Calorie requirement for Women ranges from 1500-3000. " + System.Environment.NewLine + System.Environment.NewLine +
                                         "● The Calorie requirement lowers after 60+. " + System.Environment.NewLine + System.Environment.NewLine +
                                         "● During Pregnancy and Lactation an additional 300 cal and 550 cal respectively are recommemded. " + System.Environment.NewLine + System.Environment.NewLine +
                                         "● In some disease conditions energy requirements increases or decreases. ";

            txtBodyFatComments.Text = "                   Body Fat% Categories" + "\n\n" +
                                        "Classifications    Women (% fat)          Men (% fat)" + "\n\n" +
                                        "Essential Fat          10-13%                   2-5%" + "\n\n" +
                                        "Athletes                14-20%                   6-13%" + "\n\n" +
                                        "Fitness                  21-24%                  14-17%" + "\n\n" +
                                        "Acceptable             25-31%                  18-24%" + "\n\n" +
                                        "Obese                    32%+                    25%+";
        }

        private void FillSex(ComboBox cb)
        {
            try
            {
                List<NSysAdmin> adminList = new List<NSysAdmin>();
                adminList = NSysAdminManager.GetSex();
                if (adminList != null)
                {
                    NSysAdmin admin = new NSysAdmin();
                    admin.SexName = "---Select---";
                    admin.SexID = 0;
                    adminList.Insert(0, admin);
                    cb.DisplayMemberPath = "SexName";
                    cb.SelectedValuePath = "SexID";
                    cb.ItemsSource = adminList;
                }
                cb.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void FillLifeStyle(ComboBox cb)
        {
            try
            {
                List<NSysAdmin> adminList = new List<NSysAdmin>();
                adminList = NSysAdminManager.GetLifeStyle();
                if (adminList != null)
                {
                    NSysAdmin admin = new NSysAdmin();
                    admin.LifeStyleName = "---Select---";
                    admin.LifeStyleID = 0;
                    adminList.Insert(0, admin);
                    cb.DisplayMemberPath = "LifeStyleName";
                    cb.SelectedValuePath = "LifeStyleID";
                    cb.ItemsSource = adminList;
                }
                cb.SelectedIndex = 0;
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

        private bool GetDishDetails(int GridID, int DishID)
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
                        GetDishDetails(1, Classes.CommonFunctions.Convert2Int(Convert.ToString(searchDishID[i])));
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
                if (excelOut.CreateExcel("CalorieCalculator", filePath))
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

        //Body Mass Index
        public double CalculateBMI()
        {
            double WeightinPounds, HeightinInches, BMI = 0;

            try
            {
                if (ValidateBMI() == true)
                {
                    WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.Convert2Int(txtWeight1.Text) * CalorieFormula.KG_POUND));
                    HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.Convert2Int(txtHeight1.Text) * CalorieFormula.CM_INCH));

                    BMI = Math.Round((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) / (Math.Pow((HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)), 2);
                }
                return BMI;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return BMI;
            }
            finally
            {

            }
        }

        private bool ValidateBMI()
        {
            if (txtHeight1.Text == string.Empty)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1010"), "", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1112"), "", AlertType.Information, AlertButtons.OK);
                txtHeight1.Focus();
                return false;
            }
            else if (Classes.CommonFunctions.Convert2Int(txtHeight1.Text) < 50 || Classes.CommonFunctions.Convert2Int(txtHeight1.Text) > 200)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1038"), "", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1113"), "", AlertType.Information, AlertButtons.OK);
                txtHeight1.Focus();
                return false;
            }
            else if (txtWeight1.Text == string.Empty)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1011"), "", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1114"), "", AlertType.Information, AlertButtons.OK);
                txtWeight1.Focus();
                return false;
            }
            else if (Classes.CommonFunctions.Convert2Int(txtWeight1.Text) < 1 || Classes.CommonFunctions.Convert2Int(txtWeight1.Text) > 200)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1039"), "", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1115"), "", AlertType.Information, AlertButtons.OK);
                txtWeight1.Focus();
                return false;
            }
            return true;
        }

        //Calorie Needs
        public int CalculateCalorieNeeds()
        {
            int SexID, Age, LifeStyleID;
            double HeightinInches, WeightinPounds, LifeStyleValue = 0, CalorieNeeds = 0;

            try
            {
                if (ValidateCalorieNeeds() == true)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbSex2.SelectedIndex));
                    Age = Classes.CommonFunctions.Convert2Int(Convert.ToString(txtAge2.Text));

                    HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.Convert2Int(txtHeight2.Text) * CalorieFormula.CM_INCH));
                    WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.Convert2Int(txtWeight2.Text) * CalorieFormula.KG_POUND));
                    LifeStyleID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbLifeStyle2.SelectedIndex));

                    switch (LifeStyleID)
                    {
                        case (int)LifeStyleType.Sedentary:
                            LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(NutritionV1.Constants.LifeStyle.SEDENTARY));
                            break;
                        case (int)LifeStyleType.LightlyActive:
                            LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(NutritionV1.Constants.LifeStyle.LIGHTLYACTIVE));
                            break;
                        case (int)LifeStyleType.ModeratelyActive:
                            LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(NutritionV1.Constants.LifeStyle.MODERATELYACTIVE));
                            break;
                        case (int)LifeStyleType.VeryActive:
                            LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(NutritionV1.Constants.LifeStyle.VERYACTIVE));
                            break;
                        case (int)LifeStyleType.ExtraActive:
                            LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(NutritionV1.Constants.LifeStyle.EXTRAACTIVE));
                            break;
                    }

                    if (SexID == 1)
                    {
                        CalorieNeeds = Math.Round((((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEWEIGHT_MEN))) + (HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEHEIGHT_MEN))) + Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIECOMMON_MEN))) - Age * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEAGE_MEN))) * LifeStyleValue, 0);
                    }
                    else if (SexID == 2)
                    {
                        CalorieNeeds = Math.Round((((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEWEIGHT_WOMEN))) + (HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEHEIGHT_WOMEN))) + Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIECOMMON_WOMEN))) - Age * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEAGE_WOMEN))) * LifeStyleValue, 0);
                    }
                }
                return (int)CalorieNeeds;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return (int)CalorieNeeds;
            }
            finally
            {

            }
        }

        private bool ValidateCalorieNeeds()
        {
            if (cbSex2.SelectedIndex <= 0)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1008"), "Calorie needs", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1116"), "", AlertType.Information, AlertButtons.OK);
                cbSex2.Focus();
                return false;
            }
            else if (txtAge2.Text == string.Empty)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1009"), "Calorie needs", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1117"), "", AlertType.Information, AlertButtons.OK);
                txtAge2.Focus();
                return false;
            }
            else if (Classes.CommonFunctions.Convert2Int(txtAge2.Text) < 1 || Classes.CommonFunctions.Convert2Int(txtAge2.Text) > 120)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1037"), "Calorie needs", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1118"), "", AlertType.Information, AlertButtons.OK);
                txtAge2.Focus();
                return false;
            }
            else if (txtHeight2.Text == string.Empty)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1010"), "BMI", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1119"), "", AlertType.Information, AlertButtons.OK);
                txtHeight2.Focus();
                return false;
            }
            else if (Classes.CommonFunctions.Convert2Int(txtHeight2.Text) < 50 || Classes.CommonFunctions.Convert2Int(txtHeight2.Text) > 200)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1038"), "BMI", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1120"), "", AlertType.Information, AlertButtons.OK);
                txtHeight2.Focus();
                return false;
            }
            else if (txtWeight2.Text == string.Empty)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1011"), "BMI", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1121"), "", AlertType.Information, AlertButtons.OK);
                txtWeight2.Focus();
                return false;
            }
            else if (Classes.CommonFunctions.Convert2Int(txtWeight2.Text) < 1 || Classes.CommonFunctions.Convert2Int(txtWeight2.Text) > 200)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1039"), "BMI", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1122"), "", AlertType.Information, AlertButtons.OK);
                txtWeight2.Focus();
                return false;
            }
            if (cbLifeStyle2.SelectedIndex <= 0)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1012"), "BMI", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1123"), "", AlertType.Information, AlertButtons.OK);
                cbLifeStyle2.Focus();
                return false;
            }
            return true;
        }

        //Waist-Hips Ratio
        public double CalculateWaistHipsRatio()
        {
            double Ratio = 0;
            double dWaist, dHips;

            try
            {
                if (ValidateWaistHipsRatio() == true)
                {
                    dWaist = Classes.CommonFunctions.Convert2Double(txtWaist3.Text.Trim());
                    dHips = Classes.CommonFunctions.Convert2Double(txtHips3.Text.Trim());
                    if (dHips != 0)
                    {
                        Ratio = dWaist / dHips;
                        Ratio = Math.Round(dWaist / dHips, 2);
                    }
                }
                return Ratio;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return Ratio;
            }
            finally
            {

            }
        }

        private bool ValidateWaistHipsRatio()
        {
            if (txtWaist3.Text == string.Empty || Classes.CommonFunctions.Convert2Int(txtWaist3.Text) == 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1124"), "", AlertType.Information, AlertButtons.OK);
                txtWaist3.Focus();
                return false;
            }
            else if (txtHips3.Text == string.Empty || Classes.CommonFunctions.Convert2Int(txtHips3.Text) == 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1125"), "", AlertType.Information, AlertButtons.OK);
                txtHips3.Focus();
                return false;
            }
            return true;
        }

        //Ideal Weight
        public int CalculateIdealWeight()
        {
            int SexID, Height;
            double IdealWeight = 0;

            try
            {
                if (ValidateIdealWeight() == true)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbSex5.SelectedIndex));
                    Height = Classes.CommonFunctions.Convert2Int(Convert.ToString(txtHeight5.Text.Trim()));

                    if (SexID == 1)
                    {
                        IdealWeight = Math.Round(((Height - 100) - ((Height - 100) * 0.1)), 0);
                    }
                    else if (SexID == 2)
                    {
                        IdealWeight = Math.Round(((Height - 100) - ((Height - 100) * 0.15)), 0);
                    }
                }
                return (int)IdealWeight;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return (int)IdealWeight;
            }
            finally
            {

            }
        }

        public double[] CalculateIdealWeightRange()
        {
            int SexID;
            double Height = 0, HeightinInches = 0;
            double IdelaWeightLower = 0, IdealWeightUpper = 0;
            double[] IdealWeight = new double[2];

            try
            {
                SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbSex5.SelectedIndex));
                Height = Classes.CommonFunctions.Convert2Double(Convert.ToString(txtHeight5.Text.Trim()));

                if (ValidateIdealWeight() == true)
                {
                    if (Height != 0)
                    {
                        HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(Height * CalorieFormula.CM_INCH));

                        IdelaWeightLower = Math.Round(BodyMassIndex.BMINORMAL_RangeLOW * Math.Pow(Classes.CommonFunctions.Convert2Double(Convert.ToString(Height / (int)PercentageType.Percentage_Normal)), 2), 0);

                        IdealWeightUpper = Math.Round(BodyMassIndex.BMINORMAL_RangeHIGH * Math.Pow(Classes.CommonFunctions.Convert2Double(Convert.ToString(Height / (int)PercentageType.Percentage_Normal)), 2), 0);

                        IdealWeight[0] = Classes.CommonFunctions.Convert2Float(Convert.ToString(IdelaWeightLower));
                        IdealWeight[1] = Classes.CommonFunctions.Convert2Float(Convert.ToString(IdealWeightUpper));

                        return IdealWeight;
                    }
                }

                return IdealWeight;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return IdealWeight;
            }
            finally
            {

            }
        }

        private bool ValidateIdealWeight()
        {
            if (cbSex5.SelectedIndex <= 0)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1008"), "Food Type", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1126"), "", AlertType.Information, AlertButtons.OK);
                cbSex5.Focus();
                return false;
            }
            else if (txtHeight5.Text == string.Empty)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1010"), "Food Type", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1127"), "", AlertType.Information, AlertButtons.OK);
                txtHeight5.Focus();
                return false;
            }
            else if (Classes.CommonFunctions.Convert2Int(txtHeight5.Text) < 120 || Classes.CommonFunctions.Convert2Int(txtHeight5.Text) > 200)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1038"), "Food Type", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1128"), "", AlertType.Information, AlertButtons.OK);
                txtHeight5.Focus();
                return false;
            }
            return true;

        }

        //BodyFat Ratio
        public double CalculateBodyFatRatio()
        {
            int SexID;
            double Ratio = 0;
            double dWeight, dWaist, dHips, dWrist, dForeArm;
            double WeightinPounds, WaistinInches, HipsinInches, WristinInches, ForeArminInches;

            try
            {
                if (ValidateBodyFatRatio() == true)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbSex7.SelectedIndex));
                    dWeight = Classes.CommonFunctions.Convert2Double(txtWeight7.Text.Trim());
                    dWaist = Classes.CommonFunctions.Convert2Double(txtWaist7.Text.Trim());
                    dHips = Classes.CommonFunctions.Convert2Double(txtHips7.Text.Trim());
                    dWrist = Classes.CommonFunctions.Convert2Double(txtWrist7.Text.Trim());
                    dForeArm = Classes.CommonFunctions.Convert2Double(txtForeArm7.Text.Trim());

                    WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString(dWeight * CalorieFormula.KG_POUND));
                    WaistinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(dWaist * CalorieFormula.CM_INCH));
                    HipsinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(dHips * CalorieFormula.CM_INCH));
                    WristinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(dWrist * CalorieFormula.CM_INCH));
                    ForeArminInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(dForeArm * CalorieFormula.CM_INCH));

                    if (SexID == 1)
                    {
                        Ratio = Math.Round(((WeightinPounds - (((WeightinPounds * 1.082) + 94.42) - (WaistinInches * 4.15))) * 100) / WeightinPounds, 0);
                    }
                    else if (SexID == 2)
                    {
                        Ratio = Math.Round(((WeightinPounds - (((WeightinPounds * 0.732) + 8.987) + (WristinInches / 3.14) + (WaistinInches * 0.157) + (HipsinInches * 0.249) + (ForeArminInches * 0.434))) * 100) / WeightinPounds, 0);
                    }
                }
                return Ratio;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return Ratio;
            }
            finally
            {

            }
        }

        private bool ValidateBodyFatRatio()
        {
            if (cbSex7.SelectedIndex <= 0)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1008"), "BodyFat Ratio", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1129"), "", AlertType.Information, AlertButtons.OK);
                cbSex7.Focus();
                return false;
            }
            else if (txtWeight7.Text == string.Empty)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1011"), "BMI", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1130"), "", AlertType.Information, AlertButtons.OK);
                txtWeight7.Focus();
                return false;
            }
            else if (Classes.CommonFunctions.Convert2Int(txtWeight7.Text) < 1 || Classes.CommonFunctions.Convert2Int(txtWeight7.Text) > 200)
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1039"), "BMI", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1131"), "", AlertType.Information, AlertButtons.OK);
                txtWeight7.Focus();
                return false;
            }
            else if (txtWaist7.Text == string.Empty || Classes.CommonFunctions.Convert2Int(txtWaist7.Text) == 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1132"), "", AlertType.Information, AlertButtons.OK);
                txtWaist7.Focus();
                return false;
            }

            if (cbSex7.SelectedIndex == 2)
            {

                if (txtHips7.Text == string.Empty || Classes.CommonFunctions.Convert2Int(txtHips7.Text) == 0)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1133"), "", AlertType.Information, AlertButtons.OK);
                    txtHips7.Focus();
                    return false;
                }
                else if (txtWrist7.Text == string.Empty || Classes.CommonFunctions.Convert2Int(txtWrist7.Text) == 0)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1134"), "", AlertType.Information, AlertButtons.OK);
                    txtWrist7.Focus();
                    return false;
                }
                else if (txtForeArm7.Text == string.Empty || Classes.CommonFunctions.Convert2Int(txtForeArm7.Text) == 0)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1135"), "", AlertType.Information, AlertButtons.OK);
                    txtForeArm7.Focus();
                    return false;
                }
            }

            return true;
        }

        //Minimum,Maximum Weight
        public double[] CalculateMinMaxWeight()
        {
            int SexID;
            double Height = 0, HeightinInches = 0;
            double MaximumWeight = 0, MinimumWeight = 0;
            double[] MimMaxWeight = new double[2];

            try
            {
                SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbSex5.SelectedIndex));
                Height = Classes.CommonFunctions.Convert2Double(Convert.ToString(txtHeight5.Text.Trim()));

                if (Height != 0)
                {
                    HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(Height * CalorieFormula.CM_INCH));

                    //MinimumWeight = Math.Round(((((Math.Pow((HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)) / Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) * 20) * CalorieFormula.POUND_KG), 0);
                    MinimumWeight = Math.Round(BodyMassIndex.BMINORMAL_LOW * Math.Pow(Classes.CommonFunctions.Convert2Double(Convert.ToString(Height / (int)PercentageType.Percentage_Normal)), 2), 0);

                    //MaximumWeight = Math.Round(((((Math.Pow((HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)) / Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) * 25) * CalorieFormula.POUND_KG), 0);
                    MaximumWeight = Math.Round(BodyMassIndex.BMINORMAL_HIGH2 * Math.Pow(Classes.CommonFunctions.Convert2Double(Convert.ToString(Height / (int)PercentageType.Percentage_Normal)), 2), 0);

                    MimMaxWeight[0] = MinimumWeight;
                    MimMaxWeight[1] = MaximumWeight;

                    return MimMaxWeight;
                }

                return MimMaxWeight;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return MimMaxWeight;
            }
            finally
            {

            }
        }

        public void FillTipsBMI(double BMI)
        {
            try
            {
                List<BONutrition.NSysAdmin> calorieCalculator = new List<BONutrition.NSysAdmin>();
                calorieCalculator = NSysAdminManager.GetBMIImpact();

                if (calorieCalculator.Count > 0)
                {
                    if (BMI > 0)
                    {
                        if (BMI <= BodyMassIndex.BMIUNDERWEIGHT)
                        {
                            txtBMITips.Text = "YOUR HEALTH STATUS : " + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(calorieCalculator[0].BMIIMpactDescription) + System.Environment.NewLine + System.Environment.NewLine + "WHAT CAN YOU DO : " + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(calorieCalculator[0].BMIIMpactComments);
                        }
                        else if (BMI > BodyMassIndex.BMINORMAL_LOW && BMI <= BodyMassIndex.BMINORMAL_HIGH2)
                        {
                            txtBMITips.Text = "YOUR HEALTH STATUS : " + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(calorieCalculator[1].BMIIMpactDescription) + System.Environment.NewLine + System.Environment.NewLine + "WHAT CAN YOU DO : " + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(calorieCalculator[1].BMIIMpactComments);
                        }
                        else if (BMI >= BodyMassIndex.BMIOVERWEIGHT_LOW2 && BMI <= BodyMassIndex.BMIOVERWEIGHT_HIGH2)
                        {
                            txtBMITips.Text = "YOUR HEALTH STATUS : " + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(calorieCalculator[2].BMIIMpactDescription) + System.Environment.NewLine + System.Environment.NewLine + "WHAT CAN YOU DO : " + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(calorieCalculator[2].BMIIMpactComments);
                        }
                        else if (BMI >= BodyMassIndex.BMIOVEROBESE2)
                        {
                            txtBMITips.Text = "YOUR HEALTH STATUS : " + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(calorieCalculator[3].BMIIMpactDescription) + System.Environment.NewLine + System.Environment.NewLine + "WHAT CAN YOU DO : " + System.Environment.NewLine + System.Environment.NewLine + Convert.ToString(calorieCalculator[3].BMIIMpactComments);
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

        public void FillTipsCalorieNeeds()
        {
            try
            {
                txtCalorieNeedsTips.Text = "CALORIE ESTIMATION - " + System.Environment.NewLine + System.Environment.NewLine +
                                           "1. Based on your current Weight, Height and Activity level your Daily Calorie Requirement is mentioned above. This is how you need to maintain your weight." + System.Environment.NewLine +
                                           "2. To Gain or Lose Weight, you need to adjust your Calorie upward or downward from this amount." + System.Environment.NewLine +
                                           "3. To Change your Weight 1/2 Kg or 1lbs in 1 week, you must increase or reduce your net Calorie intake by 500 Calorie per day." + System.Environment.NewLine +
                                           "Atleast a minimum amount of 1200 Calories of nutritious food should be consumed a day. Fewer than that would lower iron level.";
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillTipsWaistHips()
        {
            try
            {
                txtWaistHipsTips.Text = "WHERE TO MEASURE - " + System.Environment.NewLine + System.Environment.NewLine +
                                        "1. Measure waist at the navel in men, and midway between the bottom of the ribs and the top of the hip bone in women." + System.Environment.NewLine +
                                        "2. Measure hips at the tip of the hip bone in men and at the widest point between the hips and buttocks in women." + System.Environment.NewLine +
                                        "3. Divide your waist size at its smallest by your hip size at its largest and you get a Waist-to-Hip ratio.";
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillTipsIdealWeight()
        {
            try
            {
                txtIdealWeightTips.Text = "TIPS TO BE NOTED - " + System.Environment.NewLine + System.Environment.NewLine +
                                          "1. Being underweight or overweight are recognized risk factors for many diseases, " +
                                          "such as hypertension, diabetes, hyperlipidemias, and also certain types of cancers.";

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillTipsBodyFatRatio()
        {
            try
            {
                txtBodyFatTips.Text = "WHEN TO MEASURE - " + System.Environment.NewLine + System.Environment.NewLine +
                                      "● The best time to use this formula, is in the morning. Your body weight and waist measurements" +
                                      "  are the most accurate just after you wake up from 7-8 hours of sleep.";
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillTipsBodyFatRatio(double BodyFatPercentage, int SexID)
        {
            try
            {
                if (BodyFatPercentage > 0)
                {
                    if (SexID == 1)
                    {
                        if (BodyFatPercentage >= BodyFat.EssentialFat_Low_Men && BodyFatPercentage <= BodyFat.EssentialFat_High_Men)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + System.Environment.NewLine + System.Environment.NewLine + "You Body Fat Percentage lies With in the EssentialFat Range ";
                        }
                        else if (BodyFatPercentage > BodyFat.Athletes_Low_Men && BodyFatPercentage <= BodyFat.Athletes_High_Men)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + System.Environment.NewLine + System.Environment.NewLine + "You Body Fat Percentage lies With in the Athletes Range ";
                        }
                        else if (BodyFatPercentage >= BodyFat.Fitness_Low_Men && BodyFatPercentage <= BodyFat.Fitness_High_Men)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + System.Environment.NewLine + System.Environment.NewLine + "You Body Fat Percentage lies With in the Fitness Range ";
                        }
                        else if (BodyFatPercentage >= BodyFat.Acceptable_Low_Men && BodyFatPercentage <= BodyFat.Acceptable_High_Men)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + System.Environment.NewLine + System.Environment.NewLine + "You Body Fat Percentage lies With in the Acceptable Range ";
                        }
                        else if (BodyFatPercentage >= BodyFat.Obese_Low_Men)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + System.Environment.NewLine + System.Environment.NewLine + "You Body Fat Percentage lies With in the Obese Range ";
                        }
                    }
                    else if (SexID == 2)
                    {
                        if (BodyFatPercentage >= BodyFat.EssentialFat_Low_Women && BodyFatPercentage <= BodyFat.EssentialFat_High_Women)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + "You Body Fat Percentage lies With in the EssentialFat Range ";
                        }
                        else if (BodyFatPercentage > BodyFat.Athletes_Low_Women && BodyFatPercentage <= BodyFat.Athletes_High_Women)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + "You Body Fat Percentage lies With in the Athletes Range ";
                        }
                        else if (BodyFatPercentage >= BodyFat.Fitness_Low_Women && BodyFatPercentage <= BodyFat.Fitness_High_Women)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + "You Body Fat Percentage lies With in the Fitness Range ";
                        }
                        else if (BodyFatPercentage >= BodyFat.Acceptable_Low_Women && BodyFatPercentage <= BodyFat.Acceptable_High_Women)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + "You Body Fat Percentage lies With in the Acceptable Range ";
                        }
                        else if (BodyFatPercentage >= BodyFat.Obese_Low_Women)
                        {
                            txtBodyFatTips.Text = txtBodyFatTips.Text + "You Body Fat Percentage lies With in the Obese Range ";
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

        #endregion

        #region Events

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            SetCulture();
            SetMaxLength();
            SetTemplate();
            Initialize();

            FillTextBlocks();
            FillComments();
            FillSex(cbSex2);
            FillSex(cbSex5);
            FillSex(cbSex7);
            FillLifeStyle(cbLifeStyle2);

            FillTipsCalorieNeeds();
            FillTipsWaistHips();
            FillTipsIdealWeight();
            FillTipsBodyFatRatio();

            Classes.CommonFunctions.FillDishCategory(cboDishCategory);
            LoadTemplate();
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

                    if (GetDishDetails(1, CurrentDishID) == false)
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

        private void btnCalculate1_Click(object sender, RoutedEventArgs e)
        {
            //BodyMassIndex
            double BMI = 0;
            BMI = CalculateBMI();
            if (BMI > 0)
            {
                lblBMIValue.Content = Convert.ToString(BMI);
            }
            else
            {
                lblBMIValue.Content = string.Empty;
            }

            FillTipsBMI(Classes.CommonFunctions.Convert2Float(Convert.ToString(lblBMIValue.Content)));
        }

        private void btnCalculate2_Click(object sender, RoutedEventArgs e)
        {
            //Calorie Needs
            double Calorie = 0;
            Calorie = CalculateCalorieNeeds();
            if (Calorie > 0)
            {
                lblCalorieValue.Content = Convert.ToString(Calorie) + " Calorie";
            }
            else
            {
                lblCalorieValue.Content = string.Empty;
            }
        }

        private void btnCalculate3_Click(object sender, RoutedEventArgs e)
        {
            //WaistHips Ratio
            double WHRatio = 0;
            WHRatio = CalculateWaistHipsRatio();
            if (WHRatio > 0)
            {
                lblWHRatioValue.Content = Convert.ToString(WHRatio);
            }
            else
            {
                lblWHRatioValue.Content = string.Empty;
            }
        }

        private void btnCalculate5_Click(object sender, RoutedEventArgs e)
        {
            //Ideal Weight
            double[] IdealWeight = new double[2];
            IdealWeight = CalculateIdealWeightRange();

            if (IdealWeight.Length > 0)
            {
                if (IdealWeight[0] > 0 && IdealWeight[1] > 0)
                {
                    lblIdealWeightValue.Content = Convert.ToString(IdealWeight[0]) + " - " + Convert.ToString(IdealWeight[1]) + " Kg";
                }
                else
                {
                    lblIdealWeightValue.Content = string.Empty;
                }
            }

            //Minmum and Maximum Weight
            double[] MinMaxWeight = new double[2];
            MinMaxWeight = CalculateMinMaxWeight();

            if (MinMaxWeight.Length > 0)
            {
                if (MinMaxWeight[0] > 0)
                {
                    lblMinHealthyWeightValue.Content = MinMaxWeight[0];
                }
                else
                {
                    lblMinHealthyWeightValue.Content = string.Empty;
                }

                if (MinMaxWeight[1] > 0)
                {
                    lblMaxHealthyWeightValue.Content = MinMaxWeight[1];
                }
                else
                {
                    lblMaxHealthyWeightValue.Content = string.Empty;
                }
            }
        }

        private void btnCalculate7_Click(object sender, RoutedEventArgs e)
        {
            //BodyFat Ratio
            double BFRatio = 0;
            BFRatio = CalculateBodyFatRatio();
            if (BFRatio > 0)
            {
                lblBFRatioValue.Content = Convert.ToString(BFRatio) + " %";
            }
            else
            {
                lblBFRatioValue.Content = string.Empty;
            }

            FillTipsBodyFatRatio();
            FillTipsBodyFatRatio(BFRatio, Classes.CommonFunctions.Convert2Int(Convert.ToString(cbSex7.SelectedIndex)));
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.Close();
        }
        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                //Close();
            }
        }

        private void cbSex7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSex7.SelectedIndex > 0)
            {
                if (cbSex7.SelectedIndex == 1)
                {
                    lblHips7.Visibility = Visibility.Hidden;
                    lblWrist7.Visibility = Visibility.Hidden;
                    lblForeArm7.Visibility = Visibility.Hidden;
                    txtHips7.Visibility = Visibility.Hidden;
                    txtWrist7.Visibility = Visibility.Hidden;
                    txtForeArm7.Visibility = Visibility.Hidden;
                    img4.Visibility = Visibility.Hidden;
                    img5.Visibility = Visibility.Hidden;
                    img6.Visibility = Visibility.Hidden;
                }
                else if (cbSex7.SelectedIndex == 2)
                {
                    lblHips7.Visibility = Visibility.Visible;
                    lblWrist7.Visibility = Visibility.Visible;
                    lblForeArm7.Visibility = Visibility.Visible;
                    txtHips7.Visibility = Visibility.Visible;
                    txtWrist7.Visibility = Visibility.Visible;
                    txtForeArm7.Visibility = Visibility.Visible;
                    img4.Visibility = Visibility.Visible;
                    img5.Visibility = Visibility.Visible;
                    img6.Visibility = Visibility.Visible;
                }
            }
        }

        private void tbAdvancedSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbAdvancedSearch.SelectedIndex == 0)
            {
                txtHeight1.Focus();
            }
            else if (tbAdvancedSearch.SelectedIndex == 1)
            {
                cbSex2.Focus();
            }
            else if (tbAdvancedSearch.SelectedIndex == 2)
            {
                txtWaist3.Focus();
            }
            else if (tbAdvancedSearch.SelectedIndex == 3)
            {
                cbSex5.Focus();
            }
            else if (tbAdvancedSearch.SelectedIndex == 4)
            {
                cbSex7.Focus();
            }

        }

        private void lblHelp_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion

    }
}

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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Globalization;
using System.IO;
using Microsoft.Win32;
using BONutrition;
using BLNutrition;
using NutritionV1.Common.Classes;
using NutritionV1.Classes;
using NutritionV1.Enums;
using NutritionV1.Constants;
using System.Configuration;
using System.Collections;
using Visifire.Charts;
using Visifire.Commons;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for MemberSearch.xaml
    /// </summary>
    public partial class MemberSearch : Page
    {

        #region Varaibles

        int familyID;
        int memberID;        
        Member member = new Member();
        List<Member> memberList = new List<Member>();
        string SearchString = string.Empty;
        private bool IsMyGridVisible = false;
        private string emptyImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";

        #endregion

        #region Constructor

        public MemberSearch()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region Methods
        
        private void MyGridAnimation(string AnimatioName)
        {
            MyGrid.Visibility = Visibility.Visible;
            //Storyboard gridAnimation = (Storyboard)FindResource(AnimatioName);
            //gridAnimation.Begin(this);
            if (AnimatioName == "ExpandMyGrid")
            {
                IsMyGridVisible = true;
                MyGrid.Visibility = Visibility.Visible;
            }
            else if (AnimatioName == "CollapseMyGrid")
            {
                IsMyGridVisible = false;
                MyGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            MyGrid.Visibility = Visibility.Hidden;
            ((NutritionV1.MasterPage)(Window.GetWindow(this))).mnuTop.Visibility = Visibility.Visible;
        }

        private void SetThemeOnClick(System.Windows.Shapes.Rectangle SelRectangle)
        {
            App apps = (App)Application.Current;
            SelRectangle.Style = (Style)apps.SetStyle["HomeBarSelectStyle"];
        }

        private void RepopulateData()
        {
            FillSearchList();
        }        

        public void InitailizeControls()
        {
            txtMemberName.Text = string.Empty;
            txtSex.Text = string.Empty;
            txtSexID.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtBloodGroup.Text = string.Empty;
            txtLifeStyle.Text = string.Empty;
            txtLifeStyleID.Text = string.Empty;
            txtBodyType.Text = string.Empty;
            txtHeight.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtWaist.Text = string.Empty;
        }

        private void FillSearchList()
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                SearchString = string.Empty;
                if (txtSearch.Text != string.Empty)
                {
                    SearchString = SearchString + "And MemberName LIKE '" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                }
                memberList = MemberManager.GetList(SearchString);
                FillData();
            }
        }

        private void FillData()
        {
            if (memberList.Count > 0)
            {
                lblTotalItemsList.Text = "Total Members : " + Convert.ToString(memberList.Count);
                lvMembers.ItemsSource = memberList;
            }
        }
                                  
        public void DisplayDetails(int MemberID)
        {
            try
            {
                MyGridAnimation("ExpandMyGrid");
                InitailizeControls();
                FillMemberDetails(MemberID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillMemberDetails(int MemberID)
        {
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetList(" AND FamilyMember.MemberID = " + MemberID);
                if (memberList.Count > 0)
                {
                    txtMemberID.Text = Convert.ToString(MemberID);
                    txtMemberName.Text = Convert.ToString(memberList[0].MemberName);
                    txtSex.Text = Convert.ToString(memberList[0].SexName);
                    txtSexID.Text = Convert.ToString(memberList[0].SexID);
                    txtAge.Text = Convert.ToString(memberList[0].Age);
                    txtBloodGroup.Text = Convert.ToString(memberList[0].BloodGroupName);
                    txtLifeStyle.Text = Convert.ToString(memberList[0].LifeStyleName);
                    txtLifeStyleID.Text = Convert.ToString(memberList[0].LifeStyleID);
                    txtBodyType.Text = Convert.ToString(memberList[0].BodyTypeName);
                    txtHeight.Text = Convert.ToString(memberList[0].Height);
                    txtWeight.Text = Convert.ToString(memberList[0].Weight);
                    txtWaist.Text = Convert.ToString(memberList[0].Waist);

                    if (File.Exists(memberList[0].ImagePath) && memberList[0].ImagePath != emptyImagePath)
                    {
                        imgDisplay.ImagePath = memberList[0].ImagePath;
                    }
                    else
                    {
                        imgDisplay.ImagePath = string.Empty;
                    }

                    rdAsian.IsChecked = true;
                    rdBMIBased.IsChecked = true;
                    rdHBFormula.IsChecked = true;
                    if (MemberID > 0)
                    {
                        CheckBMI();
                        CheckIBW();
                        CheckDCR();
                    }
                }
                else
                {
                    imgDisplay.ImagePath = string.Empty;
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

        /// <summary>
        /// Calculate Body Mass Index
        /// </summary>
        private void CheckBMI()
        {
            if (rdAsian.IsChecked == true)
            {
                CalculateBMI_Asian();
            }
            else
            {
                CalculateBMI_WHO();
            }
        }

        /// <summary>
        /// Calculate Ideal Body Weight
        /// </summary>
        private void CheckIBW()
        {
            if (rdBMIBased.IsChecked == true)
            {
                txtIBW.Text = CalculateIBWBMIBased();
            }
            else
            {
                txtIBW.Text = CalculateIBWHamwi();
            }
        }

        /// <summary>
        /// Calculate Daily Calorie Requirement
        /// </summary>
        private void CheckDCR()
        {
            if (rdHBFormula.IsChecked == true)
            {
                lblKCalorieCaption.Visibility = Visibility.Collapsed;
                cbKCalorie.Visibility = Visibility.Collapsed;
                lblKCal.Visibility = Visibility.Collapsed;
                CalculateDCR_HarrisBenedict();
            }
            else
            {
                lblKCalorieCaption.Visibility = Visibility.Visible;
                cbKCalorie.Visibility = Visibility.Visible;
                lblKCal.Visibility = Visibility.Visible;
                CalculateDCR_Manual();
            }
        }

        private void CalculateBMI_WHO()
        {
            double BMI = 0, BMIPercent = 0;
            string imgPath = string.Empty;
            float Height = 0, Weight = 0, HeightinInches = 0, WeightinPounds = 0;
            int memberAge = 0, SexID;
            imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();

            Height = CommonFunctions.Convert2Float(Convert.ToString(txtHeight.Text));
            Weight = CommonFunctions.Convert2Float(Convert.ToString(txtWeight.Text));
            HeightinInches = CommonFunctions.Convert2Float(Convert.ToString(Height * CalorieFormula.CM_INCH));
            WeightinPounds = CommonFunctions.Convert2Float(Convert.ToString(Weight * CalorieFormula.KG_POUND));
            SexID = CommonFunctions.Convert2Int(Convert.ToString(txtSexID.Text));

            BMI = Math.Round((WeightinPounds * CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) / (Math.Pow((HeightinInches * CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)), 2);
            memberAge = CommonFunctions.Convert2Int(txtAge.Text);

            List<BONutrition.NSysAdmin> calorieCalculator = new List<BONutrition.NSysAdmin>();
            calorieCalculator = NSysAdminManager.GetBMIImpact();
            if (HeightinInches != 0 && WeightinPounds != 0)
            {
                if (CheckMajor(familyID, CommonFunctions.Convert2Int(txtMemberID.Text)) == true)
                {
                    if (calorieCalculator.Count > 0)
                    {
                        if (BMI > 0)
                        {
                            imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                            if (BMI < BodyMassIndex.BMIUNDERWEIGHT)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "UNDER WEIGHT";
                                txtRisk.Text = string.Empty;
                            }
                            else if (BMI >= BodyMassIndex.BMINORMAL_LOW && BMI <= BodyMassIndex.BMINORMAL_HIGH1)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "NORMAL WEIGHT";
                                txtRisk.Text = string.Empty;
                            }
                            else if (BMI >= BodyMassIndex.BMIOVERWEIGHT_LOW1 && BMI <= BodyMassIndex.BMIOVERWEIGHT_HIGH1)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OVER WEIGHT";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) <= WaistCircumference.WCMALE_WHO)
                                    {
                                        txtRisk.Text = "INCREASED";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) > WaistCircumference.WCMALE_WHO)
                                    {
                                        txtRisk.Text = "HIGH";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) <= WaistCircumference.WCFEMALE_WHO)
                                    {
                                        txtRisk.Text = "INCREASED";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) > WaistCircumference.WCFEMALE_WHO)
                                    {
                                        txtRisk.Text = "HIGH";
                                    }
                                }
                            }
                            else if (BMI >= BodyMassIndex.BMIOBESE1_LOW1 && BMI <= BodyMassIndex.BMIOBESE1_HIGH1)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OBESE (Level I)";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) <= WaistCircumference.WCMALE_WHO)
                                    {
                                        txtRisk.Text = "HIGH";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) > WaistCircumference.WCMALE_WHO)
                                    {
                                        txtRisk.Text = "VERY HIGH";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) <= WaistCircumference.WCFEMALE_WHO)
                                    {
                                        txtRisk.Text = "HIGH";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) > WaistCircumference.WCFEMALE_WHO)
                                    {
                                        txtRisk.Text = "VERY HIGH";
                                    }
                                }
                            }
                            else if (BMI >= BodyMassIndex.BMIOBESE2_LOW && BMI <= BodyMassIndex.BMIOBESE2_HIGH)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OBESE (Level II)";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) <= WaistCircumference.WCMALE_WHO)
                                    {
                                        txtRisk.Text = "VERY HIGH";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) > WaistCircumference.WCMALE_WHO)
                                    {
                                        txtRisk.Text = "VERY HIGH";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) <= WaistCircumference.WCFEMALE_WHO)
                                    {
                                        txtRisk.Text = "VERY HIGH";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) > WaistCircumference.WCFEMALE_WHO)
                                    {
                                        txtRisk.Text = "VERY HIGH";
                                    }
                                }
                            }
                            else if (BMI >= BodyMassIndex.BMIOVEROBESE1)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "EXTREME OBESE";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) <= WaistCircumference.WCMALE_WHO)
                                    {
                                        txtRisk.Text = "EXTREMELY HIGH";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) > WaistCircumference.WCMALE_WHO)
                                    {
                                        txtRisk.Text = "EXTREMELY HIGH";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) <= WaistCircumference.WCFEMALE_WHO)
                                    {
                                        txtRisk.Text = "EXTREMELY HIGH";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) > WaistCircumference.WCFEMALE_WHO)
                                    {
                                        txtRisk.Text = "EXTREMELY HIGH";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    BMI = Math.Round(((WeightinPounds / Math.Pow(HeightinInches, 2)) * 703), 1);
                    if (memberAge > 0)
                    {
                        BMIPercent = CalorieCalculatorManager.GetBMIPercentile(memberAge, SexID, BMI);
                        if (BMIPercent >= 0)
                        {
                            imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                            if (BMIPercent < BMIPercentile.BMIUNDERWEIGHT)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "UNDER WEIGHT";
                            }
                            else if (BMIPercent >= BMIPercentile.BMINORMAL_LOW && BMIPercent <= BMIPercentile.BMINORMAL_HIGH)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "NORMAL WEIGHT";
                            }
                            else if (BMIPercent > BMIPercentile.BMIOVERWEIGHT_LOW && BMIPercent <= BMIPercentile.BMIOVERWEIGHT_HIGH)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OVER WEIGHT";
                            }
                            else if (BMIPercent >= BMIPercentile.BMIOVEROBESE)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OBESE";
                            }
                        }
                    }
                }
            }
        }

        private void CalculateBMI_Asian()
        {
            double BMI = 0, BMIPercent = 0;
            string imgPath = string.Empty;
            float Height = 0, Weight = 0, HeightinInches = 0, WeightinPounds = 0;
            int memberAge = 0, SexID;
            imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();

            Height = CommonFunctions.Convert2Float(Convert.ToString(txtHeight.Text));
            Weight = CommonFunctions.Convert2Float(Convert.ToString(txtWeight.Text));
            HeightinInches = CommonFunctions.Convert2Float(Convert.ToString(Height * CalorieFormula.CM_INCH));
            WeightinPounds = CommonFunctions.Convert2Float(Convert.ToString(Weight * CalorieFormula.KG_POUND));
            SexID = CommonFunctions.Convert2Int(Convert.ToString(txtSexID.Text));

            BMI = Math.Round((WeightinPounds * CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) / (Math.Pow((HeightinInches * CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)), 2);
            memberAge = CommonFunctions.Convert2Int(txtAge.Text);

            List<BONutrition.NSysAdmin> calorieCalculator = new List<BONutrition.NSysAdmin>();
            calorieCalculator = NSysAdminManager.GetBMIImpact();
            if (HeightinInches != 0 && WeightinPounds != 0)
            {
                if (CheckMajor(familyID, CommonFunctions.Convert2Int(txtMemberID.Text)) == true)
                {
                    if (calorieCalculator.Count > 0)
                    {
                        if (BMI > 0)
                        {
                            imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                            if (BMI < BodyMassIndex.BMIUNDERWEIGHT)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "UNDER WEIGHT";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = string.Empty;
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "AVERAGE";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = string.Empty;
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "AVERAGE";
                                    }
                                }
                            }
                            else if (BMI >= BodyMassIndex.BMINORMAL_LOW && BMI <= BodyMassIndex.BMINORMAL_HIGH2)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "MORMAL WEIGHT";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "LOW RISK";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "INCREASED";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "LOW RISK";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "INCREASED";
                                    }
                                }
                            }
                            else if (BMI >= BodyMassIndex.BMIOVERWEIGHT_LOW2 && BMI <= BodyMassIndex.BMIOVERWEIGHT_HIGH2)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OVER WEIGHT";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "AVERAGE";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "MODERATE";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "AVERAGE";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "MODERATE";
                                    }
                                }
                            }
                            else if (BMI >= BodyMassIndex.BMIOBESE1_LOW2 && BMI <= BodyMassIndex.BMIOBESE1_HIGH2)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OBESE (Level I)";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "MODERATE";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "SEVERE";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "MODERATE";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "SEVERE";
                                    }
                                }
                            }
                            else if (BMI >= BodyMassIndex.BMIOVEROBESE2)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OBESE (Level II)";
                                if (SexID == 1)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "SEVERE";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCMALE_ASIAN)
                                    {
                                        txtRisk.Text = "VERY SEVERE";
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (CommonFunctions.Convert2Int(txtWaist.Text) < WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "SEVERE";
                                    }
                                    else if (CommonFunctions.Convert2Int(txtWaist.Text) >= WaistCircumference.WCFEMALE_ASIAN)
                                    {
                                        txtRisk.Text = "VERY SEVERE";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    BMI = Math.Round(((WeightinPounds / Math.Pow(HeightinInches, 2)) * 703), 1);
                    if (memberAge > 0)
                    {
                        BMIPercent = CalorieCalculatorManager.GetBMIPercentile(memberAge, SexID, BMI);
                        if (BMIPercent >= 0)
                        {
                            imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                            if (BMIPercent < BMIPercentile.BMIUNDERWEIGHT)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "UNDER WEIGHT";
                            }
                            else if (BMIPercent >= BMIPercentile.BMINORMAL_LOW && BMIPercent <= BMIPercentile.BMINORMAL_HIGH)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "NORMAL WEIGHT";
                            }
                            else if (BMIPercent > BMIPercentile.BMIOVERWEIGHT_LOW && BMIPercent <= BMIPercentile.BMIOVERWEIGHT_HIGH)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OVER WEIGHT";
                            }
                            else if (BMIPercent >= BMIPercentile.BMIOVEROBESE)
                            {
                                txtBMIDisplay.Text = Convert.ToString(BMI);
                                txtWeightStatus.Text = "OBESE";
                            }
                        }
                    }
                }
            }
        }

        public string CalculateIBWBMIBased()
        {
            string IBWRange = string.Empty;
            double Height = 0;
            double IdelaWeightLower = 0, IdealWeightUpper = 0;
            double[] IdealWeight = new double[2];

            if (txtHeight.Text == string.Empty)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1127"), "", AlertType.Information, AlertButtons.OK);
                txtHeight.Focus();
                return string.Empty;
            }
            else if (Classes.CommonFunctions.Convert2Int(txtHeight.Text) < 50 || Classes.CommonFunctions.Convert2Int(txtHeight.Text) > 200)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1128"), "", AlertType.Information, AlertButtons.OK);
                txtHeight.Focus();
                return string.Empty;
            }

            try
            {
                Height = Classes.CommonFunctions.Convert2Double(Convert.ToString(txtHeight.Text.Trim()));

                if (Height != 0)
                {
                    IdelaWeightLower = Math.Round(BodyMassIndex.BMINORMAL_RangeLOW * Math.Pow(Classes.CommonFunctions.Convert2Double(Convert.ToString(Height / (int)PercentageType.Percentage_Normal)), 2), 0);
                    IdealWeightUpper = Math.Round(BodyMassIndex.BMINORMAL_RangeHIGH * Math.Pow(Classes.CommonFunctions.Convert2Double(Convert.ToString(Height / (int)PercentageType.Percentage_Normal)), 2), 0);

                    IdealWeight[0] = Classes.CommonFunctions.Convert2Float(Convert.ToString(IdelaWeightLower));
                    IdealWeight[1] = Classes.CommonFunctions.Convert2Float(Convert.ToString(IdealWeightUpper));

                    if (IdealWeight[0] > 0 && IdealWeight[1] > 0)
                    {
                        IBWRange = Convert.ToString(IdealWeight[0]) + " - " + Convert.ToString(IdealWeight[1]) + " Kg";
                    }
                    else
                    {
                        IBWRange = string.Empty;
                    }
                }
                return IBWRange;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return IBWRange;
            }
            finally
            {

            }
        }

        public string CalculateIBWHamwi()
        {
            double Height = 0, HeightinInches = 0;
            double IdealWeight = 0;
            string IBW = string.Empty;
            int BodyTypeID = CommonFunctions.Convert2Int(Convert.ToString(txtBodyTypeID.Text));
            int SexID = CommonFunctions.Convert2Int(Convert.ToString(txtSexID.Text));
            try
            {
                HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(Convert.ToInt32(txtHeight.Text) * CalorieFormula.CM_INCH));
                Height = Classes.CommonFunctions.Convert2Double(Convert.ToString(txtHeight.Text.Trim()));

                if (Height != 0)
                {
                    IdealWeight = Math.Round(BodyMassIndex.BMINORMAL_RangeLOW * Math.Pow(Classes.CommonFunctions.Convert2Double(Convert.ToString(Height / (int)PercentageType.Percentage_Normal)), 2), 0);
                    IBW = Convert.ToString(IdealWeight);
                }

                if (SexID == 1)
                {
                    if (HeightinInches > BodyFrame.FEET5INCHES)
                    {
                        IdealWeight = Math.Round(BodyFrame.MALEWEIGHT5FEET + (HeightinInches - BodyFrame.FEET5INCHES) * BodyFrame.MALEWEIGHTADDITION, 0);
                        IBW = Convert.ToString(IdealWeight);
                    }
                }
                else if (SexID == 2)
                {
                    if (HeightinInches > BodyFrame.FEET5INCHES)
                    {
                        IdealWeight = Math.Round(BodyFrame.FEMALEWEIGHT5FEET + (HeightinInches - BodyFrame.FEET5INCHES) * BodyFrame.FEMALEWEIGHTADDITION, 0);
                        IBW = Convert.ToString(IdealWeight);
                    }
                }

                if (BodyTypeID == (int)BodyFrameType.Small && IdealWeight > 0)
                {
                    IdealWeight = IdealWeight - (IdealWeight * (BodyFrame.IBWPERCENT / 100));
                    IBW = Convert.ToString(Math.Round(IdealWeight, 0));
                }
                else if (BodyTypeID == (int)BodyFrameType.Large && IdealWeight > 0)
                {
                    IdealWeight = IdealWeight + (IdealWeight * (BodyFrame.IBWPERCENT / 100));
                    IBW = Convert.ToString(Math.Round(IdealWeight, 0));
                }

                return IBW + " kg";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return IBW;
            }
            finally
            {

            }
        }


        private void CalculateDCR_HarrisBenedict()
        {
            int SexID, Age, LifeStyleID;
            double HeightinInches, WeightinPounds, LifeStyleValue = 0, DCRValue = 0;
            try
            {
                SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(txtSexID.Text));
                Age = 0;
                if (CommonFunctions.Convert2Int(txtAge.Text) > 0)
                {
                    Age = Classes.CommonFunctions.Convert2Int(Convert.ToString(txtAge.Text));
                }                

                HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(Convert.ToInt32(txtHeight.Text) * CalorieFormula.CM_INCH));
                WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString(Convert.ToInt32(txtWeight.Text) * CalorieFormula.KG_POUND));
                LifeStyleID = Classes.CommonFunctions.Convert2Int(Convert.ToString(txtLifeStyleID.Text));

                switch (LifeStyleID)
                {
                    case (int)LifeStyleType.Sedentary:
                        LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.SEDENTARY));
                        break;
                    case (int)LifeStyleType.LightlyActive:
                        LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.LIGHTLYACTIVE));
                        break;
                    case (int)LifeStyleType.ModeratelyActive:
                        LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.MODERATELYACTIVE));
                        break;
                    case (int)LifeStyleType.VeryActive:
                        LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.VERYACTIVE));
                        break;
                    case (int)LifeStyleType.ExtraActive:
                        LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.EXTRAACTIVE));
                        break;
                }

                if (SexID == 1)
                {
                    DCRValue = Math.Round((((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEWEIGHT_MEN))) + (HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEHEIGHT_MEN))) + Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIECOMMON_MEN))) - Age * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEAGE_MEN))) * LifeStyleValue, 0);
                }
                else if (SexID == 2)
                {
                    DCRValue = Math.Round((((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEWEIGHT_WOMEN))) + (HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEHEIGHT_WOMEN))) + Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIECOMMON_WOMEN))) - Age * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEAGE_WOMEN))) * LifeStyleValue, 0);
                }

                txtDCR.Text = Convert.ToString(DCRValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void CalculateDCR_Manual()
        {
            double DCRValue = 0;
            try
            {
                if (CommonFunctions.Convert2Double(txtWeight.Text) > 0 && cbKCalorie.SelectedItem.ToString() != string.Empty)
                {
                    DCRValue = Math.Round(CommonFunctions.Convert2Double(txtWeight.Text) * CommonFunctions.Convert2Double(cbKCalorie.SelectedItem.ToString()), 0);
                    txtDCR.Text = Convert.ToString(DCRValue);
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

        private bool CheckMajor(int FamilyID, int MemberID)
        {
            int Age = 0;
            bool IsMajor = false;

            try
            {
                Age = CommonFunctions.Convert2Int(txtAge.Text);

                if (Age >= 18)
                {
                    IsMajor = true;
                }
                else
                {
                    IsMajor = false;
                }

                return IsMajor;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return IsMajor;
            }
            finally
            {

            }
        }  

        #endregion        

        #region Events
        
        private void lblMyClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyGridAnimation("CollapseMyGrid");
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            familyID = MemberManager.GetFamilyID();

            Keyboard.Focus(txtSearch);
            RepopulateData();

            txtSource.Text = "● Comparison of Waist measurement and BMI " + System.Environment.NewLine + System.Environment.NewLine +
                                         "  SOURCE: " + System.Environment.NewLine + 
                                         "  Risk comparison - National Heart, Lung and Blood Institute, BMI classification - WHO " + System.Environment.NewLine +
                                         "  BMI, Body Mass Index; IOTF, International Obesity Taskforce, Risk assessment : Obesity Classification for ASIA- PACIFIC " + System.Environment.NewLine;
        }

        private void spSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtSearch.Text.Trim() != string.Empty)
            {
                using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
                {
                    SearchString = string.Empty;
                    SearchString = "Where 1=1 ";
                    if (txtSearch.Text != string.Empty)
                    {
                        SearchString = SearchString + "And MemberName LIKE '" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                    }
                    FillSearchList();
                }
            }            
        }
        
        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (IsMyGridVisible == true)
                {
                    MyGridAnimation("CollapseMyGrid");
                }
            }
        }

        private void spMember_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ListBoxItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListBoxItem)) as ListBoxItem;
                if (lvi != null)
                {
                    memberID = ((BONutrition.Member)(((System.Windows.Controls.ContentControl)(lvi)).Content)).MemberID;
                    if (memberID > 0)
                    {
                        DisplayDetails(memberID);
                    }
                }
            }
        }

        private void spAddNew_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                MemberProfile memberProfile = new MemberProfile();
                MemberProfile.MemberID = 0;
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(memberProfile);
            }
        }        

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (lvMembers.SelectedIndex >= 0)
                {
                    int memID = 0;
                    memID = Classes.CommonFunctions.Convert2Int(Convert.ToString(((Member)lvMembers.Items[lvMembers.SelectedIndex]).MemberID));
                    if (memID > 0)
                    {
                        MemberProfile memberProfile = new MemberProfile();
                        MemberProfile.MemberID = memID;
                        ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(memberProfile);
                    }
                }
            }
        }

        private void cbKCalorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateDCR_Manual();
        }

        private void rdAsian_Checked(object sender, RoutedEventArgs e)
        {
            CheckBMI();
        }

        private void rdWHO_Checked(object sender, RoutedEventArgs e)
        {
            CheckBMI();
        }

        private void rdBMIBased_Checked(object sender, RoutedEventArgs e)
        {
            CheckIBW();
        }

        private void rdHamwi_Checked(object sender, RoutedEventArgs e)
        {
            CheckIBW();
        }

        private void rdHBFormula_Checked(object sender, RoutedEventArgs e)
        {
            CheckDCR();
        }

        private void rdManual_Checked(object sender, RoutedEventArgs e)
        {
            CheckDCR();
        }

        #endregion                                       
    }
}

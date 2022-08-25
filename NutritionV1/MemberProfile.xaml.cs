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
using System.IO;
using System.Threading;
using System.Transactions;
using System.Resources;
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Common.Classes;
using NutritionV1.Classes;
using NutritionV1.Constants;
using NutritionV1.Enums;
using System.Collections;
using Microsoft.Win32;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for MemberProfile.xaml
    /// </summary>
    public partial class MemberProfile : Page
    {
        #region Declarations

        Member member = new Member();
        List<Ingredient> ingredientFavList = new List<Ingredient>();
        List<Ingredient> ingredientList = new List<Ingredient>();

        public static int MemberID;
        public static int familyID;
        public static int MemberIDPrev;
        private bool IsImageAdd;
        private string imageFileName = string.Empty;
        private string imageDefaultPath = string.Empty;
        OpenFileDialog openDlg = new OpenFileDialog();

        private string uploadCaption = string.Empty;
        private string removeCaption = string.Empty;
        private string emptyImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";

        #endregion

        #region Constructor

        public MemberProfile()
        {
            InitializeComponent();
        }

        #endregion

        #region MemberBasic
        
        private void FillFamilyMember(int FamilyID)
        {
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetList(" AND FamilyMember.FamilyID = " + FamilyID + " AND FamilyMember.MemberID = " + MemberID);
                if (memberList.Count > 0)
                {
                    txtMemberName.Text = Convert.ToString(memberList[0].MemberName);
                    cbSex.SelectedIndex = Convert.ToInt16(memberList[0].SexID);
                    dtpDOB.SelectedDate = Convert.ToDateTime(memberList[0].DOB);
                    // dtpDOB.IsEnabled= false;
                    cbBloodGroup.SelectedIndex = Convert.ToInt16(memberList[0].BloodGroupID);
                    cbLifeStyle.SelectedIndex = Convert.ToInt16(memberList[0].LifeStyleID);
                    cbBodyType.SelectedIndex = Convert.ToInt16(memberList[0].BodyTypeID);
                    chkPregnancy.IsChecked = Convert.ToBoolean(memberList[0].Pregnancy);
                    chkLactation.IsChecked = Convert.ToBoolean(memberList[0].Lactation);
                    txtHeight.Text = Convert.ToString(memberList[0].Height);
                    txtWeight.Text = Convert.ToString(memberList[0].Weight);
                    txtWaist.Text = Convert.ToString(memberList[0].Waist);

                    if (Convert.ToInt16(memberList[0].Lactation) == 1)
                        rdLactation1.IsChecked = true;
                    else if (Convert.ToInt16(memberList[0].Lactation) == 1)
                        rdLactation2.IsChecked = true;

                    if (File.Exists(memberList[0].ImagePath) && memberList[0].ImagePath != emptyImagePath)
                    {
                        imageFileName = memberList[0].ImagePath;
                        imageDefaultPath = memberList[0].ImagePath;
                        imgDisplay.ImagePath = memberList[0].ImagePath;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private bool SaveFamilyMember(int FamilyID)
        {
            string imageDestination = string.Empty;
            try
            {
                if (ValidateFamilyMember() == true)
                {
                    member.FamilyID = FamilyID;
                    if (MemberID > 0)
                    {
                        member.MemberID = MemberID;
                    }
                    else
                    {
                        member.MemberID = MemberManager.GetMemberID();
                    }
                    member.MemberName = txtMemberName.Text.Trim();
                    member.SexID = Convert.ToByte(cbSex.SelectedIndex);

                    DateTime dateOfBirth = DateTime.Now;
                    if (CommonFunctions.Convert2Int(txtAge.Text) > 0 && dtpDOB.SelectedDate == null)
                    {
                        dateOfBirth = new DateTime(DateTime.Now.AddYears(-CommonFunctions.Convert2Int(txtAge.Text)).Year, 1, 1);
                    }
                    else
                    {
                        dateOfBirth = CommonFunctions.Convert2DateTime(Convert.ToString(dtpDOB.SelectedDate));
                    }

                    member.DOB = dateOfBirth == null ? DateTime.Now : dateOfBirth;
                    member.BloodGroupID = Convert.ToByte(cbBloodGroup.SelectedIndex);
                    member.LifeStyleID = Convert.ToByte(cbLifeStyle.SelectedIndex);
                    member.Pregnancy = Convert.ToBoolean(chkPregnancy.IsChecked);
                    member.Lactation = Convert.ToBoolean(chkLactation.IsChecked);
                    if (rdLactation1.IsChecked == true)
                        member.LactationType = 1;
                    else if (rdLactation2.IsChecked == true)
                        member.LactationType = 2;

                    if (imageFileName != string.Empty && imageFileName != emptyImagePath)
                    {
                        imageDestination = GetImagePath("Members") + @"\" + member.MemberName + ".jpg";
                        if (imageFileName != imageDestination)
                        {
                            if (File.Exists(imageFileName))
                            {
                                member.ImagePath = CopyFile(imageFileName, imageDestination);
                                btnAddImage.Content = removeCaption;
                            }
                            else
                            {
                                member.ImagePath = imageFileName;
                            }
                        }
                        else
                        {
                            member.ImagePath = imageFileName;
                        }
                    }
                    else
                    {
                        member.ImagePath = string.Empty;
                    }
                    member.Height = CommonFunctions.Convert2Float(txtHeight.Text.Trim());
                    member.Weight = CommonFunctions.Convert2Float(txtWeight.Text.Trim());
                    member.BodyTypeID = Convert.ToByte(cbBodyType.SelectedIndex);
                    member.Waist = CommonFunctions.Convert2Float(txtWaist.Text.Trim());

                    return MemberManager.SaveFamilyMember(member);                    
                }
                else
                {
                    return false;
                }

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

        private string CopyFile(string sourceFile, string destFile)
        {
            try
            {
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
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private string GetImagePath(string FolderName)
        {
            string filePath = string.Empty;
            try
            {
                filePath = AppDomain.CurrentDomain.BaseDirectory + @"Pictures\" + FolderName;
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
        private bool ValidateFamilyMember()
        {
            int age = CommonFunctions.Convert2Int(txtAge.Text);
            if (txtMemberName.Text == string.Empty)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1204"), "", AlertType.Information, AlertButtons.OK);
                tbMemberProfile.SelectedIndex = 0;
                txtMemberName.Focus();
                return false;
            }
            if (cbSex.SelectedIndex <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1205"), "", AlertType.Information, AlertButtons.OK);
                tbMemberProfile.SelectedIndex = 0;
                cbSex.Focus();
                return false;
            }

            if (dtpDOB.SelectedDate != null)
            {
                if (GetMemberAge() > 120 || GetMemberAge() < 1)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1206"), "", AlertType.Information, AlertButtons.OK);
                    tbMemberProfile.SelectedIndex = 0;
                    dtpDOB.Focus();
                    return false;
                }
            }
            else
            {
                if (age > 0)
                {
                    if (age > 120 || age < 1)
                    {
                        AlertBox.Show(XMLServices.GetXmlMessage("E1206"), "", AlertType.Information, AlertButtons.OK);
                        tbMemberProfile.SelectedIndex = 0;
                        txtAge.Focus();
                        return false;
                    }
                }
                else
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1207"), "", AlertType.Information, AlertButtons.OK);
                    tbMemberProfile.SelectedIndex = 0;
                    dtpDOB.Focus();
                    return false;
                }
            }

            if (CommonFunctions.Convert2Double(txtHeight.Text) != 0)
            {
                if (CommonFunctions.Convert2Double(txtHeight.Text) < 50 || CommonFunctions.Convert2Double(txtHeight.Text) > 200)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1139"), "", AlertType.Information, AlertButtons.OK);
                    tbMemberProfile.SelectedIndex = 1;
                    txtHeight.Focus();
                    return false;
                }
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1210"), "", AlertType.Information, AlertButtons.OK);
                tbMemberProfile.SelectedIndex = 1;
                txtHeight.Focus();
                return false;
            }
            if (CommonFunctions.Convert2Double(txtWeight.Text) != 0)
            {
                if (CommonFunctions.Convert2Double(txtWeight.Text) < 1 || CommonFunctions.Convert2Double(txtWeight.Text) > 200)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1141"), "", AlertType.Information, AlertButtons.OK);
                    tbMemberProfile.SelectedIndex = 1;
                    txtWeight.Focus();
                    return false;
                }
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1211"), "", AlertType.Information, AlertButtons.OK);
                tbMemberProfile.SelectedIndex = 1;
                txtWeight.Focus();
                return false;
            }

            if (CommonFunctions.Convert2Int(txtWaist.Text) <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1132"), "", AlertType.Information, AlertButtons.OK);
                tbMemberProfile.SelectedIndex = 0;
                txtWaist.Focus();
                return false;
            }
            if (cbLifeStyle.SelectedIndex <= 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1209"), "", AlertType.Information, AlertButtons.OK);
                tbMemberProfile.SelectedIndex = 0;
                cbLifeStyle.Focus();
                return false;
            }

            return true;
        }       

        private void FillBloodGroup()
        {
            try
            {
                List<NSysAdmin> adminList = new List<NSysAdmin>();
                adminList = NSysAdminManager.GetBloodGroup();
                if (adminList != null)
                {
                    NSysAdmin admin = new NSysAdmin();
                    admin.BloodGroupName = "---Select---";
                    admin.BloodGroupID = 0;
                    adminList.Insert(0, admin);
                    cbBloodGroup.DisplayMemberPath = "BloodGroupName";
                    cbBloodGroup.SelectedValuePath = "BloodGroupID";
                    cbBloodGroup.ItemsSource = adminList;
                }
                cbBloodGroup.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void FillSex()
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
                    cbSex.DisplayMemberPath = "SexName";
                    cbSex.SelectedValuePath = "SexID";
                    cbSex.ItemsSource = adminList;
                }
                cbSex.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }        

        private void FillBodyType()
        {
            try
            {
                List<NSysAdmin> adminList = new List<NSysAdmin>();
                adminList = NSysAdminManager.GetBodyType();
                if (adminList != null)
                {
                    NSysAdmin admin = new NSysAdmin();
                    admin.BodyTypeName = "---Select---";
                    admin.BodyTypeID = 0;
                    adminList.Insert(0, admin);
                    cbBodyType.DisplayMemberPath = "BodyTypeName";
                    cbBodyType.SelectedValuePath = "BodyTypeID";
                    cbBodyType.ItemsSource = adminList;
                }
                cbBodyType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        
        private void FillLifeStyle()
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
                    cbLifeStyle.DisplayMemberPath = "LifeStyleName";
                    cbLifeStyle.SelectedValuePath = "LifeStyleID";
                    cbLifeStyle.ItemsSource = adminList;
                }
                cbLifeStyle.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void FillKCalorie()
        {
            try
            {
                cbKCalorie.Items.Clear();
                cbKCalorie.Items.Add("20");
                cbKCalorie.Items.Add("25");
                cbKCalorie.Items.Add("30");
                cbKCalorie.Items.Add("35");
                cbKCalorie.Items.Add("40");
                cbKCalorie.Items.Add("45");
                cbKCalorie.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public int GetMemberAge()
        {
            if(dtpDOB.SelectedDate == null)
                return 0;

            int MemberAge;
            DateTime Now = System.DateTime.Now;
            DateTime DOB = CommonFunctions.Convert2DateTime(Convert.ToString(dtpDOB.SelectedDate));

            TimeSpan ts = Now.Subtract(DOB);
            MemberAge = CommonFunctions.Convert2Int(Convert.ToString(ts.Days)) / (int)DayType.Days_Year;
            return MemberAge;
        }

        #endregion               


        #region Methods
        
        private void SetCulture()
        {
            App apps = (App)Application.Current;
            ResourceManager rm = apps.getLanguageList;
            uploadCaption = "Upload Image";
            removeCaption = "Remove Image";
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;

            //MemberBasic
            imgDisplay.SetThemes = true;
            lblMemberName.Style = (Style)apps.SetStyle["LabelStyle"];
            lblDOB.Style = (Style)apps.SetStyle["LabelStyle"];
            lblSex.Style = (Style)apps.SetStyle["LabelStyle"];
            lblBloodGroup.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWaist.Style = (Style)apps.SetStyle["LabelStyle"];

            //MemberGeneral
            lblHeight.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWeight.Style = (Style)apps.SetStyle["LabelStyle"];
            lblBodyType.Style = (Style)apps.SetStyle["LabelStyle"];

            ((NutritionV1.MasterPage)(Window.GetWindow(this))).mnuTop.Visibility = Visibility.Visible;
        }

        public void SetMaxLength()
        {
            txtMemberName.MaxLength = 50;
            txtHeight.MaxLength = 6;
            txtWeight.MaxLength = 6;
        }

        public void Refresh()
        {

        }

        public void ClearControls()
        {
            //MemberBasic
            MemberID = 0;
            txtMemberName.Text = string.Empty;
            txtAge.Text = string.Empty;
            dtpDOB.SelectedDate = null;
            cbSex.SelectedIndex = 0;
            //cbDOB.SelectedIndex = 0;
            cbBloodGroup.SelectedIndex = 0;
            cbLifeStyle.SelectedIndex = 0;
            txtWaist.Text = string.Empty;

            //MemberGeneral
            txtHeight.Text = string.Empty;
            txtWeight.Text = string.Empty;
            cbBodyType.SelectedIndex = 0;
            txtBMIDisplay.Text = string.Empty;
            txtRisk.Text = string.Empty;
            txtWeightStatus.Text = string.Empty;
            //imgWeightDisplay.ImageSource = "";

            txtIBW.Text = string.Empty;
            cbKCalorie.SelectedIndex = 0;
            txtDCR.Text = string.Empty;

            chkPregnancy.IsChecked = false;
            chkLactation.IsChecked = false;

            recPregnancyLactation.Visibility = Visibility.Hidden;

            imageFileName = string.Empty;
            imageDefaultPath = string.Empty;
            imgDisplay.ImagePath = string.Empty;
            btnAddImage.Content = uploadCaption;
            IsImageAdd = true;
        }

        private void DisableControls()
        {
            rdLactation1.IsEnabled = false;
            rdLactation2.IsEnabled = false;
            recLifestyle.Visibility = Visibility.Hidden;

            lblKCalorieCaption.Visibility = Visibility.Collapsed;
            cbKCalorie.Visibility = Visibility.Collapsed;
            lblKCal.Visibility = Visibility.Collapsed;
        }
           

        public void SaveData(int FamilyID)
        {
            try
            {
                if (SaveFamilyMember(FamilyID))
                {
                    ClearControls();
                    txtMemberName.Focus();
                    AlertBox.Show(XMLServices.GetXmlMessage("E1219"), "", AlertType.Information, AlertButtons.OK);
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

        public void FillData()
        {
            try
            {
                FillFamilyMember(familyID);                
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        
        private void CheckMajorWomen(int Age)
        {
            recPregnancyLactation.Visibility = Visibility.Hidden;
            lblPregnancy.Visibility = Visibility.Hidden;
            chkPregnancy.Visibility = Visibility.Hidden;
            lblLactation.Visibility = Visibility.Hidden;
            chkLactation.Visibility = Visibility.Hidden;
            rdLactation1.Visibility = Visibility.Hidden;
            rdLactation2.Visibility = Visibility.Hidden;

            if (cbSex.SelectedIndex == 2 && Age >= 18)
            {
                recPregnancyLactation.Visibility = Visibility.Visible;
                lblPregnancy.Visibility = Visibility.Visible;
                chkPregnancy.Visibility = Visibility.Visible;
                lblLactation.Visibility = Visibility.Visible;
                chkLactation.Visibility = Visibility.Visible;
                rdLactation1.Visibility = Visibility.Visible;
                rdLactation2.Visibility = Visibility.Visible;
            }
        }

        public void DispalySaveMessage()
        {
            string DisplayText = "After entering the Member Information," + "\n" +
                "please click the Save button.";

            PopupHelper.ShowAutomaticPopUp(70, 280, 640, 480, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\bg.png")), DisplayText, new Thickness(3));
        }

        public void HideSaveMessage()
        {

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
            SexID = CommonFunctions.Convert2Int(Convert.ToString(cbSex.SelectedIndex));

            BMI = Math.Round((WeightinPounds * CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) / (Math.Pow((HeightinInches * CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)), 2);
            memberAge = GetMemberAge();

            List<BONutrition.NSysAdmin> calorieCalculator = new List<BONutrition.NSysAdmin>();
            calorieCalculator = NSysAdminManager.GetBMIImpact();
            if (HeightinInches != 0 && WeightinPounds != 0)
            {
                if (CheckMajor(familyID) == true)
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
            SexID = CommonFunctions.Convert2Int(Convert.ToString(cbSex.SelectedIndex));

            BMI = Math.Round((WeightinPounds * CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) / (Math.Pow((HeightinInches * CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)), 2);
            memberAge = GetMemberAge();

            List<BONutrition.NSysAdmin> calorieCalculator = new List<BONutrition.NSysAdmin>();
            calorieCalculator = NSysAdminManager.GetBMIImpact();
            if (HeightinInches != 0 && WeightinPounds != 0)
            {
                if (CheckMajor(familyID) == true)
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

            //if (txtHeight.Text == string.Empty)
            //{
            //    AlertBox.Show(XMLServices.GetXmlMessage("E1127"), "", AlertType.Information, AlertButtons.OK);
            //    txtHeight.Focus();
            //    return string.Empty;
            //}
            //else if (Classes.CommonFunctions.Convert2Int(txtHeight.Text) < 50 || Classes.CommonFunctions.Convert2Int(txtHeight.Text) > 200)
            //{
            //    AlertBox.Show(XMLServices.GetXmlMessage("E1128"), "", AlertType.Information, AlertButtons.OK);
            //    txtHeight.Focus();
            //    return string.Empty;
            //}  

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

            //if (cbSex.SelectedIndex <= 0)
            //{
            //    AlertBox.Show(XMLServices.GetXmlMessage("E1126"), "", AlertType.Information, AlertButtons.OK);
            //    cbSex.Focus();
            //    return IBW;
            //}            
            //else if (Classes.CommonFunctions.Convert2Int(txtHeight.Text) < 50 || Classes.CommonFunctions.Convert2Int(txtHeight.Text) > 200)
            //{
            //    AlertBox.Show(XMLServices.GetXmlMessage("E1120"), "", AlertType.Information, AlertButtons.OK);
            //    txtHeight.Focus();
            //    return IBW;
            //}
            //if (cbBodyType.SelectedIndex <= 0)
            //{
            //    AlertBox.Show(XMLServices.GetXmlMessage("E1136"), "", AlertType.Information, AlertButtons.OK);
            //    cbBodyType.Focus();
            //    return IBW;
            //}

            int BodyTypeID = CommonFunctions.Convert2Int(Convert.ToString(cbBodyType.SelectedIndex));
            int SexID = CommonFunctions.Convert2Int(Convert.ToString(cbSex.SelectedIndex));
            try
            {
                HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.Convert2Int(txtHeight.Text) * CalorieFormula.CM_INCH));
                Height = Classes.CommonFunctions.Convert2Double(Convert.ToString(txtHeight.Text.Trim()));               
                if (SexID == 1)
                {
                    if (HeightinInches > BodyFrame.FEET5INCHES)
                    {
                        IdealWeight = Math.Round(BodyFrame.MALEWEIGHT5FEET + (HeightinInches - BodyFrame.FEET5INCHES) * BodyFrame.MALEWEIGHTADDITION,0);
                        IBW = Convert.ToString(IdealWeight);
                    }
                }
                else if (SexID == 2)
                {
                    if (HeightinInches > BodyFrame.FEET5INCHES)
                    {
                        IdealWeight = Math.Round(BodyFrame.FEMALEWEIGHT5FEET + (HeightinInches - BodyFrame.FEET5INCHES) * BodyFrame.FEMALEWEIGHTADDITION,0);
                        IBW = Convert.ToString(IdealWeight);
                    }
                }

                if (BodyTypeID == (int)BodyFrameType.Small && IdealWeight > 0)
                {
                    IdealWeight = IdealWeight - (IdealWeight * (BodyFrame.IBWPERCENT / 100));
                    IBW = Convert.ToString(Math.Round(IdealWeight,0));
                }
                else if (BodyTypeID == (int)BodyFrameType.Large && IdealWeight > 0)
                {
                    IdealWeight = IdealWeight + (IdealWeight * (BodyFrame.IBWPERCENT / 100));
                    IBW = Convert.ToString(Math.Round(IdealWeight,0));
                }
                
                if(IBW != string.Empty)
                    IBW = IBW + " kg";

                return IBW;
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
                SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbSex.SelectedIndex));
                Age = 0;
                if (CommonFunctions.Convert2Int(txtAge.Text) > 0)
                {
                    Age = Classes.CommonFunctions.Convert2Int(Convert.ToString(txtAge.Text));
                }
                else
                {
                    Age = GetMemberAge();
                }

                HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.Convert2Int(txtHeight.Text) * CalorieFormula.CM_INCH));
                WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString(CommonFunctions.Convert2Int(txtWeight.Text) * CalorieFormula.KG_POUND));
                LifeStyleID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cbLifeStyle.SelectedIndex));

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

        private bool CheckMajor(int FamilyID)
        {
            int Age = 0;
            bool IsMajor = false;

            try
            {
                Age = GetMemberAge();

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

        private string OpenDialog(string Title, string Filter, string FileName)
        {
            string filename = string.Empty;
            openDlg.Title = Title;
            openDlg.FileName = FileName; // Default file name
            openDlg.Filter = Filter; // Filter files by extension

            Nullable<bool> result = openDlg.ShowDialog();

            if (result == true)
            {
                filename = openDlg.FileName;
            }
            return filename;
        }
        
        private void SetDates()
        {
           dtpDOB.SelectedDate = System.DateTime.Now;
        }

        /// <summary>
        /// Calculate Body Mass Index
        /// </summary>
        private void CheckBMI()
        {
            if (rdAsian.IsChecked == true)
            {
                CalculateBMI_Asian();

                txtComments1.Text = "     < 18.5          Underweight";
                txtComments2.Text = "     18.5-22.9       Normal";
                txtComments3.Text = "     23.0-24.9       Risk of Obesity";
                txtComments4.Text = "     25.0-29.9       Obesity Class I";
                txtComments5.Text = "     >= 30           Obese Class II";
            }
            else
            {
                CalculateBMI_WHO();

                txtComments1.Text = "     < 18.5          Underweight";
                txtComments2.Text = "     18.5-24.9       Normal";
                txtComments3.Text = "     25.0-29.9       Overweight";
                txtComments4.Text = "     30.0-34.9       Obesity Class I";
                txtComments5.Text = "     35.0-39.9       Obesity Class II";
                txtComments6.Text = "     >= 40           Extreme Obesity";
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

        //private void FillIngredients(bool isAllergic)
        //{
        //    string searchString = "";
        //    using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
        //    {
        //        if (cboFoodCategory.SelectedIndex > 0)
        //        {
        //            lblIngredient.Content = "Ingredient List (" + cboFoodCategory.Text + ")";
        //            lblIngredientAllergic.Content = "Allergic Foods (" + cboFoodCategory.Text + ")";
        //        }
        //        else
        //        {
        //            lblIngredient.Content = "Ingredient List";
        //            lblIngredientAllergic.Content = "Allergic Foods";
        //        }

        //        //gvIngNameCol.CellTemplate = this.FindResource("nameTemplate") as DataTemplate;
        //        //gvIngAllergicNameCol.CellTemplate = this.FindResource("nameTemplate") as DataTemplate;

        //        searchString = " IsSystemIngredient = 'TRUE'";
        //        if (isAllergic)
        //        {
        //            searchString = searchString + " AND IngredientID IN (Select IngredientID from IngredientFavourite Where MemberID = " + MemberID + ")";
        //        }
        //        else
        //        {
        //            if (txtSearchIngredient.Text.Trim() != string.Empty)
        //            {
        //                searchString = searchString + " AND (IngredientName LIKE '%" + txtSearchIngredient.Text.Trim() + "%' ";
        //                searchString = searchString + " OR DisplayName LIKE '%" + txtSearchIngredient.Text.Trim() + "%') ";
        //            }
        //            if (cboFoodCategory.SelectedIndex > 0)
        //            {
        //                searchString = searchString + " AND FoodHabitID = " + cboFoodCategory.SelectedValue;
        //            }
        //            searchString = searchString + " AND IngredientID NOT IN (Select IngredientID from IngredientFavourite  Where MemberID = " + MemberID + ")";
        //        }

        //        searchString = "Where " + searchString + " Order By IngredientName";

        //        if (isAllergic)
        //        {
        //            ingredientFavList = IngredientManager.GetIngredientList(searchString);
        //            if (ingredientFavList != null)
        //            {
        //                lvIngredientAllergicList.ItemsSource = ingredientFavList;
        //                lvIngredientAllergicList.Items.Refresh();
        //            }
        //        }
        //        else
        //        {
        //            ingredientList = IngredientManager.GetIngredientList(searchString);
        //            if (ingredientList != null)
        //            {
        //                lvIngredientList.ItemsSource = ingredientList;
        //                lvIngredientList.Items.Refresh();
        //            }
        //        }
        //    }
        //}

        //private void AddIngredientAllergic()
        //{
        //    if (MemberID > 0)
        //    {
        //        if (lvIngredientList.SelectedIndex != -1)
        //        {
        //            foreach (Ingredient item in lvIngredientList.SelectedItems)
        //            {
        //                IngredientManager.SaveAllergic(MemberID, item);
        //            }
        //            FillIngredients(true);
        //            FillIngredients(false);
        //        }
        //        else
        //        {
        //            AlertBox.Show("Please select the ingredient to add as favourites");
        //        }
        //    }            
        //}

        //private void RemoveIngredientAllergic()
        //{
        //    if (MemberID > 0)
        //    {
        //        if (lvIngredientAllergicList.SelectedIndex != -1)
        //        {
        //            foreach (Ingredient item in lvIngredientAllergicList.SelectedItems)
        //            {
        //                IngredientManager.DeleteAllergic(MemberID, item);
        //            }
        //            FillIngredients(true);
        //            FillIngredients(false);
        //        }
        //        else
        //        {
        //            AlertBox.Show("Please select the ingredient to remove from favourites");
        //        }
        //    }
        //}

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
        
        private void txtNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                CommonFunctions.FilterNumeric(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDecimal_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                CommonFunctions.FilterDecimal(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                CommonFunctions.FilterNumericDate(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            SetMaxLength();
            SetCulture();
            //SetDates();
            
            familyID = MemberManager.GetFamilyID();

            //MemberBasic
            //CommonFunctions.FillFoodCategory(cboFoodCategory);
            FillBloodGroup();
            FillSex();
            FillLifeStyle();
            FillBodyType();
            FillKCalorie();
            
            DisableControls();
                        
            FillData();
            txtMemberName.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData(familyID);
        }       
                
        private void chkLactation_Checked(object sender, RoutedEventArgs e)
        {
            rdLactation1.IsEnabled = true;
            rdLactation2.IsEnabled = true;

            rdLactation1.IsChecked = true;
        }

        private void chkLactation_Unchecked(object sender, RoutedEventArgs e)
        {
            rdLactation1.IsEnabled = false;
            rdLactation2.IsEnabled = false;

            rdLactation1.IsChecked = false;
            rdLactation2.IsChecked = false;
        }

        private void btnAddImage_MouseDown(object sender, MouseButtonEventArgs e)
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
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateWeight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NewWeight objNewWeight = new NewWeight(this);
            objNewWeight.FamilyID = familyID;
            objNewWeight.MemberID = MemberID;
            objNewWeight.Owner = Application.Current.MainWindow;
            objNewWeight.ShowDialog();
        }

        private void lblCalculate_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            CheckBMI();                        
            CheckIBW();            
            CheckDCR();
        }
                        
        private void cbLifeStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string comments = string.Empty;
            App apps = (App)Application.Current;
            if (cbLifeStyle.SelectedIndex == 0)
            {
                recLifestyle.Visibility = Visibility.Hidden;
                comments = string.Empty;
            }
            if (cbLifeStyle.SelectedIndex == 1)
            {
                recLifestyle.Visibility = Visibility.Visible;
                comments = "Sedentary" + System.Environment.NewLine;
                comments = comments + "* At work- You work in an office." + System.Environment.NewLine;
                comments = comments + "* At home- You are sitting ,reading, typing or working at a computer." + System.Environment.NewLine;
                comments = comments + "* Excercise- You don't excercise regularly." ;
            }
            if (cbLifeStyle.SelectedIndex == 2)
            {
                recLifestyle.Visibility = Visibility.Visible;
                comments = "Lightly Active" + System.Environment.NewLine;
                comments = comments + "* At work-you work in an office /you walk a lot."+ System.Environment.NewLine;
                comments = comments + "* At home- you keep yourself busy and move a lot, child care,house cleaning etc."+ System.Environment.NewLine;
                comments = comments + "* Excercise-You participate in light excercise or take slow walks(30 min/ mile), "+ System.Environment.NewLine;
                comments = comments + "  Play tennis/ do gardening.";
            }
            if (cbLifeStyle.SelectedIndex == 3)
            {
                recLifestyle.Visibility = Visibility.Visible;
                comments = "Moderately Active" + System.Environment.NewLine;
                comments = comments + "* At work- you are very active much of the day."+ System.Environment.NewLine;
                comments = comments + "* At home- you rarely sit and do heavy housework or gardening ."+ System.Environment.NewLine;
                comments = comments + "* Excercise - you excercise several times a week, engage in activities"+ System.Environment.NewLine;
                comments = comments + "  Like walking 15 min/mile /carry loads/ cycling /play tennis/dancing.";

            }
            if (cbLifeStyle.SelectedIndex == 4)
            {
                recLifestyle.Visibility = Visibility.Visible;
                comments = "Very Active"+ System.Environment.NewLine;
                comments = comments + "* At work- you are very active much of the day"+ System.Environment.NewLine;
                comments = comments + "* At home - you are vry active with all house hold chores and other vigourous activities."+ System.Environment.NewLine;
                comments = comments + "* Excercise - walking 10 minute/mile or walking with load uphill," + System.Environment.NewLine;
                comments = comments + "  Basketball/ climbing, or Football.";

            }
            if (cbLifeStyle.SelectedIndex == 5)
            {
                recLifestyle.Visibility = Visibility.Visible;
                comments ="Extra Active"+ System.Environment.NewLine;

                comments = comments + "* At work -you hold a labour intensive job /You are very active."+ System.Environment.NewLine;
                comments = comments + "* At home -You do all house hold chores."+ System.Environment.NewLine;
                comments = comments + "* Excercise -Engages in recreational sports/basketball/ climbing, Football";

            }
            txtLifestyleComments.Text = comments;
        }

        private void cbSex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int age = CommonFunctions.Convert2Int(txtAge.Text);
            if (age > 0)
            {
                CheckMajorWomen(age);
            }
            else
            {
                CheckMajorWomen(GetMemberAge());
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

        //private void btnSearchIngredient_Click(object sender, RoutedEventArgs e)
        //{
            //lvIngredientList.ItemsSource = null;
            //lvIngredientList.Items.Refresh();
            //lvIngredientAllergicList.ItemsSource = null;
            //lvIngredientAllergicList.Items.Refresh();
            //if (txtSearchIngredient.Text.Trim() != string.Empty || cboFoodCategory.SelectedIndex > 0)
            //{
            //    FillIngredients(false);
            //    FillIngredients(true);
            //}
            //else
            //{
                
            //}
        //}

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                MemberSearch memberSearch = new MemberSearch();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(memberSearch);
            }
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

        //private void btnAddIngAllergic_Click(object sender, RoutedEventArgs e)
        //{
        //    using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
        //    {
        //        AddIngredientAllergic();
        //    }
        //}

        //private void btnRemIngAllergic_Click(object sender, RoutedEventArgs e)
        //{
        //    using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
        //    {
        //        RemoveIngredientAllergic();
        //    }
        //}

        private void txtSearchIngredient_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        //private void tbMemberProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (tbMemberProfile.SelectedIndex == 1)
        //    {
        //        FillIngredients(true);
        //    }
        //}
             
        #endregion                                        
    }
}

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
using System.Transactions;
using System.Resources;
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Common.Classes;
using NutritionV1.Enums;
using System.Windows.Threading;
using System.Windows.Controls.DataVisualization.Charting;
using Visifire.Charts;
using Visifire.Commons;
using NutritionV1.Constants;
using System.IO;
using Microsoft.Win32;
using System.Configuration;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for SimulateYourFoodSetting.xaml
    /// </summary>
    public partial class SimulateYourFoodSetting : Window
    {
        #region Declarations

        Member member = new Member();

        int LanguageID = (int)ResourceSetting.defaultlanguage;
        public static int memberID;
        public static int familyID;
        double BMI = 0;
        double dExCalorie = 0;

        private int selectedLanguage = (int)ResourceSetting.selectlanguage;

        Visifire.Charts.Chart chart;
        SaveFileDialog SaveDlg = new SaveFileDialog();

        bool IsGoal = false;
        bool IsExercise = false;
        double DailyCalorieInTake = 0;
        private double ExcerciseCalorie = 0;

        #endregion

        #region Constructor

        public SimulateYourFoodSetting()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region Methods

        public int FamilyID
        {
            get
            {
                return familyID;
            }
            set
            {
                familyID = value;
            }
        }

        public int MemberID
        {
            get
            {
                return memberID;
            }
            set
            {
                memberID = value;
            }
        }

        private void Initailize()
        {
            MemberID = Classes.CommonFunctions.Convert2Int(Convert.ToString(cboMember.SelectedValue));
            IsExercise = false;
        }

        private void SetCulture()
        {
            App apps = (App)Application.Current;
            ResourceManager rm = apps.getLanguageList;

            lblExcersiceType.Content = rm.GetString("MemberExercise_ExerciseType");
            lblMinutes1.Content = rm.GetString("MemberExercise_Minutes");
            lblMinutes2.Content = rm.GetString("MemberExercise_Minutes");
            lblMinutes3.Content = rm.GetString("MemberExercise_Minutes");
            lblMinutes4.Content = rm.GetString("MemberExercise_Minutes");
            lblMinutes5.Content = rm.GetString("MemberExercise_Minutes");
            lblMinutes6.Content = rm.GetString("MemberExercise_Minutes");
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            this.Style = (Style)apps.SetStyle["WinStyle"];

            dgMemberExcercise.Style = (Style)apps.SetStyle["WindowStyle"];
        }

        private void FillXMLCombo()
        {
            XMLServices.GetXMLData(cboSimulate, new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\Simulate.xml"),false);
        }

        private void CheckIsGoalExists(int FamilyID, int MemberID)
        {
            List<BONutrition.GoalSetting> goalSettingList = new List<BONutrition.GoalSetting>();
            goalSettingList = GoalSettingManager.GetList(FamilyID, MemberID);

            if (goalSettingList.Count > 0)
            {
                IsGoal = true;
                DailyCalorieInTake = Math.Round(Classes.CommonFunctions.Convert2Double(Convert.ToString(goalSettingList[0].CalorieInTake)), 0);
            }
            else
            {
                IsGoal = false;
                DailyCalorieInTake = Math.Round(CalculateDailyCalorieInTake(MemberID), 0);
            }

        }

        private void CheckIsExercise()
        {
            try
            {
                for (int i = 1; i <= Exercise.EXERCISE_MAXIMUM; i++)
                {
                    ComboBox combo = (ComboBox)this.GetType().InvokeMember("cbExerciseType" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                    TextBox txt = (TextBox)this.GetType().InvokeMember("txtMinutes" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                    if (combo.SelectedIndex > 0 || Classes.CommonFunctions.Convert2Double(Convert.ToString(txt.Text)) != 0)
                    {
                        IsExercise = true;
                        break;
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

        private void FillMember(int FamilyID,int LanguageID)
        {
            try
            {
                List<Member> MemberList = new List<Member>();
                MemberList = MemberManager.GetMemberList(FamilyID);
                if (MemberList != null)
                {
                    Member Member = new Member();
                    //Member.MemberName = Classes.CommonFunctions.GetComboLang(LanguageID);
                    //Member.MemberID = 0;
                    //MemberList.Insert(0, Member);

                    cboMember.DisplayMemberPath = "MemberName";
                    cboMember.SelectedValuePath = "MemberID";
                    cboMember.ItemsSource = MemberList;
                    cboMember.SelectedIndex = 0;
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

        public void SetMemberName(int FamilyID, int MemberID)
        {
            string Name = string.Empty;
            string Sex = string.Empty;
            string Weight = string.Empty;
            string dailyCalorieRequirement = string.Empty;
            string abnormalities = string.Empty;

            List<Member> memberListBasic = new List<Member>();
            List<Member> memberListGeneral = new List<Member>();

            memberListBasic = MemberManager.GetListMember(LanguageID, FamilyID, MemberID);
            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

            lblMemberName.FontSize = 15;

            if (memberListBasic.Count > 0)
            {
                if (Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)).Length > 13)
                {
                    Name = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)).Substring(0, 13) + "..";
                }
                else
                {
                    Name = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID));
                }

                if (memberListBasic[0].SexItem != null)
                {
                    Sex = Convert.ToString(memberListBasic[0].SexItem.SexName) + " ";
                }

                if (memberListGeneral.Count > 0)
                {
                    if (memberListGeneral[0].Weight != 0)
                    {
                        Weight = Convert.ToString(memberListGeneral[0].Weight) + " kg ";
                    }
                    else
                    {
                        Sex = Sex.Replace(" ", "");
                    }
                }

                abnormalities = Classes.CommonFunctions.CalculatePersonalAbnormalitiesNew(FamilyID, MemberID);
                if (abnormalities == string.Empty)
                {
                    Weight = Weight.Replace(" ", "");
                }

                if (Classes.CommonFunctions.CalculateCalorieInTake(FamilyID, MemberID) != 0.0)
                {
                    dailyCalorieRequirement = " Daily Calorie Requirement : " + Convert.ToString(Classes.CommonFunctions.CalculateCalorieInTake(FamilyID, MemberID));
                }

                lblMemberName.Content = Name + " [ " + Sex + Weight + abnormalities + " ]" + dailyCalorieRequirement;
            }
        }

        public int GetMemberAge()
        {
            int MemberAge = 0;
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetListFamilyMember(FamilyID, MemberID);

                if (memberList.Count > 0)
                {
                    DateTime Now = System.DateTime.Now;
                    DateTime DOB = Classes.CommonFunctions.Convert2DateTime(Convert.ToString(Convert.ToDateTime(memberList[0].DOB)));

                    TimeSpan ts = Now.Subtract(DOB);
                    MemberAge = Classes.CommonFunctions.Convert2Int(Convert.ToString(ts.Days)) / (int)DayType.Days_Year;
                }

                return MemberAge;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return MemberAge;
            }
            finally
            {

            }
        }

        public int GetMemberSex()
        {
            int SexID = 0;
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetListFamilyMember(FamilyID, MemberID);

                if (memberList.Count > 0)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberList[0].SexID));

                }
                return SexID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return SexID;
            }
            finally
            {

            }
        }

        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
               
        private bool ValidateSimulation()
        {
            if (cboMember.SelectedIndex < 0)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1260"), "", AlertType.Information, AlertButtons.OK);
                cboMember.Focus();
                return false;
            }
            if (cboSimulate.SelectedIndex < 0)
            {
                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1263"), "", AlertType.Information, AlertButtons.OK);
                cboSimulate.Focus();
                return false;
            }
            if (Math.Round(CalculateDailyCalorieInTake(MemberID), 0) <= 0)
            {
                AlertBox.Show("Please Update your Height and Weight in EditProfile.", "", AlertType.Information, AlertButtons.OK);
                return false;
            }
            if (Math.Round(GetDailyDishCalorie(MemberID) - ExcerciseCalorie, 0) < 1000)
            {
                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1264"), "", AlertType.Information, AlertButtons.OK);
                return false;
            }

            return true;
            
        }

        private bool ValidateCalorie()
        {
            double CalorieInTake = 0, DailyDishCalorie = 0, CalorieFoodDairyExercise = 0, DifferanceCalorie = 0;
            string Message = string.Empty;

            //CalorieInTake = Math.Round(CalculateDailyCalorieInTake(MemberID),0);
            CalorieInTake = DailyCalorieInTake;
            DailyDishCalorie = Math.Round(GetDailyDishCalorie(MemberID),0);
            CalorieFoodDairyExercise = ExcerciseCalorie;

            DifferanceCalorie = Math.Round(((DailyDishCalorie - CalorieFoodDairyExercise) - CalorieInTake), 0);

            if (DifferanceCalorie > 0)
            {
                if (Math.Abs(DifferanceCalorie) > (int)CalorieType.CalorieWeightGain)
                {
                    if (Math.Round(DailyDishCalorie - CalorieFoodDairyExercise, 0) > (int)CalorieType.CalorieUpperLimit)
                    {
                        Message = "Your Simulation Calorie Intake Is Too High ." + System.Environment.NewLine +
                                  "Simulation Is Not Possible.";
                        AlertBox.Show(Message, "", AlertType.Information, AlertButtons.OK);
                    }
                    else
                    {
                        Message = "Your Daily Calorie Requirement is " + Convert.ToString(CalorieInTake) + ".  " + System.Environment.NewLine +
                                 "Your Simulation Calorie Intake is " + Math.Round(DailyDishCalorie - CalorieFoodDairyExercise, 0) + ". " + System.Environment.NewLine +
                                 "It is better to avoid your Simulation Calorie Intake above " + Convert.ToString(CalorieInTake + (int)CalorieType.CalorieWeightGain) + " ." + System.Environment.NewLine +
                                 "Do you want to continue?";
                        return AlertBox.Show(Message, "", AlertType.Information, AlertButtons.YESNO);
                    }
                }
            }
            else if (DifferanceCalorie == 0)
            {
                
            }
            else if (DifferanceCalorie < 0)
            {
                if (Math.Abs(DifferanceCalorie) > (int)CalorieType.CalorieWeightLoss)
                {
                    if (Math.Round(DailyDishCalorie - CalorieFoodDairyExercise, 0) < (int)CalorieType.CalorieLowerLimit)
                    {
                        Message = "Your Simulation Calorie Intake is too Low ." + System.Environment.NewLine +
                                  "Simulation is not possible.";
                        AlertBox.Show(Message, "", AlertType.Information, AlertButtons.OK);
                    }
                    else
                    {
                        Message = "Your Daily Calorie Requirement is " + Convert.ToString(CalorieInTake) + ".  " + System.Environment.NewLine +
                                  "Your Simulation Calorie Intake is " + Math.Round(DailyDishCalorie - CalorieFoodDairyExercise, 0) + ". " + System.Environment.NewLine +
                                  "It is better to avoid your Simulation Calorie Intake below " + Convert.ToString(CalorieInTake - (int)CalorieType.CalorieWeightLoss) + " ." + System.Environment.NewLine +
                                  "Do you want to continue?";
                                  return AlertBox.Show(Message, "", AlertType.Exclamation, AlertButtons.YESNO);
                    }

                    AlertBox.Show(Message, "", AlertType.Information, AlertButtons.OK);
                    return false;
                }
            }

            return true;
        }

        public bool ValidateFoodDairyExercise(int SelectedIndex, int ComboBoxSerailNo)
        {
            if (SelectedIndex != 0)
            {
                for (int i = 1; i <= Exercise.EXERCISE_MAXIMUM; i++)
                {
                    if (i != ComboBoxSerailNo)
                    {
                        ComboBox cb = (ComboBox)this.GetType().InvokeMember("cbExerciseType" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                        TextBox txt = (TextBox)this.GetType().InvokeMember("txtMinutes" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                        if (cb.SelectedIndex == SelectedIndex)
                        {
                            //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1007"), "", AlertType.Error, AlertButtons.OK);
                            AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1007"), "", AlertType.Information, AlertButtons.OK);
                            cb.Focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool ValidateFoodDairyExercise()
        {
            for (int i = 1; i <= Exercise.EXERCISE_MAXIMUM; i++)
            {
                ComboBox cb = (ComboBox)this.GetType().InvokeMember("cbExerciseType" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                TextBox txt = (TextBox)this.GetType().InvokeMember("txtMinutes" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                if (cb.SelectedIndex != 0 && Classes.CommonFunctions.Convert2Int(txt.Text) == 0)
                {
                    //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1034"), "", AlertType.Error, AlertButtons.OK);
                    AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1034"), "", AlertType.Information, AlertButtons.OK);
                    txt.Focus();
                    return false;
                }
                if (cb.SelectedIndex == 0 && Classes.CommonFunctions.Convert2Int(txt.Text) != 0)
                {
                    //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1035"), "", AlertType.Error, AlertButtons.OK);
                    AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1035"), "", AlertType.Information, AlertButtons.OK);
                    cb.Focus();
                    return false;
                }
                if (Classes.CommonFunctions.Convert2Int(txt.Text) > 120)
                {
                    //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1036"), "", AlertType.Error, AlertButtons.OK);
                    AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1145"), "", AlertType.Information, AlertButtons.OK);
                    txt.Focus();
                    return false;
                }
            }
            return true;
        }

        public double CalculateBMI(int FamilyID, int MemberID)
        {
            double BodyMassIndex = 0;
            float Height, Weight, HeightinInches, WeightinPounds = 0;

            try
            {
                List<Member> memberListGeneral = new List<Member>();
                List<BONutrition.GoalSetting> goalSettingList = new List<BONutrition.GoalSetting>();

                memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);
                goalSettingList = GoalSettingManager.GetList(FamilyID, MemberID);

                if (memberListGeneral.Count > 0)
                {
                    Height = Classes.CommonFunctions.Convert2Float(Convert.ToString(memberListGeneral[0].Height));

                    if (goalSettingList.Count > 0)
                    {
                        if (Convert.ToDateTime(goalSettingList[0].EndDate.Date) >= System.DateTime.Now.Date)
                        {
                            Weight = Classes.CommonFunctions.Convert2Float(Convert.ToString(goalSettingList[0].PresentWeight));
                        }
                    }
                    else
                    {

                        Weight = Classes.CommonFunctions.Convert2Float(Convert.ToString(memberListGeneral[0].Weight));
                    }

                    HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString((memberListGeneral[0].Height) * CalorieFormula.CM_INCH));
                    WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString((memberListGeneral[0].Weight) * CalorieFormula.KG_POUND));

                    if (HeightinInches != 0 && WeightinPounds != 0)
                    {
                        BodyMassIndex = Math.Round((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) / (Math.Pow((HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)), 0);
                    }
                }
                return BodyMassIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (0);
            }
            finally
            {

            }
        }

        public double CalculateDailyCalorieInTake(int MemberID)
        {
            int SexID, Age, LifeStyleID, LactationType;
            float HeightinInches, WeightinPounds, LifeStyleValue = 0;
            double CalorieRequired = 0, ExerciseCalorie = 0;
            double Height = 0; double Weight = 0;
            bool IsPregnant, IsLactation;

            List<Member> memberList = new List<Member>();
            List<Member> memberListGeneral = new List<Member>();
            memberList = MemberManager.GetListFamilyMember(FamilyID, MemberID);
            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

            try
            {
                if (memberList.Count > 0)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberList[0].SexID));
                    Age = GetMemberAge();
                    LifeStyleID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberList[0].LifeStyleID));
                    IsPregnant = Convert.ToBoolean(Convert.ToString(memberList[0].Pregnancy));
                    IsLactation = Convert.ToBoolean(Convert.ToString(memberList[0].Lactation));
                    LactationType = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberList[0].LactationType));

                    if (memberListGeneral.Count > 0)
                    {
                        Height = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Height));
                        Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight));

                        if (Height > 0 && Weight > 0)
                        {
                            HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(Height * CalorieFormula.CM_INCH));
                            WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString(Weight * CalorieFormula.KG_POUND));

                            if (HeightinInches != 0 && WeightinPounds != 0)
                            {
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
                                    if (Age > 18)
                                    {
                                        CalorieRequired = Math.Round((((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEWEIGHT_MEN))) + (HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEHEIGHT_MEN))) + Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIECOMMON_MEN))) - Age * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEAGE_MEN))) * LifeStyleValue, 0);
                                    }
                                    else
                                    {
                                        CalorieRequired = Math.Round(CalculateChildrenCalorie(Age, SexID, Classes.CommonFunctions.Convert2Double(Convert.ToString((memberListGeneral[0].Weight)))), 0);
                                    }
                                }
                                else if (SexID == 2)
                                {
                                    if (Age > 18)
                                    {
                                        CalorieRequired = Math.Round((((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEWEIGHT_WOMEN))) + (HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEHEIGHT_WOMEN))) + Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIECOMMON_WOMEN))) - Age * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEAGE_WOMEN))) * LifeStyleValue, 0);
                                    }
                                    else
                                    {
                                        CalorieRequired = Math.Round(CalculateChildrenCalorie(Age, SexID, Classes.CommonFunctions.Convert2Double(Convert.ToString((memberListGeneral[0].Weight)))), 0);
                                    }

                                    if (IsPregnant)
                                    {
                                        CalorieRequired = CalorieRequired + (int)CalorieType.CaloriePregnancy;
                                    }
                                    if (IsLactation)
                                    {
                                        if (LactationType == 1)
                                            CalorieRequired = CalorieRequired + (int)CalorieType.CalorieLactation1;
                                        else if (LactationType == 2)
                                            CalorieRequired = CalorieRequired + (int)CalorieType.CalorieLactation2;
                                    }
                                }

                                //ExerciseCalorie = CalculateDailyExerciseCalorie(SexID, FamilyID, MemberID);
                                ExerciseCalorie = CalculateDailyExerciseCalorie(Weight, FamilyID, MemberID);

                                //CalorieRequired = CalorieRequired + ExerciseCalorie;
                            }
                        }
                    }
                }

                return CalorieRequired;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (0);
            }
            finally
            {

            }
        }

        public double CalculateChildrenCalorie(int Age, int SexID, double Weight)
        {
            int EnergyRequired = 0;
            double CalorieRequired = 0, MeanWeight = 0, EnergyPerKg = 0;

            try
            {
                if (Age > 0)
                {
                    EnergyRequired = CalorieCalculatorManager.GetEnergyRequirement(Age, SexID);
                    MeanWeight = CalorieCalculatorManager.GetMeanWeight(Age, SexID);

                    if (EnergyRequired != 0 && MeanWeight != 0)
                    {
                        EnergyPerKg = EnergyRequired / MeanWeight;
                        CalorieRequired = EnergyPerKg * Weight;
                    }
                }

                return CalorieRequired;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return CalorieRequired;
            }
            finally
            {

            }
        }

        public double CalculateDailyExerciseCalorie(int SexID, int FamilyID, int MemberID)
        {
            int ExerciseID, ExerciseDuration, ExerciseTypeID;
            double CalorieRequired = 0;

            try
            {
                List<Member> exerciseList = new List<Member>();
                exerciseList = MemberManager.GetListFamilyExercise(FamilyID, MemberID);

                if (exerciseList.Count > 0)
                {
                    if (SexID == 1)
                    {
                        for (int i = 1; i <= exerciseList.Count; i++)
                        {
                            ExerciseID = Classes.CommonFunctions.Convert2Byte(Convert.ToString(exerciseList[i - 1].ExerciseID));

                            if (ExerciseID > 0)
                            {
                                ExerciseDuration = Classes.CommonFunctions.Convert2Int(Convert.ToString(exerciseList[i - 1].ExerciseDuration));

                                ExerciseTypeID = MemberManager.GetExerciseType(ExerciseID);

                                switch (ExerciseTypeID)
                                {
                                    case (int)ExerciseType.Easy:
                                        CalorieRequired = CalorieRequired + (ExerciseFormula.EASY_MEN * ExerciseDuration);
                                        break;
                                    case (int)ExerciseType.Moderate:
                                        CalorieRequired = CalorieRequired + (ExerciseFormula.MODERATE_MEN * ExerciseDuration);
                                        break;
                                    case (int)ExerciseType.Hard:
                                        CalorieRequired = CalorieRequired + (ExerciseFormula.HARD_MEN * ExerciseDuration);
                                        break;
                                }
                            }
                        }
                    }
                    else if (SexID == 2)
                    {
                        for (int i = 1; i <= exerciseList.Count; i++)
                        {
                            ExerciseID = Classes.CommonFunctions.Convert2Byte(Convert.ToString(exerciseList[i - 1].ExerciseID));

                            if (ExerciseID > 0)
                            {
                                ExerciseDuration = Classes.CommonFunctions.Convert2Int(Convert.ToString(exerciseList[i - 1].ExerciseDuration));

                                ExerciseTypeID = MemberManager.GetExerciseType(ExerciseID);

                                switch (ExerciseTypeID)
                                {
                                    case (int)ExerciseType.Easy:
                                        CalorieRequired = CalorieRequired + (ExerciseFormula.EASY_WOMEN * ExerciseDuration);
                                        break;
                                    case (int)ExerciseType.Moderate:
                                        CalorieRequired = CalorieRequired + (ExerciseFormula.MODERATE_WOMEN * ExerciseDuration);
                                        break;
                                    case (int)ExerciseType.Hard:
                                        CalorieRequired = CalorieRequired + (ExerciseFormula.HARD_WOMEN * ExerciseDuration);
                                        break;
                                }
                            }
                        }
                    }
                }

                return CalorieRequired;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return CalorieRequired;
            }
            finally
            {

            }
        }

        public static double CalculateDailyExerciseCalorie(double Weight, int FamilyID, int MemberID)
        {
            int ExerciseID, ExerciseDuration;
            double ExerciseCalorie = 0;

            try
            {
                List<Member> exerciseList = new List<Member>();
                exerciseList = MemberManager.GetListFamilyExercise(FamilyID, MemberID);

                if (exerciseList.Count > 0)
                {
                    for (int i = 1; i <= exerciseList.Count; i++)
                    {
                        ExerciseID = Classes.CommonFunctions.Convert2Byte(Convert.ToString(exerciseList[i - 1].ExerciseID));

                        if (ExerciseID > 0)
                        {
                            ExerciseDuration = Classes.CommonFunctions.Convert2Int(Convert.ToString(exerciseList[i - 1].ExerciseDuration));

                            ExerciseCalorie = ExerciseCalorie + (MemberManager.GetExerciseCalorie(ExerciseID, Math.Round(Classes.CommonFunctions.Convert2Double(Convert.ToString(Weight * CalorieFormula.KG_POUND)), 0)) / 30) * ExerciseDuration;
                        }
                    }
                }

                return ExerciseCalorie;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ExerciseCalorie;
            }
            finally
            {

            }
        }

        private double GetDailyDishCalorie(int MemberID)
        {
            App apps = (App)Application.Current;
            int Days = 0;
            double Calorie = 0;

            Calorie = MemberManager.GetDishCalorie(MemberID, Classes.CommonFunctions.Convert2Int(Convert.ToString(apps.FoodSettingDate.Month)), Classes.CommonFunctions.Convert2Int(Convert.ToString(apps.FoodSettingDate.Year)));
            Days = MemberManager.GetDishDays(MemberID, Classes.CommonFunctions.Convert2Int(Convert.ToString(apps.FoodSettingDate.Month)), Classes.CommonFunctions.Convert2Int(Convert.ToString(apps.FoodSettingDate.Year)));

            if (Calorie > 0 && Days > 0)
            {
                Calorie = (Calorie / Days);
            }

            return Calorie;
        }

        private double CalculateSimulatedWeight(int MemberID)
        {
            int DaysInMonth;
            double Weight = 0, SimulatedWeight = 0;
            double Calorie = 0, SimulatedCalorie = 0;

            List<Member> memberListGeneral = new List<Member>();
            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

            DaysInMonth = DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);

            Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight));
            //Calorie = ((GetDailyDishCalorie(MemberID) * DaysInMonth) - ((CalculateDailyCalorieInTake(MemberID) * DaysInMonth)));
            Calorie = ((GetDailyDishCalorie(MemberID) * DaysInMonth) - ((DailyCalorieInTake * DaysInMonth) + (ExcerciseCalorie * DaysInMonth)));

            SimulatedCalorie = Math.Abs((Calorie) / DaysInMonth) / (int)CalorieType.CalorieSimulation;

            if (SimulatedCalorie > 0)
            {
                if (BMI > BodyMassIndex.BMIMAXIMUM)
                {
                    SimulatedWeight = SimulatedCalorie * (int)BMIWeightType.BMIAbove;
                }
                else
                {
                    SimulatedWeight = SimulatedCalorie * (int)BMIWeightType.BMIBelow;
                }
            }

            return SimulatedWeight;
        }

        private int CalculateSimulationType(int MemberID)
        {
            int DaysInMonth, SimulationType = 2;
            double Weight = 0, Calorie = 0;

            List<Member> memberListGeneral = new List<Member>();
            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

            DaysInMonth = DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);

            Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight));
            //Calorie = ((GetDailyDishCalorie(MemberID) * DaysInMonth) - ((CalculateDailyCalorieInTake(MemberID) * DaysInMonth)));
            Calorie = ((GetDailyDishCalorie(MemberID) * DaysInMonth) - (DailyCalorieInTake * DaysInMonth + (ExcerciseCalorie * DaysInMonth)));

            if (Calorie > 0)
            {
                SimulationType = (int)GoalType.WeightGain;
            }
            else if (Calorie == 0)
            {
                SimulationType = (int)GoalType.WeightGain;
            }
            else if (Calorie < 0)
            {
                SimulationType = (int)GoalType.WeightLoss;
            }

            return SimulationType;
        }

        private void FillComments(int MemberID)
        {
            double CalorieInTake = 0, DishCalorie = 0, FoodDairyExerciseCalorie = 0;

            List<BONutrition.GoalSetting> goalSettingList = new List<BONutrition.GoalSetting>();

            if (MemberID != 0)
            {
                goalSettingList = GoalSettingManager.GetList(FamilyID, MemberID);

                //CalorieInTake = Math.Round(CalculateDailyCalorieInTake(MemberID), 0);
                CalorieInTake = DailyCalorieInTake;
                DishCalorie = Math.Round(GetDailyDishCalorie(MemberID), 0);
                FoodDairyExerciseCalorie = ExcerciseCalorie;

                if (goalSettingList.Count > 0)
                {
                    txtComments.Text = "Your Daily Calorie Intake to achieve your Goal is " + Math.Round(CalorieInTake, 0) + ".  " + System.Environment.NewLine + 
                                        "Your Daily Calorie Intake in Food Setting is " + Math.Round(DishCalorie - FoodDairyExerciseCalorie, 0) + ".  " +
                                       "And we are going to Simulate your Health Condition using " + Math.Round(DishCalorie - FoodDairyExerciseCalorie, 0) + " Calorie.";
                }
                else
                {
                    txtComments.Text = "Your Daily Calorie Intake in Food Setting is " + Math.Round(DishCalorie - FoodDairyExerciseCalorie, 0) + ".  " +
                                       "And we are going to Simulate Your Health Condition using " + Math.Round(DishCalorie - FoodDairyExerciseCalorie, 0) + " Calorie.";
                }
            }
        }

        private void FillExercise(int LanguageID)
        {
            try
            {
                List<SysAdmin> adminList = new List<SysAdmin>();
                //adminList = SysAdminManager.GetExercise(LanguageID);
                adminList = SysAdminManager.GetExercise();
                if (adminList != null)
                {
                    SysAdmin admin = new SysAdmin();
                    admin.ExerciseName = Classes.CommonFunctions.GetComboLang(LanguageID);
                    admin.ExerciseID = 0;
                    adminList.Insert(0, admin);

                    for (int i = 1; i <= Exercise.EXERCISE_MAXIMUM; i++)
                    {
                        ComboBox cb = (ComboBox)this.GetType().InvokeMember("cbExerciseType" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                        cb.DisplayMemberPath = "ExerciseName";
                        cb.SelectedValuePath = "ExerciseID";
                        cb.ItemsSource = adminList;
                        cb.SelectedIndex = 0;
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

        private double CalculateWeightLimit(double Weight,int SimulationType)
        {
            double BornWeight = 0;

            if (SimulationType == (int)GoalType.WeightLoss)
            {
                BornWeight = Weight - ((Weight * (int)WeightLimit.WeightMaximum) / (int)PercentageType.Percentage_Normal);
            }
            else if (SimulationType == (int)GoalType.WeightGain)
            {
                BornWeight = Weight + ((Weight * (int)WeightLimit.WeightMaximum) / (int)PercentageType.Percentage_Normal);
            }

            return BornWeight;
        }

        public double CalculateFoodDairyExerciseCalorie(double Weight)
        {
            int ExerciseID, ExerciseDuration;
            double ExerciseCalorie = 0;

            try
            {
                for (int i = 1; i <= Exercise.EXERCISE_MAXIMUM; i++)
                {
                    ComboBox combo = (ComboBox)this.GetType().InvokeMember("cbExerciseType" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                    if (ValidateFoodDairyExercise(combo.SelectedIndex, i) && ValidateFoodDairyExercise())
                    {
                        ComboBox cb = (ComboBox)this.GetType().InvokeMember("cbExerciseType" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                        TextBox txt = (TextBox)this.GetType().InvokeMember("txtMinutes" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                        if (cb.SelectedIndex > 0)
                        {

                            ExerciseID = Classes.CommonFunctions.Convert2Byte(Convert.ToString(cb.SelectedIndex));

                            if (ExerciseID > 0)
                            {
                                ExerciseDuration = Classes.CommonFunctions.Convert2Int(txt.Text.Trim());

                                ExerciseCalorie = ExerciseCalorie + (MemberManager.GetExerciseCalorie(ExerciseID, Math.Round(Classes.CommonFunctions.Convert2Double(Convert.ToString(Weight * CalorieFormula.KG_POUND)), 0)) / 30) * ExerciseDuration;
                            }
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }

                return ExerciseCalorie;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ExerciseCalorie;
            }
            finally
            {

            }
        }

        public double GetMemberWeight()
        {
            double Weight = 0;
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

                if (memberList.Count > 0)
                {
                    Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberList[0].Weight));

                }
                return Weight;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Weight;
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

        private void txtDecimal_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Classes.CommonFunctions.FilterDecimal(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            SetCulture();
            FillXMLCombo();
            FillMember(FamilyID,LanguageID);
            cboMember.SelectedIndex = 0;
            cboSimulate.SelectedIndex = 0;
            FillExercise(LanguageID);
            Initailize();
            CheckIsGoalExists(FamilyID, MemberID);
            SetMemberName(FamilyID, MemberID);
            BMI = CalculateBMI(FamilyID, MemberID);
            FillComments(MemberID);

            CreateGraph(memberID, 0);
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnSimulate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MemberID != 0)
            {
                if (ValidateSimulation() == true)
                {
                    Initailize();
                    ExcerciseCalorie = Math.Round(CalculateFoodDairyExerciseCalorie(GetMemberWeight()), 0);
                    dExCalorie = ExcerciseCalorie;
                    txtExcercise.Text = "Calorie Burned during given Exercises is : ";
                    txtExcerciseComments.Text = ExcerciseCalorie + " Calorie";
                    CheckIsExercise();
                    FillComments(MemberID);
                    CreateGraph(MemberID, cboSimulate.SelectedIndex + 1);
                }
            }
            else
            {
                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1260"), "", AlertType.Information, AlertButtons.OK);
            }
        }

        private void lblExcerciseComments_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MemberID != 0)
            {
                //ExcerciseCalorie = Math.Round(CalculateFoodDairyExerciseCalorie(Classes.CommonFunctions.Convert2Int(Convert.ToString(GetMemberSex()))), 0);
                ExcerciseCalorie = Math.Round(CalculateFoodDairyExerciseCalorie(GetMemberWeight()), 0);
                txtExcercise.Text = "Calorie Burned during given Exercises is : ";
                txtExcerciseComments.Text = ExcerciseCalorie + " Calorie";

                FillComments(MemberID);
            }
            else
            {
                AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1260"), "", AlertType.Information, AlertButtons.OK);
            }
        }

        private void cboMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboMember.SelectedIndex >= 0)
                {
                    Initailize();
                    CheckIsGoalExists(FamilyID, MemberID);
                    SetMemberName(FamilyID, MemberID);
                    FillComments(MemberID);
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

        private void lblHelp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Help help = new Help();
            help.HelpItemID = (int)HelpType.FoodSettingSimulation;
            help.Owner = Application.Current.MainWindow;
            help.ShowDialog();
        }

        private void lblTips_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Tips tips = new Tips();
            tips.TipsItemID = (int)FlowType.FoodSettingSimulation;
            tips.Owner = Application.Current.MainWindow;
            tips.ShowDialog();
        }

        #endregion

        #region Graph

        public void CreateGraph(int MemberID, int SimulationPeriod)
        {
            int Month = 0, SimulationType =2;
            double Weight = 0, SimulatedWeight = 0;
            int Period = 0;

            chart = new Visifire.Charts.Chart();
            chart.Cursor = Cursors.Hand;
            chart.ScrollingEnabled = false;

            Title header = new Title();
            header.FontSize = 13;
            header.FontWeight = FontWeights.Bold;
            header.FontStyle = FontStyles.Italic;
            header.VerticalAlignment = VerticalAlignment.Top;
            header.HorizontalAlignment = HorizontalAlignment.Left;
            header.Text = Convert.ToString(ConfigurationManager.AppSettings["ProductName"]);
            chart.Titles.Add(header);

            Title footer = new Title();
            footer.FontSize = 10;
            footer.FontWeight = FontWeights.Bold;
            footer.VerticalAlignment = VerticalAlignment.Bottom;
            footer.HorizontalAlignment = HorizontalAlignment.Right;
            footer.Text = "Indocosmo Systems Pvt Ltd";
            chart.Titles.Add(footer);

            Title title = new Title();
            title.Text = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + " - FoodSetting Simulation";
            chart.Titles.Add(title);

            Visifire.Charts.Axis axisX = new Visifire.Charts.Axis();
            axisX.Title = "Simulation Period";
            axisX.Interval = Convert.ToDouble("1.0");
            axisX.IntervalType = IntervalTypes.Months;
            axisX.Suffix = " Month";
            chart.AxesX.Add(axisX);

            Visifire.Charts.Axis axisY = new Visifire.Charts.Axis();
            axisY.Title = "Weight";
            axisY.Interval = Convert.ToDouble("50.0");
            axisY.IntervalType = IntervalTypes.Number;
            axisY.Suffix = " Kg";
            chart.AxesY.Add(axisY);

            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = RenderAs.Line;
            dataSeries.ToolTipText = "Month : #XValue \n Weight : #YValue";

            List<Member> memberListGeneral = new List<Member>();
            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

            if (SimulationPeriod > 0)
            {
                Period = SimulationPeriod * 12;
                Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight));

                if (IsExercise == true)
                {
                    if (Weight > 0 && dExCalorie > 0)
                    {
                        if (ValidateCalorie() == true)
                        {
                            SimulationType = CalculateSimulationType(MemberID);
                            SimulatedWeight = CalculateSimulatedWeight(MemberID);

                            Visifire.Charts.DataPoint dataPoint;

                            for (int i = 0; i <= Period; i++)
                            {
                                dataPoint = new Visifire.Charts.DataPoint();

                                dataPoint.XValue = Month;

                                if (SimulationType == (int)GoalType.WeightLoss)
                                {
                                    dataPoint.YValue = Weight;
                                    Weight = Weight - CalculateSimulatedWeight(MemberID);

                                    if (Weight < CalculateWeightLimit(Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight)), SimulationType))
                                    {
                                        break;
                                    }
                                }
                                else if (SimulationType == (int)GoalType.MaintainWeight)
                                {
                                    dataPoint.YValue = Weight;
                                }
                                else if (SimulationType == (int)GoalType.WeightGain)
                                {
                                    dataPoint.YValue = Weight;
                                    Weight = Weight + CalculateSimulatedWeight(MemberID);

                                    if (Weight > CalculateWeightLimit(Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight)), SimulationType))
                                    {
                                        break;
                                    }
                                }

                                dataSeries.DataPoints.Add(dataPoint);
                                Month = Month + 1;

                            }
                        }
                    }
                }
                else
                {
                    if (Weight > 0)
                    {
                        if (ValidateCalorie() == true)
                        {
                            SimulationType = CalculateSimulationType(MemberID);
                            SimulatedWeight = CalculateSimulatedWeight(MemberID);

                            Visifire.Charts.DataPoint dataPoint;

                            for (int i = 0; i <= Period; i++)
                            {
                                dataPoint = new Visifire.Charts.DataPoint();

                                dataPoint.XValue = Month;

                                if (SimulationType == (int)GoalType.WeightLoss)
                                {
                                    dataPoint.YValue = Weight;
                                    Weight = Weight - CalculateSimulatedWeight(MemberID);

                                    if (Weight < CalculateWeightLimit(Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight)), SimulationType))
                                    {
                                        break;
                                    }
                                }
                                else if (SimulationType == (int)GoalType.MaintainWeight)
                                {
                                    dataPoint.YValue = Weight;
                                }
                                else if (SimulationType == (int)GoalType.WeightGain)
                                {
                                    dataPoint.YValue = Weight;
                                    Weight = Weight + CalculateSimulatedWeight(MemberID);

                                    if (Weight > CalculateWeightLimit(Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight)), SimulationType))
                                    {
                                        break;
                                    }
                                }

                                dataSeries.DataPoints.Add(dataPoint);
                                Month = Month + 1;

                            }
                        }
                    }
                }
            }

            chart.Series.Add(dataSeries);
            GraphLayout.Children.Add(chart);

            chart.MouseLeftButtonDown += new MouseButtonEventHandler(chart_MouseLeftButtonDown);
        }

        void chart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //string FileName = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + "-FoodSettingSimulation";

            //FileName = SaveDialog("Save Food-Setting Simulation Chart", "PNG File (*.png)|*.png", FileName + ".png", true);

            //if (FileName == string.Empty)
            //{
            //    return;
            //}
            //else
            //{
            //    ExportToPng(new Uri(FileName), (int)GraphLayout.Width, (int)GraphLayout.Height, chart);
            //}

        }

        public void ExportToPng(Uri Path, int Width, int Height, Visifire.Charts.Chart Surface)
        {
            if (Path == null)
            {
                return;
            }

            Transform Transform = Surface.LayoutTransform;
            Surface.LayoutTransform = null;
            Surface.Width = Width;
            Surface.Height = Height;

            RenderTargetBitmap RenderBitmap = new RenderTargetBitmap((int)Surface.Width, (int)Surface.Height, 96d, 96d, PixelFormats.Pbgra32);
            RenderBitmap.Render(Surface);

            using (FileStream outStream = new FileStream(Path.LocalPath, FileMode.Create))
            {
                PngBitmapEncoder Encoder = new PngBitmapEncoder();
                Encoder.Frames.Add(BitmapFrame.Create(RenderBitmap));
                Encoder.Save(outStream);
            }

            Surface.LayoutTransform = Transform;

            AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1193"), "", AlertType.Information, AlertButtons.OK);
        }

        public string SaveDialog(string Title, string Filter, string FileName, bool IsOverWrite)
        {
            SaveDlg.Title = Title;
            SaveDlg.Filter = Filter;
            SaveDlg.FilterIndex = 1;
            SaveDlg.OverwritePrompt = IsOverWrite;
            SaveDlg.FileName = FileName;
            if (SaveDlg.ShowDialog() == true)
            {
                return (SaveDlg.FileName);
            }
            else
            {
                return ("");
            }
        }

        #endregion

        private void lblSaveChart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string FileName = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + "-FoodSettingSimulation";

            FileName = SaveDialog("Save Food-Setting Simulation Chart", "PNG File (*.png)|*.png", FileName + ".png", true);

            if (FileName == string.Empty)
            {
                return;
            }
            else
            {
                ExportToPng(new Uri(FileName), (int)GraphLayout.Width, (int)GraphLayout.Height, chart);
            }
        }

               
       
    }
}

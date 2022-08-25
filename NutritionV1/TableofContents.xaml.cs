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
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using Nutrition.Common.Classes;
using Nutrition.Enums;
using Nutrition.Constants;
using System.IO;

namespace Nutrition
{
    /// <summary>
    /// Interaction logic for TableofContents.xaml
    /// </summary>
    public partial class TableofContents : Page
    {
        #region Declarations

        int LanguageID = 1;
        App apps = (App)Application.Current;

        #endregion

        public TableofContents()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #region Methods

        private void ApplyStyle(int menuCount, int selectIndex)
        {
            for (int i = 1; i <= menuCount; i++)
            {
                MenuItem mnuItem = (MenuItem)this.GetType().InvokeMember("mnuItem" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                if (i == selectIndex)
                {
                    mnuItem.Style = (Style)apps.SetStyle["MenuLeftSelectStyle"];
                }
                else
                {
                    mnuItem.Style = (Style)apps.SetStyle["MenuLeftItemStyle"];
                }
            }
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            //grdTableofContents.Style = (Style)apps.SetStyle["WindowStyle"];
            ReferanceLeftMenu.Style = (Style)apps.SetStyle["WindowStyle"];
            lblItems.Style = (Style)apps.SetStyle["LabelStyle"];

            ApplyStyle(16, 0);
        }

        private void FillXMLCombo()
        {
            XMLServices.GetXMLData(cbReferanceItems, new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\Referance.xml"),false);
        }

        public void FillData(string ContentHeader, int ContentType)
        {
            try
            {
                //lblHeader.Content = ContentHeader;
                svContent.Content = null;

                switch (ContentType)
                {
                    case (int)Enums.TableofContents.Know_Calorie:

                        FillCalorieImpact();
                        break;
                    //case (int)Enums.TableofContents.BMI_Impact:

                    //    FillBMIImpact();
                    //    break;
                    case (int)Enums.TableofContents.Adolescents_Height_Weight:

                        FillAdolescentsHeightWeightRatio();
                        break;
                    case (int)Enums.TableofContents.Baby_Height_Weight:

                        FillBabyHeightWeightRatio();
                        break;
                    case (int)Enums.TableofContents.Baby_Immunization:

                        FillImmunization();
                        break;
                    case (int)Enums.TableofContents.Ideal_Weight:

                        FillIdealWeight();
                        break;
                    case (int)Enums.TableofContents.Energy_Requirement:

                        FillEnergyRequirement();
                        break;
                    case (int)Enums.TableofContents.Mean_SE_Value_of_Height:

                        FillMeanHeight();
                        break;
                    case (int)Enums.TableofContents.Mean_SE_Value_of_Weight:

                        FillMeanWeight();
                        break;
                    case (int)Enums.TableofContents.Children_Height_Weight:

                        FillChildrenHeightWeightRatio();
                        break;
                    case (int)Enums.TableofContents.Baby_Balanced_Diet:

                        FillBabyBalancedDiet();
                        break;
                    case (int)Enums.TableofContents.Children_Balanced_Diet:

                        FillChildrenBalancedDiet();
                        break;
                    case (int)Enums.TableofContents.Adolescents_Balanced_Diet:

                        FillAdolescentsBalancedDiet();
                        break;
                    case (int)Enums.TableofContents.Elderly_Balanced_Diet:

                        FillElderlyBalancedDiet();
                        break;
                    case (int)Enums.TableofContents.Nutrient_Source:

                        FillNutrientSource();
                        break;
                    case (int)Enums.TableofContents.Pregnancy_Balanced_Diet:

                        FillPregnancyBalancedDiet();
                        break;
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

        public void FillCalorieImpact()
        {
            try
            {
                txtFewWords.Text = "Your daily calorie need is the amount of calorie you need to eat each day in order to maintain your current weight. " +
                                    "The calories estimated should be supplied through the macro nutrients; carbohydrate, protein and fat. " +
                                    "Calorie requirement vary from person to person depending on the age, sex, height, weight, lifestyle and environment in which you are living.";
                MakeCalorieDynamicGrid();
                txtReferance.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        
        public void FillBMIImpact()
        {
            try
            {
                txtFewWords.Text = "BMI is a gross estimation of fat in your body. The formulae used for calculating BMI =  weight in Kg/height in m2";
                MakeBMIDynamicGrid();
                txtReferance.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillImmunization()
        {
            try
            {
                txtFewWords.Text = "Immunization is a way of protecting the human body against infectious diseases through vaccination. Immunization prepares our bodies to fight against diseases in case we come into contact with them in the future. " +
                                    "Babies are born with some natural immunity which they get from their mother and through breast-feeding. " + 
                                    "This gradually wears off as the baby's own immune system starts to develop. Having your child immunized gives extra protection against illness.";
                MakeImmunizationDynamicGrid();
                txtReferance.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillBabyHeightWeightRatio()
        {
            try
            {
                txtFewWords.Text = "The anthropometric data given below are based on a study on a part of Indian population and do not have an all Indian character .These data are given for reference purpose only. This should not be taken as medical advice.";
                MakeBabyHWDynamicGrid();
                txtReferance.Text = "Source :- NATIONAL CENTRE FOR HEALTH STATISTICS (NCHS- 1987).";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillIdealWeight()
        {
            try
            {
                txtFewWords.Text = "Age and body weight largely determine the nutrient requirements of an individual. Body weights and heights of children reflect their state of health and growth rate, while adult weight and height represent what can be attained by an individual with normal growth.";
                MakeIdealWeightDynamicGrid();
                txtReferance.Text = "Source: - RECOMMENDED DIETARY INTAKES FOR INDIANS, ICMR (1978).";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillAdolescentsHeightWeightRatio()
        {
            try
            {
                txtFewWords.Text = "The anthropometric data given below are based on a study on a part of Indian population and do not have an all Indian character .These data are given for reference purpose only. This should not be taken as medical advice.";
                MakeAdolescentsHWDynamicGrid();
                txtReferance.Text = "Source :- NATIONAL CENTRE FOR HEALTH STATISTICS (NCHS- 1987).";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillEnergyRequirement()
        {
            try
            {
                txtFewWords.Text = " The energy requirements of the human body vary from person to person. "+
                                    "The three factors which are used to compute energy requirements are basal or resting metabolic rate, " + 
                                    "thermal effect of food and physical activity. 60-70% of calories are burned during resting, 10% are expended for digestion and absorption, " + 
                                    "20 to 30% energy are expended through activities. So these calories burned should be supplied through calorie intake.";
                MakeEnergyRequirementDynamicGrid();
                txtReferance.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillMeanHeight()
        {
            try
            {
                txtFewWords.Text = "Age and body weight largely determine the nutrient requirements of an individual. " + 
                                    "Body weights and heights of children reflects their state of health and growth rate, while adult weight and height represents what can be attained by an individual with normal growth. " +
                                    "In recommending nutrient intake, desirable height and weight of both children and adult rather than the prevailing one are considered necessarily.";
                MakeMeanHeightDynamicGrid();
                txtReferance.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillMeanWeight()
        {
            try
            {
                txtFewWords.Text = "Age and body weight largely determine the nutrient requirements of an individual. " +
                                    "Body weights and heights of children reflects their state of health and growth rate, while adult weight and height represents what can be attained by an individual with normal growth. " +
                                    "In recommending nutrient intake, desirable height and weight of both children and adult rather than the prevailing one are considered necessarily.";
                MakeMeanWeightDynamicGrid();
                txtReferance.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillChildrenHeightWeightRatio()
        {
            try
            {
                txtFewWords.Text = "The anthropometric data given below are based on a study on a part of Indian population and do not have an all Indian character .These data are given for reference purpose only. This should not be taken as medical advice.";
                MakeChildrenHWDynamicGrid();
                txtReferance.Text = "Source :- NATIONAL CENTRE FOR HEALTH STATISTICS (NCHS- 1987).";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillBabyBalancedDiet()
        {
            try
            {
                txtFewWords.Text = "Breast-milk alone is not adequate for the infant beyond 4-6 months of age. " +
                                    "Introduction of food suppliments along with breast-feeding is necessary to infants by 4-6 months of age. " +
                                    "Provision of adequate and appropriate suppliments to young children prevents malnutrition. " +
                                    "Hygienic practices should be observed while preparing and feeding the weaning food to the child, otherwise, it will lead to diarrhoea.";
                MakeBabyBalancedDietDynamicGrid();
                txtReferance.Text = "Dietary guidelines for Indians, ICMR, 1995.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillChildrenBalancedDiet()
        {
            try
            {
                txtFewWords.Text = "A nutritionally adequate diet is essential for optimal growth and development. " +
                                    "Appropriate diet during childhood may reduce the risk of diet-related chronic diseases in future. " +
                                    "Common infections and malnutrition contributes significantly to child morbidity and mortality. " +
                                    "A child needs to eat more during and after episodes of infections to maintain good nutritional status.";
                MakeChildrenBalancedDietDynamicGrid();
                txtReferance.Text = "Dietary guidelines for Indians, ICMR, 1995.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillAdolescentsBalancedDiet()
        {
            try
            {
                txtFewWords.Text = "A nutritionally adequate diet is essentail for optimal growth and development. " +
                                   "Appropriate diet during childhood may reduce the risk of diet-related chronic diseases in future. " +
                                   "Common infections and malnutrition contributes significantly to child morbidity and mortality. " +
                                   "A child needs to eat more during and after episodes of infections to maintain good nutritional status.";
                MakeAdolescentsBalancedDietDynamicGrid();
                txtReferance.Text = "Dietary guidelines for Indians, ICMR, 1995.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillElderlyBalancedDiet()
        {
            try
            {
                txtFewWords.Text = "Nutrient requirements have been worked out according to age, sex, physical activity and physiological status. " +
                                    "Considering the lower physical activity during old age, the requirement for energy is expected to be 10-11% less than that of adults, with a little difference in other nutrients. " +
                                    "Accordingly, balanced diets for elders are worked out based on the recommended Dietary Allowances for Indian Adults.";
                MakeElderlyBalancedDietDynamicGrid();
                txtReferance.Text = "Dietary guidelines for Indians, ICMR, 1995.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillPregnancyBalancedDiet()
        {
            try
            {
                txtFewWords.Text = "Pregnancy is physiologically and nutritionally a highly demanding period. Extra food is required to satisfy the needs of the foetus. " +
                                    "A women prepares herself to meet the nutritional demands by increasing her own body fat deposits during pregnancy. " +
                                    "A lactating mother requires extra food to secrete adequate quantities of milk and to safeguard her own health.";
                MakePregnancyBalancedDietDynamicGrid();
                txtReferance.Text = "Dietary guidelines for Indians, ICMR, 1995.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void FillNutrientSource()
        {
            try
            {
                txtFewWords.Text = "Nutrient that we obtain through food have vital effects on physical growth and developement, maintenance of normal body function, physical activity and health. " +
                                    "Nutritious food is, thus needed to sustain life and activity. Our diet must provide all essential nutrients in the required amount. " +
                                    "Requirement of essential nutrients vary with age, gender, physiological status and physical activity.";
                MakeNutrientSourceDynamicGrid();
                txtReferance.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void MakeCalorieDynamicGrid()
        {
            int Men = 1, Women = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 1190;
            //DynamicGrid.Height = 180;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0); 
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(150);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(170);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(170);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(170);
            ColumnDefinition gridCol5 = new ColumnDefinition();
            gridCol5.Width = new GridLength(190);
            ColumnDefinition gridCol6 = new ColumnDefinition();
            gridCol6.Width = new GridLength(170);
            ColumnDefinition gridCol7 = new ColumnDefinition();
            gridCol7.Width = new GridLength(170);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            DynamicGrid.ColumnDefinitions.Add(gridCol4);
            DynamicGrid.ColumnDefinitions.Add(gridCol5);
            DynamicGrid.ColumnDefinitions.Add(gridCol6);
            DynamicGrid.ColumnDefinitions.Add(gridCol7);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Activity Levels";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Maintain Weight cal(Men)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "Build Weight cal(Men)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 2);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "Loss Weight cal(Men)";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 3);

            TextBlock txtBlock5 = new TextBlock();
            txtBlock5.Text = "Maintain Weight cal(Women)";
            txtBlock5.FontSize = 13;
            txtBlock5.FontWeight = FontWeights.Bold;
            txtBlock5.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock5.VerticalAlignment = VerticalAlignment.Center;
            txtBlock5.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock5, 0);
            Grid.SetColumn(txtBlock5, 4);

            TextBlock txtBlock6 = new TextBlock();
            txtBlock6.Text = "Build Weight cal(Women)";
            txtBlock6.FontSize = 13;
            txtBlock6.FontWeight = FontWeights.Bold;
            txtBlock6.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock6.VerticalAlignment = VerticalAlignment.Center;
            txtBlock6.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock6, 0);
            Grid.SetColumn(txtBlock6, 5);

            TextBlock txtBlock7 = new TextBlock();
            txtBlock7.Text = "Loss Weight cal(Women)";
            txtBlock7.FontSize = 13;
            txtBlock7.FontWeight = FontWeights.Bold;
            txtBlock7.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock7.VerticalAlignment = VerticalAlignment.Center;
            txtBlock7.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock7, 0);
            Grid.SetColumn(txtBlock7, 6);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);
            DynamicGrid.Children.Add(txtBlock5);
            DynamicGrid.Children.Add(txtBlock6);
            DynamicGrid.Children.Add(txtBlock7);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListBMR(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 1)
                    {
                        TextBlock activityText = new TextBlock();
                        activityText.Text = Enum.GetName(typeof(LifeStyleType), Convert.ToInt32(tableofContentList[i].LifeStyleID));
                        activityText.FontSize = 12;
                        activityText.FontWeight = FontWeights.Bold;
                        activityText.VerticalAlignment = VerticalAlignment.Center;
                        activityText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(activityText, Men);
                        Grid.SetColumn(activityText, 0);

                        TextBlock maintainMenText = new TextBlock();
                        maintainMenText.Text = Convert.ToString(tableofContentList[i].CalorieToMaintain);
                        maintainMenText.FontSize = 12;
                        maintainMenText.FontWeight = FontWeights.Bold;
                        maintainMenText.VerticalAlignment = VerticalAlignment.Center;
                        maintainMenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(maintainMenText, Men);
                        Grid.SetColumn(maintainMenText, 1);

                        TextBlock buildMenText = new TextBlock();
                        buildMenText.Text = Convert.ToString(tableofContentList[i].CalorieToBuildFrom) + " - " + Convert.ToString(tableofContentList[i].CalorieToBuildTo);
                        buildMenText.FontSize = 12;
                        buildMenText.FontWeight = FontWeights.Bold;
                        buildMenText.VerticalAlignment = VerticalAlignment.Center;
                        buildMenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(buildMenText, Men);
                        Grid.SetColumn(buildMenText, 2);

                        TextBlock lossMenText = new TextBlock();
                        lossMenText.Text = Convert.ToString(tableofContentList[i].CalorieToLossFrom) + " - " + Convert.ToString(tableofContentList[i].CalorieToLossTo);
                        lossMenText.FontSize = 12;
                        lossMenText.FontWeight = FontWeights.Bold;
                        lossMenText.VerticalAlignment = VerticalAlignment.Center;
                        lossMenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(lossMenText, Men);
                        Grid.SetColumn(lossMenText, 3);

                        DynamicGrid.Children.Add(activityText);
                        DynamicGrid.Children.Add(maintainMenText);
                        DynamicGrid.Children.Add(lossMenText);
                        DynamicGrid.Children.Add(buildMenText);

                        Men = Men + 1;
                    }
                    else if (SexID == 2)
                    {
                        TextBlock activityText = new TextBlock();
                        activityText.Text = Enum.GetName(typeof(LifeStyleType), Convert.ToInt32(tableofContentList[i].LifeStyleID));
                        activityText.FontSize = 12;
                        activityText.FontWeight = FontWeights.Bold;
                        activityText.VerticalAlignment = VerticalAlignment.Center;
                        activityText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(activityText, Women);
                        Grid.SetColumn(activityText, 0);

                        TextBlock maintainWomenText = new TextBlock();
                        maintainWomenText.Text = Convert.ToString(tableofContentList[i].CalorieToMaintain);
                        maintainWomenText.FontSize = 12;
                        maintainWomenText.FontWeight = FontWeights.Bold;
                        maintainWomenText.VerticalAlignment = VerticalAlignment.Center;
                        maintainWomenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(maintainWomenText, Women);
                        Grid.SetColumn(maintainWomenText, 4);

                        TextBlock buildWomenText = new TextBlock();
                        buildWomenText.Text = Convert.ToString(tableofContentList[i].CalorieToBuildFrom) + " - " + Convert.ToString(tableofContentList[i].CalorieToBuildTo);
                        buildWomenText.FontSize = 12;
                        buildWomenText.FontWeight = FontWeights.Bold;
                        buildWomenText.VerticalAlignment = VerticalAlignment.Center;
                        buildWomenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(buildWomenText, Women);
                        Grid.SetColumn(buildWomenText, 5);

                        TextBlock lossWomenText = new TextBlock();
                        lossWomenText.Text = Convert.ToString(tableofContentList[i].CalorieToLossFrom) + " - " + Convert.ToString(tableofContentList[i].CalorieToLossTo);
                        lossWomenText.FontSize = 12;
                        lossWomenText.FontWeight = FontWeights.Bold;
                        lossWomenText.VerticalAlignment = VerticalAlignment.Center;
                        lossWomenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(lossWomenText, Women);
                        Grid.SetColumn(lossWomenText, 6);

                        DynamicGrid.Children.Add(activityText);
                        DynamicGrid.Children.Add(maintainWomenText);
                        DynamicGrid.Children.Add(lossWomenText);
                        DynamicGrid.Children.Add(buildWomenText);

                        Women = Women + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeBMIDynamicGrid()
        {
            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 3000;
            //DynamicGrid.Height = 330;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(250);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(450);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(630);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(1540);
            
            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
           
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(180);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(100);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(100);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(100);
            
            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "BMI Range";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            //txtBlock2.Text = "BMI Description";
            txtBlock2.Text = "Suggetions";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "BMI Comments";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 2);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "BMI Suggetions";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 3);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            //DynamicGrid.Children.Add(txtBlock3);
            //DynamicGrid.Children.Add(txtBlock4);

            List<BONutrition.SysAdmin> calorieCalculator = new List<BONutrition.SysAdmin>();
            calorieCalculator = SysAdminManager.GetBMIImpact(LanguageID);

            if (calorieCalculator != null)
            {
                for (int i = 0; i < calorieCalculator.Count; i++)
                {
                    TextBlock BMINameText = new TextBlock();
                    BMINameText.Text = Convert.ToString(calorieCalculator[i].BMIImpactName);
                    BMINameText.FontSize = 12;
                    BMINameText.FontWeight = FontWeights.Bold;
                    BMINameText.TextWrapping = TextWrapping.Wrap;
                    BMINameText.VerticalAlignment = VerticalAlignment.Center;
                    BMINameText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(BMINameText, i + 1);
                    Grid.SetColumn(BMINameText, 0);

                    TextBlock BMIDescriptionText = new TextBlock();
                    BMIDescriptionText.Text = Convert.ToString(calorieCalculator[i].BMIIMpactDescription);
                    BMIDescriptionText.FontSize = 12;
                    BMIDescriptionText.FontWeight = FontWeights.Bold;
                    BMIDescriptionText.TextWrapping = TextWrapping.Wrap;
                    BMIDescriptionText.VerticalAlignment = VerticalAlignment.Center;
                    BMIDescriptionText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(BMIDescriptionText, i + 1);
                    Grid.SetColumn(BMIDescriptionText, 1);

                    TextBlock BMICommentsText = new TextBlock();
                    BMICommentsText.Text = Convert.ToString(calorieCalculator[i].BMIIMpactComments);
                    BMICommentsText.FontSize = 12;
                    BMICommentsText.FontWeight = FontWeights.Bold;
                    BMICommentsText.TextWrapping = TextWrapping.Wrap;
                    BMICommentsText.VerticalAlignment = VerticalAlignment.Center;
                    BMICommentsText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(BMICommentsText, i + 1);
                    Grid.SetColumn(BMICommentsText, 2);

                    TextBlock BMISuggetionsText = new TextBlock();
                    BMISuggetionsText.Text = Convert.ToString(calorieCalculator[i].BMIIMpactSuggetions);
                    BMISuggetionsText.FontSize = 12;
                    BMISuggetionsText.FontWeight = FontWeights.Bold;
                    BMISuggetionsText.TextWrapping = TextWrapping.Wrap;
                    BMISuggetionsText.VerticalAlignment = VerticalAlignment.Center;
                    BMISuggetionsText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(BMISuggetionsText, i + 1);
                    Grid.SetColumn(BMISuggetionsText, 3);

                    DynamicGrid.Children.Add(BMINameText);
                    DynamicGrid.Children.Add(BMIDescriptionText);
                    //DynamicGrid.Children.Add(BMICommentsText);
                    //DynamicGrid.Children.Add(BMISuggetionsText);
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeImmunizationDynamicGrid()
        {
            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 640;
            //DynamicGrid.Height = 300;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(150);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(520);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);
            RowDefinition gridRow11 = new RowDefinition();
            gridRow11.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);
            DynamicGrid.RowDefinitions.Add(gridRow11);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Immunization Period";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Immunization Description";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListImmunization(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    TextBlock ImmunizationPeriodText = new TextBlock();
                    ImmunizationPeriodText.Text = Convert.ToString(tableofContentList[i].ImmunizationPeriod);
                    ImmunizationPeriodText.FontSize = 12;
                    ImmunizationPeriodText.FontWeight = FontWeights.Bold;
                    ImmunizationPeriodText.VerticalAlignment = VerticalAlignment.Center;
                    ImmunizationPeriodText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(ImmunizationPeriodText, i + 1);
                    Grid.SetColumn(ImmunizationPeriodText, 0);

                    TextBlock ImmunizationNameText = new TextBlock();
                    ImmunizationNameText.Text = Convert.ToString(tableofContentList[i].ImmunizationName);
                    ImmunizationNameText.FontSize = 12;
                    ImmunizationNameText.FontWeight = FontWeights.Bold;
                    ImmunizationNameText.VerticalAlignment = VerticalAlignment.Center;
                    ImmunizationNameText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(ImmunizationNameText, i + 1);
                    Grid.SetColumn(ImmunizationNameText, 1);

                    DynamicGrid.Children.Add(ImmunizationPeriodText);
                    DynamicGrid.Children.Add(ImmunizationNameText);
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeBabyHWDynamicGrid()
        {
            int Boy = 1, Girl = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 610;
            //DynamicGrid.Height = 180;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(150);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(130);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(130);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(130);
            ColumnDefinition gridCol5 = new ColumnDefinition();
            gridCol5.Width = new GridLength(130);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            DynamicGrid.ColumnDefinitions.Add(gridCol4);
            DynamicGrid.ColumnDefinitions.Add(gridCol5);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            
            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);

            TextBlock txtBlock0 = new TextBlock();
            txtBlock0.Text = "Infant Age";
            txtBlock0.FontSize = 13;
            txtBlock0.FontWeight = FontWeights.Bold;
            txtBlock0.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock0.VerticalAlignment = VerticalAlignment.Center;
            txtBlock0.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock0, 0);
            Grid.SetColumn(txtBlock0, 0);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Height cm(Boy)";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 1);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Weight kg(Boy)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 2);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "Height cm(Girl)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 3);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "Weight kg(Girl)";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 4);

            DynamicGrid.Children.Add(txtBlock0);
            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListBabyHW(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int Age = Convert.ToInt32(tableofContentList[i].InfantAge);
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 3)
                    {
                        TextBlock InfantAgeBoyText = new TextBlock();
                        InfantAgeBoyText.Text = Convert.ToString(tableofContentList[i].InfantAge) + " " + "Months";
                        InfantAgeBoyText.FontSize = 12;
                        InfantAgeBoyText.FontWeight = FontWeights.Bold;
                        InfantAgeBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantAgeBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantAgeBoyText, Boy);
                        Grid.SetColumn(InfantAgeBoyText, 0);

                        TextBlock InfantHeightBoyText = new TextBlock();
                        InfantHeightBoyText.Text = Convert.ToString(tableofContentList[i].InfantHeight);
                        InfantHeightBoyText.FontSize = 12;
                        InfantHeightBoyText.FontWeight = FontWeights.Bold;
                        InfantHeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantHeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantHeightBoyText, Boy);
                        Grid.SetColumn(InfantHeightBoyText, 1);

                        TextBlock InfantWeightBoyText = new TextBlock();
                        InfantWeightBoyText.Text = Convert.ToString(tableofContentList[i].InfantWeight);
                        InfantWeightBoyText.FontSize = 12;
                        InfantWeightBoyText.FontWeight = FontWeights.Bold;
                        InfantWeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantWeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantWeightBoyText, Boy);
                        Grid.SetColumn(InfantWeightBoyText, 2);

                        if (Age == 0 || Age == 3 || Age == 6 || Age == 9 || Age == 12)
                        {
                            DynamicGrid.Children.Add(InfantAgeBoyText);
                            DynamicGrid.Children.Add(InfantHeightBoyText);
                            DynamicGrid.Children.Add(InfantWeightBoyText);

                            Boy = Boy + 1;
                        }
                    }
                    else if (SexID == 4)
                    {
                        TextBlock InfantAgeGirlText = new TextBlock();
                        InfantAgeGirlText.Text = Convert.ToString(tableofContentList[i].InfantAge) + " " + "Months";
                        InfantAgeGirlText.FontSize = 12;
                        InfantAgeGirlText.FontWeight = FontWeights.Bold;
                        InfantAgeGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantAgeGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantAgeGirlText, Girl);
                        Grid.SetColumn(InfantAgeGirlText, 0);

                        TextBlock InfantHeightGirlText = new TextBlock();
                        InfantHeightGirlText.Text = Convert.ToString(tableofContentList[i].InfantHeight);
                        InfantHeightGirlText.FontSize = 12;
                        InfantHeightGirlText.FontWeight = FontWeights.Bold;
                        InfantHeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantHeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantHeightGirlText, Girl);
                        Grid.SetColumn(InfantHeightGirlText, 3);

                        TextBlock InfantWeightGirlText = new TextBlock();
                        InfantWeightGirlText.Text = Convert.ToString(tableofContentList[i].InfantWeight);
                        InfantWeightGirlText.FontSize = 12;
                        InfantWeightGirlText.FontWeight = FontWeights.Bold;
                        InfantWeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantWeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantWeightGirlText, Girl);
                        Grid.SetColumn(InfantWeightGirlText, 4);

                        if (Age == 0 || Age == 3 || Age == 6 || Age == 9 || Age == 12)
                        {
                            DynamicGrid.Children.Add(InfantAgeGirlText);
                            DynamicGrid.Children.Add(InfantHeightGirlText);
                            DynamicGrid.Children.Add(InfantWeightGirlText);

                            Girl = Girl + 1;
                        }
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeIdealWeightDynamicGrid()
        {
            int Men = 1, Women = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 600;
            //DynamicGrid.Height = 300;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(210);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(230);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(230);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Age";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Ideal Weight kg(Male)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "Ideal Weight kg(Female)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 2);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListIdealWeight(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 1)
                    {
                        TextBlock AgeMenText = new TextBlock();
                        AgeMenText.Text = Convert.ToString(tableofContentList[i].Age);
                        AgeMenText.FontSize = 12;
                        AgeMenText.FontWeight = FontWeights.Bold;
                        AgeMenText.VerticalAlignment = VerticalAlignment.Center;
                        AgeMenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(AgeMenText, Men);
                        Grid.SetColumn(AgeMenText, 0);

                        TextBlock WeightMenText = new TextBlock();
                        WeightMenText.Text = Convert.ToString(tableofContentList[i].Weight);
                        WeightMenText.FontSize = 12;
                        WeightMenText.FontWeight = FontWeights.Bold;
                        WeightMenText.VerticalAlignment = VerticalAlignment.Center;
                        WeightMenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(WeightMenText, Men);
                        Grid.SetColumn(WeightMenText, 1);

                        DynamicGrid.Children.Add(AgeMenText);
                        DynamicGrid.Children.Add(WeightMenText);

                        Men = Men + 1;
                    }
                    else if (SexID == 2)
                    {
                        TextBlock AgeWomenText = new TextBlock();
                        AgeWomenText.Text = Convert.ToString(tableofContentList[i].Age);
                        AgeWomenText.FontSize = 12;
                        AgeWomenText.FontWeight = FontWeights.Bold;
                        AgeWomenText.VerticalAlignment = VerticalAlignment.Center;
                        AgeWomenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(AgeWomenText, Women);
                        Grid.SetColumn(AgeWomenText, 0);

                        TextBlock WeightWomenText = new TextBlock();
                        WeightWomenText.Text = Convert.ToString(tableofContentList[i].Weight);
                        WeightWomenText.FontSize = 12;
                        WeightWomenText.FontWeight = FontWeights.Bold;
                        WeightWomenText.VerticalAlignment = VerticalAlignment.Center;
                        WeightWomenText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(WeightWomenText, Women);
                        Grid.SetColumn(WeightWomenText, 3);

                        DynamicGrid.Children.Add(AgeWomenText);
                        DynamicGrid.Children.Add(WeightWomenText);

                        Women = Women + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeAdolescentsHWDynamicGrid()
        {
            int Boy = 1, Girl = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 610;
            //DynamicGrid.Height = 270;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(150);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(130);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(130);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(130);
            ColumnDefinition gridCol5 = new ColumnDefinition();
            gridCol5.Width = new GridLength(130);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            DynamicGrid.ColumnDefinitions.Add(gridCol4);
            DynamicGrid.ColumnDefinitions.Add(gridCol5);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            
            TextBlock txtBlock0 = new TextBlock();
            txtBlock0.Text = "Adolescents Age";
            txtBlock0.FontSize = 13;
            txtBlock0.FontWeight = FontWeights.Bold;
            txtBlock0.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock0.VerticalAlignment = VerticalAlignment.Center;
            txtBlock0.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock0, 0);
            Grid.SetColumn(txtBlock0, 0);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Height cm(Boy)";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 1);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Weight kg(Boy)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 2);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "Height cm(Girl)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 3);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "Weight kg(Girl)";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 4);

            DynamicGrid.Children.Add(txtBlock0);
            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListAdolescentsHW(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int Age = Convert.ToInt32(tableofContentList[i].InfantAge);
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 1)
                    {
                        TextBlock InfantAgeBoyText = new TextBlock();
                        InfantAgeBoyText.Text = Convert.ToString(tableofContentList[i].InfantAge) + " " + "Years";
                        InfantAgeBoyText.FontSize = 12;
                        InfantAgeBoyText.FontWeight = FontWeights.Bold;
                        InfantAgeBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantAgeBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantAgeBoyText, Boy);
                        Grid.SetColumn(InfantAgeBoyText, 0);

                        TextBlock InfantHeightBoyText = new TextBlock();
                        InfantHeightBoyText.Text = Convert.ToString(tableofContentList[i].InfantHeight);
                        InfantHeightBoyText.FontSize = 12;
                        InfantHeightBoyText.FontWeight = FontWeights.Bold;
                        InfantHeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantHeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantHeightBoyText, Boy);
                        Grid.SetColumn(InfantHeightBoyText, 1);

                        TextBlock InfantWeightBoyText = new TextBlock();
                        InfantWeightBoyText.Text = Convert.ToString(tableofContentList[i].InfantWeight);
                        InfantWeightBoyText.FontSize = 12;
                        InfantWeightBoyText.FontWeight = FontWeights.Bold;
                        InfantWeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantWeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantWeightBoyText, Boy);
                        Grid.SetColumn(InfantWeightBoyText, 2);

                        DynamicGrid.Children.Add(InfantAgeBoyText);
                        DynamicGrid.Children.Add(InfantHeightBoyText);
                        DynamicGrid.Children.Add(InfantWeightBoyText);

                        Boy = Boy + 1;
                    }
                    else if (SexID == 2)
                    {
                        TextBlock InfantAgeGirlText = new TextBlock();
                        InfantAgeGirlText.Text = Convert.ToString(tableofContentList[i].InfantAge) + " " + "Years";
                        InfantAgeGirlText.FontSize = 12;
                        InfantAgeGirlText.FontWeight = FontWeights.Bold;
                        InfantAgeGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantAgeGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantAgeGirlText, Girl);
                        Grid.SetColumn(InfantAgeGirlText, 0);

                        TextBlock InfantHeightGirlText = new TextBlock();
                        InfantHeightGirlText.Text = Convert.ToString(tableofContentList[i].InfantHeight);
                        InfantHeightGirlText.FontSize = 12;
                        InfantHeightGirlText.FontWeight = FontWeights.Bold;
                        InfantHeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantHeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantHeightGirlText, Girl);
                        Grid.SetColumn(InfantHeightGirlText, 3);

                        TextBlock InfantWeightGirlText = new TextBlock();
                        InfantWeightGirlText.Text = Convert.ToString(tableofContentList[i].InfantWeight);
                        InfantWeightGirlText.FontSize = 12;
                        InfantWeightGirlText.FontWeight = FontWeights.Bold;
                        InfantWeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantWeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantWeightGirlText, Girl);
                        Grid.SetColumn(InfantWeightGirlText, 4);

                        DynamicGrid.Children.Add(InfantAgeGirlText);
                        DynamicGrid.Children.Add(InfantHeightGirlText);
                        DynamicGrid.Children.Add(InfantWeightGirlText);

                        Girl = Girl + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeEnergyRequirementDynamicGrid()
        {
            int Boy = 1, Girl = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 610;
            //DynamicGrid.Height = 570;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(150);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(250);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(250);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);
            RowDefinition gridRow11 = new RowDefinition();
            gridRow11.Height = new GridLength(30);
            RowDefinition gridRow12 = new RowDefinition();
            gridRow12.Height = new GridLength(30);
            RowDefinition gridRow13 = new RowDefinition();
            gridRow13.Height = new GridLength(30);
            RowDefinition gridRow14 = new RowDefinition();
            gridRow14.Height = new GridLength(30);
            RowDefinition gridRow15 = new RowDefinition();
            gridRow15.Height = new GridLength(30);
            RowDefinition gridRow16 = new RowDefinition();
            gridRow16.Height = new GridLength(30);
            RowDefinition gridRow17 = new RowDefinition();
            gridRow17.Height = new GridLength(30);
            RowDefinition gridRow18 = new RowDefinition();
            gridRow18.Height = new GridLength(30);
            RowDefinition gridRow19 = new RowDefinition();
            gridRow19.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);
            DynamicGrid.RowDefinitions.Add(gridRow11);
            DynamicGrid.RowDefinitions.Add(gridRow12);
            DynamicGrid.RowDefinitions.Add(gridRow13);
            DynamicGrid.RowDefinitions.Add(gridRow14);
            DynamicGrid.RowDefinitions.Add(gridRow15);
            DynamicGrid.RowDefinitions.Add(gridRow16);
            DynamicGrid.RowDefinitions.Add(gridRow17);
            DynamicGrid.RowDefinitions.Add(gridRow18);
            DynamicGrid.RowDefinitions.Add(gridRow19);

            TextBlock txtBlock0 = new TextBlock();
            txtBlock0.Text = "Children Age";
            txtBlock0.FontSize = 13;
            txtBlock0.FontWeight = FontWeights.Bold;
            txtBlock0.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock0.VerticalAlignment = VerticalAlignment.Center;
            txtBlock0.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock0, 0);
            Grid.SetColumn(txtBlock0, 0);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Energy KCal/d(Boy)";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 1);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Energy KCal/d(Girl)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 2);

            DynamicGrid.Children.Add(txtBlock0);
            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListEnergyRequirement(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int Age = Convert.ToInt32(tableofContentList[i].Age);
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 1)
                    {
                        TextBlock ChildrenAgeBoyText = new TextBlock();
                        ChildrenAgeBoyText.Text = Convert.ToString(tableofContentList[i].ChildrenAge) + " " + "Years";
                        ChildrenAgeBoyText.FontSize = 12;
                        ChildrenAgeBoyText.FontWeight = FontWeights.Bold;
                        ChildrenAgeBoyText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenAgeBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenAgeBoyText, Boy);
                        Grid.SetColumn(ChildrenAgeBoyText, 0);

                        TextBlock ChildrenHeightBoyText = new TextBlock();
                        ChildrenHeightBoyText.Text = Convert.ToString(tableofContentList[i].Energy);
                        ChildrenHeightBoyText.FontSize = 12;
                        ChildrenHeightBoyText.FontWeight = FontWeights.Bold;
                        ChildrenHeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenHeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenHeightBoyText, Boy);
                        Grid.SetColumn(ChildrenHeightBoyText, 1);

                        DynamicGrid.Children.Add(ChildrenAgeBoyText);
                        DynamicGrid.Children.Add(ChildrenHeightBoyText);

                        Boy = Boy + 1;
                    }
                    else if (SexID == 2)
                    {
                        TextBlock ChildrenAgeGirlText = new TextBlock();
                        ChildrenAgeGirlText.Text = Convert.ToString(tableofContentList[i].ChildrenAge) + " " + "Years";
                        ChildrenAgeGirlText.FontSize = 12;
                        ChildrenAgeGirlText.FontWeight = FontWeights.Bold;
                        ChildrenAgeGirlText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenAgeGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenAgeGirlText, Girl);
                        Grid.SetColumn(ChildrenAgeGirlText, 0);

                        TextBlock ChildrenHeightGirlText = new TextBlock();
                        ChildrenHeightGirlText.Text = Convert.ToString(tableofContentList[i].Energy);
                        ChildrenHeightGirlText.FontSize = 12;
                        ChildrenHeightGirlText.FontWeight = FontWeights.Bold;
                        ChildrenHeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenHeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenHeightGirlText, Girl);
                        Grid.SetColumn(ChildrenHeightGirlText, 2);

                        DynamicGrid.Children.Add(ChildrenAgeGirlText);
                        DynamicGrid.Children.Add(ChildrenHeightGirlText);

                        Girl = Girl + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeMeanHeightDynamicGrid()
        {
            int Boy = 1, Girl = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 610;
            //DynamicGrid.Height = 570;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(110);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(160);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(160);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(110);
            ColumnDefinition gridCol5 = new ColumnDefinition();
            gridCol5.Width = new GridLength(110);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            DynamicGrid.ColumnDefinitions.Add(gridCol4);
            DynamicGrid.ColumnDefinitions.Add(gridCol5);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);
            RowDefinition gridRow11 = new RowDefinition();
            gridRow11.Height = new GridLength(30);
            RowDefinition gridRow12 = new RowDefinition();
            gridRow12.Height = new GridLength(30);
            RowDefinition gridRow13 = new RowDefinition();
            gridRow13.Height = new GridLength(30);
            RowDefinition gridRow14 = new RowDefinition();
            gridRow14.Height = new GridLength(30);
            RowDefinition gridRow15 = new RowDefinition();
            gridRow15.Height = new GridLength(30);
            RowDefinition gridRow16 = new RowDefinition();
            gridRow16.Height = new GridLength(30);
            RowDefinition gridRow17 = new RowDefinition();
            gridRow17.Height = new GridLength(30);
            RowDefinition gridRow18 = new RowDefinition();
            gridRow18.Height = new GridLength(30);
            RowDefinition gridRow19 = new RowDefinition();
            gridRow19.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);
            DynamicGrid.RowDefinitions.Add(gridRow11);
            DynamicGrid.RowDefinitions.Add(gridRow12);
            DynamicGrid.RowDefinitions.Add(gridRow13);
            DynamicGrid.RowDefinitions.Add(gridRow14);
            DynamicGrid.RowDefinitions.Add(gridRow15);
            DynamicGrid.RowDefinitions.Add(gridRow16);
            DynamicGrid.RowDefinitions.Add(gridRow17);
            DynamicGrid.RowDefinitions.Add(gridRow18);
            DynamicGrid.RowDefinitions.Add(gridRow19);

            TextBlock txtBlock0 = new TextBlock();
            txtBlock0.Text = "Children Age";
            txtBlock0.FontSize = 13;
            txtBlock0.FontWeight = FontWeights.Bold;
            txtBlock0.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock0.VerticalAlignment = VerticalAlignment.Center;
            txtBlock0.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock0, 0);
            Grid.SetColumn(txtBlock0, 0);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Mean Height cm(Boy)";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 1);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Mean Height cm(Girl)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 2);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "SE (Boy)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 3);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "SE (Girl)";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 4);

            DynamicGrid.Children.Add(txtBlock0);
            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListMeanHeight(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int Age = Convert.ToInt32(tableofContentList[i].Age);
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 1)
                    {
                        TextBlock ChildrenAgeBoyText = new TextBlock();
                        ChildrenAgeBoyText.Text = Convert.ToString(tableofContentList[i].ChildrenAge) + " " + "Years";
                        ChildrenAgeBoyText.FontSize = 12;
                        ChildrenAgeBoyText.FontWeight = FontWeights.Bold;
                        ChildrenAgeBoyText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenAgeBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenAgeBoyText, Boy);
                        Grid.SetColumn(ChildrenAgeBoyText, 0);

                        TextBlock ChildrenHeightBoyText = new TextBlock();
                        ChildrenHeightBoyText.Text = Convert.ToString(tableofContentList[i].MeanHeight);
                        ChildrenHeightBoyText.FontSize = 12;
                        ChildrenHeightBoyText.FontWeight = FontWeights.Bold;
                        ChildrenHeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenHeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenHeightBoyText, Boy);
                        Grid.SetColumn(ChildrenHeightBoyText, 1);

                        TextBlock SEBoyText = new TextBlock();
                        SEBoyText.Text = Convert.ToString(tableofContentList[i].SE);
                        SEBoyText.FontSize = 12;
                        SEBoyText.FontWeight = FontWeights.Bold;
                        SEBoyText.VerticalAlignment = VerticalAlignment.Center;
                        SEBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(SEBoyText, Boy);
                        Grid.SetColumn(SEBoyText, 2);


                        DynamicGrid.Children.Add(ChildrenAgeBoyText);
                        DynamicGrid.Children.Add(ChildrenHeightBoyText);
                        DynamicGrid.Children.Add(SEBoyText);

                        Boy = Boy + 1;
                    }
                    else if (SexID == 2)
                    {
                        TextBlock ChildrenAgeGirlText = new TextBlock();
                        ChildrenAgeGirlText.Text = Convert.ToString(tableofContentList[i].ChildrenAge) + " " + "Years";
                        ChildrenAgeGirlText.FontSize = 12;
                        ChildrenAgeGirlText.FontWeight = FontWeights.Bold;
                        ChildrenAgeGirlText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenAgeGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenAgeGirlText, Girl);
                        Grid.SetColumn(ChildrenAgeGirlText, 0);

                        TextBlock ChildrenHeightGirlText = new TextBlock();
                        ChildrenHeightGirlText.Text = Convert.ToString(tableofContentList[i].MeanHeight);
                        ChildrenHeightGirlText.FontSize = 12;
                        ChildrenHeightGirlText.FontWeight = FontWeights.Bold;
                        ChildrenHeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenHeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenHeightGirlText, Girl);
                        Grid.SetColumn(ChildrenHeightGirlText, 3);

                        TextBlock SEGirlText = new TextBlock();
                        SEGirlText.Text = Convert.ToString(tableofContentList[i].SE);
                        SEGirlText.FontSize = 12;
                        SEGirlText.FontWeight = FontWeights.Bold;
                        SEGirlText.VerticalAlignment = VerticalAlignment.Center;
                        SEGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(SEGirlText, Girl);
                        Grid.SetColumn(SEGirlText, 4);

                        DynamicGrid.Children.Add(ChildrenAgeGirlText);
                        DynamicGrid.Children.Add(ChildrenHeightGirlText);
                        DynamicGrid.Children.Add(SEGirlText);

                        Girl = Girl + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeMeanWeightDynamicGrid()
        {
            int Boy = 1, Girl = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 610;
            //DynamicGrid.Height = 570;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(110);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(160);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(160);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(110);
            ColumnDefinition gridCol5 = new ColumnDefinition();
            gridCol5.Width = new GridLength(110);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            DynamicGrid.ColumnDefinitions.Add(gridCol4);
            DynamicGrid.ColumnDefinitions.Add(gridCol5);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);
            RowDefinition gridRow11 = new RowDefinition();
            gridRow11.Height = new GridLength(30);
            RowDefinition gridRow12 = new RowDefinition();
            gridRow12.Height = new GridLength(30);
            RowDefinition gridRow13 = new RowDefinition();
            gridRow13.Height = new GridLength(30);
            RowDefinition gridRow14 = new RowDefinition();
            gridRow14.Height = new GridLength(30);
            RowDefinition gridRow15 = new RowDefinition();
            gridRow15.Height = new GridLength(30);
            RowDefinition gridRow16 = new RowDefinition();
            gridRow16.Height = new GridLength(30);
            RowDefinition gridRow17 = new RowDefinition();
            gridRow17.Height = new GridLength(30);
            RowDefinition gridRow18 = new RowDefinition();
            gridRow18.Height = new GridLength(30);
            RowDefinition gridRow19 = new RowDefinition();
            gridRow19.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);
            DynamicGrid.RowDefinitions.Add(gridRow11);
            DynamicGrid.RowDefinitions.Add(gridRow12);
            DynamicGrid.RowDefinitions.Add(gridRow13);
            DynamicGrid.RowDefinitions.Add(gridRow14);
            DynamicGrid.RowDefinitions.Add(gridRow15);
            DynamicGrid.RowDefinitions.Add(gridRow16);
            DynamicGrid.RowDefinitions.Add(gridRow17);
            DynamicGrid.RowDefinitions.Add(gridRow18);
            DynamicGrid.RowDefinitions.Add(gridRow19);

            TextBlock txtBlock0 = new TextBlock();
            txtBlock0.Text = "Children Age";
            txtBlock0.FontSize = 13;
            txtBlock0.FontWeight = FontWeights.Bold;
            txtBlock0.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock0.VerticalAlignment = VerticalAlignment.Center;
            txtBlock0.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock0, 0);
            Grid.SetColumn(txtBlock0, 0);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Mean Weight cm(Boy)";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 1);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Mean Weight cm(Girl)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 2);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "SE (Boy)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 3);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "SE (Girl)";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 4);

            DynamicGrid.Children.Add(txtBlock0);
            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListMeanWeight(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int Age = Convert.ToInt32(tableofContentList[i].Age);
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 1)
                    {
                        TextBlock ChildrenAgeBoyText = new TextBlock();
                        ChildrenAgeBoyText.Text = Convert.ToString(tableofContentList[i].ChildrenAge) + " " + "Years";
                        ChildrenAgeBoyText.FontSize = 12;
                        ChildrenAgeBoyText.FontWeight = FontWeights.Bold;
                        ChildrenAgeBoyText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenAgeBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenAgeBoyText, Boy);
                        Grid.SetColumn(ChildrenAgeBoyText, 0);

                        TextBlock ChildrenHeightBoyText = new TextBlock();
                        ChildrenHeightBoyText.Text = Convert.ToString(tableofContentList[i].MeanWeight);
                        ChildrenHeightBoyText.FontSize = 12;
                        ChildrenHeightBoyText.FontWeight = FontWeights.Bold;
                        ChildrenHeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenHeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenHeightBoyText, Boy);
                        Grid.SetColumn(ChildrenHeightBoyText, 1);

                        TextBlock SEBoyText = new TextBlock();
                        SEBoyText.Text = Convert.ToString(tableofContentList[i].SE);
                        SEBoyText.FontSize = 12;
                        SEBoyText.FontWeight = FontWeights.Bold;
                        SEBoyText.VerticalAlignment = VerticalAlignment.Center;
                        SEBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(SEBoyText, Boy);
                        Grid.SetColumn(SEBoyText, 2);


                        DynamicGrid.Children.Add(ChildrenAgeBoyText);
                        DynamicGrid.Children.Add(ChildrenHeightBoyText);
                        DynamicGrid.Children.Add(SEBoyText);

                        Boy = Boy + 1;
                    }
                    else if (SexID == 2)
                    {
                        TextBlock ChildrenAgeGirlText = new TextBlock();
                        ChildrenAgeGirlText.Text = Convert.ToString(tableofContentList[i].ChildrenAge) + " " + "Years";
                        ChildrenAgeGirlText.FontSize = 12;
                        ChildrenAgeGirlText.FontWeight = FontWeights.Bold;
                        ChildrenAgeGirlText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenAgeGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenAgeGirlText, Girl);
                        Grid.SetColumn(ChildrenAgeGirlText, 0);

                        TextBlock ChildrenHeightGirlText = new TextBlock();
                        ChildrenHeightGirlText.Text = Convert.ToString(tableofContentList[i].MeanWeight);
                        ChildrenHeightGirlText.FontSize = 12;
                        ChildrenHeightGirlText.FontWeight = FontWeights.Bold;
                        ChildrenHeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        ChildrenHeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(ChildrenHeightGirlText, Girl);
                        Grid.SetColumn(ChildrenHeightGirlText, 3);

                        TextBlock SEGirlText = new TextBlock();
                        SEGirlText.Text = Convert.ToString(tableofContentList[i].SE);
                        SEGirlText.FontSize = 12;
                        SEGirlText.FontWeight = FontWeights.Bold;
                        SEGirlText.VerticalAlignment = VerticalAlignment.Center;
                        SEGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(SEGirlText, Girl);
                        Grid.SetColumn(SEGirlText, 4);

                        DynamicGrid.Children.Add(ChildrenAgeGirlText);
                        DynamicGrid.Children.Add(ChildrenHeightGirlText);
                        DynamicGrid.Children.Add(SEGirlText);

                        Girl = Girl + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeChildrenHWDynamicGrid()
        {
            int Boy = 1, Girl = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 610;
            //DynamicGrid.Height = 330;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(150);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(130);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(130);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(130);
            ColumnDefinition gridCol5 = new ColumnDefinition();
            gridCol5.Width = new GridLength(130);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            DynamicGrid.ColumnDefinitions.Add(gridCol4);
            DynamicGrid.ColumnDefinitions.Add(gridCol5);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);
            RowDefinition gridRow11 = new RowDefinition();
            gridRow11.Height = new GridLength(30);
            RowDefinition gridRow12 = new RowDefinition();
            gridRow12.Height = new GridLength(30);
            RowDefinition gridRow13 = new RowDefinition();
            gridRow13.Height = new GridLength(30);


            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);
            DynamicGrid.RowDefinitions.Add(gridRow11);
            DynamicGrid.RowDefinitions.Add(gridRow12);
            DynamicGrid.RowDefinitions.Add(gridRow13);

            TextBlock txtBlock0 = new TextBlock();
            txtBlock0.Text = "Children Age";
            txtBlock0.FontSize = 13;
            txtBlock0.FontWeight = FontWeights.Bold;
            txtBlock0.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock0.VerticalAlignment = VerticalAlignment.Center;
            txtBlock0.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock0, 0);
            Grid.SetColumn(txtBlock0, 0);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Height cm(Boy)";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 1);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Weight kg(Boy)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 2);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "Height cm(Girl)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 3);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "Weight kg(Girl)";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 4);

            DynamicGrid.Children.Add(txtBlock0);
            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListChildrenHW(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int Age = Convert.ToInt32(tableofContentList[i].InfantAge);
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 1)
                    {
                        TextBlock InfantAgeBoyText = new TextBlock();
                        InfantAgeBoyText.Text = Convert.ToString(tableofContentList[i].InfantAge) + " " + "Years";
                        InfantAgeBoyText.FontSize = 12;
                        InfantAgeBoyText.FontWeight = FontWeights.Bold;
                        InfantAgeBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantAgeBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantAgeBoyText, Boy);
                        Grid.SetColumn(InfantAgeBoyText, 0);

                        TextBlock InfantHeightBoyText = new TextBlock();
                        InfantHeightBoyText.Text = Convert.ToString(tableofContentList[i].InfantHeight);
                        InfantHeightBoyText.FontSize = 12;
                        InfantHeightBoyText.FontWeight = FontWeights.Bold;
                        InfantHeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantHeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantHeightBoyText, Boy);
                        Grid.SetColumn(InfantHeightBoyText, 1);

                        TextBlock InfantWeightBoyText = new TextBlock();
                        InfantWeightBoyText.Text = Convert.ToString(tableofContentList[i].InfantWeight);
                        InfantWeightBoyText.FontSize = 12;
                        InfantWeightBoyText.FontWeight = FontWeights.Bold;
                        InfantWeightBoyText.VerticalAlignment = VerticalAlignment.Center;
                        InfantWeightBoyText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantWeightBoyText, Boy);
                        Grid.SetColumn(InfantWeightBoyText, 2);

                        DynamicGrid.Children.Add(InfantAgeBoyText);
                        DynamicGrid.Children.Add(InfantHeightBoyText);
                        DynamicGrid.Children.Add(InfantWeightBoyText);

                        Boy = Boy + 1;
                    }
                    else if (SexID == 2)
                    {
                        TextBlock InfantAgeGirlText = new TextBlock();
                        InfantAgeGirlText.Text = Convert.ToString(tableofContentList[i].InfantAge) + " " + "Years";
                        InfantAgeGirlText.FontSize = 12;
                        InfantAgeGirlText.FontWeight = FontWeights.Bold;
                        InfantAgeGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantAgeGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantAgeGirlText, Girl);
                        Grid.SetColumn(InfantAgeGirlText, 0);

                        TextBlock InfantHeightGirlText = new TextBlock();
                        InfantHeightGirlText.Text = Convert.ToString(tableofContentList[i].InfantHeight);
                        InfantHeightGirlText.FontSize = 12;
                        InfantHeightGirlText.FontWeight = FontWeights.Bold;
                        InfantHeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantHeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantHeightGirlText, Girl);
                        Grid.SetColumn(InfantHeightGirlText, 3);

                        TextBlock InfantWeightGirlText = new TextBlock();
                        InfantWeightGirlText.Text = Convert.ToString(tableofContentList[i].InfantWeight);
                        InfantWeightGirlText.FontSize = 12;
                        InfantWeightGirlText.FontWeight = FontWeights.Bold;
                        InfantWeightGirlText.VerticalAlignment = VerticalAlignment.Center;
                        InfantWeightGirlText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(InfantWeightGirlText, Girl);
                        Grid.SetColumn(InfantWeightGirlText, 4);

                        DynamicGrid.Children.Add(InfantAgeGirlText);
                        DynamicGrid.Children.Add(InfantHeightGirlText);
                        DynamicGrid.Children.Add(InfantWeightGirlText);

                        Girl = Girl + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeBabyBalancedDietDynamicGrid()
        {
            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 620;
            //DynamicGrid.Height = 300;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(310);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(360);


            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Food Stuff";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Quantity (gm)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);

            List<HealthTips> healthTipsList = new List<HealthTips>();
            healthTipsList = HealthTipsManager.GetListBalancedDiet((int)DietType.Infants, LanguageID);

            if (healthTipsList != null)
            {
                for (int i = 0; i < healthTipsList.Count; i++)
                {
                    TextBlock FoodGroupNameText = new TextBlock();
                    FoodGroupNameText.Text = Convert.ToString(healthTipsList[i].FoodGroupName);
                    FoodGroupNameText.FontSize = 12;
                    FoodGroupNameText.FontWeight = FontWeights.Bold;
                    FoodGroupNameText.VerticalAlignment = VerticalAlignment.Center;
                    FoodGroupNameText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(FoodGroupNameText, i + 1);
                    Grid.SetColumn(FoodGroupNameText, 0);

                    TextBlock QuantityText = new TextBlock();
                    QuantityText.Text = Convert.ToString(healthTipsList[i].Quantity);
                    QuantityText.FontSize = 12;
                    QuantityText.TextWrapping = TextWrapping.Wrap;
                    QuantityText.FontWeight = FontWeights.Bold;
                    QuantityText.VerticalAlignment = VerticalAlignment.Center;
                    QuantityText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(QuantityText, i + 1);
                    Grid.SetColumn(QuantityText, 1);

                    DynamicGrid.Children.Add(FoodGroupNameText);
                    DynamicGrid.Children.Add(QuantityText);
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeElderlyBalancedDietDynamicGrid()
        {
            int Men = 1, Women = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 620;
            //DynamicGrid.Height = 300;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(310);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(180);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(180);


            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Food Stuff";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Quantity gm(Men)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "Quantity gm(Women)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 2);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);

            List<HealthTips> healthTipsList = new List<HealthTips>();
            healthTipsList = HealthTipsManager.GetListBalancedDiet((int)DietType.Elder, LanguageID);

            if (healthTipsList != null)
            {
                for (int i = 0; i < healthTipsList.Count; i++)
                {
                    int SexID = Convert.ToInt16(healthTipsList[i].SexID);

                    if (SexID == 1)
                    {

                        TextBlock FoodGroupNameText = new TextBlock();
                        FoodGroupNameText.Text = Convert.ToString(healthTipsList[i].FoodGroupName);
                        FoodGroupNameText.FontSize = 12;
                        FoodGroupNameText.FontWeight = FontWeights.Bold;
                        FoodGroupNameText.VerticalAlignment = VerticalAlignment.Center;
                        FoodGroupNameText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(FoodGroupNameText, Men);
                        Grid.SetColumn(FoodGroupNameText, 0);

                        TextBlock QuantityText = new TextBlock();
                        QuantityText.Text = Convert.ToString(healthTipsList[i].Quantity);
                        QuantityText.FontSize = 12;
                        QuantityText.TextWrapping = TextWrapping.Wrap;
                        QuantityText.FontWeight = FontWeights.Bold;
                        QuantityText.VerticalAlignment = VerticalAlignment.Center;
                        QuantityText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(QuantityText, Men);
                        Grid.SetColumn(QuantityText, 1);

                        DynamicGrid.Children.Add(FoodGroupNameText);
                        DynamicGrid.Children.Add(QuantityText);

                        Men = Men + 1;
                    }
                    else if (SexID == 2)
                    {

                        TextBlock FoodGroupNameText = new TextBlock();
                        FoodGroupNameText.Text = Convert.ToString(healthTipsList[i].FoodGroupName);
                        FoodGroupNameText.FontSize = 12;
                        FoodGroupNameText.FontWeight = FontWeights.Bold;
                        FoodGroupNameText.VerticalAlignment = VerticalAlignment.Center;
                        FoodGroupNameText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(FoodGroupNameText, Women);
                        Grid.SetColumn(FoodGroupNameText, 0);

                        TextBlock QuantityText = new TextBlock();
                        QuantityText.Text = Convert.ToString(healthTipsList[i].Quantity);
                        QuantityText.FontSize = 12;
                        QuantityText.TextWrapping = TextWrapping.Wrap;
                        QuantityText.FontWeight = FontWeights.Bold;
                        QuantityText.VerticalAlignment = VerticalAlignment.Center;
                        QuantityText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(QuantityText, Women);
                        Grid.SetColumn(QuantityText, 3);

                        DynamicGrid.Children.Add(FoodGroupNameText);
                        DynamicGrid.Children.Add(QuantityText);

                        Women = Women + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakePregnancyBalancedDietDynamicGrid()
        {
            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 620;
            //DynamicGrid.Height = 300;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(350);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(320);


            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Food Stuff";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Quantity (gm)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);

            List<HealthTips> healthTipsList = new List<HealthTips>();
            healthTipsList = HealthTipsManager.GetListBalancedDiet((int)DietType.Pregnant, LanguageID);

            if (healthTipsList != null)
            {
                for (int i = 0; i < healthTipsList.Count; i++)
                {
                    TextBlock FoodGroupNameText = new TextBlock();
                    FoodGroupNameText.Text = Convert.ToString(healthTipsList[i].FoodGroupName);
                    FoodGroupNameText.FontSize = 12;
                    FoodGroupNameText.FontWeight = FontWeights.Bold;
                    FoodGroupNameText.VerticalAlignment = VerticalAlignment.Center;
                    FoodGroupNameText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(FoodGroupNameText, i + 1);
                    Grid.SetColumn(FoodGroupNameText, 0);

                    TextBlock QuantityText = new TextBlock();
                    QuantityText.Text = Convert.ToString(healthTipsList[i].Quantity);
                    QuantityText.FontSize = 12;
                    QuantityText.TextWrapping = TextWrapping.Wrap;
                    QuantityText.FontWeight = FontWeights.Bold;
                    QuantityText.VerticalAlignment = VerticalAlignment.Center;
                    QuantityText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(QuantityText, i + 1);
                    Grid.SetColumn(QuantityText, 1);

                    DynamicGrid.Children.Add(FoodGroupNameText);
                    DynamicGrid.Children.Add(QuantityText);
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeChildrenBalancedDietDynamicGrid()
        {
            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 640;
            //DynamicGrid.Height = 300;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(280);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(130);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(130);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(130);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            DynamicGrid.ColumnDefinitions.Add(gridCol4);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Food Stuff";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "1-3 Years(gm)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "4-6 Years(gm)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 2);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "7-9 Years(gm)";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 3);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListChildrenBD(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    TextBlock FoodGroupNameText = new TextBlock();
                    FoodGroupNameText.Text = Convert.ToString(tableofContentList[i].FoodGroupName);
                    FoodGroupNameText.FontSize = 12;
                    FoodGroupNameText.FontWeight = FontWeights.Bold;
                    FoodGroupNameText.VerticalAlignment = VerticalAlignment.Center;
                    FoodGroupNameText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(FoodGroupNameText, i + 1);
                    Grid.SetColumn(FoodGroupNameText, 0);

                    TextBlock Years1to3Text = new TextBlock();
                    Years1to3Text.Text = Convert.ToString(tableofContentList[i].Years1to3);
                    Years1to3Text.FontSize = 12;
                    Years1to3Text.FontWeight = FontWeights.Bold;
                    Years1to3Text.VerticalAlignment = VerticalAlignment.Center;
                    Years1to3Text.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(Years1to3Text, i + 1);
                    Grid.SetColumn(Years1to3Text, 1);

                    TextBlock Years4to6Text = new TextBlock();
                    Years4to6Text.Text = Convert.ToString(tableofContentList[i].Years4to6);
                    Years4to6Text.FontSize = 12;
                    Years4to6Text.FontWeight = FontWeights.Bold;
                    Years4to6Text.VerticalAlignment = VerticalAlignment.Center;
                    Years4to6Text.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(Years4to6Text, i + 1);
                    Grid.SetColumn(Years4to6Text, 2);


                    TextBlock Years7to9Text = new TextBlock();
                    Years7to9Text.Text = Convert.ToString(tableofContentList[i].Years7to9);
                    Years7to9Text.FontSize = 12;
                    Years7to9Text.TextWrapping = TextWrapping.Wrap;
                    Years7to9Text.FontWeight = FontWeights.Bold;
                    Years7to9Text.VerticalAlignment = VerticalAlignment.Center;
                    Years7to9Text.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(Years7to9Text, i + 1);
                    Grid.SetColumn(Years7to9Text, 3);

                    DynamicGrid.Children.Add(FoodGroupNameText);
                    DynamicGrid.Children.Add(Years1to3Text);
                    DynamicGrid.Children.Add(Years4to6Text);
                    DynamicGrid.Children.Add(Years7to9Text);
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeAdolescentsBalancedDietDynamicGrid()
        {
            int Boy = 1, Girl = 1;

            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 880;
            //DynamicGrid.Height = 300;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(280);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(150);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(150);
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol4.Width = new GridLength(150);
            ColumnDefinition gridCol5 = new ColumnDefinition();
            gridCol5.Width = new GridLength(150);

            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);
            DynamicGrid.ColumnDefinitions.Add(gridCol4);
            DynamicGrid.ColumnDefinitions.Add(gridCol5);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(30);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(30);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(30);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(30);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(30);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(30);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(30);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(30);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(30);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(30);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Food Stuff";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "10-12 Years gm(Boy)";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "13-18 Years gm(Boy)";
            txtBlock3.FontSize = 13;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock3.VerticalAlignment = VerticalAlignment.Center;
            txtBlock3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 2);

            TextBlock txtBlock4 = new TextBlock();
            txtBlock4.Text = "10-12 Years gm(Girl)";
            txtBlock4.FontSize = 13;
            txtBlock4.FontWeight = FontWeights.Bold;
            txtBlock4.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock4.VerticalAlignment = VerticalAlignment.Center;
            txtBlock4.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock4, 0);
            Grid.SetColumn(txtBlock4, 3);

            TextBlock txtBlock5 = new TextBlock();
            txtBlock5.Text = "13-18 Years gm(Girl)";
            txtBlock5.FontSize = 13;
            txtBlock5.FontWeight = FontWeights.Bold;
            txtBlock5.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock5.VerticalAlignment = VerticalAlignment.Center;
            txtBlock5.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock5, 0);
            Grid.SetColumn(txtBlock5, 4);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);
            DynamicGrid.Children.Add(txtBlock5);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListAdolescentsBD(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    int SexID = Convert.ToInt16(tableofContentList[i].SexID);

                    if (SexID == 1)
                    {
                        TextBlock FoodGroupNameText = new TextBlock();
                        FoodGroupNameText.Text = Convert.ToString(tableofContentList[i].FoodGroupName);
                        FoodGroupNameText.FontSize = 12;
                        FoodGroupNameText.FontWeight = FontWeights.Bold;
                        FoodGroupNameText.VerticalAlignment = VerticalAlignment.Center;
                        FoodGroupNameText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(FoodGroupNameText, Boy);
                        Grid.SetColumn(FoodGroupNameText, 0);

                        TextBlock Years10to12Text = new TextBlock();
                        Years10to12Text.Text = Convert.ToString(tableofContentList[i].Years10to12);
                        Years10to12Text.FontSize = 12;
                        Years10to12Text.FontWeight = FontWeights.Bold;
                        Years10to12Text.VerticalAlignment = VerticalAlignment.Center;
                        Years10to12Text.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(Years10to12Text, Boy);
                        Grid.SetColumn(Years10to12Text, 1);

                        TextBlock Years13to18Text = new TextBlock();
                        Years13to18Text.Text = Convert.ToString(tableofContentList[i].Years13to18);
                        Years13to18Text.FontSize = 12;
                        Years13to18Text.FontWeight = FontWeights.Bold;
                        Years13to18Text.VerticalAlignment = VerticalAlignment.Center;
                        Years13to18Text.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(Years13to18Text, Boy);
                        Grid.SetColumn(Years13to18Text, 2);

                        DynamicGrid.Children.Add(FoodGroupNameText);
                        DynamicGrid.Children.Add(Years10to12Text);
                        DynamicGrid.Children.Add(Years13to18Text);

                        Boy = Boy + 1;
                    }
                    else if (SexID == 2)
                    {
                        TextBlock FoodGroupNameText = new TextBlock();
                        FoodGroupNameText.Text = Convert.ToString(tableofContentList[i].FoodGroupName);
                        FoodGroupNameText.FontSize = 12;
                        FoodGroupNameText.FontWeight = FontWeights.Bold;
                        FoodGroupNameText.VerticalAlignment = VerticalAlignment.Center;
                        FoodGroupNameText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(FoodGroupNameText, Girl);
                        Grid.SetColumn(FoodGroupNameText, 0);

                        TextBlock Years10to12Text = new TextBlock();
                        Years10to12Text.Text = Convert.ToString(tableofContentList[i].Years10to12);
                        Years10to12Text.FontSize = 12;
                        Years10to12Text.FontWeight = FontWeights.Bold;
                        Years10to12Text.VerticalAlignment = VerticalAlignment.Center;
                        Years10to12Text.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(Years10to12Text, Girl);
                        Grid.SetColumn(Years10to12Text, 3);

                        TextBlock Years13to18Text = new TextBlock();
                        Years13to18Text.Text = Convert.ToString(tableofContentList[i].Years13to18);
                        Years13to18Text.FontSize = 12;
                        Years13to18Text.FontWeight = FontWeights.Bold;
                        Years13to18Text.VerticalAlignment = VerticalAlignment.Center;
                        Years13to18Text.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(Years13to18Text, Girl);
                        Grid.SetColumn(Years13to18Text, 4);

                        DynamicGrid.Children.Add(FoodGroupNameText);
                        DynamicGrid.Children.Add(Years10to12Text);
                        DynamicGrid.Children.Add(Years13to18Text);

                        Girl = Girl + 1;
                    }
                }
            }

            svContent.Content = DynamicGrid;
        }

        public void MakeNutrientSourceDynamicGrid()
        {
            Grid DynamicGrid = new Grid();
            //DynamicGrid.Width = 720;
            //DynamicGrid.Height = 380;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);

            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(140);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(530);


            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);

            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(33);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(33);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(33);
            RowDefinition gridRow4 = new RowDefinition();
            gridRow4.Height = new GridLength(33);
            RowDefinition gridRow5 = new RowDefinition();
            gridRow5.Height = new GridLength(33);
            RowDefinition gridRow6 = new RowDefinition();
            gridRow6.Height = new GridLength(33);
            RowDefinition gridRow7 = new RowDefinition();
            gridRow7.Height = new GridLength(33);
            RowDefinition gridRow8 = new RowDefinition();
            gridRow8.Height = new GridLength(33);
            RowDefinition gridRow9 = new RowDefinition();
            gridRow9.Height = new GridLength(33);
            RowDefinition gridRow10 = new RowDefinition();
            gridRow10.Height = new GridLength(53);
            RowDefinition gridRow11 = new RowDefinition();
            gridRow11.Height = new GridLength(33);
            RowDefinition gridRow12 = new RowDefinition();
            gridRow12.Height = new GridLength(33);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);
            DynamicGrid.RowDefinitions.Add(gridRow4);
            DynamicGrid.RowDefinitions.Add(gridRow5);
            DynamicGrid.RowDefinitions.Add(gridRow6);
            DynamicGrid.RowDefinitions.Add(gridRow7);
            DynamicGrid.RowDefinitions.Add(gridRow8);
            DynamicGrid.RowDefinitions.Add(gridRow9);
            DynamicGrid.RowDefinitions.Add(gridRow10);
            DynamicGrid.RowDefinitions.Add(gridRow11);
            DynamicGrid.RowDefinitions.Add(gridRow12);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Nutrients";
            txtBlock1.FontSize = 13;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Sources";
            txtBlock2.FontSize = 13;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Chocolate);
            txtBlock2.VerticalAlignment = VerticalAlignment.Center;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);

            List<TableofContent> tableofContentList = new List<TableofContent>();
            tableofContentList = TableofContentManager.GetListNutrientSource(LanguageID);

            if (tableofContentList != null)
            {
                for (int i = 0; i < tableofContentList.Count; i++)
                {
                    TextBlock NutrientText = new TextBlock();
                    NutrientText.Text = Convert.ToString(tableofContentList[i].Nutrient);
                    NutrientText.FontSize = 12;
                    NutrientText.FontWeight = FontWeights.Bold;
                    NutrientText.VerticalAlignment = VerticalAlignment.Center;
                    NutrientText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(NutrientText, i + 1);
                    Grid.SetColumn(NutrientText, 0);

                    TextBlock NutrientSourceText = new TextBlock();
                    NutrientSourceText.Text = Convert.ToString(tableofContentList[i].NutrientSource);
                    NutrientSourceText.FontSize = 12;
                    NutrientSourceText.TextWrapping = TextWrapping.Wrap;
                    NutrientSourceText.FontWeight = FontWeights.Bold;
                    NutrientSourceText.VerticalAlignment = VerticalAlignment.Center;
                    NutrientSourceText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(NutrientSourceText, i + 1);
                    Grid.SetColumn(NutrientSourceText, 1);

                    DynamicGrid.Children.Add(NutrientText);
                    DynamicGrid.Children.Add(NutrientSourceText);
                }
            }

            svContent.Content = DynamicGrid;
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            ApplyStyle(16, 1);
            FillXMLCombo();
            Keyboard.Focus(cbReferanceItems);
            cbReferanceItems.SelectedIndex = 0;

            //FillData(Enum.GetName(typeof(Enums.TableofContents), 1).Replace("_", " "), (int)Enums.TableofContents.Know_Calorie);
        }

        private void cbReferanceItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbReferanceItems.SelectedIndex >= 0)
            {
                FillData(Enum.GetName(typeof(Enums.TableofContents), cbReferanceItems.SelectedIndex + 1).Replace("_", " "), cbReferanceItems.SelectedIndex + 1);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.Close();
        }

        private void mnuItem1_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 1);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 1).Replace("_", " "), (int)Enums.TableofContents.Know_Calorie);
        }

        private void mnuItem2_Click(object sender, RoutedEventArgs e)
        {
            //ApplyStyle(16, 2);
            //FillData(Enum.GetName(typeof(Enums.TableofContents), 2).Replace("_", " "), (int)Enums.TableofContents.BMI_Impact);
        }

        private void mnuItem3_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 3);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 3).Replace("_", " "), (int)Enums.TableofContents.Children_Height_Weight); 
        }

        private void mnuItem4_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 4);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 5).Replace("_", " "), (int)Enums.TableofContents.Adolescents_Height_Weight);
        }

        private void mnuItem5_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 5);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 6).Replace("_", " "), (int)Enums.TableofContents.Baby_Height_Weight);
        }

        private void mnuItem6_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 6);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 7).Replace("_", " "), (int)Enums.TableofContents.Baby_Immunization);
        }

        private void mnuItem7_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 7);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 8).Replace("_", " "), (int)Enums.TableofContents.Ideal_Weight);
        }

        private void mnuItem8_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 8);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 9).Replace("_", " "), (int)Enums.TableofContents.Energy_Requirement);
        }

        private void mnuItem9_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 9);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 10).Replace("_", " "), (int)Enums.TableofContents.Mean_SE_Value_of_Height);
        }

        private void mnuItem10_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 10);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 11).Replace("_", " "), (int)Enums.TableofContents.Mean_SE_Value_of_Weight);
        }

        private void mnuItem11_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 11);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 12).Replace("_", " "), (int)Enums.TableofContents.Baby_Balanced_Diet);
        }

        private void mnuItem12_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 12);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 13).Replace("_", " "), (int)Enums.TableofContents.Children_Balanced_Diet);
        }

        private void mnuItem13_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 13);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 14).Replace("_", " "), (int)Enums.TableofContents.Adolescents_Balanced_Diet);
        }

        private void mnuItem14_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 14);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 15).Replace("_", " "), (int)Enums.TableofContents.Elderly_Balanced_Diet);
        }

        private void mnuItem15_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 15);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 16).Replace("_", " "), (int)Enums.TableofContents.Nutrient_Source);
        }

        private void mnuItem16_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle(16, 16);
            FillData(Enum.GetName(typeof(Enums.TableofContents), 17).Replace("_", " "), (int)Enums.TableofContents.Pregnancy_Balanced_Diet);
        }

        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                //Close();
            }
        }

        private void lblHelp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Help help = new Help();
            help.HelpItemID = (int)HelpType.Referance;
            help.Owner = Application.Current.MainWindow;
            help.ShowDialog();
        }

        #endregion

    }
}

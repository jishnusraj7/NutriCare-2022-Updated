using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.IO;
using System.IO.Packaging;
using BONutrition;
using BLNutrition;
using NutritionV1.Enums;

namespace NutritionV1.Classes
{
    public class CommonFunctions
    {        
        #region Get current culture
        /// <summary>
        /// Get the language
        /// </summary>
        /// <returns>string</returns>
        public static string GetLanguage()
        {
            string Language = string.Empty;
            try
            {
                Language = ConfigurationManager.AppSettings["LANGUAGE"].ToString();
            }
            catch
            {
            }
            finally
            {
                if (Language != "en")
                    Language = "ja";
            }
            return Language;
        }
        #endregion       

        #region Convertions

        /// <summary>
        /// Convert string to integer
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>int</returns>
        public static int Convert2Int(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    expression = expression.Replace(",", "");
                    int val = int.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }

        /// <summary>
        /// Convert string to short.
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>short</returns>
        public static short Convert2Short(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    short val = short.Parse(expression);
                    return (val);
                }
                catch
                {
                    return ((short)(0));
                }
            }
        }

        /// <summary>
        /// Convert string to long
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>long</returns>
        public static long Convert2Long(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    long val = long.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }

        /// <summary>
        /// Convert string to float
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>float</returns>
        public static float Convert2Float(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    float val = float.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }

        /// <summary>
        /// Convert string to Decimal
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>Decimal</returns>
        public static decimal Convert2Decimal(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    decimal val = decimal.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }


        /// <summary>
        /// Convert string to double
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>double</returns>
        public static double Convert2Double(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    expression = expression.Replace(",", "");
                    double val = double.Parse(expression);
                    val = Math.Round(val, 2);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }

        /// <summary>
        /// Convert string to byte
        /// </summary>
        /// <param name="expression">byte</param>
        /// <returns>byte</returns>
        public static byte Convert2Byte(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    expression = expression.Replace(",", "");
                    byte val = byte.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }


        /// <summary>
        /// Convert string to double and return the formatted string.
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>string</returns>
        public static string FormatDouble(string expression)
        {
            if (expression.Length == 0)
                return "0";
            else
            {
                try
                {
                    expression = expression.Replace(",", "");
                    double val = double.Parse(expression);
                    return val.ToString("N0");
                }
                catch
                {
                    return "0";
                }
            }
        }

        /// <summary>
        /// Convert string to DateTime
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>DateTime</returns>
        public static DateTime Convert2DateTime(string date)
        {
            DateTime myDate = new DateTime();
            string temp;
            try
            {
                if (date.Trim() == string.Empty) return myDate;
                myDate = Convert.ToDateTime(date);
                temp = myDate.ToString("yyyy/MM/dd hh:mm:ss tt");
                myDate = Convert.ToDateTime(temp);
                return myDate;
            }
            catch
            {
                return myDate;
            }
        }

        /// <summary>
        /// Convert string to DateTime
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>DateTime</returns>
        public static DateTime DateTimeConvert(string date)
        {
            DateTime myDate = new DateTime(1753, 1, 1);
            string temp;
            try
            {
                if (date.Trim() == string.Empty) return myDate;
                myDate = Convert.ToDateTime(date);
                temp = myDate.ToString("yyyy/MM/dd hh:mm:ss");
                myDate = Convert.ToDateTime(temp);
                return myDate;
            }
            catch
            {
                return myDate;
            }
        }

        /// <summary>
        /// Replace single quotes with double quotes
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>string</returns>
        public static string Convert2String(string expression)
        {
            if (expression.Length == 0)
                return "";
            else
            {
                try
                {
                    expression = expression.Replace("'", "''");
                    return Convert.ToString(expression);
                }
                catch
                {
                    return "";
                }
            }
        }

        
        #endregion

        #region Functions

        /// <summary>
        /// Validate date
        /// </summary>
        /// <param name="dts"> String date</param>
        /// <returns> True  - valid date 
        ///           False - Invalid date 
        /// </returns>
        public static bool IsDate(string dte)
        {
            DateTime date;
            try
            {
                date = DateTime.Parse(dte);
                if (date >= Convert2DateTime("1900/01/01") && date <= Convert2DateTime("2100/01/01"))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        public static int RandomNumber(int minimum, int maximum)
        {
            Random random = new Random();
            return random.Next(minimum, maximum);
        }

        public static void FilterNumeric(object sender, KeyEventArgs e)
        {

            InputMethod.Current.ImeState = InputMethodState.Off; 

            bool isNumPadNumeric = (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal;
            bool isNumeric = (e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod ;

            if ((isNumeric || isNumPadNumeric) && Keyboard.Modifiers != ModifierKeys.None)
            {
                e.Handled = true;
                return;
            }

            bool isControl = ((Keyboard.Modifiers != ModifierKeys.None && Keyboard.Modifiers != ModifierKeys.Shift)
                || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Insert
                || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up
                || e.Key == Key.Tab
                || e.Key == Key.PageDown || e.Key == Key.PageUp
                || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape
                || e.Key == Key.Home || e.Key == Key.End);

            e.Handled = !isControl && !isNumeric && !isNumPadNumeric;
        }

        public static void FilterDecimal(object sender, KeyEventArgs e)
        {

            InputMethod.Current.ImeState = InputMethodState.Off;

            bool isNumPadNumeric = (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);
            bool isNumeric = (e.Key >= Key.D0 && e.Key <= Key.D9);

            if ((isNumeric || isNumPadNumeric) && Keyboard.Modifiers != ModifierKeys.None)
            {
                e.Handled = true;
                return;
            }

            bool isControl = ((Keyboard.Modifiers != ModifierKeys.None && Keyboard.Modifiers != ModifierKeys.Shift)
                || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Insert
                || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up
                || e.Key == Key.Tab
                || e.Key == Key.PageDown || e.Key == Key.PageUp
                || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape
                || e.Key == Key.Home || e.Key == Key.End);

            e.Handled = !isControl && !isNumeric && !isNumPadNumeric;
        }

        public static void FilterNumericDate(object sender, KeyEventArgs e)
        {
            bool isNumPadNumeric = (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);
            bool isNumeric = (e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.Oem2 || e.Key == Key.Divide;

            if ((isNumeric || isNumPadNumeric) && Keyboard.Modifiers != ModifierKeys.None)
            {
                e.Handled = true;
                return;
            }

            bool isControl = ((Keyboard.Modifiers != ModifierKeys.None && Keyboard.Modifiers != ModifierKeys.Shift)
                || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Insert
                || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up
                || e.Key == Key.Tab
                || e.Key == Key.PageDown || e.Key == Key.PageUp
                || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape
                || e.Key == Key.Home || e.Key == Key.End);

            e.Handled = !isControl && !isNumeric && !isNumPadNumeric;
        }


        /// <summary>
        /// Check the Text Expression is numeric or not
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(string Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;

        }

        public static ArrayList SplitInput(string input)
        {
            string strvalue = string.Empty;
            ArrayList result = new ArrayList();
            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ',')
                    {
                        result.Add(strvalue);
                        strvalue = string.Empty;
                    }
                    else
                    {
                        strvalue = strvalue + input[i];
                    }
                }

                if (strvalue != string.Empty)
                    result.Add(strvalue);

                return result;
            }
            catch
            {
                return null;
            }
        }               

        public static string EncryptString(string p_sStr)
        {
            string sPwdRet = p_sStr.Trim();
            byte[] btPwd = GetCharArray(sPwdRet);
            ushort iUBArr = Convert.ToUInt16(btPwd.GetLength(0));
            sPwdRet = string.Empty;
            for (ushort iNo = 0; iNo < iUBArr; iNo++)
                sPwdRet += String.Format("{0:x2}", (btPwd[iNo] ^ 199)).PadLeft(2, '0');
            return (sPwdRet);
        }

        private static byte[] GetCharArray(string p_sStr)
        {
            Encoding ascii = Encoding.ASCII;
            char[] cStr = p_sStr.ToCharArray();
            byte[] btStr = ascii.GetBytes(cStr);
            return (btStr);
        }

        public static string DecryptString(string p_sStr)
        {
            string sPwdStr = p_sStr.Trim();
            byte[] btPwd = new byte[sPwdStr.Length / 2];
            ushort iUBArr = Convert.ToUInt16(sPwdStr.Length);
            for (ushort iNo = 0, iNum = 0; iNo < iUBArr; iNo += 2, iNum++)
            {
                btPwd[iNum] = Convert.ToByte(199 ^ Convert.ToByte(sPwdStr.Substring(iNo, 2), 16));
            }
            Encoding ascii = Encoding.ASCII;
            return (ascii.GetString(btPwd));
        }

        #endregion

        #region Populate ComboBox

        public static string GetComboLang(int languageID)
        {
            string selectLan = string.Empty;
            if (languageID == 1)
            {
                selectLan = "--- Select ---";
            }
            else
            {
                selectLan = "- തെരഞ്ഞെടുക്കുക -";
            }
            return selectLan;

        }        

        public static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);
        }


        #endregion        

        #region User Functions

        public static void FillFoodCategory(ComboBox cboFoodCategory)
        {
            try
            {
                List<NSysFoodCategory> foodCategoryList = new List<NSysFoodCategory>();
                foodCategoryList = NSysFoodCategoryManager.GetList();
                if (foodCategoryList != null)
                {
                    NSysFoodCategory foodCategory = new NSysFoodCategory();
                    foodCategory.FoodCategoryName = "---Select---";
                    foodCategory.FoodCategoryID = 0;
                    foodCategoryList.Insert(0, foodCategory);
                    cboFoodCategory.DisplayMemberPath = "FoodCategoryName";
                    cboFoodCategory.SelectedValuePath = "FoodCategoryID";
                    cboFoodCategory.ItemsSource = foodCategoryList;
                }
                cboFoodCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public static void FillMemberList(ComboBox cboMember)
        {
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetMemberNameList();
                if (memberList != null)
                {
                    Member member = new Member();
                    member.MemberName = "---Select---";
                    member.MemberID = 0;
                    memberList.Insert(0, member);
                    cboMember.DisplayMemberPath = "MemberName";
                    cboMember.SelectedValuePath = "MemberID";
                    cboMember.ItemsSource = memberList;
                }
                cboMember.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public static void FillDishCategory(ComboBox cboDishCategory)
        {
            try
            {
                List<NSysDishCategory> dishList = new List<NSysDishCategory>();
                dishList = NSysDishCategoryManager.GetList();
                if (dishList != null)
                {
                    NSysDishCategory dishCat = new NSysDishCategory();
                    dishCat.DishCategoryName = "-----------";
                    dishCat.DishCategoryID = 0;
                    dishList.Insert(0, dishCat);
                    cboDishCategory.DisplayMemberPath = "DishCategoryName";
                    cboDishCategory.SelectedValuePath = "DishCategoryID";
                    cboDishCategory.ItemsSource = dishList;
                }
                cboDishCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public static void FillEthnic(ComboBox cboEthnic)
        {
            try
            {
                List<Ethnic> ethnicList = new List<Ethnic>();
                ethnicList = EthnicManager.GetEthnic();
                if (ethnicList != null)
                {
                    Ethnic ethnic = new Ethnic();
                    ethnic.EthnicName = "---Select---";
                    ethnic.EthnicID = 0;
                    ethnicList.Insert(0, ethnic);
                    cboEthnic.DisplayMemberPath = "EthnicName";
                    cboEthnic.SelectedValuePath = "EthnicID";
                    cboEthnic.ItemsSource = ethnicList;
                }
                cboEthnic.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public static void FillFoodHabit(ComboBox cboFood)
        {
            try
            {
                List<NSysAdmin> adminList = new List<NSysAdmin>();
                adminList = NSysAdminManager.GetFoodHabit();
                if (adminList != null)
                {
                    NSysAdmin admin = new NSysAdmin();
                    admin.FoodHabitName = "---Select---";
                    admin.FoodHabitID = 0;
                    adminList.Insert(0, admin);
                    cboFood.DisplayMemberPath = "FoodHabitName";
                    cboFood.SelectedValuePath = "FoodHabitID";
                    cboFood.ItemsSource = adminList;
                }
                cboFood.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public static void FillServeUnit(ComboBox cboServeUnit)
        {
            try
            {
                List<NSysServeUnit> serveUnitList = new List<NSysServeUnit>();
                serveUnitList = NSysServeUnitManager.GetList();
                if (serveUnitList != null)
                {
                    NSysServeUnit serveUnit = new NSysServeUnit();
                    serveUnit.ServeUnitName = "---Select---";
                    serveUnit.ServeUnitID = 0;
                    serveUnitList.Insert(0, serveUnit);
                    cboServeUnit.DisplayMemberPath = "ServeUnitName";
                    cboServeUnit.SelectedValuePath = "ServeUnitID";
                    cboServeUnit.ItemsSource = serveUnitList;
                }
                cboServeUnit.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public static void FillIngredients(ComboBox cboIngredients)
        {
            try
            {
                string searchString = string.Empty;                
                searchString = " Where IsSystemIngredient = false";
                List<Ingredient> ingredientList = new List<Ingredient>();
                ingredientList = IngredientManager.GetList(searchString);
                if (ingredientList != null)
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient.Name = "---Select---";
                    ingredient.Id = 0;
                    ingredientList.Insert(0, ingredient);
                    cboIngredients.DisplayMemberPath = "Name";
                    cboIngredients.SelectedValuePath = "ID";
                    cboIngredients.ItemsSource = ingredientList;
                }
                cboIngredients.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public static bool IsDishExists(List<Dish> DishList, int dishID)
        {
            try
            {
                bool IsExists = false;
                foreach (Dish DishItem in DishList)
                {
                    if (DishItem.Id == dishID)
                    {
                        IsExists = true;
                        return IsExists;
                    }
                }
                return IsExists;
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

        public static void FillNutrients(ComboBox cboNutrient)
        {
            try
            {
                List<NSysNutrient> sysNutrientList = new List<NSysNutrient>();
                sysNutrientList = NSysNutrientManager.GetListNutrient();
                if (sysNutrientList != null)
                {
                    NSysNutrient sysNutrient = new NSysNutrient();
                    sysNutrient.NutrientParam = "--- Select ---";
                    sysNutrient.NutrientID = 0;
                    sysNutrientList.Insert(0, sysNutrient);
                    cboNutrient.DisplayMemberPath = "NutrientParam";
                    cboNutrient.SelectedValuePath = "NutrientID";
                    cboNutrient.ItemsSource = sysNutrientList;
                    cboNutrient.SelectedIndex = 0;
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

        public static bool IsIngredientExists(List<Ingredient> IngredientList, int ingredientID)
        {
            try
            {
                bool IsExists = false;
                foreach (Ingredient IngredientItem in IngredientList)
                {
                    if (IngredientItem.Id == ingredientID)
                    {
                        IsExists = true;
                        return IsExists;
                    }
                }
                return IsExists;
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

        public static void SetControlFocus(UIElement element)
        {
            element.Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(delegate()
            {
                element.Focus();
            }));
        }

        public static double RoundDown(double input, int digits)
        {

            string strPrimary, strTemp, strDecimal, strfinal;
            strPrimary = input.ToString();
            string strNum;
            int decim;
            int leng;
            int end;
            try
            {
                leng = strPrimary.Length;
                decim = strPrimary.IndexOf(".") + 1;
                if (decim <= 0)
                    return Convert.ToDouble(strPrimary);

                end = leng - decim;
                strNum = strPrimary.Substring(0, decim);
                strTemp = strPrimary.Substring(decim, end);
                if (strTemp.Length > digits)
                {
                    strDecimal = strTemp.Substring(0, digits);
                    strfinal = strNum + strDecimal;
                }
                else
                    strfinal = strNum + strTemp;
                return Convert.ToDouble(strfinal);
            }
            catch
            {
                return 0;
            }
        }

        public static double RoundUp(double number, int digits)
        {
            string temp;
            string temp2;
            string digitAdd;
            int i, j;
            double resultValue = 0;
            double numberTemp;
            temp = Convert.ToString(number);
            i = temp.LastIndexOf(".");
            if (((temp.Length - (i + 1)) <= digits) || (i == -1))
            {
                number = Convert2Double(temp);
                return number;
            }

            temp2 = temp.Substring(i + digits + 1, 1);
            j = Convert2Int(temp2);
            numberTemp = Convert2Double(temp.Substring(0, i + digits + 1));

            if (j > 5)
            {
                resultValue = Math.Round(number, digits);
            }
            else if (j == 5)
            {
                digitAdd = "0.";
                for (int iNum = 0; iNum <= digits; iNum++)
                {
                    if (iNum == digits)
                    {
                        digitAdd = digitAdd + "1";
                    }
                    else
                    {
                        digitAdd = digitAdd + "0";
                    }
                }
                resultValue = Math.Round((number + Convert2Double(digitAdd)), digits);
            }
            else
            {
                resultValue = numberTemp + (1 / (Math.Pow(10, digits)));
            }
            return resultValue;
        }

        public static int SaveAsXps(string fileName)
        {
            object doc;
            FileInfo fileInfo = new FileInfo(fileName);
            using (FileStream file = fileInfo.OpenRead())
            {
                System.Windows.Markup.ParserContext context = new System.Windows.Markup.ParserContext();
                context.BaseUri = new Uri(fileInfo.FullName, UriKind.Absolute);
                doc = System.Windows.Markup.XamlReader.Load(file, context);
            }

            if (!(doc is IDocumentPaginatorSource))
            {
                Console.WriteLine("DocumentPaginatorSource expected");
                return -1;
            }

            using (Package container = Package.Open(fileName + ".xps", FileMode.Create))
            {
                using (XpsDocument xpsDoc = new XpsDocument(container, CompressionOption.Maximum))
                {
                    XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
                    DocumentPaginator paginator = ((IDocumentPaginatorSource)doc).DocumentPaginator;

                    // 8 inch x 6 inch, with half inch margin
                    paginator = new DocumentPaginatorWrapper(paginator, new Size(768, 676), new Size(48, 48));
                    rsm.SaveAsXaml(paginator);
                }
            }
            Console.WriteLine("{0} generated.", fileName + ".xps");
            return 0;
        }

        public static string CalculatePersonalAbnormalitiesNew(int FamilyID, int MemberID)
        {
            string PersonalAbnormalities = string.Empty;
            float Height = 0, Weight = 0, HeightinInches = 0, WeightinPounds = 0;
            double BodyMassIndexValue = 0, BodyMassIndexPercentile = 0;
            int SexID, Age;

            try
            {
                List<Member> memberListBasic = new List<Member>();
                List<Member> memberListGeneral = new List<Member>();
                memberListBasic = MemberManager.GetListFamilyMember(FamilyID, MemberID);
                if (memberListBasic.Count > 0)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberListBasic[0].SexID));
                    Age = GetMemberAge(Convert.ToDateTime(memberListBasic[0].DOB));

                    if (memberListGeneral.Count > 0)
                    {
                        Height = Classes.CommonFunctions.Convert2Float(Convert.ToString(memberListGeneral[0].Height));
                        Weight = Classes.CommonFunctions.Convert2Float(Convert.ToString(memberListGeneral[0].Weight));
                        HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString((memberListGeneral[0].Height) * CalorieFormula.CM_INCH));
                        WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString((memberListGeneral[0].Weight) * CalorieFormula.KG_POUND));

                        if (HeightinInches != 0 && WeightinPounds != 0)
                        {
                            if (CheckMajor(FamilyID, MemberID) == true)
                            {
                                BodyMassIndexValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Math.Round((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIWEIGHT))) / (Math.Pow((HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(BodyMassIndexFormula.BMIHEIGHT))), 2)), 2)));

                                if (BodyMassIndexValue > 0)
                                {
                                    if (BodyMassIndexValue <= BodyMassIndex.BMIUNDERWEIGHT)
                                    {
                                        PersonalAbnormalities = "UnderWeight";
                                    }
                                    else if (BodyMassIndexValue > BodyMassIndex.BMINORMAL_LOW && BodyMassIndexValue <= BodyMassIndex.BMINORMAL_HIGH2)
                                    {
                                        PersonalAbnormalities = "NormalWeight";
                                    }
                                    else if (BodyMassIndexValue >= BodyMassIndex.BMIOVERWEIGHT_LOW2 && BodyMassIndexValue <= BodyMassIndex.BMIOVERWEIGHT_HIGH2)
                                    {
                                        PersonalAbnormalities = "OverWeight";
                                    }
                                    else if (BodyMassIndexValue >= BodyMassIndex.BMIOVEROBESE2)
                                    {
                                        PersonalAbnormalities = "Obese";
                                    }
                                }
                            }
                            else
                            {
                                BodyMassIndexValue = Classes.CommonFunctions.Convert2Double(Convert.ToString(Math.Round(((WeightinPounds / Math.Pow(HeightinInches, 2)) * 703), 1)));

                                if (Age > 0)
                                {
                                    BodyMassIndexPercentile = CalorieCalculatorManager.GetBMIPercentile(Age, SexID, BodyMassIndexValue);

                                    if (BodyMassIndexPercentile >= 0)
                                    {
                                        if (BodyMassIndexPercentile < BMIPercentile.BMIUNDERWEIGHT)
                                        {
                                            PersonalAbnormalities = "UnderWeight";
                                        }
                                        else if (BodyMassIndexPercentile >= BMIPercentile.BMINORMAL_LOW && BodyMassIndexPercentile <= BMIPercentile.BMINORMAL_HIGH)
                                        {
                                            PersonalAbnormalities = "NormalWeight";
                                        }
                                        else if (BodyMassIndexPercentile > BMIPercentile.BMIOVERWEIGHT_LOW && BodyMassIndexPercentile <= BMIPercentile.BMIOVERWEIGHT_HIGH)
                                        {
                                            PersonalAbnormalities = "OverWeight";
                                        }
                                        else if (BodyMassIndexPercentile > BMIPercentile.BMIOVEROBESE)
                                        {
                                            PersonalAbnormalities = "Obese";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return PersonalAbnormalities;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return PersonalAbnormalities;
            }
            finally
            {

            }
        }

        public static double CalculateCalorieInTake(int FamilyID, int MemberID)
        {
            int SexID, Age, LifeStyleID, LactationType;
            float HeightinInches, WeightinPounds, LifeStyleValue = 0;
            double CalorieRequired = 0, ExerciseCalorie = 0;
            double Height = 0, Weight = 0;
            bool IsPregnant, IsLactation;

            List<Member> memberListBasic = new List<Member>();            
            memberListBasic = MemberManager.GetListFamilyMember(FamilyID, MemberID);
            try
            {
                if (memberListBasic.Count > 0)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberListBasic[0].SexID));
                    Age = GetMemberAge(Convert.ToDateTime(memberListBasic[0].DOB));
                    LifeStyleID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberListBasic[0].LifeStyleID));
                    IsPregnant = Convert.ToBoolean(Convert.ToString(memberListBasic[0].Pregnancy));
                    IsLactation = Convert.ToBoolean(Convert.ToString(memberListBasic[0].Lactation));
                    LactationType = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberListBasic[0].LactationType));
                    Height = Classes.CommonFunctions.Convert2Float(Convert.ToString(memberListBasic[0].Height));
                    Weight = Classes.CommonFunctions.Convert2Float(Convert.ToString(memberListBasic[0].Weight));
                    if (Height != 0 && Weight != 0)
                    {
                        HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString(Convert.ToDouble(Height) * CalorieFormula.CM_INCH));
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
                                    CalorieRequired = Math.Round(CalculateChildrenCalorie(Age, SexID, Classes.CommonFunctions.Convert2Double(Convert.ToString((memberListBasic[0].Weight)))), 0);
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
                                    CalorieRequired = Math.Round(CalculateChildrenCalorie(Age, SexID, Classes.CommonFunctions.Convert2Double(Convert.ToString((memberListBasic[0].Weight)))), 0);
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

        public static int GetMemberAge(DateTime DOB)
        {
            int MemberAge;
            DateTime Now = System.DateTime.Now;
            TimeSpan ts = Now.Subtract(DOB);
            MemberAge = Classes.CommonFunctions.Convert2Int(Convert.ToString(ts.Days)) / (int)DayType.Days_Year;
            return MemberAge;
        }

        private static bool CheckMajor(int FamilyID, int MemberID)
        {
            int Age = 0;
            bool IsMajor = false;

            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetListFamilyMember(FamilyID, MemberID);

                if (memberList.Count > 0)
                {
                    Age = GetMemberAge(Convert.ToDateTime(memberList[0].DOB));

                    if (Age >= 18)
                    {
                        IsMajor = true;
                    }
                    else
                    {
                        IsMajor = false;
                    }
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

        public static double CalculateChildrenCalorie(int Age, int SexID, double Weight)
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
        
        #endregion

        #region Excel Report

        //public static void ExportToExcel(this ListView listView,string fileName, string worksheetName)
        //{
            //if (listView == null)
            //{
            //    throw new ArgumentNullException("ListView");
            //}

            //var excel = new Microsoft.Office.Interop.Excel.Application();

            //try
            //{
            //    Workbook book = null;
            //    if (File.Exists(fileName))
            //    {
            //        book = excel.Workbooks.Open(fileName);
            //    }
            //    else
            //    {
            //        book = excel.Workbooks.Add();
            //    }

            //    dynamic sheet = book.Sheets.Add();
                
            //    sheet.Name = worksheetName;

            //    foreach (ListViewItem lvi in listView.Items)
            //    {
            //        DataRowView drv = (DataRowView)lvi.Content;
            //        string thisRowsValueForColumnName = drv[0].ToString();
            //    }

            //    for (int i = 0; i < listView.Items.Count; i++)
            //    {
            //        sheet.Cells[1, i + 1] = "";
            //    }                
            //    sheet.get_Range(sheet.Cells[1, 1],sheet.Cells[1, dataGridView.Columns.Count]).Font.Bold = true;

            //    for (int row = 0; row < dataGridView.Rows.Count; row++)
            //    {
            //        for (int column = 0; column < dataGridView.Columns.Count; column++)
            //        {
            //            sheet.Cells[row + 2, column + 1] = dataGridView.Rows[row].Cells[column].Value;
            //        }
            //    }
            //    book.SaveAs(fileName, AccessMode: XlSaveAsAccessMode.xlShared);
            //    book.Close();
            //}
            //finally
            //{
            //    excel.Workbooks.Close();
            //    excel.Quit();
            //}
        //}

        #endregion
    }
}

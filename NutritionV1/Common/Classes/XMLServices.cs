using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Linq;
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
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Common.Classes;
using NutritionV1.Classes;

namespace NutritionV1.Common.Classes
{
   public  static class XMLServices
    {
        #region Get current culture

        public static string getLanguage()
        {
            string Language = string.Empty;
            try
            {
                App apps = (App)Application.Current;
                Language = "en-US";
            }
            catch
            {
            }
            finally
            {
               
            }
            return Language;
        }

        #endregion
       
        public static string GetXmlMessage(string msgCode)
       {
           XmlDocument xmlDoc = new XmlDocument();
           string filePath;
           filePath = AppDomain.CurrentDomain.BaseDirectory + "\\XML\\Messages.xml";
           try
           {
               XmlReader xr = XmlReader.Create(new StreamReader(filePath, Encoding.Default));
               xmlDoc.Load(xr);
               XmlNode xmlNode = xmlDoc.SelectSingleNode("//message/code[.='" + msgCode + "']");
               if (!(xmlNode == null))
               {
                   return xmlNode.NextSibling.InnerText;
               }
               else
               {
                   return "";
               }
           }
           catch(Exception ex) 
           {
               MessageBox.Show(ex.ToString());
               return ""; 
           }
       }

       public static void GetXMLData(ComboBox cboSource, FileInfo file)
       {
           try
           {
               DataTable dtData = new DataTable();
               XmlTextReader reader = new XmlTextReader(file.FullName);

               dtData.Columns.Add("ID");
               dtData.Columns.Add("Name");
               DataRow firstRow = dtData.NewRow();
               firstRow["ID"] = "0";
               firstRow["Name"] = "---Select---";
               dtData.Rows.Add(firstRow);

               while (reader.Read())
               {
                   switch (reader.NodeType)
                   {
                       case XmlNodeType.Element:
                           if (reader["ID"] != null)
                           {
                               DataRow dr = dtData.NewRow();
                               dr["ID"] = reader["ID"];
                               dr["Name"] = reader["Name"];
                               dtData.Rows.Add(dr);
                           }
                           break;
                   }
               }

               cboSource.SelectedValuePath = "ID";
               cboSource.DisplayMemberPath = "Name";
               cboSource.ItemsSource = dtData.DefaultView;
               cboSource.SelectedIndex = 0;
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.ToString());
           }
       }

       public static void GetXMLData(ComboBox cboSource, FileInfo file,bool IsSelect)
       {
           try
           {
               DataTable dtData = new DataTable();
               XmlTextReader reader = new XmlTextReader(file.FullName);

               dtData.Columns.Add("ID");
               dtData.Columns.Add("Name");

               if (IsSelect == true)
               {
                   DataRow firstRow = dtData.NewRow();
                   firstRow["ID"] = "0";
                   firstRow["Name"] = "---Select---";
                   dtData.Rows.Add(firstRow);
               }

               while (reader.Read())
               {
                   switch (reader.NodeType)
                   {
                       case XmlNodeType.Element:
                           if (reader["ID"] != null)
                           {
                               DataRow dr = dtData.NewRow();
                               dr["ID"] = reader["ID"];
                               dr["Name"] = reader["Name"];
                               dtData.Rows.Add(dr);
                           }
                           break;
                   }
               }

               cboSource.SelectedValuePath = "ID";
               cboSource.DisplayMemberPath = "Name";
               cboSource.ItemsSource = dtData.DefaultView;
               cboSource.SelectedIndex = 0;
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.ToString());
           }
       }

       public static void GetXMLDataLogin(ComboBox cboSource, FileInfo file)
       {
           try
           {
               DataTable dtData = new DataTable();               
               dtData.Columns.Add("ID");
               dtData.Columns.Add("Name");

               XmlTextReader reader = new XmlTextReader(file.FullName);
               while (reader.Read())
               {
                   switch (reader.NodeType)
                   {
                       case XmlNodeType.Element:
                           if (reader["ID"] != null)
                           {
                               DataRow dr = dtData.NewRow();
                               dr["ID"] = reader["ID"];
                               dr["Name"] = reader["Name"];
                               dtData.Rows.Add(dr);
                           }
                           break;
                   }
               }

               cboSource.SelectedValuePath = "ID";
               cboSource.DisplayMemberPath = "Name";
               cboSource.ItemsSource = dtData.DefaultView;
               cboSource.SelectedIndex = 0;
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.ToString());
           }
       }

       public static void GetXMLDataByLanguage(ComboBox cboSource, string fileName,int type)
       {
           DataTable dtData = new DataTable();
          
           XmlDocument xmlDoc = new XmlDocument();
           XmlNodeList xlist = null;

           dtData.Columns.Add("ID");
           dtData.Columns.Add("Name");
           DataRow firstRow = dtData.NewRow();
           firstRow["ID"] = "0";           
           firstRow["Name"] = "---Select---";

           dtData.Rows.Add(firstRow);
           xmlDoc.Load(fileName);
           if (type == 1)
           {
               xlist = xmlDoc.SelectNodes("//Nutrtions/Nutrtion");
           }
           else if (type == 2)
           {
               xlist = xmlDoc.SelectNodes("//Suitables/Suitable");
           }
           else
           {
               xlist = xmlDoc.SelectNodes("//ServeUnit/ServeUnitList");
           }
           foreach (XmlElement xelement in xlist)
           {
               XmlNodeList childList = xelement.ChildNodes;
               foreach (XmlElement childElement in childList)
               {
                   DataRow dr = dtData.NewRow();
                   dr["ID"] = CommonFunctions.Convert2Int(childElement.Attributes[0].InnerText);
                   dr["Name"] = Convert.ToString(childElement.Attributes[1].InnerText);
                   dtData.Rows.Add(dr);
               }
           }
           cboSource.SelectedValuePath = "ID";
           cboSource.DisplayMemberPath = "Name";
           cboSource.ItemsSource = dtData.DefaultView;
           cboSource.SelectedIndex = 0;
       }

       public static DataTable  GetXMLData(string fileName, int type)
       {
           DataTable dtData = new DataTable();

           XmlDocument xmlDoc = new XmlDocument();
           XmlNodeList xlist = null;

           dtData.Columns.Add("ID");
           dtData.Columns.Add("Name");
           DataRow firstRow = dtData.NewRow();
           firstRow["ID"] = "0";
           firstRow["Name"] = "---Select---";
           
           dtData.Rows.Add(firstRow);
           xmlDoc.Load(fileName);
           if (type == 1)
           {
               xlist = xmlDoc.SelectNodes("//Nutrtions/Nutrtion");
           }
           else if (type == 2)
           {
               xlist = xmlDoc.SelectNodes("//Suitables/Suitable");
           }
           else
           {
               xlist = xmlDoc.SelectNodes("//ServeUnit/ServeUnitList");
           }
           foreach (XmlElement xelement in xlist)
           {

               XmlNodeList childList = xelement.ChildNodes;

               foreach (XmlElement childElement in childList)
               {
                   DataRow dr = dtData.NewRow();
                   dr["ID"] = CommonFunctions.Convert2Int(childElement.Attributes[0].InnerText);
                   dr["Name"] = Convert.ToString(childElement.Attributes[1].InnerText);
                   dtData.Rows.Add(dr);
               }
           }
           return dtData;
       }

       public static string GetXMLQuotes()
       {
           try
           {
               DataTable dtData = new DataTable();
               string fileName = AppDomain.CurrentDomain.BaseDirectory + "XML\\Quotes.xml";
               XmlDocument xmlDoc = new XmlDocument();
               XmlNodeList xlist = null;
               dtData.Columns.Add("ID");
               dtData.Columns.Add("Quote");
               xmlDoc.Load(fileName);
               xlist = xmlDoc.SelectNodes("Quotes");
               foreach (XmlElement xelement in xlist)
               {
                   XmlNodeList childList = xelement.ChildNodes;

                   foreach (XmlElement childElement in childList)
                   {
                       DataRow dr = dtData.NewRow();
                       dr["ID"] = CommonFunctions.Convert2Int(childElement.Attributes[0].InnerText);
                       dr["Quote"] = Convert.ToString(childElement.Attributes[1].InnerText);
                       dtData.Rows.Add(dr);
                   }
               }
               return dtData.Rows[CommonFunctions.RandomNumber(0, dtData.Rows.Count - 1)][1].ToString();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               return "";
           }
       }

       public static string GetServeUnitName(int serveUnitID)
       {
           string serverUnitName = string.Empty;
           XmlDocument xmlDoc = new XmlDocument();
           XmlNodeList xlist = null;
           xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\ServeUnit.xml");
           xlist = xmlDoc.SelectNodes("//ServeUnit/ServeUnitList");
           foreach (XmlElement xelement in xlist)
           {
               XmlNodeList childList = xelement.ChildNodes;
               foreach (XmlElement childElement in childList)
               {
                   if (serveUnitID == CommonFunctions.Convert2Int(childElement.Attributes[0].InnerText))
                   {
                       serverUnitName = Convert.ToString(childElement.Attributes[1].InnerText);
                   }
               }
           }
           
           return serverUnitName;
       }
    }
}

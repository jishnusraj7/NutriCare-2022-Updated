using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Configuration;

namespace NutritionV1
{
   public static class AlertBox
    {

       public static bool Show(string Message)
       {
           ShowAlertBox alertdisplay = new ShowAlertBox();
           alertdisplay.MessageContent = Message;
           alertdisplay.MessageSubject = string.Empty;
           alertdisplay.MessageAlertType = 1;
           alertdisplay.MessageTitle = Convert.ToString(ConfigurationManager.AppSettings["ProductName"]);
           alertdisplay.CancelContent = "OK";
           alertdisplay.OKContent = string.Empty;
           alertdisplay.Owner = Application.Current.MainWindow;
           alertdisplay.ShowDialog();
           return alertdisplay.returnResult;
       }

       public static bool Show(string Message,string Title)
       {
           ShowAlertBox alertdisplay = new ShowAlertBox();
           alertdisplay.MessageContent = Message;
           alertdisplay.MessageSubject = Title;
           alertdisplay.MessageAlertType = 1;
           alertdisplay.MessageTitle = Convert.ToString(ConfigurationManager.AppSettings["ProductName"]);
           alertdisplay.CancelContent = "OK";
           alertdisplay.OKContent = string.Empty;
           alertdisplay.Owner = Application.Current.MainWindow;
           alertdisplay.ShowDialog();

           return alertdisplay.returnResult;
       }

       public static bool Show(string Message,string Title,AlertType alttype,AlertButtons altbuttons)
       {
           int atype=1;
           ShowAlertBox alertdisplay = new ShowAlertBox();
           alertdisplay.MessageContent = Message;
           alertdisplay.MessageSubject = Title;
           alertdisplay.MessageTitle = Convert.ToString(ConfigurationManager.AppSettings["ProductName"]);
           if (alttype == AlertType.Information)
           {
               atype = 1;
           }
           else if (alttype == AlertType.Exclamation)
           {
               atype = 2;
           }
           else if (alttype == AlertType.Error)
           {
               atype = 3;
           }
           alertdisplay.MessageAlertType = atype;

           if (altbuttons == AlertButtons.OKCancel)
           {
               alertdisplay.OKContent = "OK";
               alertdisplay.CancelContent = "Cancel";
           }
           else if (altbuttons == AlertButtons.YESNO)
           {
               alertdisplay.OKContent = "Yes";
               alertdisplay.CancelContent = "No";
           }
           else if (altbuttons == AlertButtons.OK)
           {
               alertdisplay.CancelContent = "OK";
               alertdisplay.OKContent = string.Empty; 
           }
           alertdisplay.TabIndex = 0;
           alertdisplay.Owner = Application.Current.MainWindow;
           alertdisplay.ShowDialog();
           return alertdisplay.returnResult;
       }

       public static bool Show(string MessageCode, AlertType alttype, AlertButtons altbuttons)
       {
           App apps = (App)Application.Current;
           string Title = string.Empty;
           int atype = 1;
           ShowAlertBox alertdisplay = new ShowAlertBox();
           alertdisplay.MessageContent = Common.Classes.XMLServices.GetXmlMessage(MessageCode);
           alertdisplay.MessageSubject = Title;
           alertdisplay.MessageTitle = Convert.ToString(ConfigurationManager.AppSettings["ProductName"]);
           if (alttype == AlertType.Information)
           {
               atype = 1;
           }
           else if (alttype == AlertType.Exclamation)
           {
               atype = 2;
           }
           else if (alttype == AlertType.Error)
           {
               atype = 3;
           }
           alertdisplay.MessageAlertType = atype;

           if (altbuttons == AlertButtons.OKCancel)
           {
               alertdisplay.OKContent = "OK";
               alertdisplay.CancelContent = "Cancel";
           }
           else if (altbuttons == AlertButtons.YESNO)
           {
               alertdisplay.OKContent = "Yes";
               alertdisplay.CancelContent = "No";
           }
           else if (altbuttons == AlertButtons.OK)
           {
               alertdisplay.CancelContent = "OK";
               alertdisplay.OKContent = string.Empty;
           }
           alertdisplay.Owner = Application.Current.MainWindow;
           alertdisplay.ShowDialog();
           return alertdisplay.returnResult;
       }
    }

  
}

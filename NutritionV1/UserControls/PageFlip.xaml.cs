using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace NutritionV1
{
	public partial class pageFlip
	{
		public pageFlip()
		{
			this.InitializeComponent();
		}

        private void setTheme()
        {
            App apps = (App)Application.Current;
            recMain.Style = (Style)apps.SetStyle["LeftBarStyle"];
        }

        public bool setThemes
        {
            set
            {
                if (value == true)
                {
                    setTheme();
                }
            }
        }
        public int ControlHeight
        {
            set
            {
                this.Height = value;
                recMain.Height = value;
            }
        }
        public int ControlWidth
        {
            set
            {
                this.Width = value;
                recMain.Width = value;
            }
        }


	}
}
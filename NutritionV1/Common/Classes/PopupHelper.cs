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
using System.Windows.Media.Animation;

namespace NutritionV1
{
	public class PopupHelper
	{
		public static void ShowPopUp(double winHeight, double winWidth, double popLeft, double popTop,ImageSource bgImg, string msgText, Thickness msgPadding)
		{
			Window popUp = new Window();
			popUp.Name = "PopUp";
			popUp.AllowsTransparency = true;
			popUp.Background = Brushes.Transparent;
			popUp.WindowStyle = WindowStyle.None;
			popUp.ShowInTaskbar = false;
			popUp.Topmost = true;
			popUp.Height = winHeight;
			popUp.Width = winWidth;
            popUp.Left = popLeft - 230;
            popUp.Top = popTop - 50;
            popUp.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                popUp.Close();
            };

			//Create a inner Grid
			Grid g = new Grid();

			//Create a Image for irregular background display
			Image img = new Image();
			img.Stretch = Stretch.Fill;
			img.Source = bgImg;
			img.BitmapEffect = new System.Windows.Media.Effects.DropShadowBitmapEffect();
			g.Children.Add(img);

			//Create a TextBlock for message display
			TextBlock msg = new TextBlock();
			msg.Padding = msgPadding;
			msg.VerticalAlignment = VerticalAlignment.Center;
			msg.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
			msg.TextWrapping = TextWrapping.Wrap;
			msg.Text = msgText;
			g.Children.Add(msg);

            //Create a CloseButton for closing the popup
            TextBlock close = new TextBlock();
            close.VerticalAlignment = VerticalAlignment.Top;
            close.Margin = new Thickness(250,18,0,0);
            close.Text = "Close";
            close.Foreground = Brushes.Red;
            close.FontWeight = FontWeights.Bold;
            close.Cursor = Cursors.Hand;
            close.MouseDown += delegate(object sender, MouseButtonEventArgs e)
            {
                popUp.Close();
            };
            g.Children.Add(close);           
			popUp.Content = g;

			//Register the window's name, this is necessary for creating Storyboard using codes instead of XAML
			NameScope.SetNameScope(popUp, new NameScope());
			popUp.RegisterName(popUp.Name, popUp);

			//Create the fade in & fade out animation
            DoubleAnimation winFadeAni = new DoubleAnimation();
            winFadeAni.From = 0;
            winFadeAni.To = 30;
            winFadeAni.Duration = new Duration(TimeSpan.FromSeconds(30));
            winFadeAni.AutoReverse = true;
            winFadeAni.Completed += delegate(object sender, EventArgs e)
            {
                popUp.Close();
            };

			// Configure the animation to target the window's opacity property
            Storyboard.SetTargetName(winFadeAni, popUp.Name);
            Storyboard.SetTargetProperty(winFadeAni, new PropertyPath(Window.OpacityProperty));

			// Add the fade in & fade out animation to the Storyboard
            Storyboard winFadeStoryBoard = new Storyboard();
            winFadeStoryBoard.Children.Add(winFadeAni);

			// Set event trigger, make this animation played on window.Loaded
            popUp.Loaded += delegate(object sender, RoutedEventArgs e)
            {
                winFadeStoryBoard.Begin(popUp);
            };

			//Finally show the window
			popUp.ShowDialog();
		}


        public static void ShowAutomaticPopUp(double winHeight, double winWidth, double popLeft, double popTop, ImageSource bgImg, string msgText, Thickness msgPadding)
        {
            Window popUp = new Window();
            popUp.Name = "PopUp";
            popUp.AllowsTransparency = true;
            popUp.Background = Brushes.Transparent;
            popUp.WindowStyle = WindowStyle.None;
            popUp.ShowInTaskbar = false;
            popUp.Topmost = true;
            popUp.Height = winHeight;
            popUp.Width = winWidth;
            popUp.Left = popLeft;
            popUp.Top = popTop;

            //Create a inner Grid
            Grid g = new Grid();

            //Create a Image for irregular background display
            Image img = new Image();
            img.Stretch = Stretch.Fill;
            img.Source = bgImg;
            img.BitmapEffect = new System.Windows.Media.Effects.DropShadowBitmapEffect();
            g.Children.Add(img);

            //Create a TextBlock for message display
            TextBlock msg = new TextBlock();
            msg.Padding = msgPadding;
            msg.VerticalAlignment = VerticalAlignment.Center;
            msg.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            msg.TextWrapping = TextWrapping.Wrap;
            msg.Text = msgText;
            g.Children.Add(msg);
            popUp.Content = g;

            //Register the window's name, this is necessary for creating Storyboard using codes instead of XAML
            NameScope.SetNameScope(popUp, new NameScope());
            popUp.RegisterName(popUp.Name, popUp);

            //Create the fade in & fade out animation
            DoubleAnimation winFadeAni = new DoubleAnimation();
            winFadeAni.From = 0;
            winFadeAni.To = 1;
            winFadeAni.Duration = new Duration(TimeSpan.FromSeconds(1));
            winFadeAni.AutoReverse = true;
            winFadeAni.Completed += delegate(object sender, EventArgs e)
            {
                popUp.Close();
            };

            // Configure the animation to target the window's opacity property
            Storyboard.SetTargetName(winFadeAni, popUp.Name);
            Storyboard.SetTargetProperty(winFadeAni, new PropertyPath(Window.OpacityProperty));

            // Add the fade in & fade out animation to the Storyboard
            Storyboard winFadeStoryBoard = new Storyboard();
            winFadeStoryBoard.Children.Add(winFadeAni);

            // Set event trigger, make this animation played on window.Loaded
            popUp.Loaded += delegate(object sender, RoutedEventArgs e)
            {
                winFadeStoryBoard.Begin(popUp);
            };

            //Finally show the window
            popUp.Show();
        }
	}
}

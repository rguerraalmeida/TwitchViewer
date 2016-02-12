using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TwitchClientViewer.Views.Following
{
    public class FollowingTemplateSelector : DataTemplateSelector
    {
        //public override DataTemplate SelectTemplate(object item, DependencyObject container)
        //{
        //    string viewMode = item as string;
        //    Window window = Application.Current.MainWindow;

        //    if (viewMode == null) return window.FindResource("numberTemplate") as DataTemplate;


        //    if (viewMode != null)
        //    {
        //        int num;
        //        Window win = Application.Current.MainWindow;

        //        try
        //        {
        //            num = Convert.ToInt32(numberStr);
        //        }
        //        catch
        //        {
        //            return null;
        //        }

        //        // Select one of the DataTemplate objects, based on the 
        //        // value of the selected item in the ComboBox.
        //        if (num < 5)
        //        {
        //            return win.FindResource("numberTemplate") as DataTemplate;
        //        }
        //        else
        //        {
        //            return win.FindResource("largeNumberTemplate") as DataTemplate;

        //        }
        //    }

        //    return null;
        //}

    }
}

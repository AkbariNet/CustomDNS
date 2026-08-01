using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace EasyTask.Class
{
    class RunStoryboard
    {
        public static bool Run(string NameOfStoryborad, Window WindowOfRunStoryboard, UserControl UsercontrolOfRunStoryboard, DependencyObject ElementOfStartStoryboradOnMainElement)
        {
            if (UsercontrolOfRunStoryboard is null)
            {
                try
                {

                    Storyboard sb = WindowOfRunStoryboard.FindResource(NameOfStoryborad) as Storyboard;
                    Storyboard.SetTarget(sb, ElementOfStartStoryboradOnMainElement);
                    sb.Begin();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }

            }
            else if (WindowOfRunStoryboard is null)
            {
                try
                {

                    Storyboard sb = UsercontrolOfRunStoryboard.FindResource(NameOfStoryborad) as Storyboard;
                    Storyboard.SetTarget(sb, ElementOfStartStoryboradOnMainElement);
                    sb.Begin();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

    }
}

using System.Text.RegularExpressions;
using System.Windows.Input;

namespace EasyTask.Class
{
    // for check TextBox for only NUMBER ~~
    class ImportNumberOnly
    {
        public static void onlyNumKeyPreviewDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // for dont add space
            if (e.Key == Key.Space)
                e.Handled = true;

        }
        public static void onlyNumPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // for limit To Number
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }


        public static bool IPChecker(string _IpPartRequest)
        {
            int IpPartRequest;
            try
            {

                 IpPartRequest = Convert.ToInt32(_IpPartRequest);

                if (IpPartRequest >= 1 && IpPartRequest < 255)
                {

                    return true;

                }

                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
           
        }
    }
}

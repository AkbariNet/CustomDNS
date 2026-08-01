using CustomDNS.Data.DataMethod;
using CustomDNS.Method.Selector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomDNS.Extra.ConfigControl
{
    /// <summary>
    /// Interaction logic for ConfigControler.xaml
    /// </summary>
    public partial class ConfigController : Button
    {


        public string DNSName
        {
            get { return (string)GetValue(DNSNameProperty); }
            set { SetValue(DNSNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DNSName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DNSNameProperty =
            DependencyProperty.Register("DNSName", typeof(string), typeof(ConfigController), new PropertyMetadata("DNS NAME"));



        public string DNSValue1
        {
            get { return (string)GetValue(DNSValue1Property); }
            set { SetValue(DNSValue1Property, value); }
        }

        // Using a DependencyProperty as the backing store for DNSValue1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DNSValue1Property =
            DependencyProperty.Register("DNSValue1", typeof(string), typeof(ConfigController), new PropertyMetadata("0.0.0.0"));


        public string DNSValue2
        {
            get { return (string)GetValue(DNSValue2Property); }
            set { SetValue(DNSValue2Property, value); }
        }

        // Using a DependencyProperty as the backing store for DNSValue2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DNSValue2Property =
            DependencyProperty.Register("DNSValue2", typeof(string), typeof(ConfigController), new PropertyMetadata("0.0.0.0"));



        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(ConfigController), new PropertyMetadata(null));




        public event Action IsEnabledEvent;
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { 
                SetValue(IsEnabledProperty, value);
                IsEnabledEvent?.Invoke();
            }
        }

        // Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(ConfigController), new PropertyMetadata(false));



        public SolidColorBrush SolidOfIconEnebled
        {
            get { return (SolidColorBrush)GetValue(SolidOfIconEnebledProperty); }
            set { SetValue(SolidOfIconEnebledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SolidOfIconEnebled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolidOfIconEnebledProperty =
            DependencyProperty.Register("SolidOfIconEnebled", typeof(SolidColorBrush), typeof(ConfigController), new PropertyMetadata(new SolidColorBrush(Colors.White)));




        public ConfigController()
        {
            InitializeComponent(); IsEnabledEvent += ConfigController_IsEnabledEvent;

            var PrimeryColor = this.TryFindResource("PrimeryColor") as SolidColorBrush;

            SolidOfIconEnebled = PrimeryColor;
        }

        private void ConfigController_IsEnabledEvent()
        {
            if (IsEnabled)
            {
                var OrengeColor = this.TryFindResource("OrangeColor") as SolidColorBrush;

                SolidOfIconEnebled = OrengeColor;
            }
            else {

                var PrimeryColor = this.TryFindResource("PrimeryColor") as SolidColorBrush;

                SolidOfIconEnebled = PrimeryColor;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ModifyDNS.RemoveDNS(ID);
        }


        //--Enable EVENT
        private void Controller_Click(object sender, RoutedEventArgs e)
        {
            DNSSelector.Select(this);
        }
    }
}

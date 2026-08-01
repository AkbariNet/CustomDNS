using CustomDNS.Data.DataMethod;
using CustomDNS.Extra.ConfigControl;
using CustomDNS.Method.Connector;
using CustomDNS.Method.Selector;
using EasyTask.Class;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

using static MaterialDesignThemes.Wpf.Theme;

namespace CustomDNS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>



    public partial class Lobby : Window
    {
        public Lobby()
        {
            InitializeComponent();
            LoadDNS();
            this.Loaded += MainWindow_Loaded;
            DataPattern.isDNSListChanged += LoadDNS;
            DNSSelector.isSelectorChangedEvent += DNSSelector_isSelectorChangedEvent;
        }

        private void DNSSelector_isSelectorChangedEvent(ConfigController obj)
        {

            var DNSSelected = ComboItemsDNS.FirstOrDefault(dns => dns.Content == obj.DNSName);
            if (DNSSelected != null)
            {
                ComboOfSelectDNS.SelectedItem = DNSSelected;
            }
        }

        private void TopOfAPP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        List<ConfigController> configControllers = new List<ConfigController>();
        List<ComboBoxItem> ComboItemsDNS = new List<ComboBoxItem>();
        public void LoadDNS()
        {
            configControllers.Clear();
            ComboItemsDNS.Clear();
            StackOfValueOfDns.Children.Clear();
            ComboOfSelectDNS.Items.Clear();
            var dnsList = DataPattern.LoadDNSList();




            foreach (var dns in dnsList)
            {
                ConfigController controller = new ConfigController();
                controller.DNSName = dns.DNSName;
                controller.DNSValue1 = dns.DNSMain;
                controller.DNSValue2 = dns.DNSSec;
                controller.ID = dns.Id;

                configControllers.Add(controller);
                ComboBoxItem ComboItemDNS = new ComboBoxItem();
                ComboItemDNS.Content = dns.DNSName;
                Action isEnableDNSEvent;
                ComboItemsDNS.Add(ComboItemDNS);
            }

            foreach (ConfigController controllerStack in configControllers)
            {
                StackOfValueOfDns.Children.Add(controllerStack);
            }
            foreach (ComboBoxItem CItemDNS in ComboItemsDNS)
            {

                var BoldestExtraColor = this.TryFindResource("BoldestExtraColor") as SolidColorBrush;

                CItemDNS.Background = BoldestExtraColor;


                CItemDNS.Selected += ComboItemsDNS_Selected;
                ComboOfSelectDNS.Items.Add(CItemDNS);
            }
            ComboOfSelectDNS.SelectedIndex = 0;
        }

        private void ComboItemsDNS_Selected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem CItem = sender as ComboBoxItem;

            var DNSToEdit = configControllers.FirstOrDefault(dns => dns.DNSName == CItem.Content);
            if (DNSToEdit != null)
            {
                DNSSelector.Select(DNSToEdit);
            }
        }

        private void CancelForAddDNSBox_Click(object sender, RoutedEventArgs e)
        {
            RunStoryboard.Run("AddDNSPopUpOut", this, null, AllCOntent);
            RunStoryboard.Run("AddDNSPopUpFadeOut", this, null, AddDNSPopUp);
        }

        private void ApplyAddDNS_Click(object sender, RoutedEventArgs e)
        {
            string DNS1 = DNS1PART1.Text + "." + DNS1PART2.Text + "." + DNS1PART3.Text + "." + DNS1PART4.Text;
            string DNS2 = DNS2PART1.Text + "." + DNS2PART2.Text + "." + DNS2PART3.Text + "." + DNS2PART4.Text;
            if (DNSNAME.Text != "")
            {
                if (!IsHadNameInList)
                {


                    if (ImportNumberOnly.IPChecker(DNS1PART1.Text) && ImportNumberOnly.IPChecker(DNS1PART2.Text) && ImportNumberOnly.IPChecker(DNS1PART3.Text)
                    && ImportNumberOnly.IPChecker((DNS1PART4.Text)) && ImportNumberOnly.IPChecker((DNS2PART1.Text)) && ImportNumberOnly.IPChecker((DNS2PART2.Text))
                    && ImportNumberOnly.IPChecker((DNS2PART3.Text)) && ImportNumberOnly.IPChecker((DNS2PART4.Text)))
                    {
                        if (DNS1 != DNS2)
                        {
                            ModifyDNS.AddDNS(DNSNAME.Text, DNS1, DNS2);
                            LoadDNS();
                            RunStoryboard.Run("AddDNSPopUpOut", this, null, AllCOntent);
                            RunStoryboard.Run("AddDNSPopUpFadeOut", this, null, AddDNSPopUp);

                            ShowError.Text = DNSNAME.Text = DNS1PART1.Text = DNS1PART2.Text = DNS1PART3.Text = DNS1PART4.Text = DNS2PART1.Text = DNS2PART2.Text = DNS2PART3.Text = DNS2PART4.Text = "";

                        }
                        else
                        {
                            ShowError.Text = "The two DNS addresses cannot be the same. Please enter different DNS addresses.";

                        }
                    }
                    else
                    {
                        ShowError.Text = "The entered DNS contains invalid values. Please correct them.";
                    }
                }
                else
                {
                    ShowError.Text = "The name you entered is already in use. Please enter a new name.";

                }
            }
            else
            {
                ShowError.Text = "The DNS name cannot be empty. Please enter your new DNS name.";

            }
        }


        private void AddDNSButton_Click(object sender, RoutedEventArgs e)
        {
            AddDNSPopUp.Visibility = Visibility.Visible;
            DNSNAME.Focus();
            RunStoryboard.Run("AddDNSSPopUpIn", this, null, AllCOntent);
            RunStoryboard.Run("AddDNSPopUpFadeIn", this, null, AddDNSPopUp);
        }

        private void AddDNSPopUpOut_Completed(object sender, EventArgs e)
        {

            AddDNSPopUp.Visibility = Visibility.Collapsed;
        }

        bool IsAdvanceModeTrue = false;
        private void AdvanceMode_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAdvanceModeTrue)
            {

                RunStoryboard.Run("AdvanceModeIn", this, null, this);
                IsAdvanceModeTrue = true; AdvanceMode.Content = "Compact";
            }
            else
            {

                RunStoryboard.Run("AdvanceModeOut", this, null, this);
                IsAdvanceModeTrue = false; AdvanceMode.Content = "Advance";
            }
        }

        private void ChangeDNSButton_Click(object sender, RoutedEventArgs e)
        {
            if (DNSConnector.ISSELECTOROPEN)
            {
                if (DNSConnector.CONNECT())
                {
                    DNSConnector.ISSELECTOROPEN = false;
                    StackOfValueOfDns.IsEnabled = false;
                    StackOfValueOfDns.Opacity = .4;
                    StatusOfConnection.Text = "Connected!";
                    ComboOfSelectDNS.IsEnabled = false;
                    var OrengeColor = this.TryFindResource("OrangeColor") as SolidColorBrush;

                    Contents.Background = OrengeColor;


                }
                else
                {
                    StackOfValueOfDns.Opacity = 1;
                    DNSConnector.ISSELECTOROPEN = true;
                    StatusOfConnection.Text = "Failed!";
                    StackOfValueOfDns.IsEnabled = true;
                    ComboOfSelectDNS.IsEnabled = true;


                    Contents.Background = null;

                }
            }
            else
            {
                if (DNSConnector.DISCONNECT())
                {
                    StackOfValueOfDns.Opacity = 1;
                    StackOfValueOfDns.IsEnabled = true;
                    DNSConnector.ISSELECTOROPEN = true;
                    ComboOfSelectDNS.IsEnabled = true;
                    StatusOfConnection.Text = "Disconnected!"; Contents.Background = null;

                }
                else
                {
                    StackOfValueOfDns.Opacity = 1;
                    StackOfValueOfDns.IsEnabled = true;
                    ComboOfSelectDNS.IsEnabled = true;
                    StatusOfConnection.Text = "Failed!";
                }




            }
        }

   

        private void ResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            DataPattern.SaveDNSList(DataPattern.DefualtDNS());
            ChangeDNSButton_Click(null, null);
            LoadDNS();
            ComboOfSelectDNS.SelectedIndex = 0;
        }

        private void DNSButtons_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;

            try
            {
                if (!ImportNumberOnly.IPChecker((textBox.Text)))
                {

                    var OrengeColor = this.TryFindResource("OrangeColor") as SolidColorBrush;

                    textBox.Foreground = OrengeColor;

                }
                else
                {
                    var LightestColor = this.TryFindResource("LightestColor") as SolidColorBrush;
                    textBox.Foreground = LightestColor;

                }

            }
            catch
            {
            }

        }

        private void DNSButtons_KeyUp(object sender, KeyEventArgs e)
        {

            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;

            if (textBox.Text.Length == 3 || e.Key == Key.OemPeriod)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(request);
                }
            }
            else if (textBox.Text.Length == 0)
            {
                new TraversalRequest(FocusNavigationDirection.Previous);

            }
        }

        private void DNSButtons_KeyDown(object sender, KeyEventArgs e)
        {
            ImportNumberOnly.onlyNumKeyPreviewDown(sender, e);
        }

        private void DNSButtons_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {

                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(request);
                }
            }

            ImportNumberOnly.onlyNumPreviewTextInput(sender, e);
        }

        bool IsHadNameInList = false;
        private void DNSNAME_LostFocus(object sender, RoutedEventArgs e)
        {
            bool InternalSearchEngine = false;
            var dnsList = DataPattern.LoadDNSList();
            foreach (var dns in dnsList)
            {
                if (dns.DNSName.ToLower() == DNSNAME.Text.ToLower())
                {
                    var OrangeColor = this.TryFindResource("OrangeColor") as SolidColorBrush;
                    DNSNAME.Foreground = OrangeColor; IsHadNameInList = true;
                    InternalSearchEngine = true;
                }
            }
            if (!IsHadNameInList || !InternalSearchEngine)
            {
                var LightestColor = this.TryFindResource("LightestColor") as SolidColorBrush;
                DNSNAME.Foreground = LightestColor;
                IsHadNameInList = false;


            }
        }

        private void AboutAppPopUpOut_Completed(object sender, EventArgs e)
        {

            InfoApp.Visibility = Visibility.Collapsed;
        }
        private void AboutAppButton_Click(object sender, RoutedEventArgs e)
        {
            InfoApp.Visibility = Visibility.Visible;
            RunStoryboard.Run("AboutAppPopUpIn", this, null, AllCOntent);
            RunStoryboard.Run("AboutAppPopUpFadeIn", this, null, InfoApp);

        }
        private void CancelForAbout_Click(object sender, RoutedEventArgs e)
        {
            RunStoryboard.Run("AboutAppPopUpOut", this, null, AllCOntent);
            RunStoryboard.Run("AboutAppPopUpFadeOut", this, null, InfoApp);

        }
        private void VisitSiteForAbout_Click(object sender, RoutedEventArgs e)
        {

            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.moradstudio.com",
                UseShellExecute = true
            });
        }

        ///------- CurveWindow DLL
        ///


        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attr, ref DWM_WINDOW_CORNER_PREFERENCE attrValue, uint attrSize);



        private const int DWMWA_CAPTION_COLOR = 35;
        private const int DWMWA_TEXT_COLOR = 36;

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hWnd, int attr, ref uint attrValue, int attrSize);

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd == IntPtr.Zero) return;

            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND; // یا .ROUNDSMALL برای گرد کوچکتر
            DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(uint));
            // رنگ پس‌زمینه TitleBar
            uint captionColor = 0x00191919; // Primery
            DwmSetWindowAttribute(hwnd, DWMWA_CAPTION_COLOR, ref captionColor, sizeof(uint));

            // رنگ متن TitleBar
            uint textColor = 0xFFFFFFFF; // سفید
            DwmSetWindowAttribute(hwnd, DWMWA_TEXT_COLOR, ref textColor, sizeof(uint));

            int style = GetWindowLong(hwnd, GWL_STYLE);
            SetWindowLong(hwnd, GWL_STYLE, style & ~WS_MAXIMIZEBOX); // حذف بیت ماکسیمایز


        }



        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int GWL_STYLE = -16;
        const int WS_MAXIMIZEBOX = 0x00010000;

    }


}
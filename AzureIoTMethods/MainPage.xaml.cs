using AzureIoTMethods.IotClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AzureIoTMethods
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        IOTHub iot;

        public MainPage()
        {
            this.InitializeComponent();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            button.Background = new SolidColorBrush(Windows.UI.Colors.Green);
            iot = new IOTHub();
            Thread tr = new Thread(iot.ListenMethod);
            tr.Start();

        }

    }
}

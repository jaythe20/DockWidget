using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DockWidget
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public bool IsInstalled { get; set; }
        public SearchWindow()
        {
            IsInstalled = IsApplictionInstalled("Opera");
            InitializeComponent();
        }

        private void serachText_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Close();
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                var prs = IsInstalled ? new ProcessStartInfo("Opera.exe") : new ProcessStartInfo("IExplore.exe");
                prs.Arguments = "https://www.google.com/search?q=" + textBox.Text;
                Process.Start(prs);
                serachText.Text = string.Empty;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            serachText.Text = string.Empty;
            this.Close();
        }

        public static bool IsApplictionInstalled(string p_name)
        {
            string displayName;
            RegistryKey key;

            // search in: CurrentUser
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (displayName != null && p_name.Contains(displayName) == true)
                {
                    return true;
                }
            }

            // search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (displayName != null && p_name.Contains(displayName) == true)
                {
                    return true;
                }
            }

            // search in: LocalMachine_64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (displayName != null && p_name.Contains(displayName) == true)
                {
                    return true;
                }
            }

            // NOT FOUND
            return false;
        }
    }
}

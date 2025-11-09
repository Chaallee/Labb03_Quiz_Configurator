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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb3.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationView.xaml
    /// </summary>
    public partial class ConfigurationView : UserControl
    {
        public ConfigurationView()
        {
            InitializeComponent();
        }



    }
}

        

public partial class PackOptions : Window
{
    public string Difficulty
    {
        get => Difficulty;
        set => Difficulty = value;
    }

    public string TimeLimitInSeconds
    {
        get => TimeLimitInSeconds;
        set => TimeLimitInSeconds = value;
    }

    //public PackOptions()
    //{
    //    InitializeComponent();  ??
    //}

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
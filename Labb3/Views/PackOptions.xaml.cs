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
using System.Windows.Shapes;

namespace Labb3.Views
{
    /// <summary>
    /// Interaction logic for PackOptions.xaml
    /// </summary>
    public partial class PackOptions : Window
    {
        public PackOptions()
        {
            InitializeComponent();
        }
    }

    public partial class PackOptions : Window
    {
        private string _difficulty;
        public string Difficulty
        {
            get => _difficulty;
            set => _difficulty = value;
        }

        private int _timeLimitInSeconds;
        public int TimeLimitInSeconds
        {
            get => _timeLimitInSeconds;
            set => _timeLimitInSeconds = value;
        }

        private string _packName;
        public string PackName
        {
            get => _packName;
            set => _packName = value;
        }

    }
}

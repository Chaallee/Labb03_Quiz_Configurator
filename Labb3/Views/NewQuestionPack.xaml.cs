using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Labb3.Models;

namespace Labb3.Views
{
    public partial class NewQuestionPack : Window, INotifyPropertyChanged
    {
        private string _packName = "New Question Pack";
        public string PackName
        {
            get => _packName;
            set
            {
                _packName = value;
                OnPropertyChanged();
            }
        }

        private Difficulty _difficulty = Difficulty.Medium;
        public Difficulty Difficulty
        {
            get => _difficulty;
            set
            {
                _difficulty = value;
                OnPropertyChanged();
            }
        }

        private int _timeLimitInSeconds = 30;
        public int TimeLimitInSeconds
        {
            get => _timeLimitInSeconds;
            set
            {
                _timeLimitInSeconds = value;
                OnPropertyChanged();
            }
        }

        public NewQuestionPack()
        {
            InitializeComponent();
            DataContext = this;
            
            PackNameTextBox.Focus();
            PackNameTextBox.SelectAll();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PackName))
            {
                MessageBox.Show("Please enter a pack name.", "Invalid Name", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                PackNameTextBox.Focus();
                return;
            }

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

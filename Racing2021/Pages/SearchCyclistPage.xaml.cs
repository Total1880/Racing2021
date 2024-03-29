﻿using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages;
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

namespace Racing2021.Pages
{
    /// <summary>
    /// Interaction logic for SearchCyclistPage.xaml
    /// </summary>
    public partial class SearchCyclistPage : Page
    {
        public SearchCyclistPage()
        {
            InitializeComponent();
        }

        private void ListView_dubbleclick(object sender, MouseButtonEventArgs e)
        {
            Messenger.Default.Send(new SearchCyclistDoubleClick());
        }
    }
}

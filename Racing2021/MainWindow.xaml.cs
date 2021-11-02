﻿using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages.WindowOpener;
using Racing2021.Pages;
using System;
using System.Windows;

namespace Racing2021
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EditorHomePage _editorHomePage;
        private EditorTracksPage _editorTracksPage;
        private HomePage _homePage;
        private StartRacePage _startRacePage;

        public EditorHomePage EditorHomePage => _editorHomePage ??= new EditorHomePage();
        public EditorTracksPage EditorTracksPage => _editorTracksPage ??= new EditorTracksPage();
        public StartRacePage StartRacePage => _startRacePage ??= new StartRacePage();
        public HomePage HomePage => _homePage ??= new HomePage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(HomePage);
            Messenger.Default.Register<OpenEditorHomePageMessage>(this, OpenEditorHomePage);
            Messenger.Default.Register<OpenEditorTracksPageMessage>(this, OpenEditorTracksPage);
            Messenger.Default.Register<OpenStartRacePageMessage>(this, OpenStartRacePage);
        }

        private void OpenEditorHomePage(OpenEditorHomePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(EditorHomePage);
        }


        private void OpenEditorTracksPage(OpenEditorTracksPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(EditorTracksPage);
        }


        private void OpenStartRacePage(OpenStartRacePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(StartRacePage);
        }
    }
}
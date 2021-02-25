using ListDetailSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.DualScreen;
using Xamarin.Forms.Xaml;

namespace ListDetailSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly DetailPage _detailPage;

        private bool IsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

        public MainPage()
        {
            InitializeComponent();

            ListViewPage.SelectionChanged += OnSelectionChanged;
            _detailPage = new DetailPage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!IsSpanned)
                ListViewPage.SelectedItem = null;

            DualScreenInfo.Current.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            DualScreenInfo.Current.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DualScreenInfo.Current.SpanMode)
                || e.PropertyName == nameof(DualScreenInfo.Current.IsLandscape))
            {
                SetupViews();
            }
        }

        private async void SetupViews()
        {
            if (IsSpanned && !DualScreenInfo.Current.IsLandscape)
                SetBindingContext();

            if (DetailViewPage.BindingContext == null)
                return;

            if (!IsSpanned)
            {
                if (!Navigation.NavigationStack.Contains(_detailPage))
                    await Navigation.PushAsync(_detailPage);
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
                return;

            SetBindingContext();
            SetupViews();
        }

        private void SetBindingContext()
        {
            var selectedItem = ListViewPage.SelectedItem 
                ?? (ListViewPage.ItemsSource as IEnumerable<MyListItem>).FirstOrDefault();

            DetailViewPage.BindingContext = selectedItem;
            _detailPage.BindingContext = selectedItem;
        }
    }
}
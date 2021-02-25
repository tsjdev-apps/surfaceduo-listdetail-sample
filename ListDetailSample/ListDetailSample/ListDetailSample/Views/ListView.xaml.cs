using ListDetailSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListDetailSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListView : CollectionView
    {
        public ListView()
        {
            InitializeComponent();

            ItemsSource = Enumerable.Range(1, 50)
                .Select(x => new MyListItem(x));
        }
    }
}
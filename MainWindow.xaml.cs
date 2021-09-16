using ReactiveUI;
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

namespace WPFEmptyProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

        public partial class MainWindow : Window, IViewFor<MainWindowViewModel>
        {
            public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel",
                typeof(MainWindowViewModel), typeof(MainWindow), new PropertyMetadata(null));

            public MainWindow()
            {
                InitializeComponent();
                ViewModel = new MainWindowViewModel();
                DataContext = ViewModel;
                //if ever decide to have binding in codebehind, it should be like this:

                //this
                //.WhenActivated(disposables => {
                //this
                //.BindCommand(this.ViewModel, vm => vm.TranslateAllProvidersCommand, v => v.TestButton)
                //.DisposeWith(disposables);
                //});
            }

            public MainWindowViewModel ViewModel
            {
                get { return (MainWindowViewModel)GetValue(ViewModelProperty); }
                set { SetValue(ViewModelProperty, value); }
            }

            object IViewFor.ViewModel
            {
                get { return ViewModel; }
                set { ViewModel = (MainWindowViewModel)value; }
            }


        }
}

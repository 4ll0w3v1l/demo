using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
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

namespace praktika_demo
{
    public partial class MainPage : Page
    {
        private DbHandler handler;
        private int uId;
        private int userAccess;
        public MainPage(int user, int userAccess)
        {
            InitializeComponent();
            this.uId = user;
            this.userAccess = userAccess;
            this.handler = new DbHandler(this.uId, this.userAccess);
            dt.ItemsSource = this.handler.GetDataTable().DefaultView;
        }
        
        public void refresh(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Refresh();
            dt.ItemsSource = this.handler.GetDataTable().DefaultView;
        }
        public void saveData(object sender, RoutedEventArgs e) 
        {
            this.handler.Update();
        }
    }
}

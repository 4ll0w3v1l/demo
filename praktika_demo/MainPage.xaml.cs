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
        private int accessLevel;
        public MainPage(int user, int userAccess)
        {
            InitializeComponent();
            uId = user;
            accessLevel = userAccess;
            handler = new DbHandler(uId, accessLevel);
            dt.ItemsSource = handler.GetDataTable(accessLevel).DefaultView;
        }
        
        public void refresh(object sender, RoutedEventArgs e)
        {
            NavigationService.Refresh();
            dt.ItemsSource = handler.GetDataTable(accessLevel).DefaultView;
        }
        public void saveData(object sender, RoutedEventArgs e) 
        {
            handler.Update();
        }
    }
}

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

namespace praktika_demo
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }
        public void loginAction(object sender, EventArgs e) 
        {
            DbHandler dbHandler = new DbHandler(-1, -1);
            List<object> uId = dbHandler.login(loginText.Text, passText.Password);
            if (uId.Count > 0)
            {
                this.NavigationService.Navigate(new MainPage((int)uId[0], (int)uId[1]));
            }
            else { MessageBox.Show("Некорректные данные"); }

        }
    }
}

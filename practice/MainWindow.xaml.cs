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

namespace practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
    {
        public string Role;
        bool admin = false;
        public MainWindow()
        {
            InitializeComponent();
            Role = authorizaiton.role;
            if (Role == "admin")
            {
                admin = true;
            }
            if (admin != true)
            {
                btnAdmin.Visibility = Visibility.Hidden;
            }
            lblRole.Content = Role;
        }

        private void LblGoBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?","Предупреждение",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                authorizaiton auth = new authorizaiton();
                auth.Show();
                this.Close();
            }
        }

        private void LblGoBack_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void LblGoBack_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

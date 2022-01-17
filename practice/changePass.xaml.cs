using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using practice;
using System.Data.Entity;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace practice
{
    /// <summary>
    /// Логика взаимодействия для changePass.xaml
    /// </summary>
    public partial class changePass : Window
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? dt = null;
        Context db = new Context();
        

        public changePass()
        {
            InitializeComponent();
            
        }
        private void LblBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            authorizaiton auth = new authorizaiton();
            auth.Show();
            this.Close();
        }

        private void LblBack_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void LblBack_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void LblClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            lblClose.Foreground = Brushes.Red;
        }

        private void LblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void LblClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            lblClose.Foreground = new SolidColorBrush(Color.FromRgb(0, 65, 101));
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            users currentUser = new users();
            string pass = tbPassword.Text;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            db = new Context();
            db.users.Load();
            bool connected = false;
            foreach (var item in db.users)
            {
                if ((item.password==Password) && (item.login == Login))
                {
                    currentUser = item;
                    connected = true;
                }
            }

            if (connected)
            {
                if (hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasMinimum8Chars.IsMatch(pass))
                {
                    MessageBox.Show("Вы успешно сменили пароль!");
                    authorizaiton auth = new authorizaiton();
                    currentUser.lastTimeChangePass = DateTime.Today;
                    currentUser.password = pass;
                    db.SaveChanges();
                    auth.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пароль не подходит по одному из критериев.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Нет подключения");
            }
            
        }
    }
}

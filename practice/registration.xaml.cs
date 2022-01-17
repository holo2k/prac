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
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace practice
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        Context db = new Context();

        public registration()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            db = new Context();
            db.users.Load();
            bool isSameLogin = false;
            string login = tbLogin.Text;
            string pass = tbPassword.Text;

            foreach (var item in db.users)
            {
                if(item.login== login)
                {
                    isSameLogin = true;
                }
            }
            if(!isSameLogin)
            {
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimumEightNumbers = new Regex(@".{8,}");

                if ((hasNumber.IsMatch(pass)) && (hasUpperChar.IsMatch(pass)) && (hasMinimumEightNumbers.IsMatch(pass)))
                {
                    users newUser = new users();
                    newUser.lastTimeChangePass = DateTime.Today;
                    newUser.login = tbLogin.Text;
                    newUser.password = tbPassword.Text;
                    newUser.lastTimeLogin = DateTime.Today;
                    newUser.role = "user";
                    db.users.Add(newUser);
                    db.SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались.");
                    authorizaiton auth = new authorizaiton();
                    auth.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Слабый пароль");
                }

                
            }
            else if (isSameLogin)
            {
                MessageBox.Show("Такой логин уже существует");
            }
           
        }

        private void LblBack_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void LblBack_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void LblBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            authorizaiton auth = new authorizaiton();
            auth.Show();
            Close();
        }
    }
}

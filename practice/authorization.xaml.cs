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
using System.Data.Entity;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Windows.Threading;

namespace practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 


    public partial class authorizaiton : Window
    {
        public static string role = null;
        bool connected = false;
        DateTime? loginDate = null;
        DateTime? PassDate = null;
        Context db = new Context();
        public  users currentUser = new users();
        int count = 0;
        int value = 0;
        int time = 15000;
        DispatcherTimer timer = new DispatcherTimer();
        public authorizaiton()
        {
            InitializeComponent();
        }

        private void LblRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {
            registration reg = new registration();
            reg.Show();
            Close();
        }

        private void LblRegister_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void LblRegister_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            
            db = new Context();
            db.users.Load();
            try
            {
                
                foreach (var item in db.users)
                {
                    if ((item.login == tbLogin.Text) && (item.password == tbPassword.Text))
                    {
                        currentUser = item;
                        connected = true;
                        PassDate = item.lastTimeChangePass.Value;
                        loginDate = item.lastTimeLogin.Value;
                        role = item.role;
                        
                    }

                }
                if (connected == true)
                {
                    System.TimeSpan diff = DateTime.Today - PassDate.Value;
                    System.TimeSpan diff1 = DateTime.Today - loginDate.Value;
                    if ((diff.Days >= 14) && (diff.Days < 93))
                    {
                        count = 0;
                        MessageBox.Show("Вы не меняли пароль 14 дней! Смените пароль!", "Пароль", MessageBoxButton.OK);
                        changePass cp = new changePass();
                        cp.Login = currentUser.login;
                        cp.Password = currentUser.password;
                        cp.Show();

                        this.Close();
                    }
                    else if (diff1.Days > 93)
                    {
                        count = 0;
                        MessageBox.Show("Вы не заходили 3 месяца. Ваша учетная запись деактивирована.", "Сообщение", MessageBoxButton.OK);
                    }
                    else
                    {
                        count = 0;
                        MessageBox.Show("Вы вошли");
                        currentUser.lastTimeLogin = DateTime.Today;
                        MainWindow mw = new MainWindow();
                        mw.Show();
                        Close();
                    }
                }
                else
                {
                    if (count < 3)
                    {
                        count++;
                        if(count<3)
                        MessageBox.Show("Неверный логин или пароль");
                        else
                        {
                            time = time + ((count - 3) * 20000);
                            MessageBox.Show($"Вы ввели неверный пароль 3 раза. Подождите {time / 1000} секунд прежде чем вам снова будет доступен вход.");
                            tbLogin.IsEnabled = false;
                            tbPassword.IsEnabled = false;
                            btnConnect.IsEnabled = false;
                            lblRegister.IsEnabled = false;
                            value = 0;
                            timer.IsEnabled = true;
                            timer.Interval = TimeSpan.FromMilliseconds(1000);
                            timer.Tick += timer_Tick;
                            timer.Start();
                            count++;
                        }
                    }
                    else
                    {
                        if (count > 3)
                        {
                            time = time + ((count - 3) * 20000);
                        }
                        MessageBox.Show($"Вы ввели неверный пароль 3 раза. Подождите {time / 1000} секунд прежде чем вам снова будет доступен вход.");
                        tbLogin.IsEnabled = false;
                        tbPassword.IsEnabled = false;
                        btnConnect.IsEnabled = false;
                        lblRegister.IsEnabled = false;
                        value = 0;
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.IsEnabled = true;
                        timer.Interval = TimeSpan.FromMilliseconds(1000);
                        timer.Tick += timer_Tick;
                        timer.Start();
                        count++;
                    }         
                    
                }
                connected = false;
                db.SaveChanges(); 
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message,"");
            }
            
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            value++;
            lblTime.Content = $"Осталось : {time/1000-value} секунд";
            if (time / 1000 - value == 0)
            {
                tbLogin.IsEnabled = true;
                tbPassword.IsEnabled = true;
                btnConnect.IsEnabled = true;
                lblRegister.IsEnabled = true;
                timer.Stop();
            }
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
            lblClose.Foreground = new SolidColorBrush(Color.FromRgb(0,65,101));
        }
    }
}

using System.Windows;
using BusinessLogic.Concrete;
using Dal.Interface;
using Dal.Concrete;
using BusinessLogic.Interface;
using Dal.Concreate;
using SupplierProductManagementApp;
using System.Windows.Controls;

namespace SupplierProductManagementApp
{
    public partial class LoginWindow : Window
    {
        private readonly IUserManager _usersManager;

        public LoginWindow()
        {
            InitializeComponent();
            var dbConfig = new Configuration("Server=DESKTOP-9MEF99H;Database=SupplierManagement;Integrated Security=True;"); 
            var usersDal = new UserDal(dbConfig.ConnectionString);
            _usersManager = new UserManager(usersDal);
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Enter Username")
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Enter Username";
                UsernameTextBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "Enter Password")
            {
                PasswordBox.Password = "";
                PasswordBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                PasswordBox.Password = "Enter Password";
                PasswordBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            var user = _usersManager.GetUserByCredentials(username, password);
            if (user != null) 
            {
                var mainWindow = new MainWindow();  
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ErrorMessage.Text = "Invalid username or password!";
            }
        }

        // Обробник для кнопки "Register"
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(); 
            registerWindow.Show();
            this.Close();
        }
    }
}

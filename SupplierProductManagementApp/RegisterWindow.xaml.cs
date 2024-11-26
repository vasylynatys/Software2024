using System.Windows;
using BusinessLogic.Concrete;
using Dal.Interface;
using Dal.Concrete;
using BusinessLogic.Interface;
using Dal.Concreate;
using DTO;
using SupplierProductManagementApp;
using System.Windows.Controls;

namespace SupplierProductManagementApp
{
    public partial class RegisterWindow : Window
    {
        private readonly UserManager _usersManager;

        public RegisterWindow()
        {
            InitializeComponent();
            var dbConfig = new Configuration("Server=DESKTOP-9MEF99H;Database=SupplierManagement;Integrated Security=True;");  // Заміни на реальний рядок підключення
            var usersDal = new UserDal(dbConfig.ConnectionString);
            _usersManager = new UserManager(usersDal);

            UsernameTextBox.GotFocus += RemoveUsernameWatermark;
            UsernameTextBox.LostFocus += ShowUsernameWatermark;
            PasswordBox.GotFocus += RemovePasswordWatermark;
            PasswordBox.LostFocus += ShowPasswordWatermark;
            ConfirmPasswordBox.GotFocus += RemoveConfirmPasswordWatermark;
            ConfirmPasswordBox.LostFocus += ShowConfirmPasswordWatermark;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (password != confirmPassword)
            {
                ErrorMessage.Text = "Passwords do not match!";
                return;
            }
            var newUser = new User
            {
                Username = username,
                Password = password 
            };

            var user = _usersManager.AddUser(newUser);
            if (user != null)
            {
                MessageBox.Show("Registration successful!");
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                ErrorMessage.Text = "Registration failed!";
            }
        }


        private void ShowUsernameWatermark(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                UsernameWatermark.Visibility = Visibility.Visible;
            }
        }

        private void RemoveUsernameWatermark(object sender, RoutedEventArgs e)
        {
            UsernameWatermark.Visibility = Visibility.Collapsed;
        }

        private void ShowPasswordWatermark(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                PasswordWatermark.Visibility = Visibility.Visible;
            }
        }

        private void RemovePasswordWatermark(object sender, RoutedEventArgs e)
        {
            PasswordWatermark.Visibility = Visibility.Collapsed;
        }

        private void ShowConfirmPasswordWatermark(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ConfirmPasswordBox.Password))
            {
                ConfirmPasswordWatermark.Visibility = Visibility.Visible;
            }
        }

        private void RemoveConfirmPasswordWatermark(object sender, RoutedEventArgs e)
        {
            ConfirmPasswordWatermark.Visibility = Visibility.Collapsed;
        }
    }
}

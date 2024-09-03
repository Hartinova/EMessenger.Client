using EMessenger.Client.Model;
using EMessenger.Client.Model.DTO;
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

namespace EMessenger.Client
{
  /// <summary>
  /// Логика взаимодействия для AuthorizationWindow.xaml
  /// </summary>
  public partial class AuthorizationWindow : Window
  {
    public AuthorizationWindow()
    {
      InitializeComponent();
    }
    private void AuthorizationButtonClick(object sender, RoutedEventArgs e)
    {
      Authtorizate();
    }
    private void OpenRegisrationButtonClick(object sender, RoutedEventArgs e)
    {
      RegistrationWindow regisration = new RegistrationWindow();
      regisration.ShowDialog();
      this.Close();
    }

    private void OpenWitoutRegistration_Click(object sender, RoutedEventArgs e)
    {
      Messenger.CurrentUser = Registration.RegistrateUser(new UserDto("Гость"));
      this.Close();
    }

    private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        PasswordBox.Focus();
      }
    }

    private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        Authtorizate();
      }
    }

    private void Authtorizate()
    {
      string username = UsernameTextBox.Text;
      string password = PasswordBox.Password;

      if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
      {
        MessageTextBlock.Text = "Пожалуйста, заполните все поля.";
        return;
      }

      Messenger.CurrentUser = Registration.AuthorizateUser(username, password);
      this.Close();
    }
  }
}

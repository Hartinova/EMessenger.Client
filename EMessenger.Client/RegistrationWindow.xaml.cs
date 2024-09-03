using EMessenger.Client.Model.DTO;
using EMessenger.Client.Model;
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
  /// Логика взаимодействия для RegistrationWindow.xaml
  /// </summary>
  public partial class RegistrationWindow : Window
  {
    public RegistrationWindow()
    {
      InitializeComponent();
    }

    private void RegisrationButtonClick(object sender, RoutedEventArgs e)
    {
      Registrate();
    }

    private void Registrate()
    {
      string name = nameTextBox.Text;
      string login = UsernameTextBox.Text;
      string passwordOne = PasswordBoxOne.Password;
      string passwordTwo = PasswordBoxTwo.Password;

      if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(passwordOne) || string.IsNullOrWhiteSpace(passwordTwo))
      {
        MessageTextBlock.Text = "Пожалуйста, заполните все поля.";
        return;
      }
      else if (passwordOne != passwordTwo)
      {
        MessageTextBlock.Text = "Пароли не соотвествуют друг другу";
        return;
      }

      Messenger.CurrentUser = Registration.RegistrateUserAccount(new UserRegistrationDto(name, login, passwordOne));
      this.Close();
    }
    private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        PasswordBoxOne.Focus();
      }
    }

    private void nameTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        UsernameTextBox.Focus();
      }
    }

    private void PasswordBoxOne_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        PasswordBoxTwo.Focus();
      }
    }

    private void PasswordBoxTwo_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        Registrate();
      }
    }
  }
}

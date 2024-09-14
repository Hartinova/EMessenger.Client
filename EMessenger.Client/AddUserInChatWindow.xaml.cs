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
  /// Логика взаимодействия для AddUserInChatWindow.xaml
  /// </summary>
  public partial class AddUserInChatWindow : Window
  {

    public Messenger messengerContext;
    public AddUserInChatWindow(Messenger messenger)
    {
      InitializeComponent();
      messengerContext = messenger;
      DataContext = messengerContext;
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
      AddUserInChat();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }

    private void UserIdTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        AddUserInChat();
      }
    }

    private void AddUserInChat()
    {
      messengerContext.AddUserInGroupChat(messengerContext.SelectedUser);
      DialogResult = true;
    }
  }
}

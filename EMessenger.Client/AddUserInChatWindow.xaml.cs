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

  public partial class AddUserInChatWindow : Window
  {
    public int UserId { get; set; }
    public AddUserInChatWindow()
    {
      InitializeComponent();
      UserIdTextBox.Focus();
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
      UserId = int.Parse(UserIdTextBox.Text);
      DialogResult = true;
    }
  }
}

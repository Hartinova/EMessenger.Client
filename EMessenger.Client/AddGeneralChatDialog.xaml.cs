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

namespace EMessenger.Client.Views
{
  /// <summary>
  /// Логика взаимодействия для AddGeneralChatDialog.xaml
  /// </summary>
  public partial class AddGeneralChatDialog : Window
  {
    public string ChatName { get; set; }

    public AddGeneralChatDialog()
    {
      InitializeComponent();
      ChatNameTextBox.Focus();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
      AddGeneralChat();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }

    private void AddGeneralChat()
    {
      ChatName = ChatNameTextBox.Text;
      DialogResult = true;
    }

    private void ChatNameTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        AddGeneralChat();
      }
    }
  }
}

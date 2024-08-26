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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EMessenger.Client
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private Messenger messenger;
    public MainWindow()
    {
      InitializeComponent();

      //тут необходимо вызвать форму для ввода логина пароля или захода без регистрации
      User currentUser = new User(1, "Я");
      messenger = new Messenger(currentUser);
      this.DataContext= messenger;
    }
  }
}

using EMessenger.Client.Model;
using EMessenger.Client.Model.DTO;
using EMessenger.Client.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// <summary>
    /// Мессенджер.
    /// </summary>
    private Messenger messenger;

    /// <summary>
    /// Интервал времени ,через которое обновляем сообщения.
    /// </summary>
    private int interval = 1000;

    Timer timer;


    public MainWindow()
    {
      InitializeComponent();

      #region тут необходимо вызвать форму для ввода логина пароля или захода без регистрации и вернуть сюда полученного пользователя
      //код внутри этого региона нужно перенести в форму регистрации

      // var comm = Communication.GetInstance();

      //если вход с регистрацией
      //  User currentUser = Registration.RegistrateUserAccount(new UserRegistrationDto("Камилла","login2","000000"));

      //авторизация
      var currentUser = Registration.AuthorizateUser("login10", "000000");

      //если вход без авторизации
      // User currentUser = Registration.RegistrateUser(new UserDto("Гость2"));

      #endregion

      messenger = new Messenger(currentUser);
      this.DataContext = messenger;

      //запуск по таймеру - обновление сообщений
      timer = new Timer(TimerCallback, null, 0, interval);

    }

    /// <summary>
    /// Произвести добавление чата на событие щелком ЛКМ по кнопке "Добавить общий чат"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnAddGeneralChat_Click(object sender, RoutedEventArgs e)
    {
      messenger.AddGeneralChat();
    }

    /// <summary>
    /// Событие по таймеру
    /// </summary>
    /// <param name="o"></param>
    private void TimerCallback(Object o)
    {
      messenger.SelectedChat.GetMessages(Messenger.CurrentUser);
      dataGridMessage.ScrollIntoView(messenger.SelectedChat.SelectedMessage);

      GC.Collect();
    }


    private void BtnAddGroupChat_Click(object sender, RoutedEventArgs e)
    {
      messenger.AddGroupChat();
    }

    private void BtnDelChat_Click(object sender, RoutedEventArgs e)
    {
      messenger.DeleteChat();
    }

    private void BtnSendMessage_Click(object sender, RoutedEventArgs e)
    {
      if (messageControl.Text.Trim() != "")
      {
        messenger.SendMessage(messageControl.Text);
        messageControl.Text = "";
      }
    }

    private void messageControl_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        if (messageControl.Text.Trim() != "")
        {
          messenger.SendMessage(messageControl.Text);
          messageControl.Text = "";
        }
      }
    }
  }
}

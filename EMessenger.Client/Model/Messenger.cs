using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Чат
  /// </summary>
  public class Messenger:INotifyPropertyChanged
  {
    #region Поля и свойства

    /// <summary>
    /// Заголовок формы.
    /// </summary>
    public string Title
    {
      get
      {
        return $"Мессенджер [{CurrentUser.NickName}]";
      }
    }

    /// <summary>
    /// Текущий пользователь.
    /// </summary>
    public static  User CurrentUser { get; private set; }

    /// <summary>
    /// Список чатов, доступный пользователю.
    /// </summary>
    public List<Chat> Chats { get; set; }

    /// <summary>
    /// Текущий (выбранный чат)
    /// </summary>
    private Chat selectedChat;

    /// <summary>
    /// Текущий (выбранный чат)
    /// </summary>
    public Chat SelectedChat
    {
      get
      {
        return this.selectedChat;
      }

      set
      {
        if (value != this.selectedChat)
        {
          this.selectedChat = value;
          this.selectedChat.GetMessages(CurrentUser);
          NotifyPropertyChanged("SelectedChat");
        }
      }
    }

    #endregion

    #region Поля и свойства для элементов формы

    /// <summary>
    /// Видимость панели редактирования чатов.
    /// </summary>
    public Visibility EditPanelVisible 
    { 
      get 
      { 
        return (CurrentUser==null || CurrentUser.Role==Roles.None) ? Visibility.Collapsed : Visibility.Visible; 
      } 
    }

    /// <summary>
    /// Видимость области с сообщениями выбранного чата.
    /// </summary>
    public Visibility MessagesPanelVisible
    {
      get
      {
        return (SelectedChat == null) ? Visibility.Hidden : Visibility.Visible;
      }
    }

    #endregion

    #region Базовая реализация INotifyPropertyChanged - необходима для автоматического обновления данных на форме (использование binding)

    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region Методы

   
    /// <summary>
    /// Получение списка чатов текущего пользователя.
    /// </summary>
    /// <param name="currentUser">Текущий пользователь.</param>
    private void GetChats(User currentUser)
    {
      Chats = new List<Chat>();

      if (currentUser == null)
      {
        return;
      }

      //Заполним список чатов общими чатами
      var generalChats = Queries.GetGeneralChats();
      foreach (var chatDto in generalChats)
      {
          GeneralChat chat = new GeneralChat(chatDto.Id, chatDto.Name);
          Chats.Add(chat);
      }

      //список пользователей и групповых чатов получаем только для зарегистрированного пользователя
      if (currentUser.Role == Roles.All)
      {
        //Заполним список чатов групповыми чатами
        var groupChats = Queries.GetGroupChats(currentUser.Id);
        foreach (var chatDto in groupChats)
        {
          GroupChat chat = new GroupChat(chatDto.Id, chatDto.Name);
          Chats.Add(chat);
        }

        //заполним списком пользователей
        var users = Queries.GetUsers();
        foreach (var user in users)
        {
          if (user.Id != currentUser.Id)
          {
            var chatId = Queries.GetPrivateChat(currentUser.Id, user.Id);
            PrivateChat chat = new PrivateChat(chatId, user.NickName, user);         
            Chats.Add(chat);
          }
        }
      }

      if (Chats != null && Chats.Count() > 0)
      {
        SelectedChat = Chats.First();
      }
    }

    /// <summary>
    /// Отправить сообщение.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public void SendMessage(string message)
    {
      if (SelectedChat==null)
      { 
        return;
      }

      selectedChat.SendMessage(message, CurrentUser);
    }

    /// <summary>
    /// Тут необходимо реализовать добавление общего чата, добавить входные параметры, которые необходимы
    /// </summary>
    public void AddGeneralChat()
    {
      MessageBox.Show("Тут необходимо реализовать добавление общего чата");
    }

    /// <summary>
    /// Тут необходимо реализовать добавление группового чата, добавить входные параметры, которые необходимы
    /// </summary>
    public void AddGroupChat()
    {
      MessageBox.Show("Тут необходимо реализовать добавление группового чата");
    }

    /// <summary>
    /// Тут необходимо реализовать удаление чата, добавить входные параметры, которые необходимы
    /// </summary>
    public void DeleteChat()
    {
      MessageBox.Show("Тут необходимо реализовать удаление чата");
    }


    #endregion

    #region Конструктор

    public Messenger(User user)
    {
      CurrentUser = user;
      GetChats(CurrentUser);
    }

    #endregion
  }
}

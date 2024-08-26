using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Чат
  /// </summary>
  public class Messenger:INotifyPropertyChanged
  {
    #region Поля и свойства

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
          NotifyPropertyChanged("SelectedChat");
          this.selectedChat.GetMessages();
        }
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

    /// <summary>
    /// Получение списка чатов текущего пользователя.
    /// !!!!!!!!!!!!!!!!!!!!!!!Тут необходим получать их с сервера.
    /// пока генерирую сама....
    /// </summary>
    private void GetChats(User currentUser)
    {
      Chats = new List<Chat>();
     User apponent1= new User(2,"Тимур");
      User apponent2 = new User(3, "Камилла");
      List<User> usersForExample = new List<User>();
      usersForExample.Add(currentUser);
      usersForExample.Add(apponent1);
      usersForExample.Add(apponent2);
      Chat chat1 = new Chat(1, "Общий чат 1", usersForExample);
      Chat chat2 = new Chat(2, currentUser,apponent2);
      Chat chat3 = new Chat(3, currentUser, apponent1);

      Chats.Add(chat1);
      Chats.Add(chat2);
      Chats.Add(chat3);

      SelectedChat = Chats.First();
    }

    #region Конструктор

    public Messenger(User user)
    {
      CurrentUser = user;
      GetChats(CurrentUser);
    }

    #endregion
  }
}

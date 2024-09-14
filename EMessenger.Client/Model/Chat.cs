using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Documents;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Чат общий или групповой.
  /// </summary>
  public class Chat : INotifyPropertyChanged
  {
    #region Поля и свойства

    /// <summary>
    /// Идентификатор чата. 
    /// </summary>
    public int? Id { get; protected set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Имя чата с указанием типа чата.
    /// </summary>
    public virtual string NameWithType
    {
      get
      {
        return Name;
      }
    }

    /// <summary>
    /// Список сообщений.
    /// </summary>
    public List<Message> Messages { get; private set; }



    /// <summary>
    /// Выбранное сообщение.
    /// </summary>
    private Message selectedMessage;

    /// <summary>
    /// Выбранное сообщение.
    /// </summary>
    public Message SelectedMessage
    {
      get
      {
        return this.selectedMessage;
      }

      set
      {
        if (value != this.selectedMessage)
        {
          this.selectedMessage = value;
          NotifyPropertyChanged("SelectedMessage");
        }
      }
    }

    /// <summary>
    /// Доступность кнопки удаления чата.
    /// </summary>
    public bool DeleteChatEnabled
    {
      get
      {
        return (this.Id != null && this.Id != 0 );
      }
    }

    /// <summary>
    /// Доступность кнопки добавления пользователя в чат.
    /// </summary>
    public virtual bool AddUserInChatEnabled
    {
      get
      {
        return false;
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
    /// Метод получения сообщений чата.
    /// </summary>
    /// <param name="currentUser">Текущий пользователь</param>
    public void GetMessages(User currentUser)
    {
      if (this.Id == null)
      {
        Messages = null;
      }
      else
      {
        Messages = Queries.GetAllMessages(this.Id.Value, currentUser);
        SelectedMessage = Messages?.LastOrDefault();
      }

      NotifyPropertyChanged("Messages");
      NotifyPropertyChanged("DeleteChatEnabled");
      NotifyPropertyChanged("AddUserInChatEnabled");
    }

    /// <summary>
    /// Добавить пользователя в чат.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public virtual bool AddAccount(User user)
    {
      if (Id != null)
      {
        return Queries.PostAccountInChat(this.Id.Value, user.Id);
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Записать сообщение.
    /// </summary>
    /// <param name="text"></param>
    public virtual void SendMessage(string text, User currentUser)
    {
      if (Id != null && currentUser != null)
      {
        Queries.PostMessage(Id.Value, currentUser.Id, text);

        NotifyPropertyChanged("DeleteChatEnabled");
        NotifyPropertyChanged("AddUserInChatEnabled");
      }
    }
    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор чата.</param>
    /// <param name="name">Наименование чата.</param>
    public Chat(int? id, string name)
    {
      this.Id= id;
      this.Name = name;
    }

    #endregion
  }
}

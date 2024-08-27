using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Чат.
  /// </summary>
  public class Chat
  {
    #region Поля и свойства
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Тип чата.
    /// </summary>
    public TypeChat Type { get; private set; }

    /// <summary>
    /// Список пользователей чата.
    /// </summary>
    public List<User> Users { get; private set; }

    /// <summary>
    /// Список сообщений.
    /// </summary>
    public List<Message> Messages { get; private set; }

    #endregion

    #region Методы.

    /// <summary>
    /// Метод получения сообщений чата.
    /// !!!!!!!!!!!!!!!!!!!!!!!!нужно доработать, чтобы получать данные с сервера.
    /// </summary>
    public void GetMessages()
    {
      Messages = new List<Message>();
      User apponent1 = new User(2, "Тимур");
      User apponent2 = new User(3, "Камилла");
      Messages.Add(new Message(1, apponent1, DateTime.Now.ToString(), "Hello", false));
      Messages.Add(new Message(2, Messenger.CurrentUser, DateTime.Now.ToString(), ";)",true));
      Messages.Add(new Message(3, apponent2, DateTime.Now.ToString(), "Hi", false));
    }

    #endregion

    #region Конструкторы
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор чата.</param>
    /// <param name="name">Наименование чата.</param>
    /// <param name="type">Тип чата.</param>
    private Chat(int id, string name, TypeChat type)
    {
      this.Id = id;
      this.Name = name;
      this.Type = type;
    }

    /// <summary>
    /// Конструктор для создания общего чата.
    /// </summary>
    /// <param name="id">Идентификатор чата.</param>
    /// <param name="name">Наименование чата.</param>
    /// <param name="users">Пользователи чата.</param>
    public Chat(int id, string name, List<User> users) : this(id, name, TypeChat.General)
    {
      this.Users = users;
    }

    /// <summary>
    /// Конструктор для создания личного чата.
    /// </summary>
    /// <param name="id">Идентификатор чата.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <param name="appender">Аппонент.</param>
    public Chat(int id, User currentUser, User appender) : this(id, appender.NickName, TypeChat.Private)
    {
      this.Users = new List<User>();
      this.Users.Add(currentUser);
      this.Users.Add(appender);
    }
    #endregion
  }
}

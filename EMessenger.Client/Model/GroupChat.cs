using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Групповой чат.
  /// </summary>
  public class GroupChat: Chat
  {
    #region Поля и свойства

    /// <summary>
    /// Имя чата с указанием типа чата.
    /// </summary>
    public override string NameWithType
    {
      get
      {
        return $"{Name} [групповой]";
      }
    }

    /// <summary>
    /// Список пользователей групповых чатов.
    /// </summary>
    public List<User> Users { get; private set; }

    /// <summary>
    /// Доступность кнопки добавления пользователя в чат.
    /// </summary>
    public override bool AddUserInChatEnabled
    {
      get
      {
        return true;
      }
    }

    #endregion

    #region Конструктор

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор чата.</param>
    /// <param name="name">Наименование чата.</param>
    public GroupChat(int id, string name):base(id, name)
    {
      this.Users = new List<User>();
    }

    #endregion
  }
}

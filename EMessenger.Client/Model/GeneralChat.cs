using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Общий чат.
  /// </summary>
  public class GeneralChat: Chat
  {
    #region Поля и свойства

    /// <summary>
    /// Имя чата с указанием типа чата.
    /// </summary>
    public override string NameWithType
    {
      get
      {
        return $"{Name} [общий]";
      }
    }

    #endregion

    #region

    /// <summary>
    /// Добавить пользователя в чат (в общий чат не нужно добавлять пользователей).
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public override bool AddAccount(User user)
    {
      return false;
    }

    #endregion

    #region Конструктор

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор чата.</param>
    /// <param name="name">Наименование чата.</param>
    public GeneralChat(int id, string name) : base(id, name) { }

    #endregion
  }
}

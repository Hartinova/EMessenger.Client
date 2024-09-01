using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Личный чат.
  /// </summary>
  public class PrivateChat:Chat
  {
    #region Поля и свойства

    /// <summary>
    /// Пользователь, с которым ведется переписка.
    /// </summary>
    public User Penfriend { get; set; }

    #endregion

    #region Методы

    /// <summary>
    /// Установить идентификатор личного чата.
    /// </summary>
    /// <param name="idChat"></param>
    private void SetIdChat(int idChat)
    {
      if (base.Id == null)
      {
        base.Id = idChat;
      }
    }

    /// <summary>
    /// Записать сообщение.
    /// </summary>
    /// <param name="text"></param>
    public override void SendMessage(string text, User currentUser)
    {
      //у личного чата может не быть идентификатора, если они еще не общались
      //в этом случае создадим и обновим идентификатор чата.
      if (Id == null)
      {
        var id = Queries.PostChat(ChatType.Private, "");
        if (id != null)
        {
          this.SetIdChat(id.Value);
        }

        //теперь добавим пользователей
        AddAccount(currentUser);
        AddAccount(this.Penfriend);
      }
      base.SendMessage(text, currentUser);
    }

    #endregion

    #region Конструктор

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор чата.</param>
    /// <param name="name">Наименование чата.</param>
    public PrivateChat(int? id, string name, User penfriend) : base(id, name) 
    {
      this.Penfriend = penfriend;
    }

    #endregion
  }
}

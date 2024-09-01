using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model.DTO
{
  /// <summary>
  /// DTO чата.
  /// </summary>
  public class ChatGroupDto
  {
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Тип чата.
    /// </summary>
    public ChatType Type { get; set; }

    /// <summary>
    /// Список пользователей чата.
    /// </summary>
    public List<Account> Accounts { get; set; }
  }
}

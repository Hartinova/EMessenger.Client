using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model.DTO
{
  /// <summary>
  /// DTO для пользователя без регистрации.
  /// </summary>
  public class UserDto
  {
    /// <summary>
    /// Ник.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="nickName">Ник.</param>
    public UserDto(string nickName)
    {
      NickName = nickName;
    }
  }
}

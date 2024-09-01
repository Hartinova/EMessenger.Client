using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model.DTO
{
  /// <summary>
  /// DTO для авторизации пользователя.
  /// </summary>
  public class UserAuthorizationDto
  {
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Никнейм.
    /// </summary>
    public string NickName { get; set; }
  }
}

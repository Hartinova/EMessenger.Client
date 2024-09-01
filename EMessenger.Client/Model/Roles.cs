using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Роли пользователя.
  /// </summary>
  public enum Roles
  {
    /// <summary>
    /// Нет роли - для неавторизованного пользователя.
    /// </summary>
    None,

    /// <summary>
    /// Все права - для авторизованного пользователя.
    /// </summary>
    All
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model.DTO
{
  /// <summary>
  /// DTO для пользователя с регистрацией.
  /// </summary>
  public class UserRegistrationDto
  {
    /// <summary>
    /// Ник.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="nickName">Ник.</param>
    /// <param name="login">Логин.</param>
    /// <param name="password">Пароль.</param>
    public UserRegistrationDto(string nickName, string login, string password)
    {
      this.NickName = nickName;
      this.Login = login;
      this.Password = password;
    }
  }
}

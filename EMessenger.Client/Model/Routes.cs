using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Пути к методам контроллера на сервере.
  /// </summary>
  public static class Routes
  {
    /// <summary>
    /// Добавить пользователя.
    /// </summary>
    public const string RouteAddUserWithoutRegistration= "api/user/add";

    /// <summary>
    /// Добавить пользователя с аккаунтом.
    /// </summary>
    public const string RouteAddUserWithRegistration = "api/user/addwithaccount";

    /// <summary>
    /// Получить данные пользователя по логину и паролю.
    /// </summary>
    public const string RouteGetByLoginAndPassword = "api/user/getbyloginandpassword/?login={0}&password={1}";

    /// <summary>
    /// Получить список пользователей с аккаунтом.
    /// </summary>
    public const string RouteGetUsers = "api/user/getallregistred";

    /// <summary>
    /// Получить список общих чатов.
    /// </summary>
    public const string RouteGetGeneralChats = "api/chat/getgeneralchats";

    /// <summary>
    /// Получить все чаты.
    /// </summary>
    public const string RouteGetAllChats= "api/chat/getallchats";

    /// <summary>
    /// Получить список групповых чатов.
    /// </summary>
    public const string RouteGetGroupChats = "api/chat/getgroupchats/?userid={0}";

    /// <summary>
    /// Получить все сообщения чата.
    /// </summary>
    public const string RouteGetAllMessages = "api/message/getallmessagesofchat/?chatid={0}";

    /// <summary>
    /// Создать чат.
    /// </summary>
    public const string RoutePostChat = "api/chat/addchat";

    /// <summary>
    /// Добавить пользователя в чат.
    /// </summary>
    public const string RouteAddAccountInChat = "api/chat/addaccountinchat/?accountid={0}&chatId={1}";

    /// <summary>
    /// Записать сообщение.
    /// </summary>
    public const string RoutePostMessage = "api/message/addmessage";
  }
}

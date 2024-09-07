using EMessenger.Client.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Запросы post/get к серверу.
  /// </summary>
  public static class Queries
  {

    #region Запросы GET

    /// <summary>
    /// Получить список всех зарегистрированных пользователей.
    /// </summary>
    /// <returns>Список зарегестрированных пользователей</returns>
    public static List<User> GetUsers()
    {
      var result = new List<User>();

      var res = SimpleGet(Routes.RouteGetUsers, out HttpStatusCode statusCode);
      if (statusCode == HttpStatusCode.OK)
      {
        var allUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<User>>(res);
        if (allUsers != null)
        { 
          return allUsers.OrderBy(e => e.NickName).ToList(); 
        }
      }
      else
      {
        MessageBox.Show($"Ошибка {statusCode}");
      }

      return result;
    }

    /// <summary>
    /// Получить список общих чатов.
    /// </summary>
    /// <returns>Список общих чатов.</returns>
    public static List<ChatGroupDto> GetGeneralChats()
    {
      var result = new List<ChatGroupDto>();

      var res = SimpleGet(Routes.RouteGetGeneralChats, out HttpStatusCode statusCode);
      if (statusCode == HttpStatusCode.OK)
      {
        var chats = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ChatGroupDto>>(res);
        if (chats != null)
        { 
          return chats.OrderBy(e => e.Name).ToList(); 
        }
      }
      else
      {
        MessageBox.Show($"Ошибка {statusCode}");
      }

      return result;
    }

    /// <summary>
    /// Получить список групповых чатов.
    /// </summary>
    /// <param name="idUser">Идентификатор пользователя.</param>
    /// <returns>Список групповых чатов.</returns>
    public static List<ChatGroupDto> GetGroupChats(int idUser)
    {
      var result = new List<ChatGroupDto>();

      var res = SimpleGet(string.Format(Routes.RouteGetGroupChats, idUser), out HttpStatusCode statusCode);
      if (statusCode == HttpStatusCode.OK)
      {
        var chats = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ChatGroupDto>>(res);
        if (chats != null)
        { 
          return chats.OrderBy(e => e.Name).ToList(); 
        }
      }
      else
      {
        MessageBox.Show($"Ошибка {statusCode}");
      }

      return result;
    }

    /// <summary>
    /// Получить идентификатор личного чата.
    /// </summary>
    /// <param name="idCurrentUser">Идентификатор пользователя.</param>
    /// <param name="idPenfriend">Идентификатор собеседника</param>
    /// <returns>Идентификатор личного чата.</returns>
    public static int? GetPrivateChat(int idCurrentUser, int idPenfriend)
    {
      var res = SimpleGet(Routes.RouteGetAllChats, out HttpStatusCode statusCode);
      if (statusCode == HttpStatusCode.OK)
      {
        var chats = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ChatGroupDto>>(res);
        if (chats != null)
        {
          var privateChates = chats.Where(e => e.Type == ChatType.Private).ToList();
          foreach(var chat in privateChates)
          {
            if (chat.Accounts!=null && chat.Accounts.Count ()>0)
            {
              if (chat.Accounts.FirstOrDefault(e=>e.UserId==idCurrentUser) !=null
                 && chat.Accounts.FirstOrDefault(e => e.UserId == idPenfriend) != null)
              {
                return chat.Id;
              }
            }
          }
        }
      }
      else
      {
        MessageBox.Show($"Ошибка {statusCode}");
      }

      return null;
    }

    /// <summary>
    /// Получить список сообщений чата.
    /// </summary>
    /// <param name="idChat">Идентификатор чата.</param>
    /// <returns>Список групповых чатов.</returns>
    public static List<Message> GetAllMessages(int idChat, User currentUser)
    {
      var result = new List<Message>();

      var res = SimpleGet(string.Format(Routes.RouteGetAllMessages, idChat), out HttpStatusCode statusCode);
      if (statusCode == HttpStatusCode.OK)
      {
        var messages = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<MessageDto>>(res);
        if (messages != null && messages.Count() > 0)
        {
          messages = messages.OrderBy(e => e.Id).ToList();
          foreach(var message in messages)
          {
            var isMyMessage=(message.User.Id==currentUser.Id);
            result.Add(new Message(message.Id, message.User, message.CreatedAt, message.Text, isMyMessage));
          }
        }
      }
      else
      {
        return result;
       // MessageBox.Show($"Ошибка {statusCode}");
      }

      return result;
    }

    #endregion

    #region Запросы POST

    /// <summary>
    /// Создать чат.
    /// </summary>
    /// <param name="type">Тип чата.</param>
    /// <param name="name">Наименование чата.</param>
    public static int? PostChat(ChatType type, string name)
    {
      using (var client = new HttpClient())
      {
        string route = Registration.GetPathServer() + Routes.RoutePostChat;

        ChatDto message = new ChatDto(name, type);

        var response = client.PostAsJsonAsync<ChatDto>(route, message).Result;
        if (response.IsSuccessStatusCode)
        {
          var result = response.Content.ReadAsStringAsync().Result;
          return  Newtonsoft.Json.JsonConvert.DeserializeObject<int>(result);
        }
        else
        {
          MessageBox.Show($"Ошибка при создании чата {response.StatusCode}");
          return 0;
        }
      }
    }


    /// <summary>
    /// Добавить пользователя в чат.
    /// </summary>
    /// <param name="idChat">Идентификатор чата.</param>
    /// <param name="idUser">Идентификатор пользователя.</param>
    /// <exception cref="Exception">Ошибка при записи.</exception>
    public static bool PostAccountInChat(int idChat, int idUser)
    {
      using (var client = new HttpClient())
      {
        string route = string.Format(Registration.GetPathServer() + Routes.RouteAddAccountInChat, idUser, idChat);
      
        var response = client.PostAsJsonAsync(route,"").Result;
        if (response.IsSuccessStatusCode)
        {
          return true;
        }
        else
        {
          MessageBox.Show($"Ошибка при создании аккаунта {response.StatusCode}");
          return false;
        }
      }
    }

    /// <summary>
    /// Записать сообщение.
    /// </summary>
    /// <param name="idChat">Идентификатор чата.</param>
    /// <param name="idUser">Идентификатор пользователя.</param>
    /// <param name="text">Текст сообщения.</param>
    /// <exception cref="Exception">Ошибка при записи сообщения.</exception>
    public static void PostMessage(int idChat, int idUser, string text)
    {
      using (var client = new HttpClient())
      {
        string route = Registration.GetPathServer() + Routes.RoutePostMessage;

        MessageDto message=new MessageDto(idUser,idChat,text);
      
        var response = client.PostAsJsonAsync<MessageDto>(route, message).Result;
        if (!response.IsSuccessStatusCode)
        {
          MessageBox.Show($"Ошибка при записи сообщения {response.StatusCode}");
        }
      }
    }

    #endregion

    #region Delete
    /// <summary>
    /// Удалить чат.
    /// </summary>
    /// <param name="idChat"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool DeleteChat(int idChat)
    {
      using (var client = new HttpClient())
      {
        string route = string.Format(Registration.GetPathServer() + Routes.RouteDeleteChat, idChat);

        var response = client.DeleteAsync(route).Result;
        if (response.IsSuccessStatusCode)
        {
          return true;
        }
        else
        {
          MessageBox.Show($"Ошибка при удалении чата {response.StatusCode}");
          return false;
        }
      }
    }

    #endregion
    /// <summary>
    /// Общий метод отправки запросов  get (Получение данных с сервера).
    /// </summary>
    /// <param name="query">адрес метода на сервере.</param>
    /// <param name="statusCode">Код результата отправки.</param>
    /// <returns></returns>
    public static string SimpleGet(string query, out HttpStatusCode statusCode)
    {
      using (var client = new HttpClient())
      {
        var response = client.GetAsync(Registration.GetPathServer() + query).Result;
        statusCode = response.StatusCode;
        return response.Content.ReadAsStringAsync().Result;
      }
    }
  }
}

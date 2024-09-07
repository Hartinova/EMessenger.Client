using EMessenger.Client.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Класс для регистрации/авторизации пользователя.
  /// </summary>
  public static class Registration
  {
    #region Поля и свойства

    /// <summary>
    /// Адрес веб сервера приложения.
    /// </summary>
    private static string pathServer;

    /// <summary>
    /// Адрес веб сервера приложения.
    /// </summary>
    private static string PathServer
    {
      get
      {
        if (pathServer == null)
        {
          pathServer = GetPathServer();
        }
        return pathServer;
      }
    }

    #endregion

    #region

    /// <summary>
    /// Получить адрес сервера из настроек.
    /// </summary>
    public static string GetPathServer()
    {
      Settings settings = new Settings();
      return settings.PathWebServer;
    }

    /// <summary>
    /// Регистрация без авторизации.
    /// </summary>
    /// <param name="userDto">Данные для регистрации неавторизованного пользователя.</param>
    /// <returns>Информация о пользователе.</returns>
    public static User RegistrateUser(UserDto userDto)
    {
      return AddUser(userDto);
    }

    /// <summary>
    /// Регистрация с авторизацией.
    /// </summary>
    /// <param name="userDto">Данные для регистрации авторизованного пользователя.</param>
    /// <returns>Информация о пользователе.</returns>
    public static User RegistrateUserAccount(UserRegistrationDto userDto)
    {
      return AddUser(userDto);
    }

    /// <summary>
    /// Авторизация.
    /// </summary>
    /// <param name="userDto">Данные для регистрации авторизованного пользователя.</param>
    /// <returns>Информация о пользователе.</returns>
    public static User AuthorizateUser(string login, string password)
    {
      return GetUserInfo(login, password);
    }

    /// <summary>
    /// Добавить пользователя без регистрации.
    /// </summary>
    /// <param name="userDto">Данные для регистрации неавторизованного пользователя.</param>
    /// <returns>Информация о пользователе.</returns>
    /// <exception cref="Exception">Ошибка соединения с сервером.</exception>
    private static User AddUser(UserDto userDto)
    {
      User user = new User();

      using (var client = new HttpClient())
      {
        string route = PathServer + Routes.RouteAddUserWithoutRegistration;

        var response = client.PostAsJsonAsync<UserDto>(route, userDto).Result;
        if (response.IsSuccessStatusCode)
        {
          var result = response.Content.ReadAsStringAsync().Result;
          var idUser = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(result);

          user.Id = idUser;
          user.NickName = userDto.NickName;
        }
        else
        {
          throw new Exception($"Ошибка {response.StatusCode}");
        }
      }

      return user;
    }

    /// <summary>
    /// Добавить пользователя с регистрацией (добавление аккаунта).
    /// </summary>
    /// <param name="userDto">Данные для регистрации авторизованного пользователя.</param>
    /// <returns>Информация о пользователе.</returns>
    /// <exception cref="Exception">Ошибка соединения с сервером.</exception>
    private static User AddUser(UserRegistrationDto userRegistrationDto)
    {
      User user = new User();

      using (var client = new HttpClient())
      {
        string route = PathServer + Routes.RouteAddUserWithRegistration;

        var response = client.PostAsJsonAsync<UserRegistrationDto>(route, userRegistrationDto).Result;
        if (response.IsSuccessStatusCode)
        {
          var result = response.Content.ReadAsStringAsync().Result;
          var idUser = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(result);

          user.Id = idUser;
          user.NickName = userRegistrationDto.NickName;
          user.Role = Roles.All;
        }
        else
        {
          MessageBox.Show($"Ошибка {response.StatusCode}");
        }
      }

      return user;
    }

    /// <summary>
    /// Получить информацию о пользователе.
    /// </summary>
    private static User GetUserInfo(string login, string password)
    {
      User user = null;
      login = HttpUtility.UrlEncode(login);
      password = HttpUtility.UrlEncode(password);

      var res = Queries.SimpleGet(string.Format(Routes.RouteGetByLoginAndPassword, login,password), out HttpStatusCode statusCode);
      if (statusCode == HttpStatusCode.OK)
      {
        user = new User();
        var userDto = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAuthorizationDto>(res);
        if (userDto != null)
        {
          user.Role = Roles.All;
          user.Id = userDto.Id;
          user.NickName=userDto.NickName;
        }
      }
      else
      {
         MessageBox.Show($"Ошибка при регистрации {statusCode}");
      }
     
      return user;
    }

    #endregion
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Чат
  /// </summary>
  public class Messenger : INotifyPropertyChanged
  {
    #region Поля и свойства

    /// <summary>
    /// Заголовок формы.
    /// </summary>
    public string Title
    {
      get
      {
        return $"Мессенджер [{CurrentUser.NickName}]";
      }
    }

    /// <summary>
    /// Текущий пользователь.
    /// </summary>
    public static User CurrentUser { get; set; }

    /// <summary>
    /// Список чатов, доступный пользователю.
    /// </summary>
    public List<Chat> Chats { get; set; }

    /// <summary>
    /// Текущий (выбранный чат)
    /// </summary>
    private Chat selectedChat;

    /// <summary>
    /// Текущий (выбранный чат)
    /// </summary>
    public Chat SelectedChat
    {
      get
      {
        return this.selectedChat;
      }

      set
      {
        if (value != this.selectedChat)
        {
          this.selectedChat = value;
          this.selectedChat?.GetMessages(CurrentUser);
          NotifyPropertyChanged("SelectedChat");
          NotifyPropertyChanged("SelectedChat.DeleteChatEnabled");
        }
      }
    }

    #endregion

    #region Поля и свойства для элементов формы

    /// <summary>
    /// Видимость панели редактирования чатов.
    /// </summary>
    public Visibility EditPanelVisible
    {
      get
      {
        return (CurrentUser == null || CurrentUser.Role == Roles.None) ? Visibility.Collapsed : Visibility.Visible;
      }
    }

    /// <summary>
    /// Видимость области с сообщениями выбранного чата.
    /// </summary>
    public Visibility MessagesPanelVisible
    {
      get
      {
        return (SelectedChat == null) ? Visibility.Hidden : Visibility.Visible;
      }
    }

    #endregion

    #region Базовая реализация INotifyPropertyChanged - необходима для автоматического обновления данных на форме (использование binding)

    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region Методы


    /// <summary>
    /// Получение списка чатов текущего пользователя.
    /// </summary>
    /// <param name="currentUser">Текущий пользователь.</param>
    public void GetChats(User currentUser, Chat lastChat = null)
    {
      Chats = new List<Chat>();

      if (currentUser == null)
      {
        return;
      }

      //Заполним список чатов общими чатами
      var generalChats = Queries.GetGeneralChats();
      foreach (var chatDto in generalChats)
      {
        GeneralChat chat = new GeneralChat(chatDto.Id, chatDto.Name);
        Chats.Add(chat);
      }

      //список пользователей и групповых чатов получаем только для зарегистрированного пользователя
      if (currentUser.Role == Roles.All)
      {
        //Заполним список чатов групповыми чатами
        var groupChats = Queries.GetGroupChats(currentUser.Id);
        foreach (var chatDto in groupChats)
        {
          GroupChat chat = new GroupChat(chatDto.Id, chatDto.Name);
          Chats.Add(chat);
        }

        //заполним списком пользователей
        var users = Queries.GetUsers();
        foreach (var user in users)
        {
          if (user.Id != currentUser.Id)
          {
            var chatId = Queries.GetPrivateChat(currentUser.Id, user.Id);
            PrivateChat chat = new PrivateChat(chatId, user.NickName, user);
            Chats.Add(chat);
          }
        }
      }

      NotifyPropertyChanged("Chats");

      if (Chats != null && Chats.Count() > 0)
      {
        if (lastChat != null)
        {
          SelectedChat = Chats.Where(e => e.Id == lastChat.Id).FirstOrDefault();
        }

        if (SelectedChat == null)
        {
          SelectedChat = Chats.First();
        }
      }
    }

    /// <summary>
    /// Отправить сообщение.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public void SendMessage(string message)
    {
      if (SelectedChat == null)
      {
        return;
      }

      selectedChat.SendMessage(message, CurrentUser);
    }

    /// <summary>
    /// Добавить общий чат.
    /// </summary>
    /// <param name="chatName">Имя нового общего чата.</param>
    public void AddGeneralChat(string chatName)
    {
      // Проверяем, не существует ли уже чат с таким именем
      if (Chats.Any(c => c.Name == chatName))
      {
        MessageBox.Show($"Чат с именем '{chatName}' уже существует.");
        return;
      }

      // Вызываем метод PostChat из Queries.cs
      int? newChatId = Queries.PostChat(ChatType.General, chatName);

      if (newChatId.HasValue)
      {
        // Создаем новый чат, используя полученный идентификатор
        GeneralChat newChat = new GeneralChat(newChatId.Value, chatName);

        // обновим список
        this.GetChats(CurrentUser);
      }
      else
      {
        // Обработка ошибки, если идентификатор чата не получен
        MessageBox.Show("Ошибка при создании чата.");
      }
    }

    /// <summary>
    /// Добавить групповой чат.
    /// </summary>
    /// <param name="chatName">Имя нового чата.</param>
    public void AddGroupChat(string chatName)
    {
      // Проверяем, не существует ли уже чат с таким именем
      if (Chats.Any(c => c.Name == chatName))
      {
        MessageBox.Show($"Чат с именем '{chatName}' уже существует.");
        return;
      }

      // Вызываем метод PostChat из Queries.cs
      int? newChatId = Queries.PostChat(ChatType.Group, chatName);

      if (newChatId.HasValue)
      {
        // Создаем новый чат, используя полученный идентификатор
        GroupChat newChat = new GroupChat(newChatId.Value, chatName);

        newChat.AddAccount(CurrentUser);

        // обновим список
        this.GetChats(CurrentUser);
      }
      else
      {
        // Обработка ошибки, если идентификатор чата не получен
        MessageBox.Show("Ошибка при создании чата.");
      }
    }

    
    public void AddUserInGroupChat(int userId)
    {
      var users = Queries.GetUsers();
      if (!users.Select(x => x.Id).Contains(userId))
      {
        MessageBox.Show($"Пользователя с таким идентификатором нет.");
        return;
      }

      var chars = Queries.GetGroupChats(userId);
      if (chars.Select(x => x.Id).Contains((int)SelectedChat.Id))
      {
        MessageBox.Show($"Пользователь уже есть в этом чате.");
        return;
      }

      bool newChatId = Queries.PostAccountInChat((int)SelectedChat.Id, userId);

      if (newChatId)
      {
        MessageBox.Show($"Пользователь добавлен в чат {SelectedChat.Name}.");
        // обновим список
        this.GetChats(CurrentUser);
      }
      else
      {
        // Обработка ошибки
        MessageBox.Show("Ошибка при при добавлении пользователя в чат.");
      }
    }


    /// <summary>
    /// Удалить чат.
    /// </summary>
    public void DeleteChat()
    {
      if (SelectedChat != null)
      {
        // Проверяем, есть ли у чата ID (т.е. он уже создан на сервере)
        if (SelectedChat.Id !=null && SelectedChat.Id != 0)
        {
          // Вызываем метод DeleteChat из Queries.cs
          bool success = Queries.DeleteChat((int)SelectedChat.Id);

          if (success)
          {
            MessageBox.Show("Чат удален.");
            this.GetChats(CurrentUser);
          }
          else
          {
            // Обработка ошибки, если удаление не удалось
            MessageBox.Show("Ошибка при удалении чата.");
          }
        }
        
      }
    }

    #endregion

    #region Конструктор

    public Messenger()
    {
      GetChats(CurrentUser);
    }

    #endregion
  }
}

using System;
using System.Windows;
using System.Windows.Media;


namespace EMessenger.Client.Model
{
  /// <summary>
  /// Сообщение.
  /// </summary>
  public class Message
  {
    #region Поля и свойства

    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Пользователь, написавший сообщение.
    /// </summary>
    public User User { get;private set; }

    /// <summary>
    /// Время записи сообщения.
    /// </summary>
    public DateTime Time {  get; private set; }

    public string TimeForView
    {
      get
      {
        return Time.ToString("dd.mm.yyyy hh:mm:ss");
      }
    }

    /// <summary>
    /// Текст.
    /// </summary>
    public string Text { get;private set; }

    /// <summary>
    /// признак, что автора сообщения - это текущий пользователь
    /// </summary>
    public bool IsMyMessage { get; private set; }

    #endregion

    #region Поля и свойства для элементов формы

    /// <summary>
    /// Привязка сообщения к краю.
    /// </summary>
    public HorizontalAlignment Alignment
    { 
      get 
      { 
        return IsMyMessage ? HorizontalAlignment.Right : HorizontalAlignment.Left; 
      } 
    }

    /// <summary>
    /// Цвет области с сообщением.
    /// </summary>
    public SolidColorBrush MessageColor
    {
      get 
      { 
        return IsMyMessage ? Brushes.Aqua : Brushes.White; 
      }
    }

    /// <summary>
    /// Видимость текста с ником.
    /// </summary>
    public Visibility NickNameVisible
    {
      get
      {
        return (IsMyMessage) ? Visibility.Collapsed : Visibility.Visible;
      }
    }
   
    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор сообщения.</param>
    /// <param name="user">Пользователь.</param>
    /// <param name="time">Время записи.</param>
    /// <param name="text">Текст.</param>
    public Message(int id, User user, DateTime time, string text, bool isMyMessage)
    {
      this.Id = id;
      this.User = user;
      this.Time = time;
      this.Text = text;
      this.IsMyMessage = isMyMessage;
    }

    #endregion
  }
}

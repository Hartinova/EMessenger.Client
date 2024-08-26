using System.Windows;

namespace EMessenger.Client.Model
{
  /// <summary>
  /// Сообщение.
  /// </summary>
  public class Message
  {
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
    public string Time {  get; private set; }

    /// <summary>
    /// Текст.
    /// </summary>
    public string Text { get;private set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор сообщения.</param>
    /// <param name="user">Пользователь.</param>
    /// <param name="time">Время записи.</param>
    /// <param name="text">Текст.</param>
    public Message(int id, User user, string time, string text)
    {
      this.Id = id;
      this.User = user;
      this.Time = time;
      this.Text = text;
    }
  }
}

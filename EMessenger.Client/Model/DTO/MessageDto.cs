using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMessenger.Client.Model.DTO
{
  /// <summary>
  /// DTO сообщения.
  /// </summary>
  internal class MessageDto
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата создания сообщения.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Пользователь.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public int ChatId { get; set; }

    /// <summary>
    /// Чат.
    /// </summary>
    public Chat Chat { get; set; }

    public MessageDto(int userId, int chatId, string text)
    {
      this.ChatId = chatId;
      this.UserId= userId;
      this.Text = text;
    }
  }
}

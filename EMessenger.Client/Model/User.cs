namespace EMessenger.Client.Model
{
  /// <summary>
  /// Пользователь.
  /// </summary>
  public class User
  {
    #region Поля и свойства
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Ник.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public Roles Role {  get; set; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public User() : this(0, "") 
    { 
      this.Role = Roles.None;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор  пользователя.</param>
    /// <param name="nickName">Ник.</param>
    public User(int id, string nickName)
    {
      this.Id = id;
      this.NickName = nickName;
    }

    #endregion
  }
}

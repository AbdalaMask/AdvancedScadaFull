namespace AdvancedScada.Controls_Binding.ImageAll
{
    /// <summary>
    /// Specifies items with unique keys
    /// <para>Определяет элементы с уникальными ключами</para>
    /// </summary>
    public interface IUniqueItem
    {
        /// <summary>
        /// Проверить ключ на уникальность
        /// </summary>
        bool KeyIsUnique(string key);
    }
}

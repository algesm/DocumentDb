namespace DocumentDb.Core.Services
{
    /// <summary>
    /// Предоставляет настройки для работы DocumentDb
    /// </summary>
    public class DocumentDbSettingsProvider : IDocumentDbSettingsProvider
    {
        /// <summary>
        /// Возвращает конфигурационную строку БД, в которой будут сохранены данные
        /// </summary>
        /// <returns></returns>
        public string GetConfigurationSettings()
        {
            return "Data Source=localhost;Initial Catalog=test2;Integrated Security=true;";
        }
    }
}
namespace DocumentDb.Core.Services
{
    /// <summary>
    /// Интерфейс для получения настроек для работы DocumentDb
    /// </summary>
    public interface IDocumentDbSettingsProvider
    {
        /// <summary>
        /// Получение конфигурационной строки соединения с БД
        /// </summary>
        /// <returns></returns>
        string GetConfigurationSettings();
    }
}
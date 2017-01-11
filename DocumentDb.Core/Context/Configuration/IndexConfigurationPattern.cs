using System.Data.Entity.ModelConfiguration;
using DocumentDb.Core.Indexes;
using DocumentDb.Core.Model;

namespace DocumentDb.Core.Context.Configuration
{
    /// <summary>
    /// Шаблон связи основной таблицы Document с таблицами индексов
    /// Предполагается автоматическое обнаружение и создание конфигураций по данному шаблону
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexConfigurationPattern<T> : EntityTypeConfiguration<T> where T : Index
    {
        public IndexConfigurationPattern()
        {
            ToTable($"{nameof(Document)}{typeof(T).Name}" );
            HasKey(t => t.IndexId);
            HasRequired<Document>(property => property.Document).WithMany();
        }
    }
}
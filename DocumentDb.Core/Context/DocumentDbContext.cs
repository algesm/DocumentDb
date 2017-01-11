using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using Autofac;
using DocumentDb.Core.Container;
using DocumentDb.Core.Context.Configuration;
using DocumentDb.Core.Context.Convention;
using DocumentDb.Core.Indexes;
using DocumentDb.Core.Model;
using DocumentDb.Core.Reflection;
using DocumentDb.Core.Services;

namespace DocumentDb.Core.Context
{
    /// <summary>
    /// Контекст работы с DocumentDb таблицами
    /// </summary>
    [DbConfigurationType(typeof(DocumentDbModelConfiguration))]
    internal class DocumentDbContext : DbContext
    {
        static DocumentDbContext()
        {
            Database.SetInitializer<DocumentDbContext>(new DocumentDbDatabaseInitializator());
        }

        public DocumentDbContext() : base(AutofacContainer.Instance.Resolve<IDocumentDbSettingsProvider>().GetConfigurationSettings())
        {
        }

        /// <summary>
        /// Сущность документ
        /// </summary>
        public IDbSet<Document> Document { get; set; }

        /// <summary>
        /// Переопределяем правила создания модели
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //убирем множественное окончание в названии создаваемых таблиц БД
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //убираем подчеркивания во внешних ключах
            modelBuilder.Conventions.Add<ForeignKeyNamingConvention>();

            //Добавляем конфигурации сущностей
            AddConfigurations(modelBuilder);

            //Конфигурируем параметры EntityFramework
            ConfigurationContext();
        }

        #region Private field

        private void ConfigurationContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }



        private void AddConfigurations(DbModelBuilder modelBuilder)
        {
            //Динамически создаем мэппинг всех определенных индексов к
            foreach (var c in BuildIndexConfiguration(typeof(Index), typeof(IndexConfigurationPattern<>)))
            {
                modelBuilder.Configurations.Add(c);
            }
        }

        /// <summary>
        /// Создание конфигураций для определенных в сборках индексов к типам
        /// </summary>
        /// <param name="index">Тип наследников</param>
        /// <param name="genericType">Тип конфиграции которую нужно создать</param>
        /// <returns></returns>
        private IEnumerable<dynamic> BuildIndexConfiguration(Type index, Type genericType)
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //найдем всех наследников T, в нашем 
            IEnumerable<Type> subTypes = index.FindSubType(assemblies);

            //Создадим дженерики
            return genericType.MakeGenericTypes(subTypes)
                //Активируем их
                .Select(t => (dynamic)Activator.CreateInstance(t));
        } 
        #endregion
    }
}
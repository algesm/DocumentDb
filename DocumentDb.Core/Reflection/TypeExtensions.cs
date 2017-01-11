using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DocumentDb.Core.Reflection
{
    /// <summary>
    /// Методы расширения для работы с <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {

        public static IEnumerable<Type> FindSubType(this Type type, IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsSubclassOf(type));
        }

        public static IEnumerable<Type> FindSubType(this Type type, IEnumerable<Type> types)
        {
            return types.Where(t => t.IsSubclassOf(type));
        }

        public static IEnumerable<Type> MakeGenericTypes(this Type genericType, IEnumerable<Type> subTypes)
        {
            return subTypes.Select(subType => genericType.MakeGenericType(subType));
        }


        /// <summary>
        /// Среди доступных типов сборок ищем типы, которые в качестве базового типа имеют тип дженерик с заданным шаблоном и с двумя аргументамию. Типы которые мы ищем сами не дженерики
        /// </summary>
        /// <param name="genericTypeDefinition">Шаблонный тип дженерик, чьи реалиации имеют базовые типы </param>
        /// <param name="firstGenericArgument">Тип первого базового аргумента</param>
        /// <returns>Перечисление BaseGenericWith2Argument</returns>
        public static IEnumerable<Type> GetTypesWithGenericDefinition(this Type genericTypeDefinition, Type firstGenericArgument)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());

            return types.Where(
                t => t.BaseType != null &&
                t.BaseType.IsGenericType &&
                t.BaseType.GetGenericTypeDefinition() == genericTypeDefinition &&
                t.BaseType.GetGenericArguments().First() == firstGenericArgument);
        }



        /// <summary>
        /// Проверка типа <see cref="Type"/> на анонимность
        /// </summary>
        /// <param name="type"><see cref="Type"/></param>
        /// <returns><value>true</value> тайп анонимный, <value>false</value> обычный.</returns>
        public static bool IsAnonymousType(this Type type)
        {
            return Attribute.IsDefined(type, typeof (CompilerGeneratedAttribute), false)
                   && type.IsGenericType && type.Name.Contains("AnonymousType")
                   && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                   && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }

        /// <summary>
        /// Упощенное имя типа
        /// </summary>
        /// <param name="type"><see cref="Type"/>.</param>
        /// <returns>Имя типа без информации о версиях.</returns>
        public static string SimplifiedTypeName(this Type type)
        {
            return $"{type.FullName}, {type.Assembly.GetName().Name}";
        }
    }
}
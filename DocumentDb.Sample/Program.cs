using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using DocumentDb.Core.Container;
using DocumentDb.Core.Services;
using DocumentDb.Sample.Documents.Position;
using DocumentDb.Sample.Documentsgfdsf;
using DocumentDb.Sample.Indexes.Position;

namespace DocumentDb.Sample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestSaveSpeed();
        }


        private static void TestSaveSpeed()
        {
            Positions positions1000_1 = GenerateTestData.GetCaseElementPosition(1000);
            Positions positions10000_1 = GenerateTestData.GetCaseElementPosition(10000);
            Positions positions100000_1 = GenerateTestData.GetCaseElementPosition(100000);
            Positions positions1000_2 = GenerateTestData.GetCaseElementPosition(1000);
            Positions positions10000_2 = GenerateTestData.GetCaseElementPosition(10000);
            Positions positions100000_2 = GenerateTestData.GetCaseElementPosition(100000);

            #region Тестовый запуск, чтоб время инициализации не влияло на конечный результат

            using (IDocumentDbSessionFactory sessionFactory = AutofacContainer.Instance.Resolve<IDocumentDbSessionFactory>())
            {
                using (IDocumentDbSession session = sessionFactory.CreateSession())
                {
                    //session.Save(positions1000_1);
                    //session.Commit();
                    var t = session.LoadWithIndexes<Positions, PositionCaseIdIndex>(a => a.CaseId == 26).ToList();

                }

                using (IDocumentDbSession session = sessionFactory.CreateSession())
                {
                    session.Add(positions1000_1);
                    session.Commit();
                    Positions t = session.LoadWithIndexes<Positions, PositionCaseIdIndex>(a => a.CaseId == 26).FirstOrDefault();
                    t.CaseElementPositions = null;
                    //session.Update(t);
                    session.Commit();                    
                }
            }


            
            #endregion

            //Console.WriteLine("=================================================================");
            //Console.WriteLine("===================Тест серлизованных данных=====================");
            //Console.WriteLine("========================Чистая база==============================");


            //Stopwatch watch = new Stopwatch();
            //watch.Start();

            //using (IDocumentDbSessionFactory sessionFactory = AutofacContainer.Instance.Resolve<IDocumentDbSessionFactory>())
            //{
            //    using (IDocumentDbSession session = sessionFactory.CreateSession())
            //    {
            //        session.Save(positions1000_1);
            //        session.Commit();
            //    }
            //}
            //Console.WriteLine("Время записи 1000 строк через серлизацию = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();

            //watch.Start();

            //using (IDocumentDbSessionFactory sessionFactory = AutofacContainer.Instance.Resolve<IDocumentDbSessionFactory>())
            //{
            //    using (IDocumentDbSession session = sessionFactory.CreateSession())
            //    {
            //        session.Save(positions1000_2);
            //        session.Commit();
            //    }
            //}
            //Console.WriteLine("Время записи 1000 строк через серлизацию = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();

            //watch.Start();

            //using (IDocumentDbSessionFactory sessionFactory = AutofacContainer.Instance.Resolve<IDocumentDbSessionFactory>())
            //{
            //    using (IDocumentDbSession session = sessionFactory.CreateSession())
            //    {
            //        session.Save(positions10000_1);
            //        session.Commit();
            //    }
            //}
            //Console.WriteLine("Время записи 10000 строк через серлизацию = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();

            //watch.Start();

            //using (IDocumentDbSessionFactory sessionFactory = AutofacContainer.Instance.Resolve<IDocumentDbSessionFactory>())
            //{
            //    using (IDocumentDbSession session = sessionFactory.CreateSession())
            //    {
            //        session.Save(positions10000_2);
            //        session.Commit();
            //    }
            //}
            //Console.WriteLine("Время записи 10000 строк через серлизацию = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();

            //watch.Start();

            //using (IDocumentDbSessionFactory sessionFactory = AutofacContainer.Instance.Resolve<IDocumentDbSessionFactory>())
            //{
            //    using (IDocumentDbSession session = sessionFactory.CreateSession())
            //    {
            //        session.Save(positions100000_1);
            //        session.Commit();
            //    }
            //}
            //Console.WriteLine("Время записи 100000 строк через серлизацию = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();
            //watch.Start();

            //using (IDocumentDbSessionFactory sessionFactory = AutofacContainer.Instance.Resolve<IDocumentDbSessionFactory>())
            //{
            //    using (IDocumentDbSession session = sessionFactory.CreateSession())
            //    {
            //        session.Save(positions100000_2);
            //        session.Commit();
            //    }
            //}
            //Console.WriteLine("Время записи 100000 строк через серлизацию = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();













            //Console.WriteLine("================================================================");
            //Console.WriteLine("===================Тест реляционных данных=======================");
            //Console.WriteLine("========================Чистая база==============================");

            //#region Тестовый запуск, чтоб время инициализации не влияло на конечный результат

            //using (PositionContext context = new PositionContext())
            //{
            //    var t = context.Positions;
            //    context.SaveChanges();
            //}

            //#endregion

            //watch.Start();
            //using (PositionContext context = new PositionContext())
            //{
            //    foreach (var cep in positions1000_1.CaseElementPositions)
            //    {
            //        var elemposition = cep.ElementPosition;
            //        var position = elemposition.Position;

            //        context.Positions.Add(position);
            //        context.ElementPositions.Add(elemposition);
            //        context.CaseElementPositions.Add(cep);
            //        context.SaveChanges();
            //    }
            //}
            //Console.WriteLine("Время записи 1000 строк в реляционном виде = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();

            //watch.Start();
            //using (PositionContext context = new PositionContext())
            //{
            //    foreach (var cep in positions1000_2.CaseElementPositions)
            //    {
            //        var elemposition = cep.ElementPosition;
            //        var position = elemposition.Position;

            //        context.Positions.Add(position);
            //        context.ElementPositions.Add(elemposition);
            //        context.CaseElementPositions.Add(cep);
            //        context.SaveChanges();
            //    }
            //}
            //Console.WriteLine("Время записи 1000 строк в реляционном виде = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();

            //watch.Start();
            //using (PositionContext context = new PositionContext())
            //{
            //    foreach (var cep in positions10000_1.CaseElementPositions)
            //    {
            //        var elemposition = cep.ElementPosition;
            //        var position = elemposition.Position;

            //        context.Positions.Add(position);
            //        context.ElementPositions.Add(elemposition);
            //        context.CaseElementPositions.Add(cep);
            //        context.SaveChanges();
            //    }
            //}
            //Console.WriteLine("Время записи 10000 строк в реляционном виде = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();


            //watch.Start();
            //using (PositionContext context = new PositionContext())
            //{
            //    foreach (var cep in positions10000_2.CaseElementPositions)
            //    {
            //        var elemposition = cep.ElementPosition;
            //        var position = elemposition.Position;

            //        context.Positions.Add(position);
            //        context.ElementPositions.Add(elemposition);
            //        context.CaseElementPositions.Add(cep);
            //        context.SaveChanges();
            //    }
            //}
            //Console.WriteLine("Время записи 10000 строк в реляционном виде = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();


            //watch.Start();
            //using (PositionContext context = new PositionContext())
            //{
            //    foreach (var cep in positions100000_1.CaseElementPositions)
            //    {
            //        var elemposition = cep.ElementPosition;
            //        var position = elemposition.Position;

            //        context.Positions.Add(position);
            //        context.ElementPositions.Add(elemposition);
            //        context.CaseElementPositions.Add(cep);
            //        context.SaveChanges();
            //    }
            //}
            //Console.WriteLine("Время записи 100000 строк в реляционном виде = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();

            //watch.Start();
            //using (PositionContext context = new PositionContext())
            //{
            //    foreach (var cep in positions100000_2.CaseElementPositions)
            //    {
            //        var elemposition = cep.ElementPosition;
            //        var position = elemposition.Position;

            //        context.Positions.Add(position);
            //        context.ElementPositions.Add(elemposition);
            //        context.CaseElementPositions.Add(cep);
            //        context.SaveChanges();
            //    }
            //}
            //Console.WriteLine("Время записи 100000 строк в реляционном виде = {0} мс", watch.ElapsedMilliseconds);
            //watch.Reset();

            Console.WriteLine("Выполнено ");
            Console.ReadLine();
        }
    }
}
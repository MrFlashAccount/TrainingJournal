using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingJournal;

namespace TrainJournalTests
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void RegistrationTest()
        {
            User user = new User()
            {
                Identificator = "1",
                Name = "Иван Иванов",
                Password = ""
            };

            Session session = new Session();
            Assert.IsFalse(session.Registration(user),"session.Registration(user)");
        }

        [TestMethod]
        public void TryLoginTest()
        {
            User user = new User()
            {
                Identificator = "1",
                Name = "Иван Иванов",
                Password = ""
            };
            Session session = new Session();
            Assert.IsFalse(session.TryLogin(user),"session.TryLogin(user)");
        }

        [TestMethod]
        public void AddExerciseTest()
        {
            User user = new User()
            {
                Identificator = "1",
                Name = "Иван Иванов",
                Password = ""
            };

            TrainJournal trainJournal = new TrainJournal()
            {
                Comment = "Test",
                Date = DateTime.Today,
                Identificator = 1,
                Login = "test",
                Name = "Жим лежа",
                NumOfReps = 5,
                NumOfSets = 5,
                User = user,
                Weight = 100
            };

            Session session = new Session();
            session.TryLogin(user);
            Assert.IsFalse(session.AddExersice(trainJournal));
        }

        [TestMethod]
        public void NewUserRegistration()
        {
            User user = new User()
            {
                Identificator = "1",
                Name = "Иван Иванов",
                Password = ""
            };

            Session session = new Session();
            Assert.IsFalse(session.Registration(user), "session.Registration(user)");
            Assert.IsFalse(session.TryLogin(user), "session.TryLogin(user)");
        }

        [TestMethod]
        public void AddNewTrainingJournal()
        {
            User user = new User()
            {
                Identificator = "1",
                Name = "Иван Иванов",
                Password = ""
            };
            Session session = new Session();
            Assert.IsFalse(session.TryLogin(user), "session.TryLogin(user)");

            TrainJournal trainJournal = new TrainJournal()
            {
                Comment = "Test",
                Date = DateTime.Today,
                Identificator = 1,
                Login = "test",
                Name = "Жим лежа",
                NumOfReps = 5,
                NumOfSets = 5,
                User = user,
                Weight = 100
            };

            Assert.IsFalse(session.AddExersice(trainJournal));
        }
    }
}

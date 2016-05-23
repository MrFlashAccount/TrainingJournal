using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingJournal;

namespace TrainJournalTests
{
    [TestClass]
    public class ModuleTesting
    {
        [TestMethod]
        public void RegisterSuccess()
        {
            User user = new User()
            {
                Identificator = "1",
                Name = "Иван Иванов",
                Password = ""
            };
            
            Assert.IsNotNull(DBworker.Registration(user));
        }

        [TestMethod]
        public void RegisterException()
        {
            Assert.IsFalse(DBworker.Registration(null),"DBworker.Registration(null) != null");
        }

        [TestMethod]
        public void LoginSuccess()
        {
            User user = new User()
            {
                Identificator = "1",
                Name = "Иван Иванов",
                Password = ""
            };

            Assert.IsNull(DBworker.Login(user));
        }

        [TestMethod]
        public void LoginException()
        {
            Assert.IsNull(DBworker.Login(null));
        }

        [TestMethod]
        public void AddExerciseSuccess()
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

            Assert.IsNotNull(DBworker.AddExersice(trainJournal));
        }

        [TestMethod]
        public void AddExerciseException()
        {
            Assert.IsFalse(DBworker.AddExersice(null),"DBworker.AddExersice(null) != null");
        }
    }
}

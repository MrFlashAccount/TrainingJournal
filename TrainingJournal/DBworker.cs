using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TrainingJournal
{
    public static class DBworker
    {
        /// <summary>
        /// Метод пробует верифицировать пользователя
        /// </summary>
        /// <param name="userdata">Данные пользователя для входа</param>
        /// <returns>Возвращает данные пользователя в случае удачной верификации, иначе возвращает null</returns>
        public static User Login(User userdata)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    userdata =
                        db.Users.FirstOrDefault(
                            p => p.Identificator == userdata.Identificator && p.Password == userdata.Password);
                    if (userdata == null) throw new UserInvalidExeption();
                }
            }
            catch
            {
                return null;
            }

            return userdata;
        }

        /// <summary>
        /// Выполняет регистрацию пользователя
        /// </summary>
        /// <param name="userdata">Данные пользователя</param>
        /// <returns>Возвращает true если регистрация успешна, иначе false</returns>
        public static bool Registration(User userdata)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.Users.Add(userdata);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Выполняет смену имени пользователя
        /// </summary>
        /// <param name="user">Текущий пользователь</param>
        /// <param name="newName">Новое имя</param>
        /// <returns>true если все ок,иначе false</returns>
        public static bool ChangeName(User user, string newName)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    User userToChange = db.Users.Find(user.Identificator);
                    userToChange.Name = newName;
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Метод меняет пароль
        /// </summary>
        /// <param name="user">Текущий пользователь</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        public static bool ChangePassword(User user, string newPassword)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    User userToChange = db.Users.Find(user.Identificator);
                    userToChange.Password = newPassword;
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool ChangeAvatar(User user, string newImage)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    User userToChange = db.Users.Find(user.Identificator);
                    userToChange.Image = newImage;
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static List<TrainJournal> GetTrainJournal(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.TrainJournals.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static bool AddExersice(TrainJournal trainJournal)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.TrainJournals.Add(trainJournal);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static List<C1RPmax> GetC1RPmaxes(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.C1RPmax.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<ArmTable> GetArmTables(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.ArmTables.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<ChestTable> GetChestTables(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.ChestTables.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<HipTable> GetHipTables(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.HipTables.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<NeckTable> GetNeckTables(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.NeckTables.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<ShinTable> GetShinTables(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.ShinTables.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<WaistTable> GetWaistTables(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.WaistTables.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static bool AddC1RPmax(C1RPmax c1RPmax)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.C1RPmax.Add(c1RPmax);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool AddArmTable(ArmTable armTable)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.ArmTables.Add(armTable);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool AddChestTable(ChestTable chestTable)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.ChestTables.Add(chestTable);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool AddHipTable(HipTable hipTable)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.HipTables.Add(hipTable);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool AddNeckTable(NeckTable neckTable)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.NeckTables.Add(neckTable);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool AddShinTable(ShinTable shinTable)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.ShinTables.Add(shinTable);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool AddWaistTable(WaistTable waistTable)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.WaistTables.Add(waistTable);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static List<Weight> GetWeight(User loginedUser)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.Weights.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static bool AddWeight(Weight weight)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    db.Weights.Add(weight);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveTrainJournals(List<TrainJournal> trainJournals)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    foreach (var trainJournal in trainJournals)
                    {
                        db.Entry(trainJournal).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static List<Weight> GetWeightByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.Weights.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<TrainJournal> GetTrainJournalsByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.TrainJournals.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<C1RPmax> GetC1RPmaxesByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.C1RPmax.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<ArmTable> GetArmTablesByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.ArmTables.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<ChestTable> GetChestTablesByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.ChestTables.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<HipTable> GetHipTablesByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.HipTables.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<NeckTable> GetNeckTablesByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.NeckTables.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<ShinTable> GetShinTablesByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.ShinTables.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<WaistTable> GetWaistTablesByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return
                        db.WaistTables.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<User> GetUserList()
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                    return db.Users.ToList();
            }
            catch
            {
                return null;
            }
        }

        public static bool RemoveTrainJournal(TrainJournal trainJournal)
        {
            try
            {
                using (TrainJournalEntities db = new TrainJournalEntities())
                {
                    TrainJournal tj = db.TrainJournals.Find(trainJournal.Identificator);
                    db.TrainJournals.Remove(tj);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
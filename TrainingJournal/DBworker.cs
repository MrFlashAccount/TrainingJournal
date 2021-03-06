﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingJournal
{
    static class DBworker
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
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    userdata = db.User.FirstOrDefault(p => p.Identificator == userdata.Identificator && p.Password == userdata.Password);
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
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    db.User.Add(userdata);
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
        public static bool ChangeName(User user,string newName)
        {
            try
            {
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    User userToChange = db.User.Find(user.Identificator);
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
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    User userToChange = db.User.Find(user.Identificator);
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
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    User userToChange = db.User.Find(user.Identificator);
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
                using (Training_JournalEntities db = new Training_JournalEntities())
                    return db.TrainJournal.Where(p => p.Login == loginedUser.Identificator).ToList();
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
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    db.TrainJournal.Add(trainJournal);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static List<UserAntropometry> GetUserAntropometry(User loginedUser)
        {
            try
            {
                using (Training_JournalEntities db = new Training_JournalEntities())
                    return db.UserAntropometry.Where(p => p.Login == loginedUser.Identificator).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static UserAntropometry GetLastUserAntropometry(User loginedUser)
        {
            try
            {
                using (Training_JournalEntities db = new Training_JournalEntities())
                    return db.UserAntropometry.Where(p => p.Login == loginedUser.Identificator).ToList().Last();
            }
            catch
            {
                return null;
            }
        }

        public static bool AddUserAntropometry(UserAntropometry userAntropometry)
        {
            try
            {
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    db.UserAntropometry.Add(userAntropometry);
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
                using (Training_JournalEntities db = new Training_JournalEntities())
                    return db.Weight.Where(p => p.Login == loginedUser.Identificator).ToList();
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
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    db.Weight.Add(weight);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveTrainJournals()
        {
            try
            {
                using (Training_JournalEntities db = new Training_JournalEntities())
                {
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static List<UserAntropometry> GetUserAntropometryByPeriod(DateTime from,DateTime to, User user)
        {
            try
            {
                using (Training_JournalEntities db = new Training_JournalEntities())
                    return
                        db.UserAntropometry.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<Weight> GetWeightByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (Training_JournalEntities db = new Training_JournalEntities())
                    return
                        db.Weight.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<TrainJournal> GeTrainJournalsByPeriod(DateTime from, DateTime to, User user)
        {
            try
            {
                using (Training_JournalEntities db = new Training_JournalEntities())
                    return
                        db.TrainJournal.Where(p => p.Login == user.Identificator && p.Date >= from && p.Date <= to)
                            .ToList();
            }
            catch
            {
                return null;
            }
        } 
    }
}
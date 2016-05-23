using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace TrainingJournal
{
    // ReSharper disable SimplifyConditionalTernaryExpression
    public class Session
    {
        /// <summary>
        /// Данные пользователя текущей сессии
        /// </summary>
        public User LoginedUser { get; private set; }
        public bool IsStarted { get; private set; }
        public BitmapImage Avatar { get; private set; }

        public Session()
        {
            LoginedUser = new User();
        }

        /// <summary>
        /// Метод выполняющий вызов верификации пользователя и устанавливающий данные сессии
        /// </summary>
        /// <param name="userdata">Данные пользователя для входа</param>
        /// <returns>true в случае удачного входа, иначе false </returns>
        public bool TryLogin(User userdata)
        {
            LoginedUser = DBworker.Login(userdata);
            if (LoginedUser == null) return false;

            IsStarted = true;
            Avatar = new BitmapImage(new Uri("./Images/" + LoginedUser.Image, UriKind.Relative));
            return true;
        }

        /// <summary>
        /// Метод выполняющий вызов регистрации пользователя с последующей верификацией.
        /// </summary>
        /// <param name="userdata">Данные для регистрации</param>
        /// <returns>true в случае удачной регистрации иначе false</returns>
        public bool Registration(User userdata)
        {
            if (DBworker.Registration(userdata))
            {
                TryLogin(userdata);
            }
            else return false;

            return true;
        }

        /// <summary>
        /// Меняет имя пользователя
        /// </summary>
        /// <param name="newName">Новое имя пользователя</param>
        /// <returns>true в случае удачной смены, иначе false</returns>
        public bool ChangeName(string newName)
        {
            if (IsStarted == false) return false;

            LoginedUser.Name = newName;
            return DBworker.ChangeName(LoginedUser, newName);
        }

        /// <summary>
        /// Пытается сменить пароль
        /// </summary>
        /// <param name="oldpassword">старый пароль</param>
        /// <param name="newpassword">новый пароль</param>
        /// <returns></returns>
        public bool TryChangePassword(string oldpassword, string newpassword)
        {
            if (IsStarted == false) return false;
            if (oldpassword != LoginedUser.Password) return false;

            return DBworker.ChangePassword(LoginedUser, newpassword);
        }

        /// <summary>
        /// Пытается сменить изображение профиля
        /// </summary>
        /// <param name="newImage">название нового изображения</param>
        /// <returns></returns>
        public bool TryChangeAvatar(string newImage)
        {
            if (IsStarted == false) return false;

            LoginedUser.Image = newImage;
            return DBworker.ChangeAvatar(LoginedUser, newImage);
        }

        /// <summary>
        /// Метод возвращает данные дневника.
        /// </summary>
        /// <returns>Данные дневника, иначе null</returns>
        public List<TrainJournal> GetTrainJournal() => IsStarted == false ? null : DBworker.GetTrainJournal(LoginedUser);

        public bool AddExersice(TrainJournal trainJournal) => IsStarted == false ? false : DBworker.AddExersice(trainJournal);

        public UserAntropometry GetLastUserAntropometry() => IsStarted == false ? null : DBworker.GetLastUserAntropometry(LoginedUser);

        public List<UserAntropometry> GetUserAntropometry() => IsStarted == false ? null : DBworker.GetUserAntropometry(LoginedUser);

        public bool AddUserAntropometry(UserAntropometry userAntropometry) => IsStarted == false ? false : DBworker.AddUserAntropometry(userAntropometry);

        public List<Weight> GetWeight() => IsStarted == false ? null : DBworker.GetWeight(LoginedUser);

        public bool AddWeight(Weight weight) => IsStarted == false ? false : DBworker.AddWeight(weight);

        public void SaveTrainJournals()
        {
            if (IsStarted == false) return;
            DBworker.SaveTrainJournals();
        }

        public List<UserAntropometry> GetUserAntropometryByPeriod(DateTime from,DateTime to) => !IsStarted ? null : DBworker.GetUserAntropometryByPeriod(@from, to, LoginedUser);

        public List<Weight> GetWeightByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetWeightByPeriod(@from, to, LoginedUser);

        public List<TrainJournal> GetTrainJournalsByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GeTrainJournalsByPeriod(@from, to, LoginedUser);

        public List<User> GetUserList() => DBworker.GetUserList();

        public bool Exit()
        {
            try
            {
                LoginedUser = null;
                IsStarted = false;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}

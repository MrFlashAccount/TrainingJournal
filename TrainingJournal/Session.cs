using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace TrainingJournal
{
    // ReSharper disable SimplifyConditionalTernaryExpression
    public class Session : INotifyPropertyChanged
    {
        /// <summary>
        /// Данные пользователя текущей сессии
        /// </summary>
        public User LoginedUser { get; private set; }
        public bool IsStarted { get; private set; }
        public BitmapImage Avatar { get; private set; }

        private List<TrainJournal> _trainJournals;
        private List<Weight> _userWeights;
        private Weight _currentWeight;
        private UserAntropometry _currentUserAntropometry;
        private List<UserAntropometry> _userAntropometries;

        public List<TrainJournal> TrainJournals
        {
            get { return _trainJournals; }
            set
            {
                _trainJournals.AddRange(value);
                OnPropertyChanged();
            }
        }

        public List<Weight> UserWeights
        {
            get { return _userWeights; }
            set
            {
                _userWeights.AddRange(value);
                OnPropertyChanged();
            }
        }

        public List<UserAntropometry> UserAntropometries {
            get { return _userAntropometries; }
            set
            {
                _userAntropometries.AddRange(value);
                OnPropertyChanged();
            }
        }

        public Weight CurrentWeight
        {
            get { return _currentWeight; }
            set
            {
                _currentWeight = value;
                OnPropertyChanged();
            }
        }

        public UserAntropometry CurrentUserAntropometry
        {
            get { return _currentUserAntropometry; }
            set
            {
                _currentUserAntropometry = value;
                OnPropertyChanged();
            }
        }

        public Session()
        {
            _userAntropometries = new List<UserAntropometry>();
            LoginedUser = new User();
            _trainJournals = new List<TrainJournal>();
            _userWeights = new List<Weight>();
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
            TrainJournals = GetTrainJournal();
            UserWeights = GetWeight();
            UserAntropometries = GetUserAntropometry();

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

        public bool AddExersice(TrainJournal trainJournal)
        {
            if (IsStarted == false) return false;

            try
            {
                TrainJournals.Add(trainJournal);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public UserAntropometry GetLastUserAntropometry() => IsStarted == false ? null : DBworker.GetLastUserAntropometry(LoginedUser);

        public List<UserAntropometry> GetUserAntropometry() => IsStarted == false ? null : DBworker.GetUserAntropometry(LoginedUser);

        public bool AddUserAntropometry(UserAntropometry userAntropometry) => IsStarted == false ? false : DBworker.AddUserAntropometry(userAntropometry);

        public List<Weight> GetWeight() => IsStarted == false ? null : DBworker.GetWeight(LoginedUser);

        public bool AddWeight(Weight weight) => IsStarted == false ? false : DBworker.AddWeight(weight);

        public void SaveTrainJournals()
        {
            if (IsStarted == false) return;

            foreach (var trainJournal in TrainJournals.Where(x => x.Identificator == 0))
            {
                DBworker.AddExersice(trainJournal);
            }


            DBworker.SaveTrainJournals(TrainJournals.Where(x => x.Identificator != 0).ToList());
        }

        // ReSharper disable once InconsistentNaming
        public bool DeleteTrainJournalByID(TrainJournal trainJournal)
        {
            if (!IsStarted) return false;

            try
            {
                if (trainJournal.Identificator != 0) DBworker.RemoveTrainJournal(trainJournal);

                TrainJournals.Remove(trainJournal);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public List<UserAntropometry> GetUserAntropometryByPeriod(DateTime from,DateTime to) => !IsStarted ? null : DBworker.GetUserAntropometryByPeriod(@from, to, LoginedUser);

        public List<Weight> GetWeightByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetWeightByPeriod(@from, to, LoginedUser);

        public List<TrainJournal> GetTrainJournalsByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GeTrainJournalsByPeriod(@from, to, LoginedUser);

        public List<User> GetUserList() => DBworker.GetUserList();

        public bool Exit()
        {
            try
            {
                SaveTrainJournals();

                LoginedUser = null;
                _trainJournals = new List<TrainJournal>();
                IsStarted = false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (prop == "UserWeights") CurrentWeight = UserWeights.LastOrDefault();
            if (prop == "UserAntropometries") CurrentUserAntropometry = UserAntropometries.LastOrDefault();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}

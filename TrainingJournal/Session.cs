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

        #region Hidden Fields OnPropertyChanged

        private List<TrainJournal> _trainJournals;
        private List<Weight> _userWeights;
        private List<C1RPmax> _c1RPmaxes;
        private List<ArmTable> _armTables;
        private List<ChestTable> _chestTables;
        private List<HipTable> _hipTables;
        private List<NeckTable> _neckTables;
        private List<ShinTable> _shinTables;
        private List<WaistTable> _waistTables;

        #endregion

        #region Public Fields OnPropertyChanged

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

        public List<C1RPmax> C1RPmaxes
        {
            get { return _c1RPmaxes; }
            set
            {
                _c1RPmaxes.AddRange(value);
                OnPropertyChanged();
            }
        }

        public List<ArmTable> ArmTables
        {
            get { return _armTables; }
            set
            {
                _armTables.AddRange(value);
                OnPropertyChanged();
            }
        }

        public List<ChestTable> ChestTables
        {
            get { return _chestTables; }
            set
            {
                _chestTables.AddRange(value);
                OnPropertyChanged();
            }
        }

        public List<HipTable> HipTables
        {
            get { return _hipTables; }
            set
            {
                _hipTables.AddRange(value);
                OnPropertyChanged();
            }
        }

        public List<NeckTable> NeckTables
        {
            get { return _neckTables; }
            set
            {
                _neckTables.AddRange(value);
                OnPropertyChanged();
            }
        }

        public List<ShinTable> ShinTables
        {
            get { return _shinTables; }
            set
            {
                _shinTables.AddRange(value);
                OnPropertyChanged();
            }
        }

        public List<WaistTable> WaistTables
        {
            get { return _waistTables; }
            set
            {
                _waistTables.AddRange(value);
                OnPropertyChanged();
            }
        }

        #endregion

        public Session()
        {
            LoginedUser = new User();
            _trainJournals = new List<TrainJournal>();
            _userWeights = new List<Weight>();
            _c1RPmaxes = new List<C1RPmax>();
            _armTables = new List<ArmTable>();
            _chestTables = new List<ChestTable>();
            _hipTables = new List<HipTable>();
            _neckTables = new List<NeckTable>();
            _shinTables = new List<ShinTable>();
            _waistTables = new List<WaistTable>();
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
            C1RPmaxes = GetC1RPmaxes();
            ArmTables = GetArmTables();
            ChestTables = GetChestTables();
            HipTables = GetHipTables();
            NeckTables = GetNeckTables();
            ShinTables = GetShinTables();
            WaistTables = GetWaistTables();

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

        public List<C1RPmax> GetC1RPmaxes() => IsStarted == false ? null : DBworker.GetC1RPmaxes(LoginedUser);
        public List<ArmTable> GetArmTables() => IsStarted == false ? null : DBworker.GetArmTables(LoginedUser);
        public List<ChestTable> GetChestTables() => IsStarted == false ? null : DBworker.GetChestTables(LoginedUser);
        public List<HipTable> GetHipTables() => IsStarted == false ? null : DBworker.GetHipTables(LoginedUser);
        public List<NeckTable> GetNeckTables() => IsStarted == false ? null : DBworker.GetNeckTables(LoginedUser);
        public List<ShinTable> GetShinTables() => IsStarted == false ? null : DBworker.GetShinTables(LoginedUser);
        public List<WaistTable> GetWaistTables() => IsStarted == false ? null : DBworker.GetWaistTables(LoginedUser);

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

        public void SaveUserAntropometries()
        {
            if (IsStarted == false) return;

            foreach (var weight in UserWeights.Where(x => x.Identificator == 0))
            {
                DBworker.AddWeight(weight);
            }

            foreach (var c1RPmax in C1RPmaxes.Where(x=>x.Identificator==0))
            {
                DBworker.AddC1RPmax(c1RPmax);
            }

            foreach (var armTable in ArmTables.Where(x => x.Identificator == 0))
            {
                DBworker.AddArmTable(armTable);
            }

            foreach (var chestTable in ChestTables.Where(x => x.Identificator == 0))
            {
                DBworker.AddChestTable(chestTable);
            }

            foreach (var hipTable in HipTables.Where(x => x.Identificator == 0))
            {
                DBworker.AddHipTable(hipTable);
            }

            foreach (var neckTable in NeckTables.Where(x => x.Identificator == 0))
            {
                DBworker.AddNeckTable(neckTable);
            }

            foreach (var shinTable in ShinTables.Where(x => x.Identificator == 0))
            {
                DBworker.AddShinTable(shinTable);
            }

            foreach (var waistTable in WaistTables.Where(x => x.Identificator == 0))
            {
                DBworker.AddWaistTable(waistTable);
            }
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

        public List<C1RPmax> GetC1RPmaxesByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetC1RPmaxesByPeriod(@from, to, LoginedUser);
        public List<ArmTable> GetArmTablesByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetArmTablesByPeriod(@from, to, LoginedUser);
        public List<ChestTable> GetChestTablesByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetChestTablesByPeriod(@from, to, LoginedUser);
        public List<HipTable> GetHipTablesByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetHipTablesByPeriod(@from, to, LoginedUser);
        public List<NeckTable> GetNeckTablesByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetNeckTablesByPeriod(@from, to, LoginedUser);
        public List<ShinTable> GetShinTablesByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetShinTablesByPeriod(@from, to, LoginedUser);
        public List<WaistTable> GetWaistTablesByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetWaistTablesByPeriod(@from, to, LoginedUser);

        public List<Weight> GetWeightByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetWeightByPeriod(@from, to, LoginedUser);

        public List<TrainJournal> GetTrainJournalsByPeriod(DateTime from, DateTime to) => !IsStarted ? null : DBworker.GetTrainJournalsByPeriod(@from, to, LoginedUser);

        public List<User> GetUserList() => DBworker.GetUserList();

        public bool Exit()
        {
            try
            {
                SaveTrainJournals();
                SaveUserAntropometries();

                _trainJournals = new List<TrainJournal>();
                _c1RPmaxes = new List<C1RPmax>();
                _armTables = new List<ArmTable>();
                _chestTables = new List<ChestTable>();
                _hipTables = new List<HipTable>();
                _neckTables = new List<NeckTable>();
                _shinTables = new List<ShinTable>();
                _waistTables = new List<WaistTable>();
                _userWeights = new List<Weight>();

                LoginedUser = null;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
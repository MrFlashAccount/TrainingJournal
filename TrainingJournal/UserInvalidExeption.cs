using System;

namespace TrainingJournal
{
    class UserInvalidExeption : SystemException
    {
        public override string Message { get; }

        public UserInvalidExeption()
        {
            Message = "Неверный логин или пароль";
        }
    }
}

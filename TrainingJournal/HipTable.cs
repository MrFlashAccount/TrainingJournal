//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrainingJournal
{
    using System;
    using System.Collections.Generic;
    
    public partial class HipTable
    {
        public int Identificator { get; set; }
        public System.DateTime Date { get; set; }
        public float Hip { get; set; }
        public string Login { get; set; }
    
        public virtual User User { get; set; }
    }
}

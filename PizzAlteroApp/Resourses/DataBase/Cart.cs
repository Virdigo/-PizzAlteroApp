//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PizzAlteroApp.Resourses.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public int id_Cart { get; set; }
        public int id_user { get; set; }
        public int id_Product { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Users Users { get; set; }
    }
}

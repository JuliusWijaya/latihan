//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestingApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int Id { get; set; }
        public string Nim { get; set; }
        public string Name { get; set; }
        public string Jk { get; set; }
        public Nullable<int> Id_jurusan { get; set; }
    
        public virtual Jurusan Jurusan { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test_Sibers.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectWorker
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int WorkerID { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual Worker Worker { get; set; }
    }
}

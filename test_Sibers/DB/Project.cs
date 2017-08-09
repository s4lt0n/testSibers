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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            this.ProjectWorker = new HashSet<ProjectWorker>();
            this.Customer = new Company();
            this.Executor = new Company();
            this.Leader = new Worker();
            this.Companies = new HashSet<Company>();
            this.Workers = new List<Worker>();
        }

        public int ID { get; set; }
        [DisplayName("��������")]
        [Required(ErrorMessage = "������� ��������")]
        public string Name { get; set; }
        [DisplayName("��������")]
        [Required(ErrorMessage = "�������� ���������")]
        public Nullable<int> CustomerID { get; set; }
        [DisplayName("�����������")]
        [Required(ErrorMessage = "�������� �����������")]
        public Nullable<int> ExecutorID { get; set; }
        [DisplayName("������������ �������")]
        [Required(ErrorMessage = "�������� ������������")]
        public Nullable<int> LeaderID { get; set; }
        [DisplayName("���� ������")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayName("���� ���������")]
        //[Required(ErrorMessage = "�������� ����")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [DisplayName("���������")]
        [Required(ErrorMessage = "������� ��������� �������")]
        [Range(0,int.MaxValue,ErrorMessage ="��������� �� ����� ���� �������������")]
        public int Priority_ { get; set; }
        [DisplayName("�����������")]
        public string Comment { get; set; }

        public List<int> WorkersIDs { get; set; }
        public List<int> ProjectsIDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DisplayName("��������� �������:")]
        public virtual ICollection<ProjectWorker> ProjectWorker { get; set; }
        public virtual ICollection<Company> Companies{ get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
        public virtual Company Customer { get; set; }
        public virtual Company Executor { get; set; }
        public virtual Worker Leader { get; set; }

    }
}
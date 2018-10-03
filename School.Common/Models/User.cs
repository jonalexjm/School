namespace School.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50)]//only you can to write 50 characters
        [DataType(DataType.EmailAddress)]
        // [Index("UserNameIndex", Isnique = true)]
        public string UserName { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public string Photo { get; set; }

        [Display(Name = "Is Student")]
        public bool IsStudent { get; set; }

        [Display(Name = "Is Teacher")]
        public bool IsTeacher { get; set; }



        /**
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.Photo))
                {
                    return "nouser";
                }

                return $"https://apischool2.azurewebsites.net/api/Users/{this.Photo.Substring(1)}",
                       
            }

    **/






    }
}


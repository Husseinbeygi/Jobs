using Domain.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.UserAgg
{

    public class User : Entity,
                        IEntityHasIsActive,
                        IEntityHasIsUpdatable,
                        IEntityHasIsDeletable
    {
        public User()
        {
            IsActive = true;
            IsDeletable = true;
            IsUpdatable = true;
        }




        // **********
        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        [MaxLength(Constants.MaxLength.FirstName)]
        public string FirstName { get; set; }
        // **********

        // **********
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        [MaxLength(Constants.MaxLength.LastName)]
        public string LastName { get; set; }
        // **********

        // **********
        [Display(Name = "ایمیل")]
        [MaxLength(Constants.MaxLength.EmailAddress)]
        public string? EmailAddress { get; set; }
        // **********

        // **********
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
        // **********

        // **********
        [Display(Name = "نام کاربری")]
        [MaxLength(Constants.MaxLength.Username)]
        public string? Username { get; set; }
        // **********

        // **********
        [Display(Name = "قابل حذف")]
        public bool IsDeletable { get; set; }
        // **********

        // **********
        [Display(Name = "قابل آپدیت")]
        public bool IsUpdatable { get; set; }
        // **********

        // **********
        [Display(Name = "شماره همراه")]
        [MaxLength(Constants.FixedLength.CellPhoneNumber)]
        public string? CellPhoneNumber { get; set; }
        // **********

        // **********
        [Display(Name = "رمز عبور")]
        [MaxLength(Constants.FixedLength.DatabasePassword)]
        public string? Password { get; set; }
        // **********

        // **********
        [Display(Name = "آخرین تاریخ آپدیت")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime UpdateDateTime { get; set; }
        // **********

        // **********
        [Display(Name = "نقش دسترسی")]
        [MaxLength(10)]
        public string? Role { get; set; }
        // **********
        public void SetUpdateDateTime()
        {
            UpdateDateTime = Utility.Now;
        }

        public void Edit(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
            IsActive = user.IsActive;
            Username = user.Username;
            IsDeletable = user.IsDeletable;
            IsUpdatable = user.IsUpdatable;
            CellPhoneNumber = user.CellPhoneNumber;
            SetUpdateDateTime();
        }

    }
}

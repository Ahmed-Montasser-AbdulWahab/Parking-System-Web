using System.ComponentModel.DataAnnotations;

namespace Parking_System.Model
{
    
    public class SystemUser
    {
        [Required] //, RegularExpression("([a-z]|[A-Z])+")]
        public string Name { get; set; }

        [Required, 
            RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        public string Email { get; set; }

        [Required, StringLength(10,MinimumLength=6)]
        public string Password { get; set; }

        [Required]
        public string Type { get; set; } //0:Operator, 1:Admin

        //public bool IsActivated { set {

        //        if (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Password))
        //        {
        //            IsActivated = true;
        //        }
        //        else
        //        {
        //            IsActivated = false;
        //        }
        //    }
        //    get { return IsActivated; }  }
    }
}

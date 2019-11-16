using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

       
        [Required]
        public string UserName { get; set; }  
        
        public int Credit_ID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //بداية حقول الداتا ماينينغ
        public int Age { get; set; }     
        public string EducationLevel { get; set; }
        public string Gender { get; set; }
        public string HomeOwnerShaip { get; set; }
        public string InternetConnections { get; set; }
        public string MaritalStatus { get; set; }
        public string MovieSelector { get; set; }
        public int NumBathrooms { get; set; }
        public int NumBedrooms { get; set; }
        public int NumCars { get; set; }
        public int NumChildren { get; set; }
        public int NumTVs { get; set; }
        public string PPV_Freq { get; set; }
        public string Buying_Freq { get; set; }
        public string Format { get; set; }
        public string RentingFreq { get; set; }
        public string ViewigFreq { get; set; }
        public string TheaterFreq { get; set; }
        public string TV_MovieFreq { get; set; }
        public string TV_Signal { get; set; }
        public string Channel { get; set; }
        public string Criteria { get; set; }
        public string Technology { get; set; }
        public string Hobbies { get; set; }

        //نهاية حقول الداتا ماينينغ

    }

    public class EditRegisterViewModel
    {

        public int id { get; set; }
        [Required]
        public string UserName { get; set; }

        public int Credit_ID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }

        //بداية حقول الداتا ماينينغ
        public int Age { get; set; }
        public string EducationLevel { get; set; }
        public string Gender { get; set; }
        public string HomeOwnerShaip { get; set; }
        public string InternetConnections { get; set; }
        public string MaritalStatus { get; set; }
        public string MovieSelector { get; set; }
        public int NumBathrooms { get; set; }
        public int NumBedrooms { get; set; }
        public int NumCars { get; set; }
        public int NumChildren { get; set; }
        public int NumTVs { get; set; }
        public string PPV_Freq { get; set; }
        public string Buying_Freq { get; set; }
        public string Format { get; set; }
        public string RentingFreq { get; set; }
        public string ViewigFreq { get; set; }
        public string TheaterFreq { get; set; }
        public string TV_MovieFreq { get; set; }
        public string TV_Signal { get; set; }
        public string Channel { get; set; }
        public string Criteria { get; set; }
        public string Technology { get; set; }
        public string Hobbies { get; set; }


        //نهاية حقول الداتا ماينينغ

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

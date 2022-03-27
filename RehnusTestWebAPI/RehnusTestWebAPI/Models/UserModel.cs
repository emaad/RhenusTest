using System.ComponentModel.DataAnnotations;

namespace RehnusTestWebAPI.Models
{
    /// <summary>
    /// This class will be used to get values from the end user and check the validation.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Any username can be provided
        /// </summary>
        [Required(ErrorMessage = "Username required")]
        [MaxLength(50,ErrorMessage ="Username must be maximum 50 characters")]
        public string UserName { get; set; }

        /// <summary>
        /// Password must be from 8 to 15 characters
        /// </summary>
        [Required(ErrorMessage = "Password required")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters")]
        [MaxLength(15, ErrorMessage = "Username must be maximum 15 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

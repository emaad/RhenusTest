using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RehnusTestWebAPI.Models
{
    /// <summary>
    /// This class will allow current user to bet in the system using two parameters Points and Bet Number.
    /// </summary>
    public class UserBetModel
    {
        /// <summary>
        /// Points must be less than equal to current points.
        /// </summary>
        [Required(ErrorMessage = "Points required (must be less than equal to your points).")]
        [Range(1, int.MaxValue, ErrorMessage = "Points must be greater than 0.")]
        [DefaultValue("0")]
        public int Points { get; set; }

        /// <summary>
        /// Any number from 0 to 9 for betting in the system.
        /// </summary>
        [Required(ErrorMessage = "Bet number required (must be in between 1 to 9).")]
        [Range(1, 9, ErrorMessage = "Bet number must be from 1 to 9.")]
        [DefaultValue("0")]
        public int BetNumber { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace RehnusTestWebAPI.DataModels
{
    /// <summary>
    /// Code first approached used. This class will be generating table in DB.
    /// </summary>
    public partial class User : IdentityUser
    {
        public long UserPoints { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }

}

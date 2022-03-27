using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RehnusTestWebAPI.DataModels;

namespace RehnusTestWebAPI.Helpers
{

    /// <summary>
    /// Customer defined user manager with override methods to customize the functionality.
    /// </summary>
    public class CustomUserManager : UserManager<User>
    {
        public CustomUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        /// <summary>
        /// Return user if user id match
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override async Task<User> FindByIdAsync(string userId)
        {
            return await base.FindByIdAsync(userId);
        }
        /// <summary>
        /// return user if email match.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public override async Task<User> FindByEmailAsync(string email)
        {
            return await base.FindByEmailAsync(email);
        }
        /// <summary>
        /// return user if username match.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public override async Task<User> FindByNameAsync(string userName)
        {
            return await base.FindByNameAsync(userName);
        }
    }
}

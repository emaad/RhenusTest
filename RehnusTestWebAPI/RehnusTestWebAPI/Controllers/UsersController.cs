using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehnusTestWebAPI.DataModels;
using RehnusTestWebAPI.Helpers;
using RehnusTestWebAPI.Models;

namespace RehnusTestWebAPI.Controllers
{
    /// <summary>
    /// User login and registration
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;
        private readonly CustomUserManager _manager;

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration, CustomUserManager manager)
        {
            _logger = logger;
            _configuration = configuration;
            _manager = manager;
        }
        /// <summary>
        /// User will get jwt token & current points if credentials authenticated.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Jwt Token</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> UserLogin(UserModel model)
        {

            try
            {
                if (!ModelState.IsValid)//Check if model valid
                {
                    _logger.LogError($"{LogLabels.Login} Invalid information submitted for username: {model.UserName}");
                    return BadRequest(new APIResponse()
                    {
                        Message = ApiMessages.ErrorMessage,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray()
                    });
                }

                var user = await _manager.FindByNameAsync(model.UserName);// checking if username exists in the system

                if (user == null)
                {
                    _logger.LogError($"{LogLabels.Login} Username not exists: {model.UserName}");
                    return BadRequest(new APIResponse()
                    {
                        Message = ApiMessages.ErrorMessage,
                        Errors = new string[] { "Please check username and password" }
                    });
                }
                if (user != null && await _manager.CheckPasswordAsync(user, model.Password) == true)//Checking if user credentials are valid
                {
                    var jwtAuthorization = new JWTAuthorization(_configuration);

                    var accessTokenDict = new Dictionary<string, object>();//Dictionary object created to send user related information
                    accessTokenDict.Add(ApiMessages.AccessToken, jwtAuthorization.GenerateToken(user.Id));//generating jwt token and adding the dictionary object
                    accessTokenDict.Add(ApiMessages.CurrentPoints, user.UserPoints.ToString());//Sending current points 

                    _logger.LogInformation($"{LogLabels.Login} Generated access token for username: {user.UserName}");
                    return Ok(new APIResponse()
                    {
                        Data = accessTokenDict
                    });
                }
                else
                {
                    _logger.LogError($"{LogLabels.Login} Invalid username: {model.UserName} or password provided");
                    return BadRequest(new APIResponse()//If user not valid
                    {
                        Message = ApiMessages.ErrorMessage,
                        Errors = new string[] { "Please check username and password" }
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{LogLabels.Login} " + ex.ToString());//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// User will be registered using username and password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult> UserRegister(UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)//Check if model valid
                {
                    _logger.LogError($"{LogLabels.Register} Invalid information submitted for username: {model.UserName}");

                    return BadRequest(new APIResponse()
                    {
                        Message = ApiMessages.ErrorMessage,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray()
                    });
                }

                var userModel = new User()
                {
                    CreatedDateTime = DateTime.Now,
                    UserName = model.UserName,
                    UserPoints = 10000,

                };
                var result = await _manager.CreateAsync(userModel, model.Password);//user will be created if username is unique

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(a => a.Description).ToArray();
                    _logger.LogError($"{LogLabels.Register} Fatal issues: {string.Join(',', errors) }");
                    return BadRequest(new APIResponse()
                    {
                        Message = ApiMessages.ErrorMessage,
                        Errors = errors
                    });
                }
                _logger.LogInformation($"{LogLabels.Register} Successfull - username: {userModel.UserName}");

                return Ok(new APIResponse()
                {
                    Message = ApiMessages.SuccessMessage,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{LogLabels.Register} { ex.ToString()}");//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }

        }

    }
}
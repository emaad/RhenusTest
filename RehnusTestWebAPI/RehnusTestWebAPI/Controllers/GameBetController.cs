using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehnusTestWebAPI.DataModels;
using RehnusTestWebAPI.Helpers;
using RehnusTestWebAPI.Models;

namespace RehnusTestWebAPI.Controllers
{
    /// <summary>
    /// Validated user will be able to bet in the game.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GameBetController : ControllerBase
    {
        private readonly ILogger<GameBetController> _logger;
        private readonly CustomUserManager _manager;
        private readonly JWTAuthorization jwtAuthorization;

        public GameBetController(ILogger<GameBetController> logger, IConfiguration configuration, CustomUserManager manager)
        {
            _logger = logger;
            _manager = manager;
            jwtAuthorization = new JWTAuthorization(configuration);
        }
        /// <summary>
        /// Returning the current points of the user.
        /// </summary>
        /// <returns>Points</returns>
        [HttpPost]
        [Route("CheckPoints")]
        public async Task<IActionResult> CheckPoints()
        {
            try
            {
                var userId = jwtAuthorization.ValidateToken(Request);//Validate the token anyway, just in case to void issues.

                var user = await _manager.FindByIdAsync(userId);

                var returnValues = new Dictionary<string, object>();
                returnValues.Add(ApiMessages.CurrentPoints, user.UserPoints);

                _logger.LogInformation($"User :{user.Id} Current Points: {user.UserPoints}");
                return Ok(new APIResponse()
                {
                    Message = ApiMessages.SuccessMessage,
                    Data = returnValues
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }


        /// <summary>
        /// Authenticated user will make a bet with desire points and bet number.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MakeBet")]
        public async Task<ActionResult> MakeABet(UserBetModel model)
        {
            try
            {
                if (!ModelState.IsValid)//Check if model valid
                {
                    _logger.LogError($"{LogLabels.MakeBet} Invalid bet submitted");

                    return BadRequest(new APIResponse()
                    {
                        Message = ApiMessages.ErrorMessage,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray()
                    });

                }
                var userId = jwtAuthorization.ValidateToken(Request);//Validate the token anyway, just in case to void issues.

                var user = await _manager.FindByIdAsync(userId);

                if (user.UserPoints < model.Points)//If bet points are greater than current points then not allowed.
                {
                    _logger.LogError($"{LogLabels.MakeBet} User {user.Id} bet points greater than current points {user.UserPoints}");

                    return BadRequest(new APIResponse()
                    {
                        Message = ApiMessages.ErrorMessage,
                        Errors = new string[] { $"You can't bet greater than your points. Your current points are {user.UserPoints}" }
                    });
                }
                var rndFtn = new Random();
                var rndNumber = rndFtn.Next(0, 10);//Select random number from 0 to 9.

                var isBetSuccess = false;//Parameter defined to check if bet succeeded or not.

                if (rndNumber == model.BetNumber)//If random number and user provided bet number matches
                {
                    user.UserPoints = user.UserPoints + (model.Points * 9);//add current points with user bet points.
                    isBetSuccess = true;
                }
                else
                {
                    user.UserPoints = user.UserPoints - model.Points;//minus bet points from current points.
                    isBetSuccess = false;

                }
                user.UpdatedDateTime = DateTime.Now;//Update the record date and time.

                var result = await _manager.UpdateAsync(user);

                if (result.Succeeded)//If update works
                {
                    var returnValues = new Dictionary<string, object>();
                    returnValues.Add(ApiMessages.CurrentPoints, user.UserPoints);
                    returnValues.Add(ApiMessages.Points, isBetSuccess ? $"+{model.Points}" : $"-{model.Points}");

                    var msg = isBetSuccess ? ApiMessages.SuccessBetMessage : ApiMessages.ErrorBetMessage;
                    _logger.LogInformation($"{LogLabels.MakeBet} {user.Id} {ApiMessages.CurrentPoints}: {user.UserPoints}, {msg }");
                    return Ok(new APIResponse()
                    {
                        Message = msg,//setting message as per bet status.
                        Data = returnValues
                    });
                }
                else
                {
                    _logger.LogError($"{LogLabels.MakeBet} {user.Id} Bet not succeeded");
                    return BadRequest(new APIResponse()//returning errors found while performing DB action.
                    {
                        Message = ApiMessages.ErrorMessage,
                        Errors = result.Errors.Select(a => a.Description).ToArray()
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{LogLabels.MakeBet} {ex.ToString()}");//Log error details
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
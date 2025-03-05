using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace HelloGreetingApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private readonly IGreetingBL greetingBL;

        ResponseModel<string> response = new ResponseModel<String>();

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        public HelloGreetingController(IGreetingBL _greetingBL)
        {
            greetingBL = _greetingBL;
        }

        /// <summary>
        /// Get Method to get the Greeting Message
        /// </summary>
        /// <param name="responseModel"></param>
        /// <returns> response model </returns>
        ///

        [HttpGet("{id}")]
        public IActionResult GetGreetingById(int id)
        {
            try
            {
                logger.Info($"GET request received to find greeting with ID={id}");
                var greeting = greetingBL.GetGreetingById(id);

                if (greeting == null)
                {
                    response.Sucess = false;
                    response.Message = "Greeting not found!";
                    response.Data = null;
                    logger.Info("Find Greeting by ID failed.");
                    return NotFound(response);
                }

                response.Sucess = true;
                response.Message = "Greeting found successfully.";
                response.Data = greeting.Message;
                logger.Info("Find Greeting by ID succeeded.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while fetching greeting by ID.");
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// Get Greeting Message (Hello World)
        /// </summary>
        [HttpGet]
        [Route("getMsg")]
        public string GetMsg1()
        {
            logger.Info("GET request received at /getMsg");
            string message = greetingBL.GetMsg();
            logger.Info($"Response: {message}");
            return message;
        }
        /// <summary>
        /// Get All Greeting Message 
        /// </summary>
        ///


        [HttpGet("all")]
        public IActionResult GetAllGreetings()
        {
            var greetings = greetingBL.GetAllGreetings();
            if (greetings == null || greetings.Count == 0)
            {
                logger.Info("No greetings found.");
                return NotFound(new { Success = false, Message = "No greetings found." });
            }

            logger.Info("All greetings retrieved successfully.");
            return Ok(new { Success = true, Data = greetings });
        }
        /// <summary>
        /// Post Greeting Message based on First Name and/or Last Name
        /// </summary>
        ///

        [HttpPost]
        [Route("postMsgByName")]
        public string PostMsgByName([FromBody] UserNameRequestModel userNameRequestModel)
        {
            logger.Info($"POST request received at /postMsgByName with Data: FirstName={userNameRequestModel.FirstName}, LastName={userNameRequestModel.LastName}");

            string message = greetingBL.GetGreetingMessage(userNameRequestModel);

            logger.Info($"Response: {message}");
            return message;
        }
        /// <summary>
        /// Post Method to post the Greeting Message
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns> response model </returns>
        ///

        [HttpPost]
        public IActionResult Post(RequestGreetingModel requestGreetingModel)
        {
            try
            {
                logger.Info($"POST request received: Value={requestGreetingModel.Message}");

                //ResponseModel<string> responseModel = new ResponseModel<string>
                //{
                //    StatusCode = 200,
                //    Sucess = true,
                //    Message="Hello to Greeting App from Post Method",
                //    Data = $"Key : {requestModel.Id}, Value: {requestModel.Message}"
                //};
                //return Ok(responseModel);

                logger.Info("POST request processed successfully.");
                var result = greetingBL.registerBL(requestGreetingModel);
                response = new ResponseModel<String>();
                response.StatusCode = 201;
                response.Sucess = true;
                response.Message = "Data Sucessfully Saved";
                response.Data = result.Message;
                return Created("user created", result);


            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while processing POST request.");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut]
        public IActionResult Put(RequestGreetingModel requestGreetingModel)
        {
            try
            {
                logger.Info($"PUT request received:Value={requestGreetingModel.Message}");

                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    StatusCode = 200,
                    Sucess = true,
                    Message = "Hello Greeting from Put Method",
                    Data = $"Updated Value: {requestGreetingModel.Message}"
                };

                logger.Info("PUT request processed successfully.");
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while processing PUT request.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch]
        public IActionResult Patch(RequestGreetingModel requestGreetingModel)
        {
            try
            {
                logger.Info($"PATCH request received:Value={requestGreetingModel.Message}");

                // Check if the request model is null
                if (requestGreetingModel == null)
                {
                    logger.Warn("PATCH request received with invalid data.");
                    return BadRequest("Invalid data.");
                }

                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    StatusCode = 200,
                    Sucess = true,
                    Message = "Hello Greeting from Patch Method",
                    Data = $"Patched Value: {requestGreetingModel.Message}"
                };

                logger.Info("PATCH request processed successfully.");
                return Ok(responseModel);


            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while processing PATCH request.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id,RequestGreetingModel requestGreetingModel)
        {
            try
            {
                logger.Info($"PATCH request received: Update Greeting ID={id} with new message={requestGreetingModel.Message}");

                var updatedGreeting = greetingBL.UpdateGreeting(id, requestGreetingModel.Message);

                if (updatedGreeting == null)
                {
                    response.Sucess = false;
                    response.Message = "Greeting not found!";
                    response.Data = null;
                    logger.Info("Greeting update failed: ID not found.");
                    return NotFound(response);
                }

                response.Sucess = true;
                response.Message = "Greeting updated successfully!";
                response.Data = updatedGreeting.Message;
                logger.Info("Greeting updated successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while updating the greeting.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                logger.Info($"DELETE request received: Key={id}");

                // Logic for deletion can be added here, such as checking if the item exists

                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    StatusCode = 200,
                    Sucess = true,
                    Message = "Hello Greeting from Delete Method",
                    Data = $"Deleted Item with Key: {id}"
                };

                logger.Info($"DELETE request processed successfully for Key={id}");
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while processing DELETE request.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

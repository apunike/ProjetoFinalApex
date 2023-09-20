using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Threading.Tasks;
using Utils.Api;
using Utils.Dtos.Auth;
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;

        public AuthController(IAuthService authservice)
        {
            _authservice = authservice;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var token = await _authservice.Login(loginRequestDto);

                if (token == string.Empty)
                {
                    return BadRequest(new ApiResponse("Dados de Longin Inválidos! Verifique e Tente novamente"));

                }
                return Ok(new ApiResponse<string>(token));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(ex.Message));
            }

        }


    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class LoginController : ControllerBase
	{
		private readonly ILoginRepository _loginRepository;

		public LoginController(ILoginRepository loginRepository)
		{
			_loginRepository = loginRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
		{
			try
			{
				var result = await _loginRepository.Login(loginDTO);
				if (result.Success)
				{
					return Ok(result);
				}
				return BadRequest(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Success = false, Message = ex.Message });
			}
		}

		[HttpGet("check-username")]
		[Authorize] // Thêm attribute này để yêu cầu xác thực
		public async Task<IActionResult> CheckUsername([FromQuery] string username)
		{
			try
			{
				var exists = await _loginRepository.CheckExistUsername(username);
				return Ok(new { Exists = exists });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Success = false, Message = ex.Message });
			}
		}

		[HttpPost("refresh-token")]
		public async Task<IActionResult> RenewToken(RefreshTokenDTO model)
		{
			try
			{
				var result = await _loginRepository.RenewToken(model);
				if (result == null)
					return BadRequest(new { Success = false, Message = "Token không hợp lệ" });

				return Ok(new { Success = true, Data = result });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Success = false, Message = "Lỗi khi làm mới token: " + ex.Message });
			}
		}
	}
}

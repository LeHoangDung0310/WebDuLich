using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KhachSanController : ControllerBase
    {
        private readonly IKhachSanRepository _khachSanRepository;

        public KhachSanController(IKhachSanRepository khachSanRepository)
        {
            _khachSanRepository = khachSanRepository;
        }

        // GET: api/KhachSan
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetKhachSans()
        {
            try
            {
                var khachSans = await _khachSanRepository.GetAllKhachSanAsync();
                return Ok(khachSans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách khách sạn", Error = ex.Message });
            }
        }

        // GET: api/KhachSan/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetKhachSan(string id)
        {
            try
            {
                var khachSan = await _khachSanRepository.GetKhachSanByIdAsync(id);
                if (khachSan == null)
                    return NotFound(new { Message = $"Không tìm thấy khách sạn với mã {id}" });

                return Ok(khachSan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin khách sạn", Error = ex.Message });
            }
        }

        // GET: api/KhachSan/Tinh/5
        [HttpGet("Tinh/{maTinh}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetKhachSanByTinh(int maTinh)
        {
            try
            {
                var khachSans = await _khachSanRepository.GetKhachSanByTinhAsync(maTinh);
                return Ok(khachSans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách khách sạn theo tỉnh", Error = ex.Message });
            }
        }

        // POST: api/KhachSan
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateKhachSan(KhachSanDTO khachSanDTO)
        {
            try
            {
                var newKhachSan = await _khachSanRepository.CreateKhachSanAsync(khachSanDTO);
                return CreatedAtAction(nameof(GetKhachSan), new { id = newKhachSan.MaKs }, newKhachSan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo khách sạn mới", Error = ex.Message });
            }
        }

        // PUT: api/KhachSan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKhachSan(string id, KhachSanUpdateDTO khachSanDTO)
        {
            try
            {
                // Kiểm tra quyền: Admin hoặc chính khách sạn đó
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var userMaKs = User.FindFirst("MaKs")?.Value;

                if (userRole != "1" && userMaKs != id)
                {
                    return Forbid();
                }

                var updatedKhachSan = await _khachSanRepository.UpdateKhachSanAsync(id, khachSanDTO);
                if (updatedKhachSan == null)
                    return NotFound(new { Message = $"Không tìm thấy khách sạn với mã {id}" });

                return Ok(updatedKhachSan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật khách sạn", Error = ex.Message });
            }
        }

        // DELETE: api/KhachSan/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteKhachSan(string id)
        {
            try
            {
                var result = await _khachSanRepository.DeleteKhachSanAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy khách sạn với mã {id}" });

                return Ok(new { Message = "Xóa khách sạn thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa khách sạn", Error = ex.Message });
            }
        }
    }
}
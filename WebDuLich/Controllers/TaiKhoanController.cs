using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]  // Chỉ Admin mới có quyền truy cập
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public TaiKhoanController(ITaiKhoanRepository taiKhoanRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
        }

        // GET: api/TaiKhoan
        [HttpGet]
        public async Task<IActionResult> GetTaiKhoans()
        {
            try
            {
                var taiKhoans = await _taiKhoanRepository.GetAllTaiKhoanAsync();
                return Ok(taiKhoans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách tài khoản", Error = ex.Message });
            }
        }

        // GET: api/TaiKhoan/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaiKhoan(string id)
        {
            try
            {
                var taiKhoan = await _taiKhoanRepository.GetTaiKhoanByIdAsync(id);
                if (taiKhoan == null)
                    return NotFound(new { Message = "Không tìm thấy tài khoản" });

                return Ok(taiKhoan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin tài khoản", Error = ex.Message });
            }
        }

        // POST: api/TaiKhoan
        [HttpPost]
        public async Task<IActionResult> CreateTaiKhoan(TaiKhoanDTO taiKhoanDTO)
        {
            try
            {
                var taiKhoan = await _taiKhoanRepository.CreateTaiKhoanAsync(taiKhoanDTO);
                return CreatedAtAction(nameof(GetTaiKhoan), new { id = taiKhoan.MaTK }, taiKhoan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo tài khoản", Error = ex.Message });
            }
        }

        // PUT: api/TaiKhoan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaiKhoan(string id, TaiKhoanUpdateDTO taiKhoanDTO)
        {
            try
            {
                var taiKhoan = await _taiKhoanRepository.UpdateTaiKhoanAsync(id, taiKhoanDTO);
                if (taiKhoan == null)
                    return NotFound(new { Message = "Không tìm thấy tài khoản" });

                return Ok(taiKhoan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật tài khoản", Error = ex.Message });
            }
        }

        // DELETE: api/TaiKhoan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoan(string id)
        {
            try
            {
                var result = await _taiKhoanRepository.DeleteTaiKhoanAsync(id);
                if (!result)
                    return NotFound(new { Message = "Không tìm thấy tài khoản" });

                return Ok(new { Message = "Xóa tài khoản thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa tài khoản", Error = ex.Message });
            }
        }
    }
}
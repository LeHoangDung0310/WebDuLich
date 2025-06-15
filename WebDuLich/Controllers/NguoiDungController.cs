using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NguoiDungController : ControllerBase
    {
        private readonly INguoiDungRepository _nguoiDungRepository;

        public NguoiDungController(INguoiDungRepository nguoiDungRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
        }

        // GET: api/NguoiDung
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetNguoiDungs()
        {
            try
            {
                var nguoiDungs = await _nguoiDungRepository.GetAllNguoiDungAsync();
                return Ok(nguoiDungs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách người dùng", Error = ex.Message });
            }
        }

        // GET: api/NguoiDung/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNguoiDung(string id)
        {
            try
            {
                var nguoiDung = await _nguoiDungRepository.GetNguoiDungByIdAsync(id);
                if (nguoiDung == null)
                    return NotFound(new { Message = $"Không tìm thấy người dùng với mã {id}" });

                // Kiểm tra quyền truy cập
                if (User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value != "1" && 
                    User.FindFirst("MaTK")?.Value != id)
                {
                    return Forbid();
                }

                return Ok(nguoiDung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin người dùng", Error = ex.Message });
            }
        }

        // POST: api/NguoiDung
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateNguoiDung(NguoiDungDTO nguoiDungDTO)
        {
            try
            {
                var newNguoiDung = await _nguoiDungRepository.CreateNguoiDungAsync(nguoiDungDTO);
                return CreatedAtAction(nameof(GetNguoiDung), new { id = newNguoiDung.MaTV }, newNguoiDung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo người dùng mới", Error = ex.Message });
            }
        }

        // PUT: api/NguoiDung/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNguoiDung(string id, NguoiDungUpdateDTO nguoiDungDTO)
        {
            try
            {
                // Kiểm tra quyền truy cập
                if (User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value != "1" && 
                    User.FindFirst("MaTK")?.Value != id)
                {
                    return Forbid();
                }

                var updatedNguoiDung = await _nguoiDungRepository.UpdateNguoiDungAsync(id, nguoiDungDTO);
                if (updatedNguoiDung == null)
                    return NotFound(new { Message = $"Không tìm thấy người dùng với mã {id}" });

                return Ok(updatedNguoiDung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật người dùng", Error = ex.Message });
            }
        }

        // DELETE: api/NguoiDung/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteNguoiDung(string id)
        {
            try
            {
                var result = await _nguoiDungRepository.DeleteNguoiDungAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy người dùng với mã {id}" });

                return Ok(new { Message = "Xóa người dùng thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa người dùng", Error = ex.Message });
            }
        }
    }
}
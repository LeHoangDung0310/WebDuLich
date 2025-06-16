using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NhaHangController : ControllerBase
    {
        private readonly INhaHangRepository _nhaHangRepository;

        public NhaHangController(INhaHangRepository nhaHangRepository)
        {
            _nhaHangRepository = nhaHangRepository;
        }

        // GET: api/NhaHang
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetNhaHangs()
        {
            try
            {
                var nhaHangs = await _nhaHangRepository.GetAllNhaHangAsync();
                return Ok(nhaHangs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách nhà hàng", Error = ex.Message });
            }
        }

        // GET: api/NhaHang/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNhaHang(string id)
        {
            try
            {
                var nhaHang = await _nhaHangRepository.GetNhaHangByIdAsync(id);
                if (nhaHang == null)
                    return NotFound(new { Message = $"Không tìm thấy nhà hàng với mã {id}" });

                return Ok(nhaHang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin nhà hàng", Error = ex.Message });
            }
        }

        // GET: api/NhaHang/Tinh/5
        [HttpGet("Tinh/{maTinh}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNhaHangByTinh(int maTinh)
        {
            try
            {
                var nhaHangs = await _nhaHangRepository.GetNhaHangByTinhAsync(maTinh);
                return Ok(nhaHangs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách nhà hàng theo tỉnh", Error = ex.Message });
            }
        }

        // POST: api/NhaHang
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateNhaHang(NhaHangDTO nhaHangDTO)
        {
            try
            {
                var newNhaHang = await _nhaHangRepository.CreateNhaHangAsync(nhaHangDTO);
                return CreatedAtAction(nameof(GetNhaHang), new { id = newNhaHang.MaNhaHang }, newNhaHang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo nhà hàng mới", Error = ex.Message });
            }
        }

        // PUT: api/NhaHang/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNhaHang(string id, NhaHangUpdateDTO nhaHangDTO)
        {
            try
            {
                // Kiểm tra quyền: Admin hoặc chính nhà hàng đó
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var userMaNhaHang = User.FindFirst("MaNhaHang")?.Value;

                if (userRole != "1" && userMaNhaHang != id)
                {
                    return Forbid();
                }

                var updatedNhaHang = await _nhaHangRepository.UpdateNhaHangAsync(id, nhaHangDTO);
                if (updatedNhaHang == null)
                    return NotFound(new { Message = $"Không tìm thấy nhà hàng với mã {id}" });

                return Ok(updatedNhaHang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật nhà hàng", Error = ex.Message });
            }
        }

        // DELETE: api/NhaHang/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteNhaHang(string id)
        {
            try
            {
                var result = await _nhaHangRepository.DeleteNhaHangAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy nhà hàng với mã {id}" });

                return Ok(new { Message = "Xóa nhà hàng thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa nhà hàng", Error = ex.Message });
            }
        }
    }
}
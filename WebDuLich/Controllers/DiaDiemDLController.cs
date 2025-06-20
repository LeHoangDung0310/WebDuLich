using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiaDiemDLController : ControllerBase
    {
        private readonly IDiaDiemDLRepository _diaDiemDLRepository;

        public DiaDiemDLController(IDiaDiemDLRepository diaDiemDLRepository)
        {
            _diaDiemDLRepository = diaDiemDLRepository;
        }

        // GET: api/DiaDiemDL
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDiaDiemDLs()
        {
            try
            {
                var diaDiemDLs = await _diaDiemDLRepository.GetAllDiaDiemDLAsync();
                return Ok(diaDiemDLs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách địa điểm du lịch", Error = ex.Message });
            }
        }

        // GET: api/DiaDiemDL/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDiaDiemDL(string id)
        {
            try
            {
                var diaDiemDL = await _diaDiemDLRepository.GetDiaDiemDLByIdAsync(id);
                if (diaDiemDL == null)
                    return NotFound(new { Message = $"Không tìm thấy địa điểm du lịch với mã {id}" });

                return Ok(diaDiemDL);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin địa điểm du lịch", Error = ex.Message });
            }
        }

        // GET: api/DiaDiemDL/Tinh/5
        [HttpGet("Tinh/{maTinh}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDiaDiemDLByTinh(int maTinh)
        {
            try
            {
                var diaDiemDLs = await _diaDiemDLRepository.GetDiaDiemDLByTinhAsync(maTinh);
                return Ok(diaDiemDLs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách địa điểm du lịch theo tỉnh", Error = ex.Message });
            }
        }

        // POST: api/DiaDiemDL
        [HttpPost]
        [Authorize(Roles = "1")] // Chỉ Admin mới được tạo
        public async Task<IActionResult> CreateDiaDiemDL(DiaDiemDLCreateDTO diaDiemDLDTO)
        {
            try
            {
                var userMaTK = User.FindFirst("MaTK")?.Value;
                if (string.IsNullOrEmpty(userMaTK))
                {
                    return BadRequest(new { Message = "Không tìm thấy thông tin tài khoản" });
                }

                var newDiaDiemDL = await _diaDiemDLRepository.CreateDiaDiemDLAsync(userMaTK, diaDiemDLDTO);
                return CreatedAtAction(nameof(GetDiaDiemDL), new { id = newDiaDiemDL.MaDiaDiem }, newDiaDiemDL);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo địa điểm du lịch mới", Error = ex.Message });
            }
        }

        // PUT: api/DiaDiemDL/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")] // Chỉ Admin mới được cập nhật
        public async Task<IActionResult> UpdateDiaDiemDL(string id, DiaDiemDLUpdateDTO diaDiemDLDTO)
        {
            try
            {
                var diaDiemDL = await _diaDiemDLRepository.GetDiaDiemDLByIdAsync(id);
                if (diaDiemDL == null)
                    return NotFound(new { Message = $"Không tìm thấy địa điểm du lịch với mã {id}" });

                var updatedDiaDiemDL = await _diaDiemDLRepository.UpdateDiaDiemDLAsync(id, diaDiemDLDTO);
                return Ok(updatedDiaDiemDL);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật địa điểm du lịch", Error = ex.Message });
            }
        }

        // DELETE: api/DiaDiemDL/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")] // Chỉ Admin mới được xóa
        public async Task<IActionResult> DeleteDiaDiemDL(string id)
        {
            try
            {
                var diaDiemDL = await _diaDiemDLRepository.GetDiaDiemDLByIdAsync(id);
                if (diaDiemDL == null)
                    return NotFound(new { Message = $"Không tìm thấy địa điểm du lịch với mã {id}" });

                var result = await _diaDiemDLRepository.DeleteDiaDiemDLAsync(id);
                return Ok(new { Message = "Xóa địa điểm du lịch thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa địa điểm du lịch", Error = ex.Message });
            }
        }
    }
}
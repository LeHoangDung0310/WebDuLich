using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TinhController : ControllerBase
    {
        private readonly ITinhRepository _tinhRepository;

        public TinhController(ITinhRepository tinhRepository)
        {
            _tinhRepository = tinhRepository;
        }

        // GET: api/Tinh
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTinhs()
        {
            try
            {
                var tinhs = await _tinhRepository.GetAllTinhAsync();
                return Ok(tinhs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách tỉnh", Error = ex.Message });
            }
        }

        // GET: api/Tinh/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTinh(int id)
        {
            try
            {
                var tinh = await _tinhRepository.GetTinhByIdAsync(id);
                if (tinh == null)
                    return NotFound(new { Message = $"Không tìm thấy tỉnh với mã {id}" });

                return Ok(tinh);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin tỉnh", Error = ex.Message });
            }
        }

        // GET: api/Tinh/Mien/5
        [HttpGet("Mien/{maMien}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTinhByMien(int maMien)
        {
            try
            {
                var tinhs = await _tinhRepository.GetTinhByMienAsync(maMien);
                return Ok(tinhs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách tỉnh theo miền", Error = ex.Message });
            }
        }

        // POST: api/Tinh
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateTinh(TinhDTO tinhDTO)
        {
            try
            {
                var newTinh = await _tinhRepository.CreateTinhAsync(tinhDTO);
                return CreatedAtAction(nameof(GetTinh), new { id = newTinh.MaTinh }, newTinh);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo tỉnh mới", Error = ex.Message });
            }
        }

        // PUT: api/Tinh/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateTinh(int id, TinhUpdateDTO tinhDTO)
        {
            try
            {
                var updatedTinh = await _tinhRepository.UpdateTinhAsync(id, tinhDTO);
                if (updatedTinh == null)
                    return NotFound(new { Message = $"Không tìm thấy tỉnh với mã {id}" });

                return Ok(updatedTinh);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật tỉnh", Error = ex.Message });
            }
        }

        // DELETE: api/Tinh/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteTinh(int id)
        {
            try
            {
                var result = await _tinhRepository.DeleteTinhAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy tỉnh với mã {id}" });

                return Ok(new { Message = "Xóa tỉnh thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa tỉnh", Error = ex.Message });
            }
        }
    }
}
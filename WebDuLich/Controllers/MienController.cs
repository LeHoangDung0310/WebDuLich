using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MienController : ControllerBase
    {
        private readonly IMienRepository _mienRepository;

        public MienController(IMienRepository mienRepository)
        {
            _mienRepository = mienRepository;
        }

        // GET: api/Mien
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMiens()
        {
            try
            {
                var miens = await _mienRepository.GetAllMienAsync();
                return Ok(miens);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách miền", Error = ex.Message });
            }
        }

        // GET: api/Mien/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMien(int id)
        {
            try
            {
                var mien = await _mienRepository.GetMienByIdAsync(id);
                if (mien == null)
                    return NotFound(new { Message = $"Không tìm thấy miền với mã {id}" });

                return Ok(mien);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin miền", Error = ex.Message });
            }
        }

        // POST: api/Mien
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateMien(MienDTO mienDTO)
        {
            try
            {
                var newMien = await _mienRepository.CreateMienAsync(mienDTO);
                return CreatedAtAction(nameof(GetMien), new { id = newMien.MaMien }, newMien);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo miền mới", Error = ex.Message });
            }
        }

        // PUT: api/Mien/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateMien(int id, MienUpdateDTO mienDTO)
        {
            try
            {
                var updatedMien = await _mienRepository.UpdateMienAsync(id, mienDTO);
                if (updatedMien == null)
                    return NotFound(new { Message = $"Không tìm thấy miền với mã {id}" });

                return Ok(updatedMien);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật miền", Error = ex.Message });
            }
        }

        // DELETE: api/Mien/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteMien(int id)
        {
            try
            {
                var result = await _mienRepository.DeleteMienAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy miền với mã {id}" });

                return Ok(new { Message = "Xóa miền thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa miền", Error = ex.Message });
            }
        }
    }
}
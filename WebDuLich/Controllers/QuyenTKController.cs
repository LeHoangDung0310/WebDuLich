using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuyenTKController : ControllerBase
    {
        private readonly IQuyenTKRepository _quyenTKRepository;

        public QuyenTKController(IQuyenTKRepository quyenTKRepository)
        {
            _quyenTKRepository = quyenTKRepository;
        }

        // GET: api/QuyenTK
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetQuyenTKs()
        {
            try
            {
                var quyenTKs = await _quyenTKRepository.GetAllQuyenTKAsync();
                return Ok(quyenTKs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách quyền tài khoản", Error = ex.Message });
            }
        }

        // GET: api/QuyenTK/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetQuyenTK(string id)
        {
            try
            {
                var quyenTK = await _quyenTKRepository.GetQuyenTKByIdAsync(id);
                if (quyenTK == null)
                    return NotFound(new { Message = $"Không tìm thấy quyền tài khoản với mã {id}" });

                return Ok(quyenTK);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin quyền tài khoản", Error = ex.Message });
            }
        }

        // POST: api/QuyenTK
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateQuyenTK(QuyenTKDTO quyenTKDTO)
        {
            try
            {
                var newQuyenTK = await _quyenTKRepository.CreateQuyenTKAsync(quyenTKDTO);
                return CreatedAtAction(nameof(GetQuyenTK), new { id = newQuyenTK.MaQuyen }, newQuyenTK);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo quyền tài khoản mới", Error = ex.Message });
            }
        }

        // PUT: api/QuyenTK/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateQuyenTK(string id, QuyenTKUpdateDTO quyenTKDTO)
        {
            try
            {
                var updatedQuyenTK = await _quyenTKRepository.UpdateQuyenTKAsync(id, quyenTKDTO);
                if (updatedQuyenTK == null)
                    return NotFound(new { Message = $"Không tìm thấy quyền tài khoản với mã {id}" });

                return Ok(updatedQuyenTK);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật quyền tài khoản", Error = ex.Message });
            }
        }

        // DELETE: api/QuyenTK/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteQuyenTK(string id)
        {
            try
            {
                var result = await _quyenTKRepository.DeleteQuyenTKAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy quyền tài khoản với mã {id}" });

                return Ok(new { Message = "Xóa quyền tài khoản thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa quyền tài khoản", Error = ex.Message });
            }
        }
    }
}
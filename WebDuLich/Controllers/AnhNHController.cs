using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnhNHController : ControllerBase
    {
        private readonly IAnhNHRepository _anhNHRepository;

        public AnhNHController(IAnhNHRepository anhNHRepository)
        {
            _anhNHRepository = anhNHRepository;
        }

        // GET: api/AnhNH
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhNHs()
        {
            try
            {
                var anhNHs = await _anhNHRepository.GetAllAnhNHAsync();
                return Ok(anhNHs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách ảnh nhà hàng", Error = ex.Message });
            }
        }

        // GET: api/AnhNH/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhNH(int id)
        {
            try
            {
                var anhNH = await _anhNHRepository.GetAnhNHByIdAsync(id);
                if (anhNH == null)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                return Ok(anhNH);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin ảnh nhà hàng", Error = ex.Message });
            }
        }

        // GET: api/AnhNH/NhaHang/NH001
        [HttpGet("NhaHang/{maNH}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhNHByNhaHang(string maNH)
        {
            try
            {
                var anhNHs = await _anhNHRepository.GetAnhNHByNhaHangAsync(maNH);
                return Ok(anhNHs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách ảnh theo nhà hàng", Error = ex.Message });
            }
        }

        // POST: api/AnhNH
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateAnhNH(AnhNHDTO anhNHDTO)
        {
            try
            {
                var newAnhNH = await _anhNHRepository.CreateAnhNHAsync(anhNHDTO);
                return CreatedAtAction(nameof(GetAnhNH), new { id = newAnhNH.MaAnh }, newAnhNH);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi thêm ảnh nhà hàng mới", Error = ex.Message });
            }
        }

        // PUT: api/AnhNH/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateAnhNH(int id, AnhNHUpdateDTO anhNHDTO)
        {
            try
            {
                var updatedAnhNH = await _anhNHRepository.UpdateAnhNHAsync(id, anhNHDTO);
                if (updatedAnhNH == null)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                return Ok(updatedAnhNH);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật ảnh nhà hàng", Error = ex.Message });
            }
        }

        // DELETE: api/AnhNH/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteAnhNH(int id)
        {
            try
            {
                var result = await _anhNHRepository.DeleteAnhNHAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                return Ok(new { Message = "Xóa ảnh nhà hàng thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa ảnh nhà hàng", Error = ex.Message });
            }
        }
    }
}
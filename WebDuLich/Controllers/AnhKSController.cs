using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnhKSController : ControllerBase
    {
        private readonly IAnhKSRepository _anhKSRepository;

        public AnhKSController(IAnhKSRepository anhKSRepository)
        {
            _anhKSRepository = anhKSRepository;
        }

        // GET: api/AnhKS
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhKSs()
        {
            try
            {
                var anhKSs = await _anhKSRepository.GetAllAnhKSAsync();
                return Ok(anhKSs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách ảnh khách sạn", Error = ex.Message });
            }
        }

        // GET: api/AnhKS/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhKS(int id)
        {
            try
            {
                var anhKS = await _anhKSRepository.GetAnhKSByIdAsync(id);
                if (anhKS == null)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                return Ok(anhKS);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin ảnh khách sạn", Error = ex.Message });
            }
        }

        // GET: api/AnhKS/KhachSan/KS001
        [HttpGet("KhachSan/{maKS}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhKSByKhachSan(string maKS)
        {
            try
            {
                var anhKSs = await _anhKSRepository.GetAnhKSByKhachSanAsync(maKS);
                return Ok(anhKSs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách ảnh theo khách sạn", Error = ex.Message });
            }
        }

        // POST: api/AnhKS
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateAnhKS(AnhKSDTO anhKSDTO)
        {
            try
            {
                var newAnhKS = await _anhKSRepository.CreateAnhKSAsync(anhKSDTO);
                return CreatedAtAction(nameof(GetAnhKS), new { id = newAnhKS.MaAnh }, newAnhKS);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi thêm ảnh khách sạn mới", Error = ex.Message });
            }
        }

        // PUT: api/AnhKS/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateAnhKS(int id, AnhKSUpdateDTO anhKSDTO)
        {
            try
            {
                var updatedAnhKS = await _anhKSRepository.UpdateAnhKSAsync(id, anhKSDTO);
                if (updatedAnhKS == null)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                return Ok(updatedAnhKS);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật ảnh khách sạn", Error = ex.Message });
            }
        }

        // DELETE: api/AnhKS/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteAnhKS(int id)
        {
            try
            {
                var result = await _anhKSRepository.DeleteAnhKSAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                return Ok(new { Message = "Xóa ảnh khách sạn thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa ảnh khách sạn", Error = ex.Message });
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BanNHController : ControllerBase
    {
        private readonly IBanNHRepository _banNHRepository;

        public BanNHController(IBanNHRepository banNHRepository)
        {
            _banNHRepository = banNHRepository;
        }

        // GET: api/BanNH
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBanNHs()
        {
            try
            {
                var banNHs = await _banNHRepository.GetAllBanNHAsync();
                return Ok(banNHs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách bàn nhà hàng", Error = ex.Message });
            }
        }

        // GET: api/BanNH/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBanNH(string id)
        {
            try
            {
                var banNH = await _banNHRepository.GetBanNHByIdAsync(id);
                if (banNH == null)
                    return NotFound(new { Message = $"Không tìm thấy bàn với mã {id}" });

                return Ok(banNH);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin bàn", Error = ex.Message });
            }
        }

        // GET: api/BanNH/NhaHang/NH001
        [HttpGet("NhaHang/{maNhaHang}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBanNHByNhaHang(string maNhaHang)
        {
            try
            {
                var banNHs = await _banNHRepository.GetBanNHByNhaHangAsync(maNhaHang);
                return Ok(banNHs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách bàn theo nhà hàng", Error = ex.Message });
            }
        }

        // POST: api/BanNH
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateBanNH(BanNHCreateDTO banNHDTO)
        {
            try
            {
                var newBanNH = await _banNHRepository.CreateBanNHAsync(banNHDTO);
                return CreatedAtAction(nameof(GetBanNH), new { id = newBanNH.MaBan }, newBanNH);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo bàn mới", Error = ex.Message });
            }
        }

        // PUT: api/BanNH/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateBanNH(string id, BanNHUpdateDTO banNHDTO)
        {
            try
            {
                var updatedBanNH = await _banNHRepository.UpdateBanNHAsync(id, banNHDTO);
                if (updatedBanNH == null)
                    return NotFound(new { Message = $"Không tìm thấy bàn với mã {id}" });

                return Ok(updatedBanNH);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật bàn", Error = ex.Message });
            }
        }

        // DELETE: api/BanNH/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteBanNH(string id)
        {
            try
            {
                var result = await _banNHRepository.DeleteBanNHAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy bàn với mã {id}" });

                return Ok(new { Message = "Xóa bàn thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa bàn", Error = ex.Message });
            }
        }
    }
}
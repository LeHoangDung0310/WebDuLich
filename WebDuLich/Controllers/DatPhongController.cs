using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DatPhongController : ControllerBase
    {
        private readonly IDatPhongRepository _datPhongRepository;

        public DatPhongController(IDatPhongRepository datPhongRepository)
        {
            _datPhongRepository = datPhongRepository;
        }

        // GET: api/DatPhong
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetDatPhongs()
        {
            try
            {
                var datPhongs = await _datPhongRepository.GetAllDatPhongAsync();
                return Ok(datPhongs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách đặt phòng", Error = ex.Message });
            }
        }

        // GET: api/DatPhong/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDatPhong(int id)
        {
            try
            {
                var datPhong = await _datPhongRepository.GetDatPhongByIdAsync(id);
                if (datPhong == null)
                    return NotFound(new { Message = $"Không tìm thấy đặt phòng với mã {id}" });

                // Kiểm tra quyền: Admin hoặc chính người đặt
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var userMaND = User.FindFirst("MaND")?.Value;

                if (userRole != "1" && userMaND != datPhong.MaNguoiDung)
                {
                    return Forbid();
                }

                return Ok(datPhong);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin đặt phòng", Error = ex.Message });
            }
        }

        // GET: api/DatPhong/NguoiDung/{maNguoiDung}
        [HttpGet("NguoiDung/{maNguoiDung}")]
        public async Task<IActionResult> GetDatPhongByNguoiDung(string maNguoiDung)
        {
            try
            {
                // Kiểm tra quyền: Admin hoặc chính người đặt
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var userMaND = User.FindFirst("MaND")?.Value;

                if (userRole != "1" && userMaND != maNguoiDung)
                {
                    return Forbid();
                }

                var datPhongs = await _datPhongRepository.GetDatPhongByNguoiDungAsync(maNguoiDung);
                return Ok(datPhongs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách đặt phòng theo người dùng", Error = ex.Message });
            }
        }

        // POST: api/DatPhong
        [HttpPost]
        public async Task<IActionResult> CreateDatPhong(DatPhongCreateDTO datPhongDTO)
        {
            try
            {
                var userMaND = User.FindFirst("MaND")?.Value;
                if (string.IsNullOrEmpty(userMaND))
                {
                    return BadRequest(new { Message = "Không tìm thấy thông tin người dùng" });
                }

                // Kiểm tra tính khả dụng của phòng
                if (!await _datPhongRepository.IsPhongAvailableAsync(
                    datPhongDTO.MaPhong, 
                    datPhongDTO.NgayNhanPhong, 
                    datPhongDTO.NgayTraPhong))
                {
                    return BadRequest(new { Message = "Phòng đã được đặt trong khoảng thời gian này" });
                }

                var newDatPhong = await _datPhongRepository.CreateDatPhongAsync(userMaND, datPhongDTO);
                return CreatedAtAction(nameof(GetDatPhong), new { id = newDatPhong.MaDatPhong }, newDatPhong);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo đặt phòng mới", Error = ex.Message });
            }
        }

        // PUT: api/DatPhong/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDatPhong(int id, DatPhongUpdateDTO datPhongDTO)
        {
            try
            {
                var datPhong = await _datPhongRepository.GetDatPhongByIdAsync(id);
                if (datPhong == null)
                    return NotFound(new { Message = $"Không tìm thấy đặt phòng với mã {id}" });

                // Kiểm tra quyền: Admin hoặc chính người đặt
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var userMaND = User.FindFirst("MaND")?.Value;

                if (userRole != "1" && userMaND != datPhong.MaNguoiDung)
                {
                    return Forbid();
                }

                var updatedDatPhong = await _datPhongRepository.UpdateDatPhongAsync(id, datPhongDTO);
                return Ok(updatedDatPhong);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật đặt phòng", Error = ex.Message });
            }
        }

        // DELETE: api/DatPhong/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteDatPhong(int id)
        {
            try
            {
                var result = await _datPhongRepository.DeleteDatPhongAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy đặt phòng với mã {id}" });

                return Ok(new { Message = "Xóa đặt phòng thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa đặt phòng", Error = ex.Message });
            }
        }
    }
}
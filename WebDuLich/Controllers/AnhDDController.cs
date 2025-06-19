using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnhDDController : ControllerBase
    {
        private readonly IAnhDDRepository _anhDDRepository;
        private readonly IDiaDiemDLRepository _diaDiemDLRepository;

        public AnhDDController(IAnhDDRepository anhDDRepository, IDiaDiemDLRepository diaDiemDLRepository)
        {
            _anhDDRepository = anhDDRepository;
            _diaDiemDLRepository = diaDiemDLRepository;
        }

        // GET: api/AnhDD
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhDDs()
        {
            try
            {
                var anhDDs = await _anhDDRepository.GetAllAnhDDAsync();
                return Ok(anhDDs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách ảnh địa điểm", Error = ex.Message });
            }
        }

        // GET: api/AnhDD/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhDD(int id)
        {
            try
            {
                var anhDD = await _anhDDRepository.GetAnhDDByIdAsync(id);
                if (anhDD == null)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                return Ok(anhDD);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin ảnh địa điểm", Error = ex.Message });
            }
        }

        // GET: api/AnhDD/DiaDiem/DD001
        [HttpGet("DiaDiem/{maDiaDiem}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnhDDByDiaDiem(string maDiaDiem)
        {
            try
            {
                var anhDDs = await _anhDDRepository.GetAnhDDByDiaDiemAsync(maDiaDiem);
                return Ok(anhDDs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách ảnh theo địa điểm", Error = ex.Message });
            }
        }

        // POST: api/AnhDD
        [HttpPost]
        [Authorize(Roles = "1")] // Chỉ Admin mới được tạo
        public async Task<IActionResult> CreateAnhDD(AnhDDDTO anhDDDTO)
        {
            try
            {
                var diaDiem = await _diaDiemDLRepository.GetDiaDiemDLByIdAsync(anhDDDTO.MaDiaDiem);
                if (diaDiem == null)
                    return NotFound(new { Message = $"Không tìm thấy địa điểm với mã {anhDDDTO.MaDiaDiem}" });

                var newAnhDD = await _anhDDRepository.CreateAnhDDAsync(anhDDDTO);
                return CreatedAtAction(nameof(GetAnhDD), new { id = newAnhDD.MaAnh }, newAnhDD);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi thêm ảnh địa điểm mới", Error = ex.Message });
            }
        }

        // PUT: api/AnhDD/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")] // Chỉ Admin mới được cập nhật
        public async Task<IActionResult> UpdateAnhDD(int id, AnhDDUpdateDTO anhDDDTO)
        {
            try
            {
                var anhDD = await _anhDDRepository.GetAnhDDByIdAsync(id);
                if (anhDD == null)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                var updatedAnhDD = await _anhDDRepository.UpdateAnhDDAsync(id, anhDDDTO);
                return Ok(updatedAnhDD);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật ảnh địa điểm", Error = ex.Message });
            }
        }

        // DELETE: api/AnhDD/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")] // Chỉ Admin mới được xóa
        public async Task<IActionResult> DeleteAnhDD(int id)
        {
            try
            {
                var anhDD = await _anhDDRepository.GetAnhDDByIdAsync(id);
                if (anhDD == null)
                    return NotFound(new { Message = $"Không tìm thấy ảnh với mã {id}" });

                var result = await _anhDDRepository.DeleteAnhDDAsync(id);
                return Ok(new { Message = "Xóa ảnh địa điểm thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa ảnh địa điểm", Error = ex.Message });
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhongKSController : ControllerBase
    {
        private readonly IPhongKSRepository _phongKSRepository;

        public PhongKSController(IPhongKSRepository phongKSRepository)
        {
            _phongKSRepository = phongKSRepository;
        }

        // GET: api/PhongKS
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhongKSs()
        {
            try
            {
                var phongKSs = await _phongKSRepository.GetAllPhongKSAsync();
                return Ok(phongKSs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách phòng", Error = ex.Message });
            }
        }

        // GET: api/PhongKS/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhongKS(int id)
        {
            try
            {
                var phongKS = await _phongKSRepository.GetPhongKSByIdAsync(id);
                if (phongKS == null)
                    return NotFound(new { Message = $"Không tìm thấy phòng với mã {id}" });

                return Ok(phongKS);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy thông tin phòng", Error = ex.Message });
            }
        }

        // GET: api/PhongKS/KhachSan/KS001
        [HttpGet("KhachSan/{maKS}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhongKSByKhachSan(string maKS)
        {
            try
            {
                var phongKSs = await _phongKSRepository.GetPhongKSByKhachSanAsync(maKS);
                return Ok(phongKSs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách phòng theo khách sạn", Error = ex.Message });
            }
        }

        // POST: api/PhongKS
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreatePhongKS(PhongKSDTO phongKSDTO)
        {
            try
            {
                var newPhongKS = await _phongKSRepository.CreatePhongKSAsync(phongKSDTO);
                return CreatedAtAction(nameof(GetPhongKS), new { id = newPhongKS.MaPhong }, newPhongKS);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tạo phòng mới", Error = ex.Message });
            }
        }

        // PUT: api/PhongKS/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdatePhongKS(int id, PhongKSUpdateDTO phongKSDTO)
        {
            try
            {
                var updatedPhongKS = await _phongKSRepository.UpdatePhongKSAsync(id, phongKSDTO);
                if (updatedPhongKS == null)
                    return NotFound(new { Message = $"Không tìm thấy phòng với mã {id}" });

                return Ok(updatedPhongKS);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật phòng", Error = ex.Message });
            }
        }

        // DELETE: api/PhongKS/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeletePhongKS(int id)
        {
            try
            {
                var result = await _phongKSRepository.DeletePhongKSAsync(id);
                if (!result)
                    return NotFound(new { Message = $"Không tìm thấy phòng với mã {id}" });

                return Ok(new { Message = "Xóa phòng thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa phòng", Error = ex.Message });
            }
        }
    }
}
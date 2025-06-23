using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioiThieuController : ControllerBase
    {
        private readonly IGioiThieuRepository _repo;

        public GioiThieuController(IGioiThieuRepository repo)
        {
            _repo = repo;
        }

        // GET: api/GioiThieu
        [HttpGet]
        [AllowAnonymous]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repo.GetAllAsync();
            return Ok(list);
        }

        // GET: api/GioiThieu/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/GioiThieu
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Create(GioiThieuCreateDTO dto)
        {
            var idNguoiCapNhat = User.FindFirst("MaTK")?.Value;
            if (string.IsNullOrEmpty(idNguoiCapNhat))
                return BadRequest(new { Message = "Không tìm thấy thông tin tài khoản" });

            var result = await _repo.CreateAsync(idNguoiCapNhat, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdGioiThieu }, result);
        }

        // PUT: api/GioiThieu/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Update(int id, GioiThieuUpdateDTO dto)
        {
            var idNguoiCapNhat = User.FindFirst("MaTK")?.Value;
            if (string.IsNullOrEmpty(idNguoiCapNhat))
                return BadRequest(new { Message = "Không tìm thấy thông tin tài khoản" });

            var result = await _repo.UpdateAsync(id, idNguoiCapNhat, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // DELETE: api/GioiThieu/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _repo.DeleteAsync(id);
            if (!ok) return NotFound();
            return Ok(new { Message = "Xóa thành công" });
        }
    }
}
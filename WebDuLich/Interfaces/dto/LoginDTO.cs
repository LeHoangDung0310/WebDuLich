namespace WebDuLich.Interfaces.dto
{
    public class LoginDTO
    {
        public string TenDangNhap { get; set; }  // Đổi username thành TenDangNhap
        public string MatKhau { get; set; }      // Đổi password thành MatKhau
    }

    public class LoginResponse
    {
        public string MaTK { get; set; }
        public string TenDangNhap { get; set; }
        public string MaQuyen { get; set; }
        public string TenQuyen { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; internal set; }
    }
}

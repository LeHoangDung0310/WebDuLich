using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
	public interface ILoginRepository
	{
		Task<LoginResponse> Login(LoginDTO loginDTO);
		Task<bool> CheckExistUsername(string username);
		Task<TokenModel> RenewToken(RefreshTokenDTO model);
	}
}

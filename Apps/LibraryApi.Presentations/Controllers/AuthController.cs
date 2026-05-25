using LibraryApi.Applications.Dtos;
using LibraryApi.Applications.UseCases;
using LibraryApi.Domains.Adapters;
using LibraryApi.Presentations.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace LibraryApi.Presentations.Controllers;
/// <summary>
/// 認証(ログイン・ログアウト)に関する API を提供するコントローラー
/// </summary>
[ApiController]
[Route("library/api/auth")]
[Tags("認証")]
public class AuthController : ControllerBase
{
    /// <summary>認証トークンを格納する Cookie のキー名</summary>
    private const string AuthCookieName = "access_token";

    private readonly ILoginUseCase _loginUseCase;
    private readonly IAdapter<LoginDto, LoginRequest> _loginRequestAdapter;

    public AuthController(
        ILoginUseCase loginUseCase,
        IAdapter<LoginDto, LoginRequest> loginRequestAdapter)
    {
        _loginUseCase = loginUseCase;
        _loginRequestAdapter = loginRequestAdapter;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var input = _loginRequestAdapter.Restore(request);

        var result = await _loginUseCase.ExecuteAsync(input);

        // 発行された JWT を HttpOnly Cookie にセットする
        Response.Cookies.Append(AuthCookieName, result.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,                  // ★開発はHTTPのため false
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(60),
        });

        return Ok(new LoginResponse { Message = "ログインに成功しました。" });
    }
}
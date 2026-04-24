using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Auth.Commands;

public sealed class AdminLoginHandler(
    IAdminUserRepository adminUserRepository,
    IJwtTokenService jwtTokenService)
    : IRequestHandler<AdminLoginCommand, AuthTokenDto?>
{
    public async Task<AuthTokenDto?> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await adminUserRepository.GetByEmailAsync(request.Email.Trim(), cancellationToken);
        if (user is null)
        {
            return null;
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return null;
        }

        var token = jwtTokenService.GenerateToken(user);

        return new AuthTokenDto
        {
            Token = token,
            FullName = user.FullName,
            Role = user.Role,
            ExpiresAt = DateTime.UtcNow.AddMinutes(480)
        };
    }
}

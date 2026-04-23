using MediatR;
using AwladRizk.Application.DTOs;

namespace AwladRizk.Application.Features.Auth.Commands;

public record AdminLoginCommand(string Email, string Password) : IRequest<AuthTokenDto?>;

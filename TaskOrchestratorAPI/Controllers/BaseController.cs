using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskOrchestratorAPI.Controllers;

public class BaseController : Controller
{
    protected string? CurrentUserId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier) ??
        User.FindFirstValue("sub");
}

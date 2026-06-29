using Microsoft.AspNetCore.Mvc;
using TaskOrchestratorAPI.Model;
using TaskOrchestratorAPI.Services;

namespace TaskOrchestratorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : BaseController
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_taskService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var item = _taskService.GetById(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(TaskItem item)
    {
        var created = _taskService.Add(item);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }
}
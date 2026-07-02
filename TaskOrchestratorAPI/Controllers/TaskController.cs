using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskOrchestratorAPI.Model;
using TaskOrchestratorAPI.Services;

namespace TaskOrchestratorAPI.Controllers;

[ApiController]
[Authorize]
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
        var userId = CurrentUserId;

        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest("UserId is required.");

        return Ok(_taskService.GetAllByUserId(userId));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var userId = CurrentUserId;

        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest("UserId is required.");

        var item = _taskService.GetById(id, userId);

        if (item == null)
            return NotFound();

        return Ok(item);
    }
    
    [HttpPut("{id}")]
    public IActionResult Edit(int id, TaskItem item)
    {
        var userId = CurrentUserId;

        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest("UserId is required.");

        var updated = _taskService.Update(id, userId, item);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpPost]
    public IActionResult Create(TaskItem item)
    {
        var userId = CurrentUserId;

        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest("UserId is required.");

        item.UserId = userId;

        var created = _taskService.Add(item);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var userId = CurrentUserId;

        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest("UserId is required.");

        var deleted = _taskService.Delete(id, userId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

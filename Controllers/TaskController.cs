using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoListProjectJWT.Context;
using ToDoListProjectJWT.Models;

namespace ToDoListProjectJWT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly OrganizerContext _context;

    public TaskController(OrganizerContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var tarefa = _context.Tasks.Find(id);
        if (tarefa == null)
            return NotFound();
        return Ok(tarefa);
    }

    [HttpGet("ObterTodos")]
    public IActionResult ObterTodos()
    {
        var tarefas = _context.Tasks.ToList();
        return Ok(tarefas);
    }

    [HttpGet("ObterPorTitulo")]
    public IActionResult ObterPorTitulo(string titulo)
    {
        var tarefas = _context.Tasks.Where(x => x.Title.Contains(titulo)).ToList();
        return Ok(tarefas);
    }

    [HttpGet("ObterPorStatus")]
    public IActionResult ObterPorStatus(EnumStatusTask status)
    {
        var tarefas = _context.Tasks.Where(x => x.Status == status).ToList();
        return Ok(tarefas);
    }

    [HttpPost]
    public IActionResult Criar(ToDoTask tarefa)
    {
        _context.Tasks.Add(tarefa);
        _context.SaveChanges();

        return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, ToDoTask tarefa)
    {
        var tarefaBanco = _context.Tasks.Find(id);

        if (tarefaBanco == null)
            return NotFound();

        tarefaBanco.Title = tarefa.Title;
        tarefaBanco.Description = tarefa.Description;
        tarefaBanco.Status = tarefa.Status;

        _context.Tasks.Update(tarefaBanco);
        _context.SaveChanges();

        return Ok(tarefaBanco);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var tarefaBanco = _context.Tasks.Find(id);

        if (tarefaBanco == null)
            return NotFound();

        _context.Tasks.Remove(tarefaBanco);
        _context.SaveChanges();

        return NoContent();
    }
}

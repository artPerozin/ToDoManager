using ASPnetAPIrest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPnetAPIrest.Controllers;

public class TodoController : Controller
{
    private readonly TodosContext _context;

    public TodoController(TodosContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Lista de Tarefas";
        var todos = _context.Todos.ToList();
        return View(todos);
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "Nova Tarefa";
        return View("Form");
    }

    [HttpPost]
    public IActionResult Create(Todo todo)
    {
        if (ModelState.IsValid)
        {
            todo.CreatedAt = DateTime.Now;
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Title"] = "Nova Tarefa";
        return View("Form", todo);
    }

    public IActionResult Edit(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            return NotFound();
        }
        ViewData["Title"] = "Editar Tarefa";
        return View("Form", todo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Todo todo)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Editar Tarefa";
            return View("Form", todo);
        }
        var todoDb = _context.Todos.FirstOrDefault(t => t.Id == todo.Id);
        
        if (todoDb == null)
            return NotFound();
        
        todoDb.Title = todo.Title;
        todoDb.Deadline = todo.Deadline;
        
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            return NotFound();
        }
        ViewData["Title"] = "Excluir Tarefa";
        return View(todo);
    }

    [HttpPost]
    public IActionResult Delete(Todo todo)
    {
        _context.Todos.Remove(todo);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Finish(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            return NotFound();
        }
        todo.Finish();
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
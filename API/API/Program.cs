using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();


app.MapGet("/", () => "Prova A1");

//ENDPOINTS DE CATEGORIA
//GET: http://localhost:5273/categoria/listar
app.MapGet("/categoria/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Categorias.Any())
    {
        return Results.Ok(ctx.Categorias.ToList());
    }
    return Results.NotFound("Nenhuma categoria encontrada");
});

//POST: http://localhost:5273/categoria/cadastrar
app.MapPost("/categoria/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Categoria categoria) =>
{
    ctx.Categorias.Add(categoria);
    ctx.SaveChanges();
    return Results.Created("", categoria);
});

//ENDPOINTS DE TAREFA
//GET: http://localhost:5273/tarefas/listar
app.MapGet("/tarefas/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Tarefas.Any())
    {
        return Results.Ok(ctx.Tarefas.ToList());
    }
    return Results.NotFound("Nenhuma tarefa encontrada");
});

//POST: http://localhost:5273/tarefas/cadastrar
app.MapPost("/tarefas/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Tarefa tarefa) =>
{
    Categoria? categoria = ctx.Categorias.Find(tarefa.CategoriaId);
    if (categoria == null)
    {
        return Results.NotFound("Categoria não encontrada");
    }
    tarefa.Categoria = categoria;
    ctx.Tarefas.Add(tarefa);
    ctx.SaveChanges();
    return Results.Created("", tarefa);
});

//PUT: http://localhost:5273/tarefas/alterar/{id}
app.MapPatch("/tarefas/alterar/{id}", ([FromRoute] string id,
    [FromBody] Tarefa tarefaAlterada,[FromServices] AppDataContext ctx) =>
{
    //Implementar a alteração do status da tarefa
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if (tarefa is null)
    {
        return Results.
            NotFound("Tarefa não encontrada!");
    }
    tarefa.Status = tarefaAlterada.Status;

    ctx.Tarefas.Update(tarefa);
    ctx.SaveChanges();
    return Results.
        Ok("Status alterado com sucesso!");
    
});

//GET: http://localhost:5273/tarefas/naoconcluidas
app.MapGet("/tarefas/naoconcluidas", ([FromServices] AppDataContext ctx, string status) =>
{
    //Implementar a listagem de tarefas não concluídas
    Tarefa? tarefa =
        ctx.Tarefas.FirstOrDefault(t => t.Status == "Não Iniciada");
    if (status is null)
    {
        return Results.NotFound("Tarefa não encontrada!");
    }
    return Results.Ok(tarefa);
    
});

//GET: http://localhost:5273/tarefas/concluidas
app.MapGet("/tarefas/concluidas/{status}", ([FromServices] AppDataContext ctx, string status) =>
{
    //Implementar a listagem de tarefas concluídas
    Tarefa? tarefa =
        ctx.Tarefas.FirstOrDefault(t => t.Status == "Concluida");
    if (status is null)
    {
        return Results.NotFound("Tarefa não encontrada!");
    }
    return Results.Ok(tarefa);

});
    
app.Run();

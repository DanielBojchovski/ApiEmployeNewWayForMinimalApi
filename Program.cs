using Microsoft.AspNetCore.Mvc;
using ApiEmployee_NewWayForMinimalApi_.Repositories;
using ApiEmployee_NewWayForMinimalApi_.Models;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

//ConfigureServices
builder.Services.AddSingleton<EmployeeRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Employee>());

var app = builder.Build();

//Configure

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/employees/GetAll", ([FromServices] EmployeeRepository repo) =>
{
    return repo.GetAll();
});

app.MapGet("/employees/GetById/{id}", ([FromServices] EmployeeRepository repo, int id) =>
{
    var employee = repo.GetById(id);
    return employee is not null ? Results.Ok(employee) : Results.NotFound($"Employee with id = {id} does not exist.");
});

app.MapPost("/employees/Create", ([FromServices] EmployeeRepository repo, IValidator<Employee> validator, Employee employee) =>
{
    var validationResult = validator.Validate(employee);
    if (!validationResult.IsValid)
    {
        var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
        return Results.BadRequest(errors);
    }
    repo.Create(employee);
    return Results.Created("Employee added.", employee);
});

app.MapPut("/employees/Update/{id}", ([FromServices] EmployeeRepository repo, int id, Employee updatedEmployee) =>
{
    var employee = repo.GetById(id);
    if (employee is null)
    {
        return Results.NotFound($"There is no employee with id = {id}.");
    }
    repo.Update(updatedEmployee);
    return Results.Ok(updatedEmployee);
});

app.MapDelete("/employees/Delete/{id}", ([FromServices] EmployeeRepository repo, int id) =>
{
    var employee = repo.GetById(id);
    if (employee is null)
    {
        return Results.NotFound($"There is no employee with id = {id}.");
    }
    repo.Delete(id);
    return Results.Ok("Employee deleted.");
});

app.MapGet("/employees/GetAverageSalary", ([FromServices] EmployeeRepository repo) =>
{
    return repo.GetAverageSalary();
});

app.MapGet("/employees/GetTotalSumOfSalaries", ([FromServices] EmployeeRepository repo) =>
{
    return repo.GetTotalSumOfSalaries();
});

app.MapGet("/employees/SearchByName/{name}", ([FromServices] EmployeeRepository repo, string name) =>
{
    return repo.SearchByName(name);
});

app.MapGet("/employees/SearchBySkill/{skill}", ([FromServices] EmployeeRepository repo, string skill) =>
{
    return repo.SearchBySkill(skill);
});

app.MapGet("/employees/GetSalariesBiggerThen/{salary}", ([FromServices] EmployeeRepository repo, double salary) =>
{
    return repo.GetSalariesBiggerThen(salary);
});

app.MapGet("/employees/GetSalariesLessThen/{salary}", ([FromServices] EmployeeRepository repo, double salary) =>
{
    return repo.GetSalariesLessThen(salary);
});

app.Run();

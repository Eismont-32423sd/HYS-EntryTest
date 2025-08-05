using Application.Features.MeetingServ;
using Application.Features.UserServ;
using Application.Services.Interfaces;
using Domain.Interfaces;
using Infrastracture.Persistance.UnitOfWork;
using Infrastracture.Services.MeetingScheduler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IMeetingService, MeetingService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IMeetingSchedulerServic, MeetingSchedulerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

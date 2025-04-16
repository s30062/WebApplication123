using DeviceManager.Entities;
using DeviceManager.Logic;
 
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var manager = new DeviceManager.Logic.DeviceManager();

app.MapGet("/devices", () =>
{
    var result = manager.GetAllDevices()
        .Select(d => d.GetShortInfo());
    return Results.Ok(result);
});

app.MapGet("/devices/{id:int}", (int id) =>
{
    
    var device = manager.GetDeviceById(id);
    return device is null
        ? Results.NotFound("Device not found.")
        : Results.Ok(device.GetFullInfo());
});

app.MapPost("/devices", (DeviceDto dto) =>
{
    var device = DeviceFactory.CreateDevice(dto.Type, 0, dto.Name, dto.IsTurnedOn, dto.Parameters);
    if (device is null) return Results.BadRequest("Invalid device type or parameters.");

    bool success = manager.AddDevice(device);
    return success
        ? Results.Created($"/devices/{device.Id}", device.GetFullInfo())
        : Results.BadRequest("Device limit exceeded.");
});

app.MapPut("/devices/{id:int}", (int id, DeviceDto dto) =>
{
    var device = DeviceFactory.CreateDevice(dto.Type, id, dto.Name, dto.IsTurnedOn, dto.Parameters);
    if (device is null) return Results.BadRequest("Invalid device update payload.");
    

    bool success = manager.UpdateDevice(id, device);
    return success ? Results.Ok("Updated.") : Results.NotFound("Device not found.");
});

app.MapDelete("/devices/{id:int}", (int id) =>
{
    return manager.DeleteDevice(id)
        ? Results.Ok("Deleted.")
        : Results.NotFound("Device not found.");
});

app.Run();

public record DeviceDto(string Type, string Name, bool IsTurnedOn, string[] Parameters);
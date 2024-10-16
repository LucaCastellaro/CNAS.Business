using CNAS.Business.Extensions;
using CNAS.Business.Test.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CNAS.Business.Test;

public class UnitTest
{
    [Fact]
    public async Task AddBusiness()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Host.UseSerilog((_, config) => {
            config.WriteTo.Console();
        });

        builder.Services.AddMediator<UnitTest>();

        var app = builder.Build();

        var sender = app.Services.GetRequiredService<ISender>();
        Assert.NotNull(sender);

        var request = new GetDataQuery { Value = 1 };
        var response = await sender.Send(request);
        Assert.NotNull(response?.Data);
        Assert.Empty(response.Errors);
        Assert.Equal(request.Value, response.Data);
    }

    [Fact]
    public async Task Configure()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Host.UseSerilog((_, config) => {
            config.WriteTo.Console();
        });

        builder.Services
            .AddMediator<UnitTest>()
            .AddBehaviors()
            .AddValidators<UnitTest>()
            ;

        builder.Host.UseSerilog();

        var app = builder.Build();

        var sender = app.Services.GetRequiredService<ISender>();
        Assert.NotNull(sender);

        var request = new GetDataQuery { Value = 1 };
        var response = await sender.Send(request);
        Assert.NotNull(response);
        Assert.NotEmpty(response.Errors);
        Assert.Single(response.Errors);
    }
}
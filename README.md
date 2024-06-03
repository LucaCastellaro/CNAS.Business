# CNAS.Business

## Who
I am a software engineer and I like building custom solutions that fit my needs.

But as they are **not** professional solutions, i manually include my libraries over and over.

This is not only in conflict with the DRY principle, but also a waste of time.

## Why
To solve this *problem* i'm creating a set of libraries:
1. CNAS.Presentation
2. CNAS.Business
3. CNAS.Repository

## What
CNAS is an achronym for **C**lean .**N**ET **A**PI **S**ervice.

With the Business library you can create a clean business layer.

The idea is to create a nuget package for this library, to make it fast and easy to include in new projects.

The nuget package will be publicly deployed on the nuget store.

## How

### Install
1. Search for "CNAS.Business" in the nuget store
2. Install said nuget

### Usage

#### Register in Program.cs

``` c#
using CNAS.Business.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// Add your handlers, using "Program" as marker.
builder.Services.AddMediator<Program>();

// Add the generic validation behavior to the pipeline.
builder.Services.AddBehaviors();

// Add any custom validator that you may add, using "Program" as marker.
builder.Services.AddValidators<Program>();
```

#### Create a handler

In order to create a handler, you first need to create a response and a request.

I like to have these 3 classes in the same file, for easy debugging.

``` c#
using CNAS.Business.Handlers;
using CNAS.Business.Models;
using Microsoft.Extensions.Logging;

// The response must inherit from BaseResponse<TData>.
// In this case, the "Data" property will be of type int.
public sealed record GetDataResponse : BaseResponse<int>;

// Request are divided in queries and commands. 
// Queries are for reading data, while Commands are for writing data.
// Both of them must inherit from BaseRequest<TResponse>.
public sealed record GetDataQuery : BaseRequest<GetDataResponse>
{
    public required int Value { get; init; }
    public bool? Throw { get; init; }
}

// The handler must inherit from BaseHandler<GetDataQuery, GetDataResponse>.
// The base abstract class has a required field for the logger, which you must inject.
public sealed class GetDataHandler : BaseHandler<GetDataQuery, GetDataResponse>
{
    public GetDataHandler(ILogger<GetDataHandler> logger) : base(logger)
    {
    }

    public override Task<GetDataResponse> Handle(GetDataQuery req, CancellationToken ct)
    {
        if (req.Throw == true)
        {
            throw new NotImplementedException();
        }

        return Task.FromResult(
            new GetDataResponse
            {
                Data = req.Value
            }
        );
    }
}
```

#### Create a validator

`FluentValidation` is used to write validators. I didn't write a wrapper around it, but in future I think i will.

I like to also put this class in the handler file.

I suggest doing so only if the validator is small and doesn't have a lot of logic inside.

Following this rule I could've put this in the class above, but i wanted to have an example of having it separated.

``` c#
using CNAS.Business.Test.Handlers.Queries;
using FluentValidation;

public sealed class GetDataValidator : AbstractValidator<GetDataQuery>
{
    public GetDataValidator()
    {
        RuleFor(xx => xx.Value)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(1)
            ;
    }
}
```

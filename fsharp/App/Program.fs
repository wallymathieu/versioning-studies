module Wallymathieu.VersioningStudies.App.Program
open System

open Wallymathieu.VersioningStudies
open FSharpPlus
open FSharpPlus.Data
type DictEntry = System.Collections.DictionaryEntry

open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Http.Features
open Microsoft.AspNetCore.Authentication
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Wallymathieu.VersioningStudies.Domain

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

let configureServices (services : IServiceCollection) =
    services
        .AddGiraffe()
        .AddDataProtection() |> ignore

let configureLogging (loggerBuilder : ILoggingBuilder) =
    loggerBuilder.AddFilter(fun lvl -> lvl.Equals LogLevel.Error)
                 .AddConsole()
                 .AddDebug() |> ignore

let configureApp repository (app : IApplicationBuilder) =
    app.UseGiraffeErrorHandler(errorHandler)
       .UseGiraffe (Web.webApp repository)

[<EntryPoint>]
let main argv =
  // NOTE:
  let user1={ Id=UserId 1
              Login="user"; Password="psw"
              Data = { Email="email@email.se"
                       IsActive=true
                       FirstName="Firstname"; LastName="Lastname"
                       Roles=[Normal] } }
  let user2={ Id=UserId 2
              Login="support";Password="psw"
              Data = { Email="email2@email.se"
                       IsActive=true
                       FirstName="Firstname";LastName="Lastname"
                       Roles=[Support] } }
  let users= [user1;user2]
  let hardCodedRepository = { new IUserRepository with member this.GetUsers () = task { return users } }

  WebHost.CreateDefaultBuilder()
        .Configure(Action<IApplicationBuilder> (configureApp hardCodedRepository) )
        .ConfigureServices(configureServices)
        .ConfigureLogging(configureLogging)
        .Build()
        .Run()
  0

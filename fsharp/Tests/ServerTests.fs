module Wallymathieu.VersioningStudies.ServerTests
open Xunit
open System
open Microsoft.AspNetCore.Hosting
open Domain
open Utils
open Microsoft.AspNetCore.Builder
open Wallymathieu.VersioningStudies.App.Program
open System.Net
open Microsoft.AspNetCore.TestHost

let hardCodedRepository = { new IUserRepository with member this.GetUsers () = task { return users } }
let server =
    let builder = 
        WebHostBuilder()
                .Configure(Action<IApplicationBuilder> (configureApp hardCodedRepository))
                .UseEnvironment("Test")
                .ConfigureServices(configureServices)
                .ConfigureLogging(configureLogging)
    new TestServer(builder)
let v1UsersJson = """[
  {
    "id": 1,
    "name": "Firstname Lastname",
    "email": "email@email.se",
    "isActive": true,
    "roles": [
      "N"
    ]
  },
  {
    "id": 2,
    "name": "Firstname Lastname",
    "email": "email2@email.se",
    "isActive": true,
    "roles": [
      "S"
    ]
  }
]"""
let v2UsersJson = """[
  {
    "userUri": "/user/1",
    "name": {
      "firstname": "Firstname",
      "lastname": "Lastname"
    },
    "email": "email@email.se",
    "isActive": true,
    "roles": [
      "USR"
    ]
  },
  {
    "userUri": "/user/2",
    "name": {
      "firstname": "Firstname",
      "lastname": "Lastname"
    },
    "email": "email2@email.se",
    "isActive": true,
    "roles": [
      "SUP"
    ]
  }
]"""

[<Fact>]
let ``can query users endpoint``() = task {
    use client = server.CreateClient()
    let! response = client.GetAsync("/users")
    
    Assert.Equal (HttpStatusCode.OK, response.StatusCode)
    let! content = response.Content.ReadAsStringAsync()
    assertJsonEqual v2UsersJson content }

[<Fact>]
let ``can query users endpoint with v1``() = task {
    use client = server.CreateClient()
    client.DefaultRequestHeaders.Add ("x-version", "1")
    let! response = client.GetAsync("/users")
    
    Assert.Equal (HttpStatusCode.OK, response.StatusCode)
    let! content = response.Content.ReadAsStringAsync()
    assertJsonEqual v1UsersJson content }

[<Fact>]
let ``can query users endpoint with v2``() = task {
    use client = server.CreateClient()
    client.DefaultRequestHeaders.Add ("x-version", "2")
    let! response = client.GetAsync("/users")
    
    Assert.Equal (HttpStatusCode.OK, response.StatusCode)
    let! content = response.Content.ReadAsStringAsync()
    assertJsonEqual v2UsersJson content }
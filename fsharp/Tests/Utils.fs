module Wallymathieu.VersioningStudies.Utils
open FluentAssertions.Json
open Newtonsoft.Json.Linq

open Wallymathieu.VersioningStudies.Domain

let assertJsonEqual expected actual =
    let expectedToken = JToken.Parse expected
    let actualToken = JToken.Parse actual
    actualToken.Should().BeEquivalentTo(expected = expectedToken, because = "") // there is a default value "", bug in F# 
    |> ignore

let user1 = { Id=UserId 1
              Login="user"; Password="psw"
              Data = { Email="email@email.se"
                       IsActive=true
                       FirstName="Firstname"; LastName="Lastname"
                       Roles=[Normal] } }

let user2 = { Id=UserId 2
              Login="support";Password="psw"
              Data = { Email="email2@email.se"
                       IsActive=true
                       FirstName="Firstname";LastName="Lastname"
                       Roles=[Support] } }

let users= [user1;user2]

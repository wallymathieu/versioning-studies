module Wallymathieu.VersioningStudies.Tests
open Xunit
open Fleece
open Fleece.FSharpData

open Wallymathieu.VersioningStudies.Utils
open Wallymathieu.VersioningStudies.Domain
open Wallymathieu.VersioningStudies.Domain.JSON

let v1UserJson = """{
  "id": 1,
  "email": "email@email.se",
  "name": "Firstname Lastname",
  "isActive": true,
  "roles": ["N"]}
"""

[<Fact>]
let ``v1 format``() =
    assertJsonEqual v1UserJson (string (V1.User.toJson user1))

let v2UserJson = """{
 "userUri": "/user/1",
 "email": "email@email.se",
 "name": {
    "firstname": "Firstname",
    "lastname": "Lastname"
  },
  "isActive": true,
  "roles": ["USR"]}
"""

[<Fact>]
let ``v2 format``() =
    assertJsonEqual v2UserJson (string (V2.User.toJson user1))

[<Fact>]
let ``mix format v1 input``() =
    let decoded : UserData ParseResult = ofJsonText v1UserJson
    
    match decoded with 
    | Ok v->
      Assert.Equal ("email@email.se", v.Email)
      Assert.Equal ("Firstname", v.FirstName)
      Assert.Equal ("Lastname", v.LastName)
      Assert.Equal (true, v.IsActive)

    | Error e-> failwithf "failed to decode %O" e

[<Fact>]
let ``mix format v2 input``() =
    let decoded : UserData ParseResult = ofJsonText v2UserJson
    
    match decoded with 
    | Ok v->
      Assert.Equal ("email@email.se", v.Email)
      Assert.Equal ("Firstname", v.FirstName)
      Assert.Equal ("Lastname", v.LastName)
      Assert.Equal (true, v.IsActive)

    | Error e-> failwithf "failed to decode %O" e


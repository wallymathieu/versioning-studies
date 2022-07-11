module Tests

open System
open Xunit
open Core.Domain
open Core.JSON
open FluentAssertions.Json
open Newtonsoft.Json.Linq
let assertJsonEqual expected actual =
    (JToken.Parse expected).Should().BeEquivalentTo(JToken.Parse actual, null)
let user1={
    Id=UserId 1;Login="user"; Password="psw";Email="email@email.se";IsActive=true
    FirstName="Firstname";LastName="Lastname";Roles=[Normal]
}
let user2={
    Id=UserId 2;Login="support"; Password="psw";Email="email2@email.se";IsActive=true
    FirstName="Firstname";LastName="Lastname";Roles=[Support]
}
let users= [user1;user2]

[<Fact>]
let ``v1 format``() =
    let expected = """{
  "id": 1,
  "email": "email@email.se",
  "name": "Firstname Lastname",
  "isActive": true,
  "roles": ["N"]}
"""
    assertJsonEqual expected (string (userToJson V1 user1))
[<Fact>]
let ``v2 format``() =
    let expected = """{
 "userUri": "/user/1",
 "email": "email@email.se",
 "name": {
    "firstname": "Firstname",
    "lastname": "Lastname"
  },
  "isActive": true,
  "roles": ["USR"]}
"""
    assertJsonEqual expected (string (userToJson V2 user1))
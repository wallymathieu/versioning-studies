module Wallymathieu.VersioningStudies.Tests
open Xunit

open Wallymathieu.VersioningStudies.Utils
open Wallymathieu.VersioningStudies.JSON

[<Fact>]
let ``v1 format``() =
    let expected = """{
  "id": 1,
  "email": "email@email.se",
  "name": "Firstname Lastname",
  "isActive": true,
  "roles": ["N"]}
"""
    assertJsonEqual expected (string (V1.User.toJson user1))
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
    assertJsonEqual expected (string (V2.User.toJson user1))

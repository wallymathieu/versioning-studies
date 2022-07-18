module Wallymathieu.VersioningStudies.Tests
open Wallymathieu.VersioningStudies.Utils
open Wallymathieu.VersioningStudies.JSON

open Xunit

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
// Nice versioned spec: 
// https://github.com/abailly/sensei/blob/master/src/Sensei/User.hs#L119-L164
// https://github.com/abailly/sensei/blob/master/src/Sensei/Version.hs#L55
// https://github.com/abailly/sensei/blob/master/test/Sensei/UserSpec.hs#L29

// DataVersion:   
// https://github.com/agentultra/DataVersion/blob/master/test/Spec.hs

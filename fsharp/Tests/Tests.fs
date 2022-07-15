module Tests

open Xunit
open Core.Domain
open Core.JSON
open FluentAssertions.Json
open Newtonsoft.Json.Linq
let assertJsonEqual expected actual =
    (JToken.Parse expected).Should().BeEquivalentTo(JToken.Parse actual, null)
let user1={
    Id=UserId 1;Login="user"; Password="psw";Data={Email="email@email.se";IsActive=true
                                                   FirstName="Firstname";LastName="Lastname";Roles=[Normal]}
}
let user2={
    Id=UserId 2;Login="support"; Password="psw";Data={Email="email2@email.se";IsActive=true
                                                      FirstName="Firstname";LastName="Lastname";Roles=[Support]}
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
    assertJsonEqual expected (string (User.toJson V1 user1))
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
    assertJsonEqual expected (string (User.toJson V2 user1))
// Nice versioned spec: 
// https://github.com/abailly/sensei/blob/master/src/Sensei/User.hs#L119-L164
// https://github.com/abailly/sensei/blob/master/src/Sensei/Version.hs#L55
// https://github.com/abailly/sensei/blob/master/test/Sensei/UserSpec.hs#L29

// DataVersion:   
// https://github.com/agentultra/DataVersion/blob/master/test/Spec.hs

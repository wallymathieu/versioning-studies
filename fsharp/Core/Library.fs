namespace Core
module Domain =
    type UserId = UserId of int32
    with
        static member unwrap (UserId id) = id
    type UserRole = | Normal | Support | Administrator
    type User = {
        Id:UserId
        Login:string
        Password:string
        Email:string
        IsActive:bool
        FirstName:string
        LastName:string
        Roles: UserRole list }
    with
        member this.Name= $"%s{this.FirstName} %s{this.LastName}"

module JSON =
    open Domain
    open Fleece
    open Fleece.FSharpData
    open System
    type Version = | V1 | V2
    let mapRole version (role:UserRole)=
        match version with
        | V1 -> match role with | Normal -> "N" | Support -> "S" | Administrator -> "A"
        | V2 -> match role with | Normal -> "USR" | Support -> "SUP" | Administrator -> "ADM"

    let userToJson version (data:User)=
        let jsonName : Encoding Lazy = lazy jobj ["firstname".=data.FirstName; "lastname".=data.LastName]
        jobj (seq {
               if (version <= V1) then yield "id" .= UserId.unwrap data.Id
               if (version >= V2) then yield "userUri" .= "/user/" + Uri.EscapeDataString ( UserId.unwrap data.Id |> string )
               yield "email" .= data.Email
               yield "name" .= match version with
                               | V1 -> JString data.Name
                               | V2-> jsonName.Value
               yield "isActive" .= data.IsActive
               yield "roles" .= List.map (mapRole version) data.Roles
               } |> Seq.toList)
  

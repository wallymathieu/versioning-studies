module Wallymathieu.VersioningStudies.Web
open System
open FSharpPlus
open Giraffe
open Microsoft.AspNetCore.Http

open FSharpPlus.Data
open System.Text
open Fleece
open Wallymathieu.VersioningStudies.Giraffe.Json
open Wallymathieu.VersioningStudies.Domain
open Wallymathieu.VersioningStudies.JSON


module Paths =

  module Auction =
    /// /user
    let users = "/users"

type Version = | V1 | V2
with
    static member TryParse(v:string)=
        match v with | "v1" | "1" -> Some V1 | "v2" | "2" -> Some V2 | _ -> None

let defaultVersion = V2

let versioned f = fun (next:HttpFunc) (httpContext:HttpContext) ->
    (match httpContext.Request.Headers.TryGetValue "x-version" with
    | (true, u) ->
      string u
      |> tryParse
      |> function | Some v->f v
                  | _ ->f defaultVersion
    | _ -> f defaultVersion) next httpContext

let webApp (userRepository:IUserRepository) =

  let users = versioned (fun version -> GET >=> fun (next:HttpFunc) ctx -> task {
    let! auctionList =  userRepository.GetUsers()
    let toJson = match version with | V1 -> V1.User.toJson | V2 -> V2.User.toJson
    let jArray = auctionList |> List.map toJson |> List.toArray |> JArray
    return! json jArray next ctx
  })

  choose [ 
    route "/" >=> (text "App")
    route Paths.Auction.users >=> users ]

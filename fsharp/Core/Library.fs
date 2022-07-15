namespace Core

open FSharpPlus

module Domain =
    type UserId = UserId of int32
    with
        static member unwrap (UserId id) = id
    type UserRole = | Normal | Support | Administrator
    type UserData = {
        Email:string
        IsActive:bool
        FirstName:string
        LastName:string
        Roles: UserRole list
    }
    type User = {
        Id:UserId
        Login:string
        Password:string
        Data: UserData }
    with
        member this.Name= $"%s{this.FirstName} %s{this.LastName}"
        member this.FirstName= this.Data.FirstName
        member this.LastName= this.Data.LastName
        member this.Email= this.Data.Email
        member this.IsActive= this.Data.IsActive
        member this.Roles= this.Data.Roles

module JSON =
    open Domain
    open Fleece
    open Fleece.FSharpData
    open System
    type Version = | V1 | V2
    type Versioned<'T> = { Version:Version; Data:'T }
    with
        member self.Map map= { Version=self.Version; Data = map self.Data }
    /// 
    let tryParseDecodedString tryParse=
        function | JString v as jv -> match tryParse v with
                                      | Some v' -> Decode.Success v'
                                      | _ -> Decode.Fail.invalidValue jv ("Wrong case: " + v)
                 | x               -> Decode.Fail.strExpected  x
    /// type alias in order to shorten type parameter
    type E = Encoding

    module UserRole=
        let data = [Normal,("N","USR"); Support,("S","SUP"); Administrator,("A","ADM")]
        let parseV1 v =data |> List.tryFind (snd>> fst >> (=) v ) |> Option.map fst
        let formats v =data |> List.find (fst >> (=) v ) |> snd
        let toV1 v =formats v |> fst
        let parseV2 v = data |> List.tryFind (snd>> snd >> (=) v ) |> Option.map fst
        let toV2 v =formats v |> snd
        
        let v1C :Codec<E,_> = tryParseDecodedString parseV1 <-> (toV1 >> JString)
        let v2C :Codec<E,_> = tryParseDecodedString parseV2 <-> (toV2 >> JString)
        let vC version= match version with | V2 -> v2C | V1 -> v1C

    module Name =
        let v2C :Codec<E,string * string> =
            codec {
                let! firstname = jreq "firstname" (Some<<fst)
                and! lastname  = jreq "lastname"  (Some<<snd)
                return (firstname,lastname)
            } |> ofObjCodec
        let v1C:Codec<E,string * string> =
            let ofJson =
                function | JString v as jv -> match String.split [" "] v |> Seq.toList with
                                              | [ f;l] -> Decode.Success (f,l)
                                              | _ -> Decode.Fail.invalidValue jv ("Could not interpret name: " + v)
                         | x               -> Decode.Fail.strExpected  x
            let toJson (f,l)= JString $"%s{f} %s{l}" 
            ofJson <-> toJson
        let vC version = match version with | V2 -> v2C | V1 -> v1C

    module User=
        
        let cV version =
            codec {
                let nameC = Name.vC version
                let roleC = UserRole.vC version
                
                let! firstname,lastname = jreqWith nameC "name" (fun (x:UserData) -> Some (x.FirstName,x.LastName))
                and! email    = jreq "email"      (fun x -> Some x.Email)
                and! isActive = jreq "isActive"   (fun x -> Some x.IsActive)
                and! roles = jreqWith (Codecs.list roleC) "roles" (fun x -> Some x.Roles)
                return { Roles = roles; FirstName = firstname
                         LastName=lastname; Email = email
                         IsActive=isActive }
            }
            
        let toJson version (data:User)=
            jobj (seq {
                   if (version <= V1) then yield "id" .= UserId.unwrap data.Id
                   if (version >= V2) then yield "userUri" .= "/user/" + Uri.EscapeDataString ( UserId.unwrap data.Id |> string )
                   
                   yield! (Codec.encode (cV version) data.Data).Properties
                   } |> Seq.toList)
      

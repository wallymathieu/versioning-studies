namespace Wallymathieu.VersioningStudies
open System.Threading.Tasks
open FSharpPlus

module Domain =
    
    type UserId = UserId of int32
    with
        static member unwrap (UserId id) = id
    type UserRole = | Normal | Support | Administrator
    type UserData = {
        Email:      string
        IsActive:   bool
        FirstName:  string
        LastName:   string
        Roles:      UserRole list }
    type User = {
        Id:         UserId
        Login:      string
        Password:   string
        Data:       UserData }

    type IUserRepository = interface 
        abstract member GetUsers : unit -> Task<User list>
    end

module JSON =
    open Domain
    open Fleece
    open Fleece.FSharpData
    open System
    /// 
    let tryParseDecodedString tryParse=
        function | JString v as jv -> match tryParse v with
                                      | Some v' -> Decode.Success v'
                                      | _ -> Decode.Fail.invalidValue jv ("Wrong case: " + v)
                 | x               -> Decode.Fail.strExpected  x
    module V1=
        module UserRole=
            let data = [Normal,"N"; Support,"S"; Administrator,"A"]
            let tryParse v = data |> List.tryFind ( snd >> (=) v ) |> Option.map fst
            let toString v = data |> List.find ( fst >> (=) v ) |> snd
            
            let codec :Codec<Encoding,_> = tryParseDecodedString tryParse <-> (toString >> JString)
        module Name=
            let ofJson =
                function | JString v as jv -> match String.split [" "] v |> Seq.toList with
                                              | [ f;l] -> Decode.Success (f,l)
                                              | _ -> Decode.Fail.invalidValue jv ("Could not interpret name: " + v)
                         | x               -> Decode.Fail.strExpected  x
            let toJson (f,l)= JString $"%s{f} %s{l}" 
            let codec:Codec<Encoding,string * string> =
                ofJson <-> toJson
        module UserData=
            let codec =
                codec {
                    let! firstname,lastname = jreqWith Name.codec "name" (fun (x:UserData) -> Some (x.FirstName,x.LastName))
                    and! email    = jreq "email"      (fun x -> Some x.Email)
                    and! isActive = jreq "isActive"   (fun x -> Some x.IsActive)
                    and! roles = jreqWith (Codecs.list UserRole.codec) "roles" (fun x -> Some x.Roles)
                    return { Roles = roles; FirstName = firstname
                             LastName=lastname; Email = email
                             IsActive=isActive } }
        module User=
            let toJson (data:User)=
                jobj (seq {
                       yield "id" .= UserId.unwrap data.Id
                       
                       yield! (Codec.encode UserData.codec data.Data).Properties
                       } |> Seq.toList)
    module V2=
        module UserRole=
            let data = [Normal,"USR"; Support,"SUP"; Administrator,"ADM"]
            let toString v =data |> List.find (fst >> (=) v ) |> snd
            let tryParse v = data |> List.tryFind (snd >> (=) v ) |> Option.map fst
            
            let codec :Codec<Encoding,_> = tryParseDecodedString tryParse <-> (toString >> JString)
    
        module Name =
            let codec :Codec<Encoding,string * string> =
                codec {
                    let! firstname = jreq "firstname" (Some<<fst)
                    and! lastname  = jreq "lastname"  (Some<<snd)
                    return (firstname,lastname)
                } |> ofObjCodec
        module UserData=
            let codec =
                codec {
                    let! firstname,lastname = jreqWith Name.codec "name" (fun (x:UserData) -> Some (x.FirstName,x.LastName))
                    and! email    = jreq "email"      (fun x -> Some x.Email)
                    and! isActive = jreq "isActive"   (fun x -> Some x.IsActive)
                    and! roles = jreqWith (Codecs.list UserRole.codec) "roles" (fun x -> Some x.Roles)
                    return { Roles = roles; FirstName = firstname
                             LastName=lastname; Email = email
                             IsActive=isActive } }

        module User=
            let toJson (data:User)=
                jobj (seq {
                       yield "userUri" .= "/user/" + Uri.EscapeDataString ( UserId.unwrap data.Id |> string )
                       
                       yield! (Codec.encode UserData.codec data.Data).Properties
                       } |> Seq.toList)


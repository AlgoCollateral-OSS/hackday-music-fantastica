﻿module TagReader

open System.IO
open TagLib
open TagLib.Mpeg


let getAllMp3Files(path:string) =
    let dir = new DirectoryInfo(path)
    dir.GetFiles("*.mp3")
        |> Array.map (fun t -> (t.Name, t.FullName))
        |> Array.toList

let getAllId3v2ValidTags(mp3List) =
    let getId3v2Tag((mp3file:string,mp3path:string)) =
        let tagEngine = new TagLib.Mpeg.AudioFile(mp3path)
        let tag = tagEngine.GetTag(TagTypes.Id3v2)
        if (tag = null) then None
        else Some((tag,mp3file))

    mp3List |> List.map (fun mp3 -> getId3v2Tag mp3)
            |> List.choose id


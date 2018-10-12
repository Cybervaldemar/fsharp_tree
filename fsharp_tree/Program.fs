open System
open System.Collections
open System.IO


let displayElement (element, level, isLast) =
    printf "%s" <| String.replicate (level) "| "
    
    if isLast then 
        printfn "|_%s" element
    else 
        printfn "|-%s" element


(* Find out - what happens with file name in paths, different slashes *)
let rec surveyByDirs (path: string, level: int) =
    if  Directory.Exists path <> true then
        printfn "Path not found"
        exit 0
    
    let entryes = Directory.EnumerateFileSystemEntries path
    
    if entryes |> Seq.isEmpty then 
        exit 0
    
    let lastEl = entryes |> Seq.last
    
    for fsEntity in entryes do
        if lastEl = fsEntity then
            displayElement (fsEntity.Split '\\' |> Array.last, level, true)
        else 
            displayElement (fsEntity.Split '\\' |> Array.last, level, false)
        
        if Directory.Exists fsEntity then
            surveyByDirs(fsEntity, level + 1)

[<EntryPoint>]
let main argv =
    if Array.isEmpty argv then 
        printfn "Empty path"
    else
        printfn "%s" argv.[0]
        surveyByDirs(argv.[0], 0)
    0
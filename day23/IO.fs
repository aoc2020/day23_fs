module day23.IO

open System
open System.IO

let readLines (filePath:String) = seq {
    use sr = new StreamReader(filePath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}

let splitByBlanks(input:String[]) : String[][] =
    let lines = input |> List.ofSeq
    let accumulate (acc:List<List<String>>) (line:String) : List<List<String>>=
        match (acc,line) with
        | [],"" -> []
        | []::tail,"" -> []::tail
        | _,"" -> []::acc
        | [],x -> [[x]] 
        | head::tail,x -> (x::head)::tail
    lines
    |> List.fold accumulate []
    |> List.map (List.rev)
    |> List.map (List.toArray)
    |> List.rev
    |> List.toArray

let toXXX (block:String[]) =
    block 

let readInput (filePath:String) = 
    let lines = readLines filePath
    lines 

let testSplit () =
    let lines = [|"elem1";"elem2"|]
    let result = splitByBlanks lines
    printfn "Single block: %A" result 
    let lines = [|"elem11";"elem12";"";"elem21";"elem22"|]
    let result = splitByBlanks lines
    printfn "Two blocks: %A" result 
    let lines = [|"elem11";"elem12";"";"elem21";"elem22";"";"";"elem31";"elem32"|]
    let result = splitByBlanks lines
    printfn "Three blocks: %A" result 

open System
open day23.Types
open day23.Utils
open day23.IO
open day23.Task1
open day23.Task2 

let toCups (s:String) : Cup[] =
    let _0 = '0' |> int 
    s.ToCharArray ()
    |> Array.map (fun (c:Char) -> c |> int)
    |> Array.map (fun (c:int) -> c - _0)
    

[<EntryPoint>]
let main argv =
    let input2 = toCups("389125467")
    let input = toCups("315679824") 
    
    let circle = CupCircle(input2,1,9)
//    printfn "circle=%A" circle
    let result = circle.playRounds (100)
    let ordered = result.getOrderAfterOne () |> Array.tail |> String.Concat 
    printfn "Answer 1: %s" ordered

    let circle = circle.extendToOneMillion ()
//    printfn "extended: %A" circle
    let t1 = DateTime.Now
    let circle = circle.playRound ()
    let t2 = DateTime.Now
    let t3 = DateTime.Now 
    printfn "Time: %A %A" (t2-t1) (t3-t2) 
    
    0 // return an integer exit code
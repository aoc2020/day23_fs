open System
open day23.Types

let toCups (s:String) : Cup[] =
    let _0 = '0' |> uint 
    s.ToCharArray ()
    |> Array.map (fun (c:Char) -> c |> uint)
    |> Array.map (fun (c:Cup) -> c - _0)
    

[<EntryPoint>]
let main argv =
    let input2 = toCups("389125467")
    let input = toCups("315679824") 
    
    let circle = CupCircle(input2,1u,9u)
//    printfn "circle=%A" circle
    let result = circle.playRounds (100)
    let ordered = result.getOrderAfterOne () |> Array.tail |> String.Concat 
    printfn "Answer 1: %s" ordered

    let circle = circle.extendToOneMillion ()
    printfn "extended: %A" circle
    let t1 = DateTime.Now
    let circle = circle.playRound ()
    let t2 = DateTime.Now
    let t3 = DateTime.Now 
    printfn "Time: %A %A" (t2-t1) (t3-t2) 
 
    let cups = Cups(input2) 
    printfn "cups: %A posOf(9)=%d cupAt(2)=%d" cups (cups.posOf 9u) (cups.cupAt 2)
    let cups = cups.move3 5
    printfn "cups: %A posOf(9)=%d cupAt(2)=%d" cups (cups.posOf 9u) (cups.cupAt 2)
    let cups = cups.moveToNextCup ()
    printfn "cups: %A posOf(9)=%d cupAt(2)=%d" cups (cups.posOf 9u) (cups.cupAt 2)
    0   
    
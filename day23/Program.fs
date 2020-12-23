open System
open day23.Types

let toCups (s:String) : Cup[] =
    let _0 = '0' |> uint 
    s.ToCharArray ()
    |> Array.map (fun (c:Char) -> c |> uint)
    |> Array.map (fun (c:Cup) -> c - _0)
    

[<EntryPoint>]
let mainX argv =    
  let input2 = toCups("389125467")
  let input = toCups("315679824") 
  if false then
    let circle = CupCircle(Cups(input2),1u,9u)
    printfn "Circle: %A" circle
    let circle = circle.playRounds 10
//    printfn "Round 1: %A" circle
    0 
  else if false then      
    let circle = CupCircle(Cups(input2),1u,9u)
    printfn "circle=%A" circle
    let result = circle.playRounds (100)
    // let ordered = result.getOrderAfterOne () |> Array.tail |> String.Concat
    let ordered = "x"
    printfn "Cups: %A" circle.Cups 
    printfn "Answer 1: %s" ordered

    let circle = circle.extendToOneMillion ()
    printfn "extended: %A" circle
    let t1 = DateTime.Now
    let circle = circle.playRound ()
    let t2 = DateTime.Now
    let t3 = DateTime.Now 
    // printfn "Time: %A %A" (t2-t1) (t3-t2)
    0  
  else if false then 
    let cups = Cups(input2) 
    printfn "cups: %A posOf(9)=%d cupAt(2)=%d" cups (cups.posOf 9u) (cups.cupAt 2)
    let cups = cups.move3 9
    { 1..9 } |> Seq.map (fun (i:int) ->
        let cup = cups.cupAt i
        let pos = cups.posOf cup 
        printfn "*** %d -> %A : %A " i cup pos)
    |> Seq.toArray    
    printfn "cups: %A posOf(9)=%d cupAt(2)=%d" cups (cups.posOf 9u) (cups.cupAt 2)
    let cups = cups.moveToNextCup ()
    printfn "cups: %A posOf(9)=%d cupAt(2)=%d" cups (cups.posOf 9u) (cups.cupAt 2)
    0   
  else
      let cups = Cups([|1u;2u;3u;4u;5u;6u;7u;8u;9u|])
      let cups = cups.move3 9
      printfn "%A" cups
      {1..9} |> Seq.map (fun (i:int) -> printf "[%d@%d]" i (cups.posOf (i |> uint))) |> Seq.toArray 
      0
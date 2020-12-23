module day23.Types

open System

type Cup = int 

type CupCircle(cups:Cup[],min:Cup,max:Cup) as self =
    override this.ToString () = sprintf "CupCircle(%A)" cups
    member this.Cups = cups
    member this.Min = min
    member this.Max = max
    member this.currentCup : Cup = cups |> Array.head 
    
    member this.takeThreeNextCups () : Cup[] * CupCircle =
        [1],self 
        let taken : Cup[] = (cups |> Array.tail).[0..2]
        let rest : Cup[] = Array.append [|cups.[0]|] (cups.[4..])
        taken, CupCircle(rest,min,max)
    member this.findNextFrom (cup:Cup) : Cup =
        let cup = if cup = min then max else cup - 1
        if cups |> Array.contains cup then cup
        else this.findNextFrom cup
        
    member this.insertAt (cup:Cup) (selection:Cup[]) : CupCircle =
        let rec insertInto (list:Cup[]) : Cup[] =
            if list.[0] = cup then
                Array.append [|cup|] (Array.append selection (list.[1..list.Length-1]))
            else Array.append [|list.[0]|] (insertInto (list.[1..list.Length-1]))
        let cups = insertInto cups
        CupCircle(cups,min,max)
    
    member this.moveClockwise () : CupCircle =
        let cup = cups.[0]
        let cups = Array.append cups.[1..cups.Length-1] [|cup|]
        CupCircle(cups,min,max)
                    
    member this.playRound () : CupCircle = 
        let pickupCircle = this.takeThreeNextCups ()
        let selection = fst pickupCircle
        let circle = snd pickupCircle
        let target = circle.findNextFrom this.currentCup
        let circle = circle.insertAt target selection
//        printfn "pickup: %A" pickupCircle 
//       printfn "target: %d" target
//        printfn "inserted: %A" circle 
        let circle = circle.moveClockwise ()
//        printfn "nextState: %A" circle 
        circle 
        
    member this.playRounds (i:int) : CupCircle =
        if i = 0 then self
        else
            let next = this.playRound ()
            next.playRounds (i-1)
            
    member this.getOrderAfterOne () =
        let rec rotTo1 (cups:Cup[]) : Cup[] =
//            printfn "rotTo1: %A" cups 
            if cups.[0] = 1 then cups
            else rotTo1 (Array.append cups.[1..cups.Length] [|cups.[0]|])
        rotTo1 cups

    member this.extendToOneMillion () : CupCircle =
        let more = [|max+1..1000000|]
        let cups = Array.append cups more
        CupCircle (cups,min,1000000)
        
            
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
        let taken : Cup[] = cups.[1..3]
        let rest : Cup[] = Array.append [|cups.[0]|] (cups.[4..])
        taken, CupCircle(rest,min,max)
        
    member this.findNextFrom (cup:Cup) (selection:Cup[]) : Cup =
        let cup = if cup = min then max else cup - 1
        if selection |> Array.contains cup
        then this.findNextFrom cup selection 
        else cup 
        
    member this.insertAt (cup:Cup) (selection:Cup[]) : CupCircle =
        let i = Array.IndexOf (cups,cup) 
        let before = cups.[0..i]
        let after = cups.[i+1..cups.Length-1]
        let cups = Array.append (Array.append before selection) after
        CupCircle(cups,min,max)
    
    member this.moveClockwise () : CupCircle =
        let cup = cups.[0]
        let cups = Array.append cups.[1..cups.Length-1] [|cup|]
        CupCircle(cups,min,max)
                    
    member this.playRound () : CupCircle = 
        let pickupCircle = this.takeThreeNextCups ()
        let selection : Cup[] = fst pickupCircle
        let circle : CupCircle = snd pickupCircle
        let target : Cup = circle.findNextFrom this.currentCup selection 
        let circle : CupCircle = circle.insertAt target selection
        let circle = circle.moveClockwise ()
        circle 
        
    member this.playRounds (i:int) : CupCircle =
        if i = 0 then self
        else
            let next = this.playRound ()
            next.playRounds (i-1)
            
    member this.getOrderAfterOne () =
        let pos1: int = Array.IndexOf (cups,1)
        let before = cups.[0..pos1-1]
        let rest = cups.[pos1..cups.Length-1]
        Array.append rest before       
    
    member this.extendToOneMillion () : CupCircle =
        let more = [|max+1..1000000|]
        let cups = Array.append cups more
        CupCircle (cups,min,1000000)
        
            
module day23.Types

open System

type Cup = int
type FindPos = Cup -> int
type LookUp = int -> Cup
type Cups(cups: Cup[], findPos:FindPos,lookUp:LookUp,complexity:int) as self =
    
    new(cups:Cup[]) =
        let indexOf : Map<Cup,int> =
            let toPair (i:int) (cup:Cup) = (cup,i)
            cups |> Seq.mapi (toPair) |> Map.ofSeq  
        let lookUp : LookUp = (fun i -> cups.[i])
        let findPos = (fun (i:int) -> indexOf.[i])
        Cups (cups,findPos,lookUp,0)
        
    override this.ToString() = sprintf "Cups(%s)" ({0..cups.Length-1} |> Seq.map (lookUp) |> String.Concat )
    member this.posOf (cup:Cup) = findPos cup
    member this.cupAt (pos:int) = lookUp pos
    
    member this.move3 (target:int) =
        printfn "move3 %d" target
        let chars = [|lookUp(1);lookUp(2);lookUp(3)|]
        let at_0 = lookUp 0 
        let newLookUp (pos:int) =
            if pos = 0 then
                at_0
            else if pos < (target - 2) then 
                lookUp (pos+3)
            else if pos < target+1 then
                lookUp (pos-target+3)  
            else
                lookUp pos // it's unaffected 
        let newFindPos (cup:Cup) =
            let pos = findPos cup
            if pos < 4 && pos > 0 then (pos + target - 3) 
            else if pos > target then pos
            else if pos = 0 then 0
            else pos - 3
        Cups (cups,newFindPos,newLookUp,complexity+1)
            
        

    
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
        let t1 = DateTime.Now 
        let pickupCircle = this.takeThreeNextCups ()
        let t2 = DateTime.Now 
        let selection : Cup[] = fst pickupCircle
        let circle : CupCircle = snd pickupCircle
        let target : Cup = circle.findNextFrom this.currentCup selection 
        let t5 = DateTime.Now 
        let circle : CupCircle = circle.insertAt target selection
        let t6 = DateTime.Now 
        let circle = circle.moveClockwise ()
        let t7 = DateTime.Now
        printfn "Times: 2:%A 6:%A 7:%A" (t2-t1) (t6-t5) (t7-t6)
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
        
            
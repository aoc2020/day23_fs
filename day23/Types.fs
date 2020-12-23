module day23.Types

open System

type Cup = uint
type FindPos = Cup -> int
type LookUp = int -> Cup
type Cups(cups: Cup[],
          findPos:FindPos,
          lookUp:LookUp,
          complexity:int,
          lastIndex:int,
          dirty: Set<int>,
          shifted: int) as self =        
    new(cups:Cup[],shifted:int) =
        let indexOf : Map<Cup,int> =
            let toPair (i:int) (cup:Cup) = (cup,i+1)
            cups |> Seq.mapi (toPair) |> Map.ofSeq  
        let lookUp (i:int) =            
            cups.[i-1]
        let findPos (c:Cup) =
            indexOf.[c]
        Cups (cups,findPos,lookUp,0,cups.Length,Set.empty,0)
    new(cups:Cup[]) = Cups(cups,0)
        
    override this.ToString() =
        let length = min lastIndex 20
        sprintf "Cups(%s)" ({1..length} |> Seq.map (lookUp) |> String.Concat )
    member this.posOf (cup:Cup) = findPos cup
    member this.cupAt (pos:int) = lookUp pos
    
    member this.move3 (target:int) =
        let at_1 = lookUp 1
        let newLookUp (pos:int) =
            printfn "newLookup %d target=%d" pos target  
            let a= if pos = 1 then
                        at_1
                   elif pos > target then
                        printfn "pos after: %d:" pos 
                        lookUp pos
                   elif pos < target - 2 then
                        printfn "pos before: %d -> [%d]" pos (pos + 3)
                        lookUp (pos + 3)                       
                   else // in move group                        
                        printfn "in group: %d -> [%d]" pos  (pos-target+3)
                        lookUp (pos-target+4)
            a 
        let newFindPos (cup:Cup) =
            let pos = findPos cup
            if pos < 5 && pos > 1 then (pos + target - 4) 
            else if pos > target then pos
            else if pos = 1 then 1
            else pos - 3
        Cups (cups,newFindPos,newLookUp,complexity+1,lastIndex,dirty,shifted)
    
    member this.shift (shiftBy:int) : Cups =         
        let newLookup (i:int) =
            let i = i + shiftBy
            let i = if i > lastIndex then i - lastIndex else i
            lookUp i
        let newFindCup (cup:Cup) =
            let oldPos = findPos cup
            if oldPos = 1 then cups.Length
            else oldPos - 1
        Cups (cups,newFindCup,newLookup,complexity+1,lastIndex,dirty,shifted+1)

    member this.moveToNextCup() : Cups = (this.shift 1) 
    
    member this.extendTo (last:int) =
        Cups(cups,findPos,lookUp,complexity,last,dirty,0)
        
    member this.extendToOneMillion() : Cups = this.extendTo 1_000_000
    
type CupCircle(cups:Cups,min:Cup,max:Cup) as self =
    override this.ToString () = sprintf "CupCircle(%A)" cups
    member this.Cups = cups
    member this.Min = min
    member this.Max = max
    member this.currentCup : Cup = cups.cupAt 1 
    
    member this.findThreeNextCups () : Cup[] =
        let cup1 = cups.cupAt 2
        let cup2 = cups.cupAt 3
        let cup3 = cups.cupAt 4 
        let taken : Cup[] = [|cup1;cup2;cup3|]
        taken
        
    member this.findNextFrom (cup:Cup) (selection:Cup[]) : Cup  =
        let cup = if cup = min then max else cup - 1u
        if selection |> Array.contains cup
        then this.findNextFrom cup selection 
        else cup      
        
    member this.moveClockwise () : CupCircle =
        let cups = cups.moveToNextCup()
        CupCircle(cups,min,max)
                    
    member this.playRound () : CupCircle =
        printfn "Play round for %A" self 
        let t2 = DateTime.Now 
        let currentCup = this.currentCup
        let selection = this.findThreeNextCups ()
        let t4 = DateTime.Now
        let target : Cup = this.findNextFrom this.currentCup selection
        let targetPos = cups.posOf target 
        let t6 = DateTime.Now 
        let cups = cups.move3 targetPos
        let circle = CupCircle(cups,min,max)
        let t8 = DateTime.Now 
        let circle = circle.moveClockwise ()
        let t10 = DateTime.Now
//        printfn "Times: 2:%A 4:%A 6:%A 8:%A" (t4-t2) (t6-t4) (t8-t6) (t10-t8)
        circle 
        
    member this.playRounds (i:int) : CupCircle =
        if i = 0 then self
        else
            let next = this.playRound ()
            next.playRounds (i-1)
            
    member this.getOrderAfterOne () =
        let pos = cups.posOf 1u
        let c = cups.moveToNextCup ()
        // let cups = cups
        // let pos1: int = Array.IndexOf (cups,1)
        // let before = cups.[0..pos1-1]
        // let rest = cups.[pos1..cups.Length-1]
        // Array.append rest before
        "12345678"
    
    member this.extendToOneMillion () : CupCircle =
        let cups = cups.extendToOneMillion ()
        let max = 1_000_000u
        CupCircle(cups,min,max)
        
//        let more = [|max+1u..1000000u|]
//        let cups = Array.append cups more
//        CupCircle (cups,min,1000000u)
        
            
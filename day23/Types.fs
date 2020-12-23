module day23.Types

open System

type Cup = uint
type FindPos = Cup -> int
type LookUp = int -> Cup
type Cups(cups: Cup[], findPos:FindPos,lookUp:LookUp,complexity:int) as self =
    
    new(cups:Cup[]) =
        let indexOf : Map<Cup,int> =
            let toPair (i:int) (cup:Cup) = (cup,i+1)
            cups |> Seq.mapi (toPair) |> Map.ofSeq  
        let lookUp : LookUp = (fun i -> cups.[i-1])
        let findPos = (fun (c:Cup) -> indexOf.[c])
        Cups (cups,findPos,lookUp,0)
        
    override this.ToString() = sprintf "Cups(%s)" ({1..cups.Length} |> Seq.map (lookUp) |> String.Concat )
    member this.posOf (cup:Cup) = findPos cup
    member this.cupAt (pos:int) = lookUp pos
    
    member this.move3 (target:int) =
        // printfn "move3 %d" target
        let chars = [|lookUp(2);lookUp(3);lookUp(4)|]
        let at_1 = lookUp 1
        let newLookUp (pos:int) =
            printfn "newLookup %d target=%d" pos target  
            let a= if pos = 1 then
                        // printfn "pos=0 -> %d" at_0
                        at_1
                        // 0u
                   elif pos > target then
                        printfn "pos after: %d:" pos 
                        lookUp pos
                   elif pos < target - 2 then
                        printfn "pos before: %d -> [%d]" pos (pos + 3)
                        lookUp (pos + 3)                       
                   else // in move group                        
                        printfn "in group: %d -> [%d]" pos  (pos-target+3)
                        lookUp (pos-target+4)
//            printfn "move3:newLookup(%d) -> [%d]" pos a  
            a 
        let newFindPos (cup:Cup) =
            // printfn "move3:newFindCup(%d)" cup 
            let pos = findPos cup
            if pos < 5 && pos > 1 then (pos + target - 4) 
            else if pos > target then pos
            else if pos = 1 then 1
            else pos - 3
        Cups (cups,newFindPos,newLookUp,complexity+1)
    member this.moveToNextCup() =
        let newLookup (i:int) =
            // printfn "moveToNextCup:newLookup(%d)" i
            if i = (cups.Length) then lookUp 1
            else lookUp (i+1) 
        let newFindCup (cup:Cup) =
            // printfn "moveToNextCup:newFindCup(%d)" cup
            let oldPos = findPos cup
            if oldPos = 1 then cups.Length
            else oldPos - 1
        Cups (cups,newFindCup,newLookup,complexity+1)
    
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
        // printfn "selection: %d : %A" currentCup selection 
        let t4 = DateTime.Now
        let target : Cup = this.findNextFrom this.currentCup selection
        let targetPos = cups.posOf target 
        // printfn "target: %d" target 
        let t6 = DateTime.Now 
        let cups = cups.move3 targetPos
        // printfn "hello"       
        // printfn "cups.move3 %d -> %A" target cups 
        let circle = CupCircle(cups,min,max)
        // printfn "world"       
        let t8 = DateTime.Now 
        let circle = circle.moveClockwise ()
        // printfn "Shifted: %A" circle
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
        self 
//        let more = [|max+1u..1000000u|]
//        let cups = Array.append cups more
//        CupCircle (cups,min,1000000u)
        
            
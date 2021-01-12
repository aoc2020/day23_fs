module day23.MappedCupRing

open System
open day23.BaseTypes
open day23.CupMap
open day23.DefaultCupRing
open day23.StartCups    
type CupRange (first:Pos, last:Pos) =
    member this.First = first
    member this.Last = last      

let eqPair<'T when 'T : equality >(p:('T*'T)) = match p with | (a,b) -> a = b 

type IMappedRing =
    abstract member underlyingRing: DefaultCupRing
    abstract member Depth: uint
    abstract member Range: Option<CupRange>
    abstract member findCup: PosToCup 
    abstract member findPos: CupToPos 
    abstract member shift: Unit -> IMappedRing
    abstract member First: Pos
    abstract member Last: Pos
    abstract member Size: Pos
    abstract member Max: Cup
    abstract member Equals : 'a -> bool
    abstract member startCups : Unit -> StartCups
    abstract member moveRangeLeft : CupRange -> Pos -> IMappedRing
    abstract member moveRangeRight : CupRange -> Pos -> IMappedRing 
    
let mappedRingEqual (this: IMappedRing) (other) : bool =
        printfn "X"
        match box other with
        | :? IMappedRing as other -> 
            let cups = [|this.First..this.Last|] |> Array.map this.findCup           
            let posi = [|1u..this.Max|] |> Array.map this.findPos
            let posi = posi |> Array.map (fun x -> x + other.First) 
            let ocups = [|other.First..other.Last|] |> Array.map other.findCup
            let oposi = [|1u..other.Max|] |> Array.map other.findPos
            let oposi = oposi |> Array.map (fun x -> x + this.First)
            printfn "%A=%A  %A=%A" cups ocups posi oposi 
            cups = ocups && posi = oposi 
        | _ -> false             

type DirectMappedRing (ring:DefaultCupRing) =
    interface IMappedRing with 
        member this.underlyingRing = ring
        member this.Depth = 0u
        member this.Range = None 
        member this.findCup = ring.findCup
        member this.findPos = ring.findPos
        member this.shift () = DirectMappedRing(ring.shift()) :> IMappedRing
        member this.First = ring.First
        member this.Last = ring.Last
        member this.Size = ring.Size
        member this.Max = ring.Max
        override this.Equals (other) : bool = mappedRingEqual this other
        member this.startCups () : StartCups  =
            let cup_1 = ring.findCup (ring.First)
            let cup_2 = ring.findCup (ring.First+1UL)
            let cup_3 = ring.findCup (ring.First+2UL)
            let cup_4 = ring.findCup (ring.First+3UL)
            let first4cups = [|cup_1;cup_2;cup_3;cup_4|]
            StartCups (first4cups, ring.Max)
        member this.moveRangeLeft (range:CupRange) (steps: Pos) : IMappedRing =
            let adjust x = x - steps
            let addCup (cups:CupMap) (posCup:Pos*Cup) = cups.Add (fst posCup) (snd posCup) 
            let cupMap = ring.CupMap
            let range = [|range.First..range.Last|] 
            let cups = range |> Array.map ring.findCup 
            let range = range |> Array.map adjust           
            let posCups = Array.zip range cups
            let cupMap = posCups |> Array.fold addCup cupMap
            let ring = ring.withCups cupMap
            DirectMappedRing(ring) :> IMappedRing
        member this.moveRangeRight (range:CupRange) (steps: Pos) : IMappedRing =
            let adjust x = x + steps
            let addCup (cups:CupMap) (posCup:Pos*Cup) = cups.Add (fst posCup) (snd posCup) 
            let cupMap = ring.CupMap
            let range = [|range.First..range.Last|] |> Array.rev 
            let cups = range |> Array.map ring.findCup
            let range = range |> Array.map adjust           
            let posCups = Array.zip range cups
            let cupMap = posCups |> Array.fold addCup cupMap
            let ring = ring.withCups cupMap
            DirectMappedRing(ring) :> IMappedRing      
        
    override this.Equals (other) : bool = mappedRingEqual this other
    override this.ToString () : String =
        let first = ring.First
        let last = ring.Last
        let findCup = ring.findCup
        let sep = if ring.Size > 15UL then "..." else " " 
        let firstPart = {first..min(first+14UL) (last-2UL)}
                        |> Seq.map (fun (p:Pos) -> findCup p |> sprintf "%d" )
                        |> String.concat " "
        let lastPart = {last - 1UL .. last}
                        |> Seq.map (fun (p:Pos) -> findCup p |> sprintf "%d")
                        |> Seq.toArray 
                        |> String.concat " "
        sprintf "DefaultMappedRing(%s%s%s)" firstPart sep lastPart

let flattenRing (ring:IMappedRing) : IMappedRing =
    match ring.Range with
        | None -> ring
        | Some(range) ->
            let inner = ring.underlyingRing
            DirectMappedRing(inner) :> IMappedRing            

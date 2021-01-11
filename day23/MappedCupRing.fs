module day23.MappedCupRing

open System
open day23.BaseTypes
open day23.DefaultCupRing    
type CupRange (first:uint, last:uint) =
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
    
let mappedRingEqual (this: IMappedRing) (other) : bool =
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

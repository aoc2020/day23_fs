module day23.MappedCupRing

open day23.BaseTypes
open day23.DefaultCupRing    
type CupRange =
    | CupRange of Pos*Pos
    | Empty 

type IMappedRing =
    abstract member underlyingRing: DefaultCupRing
    abstract member Depth: uint
    abstract member Range: CupRange
    abstract member findCup: PosToCup 
    abstract member findPos: CupToPos 
    abstract member shift: Unit -> IMappedRing
    abstract member First: Pos
    abstract member Last: Pos
    abstract member Size: Pos 
  

type DirectMappedRing (ring:DefaultCupRing) =
    interface IMappedRing with 
        member this.underlyingRing = ring
        member this.Depth = 0u
        member this.Range = Empty
        member this.findCup = ring.findCup
        member this.findPos = ring.findPos
        member this.shift () = DirectMappedRing(ring.shift()) :> IMappedRing
        member this.First = ring.First
        member this.Last = ring.Last
        member this.Size = ring.Size 


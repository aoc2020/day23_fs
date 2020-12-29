module day23.MappedCupRing

open day23.BaseTypes
open day23.DefaultCupRing

type IRingOverride =
    abstract member overrideFindCup: PosToCup -> PosToCup
    abstract member overrideFindPos: CupToPos -> CupToPos
    abstract member Start: Option<Pos>
    abstract member End: Option<Pos>
    
type NoopRingOverride =
    interface IRingOverride with 
        member this.overrideFindCup (pos2Cup: PosToCup) = pos2Cup
        member this.overrideFindPos (cup2Pos: CupToPos) = cup2Pos
        member this.Start = None
        member this.End = None 
    
    

type MappedCupRing (ring:ICupRing,
                    mappedFindPos:CupToPos,
                    mappedFindCup:PosToCup,
                    overrideStart:Pos,
                    overrideEnd:Pos,
                    depth:uint) =
    member this.Ring = ring
    override this.ToString() =
        let self = this :> ICupRing 
        let sep = if self.Size > 17UL then "..." else "" 
        let firstPart = {self.First..(min self.Last (self.Last-2UL))}
                        |> Seq.map (fun (p:Pos) -> self.findCup p |> sprintf "%d " )
                        |> String.concat ""
        let lastPart = {self.Last - 2UL .. self.Last}
                        |> Seq.map (fun (p:Pos) -> self.findCup p |> sprintf " %d")
                        |> String.concat ""
        sprintf "CupRing(%s%s%s)" firstPart sep lastPart

    
    interface ICupRing with
        member this.First = ring.First
        member this.Last = ring.Last
        member this.Size = ring.Size
        member this.Max = ring.Max 
        member this.stepClockwise () =
            let ring = ring.stepClockwise ()
            let overrideStart: Pos = max overrideStart ring.First 
            MappedCupRing(ring,mappedFindPos,mappedFindCup,overrideStart,overrideEnd,depth) :> ICupRing
        member this.findPos (cup:Cup) = ring.findPos cup
        member this.findCup (pos:Pos) = ring.findCup pos 
        
let overrideCupRing (ring:ICupRing) (ringOverride:IRingOverride) : int = // ICupRing =
    let findCup = ringOverride.overrideFindCup ring.findCup
    let findPos = ringOverride.overrideFindPos ring.findPos
    42 
    
    
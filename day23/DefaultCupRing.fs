module day23.DefaultCupRing

open day23.BaseTypes
open day23.CupMap

type DefaultCupRing (cups:CupMap,
              first:Pos,
              last:Pos,
              max:Cup,
              defaultPos:CupToPos,
              defaultCup:PosToCup) =
    
    override this.ToString() =
        let self = this :> ICupRing 
        let sep = if self.Size > 17UL then "..." else "" 
        let firstPart = {first..min(first+15UL) (last-2UL)}
                        |> Seq.map (fun (p:Pos) -> self.findCup p |> sprintf "%d " )
                        |> String.concat ""
        let lastPart = {last - 2UL .. last}
                        |> Seq.map (fun (p:Pos) -> self.findCup p |> sprintf " %d")
                        |> String.concat ""
        sprintf "CupRing(%s%s%s)" firstPart sep lastPart

    new(initCups: Cup[], last:Pos) =
        DefaultCupRing(CupMap(initCups,1UL),1UL,last,asCup last,asPos,asCup)
    interface ICupRing with 
        member this.Size : Pos = last - first + 1UL  
        member this.First = first
        member this.Last = last
        member this.Max = max   
        member this.findPos (cup:Cup) =
            if cups.containsCup cup then cups.getPos cup 
            else defaultPos cup
        member this.findCup (pos:Pos) =
            if cups.containsPos pos then cups.getCup pos
            else defaultCup pos
        
        member this.stepClockwise() : ICupRing =
            let cup = (this :> ICupRing).findCup first
            let first = first + 1UL
            let last = last + 1UL
            let cups = cups.Move cup last
            DefaultCupRing (cups,first,last,max,defaultPos,defaultCup) :> ICupRing     
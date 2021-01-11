module day23.CupRing

open System
open day23.BaseTypes
open day23.MappedCupRing

type StartCups (firstCups: Cup[], max:Cup) =
    override this.ToString () =
        sprintf "StartCups(%d:(%d %d %d)" firstCups.[0] firstCups.[1] firstCups.[2] firstCups.[3]
    member this.First : Cup = firstCups.[0]
    member this.Next : Cup =
        let rec next (cup:Cup) =
            let cup = if cup = 0u then max else cup  
            if cup = firstCups.[1] || cup = firstCups.[2] || cup = firstCups.[3] then
                next (cup-1u) 
            else
                cup 
        next (firstCups.[0]-1u)
    member this.group () : Cup[] = firstCups.[1..4]
    
type ModificationApproach =
    | ModifyBack
    | ModifyFront
    | MapBack
    | MapFront 
type CupRing (mapped:IMappedRing) as self =
    override this.ToString() =
        let first = mapped.First
        let last = mapped.Last
        let findCup = mapped.findCup
        let sep = if mapped.Size > 15UL then "..." else " " 
        let firstPart = {first..min(first+14UL) (last-2UL)}
                        |> Seq.map (fun (p:Pos) -> findCup p |> sprintf "%d" )
                        |> String.concat " "
        let lastPart = {last - 1UL .. last}
                        |> Seq.map (fun (p:Pos) -> findCup p |> sprintf "%d")
                        |> Seq.toArray 
                        |> String.concat " "
        sprintf "CupRing(%s%s%s)" firstPart sep lastPart
        
    member this.Ring : IMappedRing = mapped 
    member this.shift () = CupRing(mapped.shift ())
    member this.startCups () : StartCups  =
        let cup_1 = mapped.findCup (mapped.First)
        let cup_2 = mapped.findCup (mapped.First+1UL)
        let cup_3 = mapped.findCup (mapped.First+2UL)
        let cup_4 = mapped.findCup (mapped.First+3UL)
        let first4cups = [|cup_1;cup_2;cup_3;cup_4|]
        StartCups (first4cups, mapped.Max)
        
    member this.findCup : PosToCup = mapped.findCup
    member this.findPos : CupToPos = mapped.findPos
        
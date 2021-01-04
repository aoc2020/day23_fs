module day23.CupRing

open System
open day23.BaseTypes
open day23.MappedCupRing

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
        
    member this.shift () = CupRing(mapped.shift ())
    member this.firstCup () = mapped.findCup mapped.First 
    
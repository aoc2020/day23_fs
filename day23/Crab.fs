module day23.Crab

open System
open day23.CupRing

type Crab (ring: CupRing, step: uint) =
    override this.ToString() : String = sprintf "CRAB(%d): %A" step ring
    
    member this.shuffle () : Crab =
        let start = ring.startCups ()
        let next = start.Next
        let pos = ring.findPos next
        this        
        

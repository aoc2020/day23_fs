module day23.CupRing

open System
open day23.MappedCupRing

type CupRing (mapped:IMappedRing) as self =
    override this.ToString() = sprintf "CupRing(%A)" mapped 
    
        
    
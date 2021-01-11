module day23.ShufflerFactory

open day23.BaseTypes
open day23.MappedCupRing

type Shuffler = IMappedRing -> Pos -> IMappedRing

let flattenAndDirect (ring:IMappedRing) (pos:Pos) : IMappedRing =
    let ring = flattenRing ring
    ring  

let noopShuffler (ring:IMappedRing) (pos:Pos) : IMappedRing = ring 

let chooseStrategy (range: Option<CupRange>) (lastPos:Pos) (pos:Pos) : Shuffler = 
    noopShuffler 


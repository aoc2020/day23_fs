module day23.BaseTypes

type Cup = uint32
type Pos = uint64

type CupToPos = Cup->Pos
type PosToCup = Pos->Cup

let asPos (cup:Cup):Pos = cup |> uint64
let asCup (pos:Pos):Cup = pos |> uint32    
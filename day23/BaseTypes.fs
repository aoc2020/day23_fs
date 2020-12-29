module day23.BaseTypes

type Cup = uint32
type Pos = uint64

type CupToPos = Cup->Pos
type PosToCup = Pos->Cup

type ICupRing =
    abstract member First: Pos
    abstract member Last: Pos
    abstract member Size: Pos 
    abstract member Max: Cup
    abstract member findPos: Cup -> Pos
    abstract member findCup: Pos -> Cup 
    abstract member stepClockwise: Unit -> ICupRing 

let asPos (cup:Cup):Pos = cup |> uint64
let asCup (pos:Pos):Cup = pos |> uint32


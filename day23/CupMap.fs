module day23.CupMap

open day23.BaseTypes

type CupMap (map2cup: Map<Pos,Cup>, map2pos: Map<Cup,Pos>) =
    member this.containsCup (cup:Cup) = map2pos.ContainsKey cup
    member this.containsPos (pos:Pos) = map2cup.ContainsKey pos
    member this.getCup (pos:Pos) = map2cup.[pos]
    member this.getPos (cup:Cup) = map2pos.[cup]
    new (cups: Cup[],first:Pos) =        
        let map2cup : Map<Pos,Cup> = 
            cups
            |> Array.mapi (fun (pos0:int) (cup:Cup) -> ((pos0 |> uint64 ) + first, cup))
            |> Map.ofArray
        let map2pos : Map<Cup,Pos> =
            cups
            |> Array.mapi (fun (pos0:int) (cup:Cup) -> (cup, (pos0 |> uint64 ) + first))
            |> Map.ofArray
        CupMap(map2cup,map2pos)
    member this.Remove (pos:Pos) : CupMap =
        let cup = map2cup.[pos]
        let map2pos = map2pos.Remove cup
        let map2cup = map2cup.Remove pos
        CupMap(map2cup,map2pos)
    member this.Add (pos:Pos) (cup:Cup) : CupMap =
        let map2pos = map2pos.Add (cup,pos)
        let map2cup = map2cup.Add (pos,cup)
        CupMap(map2cup,map2pos)
    member this.Move (cup:Cup) (pos:Pos) : CupMap =
        let map2cup = 
            if map2pos.ContainsKey cup then 
                let oldPos = map2pos.[cup]
                map2cup.Remove oldPos
            else map2cup  
        let map2pos = map2pos.Add (cup,pos)
        let map2cup = map2cup.Add (pos,cup)
        CupMap(map2cup,map2pos)
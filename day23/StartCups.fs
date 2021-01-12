module day23.StartCups

open day23.BaseTypes

type StartCups (firstCups: Cup[], max:Cup) =
    override this.ToString () =
        sprintf "StartCups(%d:(%d %d %d)" firstCups.[0] firstCups.[1] firstCups.[2] firstCups.[3]
    member this.First : Cup = firstCups.[0]
    member this.TargetCup : Cup =
        let rec next (cup:Cup) =
            let cup = if cup = 0u then max else cup  
            if cup = firstCups.[1] || cup = firstCups.[2] || cup = firstCups.[3] then
                next (cup-1u) 
            else
                cup 
        next (firstCups.[0]-1u)
    member this.group () : Cup[] = firstCups.[1..4]


module day23_tests.TestExample1

open Xunit
open day23.Crab
open day23.CupRing
open day23.RingFactory 
let INIT_RING () : CupRing = createRing "389125467" false

[<Fact>]
let testInitRing () =
    let ring = INIT_RING () 
    Assert.Equal ("CupRing(3 8 9 1 2 5 4 6 7)", ring.ToString ()) 

[<Fact>]
let testStep1 () =
    let ring = INIT_RING () 
    let crab = Crab(ring,0u)
//    printfn "POS: %d @ %d" next pos
//    Assert.Equal (next,2u)
//    Assert.Equal (pos, 5UL)
    Assert.Equal ("Not implemented","42u") 



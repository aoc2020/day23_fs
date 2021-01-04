module day23_tests.TestRingFactory

open Xunit
open day23.BaseTypes
open day23.CupRing
open day23.RingFactory

[<Fact>]

let CUP_0 : Cup = 0u
let CUP_9 : Cup = 9u 

[<Fact>]
let testCharToCup =
    Assert.Equal (CUP_0, charToCup '0')
    Assert.Equal (CUP_9, charToCup '9')
    
let testCreateRing =
    let ring: CupRing = createRing "123456789" false 
    let ringString = ring.ToString ()
    Assert.Equal (ringString,"CupRing[1 2 3 4 5 6 7 8 9")
    
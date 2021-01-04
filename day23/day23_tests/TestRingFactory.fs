module day23_tests.TestRingFactory

open Xunit
open day23.BaseTypes
open day23.CupRing
open day23.RingFactory

let CUP_0 : Cup = 0u
let CUP_9 : Cup = 9u 

[<Fact>]
let testCharToCup () =
    Assert.Equal (CUP_0, charToCup '0')
    Assert.Equal (CUP_9, charToCup '9')
    
[<Fact>]
let testCreateShortRing () =
    let ring: CupRing = createRing "123456789" false 
    let ringString = ring.ToString ()
    Assert.Equal ("CupRing(1 2 3 4 5 6 7 8 9)", ringString)

[<Fact>]
let testCreateExtendedRing () =
    let ring: CupRing = createRing "123456789" true
    let ringString = ring.ToString ()
    Assert.Equal ("CupRing(1 2 3 4 5 6 7 8 9 10 11 12 13 14 15...999999 1000000)", ringString)

module day23_tests.TestCupRing

open Xunit
open day23.BaseTypes
open day23.CupRing
open day23.RingFactory

let CUP_0 : Cup = 0u
let CUP_2 : Cup = 2u
let CUP_9 : Cup = 9u 

[<Fact>]
let testShiftShortRing () =
    let ring: CupRing = createRing "123456789" false
    let ring = ring.shift ()
    let ringString = ring.ToString ()
    Assert.Equal ("CupRing(2 3 4 5 6 7 8 9 1)", ringString)

[<Fact>]
let testShiftExtendedRing () =
    let ring: CupRing = createRing "123456789" true
    let ring = ring.shift ()
    let ringString = ring.ToString ()
    Assert.Equal ("CupRing(2 3 4 5 6 7 8 9 10 11 12 13 14 15 16...1000000 1)", ringString)

[<Fact>]
let testReadFirst() =
    let ring: CupRing = createRing "123456789" true
    let ring = ring.shift ()
    let firstCup = ring.firstCup()
    Assert.Equal (firstCup,CUP_2)

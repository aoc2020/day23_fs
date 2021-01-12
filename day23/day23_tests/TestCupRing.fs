module day23_tests.TestCupRing

open Xunit
open day23.BaseTypes
open day23.CupRing
open day23.RingFactory

let CUP_0 : Cup = 0u
let CUP_1 : Cup = 1u
let CUP_2 : Cup = 2u
let CUP_3 : Cup = 3u
let CUP_4 : Cup = 4u
let CUP_5 : Cup = 5u 
let CUP_9 : Cup = 9u
let CUP_1M : Cup = 1_000_000u 

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
    let firstCup = (ring.startCups ()).First
    Assert.Equal (firstCup,CUP_2)

[<Fact>]
let testReadGroup() =
    let ring: CupRing = createRing "123456789" true
    let ring = ring.shift ()
    let group = (ring.startCups()).group ()
    Assert.Equal (group,[CUP_3;CUP_4;CUP_5])

[<Fact>]
let testNextCup() =
    let ring: CupRing = createRing "432567891" true
    printfn "testNextCup: start cups: %A" (ring.startCups ())
    let next = (ring.startCups()).TargetCup
    Assert.Equal (CUP_1,next)

[<Fact>]
let testNextCupRot () =
    let ring: CupRing = createRing "231456798" true 
    let next = (ring.startCups()).TargetCup
    Assert.Equal (CUP_1M,next)

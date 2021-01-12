module day23_tests.TestMappedCupRing

open FsCheck.Arb
open Xunit
open day23.DefaultCupRing
open day23.MappedCupRing
open day23.RingFactory 

let CUP_1 = 1u
let CUP_2 = 2u
let CUP_3 = 3u

let POS_1 = 1UL
let POS_2 = 2UL
let POS_3 = 3UL 
let POS_4 = 4UL 

let ring1to9 () : IMappedRing = DirectMappedRing (DefaultCupRing([||],9UL)) :> IMappedRing 

[<Fact>]
let testDirectMappedElements () =
    let ring: DefaultCupRing = DefaultCupRing([||],3UL)
    let ring: IMappedRing = DirectMappedRing(ring) :> IMappedRing
    Assert.Equal (ring.findCup POS_1, CUP_1)
    Assert.Equal (ring.findCup POS_3, CUP_3)
   
[<Fact>] 
let testDirectShift () =
    let ring: DefaultCupRing = DefaultCupRing([||],3UL)
    let ring: IMappedRing = DirectMappedRing(ring) :> IMappedRing
    let ring = ring.shift() 
    Assert.Equal (ring.First, POS_2)
    Assert.Equal (ring.Last, POS_4)
    Assert.Equal (ring.findCup POS_2, CUP_2)
    Assert.Equal (ring.findCup POS_4, CUP_1)

[<Fact>]
let testDirectUpdate() =
    let ring: DefaultCupRing = DefaultCupRing([||],3UL)
    let ring: IMappedRing = DirectMappedRing(ring) :> IMappedRing
    let ring = ring.shift() 
    Assert.Equal (ring.First, POS_2)
    Assert.Equal (ring.Last, POS_4)
    Assert.Equal (ring.findCup POS_2, CUP_2)
    Assert.Equal (ring.findCup POS_4, CUP_1)
   
[<Fact>] 
let testEquals_Self () =
    let ring = createRing "123456789" false
    Assert.Equal (ring,ring)

[<Fact>] 
let testEquals_Same () =
    let ring1 = ( createRing "123456789" false ).Ring 
    let ring2 = ( createRing "123456789" false ).Ring 
    Assert.Equal (ring1,ring2)

[<Fact>] 
let testNotEqual () =
    let ring1 = ( createRing "1234" false ).Ring 
    let ring2 = ( createRing "4321" false ).Ring 
    Assert.NotEqual (ring1,ring2)
    
[<Fact>] 
let testShifted () =
    let ring = ( createRing "123456789" false ).Ring 
    let ring2 = ( createRing "912345678" false ).Ring 
    let ring2 = ring2.shift ()
    Assert.Equal (ring,ring2)
    
[<Fact>] 
let testFlattenFlat () =
    let ring: DefaultCupRing = DefaultCupRing([||],9UL)
    let ring: IMappedRing = DirectMappedRing(ring) :> IMappedRing 
    let flat = flattenRing ring
    Assert.Equal (ring,flat)

[<Fact>]    
let testMoveValuesLeft () =
    let ring = createRing "123456789" false
    let ring = ring.Ring
    let ring = ring.moveRangeLeft (CupRange(3UL,5UL)) 1UL
    let expected = createRing "134556789" false
    // moved/duplicate values are undefined
    Assert.Equal (expected.Ring.ToString (),ring.ToString ())

[<Fact>]    
let testMoveValuesPaddedLeft () =
    let ring = createRing "123456789" true 
    let ring = ring.Ring
    let ring = ring.moveRangeLeft (CupRange(14UL,16UL)) 12UL
    let expected = "DefaultMappedRing(1 14 15 16 5 6 7 8 9 10 11 12 13 14 15...999999 1000000)"
    Assert.Equal (expected,ring.ToString())

[<Fact>]
let testMoveValuesRight () =
    let ring = createRing "123456789" false
    let ring = ring.Ring
    let ring = ring.moveRangeRight (CupRange(3UL,5UL)) 2UL
    let expected = createRing "123434589" false
    // moved/duplicate values are undefined
    Assert.Equal (expected.Ring.ToString (),ring.ToString ())

[<Fact>]    
let testMoveValuesPaddedRight () =
    let ring = createRing "123456789" true 
    let ring = ring.Ring
    let ring = ring.moveRangeRight (CupRange(2UL,3UL)) 999996UL
    let expected = "DefaultMappedRing(1 2 3 4 5 6 7 8 9 10 11 12 13 14 15...3 1000000)"
    Assert.Equal (expected,ring.ToString())

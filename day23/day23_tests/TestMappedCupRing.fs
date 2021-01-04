module day23_tests.TestMappedCupRing

open Xunit
open day23
open day23.BaseTypes
open day23.DefaultCupRing
open day23.MappedCupRing

let CUP_1 = 1u
let CUP_2 = 2u
let CUP_3 = 3u

let POS_1 = 1UL
let POS_2 = 2UL
let POS_3 = 3UL 
let POS_4 = 4UL 

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
    
 
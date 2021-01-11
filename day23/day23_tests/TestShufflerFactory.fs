module day23_tests.TestShufflerFactory

open Xunit
open day23.MappedCupRing
open day23.RingFactory
open day23.ShufflerFactory

let testShuffleLast =
    let ring = createRing "123456789" false
    let ring : IMappedRing = ring.Ring
    let shuffle: Shuffler = chooseStrategy ring.Range ring.Last 9UL
    let newRing = shuffle ring 9UL
    Assert.Equal (newRing,ring) 
       




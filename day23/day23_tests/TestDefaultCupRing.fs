module day23_tests.TestCupRing

open Xunit
open day23.DefaultCupRing

let CUP1 = 10u
let CUP2 = 20u
let CUP3 = 30u


[<Fact>]
let test123() = 
    let cr = DefaultCupRing([|CUP1;CUP2;CUP3|],3UL)
    Assert.True <| (cr.First = 1UL)
    Assert.True <| (cr.Last = 3UL)
    Assert.Equal (cr.findCup 1UL, CUP1)
    Assert.Equal (cr.findCup 2UL, CUP2)
    Assert.Equal (cr.findCup 3UL, CUP3)
    Assert.Equal (cr.findPos CUP1, 1UL)
    Assert.Equal (cr.findPos CUP2, 2UL)
    Assert.Equal (cr.findPos CUP3, 3UL)
    
[<Fact>]
let test123Default() =
    let cr = DefaultCupRing([||],3UL) 
    Assert.True <| (cr.First = 1UL)
    Assert.True <| (cr.Last = 3UL)
    Assert.Equal (cr.findCup 1UL, 1u)
    Assert.Equal (cr.findCup 2UL, 2u)
    Assert.Equal (cr.findCup 3UL, 3u)
    Assert.Equal (cr.findPos 1u, 1UL)
    Assert.Equal (cr.findPos 2u, 2UL)
    Assert.Equal (cr.findPos 3u, 3UL)

[<Fact>]
let testRotateWithDefaults () = 
    let cr = DefaultCupRing([||],3UL)
    let cr = cr.shift ()
    Assert.True <| (cr.First = 2UL)
    Assert.True <| (cr.Last = 4UL)
    Assert.Equal (cr.findCup 2UL, 2u)
    Assert.Equal (cr.findCup 3UL, 3u)
    Assert.Equal (cr.findCup 4UL, 1u)
    Assert.Equal (cr.findPos 2u, 2UL)
    Assert.Equal (cr.findPos 3u, 3UL)
    Assert.Equal (cr.findPos 1u, 4UL)
    

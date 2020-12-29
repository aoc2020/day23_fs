module day23_tests.TestCupMap

open Xunit
open FsCheck
open day23.BaseTypes
open day23.CupMap

let FIRST_CUP : Cup = 7u
let MIDDLE_CUP : Cup = 13u
let LAST_CUP : Cup = 25u
let BEFORE_CUP: Cup = 2u
let AFTER_CUP: Cup = 100u

[<Fact>]
let testValuesCorrect () =
    let init = [|FIRST_CUP;MIDDLE_CUP;LAST_CUP|]
    let first = 7UL
    let middle = 8UL
    let last = 9UL 
    let cupMap:CupMap = CupMap(init,first)
    Assert.Equal((cupMap.getCup first),FIRST_CUP)
    Assert.Equal((cupMap.getCup middle),MIDDLE_CUP)
    Assert.Equal((cupMap.getCup last),LAST_CUP)
    Assert.Equal((cupMap.getPos FIRST_CUP),first)
    Assert.Equal((cupMap.getPos MIDDLE_CUP),middle)
    Assert.Equal((cupMap.getPos LAST_CUP),last)

[<Fact>]
let testMissingValues () =
    let init = [|FIRST_CUP;MIDDLE_CUP;LAST_CUP|]
    let cupMap:CupMap = CupMap(init,42UL)
    Assert.False <| cupMap.containsCup BEFORE_CUP
    Assert.False <| cupMap.containsCup AFTER_CUP
    Assert.False <| cupMap.containsPos 1UL
    Assert.False <| cupMap.containsPos 200UL 
    cupMap.containsCup 

[<Fact>]
let testMove () =
    let init = [|FIRST_CUP;MIDDLE_CUP;LAST_CUP|]
    let first = 7UL
    let middle = 8UL
    let last = 9UL
    let after = 10UL 
    let cupMap:CupMap = CupMap(init,first)
    let cupMap = cupMap.Move FIRST_CUP after 
    Assert.False <| cupMap.containsPos first 
    Assert.True <| cupMap.containsPos middle  
    Assert.True <| cupMap.containsPos after
    Assert.False <| cupMap.containsPos 1UL
    Assert.False <| cupMap.containsPos 200UL 
    Assert.True <| cupMap.containsCup  FIRST_CUP
    Assert.True <| cupMap.containsCup  MIDDLE_CUP
    Assert.Equal (cupMap.getCup middle, MIDDLE_CUP)
    Assert.Equal (cupMap.getCup after, FIRST_CUP)
    Assert.Equal (cupMap.getPos FIRST_CUP, after)
    Assert.Equal (cupMap.getPos MIDDLE_CUP, middle)
    

     
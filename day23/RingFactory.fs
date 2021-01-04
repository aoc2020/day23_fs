module day23.RingFactory

open System
open day23.CupRing
open day23.DefaultCupRing
open day23.MappedCupRing

let charToCup (c:char) =
    let inputChar = c |> int
    let _0 = '0' |> int
    let cupAsInt = inputChar - _0
    cupAsInt |> uint32  

let createRing (input:String) (extended:bool) : CupRing =
    let cups = input.ToCharArray() |> Array.map charToCup 
    let lastPos = if extended then 1_000_000UL else 9UL 
    let defaultRing = DefaultCupRing(cups,lastPos)
    let mappedRing = DirectMappedRing(defaultRing)
    CupRing(mappedRing)
    

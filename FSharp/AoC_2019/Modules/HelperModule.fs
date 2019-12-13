namespace AoC_2019.Modules

open System.Collections.Generic
open System.Numerics

[<AutoOpen>]
module HelperModule =
    type DirectionEnum = UP | DOWN | LEFT | RIGHT

    let bigint (x:int) = bigint(x)

    let createPanel(size: int, color: int) =
        let panel = new Dictionary<(int * int), int>()
        for idx in [0 .. size] do
            for jdx in [0 .. size] do
                panel.Add((jdx, idx), color)      
        panel
    
    let printPanel(panel: Dictionary<(int*int),int>, width: int, height: int) =
        for idx in [0 .. height] do
            for jdx in [0 .. width] do
                let color = panel.[(jdx, idx)]
                match color with
                | 1 -> 
                    printf "#"
                    ()
                | _ -> 
                    printf " "
                    ()
            printfn ""



    let getNextPosition(direction: DirectionEnum, position:int[]) =
        match direction with
        | UP -> (direction, [|position.[0]; position.[1] - 1|])
        | DOWN -> (direction, [|position.[0]; position.[1] + 1|])
        | LEFT -> (direction, [|position.[0] - 1; position.[1]|])
        | RIGHT -> (direction, [|position.[0] + 1; position.[1]|])


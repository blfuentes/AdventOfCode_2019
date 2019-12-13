namespace AoC_2019.Modules

open System.Collections.Generic
open System.Numerics

[<AutoOpen>]
module HelperModule =
    type DirectionEnum = UP | DOWN | LEFT | RIGHT

    let bigint (x:int) = bigint(x)

    let createPanel(size: int) =
        let panel = new Dictionary<(int * int), int>()
        for idx in [0 .. size] do
            for jdx in [0 .. size] do
                panel.Add((jdx, idx), 0)      
        panel
    
    let getNextPosition(direction: DirectionEnum, position:int[]) =
        match direction with
        | UP -> (direction, [|position.[0]; position.[1] - 1|])
        | DOWN -> (direction, [|position.[0]; position.[1] + 1|])
        | LEFT -> (direction, [|position.[0] - 1; position.[1]|])
        | RIGHT -> (direction, [|position.[0] + 1; position.[1]|])


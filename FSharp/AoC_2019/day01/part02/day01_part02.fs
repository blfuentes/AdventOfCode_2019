module day01_part02

open System.IO

let filepath = __SOURCE_DIRECTORY__ + @"../../day01_input.txt"
let lines = File.ReadLines(filepath)

let rec getFuel (massInput:int) =
    match massInput with
    | greater when greater > 5 -> (massInput / 3 - 2) + getFuel ((massInput / 3 - 2))
    | finish -> 0

let displayValue =
    lines
        |> Seq.map (fun mass -> getFuel(int mass))
        |> Seq.sum 
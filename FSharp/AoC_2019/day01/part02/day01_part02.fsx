open System.IO

let filepath = __SOURCE_DIRECTORY__ + @"../../day01_input.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input.txt"
let lines = File.ReadLines(filepath)

let rec getFuel (massInput:int) =
    match massInput with
    | greater when greater > 5 -> (massInput / 3 - 2) + getFuel ((massInput / 3 - 2))
    | _ -> 0

// detailed
//let rec getFuel (massInput:int) =
//    match massInput with
//    | _contiue when _contiue > 5 -> 
//        let result = (massInput / 3 - 2)
//        result + getFuel(result)
//    | _ -> 0

let displayValue =
    lines
        |> Seq.map (fun mass -> getFuel(int mass))
        |> Seq.sum 



displayValue

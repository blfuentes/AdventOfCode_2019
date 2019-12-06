open System.IO

//let filepath = __SOURCE_DIRECTORY__ + @"../../day06_input.txt"
let filepath = __SOURCE_DIRECTORY__ + @"../../test_input.txt"
let values = File.ReadAllLines(filepath)
                |> Array.map (fun x -> (x.Split(')').[0], x.Split(')').[1]))

type Orbit =
    | Parent of string * Orbit list
    | Child of string
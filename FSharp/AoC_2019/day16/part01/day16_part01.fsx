open System.IO

let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_00.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../day16_input.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../day16_input.txt"

let input = File.ReadAllText(filepath).ToCharArray() |> Array.map (fun x -> System.Convert.ToInt32((string)x))

let convertInput(pattern:int[], position: int, inputLength: int) =
    let replicated = 

let calculateInput(input:int[], pattern:int[]) =
    0
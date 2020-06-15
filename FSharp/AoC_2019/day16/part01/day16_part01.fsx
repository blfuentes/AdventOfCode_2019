open System.IO

let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_00.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../day16_input.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../day16_input.txt"

let input = File.ReadAllText(filepath).ToCharArray() |> Array.map (string >> int) 

let generatePattern(length: int, position: int, basePattern: int[]) =
    let tmpPattern = seq { for x in [| 0 .. length - 1|] do yield basePattern.[x % basePattern.Length] } |> Seq.toArray
    tmpPattern

let calculateInput(input:int[], position: int, pattern:int[]) =
    let compPattern = generatePattern(input.Length, position, pattern)
    let tmpResult = Array.map2 (*) input compPattern |> Array.sum |> fun x -> x % 10
    tmpResult

let test = calculateInput([|9;8;7;6;5|], 0, [|1;2;3|]) 

let convertInput(pattern:int[], position: int, inputLength: int) =
    // let replicated = 
    0


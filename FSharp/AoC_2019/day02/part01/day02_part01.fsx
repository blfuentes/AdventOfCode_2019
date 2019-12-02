open System.IO

//let filepath = __SOURCE_DIRECTORY__ + @"../../day02_input.txt"
let filepath = __SOURCE_DIRECTORY__ + @"../../test_input.txt"
let values = File.ReadAllText(filepath).Split(',')
                |> Array.map int


let displayValue(input: int[]) =
    printfn "Tranche %A" input

let performOperation(input: int[]) =
    match input.[0] with
    | 1 -> Array.set values input.[3] (values.[input.[1]] + values.[input.[2]])
    | 2 -> Array.set values input.[3] (values.[input.[1]] * values.[input.[2]])
    | 99 -> input
    | _ -> input

let execute =
    values |> for 
        //|> seq
        //|> Seq.chunkBySize 4
        //|> Seq.takeWhile (fun arr -> arr.[0] <> 99)
        //|> Seq.map (fun arr -> displayValue arr)        
        //|> Seq.map (fun arr -> performOperation arr)

performOperation

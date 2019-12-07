namespace AoC_2019.Modules

[<AutoOpen>]
module IntcodeComputerModule = 

    let getInput(path: string) =
        System.IO.File.ReadAllText(path).Split(',') |> Array.map int

    let performOperation(values: int array, input:int, idx: int, opDef: string array, currentOutput: int) =
        let op = int(opDef.[4]) + int(opDef.[3]) * 10
        let param1Mode = int opDef.[2]
        let param2Mode = int opDef.[1]
        //let param3Mode = int opDef.[0]

        match op with
        | 1 -> // ADD
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)
            Array.set values values.[idx + 3] (fst parameters + snd parameters)
            (true, [|idx + 4; currentOutput|])
        | 2 -> // MULTIPLY
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)
            Array.set values values.[idx + 3] (fst parameters * snd parameters)
            (true, [|idx + 4; currentOutput|])
        | 3 -> // WRITE INPUT
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[idx + 1], 0)
                | (_, _) -> (0, 0)
            Array.set values (fst parameters) input
            (true, [|idx + 2; currentOutput|])
        | 4 -> // OUTPUT
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], 0)
                | (_, _) -> (0, 0)
            (true, [|idx + 2; fst parameters|])
        | 5 -> // JUMP IF TRUE
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)

            match fst parameters with
            | 0 -> (true, [|idx + 3; currentOutput|])
            | _ -> (true, [|(snd parameters); currentOutput|]) //((snd parameters) - idx, true)
        | 6 -> // JUMP IF FALSE
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)

            match fst parameters with
            | 0 -> (true, [|(snd parameters); currentOutput|])//((snd parameters) - idx, true)
            | _ -> (true, [|idx + 3; currentOutput|]) 
        | 7 -> // LESS THAN
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)

            if (fst parameters < snd parameters) then Array.set values values.[idx + 3] 1
            else Array.set values values.[idx + 3] 0
            (true, [|idx + 4; currentOutput|]) 
        | 8 -> // EQUALS
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)

            if (fst parameters = snd parameters) then Array.set values values.[idx + 3] 1
            else Array.set values values.[idx + 3] 0
            (true, [|idx + 4; currentOutput|]) 
        | 99 -> (false, [|idx + 3; currentOutput|]) // (lastoutput, false)
        | _ -> (true, [|idx; currentOutput|]) // (0, true)


    let rec getOutput(values: int array, input:int, idx: int, currentOutput:int) =
        let opDefinition = values.[idx].ToString().PadLeft(5, '0') |> Seq.toArray |> Array.map string 
        let resultOp = performOperation(values, input, idx, opDefinition, currentOutput)

        match resultOp with
        | (true, result) -> getOutput(values, input, result.[0], result.[1])
        | (false, result) -> result.[1]

    let execute(path:string, input:int) =
        let values = getInput path
        let idx = 0

        getOutput(values, input, idx, 0)       
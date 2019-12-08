namespace AoC_2019.Modules

[<AutoOpen>]
module IntcodeComputerModule = 
    let mutable availableInputs = 2
    let getInput(path: string) =
        System.IO.File.ReadAllText(path).Split(',') |> Array.map int

    let performOperation(values: int array, phase:int, input:int, idx: int, opDef: string array, currentOutput: int) =
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
            ((false, true), [|idx + 4; currentOutput; input|])
        | 2 -> // MULTIPLY
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)
            Array.set values values.[idx + 3] (fst parameters * snd parameters)
            ((false, true), [|idx + 4; currentOutput; input|])
        | 3 -> // WRITE INPUT
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[idx + 1], 0)
                | (_, _) -> (0, 0)
            

            if availableInputs > 0 then
                //printfn "VM index %d - Opcode%d used input %d" idx 3 phase
                availableInputs <- availableInputs - 1
                Array.set values (fst parameters) phase
                ((false, true), [|idx + 2; currentOutput; input|])
            else
                ((true, true), [|idx; currentOutput; input|])
        | 4 -> // OUTPUT
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], 0)
                | (_, _) -> (0, 0)
            //printfn "VM index %d - Opcode%d outputs %d" idx 4 (fst parameters)
            ((false, true), [|idx + 2; fst parameters; input|])
        | 5 -> // JUMP IF TRUE
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)

            match fst parameters with
            | 0 -> ((false, true), [|idx + 3; currentOutput; input|])
            | _ -> ((false, true), [|(snd parameters); currentOutput; input|]) //((snd parameters) - idx, true)
        | 6 -> // JUMP IF FALSE
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (_, _) -> (0, 0)

            match fst parameters with
            | 0 -> ((false, true), [|(snd parameters); currentOutput; input|])//((snd parameters) - idx, true)
            | _ -> ( (false, true), [|idx + 3; currentOutput; input|]) 
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
            ((false, true), [|idx + 4; currentOutput; input|]) 
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
            ((false, true), [|idx + 4; currentOutput; input|]) 
        | 99 -> ((false, false), [|idx; currentOutput; input|]) // (lastoutput, false)
        | _ -> ((false, true), [|idx; currentOutput; input|]) // (0, true)


    let rec getOutput(values: int array, phase:int, input:int, idx: int, currentOutput:int) =
        let opDefinition = values.[idx].ToString().PadLeft(5, '0') |> Seq.toArray |> Array.map string 
        availableInputs <- 2
        let resultOp = performOperation(values, phase, input, idx, opDefinition, currentOutput)
        match resultOp with
        | ((_, true), result) -> getOutput(values, result.[2], input, result.[0], result.[1])
        | ((_, false), result) -> result.[1]

    let rec getOutputPhaseLoopMode(values: int array, phase:int, input:int, idx: int, currentOutput:int) =
        let opDefinition = values.[idx].ToString().PadLeft(5, '0') |> Seq.toArray |> Array.map string 
        let resultOp = performOperation(values, phase, input, idx, opDefinition, currentOutput)

        match resultOp with
        | ((false, false), result) -> (result.[1], (result.[0], false)) // no pausa + no continuar
        | ((false, true), result) -> getOutputPhaseLoopMode(values, result.[2], input, result.[0], result.[1]) // no pausa + continue
        | ((true, false), result) -> (result.[1], (result.[0], true)) // pausa + no continuar
        | ((true, true), result) -> (result.[1], (result.[0], true)) // pausa + continuar

    let execute(path:string, input:int) =
        let values = getInput path
        let idx = 0

        getOutput(values, input, input, idx, 0)

    let executeWithPhase(path: string, phase:int, input:int) =
        let values = getInput path
        let idx = 0
        getOutput(values, phase, input, idx, 0)  

    let executeWithPhaseLoopMode(values, phase: int, idx:int, input:int, numberOfInputs: int) =    
        availableInputs <- numberOfInputs
        getOutputPhaseLoopMode(values, phase, input,idx, 0)  

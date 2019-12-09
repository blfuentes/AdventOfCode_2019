namespace AoC_2019.Modules

open System.Collections.Generic
open System.Numerics

[<AutoOpen>]
module IntcodeComputerModule = 
    let mutable availableInputs = 2
    let mutable relativeBase = 0I
    let mutable relativeIntBase = 0
    let mutable initialInputSize = 0
    let getInput(path: string) =
        System.IO.File.ReadAllText(path).Split(',') |> Array.map int

    let getInputBigData(path: string) =
        System.IO.File.ReadAllText(path).Split(',') |> Array.map (fun x -> BigInteger.Parse(x))

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
                | (0, 2) -> (values.[values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (1, 2) -> (values.[idx + 1], values.[relativeIntBase + values.[idx + 2]])
                | (2, 0) -> (values.[relativeIntBase + values.[idx + 1]], values.[values.[idx + 2]])
                | (2, 1) -> (values.[relativeIntBase + values.[idx + 1]], values.[idx + 2])
                | (2, 2) -> (values.[relativeIntBase + values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (_, _) -> (0, 0)
            Array.set values values.[idx + 3] (fst parameters + snd parameters)
            ((false, true), [|idx + 4; currentOutput; input|])
        | 2 -> // MULTIPLY
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (0, 2) -> (values.[values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (1, 2) -> (values.[idx + 1], values.[relativeIntBase + values.[idx + 2]])
                | (2, 0) -> (values.[relativeIntBase + values.[idx + 1]], values.[values.[idx + 2]])
                | (2, 1) -> (values.[relativeIntBase + values.[idx + 1]], values.[idx + 2])
                | (2, 2) -> (values.[relativeIntBase + values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
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
                | (0, 2) -> (values.[values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (1, 2) -> (values.[idx + 1], values.[relativeIntBase + values.[idx + 2]])
                | (2, 0) -> (values.[relativeIntBase + values.[idx + 1]], values.[values.[idx + 2]])
                | (2, 1) -> (values.[relativeIntBase + values.[idx + 1]], values.[idx + 2])
                | (2, 2) -> (values.[relativeIntBase + values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (_, _) -> (0, 0)

            match fst parameters with
            | 0 -> ((false, true), [|idx + 3; currentOutput; input|])
            | _ -> ((false, true), [|(snd parameters); currentOutput; input|])
        | 6 -> // JUMP IF FALSE
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (0, 2) -> (values.[values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (1, 2) -> (values.[idx + 1], values.[relativeIntBase + values.[idx + 2]])
                | (2, 0) -> (values.[relativeIntBase + values.[idx + 1]], values.[values.[idx + 2]])
                | (2, 1) -> (values.[relativeIntBase + values.[idx + 1]], values.[idx + 2])
                | (2, 2) -> (values.[relativeIntBase + values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (_, _) -> (0, 0)

            match fst parameters with
            | 0 -> ((false, true), [|(snd parameters); currentOutput; input|])
            | _ -> ( (false, true), [|idx + 3; currentOutput; input|]) 
        | 7 -> // LESS THAN
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (0, 2) -> (values.[values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (1, 2) -> (values.[idx + 1], values.[relativeIntBase + values.[idx + 2]])
                | (2, 0) -> (values.[relativeIntBase + values.[idx + 1]], values.[values.[idx + 2]])
                | (2, 1) -> (values.[relativeIntBase + values.[idx + 1]], values.[idx + 2])
                | (2, 2) -> (values.[relativeIntBase + values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (_, _) -> (0, 0)

            if (fst parameters < snd parameters) then Array.set values values.[idx + 3] 1
            else Array.set values values.[idx + 3] 0
            ((false, true), [|idx + 4; currentOutput; input|]) 
        | 8 -> // EQUALS
            let parameters = 
                match (param1Mode, param2Mode) with
                | (0, 0) -> (values.[values.[idx + 1]], values.[values.[idx + 2]])
                | (0, 1) -> (values.[values.[idx + 1]], values.[idx + 2])
                | (0, 2) -> (values.[values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (1, 0) -> (values.[idx + 1], values.[values.[idx + 2]])
                | (1, 1) -> (values.[idx + 1], values.[idx + 2])
                | (1, 2) -> (values.[idx + 1], values.[relativeIntBase + values.[idx + 2]])
                | (2, 0) -> (values.[relativeIntBase + values.[idx + 1]], values.[values.[idx + 2]])
                | (2, 1) -> (values.[relativeIntBase + values.[idx + 1]], values.[idx + 2])
                | (2, 2) -> (values.[relativeIntBase + values.[idx + 1]], values.[relativeIntBase + values.[idx + 2]])
                | (_, _) -> (0, 0)

            if (fst parameters = snd parameters) then Array.set values values.[idx + 3] 1
            else Array.set values values.[idx + 3] 0
            ((false, true), [|idx + 4; currentOutput; input|]) 
        | 9 -> // ADJUST RELATIVE BASE
            let parameters = 
                match param1Mode with
                | 0 -> values.[relativeIntBase + values.[idx + 1]]
                | 1 -> values.[values.[idx + 1]]
                | 2 -> values.[idx]
                | _ -> 0
            relativeIntBase <- relativeIntBase + parameters
            ((false, true), [|idx + 2; currentOutput; input|]) 
        | 99 -> ((false, false), [|idx; currentOutput; input|]) // (lastoutput, false)
        | _ -> ((false, true), [|idx; currentOutput; input|]) // (0, true)

    let getValueByIdx(inputSize: int, values: Dictionary<bigint, bigint>, idx: bigint) =
        let found, value = values.TryGetValue idx
        match found with
        | true -> value
        | false -> 
            values.Add(idx, 0I)
            0I
    
    let setValueByIdx(values: Dictionary<bigint, bigint>, idx: bigint, newValue: bigint) =
        let found, value = values.TryGetValue idx
        match found with
        | true -> values.[idx] <- newValue
        | false -> values.Add(idx, newValue)

    let getOperatorValue(values: Dictionary<bigint, bigint>, idx: bigint, mode: int) =
        match mode with
        | 0 -> getValueByIdx(initialInputSize, values, getValueByIdx(initialInputSize, values, idx))
        | 1 -> getValueByIdx(initialInputSize, values, idx)
        | 2 -> getValueByIdx(initialInputSize, values, relativeBase + getValueByIdx(initialInputSize, values, idx))
        | _ -> 0I

    let getOperatorAddress(values: Dictionary<bigint, bigint>, idx: bigint, mode: int) =
        match mode with
        | 0 -> getValueByIdx(initialInputSize, values, idx)
        | 1 -> idx
        | 2 -> relativeBase + getValueByIdx(initialInputSize, values, idx)
        | _ -> 0I

    let performOperationBigData(values: Dictionary<bigint, bigint>, phase:bigint, input:bigint, idx: bigint, opDef: string array, currentOutput: bigint) =
        let op = int(opDef.[4]) + int(opDef.[3]) * 10
        let param1Mode = int opDef.[2]
        let param2Mode = int opDef.[1]
        let param3Mode = int opDef.[0]

        match op with
        | 1 -> // ADD
            let operator1 = getOperatorValue(values, idx + 1I, param1Mode)
            let operator2 = getOperatorValue(values, idx + 2I, param2Mode)
            let writeAddress = getOperatorAddress(values, idx + 3I, param3Mode)

            setValueByIdx(values, writeAddress, operator1 + operator2)
            //printfn "opcode= %d op1= %A op2= %A op3= %A idx= %A" op operator1 operator2 writeAddress idx
            ((false, true), [|idx + 4I; currentOutput; input|])
        | 2 -> // MULTIPLY
            let operator1 = getOperatorValue(values, idx + 1I, param1Mode)
            let operator2 = getOperatorValue(values, idx + 2I, param2Mode)
            let writeAddress = getOperatorAddress(values, idx + 3I, param3Mode)

            setValueByIdx(values, writeAddress, operator1 * operator2)
            //printfn "opcode= %d op1= %A op2= %A op3= %A idx= %A" op operator1 operator2 writeAddress idx

            ((false, true), [|idx + 4I; currentOutput; input|])
        | 3 -> // WRITE INPUT
            let writeAddress = getOperatorAddress(values, idx + 1I, param1Mode)
            if availableInputs > 0 then
                //printfn "VM index %A - Opcode %A used input %A" idx 3 phase
                availableInputs <- availableInputs - 1
                setValueByIdx(values, writeAddress, phase)
                ((false, true), [|idx + 2I; currentOutput; input|])
            else
                ((true, true), [|idx; currentOutput; input|])
        | 4 -> // OUTPUT
            let output = getOperatorValue(values, idx + 1I, param1Mode)
            //printfn "OUTPUT-->opcode= %A op1= %A idx= %A" op output idx
            ((false, true), [|idx + 2I; output; input|])
        | 5 -> // JUMP IF TRUE
            let operator1 = getOperatorValue(values, idx + 1I, param1Mode)
            let operator2 = getOperatorValue(values, idx + 2I, param2Mode)
            //printfn "opcode= %A op1= %A op2= %A idx= %A" op operator1 operator2 idx
            match int(operator1) with
            | 0 -> ((false, true), [|idx + 3I; currentOutput; input|])
            | _ -> ((false, true), [|operator2; currentOutput; input|])
        | 6 -> // JUMP IF FALSE
            let operator1 = getOperatorValue(values, idx + 1I, param1Mode)
            let operator2 = getOperatorValue(values, idx + 2I, param2Mode)
            //printfn "opcode= %A op1= %A op2= %A idx= %A" op operator1 operator2 idx
            match int(operator1) with
            | 0 -> ((false, true), [|operator2; currentOutput; input|])
            | _ -> ( (false, true), [|idx + 3I; currentOutput; input|]) 
        | 7 -> // LESS THAN
            let operator1 = getOperatorValue(values, idx + 1I, param1Mode)
            let operator2 = getOperatorValue(values, idx + 2I, param2Mode)
            let writeAddress = getOperatorAddress(values, idx + 3I, param3Mode)
            //printfn "opcode= %A op1= %A op2= %A op3= %A idx= %A" op operator1 operator2 writeAddress idx
            if (operator1 < operator2) then setValueByIdx(values, writeAddress, 1I)
            else setValueByIdx(values, writeAddress, 0I)
            ((false, true), [|idx + 4I; currentOutput; input|]) 
        | 8 -> // EQUALS
            let operator1 = getOperatorValue(values, idx + 1I, param1Mode)
            let operator2 = getOperatorValue(values, idx + 2I, param2Mode)
            let writeAddress = getOperatorAddress(values, idx + 3I, param3Mode)
            //printfn "opcode= %A op1= %A op2= %A op3= %A idx= %A" op operator1 operator2 writeAddress idx
            if (operator1 = operator2) then setValueByIdx(values, writeAddress, 1I)
            else setValueByIdx(values, writeAddress, 0I)
            ((false, true), [|idx + 4I; currentOutput; input|]) 
        | 9 -> // ADJUST RELATIVE BASE
            let operator1 = getOperatorValue(values, idx + 1I, param1Mode)
            //printfn "RELATIVE-->opcode= %A relative base= %A idx= %A" op relativeBase idx
            relativeBase <- relativeBase + operator1
            //printfn "RELATIVE-->opcode= %A relative base= %A idx= %A" op relativeBase idx
            ((false, true), [|idx + 2I; currentOutput; input|]) 
        | 99 -> ((false, false), [|idx; currentOutput; input|]) // (lastoutput, false)
        | _ -> ((false, true), [|idx; currentOutput; input|]) // (0, true)


    let rec getOutput(values: int array, phase:int, input:int, idx: int, currentOutput:int) =
        let opDefinition = values.[idx].ToString().PadLeft(5, '0') |> Seq.toArray |> Array.map string 
        availableInputs <- 2
        let resultOp = performOperation(values, phase, input, idx, opDefinition, currentOutput)
        match resultOp with
        | ((_, true), result) -> getOutput(values, result.[2], input, result.[0], result.[1])
        | ((_, false), result) -> result.[1]

    let rec getOutputBigData(values: Dictionary<bigint, bigint>, phase:bigint, input:bigint, idx: bigint, currentOutput:bigint) =
        let opDefinition = values.[idx].ToString().PadLeft(5, '0') |> Seq.toArray |> Array.map string 
        availableInputs <- 2
        let resultOp = performOperationBigData(values, phase, input, idx, opDefinition, currentOutput)
        match resultOp with
        | ((_, true), result) -> getOutputBigData(values, result.[2], input, result.[0], result.[1])
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

    let executeBigData(path:string, input:bigint) =
        let values = getInputBigData path 
        let dataContainer = new Dictionary<bigint, bigint>()
        for idx in [|0..values.Length - 1|] do
            dataContainer.Add(bigint idx, values.[idx])

        let idx = 0I
        relativeBase <- 0I
        initialInputSize <- values.Length
        getOutputBigData(dataContainer, input, input, idx, 0I)

    let executeWithPhaseLoopMode(values, phase: int, idx:int, input:int, numberOfInputs: int) =    
        availableInputs <- numberOfInputs
        getOutputPhaseLoopMode(values, phase, input,idx, 0)  

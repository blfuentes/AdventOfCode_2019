open System.IO
open System.Collections.Generic

#load @"../../Modules/IntcodeComputerModule.fs"
open AoC_2019.Modules

let filepath = __SOURCE_DIRECTORY__ + @"../../day11_input.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_01.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_02.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_03.txt"

let values = IntcodeComputerModule.getInputBigData filepath
let (result0, result1) = (executeBigData(filepath, 0I), executeBigData(filepath, 1I))

let rec getNextPaintPosition(values: Dictionary<bigint, bigint>, idx:bigint, input:bigint, paintedPositions: int) =
    let paintingResult =  executeWithPhaseLoopMode(values, input, idx, input, 1I)
    let paintingOutput = (fst paintingResult)
    let paintingInstructionIdx = (fst (snd paintingResult))
    let paintingNotFinished = (snd (snd paintingResult))

    let movementResult =  executeWithPhaseLoopMode(values, input, idx, input, 1I)
    let movementOutput = (fst movementResult)
    let movementInstruction = (fst (snd movementResult))
    let movementNotFinished = (snd (snd movementResult))

    match movementNotFinished with
    | true -> getNextPaintPosition(values, input, idx, paintedPositions + 1 )
    | false -> paintedPositions

let execute =
    let initialPoint=[|60;60|]
    let values = IntcodeComputerModule.getInputBigData filepath
    let paintedPositions = getNextPaintPosition(values, 0I, 0I, 0)
    paintedPositions
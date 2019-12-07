open System.IO

#load @"C:\Users\insan\source\repos\AdventOfCode_2019\FSharp\AoC_2019\Modules\IntcodeComputerModule.fs"
open AoC_2019.Modules

//let filepath = __SOURCE_DIRECTORY__ + @"../../day07_input.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../../day05/day05_input.txt"
let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_01.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_02.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_03.txt"

let values = IntcodeComputerModule.getInput filepath
let result1 = IntcodeComputerModule.execute (filepath, 1)
let result2 = IntcodeComputerModule.execute (filepath, 5)

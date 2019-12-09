open System.IO

//#load @"C:\Users\insan\source\repos\AdventOfCode_2019\FSharp\AoC_2019\Modules\IntcodeComputerModule.fs"
#load "C:\dev\AdventOfCode_2019\FSharp\AoC_2019\Modules\IntcodeComputerModule.fs"
open AoC_2019.Modules

let filepath = __SOURCE_DIRECTORY__ + @"../../day09_input.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../../day05/day05_input.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_01.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_02.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_03.txt"

let values = IntcodeComputerModule.getInput filepath

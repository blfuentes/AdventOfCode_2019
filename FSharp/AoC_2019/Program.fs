// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    // DAY 01
    let resultday01Part1 = day01_part01.displayValue
    printfn "Final result Day 01 part 1: %i" resultday01Part1
    let resultday02Part2 = day01_part02.displayValue
    printfn "Final result Day 01 part 2: %i" resultday02Part2

    // DAY 02
    let resultday02Part1 = day02_part01.execute
    printfn "Final result Day 02 part 1: %i" resultday02Part1
    let resultday02Part2 = day02_part02.execute
    printfn "Final result Day 02 part 2: %i" resultday02Part2

    //
    let endprogram = Console.ReadLine()
    0 // return an integer exit code

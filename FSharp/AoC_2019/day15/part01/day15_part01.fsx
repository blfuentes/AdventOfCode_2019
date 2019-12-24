open System.IO
open System.Collections.Generic

#load @"../../Modules/IntcodeComputerModule.fs"
open AoC_2019.Modules

type StatusType = WALL | WAY | OXYGEN | NONE
type MovementType = NORTH | SOUTH | WEST | EAST | NONE

let getStatus(status: bigint) =
    match int(status) with
    | 0 -> StatusType.WALL
    | 1 -> StatusType.WAY
    | 2 -> StatusType.OXYGEN
    | _ -> StatusType.NONE

let getMovement(movement: bigint) =
    match int(movement) with
    | 0 -> MovementType.NORTH
    | 1 -> MovementType.SOUTH
    | 2 -> MovementType.WEST
    | 3 -> MovementType.EAST
    | _ -> MovementType.NONE

let containsElement(theSeq: seq<int[]>, element: int[]) =
    let found = theSeq |> Seq.tryFind (fun x -> x.[0] = element.[0] && x.[1] = element.[1])
    found <> None

let getPoint(currPoint: int[], mov: MovementType) =
    let newPoint = 
        match mov with
        | NORTH -> [|currPoint.[0]; currPoint.[1] - 1|]
        | SOUTH -> [|currPoint.[0]; currPoint.[1] + 1|]
        | EAST -> [|currPoint.[0] + 1; currPoint.[1]|]
        | WEST -> [|currPoint.[0] - 1; currPoint.[1]|]
        | _ -> currPoint
    newPoint

let finBSFPath(values: Dictionary<bigint, bigint>, relativeBase: bigint, movInput:bigint, idx:bigint, numberOfInputs: bigint) = 
    let startPoint = [|0; 0|]
    let resultPath = new Queue<int[]>()
    let visitedPoints: List<int[]> = new List<int[]>()
    resultPath.Enqueue(startPoint)
    let currentPoint = [|0; 0|]
    let paramInputs = [|relativeBase; idx; numberOfInputs|]
    let mutable continueLooping = true
    while resultPath.Count > 0 && continueLooping do
        let movInputs = [| 0I; 1I; 2I; 3I |]
        let tmpPoint = resultPath.Dequeue()
        Array.set currentPoint 0 tmpPoint.[0]
        Array.set currentPoint 1 tmpPoint.[1]
        if not(containsElement(visitedPoints, currentPoint)) then    
            for mov in movInputs do
                let (output, (idx, notfinished), relativeBase)  =  executeBigDataWithMemory(values, paramInputs.[0], paramInputs.[1], mov, paramInputs.[2])
                match getStatus(output) with
                | WALL -> 
                    printfn "Found Wall at %A - %A" idx (getMovement(mov))
                    ()
                | WAY -> 
                    Array.set paramInputs 0 relativeBase
                    Array.set paramInputs 1 idx
                    Array.set paramInputs 2 numberOfInputs
                    printfn "Found WAY at %A - %A" idx (getMovement(mov))
                    let visitedPoint = getPoint(currentPoint, getMovement(mov))
                    visitedPoints.Add(visitedPoint)
                    resultPath.Enqueue(visitedPoint)
                | OXYGEN -> 
                    printfn "Found Oxygen at %A - %A" idx (getMovement(mov))
                    continueLooping <- false
                    ()
                | _ ->
                    ()

        else
            ()

let execute =
    let filepath = __SOURCE_DIRECTORY__ + @"../../day15_input.txt"
    let alloutputs = new List<bigint>()
    let values = IntcodeComputerModule.getInputBigData filepath
    
    let relBase = 0I
    let movInput = 0I
    let idx = 0I
    let numInputs = 1I

    finBSFPath(values, relBase, movInput, idx, numInputs)

    //let (score, nextMovInput, toContinue) = round(values, relBase, movInput, idx, numInputs)
    //values.[0I] <- 2I
    //let valuesArray = [|relBase; movInput|]
    //let mutable continueLooping = false
    //let mutable finalScore = 0
    //while continueLooping do
    //    let (score, nextInput, numberOfBlocks) = finBSFPath(values, relBase, movInput, idx, numInputs)
    //    continueLooping <- numberOfBlocks
    //    finalScore <- score
    //    Array.set valuesArray 1 nextInput

    //finalScore

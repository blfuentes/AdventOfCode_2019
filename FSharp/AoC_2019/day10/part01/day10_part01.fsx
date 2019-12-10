open System.IO
open System.Collections.Generic

//let filepath = __SOURCE_DIRECTORY__ + @"../../day10_input.txt"
let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_01.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_02.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_03.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_04.txt"
//let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_05.txt"

let values = File.ReadAllLines(filepath)
                |> Array.map (fun line -> line.ToCharArray())

let width = values.[0].Length - 1
let height = values.Length - 1

let asteroidsCollection =
    seq {
        for idx in [|0..height|] do
            for jdx in [|0 .. width|] do
                match values.[idx].[jdx] with
                    | '#' -> yield [|jdx; idx|]
                    | _  -> ()
    }

let pointInRange(initPoint:int[], endPoint:int[], checkPoint:int[]) =
    let topLeftPoint =
        match (initPoint.[0] <= endPoint.[0] && initPoint.[1] <= endPoint.[1]) with
        | true -> [|initPoint.[0]; initPoint.[1]|]
        | false -> [|endPoint.[0]; endPoint.[1]|]

    let bottomRightPoint =
        match (initPoint.[0] <= endPoint.[0] && initPoint.[1] <= endPoint.[1]) with
        | false -> [|initPoint.[0]; initPoint.[1]|]
        | true -> [|endPoint.[0]; endPoint.[1]|]

    topLeftPoint.[0] <= checkPoint.[0] && checkPoint.[0] <= bottomRightPoint.[0] && topLeftPoint.[1] <= checkPoint.[1] && checkPoint.[1] <= bottomRightPoint.[1]

let notEqual(a:int[], b:int[]) =
    a.[0] <> b.[0] || a.[1] <> b.[1]

let findPossibleBlockers(asteroids:seq<int[]>, initPoint:int[], endPoint:int[]) = 
    //asteroids |> Seq.filter (fun _a -> pointInRange(initPoint, endPoint, _a) && notEqual(_a, initPoint) && notEqual(_a, endPoint))
    asteroids |> Seq.filter (fun _a -> pointInRange(initPoint, endPoint, _a) && notEqual(_a, initPoint) && notEqual(_a, endPoint))
    //asteroidsCollection |> Seq.filter (fun _a -> pointInRange(initPoint, endPoint, _a))

//let isBlocked(initPoint:int[], endPoint:int[], midPoint:int[]): bool =
//    let collinearity = initPoint.[0] * (midPoint.[1] - endPoint.[1]) + midPoint.[0] * (endPoint.[1] - initPoint.[1]) + endPoint.[0] * (initPoint.[1] - midPoint.[1])
//    collinearity = 0
let isBlocked(initPoint:int[], endPoint:int[], midPoint:int[]): bool =
    (midPoint.[1] - initPoint.[1]) * (endPoint.[0] - initPoint.[0]) =  (midPoint.[0] - initPoint.[0]) * (endPoint.[1] - initPoint.[1])

let numberOfPoints = asteroidsCollection |> Seq.toList |> List.length

let pointsWithCollisions = 
    seq {
        for initIdx in [|0 .. numberOfPoints - 1|] do
            for endIdx in [|0 .. numberOfPoints - 1|] do
                match (initIdx = endIdx) with
                | false -> ()
                | true ->
                    let initPoint = asteroidsCollection |> Seq.item(initIdx)
                    let endPoint = asteroidsCollection |> Seq.item(endIdx)
                    let blockers = findPossibleBlockers(asteroidsCollection, initPoint, endPoint)
                    let valid = blockers |> Seq.filter (fun midPoint -> not (isBlocked(initPoint, endPoint, midPoint))) |> Seq.toList
                    yield (initPoint, valid.Length)
    }
  

let calculate = 
    let filepath = __SOURCE_DIRECTORY__ + @"../../day10_input.txt"
    //let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_01.txt"
    let values = File.ReadAllLines(filepath)|> Array.map (fun line -> line.ToCharArray())

    let width = values.[0].Length - 1
    let height = values.Length - 1

    let asteroids =
        seq {
            for idx in [|0..height|] do
                for jdx in [|0 .. width|] do
                    match values.[idx].[jdx] with
                        | '#' -> yield [|jdx; idx|]
                        | _  -> ()
        }
    let numberOfPoints = asteroids |> Seq.toList |> List.length

    let pointsDictionary = new Dictionary<(int*int), int>()
    for initIdx in [|0 .. numberOfPoints - 1|] do
        let endIdxs = [|0 .. numberOfPoints - 1|] |> Array.filter (fun x -> x <> initIdx)
        for endIdx in endIdxs do
            let initPoint = asteroids |> Seq.item(initIdx)
            let endPoint = asteroids |> Seq.item(endIdx)
            let blockers = findPossibleBlockers(asteroids, initPoint, endPoint)
            let walls = blockers |> Seq.filter (fun midPoint -> isBlocked(initPoint, endPoint, midPoint))
            let notvalid = blockers |> Seq.exists (fun midPoint -> isBlocked(initPoint, endPoint, midPoint))
            let addValue =
                match notvalid with
                | true -> 0
                | false -> 1
            let found, content = pointsDictionary.TryGetValue ((initPoint.[0], initPoint.[1]))
            match found with 
            | true -> pointsDictionary.[(initPoint.[0], initPoint.[1])] <- content + addValue
            | false -> pointsDictionary.Add((initPoint.[0], initPoint.[1]), addValue)
    let converted =
        pointsDictionary
        |> Seq.map (fun (KeyValue(k,v)) -> (k, v))
    converted |> Seq.iter (fun elem -> printfn "%A - %d" (fst elem) (snd elem))
    converted |> Seq.maxBy (fun x -> snd x)
 

//
findPossibleBlockers(asteroidsCollection |> Seq.item(4), asteroidsCollection |> Seq.item(9))
findPossibleBlockers(asteroidsCollection |> Seq.item(9), asteroidsCollection |> Seq.item(4))
let bloquers = findPossibleBlockers(asteroidsCollection |> Seq.item(0), asteroidsCollection |> Seq.item(36))
isBlocked(asteroidsCollection |> Seq.item(0), asteroidsCollection |> Seq.item(8), asteroidsCollection |> Seq.item(4))
isBlocked(asteroidsCollection |> Seq.item(0), asteroidsCollection |> Seq.item(8), asteroidsCollection |> Seq.item(4))
isBlocked(asteroidsCollection |> Seq.item(8), asteroidsCollection |> Seq.item(0), asteroidsCollection |> Seq.item(4))
isBlocked(asteroidsCollection |> Seq.item(1), asteroidsCollection |> Seq.item(9), asteroidsCollection |> Seq.item(6))
isBlocked(asteroidsCollection |> Seq.item(1), asteroidsCollection |> Seq.item(9), asteroidsCollection |> Seq.item(7))
isBlocked(asteroidsCollection |> Seq.item(1), asteroidsCollection |> Seq.item(9), asteroidsCollection |> Seq.item(5))
asteroidsCollection |> Seq.item(5)
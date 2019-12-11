namespace AoC_2019.Modules

open System.Collections.Generic
open System.Numerics

[<AutoOpen>]
module AsteroidStationModule =
    let pointInRange(initPoint:int[], endPoint:int[], checkPoint:int[]) =
        let (minX, maxX) = 
            match initPoint.[0] <= endPoint.[0] with
            | true -> (initPoint.[0], endPoint.[0])
            | false -> (endPoint.[0], initPoint.[0])
        let (minY, maxY) = 
            match initPoint.[1] <= endPoint.[1] with
            | true -> (initPoint.[1], endPoint.[1])
            | false -> (endPoint.[1], initPoint.[1])
    
        minX <= checkPoint.[0] && checkPoint.[0] <= maxX && minY <= checkPoint.[1] && checkPoint.[1] <= maxY
    
    let notEqual(a:int[], b:int[]) =
        a.[0] <> b.[0] || a.[1] <> b.[1]
    
    let findPossibleBlockers(asteroids:seq<int[]>, initPoint:int[], endPoint:int[]) = 
        asteroids |> Seq.filter (fun _a -> pointInRange(initPoint, endPoint, _a) && notEqual(_a, initPoint) && notEqual(_a, endPoint))
    
    let getAngleBetweenPoints(initPoint:int[], endPoint:int[]) =
        let deltaY = float(endPoint.[1] - initPoint.[1])
        let deltaX = float(endPoint.[0] - initPoint.[0])
        System.Math.Atan2(deltaY, deltaX) * 180.0  / System.Math.PI
    
    let isBlocked(initPoint:int[], endPoint:int[], midPoint:int[]): bool =
        //getAngleBetweenPoints(initPoint, endPoint) = getAngleBetweenPoints(initPoint, midPoint)
        (midPoint.[1] - initPoint.[1]) * (endPoint.[0] - initPoint.[0]) =  (midPoint.[0] - initPoint.[0]) * (endPoint.[1] - initPoint.[1])
import { Coordinate } from "../Coordinate";

let fs = require("fs");
let path = require('path');

// read and parse input
// let filepath = path.join(__dirname, "../test_input_03.txt");
let filepath = path.join(__dirname, "../day03_input.txt");

let text:string = fs.readFileSync(filepath, "utf-8");

let wireTableA: Array<Coordinate> = [];
let wireTableB: Array<Coordinate> = [];

// U: (0, 1) D: (0, -1) L: (-1, 0) R: (1, 0)
let xDirection = [0, 0, -1, 1];
let yDirection = [1, -1, 0, 0];

function getPoints(wire:string) {
    let points: Array<Array<number>> = [];
    let coordinates: Array<Coordinate> = [];
    let xPos = 0;
    let yPos = 0;
    let pathLength = 0;
    wire.split(",").forEach(_step => {
        let operation = _step.charAt(0);
        let movement = parseInt(_step.replace(operation, ""));
        let opIdx = 0;

        switch(operation) {
            case "U":
                opIdx = 0;
                break;
            case "D":
                opIdx = 1;
                break;
            case "L":
                opIdx = 2;
                break;
            case "R":
                opIdx = 3;
                break;
        }
        for (let idx = 0; idx < movement; idx++) {
            xPos += xDirection[opIdx];
            yPos += yDirection[opIdx];
            pathLength++;
            let tmpElement = coordinates.find(_c => _c.posX == xPos && _c.posY == yPos);
            if (tmpElement == undefined) {
                tmpElement = new Coordinate(xPos, yPos);
                tmpElement.path = pathLength;
                coordinates.push(tmpElement);
            }
        }
    });

    return coordinates;
}

wireTableA = getPoints(text.split("\r\n")[0]);
wireTableB = getPoints(text.split("\r\n")[1]);

function intersection(arrayOne:Array<Coordinate>, arrayTwo:Array<Coordinate>){
    let matches: Array<Coordinate> = [];
    arrayOne.forEach(function (ax){
        arrayTwo.forEach(function (bx){
            if (ax.posX===bx.posX && ax.posY === bx.posY){ // NOTE: make sure you use STRICT EQUAL
                matches.push(ax);
            }
        });
    });

    return matches;
}

let commonPoints = intersection(wireTableA, wireTableB);

let closestPoint = commonPoints.reduce(function (prev, curr) {
    return prev.distance < curr.distance ? prev : curr;
});

function returnPathSum(checkPoint: Coordinate) {
    let _a = wireTableA.find(_w => _w.posX == checkPoint.posX && _w.posY == checkPoint.posY);
    let _b = wireTableB.find(_w => _w.posX == checkPoint.posX && _w.posY == checkPoint.posY);
    if (_a != undefined &&_b != undefined) {
        return _a.path + _b.path;
    }
    else {
        return 0;
    }
}

let closestPath = commonPoints.reduce(function (prev, curr) {
    return returnPathSum(prev) < returnPathSum(curr) ? prev : curr;
});


console.log(`Result day 3 part 1: ${closestPoint.distance}`);
console.log(`Result day 3 part 2: ${returnPathSum(closestPath)}`);

let fs = require("fs");
let path = require('path');

// read and parse input
// let filepath = path.join(__dirname, "../test_input_01.txt");
let filepath = path.join(__dirname, "../day03_input.txt");

let text:string = fs.readFileSync(filepath, "utf-8");

let wireTableA: Array<Array<number>> = [];
let wireTableB: Array<Array<number>> = [];

function getManhattanDistance(_coord: Array<number>) {
    return Math.abs(_coord[0]) + Math.abs(_coord[1]);
}

// U: (0, 1) D: (0, -1) L: (-1, 0) R: (1, 0)
let xDirection = [0, 0, -1, 1];
let yDirection = [1, -1, 0, 0];

function getPoints(wire:string) {
    let points: Array<Array<number>> = [];
    let xPos = 0;
    let yPos = 0;
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
            if(points.find(_p => _p[0] == xPos && _p[1] == yPos) == undefined){
                points.push([xPos, yPos]);
            }
        }
    });

    return points;
}

wireTableA = getPoints(text.split("\r\n")[0]);
wireTableB = getPoints(text.split("\r\n")[1]);

function intersection(arrayOne:Array<Array<number>>, arrayTwo:Array<Array<number>>){
    let matches: Array<Array<number>> = [];
    arrayOne.forEach(function (ax){
        arrayTwo.forEach(function (bx){
            if (ax[0]===bx[0] && ax[1] === bx[1]){ // NOTE: make sure you use STRICT EQUAL
                matches.push([ax[0], ax[1]]);
            }
        });
    });

    return matches;
}

let commonPoints = intersection(wireTableA, wireTableB);

let lowestDistance = getManhattanDistance(commonPoints.reduce(function (prev, curr) {
    return getManhattanDistance(prev) < getManhattanDistance(curr) ? prev : curr;
}));

console.log(`Result day 3 part 1: ${lowestDistance}`);
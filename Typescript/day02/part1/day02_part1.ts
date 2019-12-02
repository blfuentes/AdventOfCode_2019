let fs = require("fs");
let path = require('path');

let trancheinput: Array<number> = [];
let result: number = 0;

// read and parse input
let filepath = path.join(__dirname, "../day02_input.txt");
// let filepath = path.join(__dirname, "../test_input.txt");

let text:string = fs.readFileSync(filepath, "utf-8");
trancheinput = text.split(",").map(_op => parseInt(_op));

// 
let done: boolean = false;
let idx: number = 0;
let opcode: number;
let op1Idx: number;
let op2Idx: number;
let resultIdx: number;

// 1202 program alarm state
trancheinput[1] = 12;
trancheinput[2] = 2;

while (!done && idx < trancheinput.length) {
    opcode = trancheinput[idx];
    op1Idx = trancheinput[idx + 1];
    op2Idx = trancheinput[idx + 2];
    resultIdx = trancheinput[idx + 3];

    performOperation(opcode, op1Idx, op2Idx, resultIdx);
    //console.log(`First position in loop ${idx} = ${trancheinput[idx]}`);
    idx += 4;
}


function performOperation(_opcode: number, _op1Idx: number, _op2Idx: number, _resultIdx: number) {
    switch (_opcode) {
        case 1:
            trancheinput[_resultIdx] = trancheinput[_op1Idx] + trancheinput[_op2Idx];
            break;
        case 2:
            trancheinput[_resultIdx] = trancheinput[_op1Idx] * trancheinput[_op2Idx]
            break;
        case 99:
            done = true;
            break;
    }
}

console.log(`Result day 2 part 1: ${trancheinput[0]}.`);
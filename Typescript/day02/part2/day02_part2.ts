let fs = require("fs");
let path = require('path');

const __OUTPUT__ = 19690720;

let trancheinput: Array<number> = [];
let result: number = 0;

// read and parse input
let filepath = path.join(__dirname, "../day02_input.txt");
// let filepath = path.join(__dirname, "../test_input.txt");

// 
let done: boolean = false;
let idx: number = 0;
let opcode: number;
let op1Idx: number;
let op2Idx: number;
let resultIdx: number;

for (let noum = 0; noum <= 99 && trancheinput[0] != __OUTPUT__; noum ++) {
    for (let verb = 0; verb <= 99 && trancheinput[0] != __OUTPUT__; verb ++) {
        setInput(noum, verb);
        done = false;
        idx = 0;
        performOperation();
        //console.log(`What is 100 * ${trancheinput[1]} + ${trancheinput[2]}? = ${100 * trancheinput[0] + trancheinput[2]}`);
    }
}

function setInput(_noum: number, _verb: number) {
    let text:string = fs.readFileSync(filepath, "utf-8");
    trancheinput = text.split(",").map(_op => parseInt(_op));
    
    // program alarm state
    trancheinput[1] = _noum;
    trancheinput[2] = _verb;
}

function performOperation() {
    while (!done && idx < trancheinput.length) {
        opcode = trancheinput[idx];
        if (opcode == 99)
            done = true;
        else {
            op1Idx = trancheinput[idx + 1];
            op2Idx = trancheinput[idx + 2];
            resultIdx = trancheinput[idx + 3];
            switch (opcode) {
                case 1:
                    trancheinput[resultIdx] = trancheinput[op1Idx] + trancheinput[op2Idx];
                    break;
                case 2:
                    trancheinput[resultIdx] = trancheinput[op1Idx] * trancheinput[op2Idx]
                    break;
            }
            idx += 4;
        }

    }
}

console.log(`Result day 2 part 2: What is 100 * ${trancheinput[1]} + ${trancheinput[2]}? = ${100 * trancheinput[0] + trancheinput[2]}`);
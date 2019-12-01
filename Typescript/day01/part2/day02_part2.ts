let fs = require("fs");
let path = require('path');

let masses: Array<string> = [];
let result: number = 0;

let filepath = path.join(__dirname, "../day01_input.txt");
// let filepath = path.join(__dirname, "../test_input.txt");
let text = fs.readFileSync(filepath, "utf-8");

masses = text.split("\r\n");

function getFuel(massInput: string) {
    let fuelresult: number = 0;
    let remainingFuel = parseInt(massInput);
    
    while (remainingFuel > 0) {
        remainingFuel = Math.floor(remainingFuel / 3) - 2;
        if (remainingFuel > 0) {
            fuelresult += remainingFuel;
        }        
    }

    return fuelresult;
}

result = masses.map(_mass => getFuel(_mass)).reduce((a, b) => a + b);

console.log(`Result day 1 part 2: ${result}.`);
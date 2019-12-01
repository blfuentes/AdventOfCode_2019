let fs = require("fs");
let path = require('path');

let masses: Array<string> = [];
let result: number = 0;

let filepath = path.join(__dirname, "../day01_input.txt");
let text = fs.readFileSync(filepath, "utf-8");

masses = text.split("\r\n");
result = masses.map(_mass => Math.floor(parseInt(_mass) / 3) - 2).reduce((a, b) => a + b);

console.log(`Result day 1 part 1: ${result}.`);
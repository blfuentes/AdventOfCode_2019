var fs = require("fs");
var path = require('path');
var masses = [];
var result = 0;
var filepath = path.join(__dirname, "../day01_input.txt");
var text = fs.readFileSync(filepath, "utf-8");
masses = text.split("\r\n");
result = masses.map(function (_mass) { return Math.floor(parseInt(_mass) / 3) - 2; }).reduce(function (a, b) { return a + b; });
console.log("Result day 1 part 1: " + result + ".");

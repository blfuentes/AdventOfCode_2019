var fs = require("fs");
var path = require('path');
var masses = [];
var result = 0;
var filepath = path.join(__dirname, "../day01_input.txt");
// let filepath = path.join(__dirname, "../test_input.txt");
var text = fs.readFileSync(filepath, "utf-8");
masses = text.split("\r\n");
function getFuel(massInput) {
    var fuelresult = 0;
    var remainingFuel = parseInt(massInput);
    while (remainingFuel > 0) {
        remainingFuel = Math.floor(remainingFuel / 3) - 2;
        if (remainingFuel > 0) {
            fuelresult += remainingFuel;
        }
    }
    return fuelresult;
}
result = masses.map(function (_mass) { return getFuel(_mass); }).reduce(function (a, b) { return a + b; });
console.log("Result day 1 part 2: " + result + ".");

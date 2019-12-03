export class Coordinate {
    posX: number;
    posY: number;
    path: number;
    distance: number;

    constructor(_x: number, _y: number) {
        this.posX = _x;
        this.posY = _y;
        this.distance = Math.abs(_x) + Math.abs(_y);
    }
}
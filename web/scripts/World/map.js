import {WorldData} from "../data/world_data.js";
import {MapCell} from "./map_cell.js";

export class Map{
    constructor(mapId){
        this.mapId = mapId;
    }
    getMapData(){
        return WorldData.data.maps[this.mapId];
    }
    getMapCell(column, row){
        return new MapCell(this.getMapData().mapCells[column + row * this.getMapData().columns]);
    }
}
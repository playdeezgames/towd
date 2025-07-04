import {WorldData} from "../data/world_data.js";
import {MapType, MapTypes} from "./enums/map_types.js";
import {CharacterType, CharacterTypes} from "./enums/character_types.js";
import {Map} from "./map.js";
import {MapCellTypes} from "./enums/map_cell_types.js";
import {Character} from "./character.js";
import {MapCell} from "./map_cell.js";
const MAP_COLUMNS = 9;
const MAP_ROWS = 9;
export class World{
    static initialize(){
        WorldData.clear();
        let map = World.createMap(MapType.NORMAL, MAP_COLUMNS, MAP_ROWS);
        World.setAvatar(World.createCharacter(CharacterType.N00B, map.getMapCell((MAP_COLUMNS/2)|0, (MAP_ROWS/2)|0)));
    }
    static createMap(mapType, columns, rows){
        let mapId = WorldData.data.maps.length;
        let descriptor = MapTypes[mapType];
        let mapData = {
            mapType: mapType,
            columns: columns,
            rows: rows
        };
        WorldData.data.maps.push(mapData);
        let map= new Map(mapId);
        mapData.mapCells = [...Array(columns*rows).keys()].map(x=>World.createMapCell(descriptor.mapCellType,map,x % columns, x / columns | 0).mapCellId);
        return map;
    }
    static createCharacter(characterType, mapCell){
        let characterId = WorldData.data.characters.length;
        let descriptor = CharacterTypes[characterType];
        let characterData = {
            characterType: characterType,
            mapCellId: mapCell.mapCellId,
            knownMapCells: []
        };
        WorldData.data.characters.push(characterData);
        let character = new Character(characterId);
        character.addKnownMapCell(mapCell);
        descriptor.initialize(character);
        return character;
    }
    static setAvatar(character){
        WorldData.data.avatarId = character?.characterId;
    }
    static createMapCell(mapCellType, map, column, row){
        let mapCellId = WorldData.data.mapCells.length;
        let descriptor = MapCellTypes[mapCellType];
        let mapCellData = {
            mapCellType: mapCellType,
            mapId: map.mapId,
            column: column,
            row: row,
        };
        WorldData.data.mapCells.push(mapCellData);
        let mapCell = new MapCell(mapCellId);
        descriptor.initialize(mapCell);
        return mapCell;
    }

    static save(saveSlot, notify) {
        throw "NOT IMPLEMENTED";
    }

    static getAvatar() {
        throw "NOT IMPLEMENTED";
    }
}

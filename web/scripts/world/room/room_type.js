import RNG from '../../utility/rng.js'
import { TerrainType } from "./terrain_type.js";
export const RoomType = {
    FIELD: "FIELD"
};
Object.freeze(RoomType);
export let RoomTypes = {};
RoomTypes[RoomType.FIELD] = {
    initialize: (room) => {
        let terrain_table = {}
        terrain_table[TerrainType.GRASS]=15;
        terrain_table[TerrainType.PINE]=10;
        terrain_table[TerrainType.ROCK]=5;
        terrain_table[TerrainType.POND]=3;
        for(let column=0;column<room.get_columns();++column){
            for(let row=0;row<room.get_rows();++row){
                let room_cell = room.get_room_cell(column, row);
                room_cell.set_terrain_type(RNG.generate(terrain_table));
            }
        }
    },
    terrain_type_id: TerrainType.GRASS
}
Object.freeze(RoomTypes);
export default RoomTypes;
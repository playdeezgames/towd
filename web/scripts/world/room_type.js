let RoomType = {
    FIELD: "FIELD"
};
Object.freeze(RoomType);
let RoomTypes = {};
RoomTypes[RoomType.FIELD] = {
    initialize: (room) => {
        let terrain_table = {}
        terrain_table[TerrainType.GRASS]=15;
        terrain_table[TerrainType.PINE]=10;
        terrain_table[TerrainType.ROCK]=5;
        for(let column=0;column<room.get_columns();++column){
            for(let row=0;row<room.get_rows();++row){
                let room_cell = room.get_cell(column, row);
                room_cell.set_terrain_type(Utility.generate(terrain_table));
            }
        }
    },
    terrain_type_id: TerrainType.GRASS
}
Object.freeze(RoomTypes);
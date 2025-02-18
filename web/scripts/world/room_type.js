let RoomType = {
    FIELD: "FIELD"
};
Object.freeze(RoomType);
let RoomTypes = {};
RoomTypes[RoomType.FIELD] = {
    initialize: (room) => {},
    terrain_type_id: TerrainType.EMPTY
}
Object.freeze(RoomTypes);
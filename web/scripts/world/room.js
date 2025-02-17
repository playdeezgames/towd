class Room {
    constructor(world_data, room_id){
        this.world_data = world_data;
        this.room_id = room_id;
    }
    get_data() {
        return this.world_data.rooms[this.room_id];
    }
    get_cell(column, row) {
        return new RoomCell(this.world_data, this.room_id, column, row);
    }
    get_rows() {
        return this.get_data().rows;
    }
    get_columns() {
        return this.get_data().columns;
    }
}
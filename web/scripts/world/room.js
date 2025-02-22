class Room {
    constructor(world_data, room_id){
        this.world_data = world_data;
        this.room_id = room_id;
    }
    get_data() {
        return this.world_data.rooms[this.room_id];
    }
    get_room_cell(column, row) {
        if(column < 0 || row < 0 || column >= this.get_columns() || row >= this.get_rows()){
            return null;
        }
        return new RoomCell(this.world_data, this.room_id, column, row);
    }
    get_rows() {
        return this.get_data().rows;
    }
    get_columns() {
        return this.get_data().columns;
    }
    get_room_type(){
        return this.get_data().room_type_id;
    }
    initialize(){
        RoomTypes[this.get_room_type()].initialize(this);
        for(let column = 0; column < this.get_columns();++column){
            for(let row = 0; row < this.get_rows(); ++ row){
                this.get_room_cell(column, row).initialize();
            }
        }
    }
    advance_time(value){
        for(let column = 0; column < this.get_columns(); ++column){
            for(let row = 0; row < this.get_rows(); ++row){
                this.get_room_cell(column, row).advance_time(value);
            }
        }
    }
}
class Character {
    constructor(world_data, character_id){
        this.world_data = world_data;
        this.character_id = character_id;
    }
    get_data() {
        return this.world_data.characters[this.character_id];
    }
    set_room_cell(room_cell) {
        let old_room_cell = this.get_room_cell();
        if(old_room_cell != null) {
            old_room_cell.set_character(null);
            this.get_data().room_cell_id = null;
        }
        if(room_cell != null) {
            this.get_data().room_cell_id = room_cell.get_id();
            room_cell.set_character(this);
        }
    }
    get_room_cell() {
        let room_cell_id = this.get_data().room_cell_id;
        if(room_cell_id==null) {
            return null;
        }
        return new RoomCell(this.world_data, room_cell_id.room_id, room_cell_id.column, room_cell_id.row);
    }
    get_id() {
        return this.character_id;
    }
    get_character_type() {
        return this.get_data().character_type;
    }
    get_room() {
        return this.get_room_cell().get_room();
    }
    move_by(dx, dy) {
        let room_cell = this.get_room_cell();
        this.set_room_cell(null);
        //TODO: this is where you are
        this.set_room_cell(room_cell)
    }
    move_north() {
        this.move_by(0, -1);
    }
    move_south() {
        this.move_by(0, 1);
    }
    move_east() {
        this.move_by(1, 0);
    }
    move_west() {
        this.move_by(-1, 0);
    }
}
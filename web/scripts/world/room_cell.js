class RoomCell {
    constructor(world_data, room_id, column, row) {
        this.world_data = world_data;
        this.room_id = room_id;
        this.column = column;
        this.row = row;
    }
    get_data() {
        return this.world_data.rooms[this.room_id].cells[this.column][this.row];
    }
    get_terrain_type() {
        return this.get_data().terrain_type_id;
    }
    get_column() {
        return this.column;
    }
    get_row() {
        return this.row;
    }
    get_id() {
        return {
            room_id: this.room_id,
            column: this.column,
            row: this.row
        }
    }
    set_character(character) {
        if(character == null) {
            this.get_data().character_id = null;
        } else {
            this.get_data().character_id = character.get_id();
        }
    }
    get_character() {
        let character_id = this.get_data().character_id;
        if(character_id==null) {
            return null;
        }
        return new Character(this.world_data, character_id);
    }
    get_room() {
        return new Room(this.world_data, this.room_id);
    }
    initialize(){
        //TODO: initialize based on terrain
    }
    get_img_url(){
        return TerrainTypes[this.get_data().terrain_type_id].img_url;
    }
}
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
    get_terrain_type_descriptor(){
        return TerrainTypes[this.get_data().terrain_type_id];
    }
    initialize(){
        this.get_terrain_type_descriptor().initialize(this);
    }
    get_img_url(){
        return this.get_terrain_type_descriptor().img_url;
    }
    set_terrain_type(terrain_type_id){
        let character = this.get_character();
        this.world_data.rooms[this.room_id].cells[this.column][this.row] = {
            terrain_type_id: terrain_type_id
        };
        this.initialize();
        this.set_character(character);
    }
    get_name(){
        return this.get_terrain_type_descriptor().name;
    }
    advance_time(value){
        let character = this.get_character();
        if(character != null){
            character.advance_time(value);
        }
        while(value > 0){
            this.get_terrain_type_descriptor().advance_time(this);
            --value;
        }
    }
    set_statistic(statistic_type_id, value){
        let room_cell_data = this.get_data();
        if(room_cell_data.statistics==null){
            room_cell_data.statistics={};
        }
        room_cell_data.statistics[statistic_type_id] = value;
    }
    get_statistic(statistic_type_id){
        let room_cell_data = this.get_data();
        if(room_cell_data.statistics==null){
            return null;
        }
        return room_cell_data.statistics[statistic_type_id];
    }
    change_statistic(statistic_type_id, delta){
        let value = this.get_statistic(statistic_type_id) + delta;
        this.set_statistic(statistic_type_id, value);
        return value;
    }    
    get_details(){
        if(this.get_terrain_type_descriptor().get_details==null){
            return [];
        }else{
            return this.get_terrain_type_descriptor().get_details(this);
        }
    }
}
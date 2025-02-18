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
        return this.get_data().character_type_id;
    }
    get_room() {
        return this.get_room_cell().get_room();
    }
    apply_hunger(amount){
        let satiety = this.get_statistic(StatisticType.SATIETY);
        let health = this.get_statistic(StatisticType.HEALTH);
        let satiety_loss = Math.min(satiety, amount);
        amount -= satiety_loss;
        satiety -= satiety_loss;
        this.set_statistic(StatisticType.SATIETY, satiety);
        let health_loss = Math.min(health, amount);
        health -= health_loss;
        this.set_statistic(StatisticType.HEALTH, health);
    }
    clear_messages() {
        this.get_data().messages = [];
    }
    get_messages() {
        let messages = this.get_data().messages;
        if(messages == null){
            return [];
        }
        return messages;
    }
    add_message(message){
        let messages = this.get_data().messages;
        if(messages == null){
            messages = [];
            this.get_data().messages = messages;
        }
        messages.push(message);
    }
    move_by(dx, dy, direction_name) {
        this.clear_messages();
        let room_cell = this.get_room_cell();
        let room = room_cell.get_room();
        this.set_room_cell(null);
        let column = room_cell.get_column() + dx;
        let row = room_cell.get_row() + dy;
        let next_room_cell = room.get_cell(column, row);
        if(next_room_cell!=null){
            room_cell = next_room_cell;
        }
        this.set_room_cell(room_cell)
        this.add_message(`You move ${direction_name}.`);
        this.apply_hunger(1);
    }
    move_north() {
        this.move_by(0, -1, "north");
    }
    move_south() {
        this.move_by(0, 1, "south");
    }
    move_east() {
        this.move_by(1, 0, "east");
    }
    move_west() {
        this.move_by(-1, 0, "west");
    }
    set_statistic(statistic_type_id, value){
        if(this.get_data().statistics == null){
            this.get_data().statistics = {};
        }
        this.get_data().statistics[statistic_type_id] = value;
    }
    get_statistic(statistic_type_id){
        if(this.get_data().statistics == null){
            return null;
        }
        return this.get_data().statistics[statistic_type_id];
    }
    is_dead(){
        let health = this.get_statistic(StatisticType.HEALTH);
        return health == 0;
    }
    can_forage(){
        let terrain_type_id = this.get_room_cell().get_terrain_type();
        return TerrainTypes[terrain_type_id].do_forage != null;
    }
    forage(){
        this.clear_messages();
        if(this.can_forage()){
            this.add_message("You forage.")
            let terrain_type_id = this.get_room_cell().get_terrain_type();
            TerrainTypes[terrain_type_id].do_forage(this);
        }
    }
    get_inventory(){
        return new Inventory(this.world_data, this.character_id);
    }
    get_img_url(){
        return CharacterTypes[this.get_character_type()].img_url;
    }
    initialize(){
        CharacterTypes[this.get_character_type()].initialize(this);
    }
    get_world(){
        return new World(this.world_data);
    }
}
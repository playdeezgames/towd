const BOARD_COLUMNS = 9;
const BOARD_ROWS = 9;
class World {
    constructor() {
    }
    get_data() {
        return WorldData.data;
    }
    clear() {
        WorldData.data = {};
    }
    create_room(columns, rows, terrain_type) {
        if(this.get_data().rooms == null) {
            this.get_data().rooms = [];
        }
        let room_id = this.get_data().rooms.length;
        let room_data = {
            columns: columns,
            rows: rows,
            cells: []
        };
        while(room_data.cells.length < columns) {
            let column_data = [];
            room_data.cells.push(column_data);
            while(column_data.length < rows) {
                column_data.push({terrain_type: terrain_type})
            }
        }
        this.get_data().rooms.push(room_data);
        return this.get_room(room_id);
    }
    get_room(room_id) {
        return new Room(this.get_data(), room_id);
    }
    get_character(character_id) {
        return new Character(this.get_data(), character_id);
    }
    create_character(character_type, map_cell) {
        if(this.get_data().characters == null) {
            this.get_data().characters = [];
        }
        let character_id = this.get_data().characters.length;
        let character_data = {
            character_type: character_type,
            inventory: {}
        };
        this.get_data().characters.push(character_data);
        let character =  this.get_character(character_id);
        character.set_room_cell(map_cell);
        CharacterTypes[character_type].initialize(character);
        return character
    }
    set_avatar(character) {
        if(character == null) {
            this.get_data().avatar_id = null;        
        } else {
            this.get_data().avatar_id = character.get_id();        
        }
    }
    get_avatar() {
        let avatar_id = this.get_data().avatar_id;
        if(avatar_id == null) {
            return null;
        }
        return this.get_character(avatar_id);
    }
    initialize(){
        this.clear();
        let room = this.create_room(BOARD_COLUMNS, BOARD_ROWS, TerrainType.EMPTY);
        let character = this.create_character(CharacterType.N00B, room.get_cell(Math.floor(BOARD_COLUMNS/2), Math.floor(BOARD_ROWS/2)))
        this.set_avatar(character);
    }
}
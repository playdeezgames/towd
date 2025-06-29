import WorldData from '../data/world_data.js'
import {RoomType, RoomTypes} from './room/room_type.js'
import Room from './room/room.js'
import { CharacterType } from './character/character_type.js';
import Character from './character/character.js';
import Item from './item/item.js';
export const BOARD_COLUMNS = 9;
export const BOARD_ROWS = 9;
export default class World {
    constructor(world_data) {
        if(world_data==null){
            world_data = WorldData.data;
        }
        this.world_data = world_data;
    }
    get_data() {
        return this.world_data;
    }
    clear() {
        for(let key in this.get_data()){
            delete this.get_data()[key];
        }
    }
    create_room(columns, rows, room_type_id) {
        if(this.get_data().rooms == null) {
            this.get_data().rooms = [];
        }
        let room_id = this.get_data().rooms.length;
        let room_data = {
            columns: columns,
            rows: rows,
            cells: [],
            room_type_id: room_type_id
        };
        let terrain_type_id = RoomTypes[room_type_id].terrain_type_id;
        while(room_data.cells.length < columns) {
            let column_data = [];
            room_data.cells.push(column_data);
            while(column_data.length < rows) {
                column_data.push({terrain_type_id: terrain_type_id})
            }
        }
        this.get_data().rooms.push(room_data);
        let room = this.get_room(room_id);
        room.initialize();
        return room
    }
    get_room(room_id) {
        return new Room(this.get_data(), room_id);
    }
    get_character(character_id) {
        return new Character(this.get_data(), character_id);
    }
    create_character(character_type_id, map_cell) {
        if(this.get_data().characters == null) {
            this.get_data().characters = [];
        }
        let character_id = this.get_data().characters.length;
        let character_data = {
            character_type_id: character_type_id,
            inventory: {}
        };
        this.get_data().characters.push(character_data);
        let character =  this.get_character(character_id);
        character.set_room_cell(map_cell);
        character.initialize();
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
        let room = this.create_room(BOARD_COLUMNS, BOARD_ROWS, RoomType.FIELD);
        let character = this.create_character(CharacterType.N00B, room.get_room_cell(Math.floor(BOARD_COLUMNS/2), Math.floor(BOARD_ROWS/2)))
        this.set_avatar(character);
    }
    create_item(item_type_id){
        let world_data = this.get_data();
        if(world_data.items==null){
            world_data.items=[];
        }
        let item_id = this.get_data().items.length;
        let item_data = {
            item_type_id: item_type_id
        };
        if(world_data.item_graveyard!=null && world_data.item_graveyard.length>0) {
            item_id = world_data.item_graveyard.pop();
            this.get_data().items[item_id] = item_data;
        } else {
            this.get_data().items.push(item_data);
        }
        let item =  this.get_item(item_id);
        item.initialize();
        return item
    }
    get_item(item_id){
        return new Item(this.world_data, item_id);
    }
    static get_avatar(){
        let world = new World();
        return world.get_avatar();
    }
    advance_time(value){
        let avatar = this.get_avatar();
        if(avatar==null){
            return;
        }
        let room = avatar.get_room();
        room.advance_time(value);
    }
}
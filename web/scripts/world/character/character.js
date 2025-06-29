import CharacterTypes from "./character_type.js";
import { StatisticType } from "../enums/statistic_type.js";
import RoomCell from "../room/room_cell.js";
import VerbTypes from "../enums/verb_type.js";
import Inventory from "../item/inventory.js";
import World from "../world.js";
export default class Character {
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
        if(satiety_loss>0){
            this.add_message(`- ${satiety_loss} Satiety`);
        }
        satiety -= satiety_loss;
        this.set_statistic(StatisticType.SATIETY, satiety);
        let health_loss = Math.min(health, amount);
        if(health_loss>0){
            this.add_message(`- ${health_loss} Health`);
        }
        health -= health_loss;
        this.set_statistic(StatisticType.HEALTH, health);
    }
    clear_messages() {
        if(this.is_avatar()){
            this.get_data().messages = [];
        }
    }
    get_messages() {
        if(this.is_avatar()){
            let messages = this.get_data().messages;
            if(messages == null){
                return [];
            }
            return messages;
        }else{
            return [];
        }
    }
    add_message(message){
        if(this.is_avatar()){
            let messages = this.get_data().messages;
            if(messages == null){
                messages = [];
                this.get_data().messages = messages;
            }
            messages.push(message);
        }
    }
    is_avatar(){
        let avatar = this.get_world().get_avatar();
        return avatar!=null && avatar.get_id() == this.get_id();
    }
    move_by(dx, dy, direction_name) {
        this.clear_messages();
        let room_cell = this.get_room_cell();
        let room = room_cell.get_room();
        this.set_room_cell(null);
        let column = room_cell.get_column() + dx;
        let row = room_cell.get_row() + dy;
        let next_room_cell = room.get_room_cell(column, row);
        if(next_room_cell!=null){
            room_cell = next_room_cell;
        }
        this.set_room_cell(room_cell)
        this.add_message(`You move ${direction_name}.`);
        this.get_world().advance_time(1);
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
    can_do_verb(verb_type_id) {
        return VerbTypes[verb_type_id].can_perform(this);
    }
    do_verb(verb_type_id){
        VerbTypes[verb_type_id].perform(this);
    }
    get_inventory(){
        return new Inventory(this.world_data, this.character_id);
    }
    get_character_type_descriptor(){
        return CharacterTypes[this.get_character_type()]
    }
    get_img_url(){
        return this.get_character_type_descriptor().img_url;
    }
    initialize(){
        this.get_character_type_descriptor().initialize(this);
    }
    get_world(){
        return new World(this.world_data);
    }
    set_flag(flag_type_id, value){
        let character_data = this.get_data();
        if(character_data.flags==null){
            character_data.flags = {};
        }
        character_data.flags[flag_type_id]=value;
    }
    get_flag(flag_type_id){
        let character_data = this.get_data();
        if(character_data.flags==null || character_data.flags[flag_type_id]==null){
            return false
        }
        return character_data.flags[flag_type_id];
    }
    create_item_of_type(item_type_id){
        let item = this.get_world().create_item(item_type_id);
        this.get_inventory().add_item(item);
        return item;
    }
    get_item_of_type(item_type_id){
        let items = this.get_inventory().get_items_of_type(item_type_id).get_items();
        if(items.length==0){
            return null;
        }
        return items[0];
    }
    remove_item(item){
        this.get_inventory().remove_item(item);
    }
    remove_item_of_type(item_type_id){
        this.remove_item(this.get_item_of_type(item_type_id));
    }
    break_item(item){
        this.add_message(`Yer ${item.get_name()} breaks.`);
        this.remove_item(item);
        item.recycle();
    }
    change_item_durability(item, delta){
        let durability = item.change_statistic(StatisticType.DURABILITY, delta);
        this.add_message(`${delta>0?"+":""}${delta} ${item.get_name()} durability(${durability}).`);
        if(durability<=0){
            this.break_item(item);
        }
    }
    advance_time(value){
        while(value > 0){
            this.get_character_type_descriptor().advance_time(this);
            --value;
        }
    }
}
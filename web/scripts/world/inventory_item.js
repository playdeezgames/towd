class InventoryItem {
    constructor(world_data, character_id, item_type_id){
        this.world_data = world_data;
        this.character_id = character_id;
        this.item_type_id = item_type_id;
    }
    get_quantity(){
        return this.world_data.characters[this.character_id].inventory[this.item_type_id];
    }
    set_quantity(value){
        this.world_data.characters[this.character_id].inventory[this.item_type_id] = value;
    }
    get_name(){
        return ItemTypes[this.item_type_id].name;
    }
}
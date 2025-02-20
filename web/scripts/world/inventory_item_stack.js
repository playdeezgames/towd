class InventoryItemStack {
    constructor(world_data, character_id, item_type_id){
        this.world_data = world_data;
        this.character_id = character_id;
        this.item_type_id = item_type_id;
    }
    get_quantity(){
        let item_ids = this.world_data.characters[this.character_id].inventory[this.item_type_id]
        if(item_ids==null){
            return 0;
        }
        return item_ids.length;
    }
    get_name(){
        return ItemTypes[this.item_type_id].name;
    }
    pop_item(){
        let item_ids = this.world_data.characters[this.character_id].inventory[this.item_type_id]
        if(item_ids==null || item_ids.length == 0){
            return null;
        }
        return new Item(this.world_data, item_ids.pop())
    }
}
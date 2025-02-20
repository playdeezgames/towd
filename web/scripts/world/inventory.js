class Inventory{
    constructor(world_data, character_id){
        this.world_data = world_data;
        this.character_id = character_id;
    }
    get_data() {
        return this.world_data.characters[this.character_id].inventory;
    }
    get_items() {
        let result = [];
        for(let item_type_id in this.get_data()){
            result.push(this.get_items_of_type(item_type_id));
        }
        return result;
    }
    get_items_of_type(item_type_id){
        return new InventoryItem(this.world_data, this.character_id, item_type_id);
    }
    has_items_of_type(item_type_id){
        let quantity = this.get_items_of_type(item_type_id).get_quantity();
        return quantity > 0;
    }
    add_item(item){
        let inventory_data = this.get_data();
        let item_type_id = item.get_item_type();
        if(inventory_data[item_type_id] == null){
            inventory_data[item_type_id] = [];
        }
        inventory_data[item_type_id].push(item.get_id());
    }
    remove_item(item){
        let item_type_id = item.get_item_type();
        this.get_items_of_type(item_type_id).remove_item(item);
    }
}
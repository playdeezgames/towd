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
            result.push(new InventoryItem(this.world_data, this.character_id, item_type_id));
        }
        return result;
    }
    add_items(item_type_id, quantity){
        let inventory_data = this.get_data();
        if(inventory_data[item_type_id] == null){
            inventory_data[item_type_id] = 0;
        }
        inventory_data[item_type_id] += quantity;
    }
}
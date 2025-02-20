class InventoryItem {
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
    get_durability(){
        let result = 0;
        for(let item of this.get_items()){
            result += item.get_statistic(StatisticType.DURABILITY);
        }
        return result;
    }
    get_name(){
        return ItemTypes[this.item_type_id].name;
    }
    pop_item(){
        let item_ids = this.world_data.characters[this.character_id].inventory[this.item_type_id]
        if(item_ids==null || item_ids.length == 0){
            return null;
        }
        return new Item(this.world_data, item_ids.pop());
    }
    get_items(){
        let item_ids = this.world_data.characters[this.character_id].inventory[this.item_type_id]
        let result = [];
        for(let item_id of item_ids){
            result.push(new Item(this.world_data, item_id));
        }
        return result;
    }
    remove_item(item){
        let item_ids = this.world_data.characters[this.character_id].inventory[this.item_type_id]
        const index = item_ids.indexOf(item.get_id());
        if(index>-1){
            item_ids.splice(index, 1);
        }
    }
}
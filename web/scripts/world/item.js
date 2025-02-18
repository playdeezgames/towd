class Item {
    constructor(world_data, item_id){
        this.world_data = world_data;
        this.item_id = item_id;
    }
    get_id(){
        return this.item_id;
    }
    get_data(){
        return this.world_data.items[this.item_id];
    }
    initialize(){

    }
    get_item_type(){
        return this.get_data().item_type_id;
    }
    get_name(){
        return ItemTypes[this.get_item_type()].name
    }
}
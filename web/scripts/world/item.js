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
        ItemTypes[this.get_item_type()].initialize(this);
    }
    get_item_type(){
        return this.get_data().item_type_id;
    }
    get_name(){
        return ItemTypes[this.get_item_type()].name
    }
    recycle(){
        this.world_data[this.item_id] = null;
        if(this.world_data.item_graveyard==null){
            this.world_data.item_graveyard = [];
        }
        this.world_data.item_graveyard.push(this.item_id);
    }
    set_statistic(statistic_type_id, value){
        let item_data = this.get_data();
        if(item_data.statistics==null){
            item_data.statistics={};
        }
        item_data.statistics[statistic_type_id] = value;
    }
    get_statistic(statistic_type_id){
        let item_data = this.get_data();
        if(item_data.statistics==null){
            return null;
        }
        return item_data.statistics[statistic_type_id];
    }
    change_statistic(statistic_type_id, delta){
        let value = this.get_statistic(statistic_type_id) + delta;
        this.set_statistic(statistic_type_id, value);
        return value;
    }
}
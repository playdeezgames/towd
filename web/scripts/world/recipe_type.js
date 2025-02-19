class Recipe {
    constructor(){
        this.inputs= {};
        this.outputs={};
    }
    static create(){
        return new Recipe();
    }
    add_input(item_type_id, quantity){
        this.inputs[item_type_id] = quantity;
        return this;
    }
    add_output(item_type_id, quantity){
        this.outputs[item_type_id] = quantity;
        return this;
    }
    get_name(){
        let first = true;
        let result = "";
        let inputs = this.inputs;
        for(let item_type_id in inputs){
            if(!first){
                result += " + ";
            }
            first = false;
            let quantity = inputs[item_type_id];
            if(quantity>1){
                result+=`${quantity} `;
            }
            result+=ItemTypes[item_type_id].name;
        }
        result += " -> "
        first = true;
        let outputs = this.outputs;
        for(let item_type_id in outputs){
            if(!first){
                result += " + ";
            }
            first = false;
            let quantity = outputs[item_type_id];
            if(quantity>1){
                result+=`${quantity} `;
            }
            result+=ItemTypes[item_type_id].name;
        }
        return result;
    }
    can_craft(character){
        let inputs = this.inputs;
        for(let item_type_id in inputs){
            let quantity = inputs[item_type_id];
            if(character.get_inventory().get_items_of_type(item_type_id).get_quantity() < quantity){
                return false;
            }
        }
        return true;
    }
    craft(character){
        if(!this.can_craft(character)){
            return;
        }
        let inputs = this.inputs;
        for(let item_type_id in inputs){
            let quantity = inputs[item_type_id];
            let items_of_type = character.get_inventory().get_items_of_type(item_type_id);
            while(quantity>0){
                let item = items_of_type.pop_item();
                character.add_message(`-1 ${item.get_name()}`)
                item.recycle();
                --quantity;
            }
        }
        let outputs = this.outputs;
        for(let item_type_id in outputs){
            let quantity = outputs[item_type_id];
            while(quantity>0){
                let item = character.get_world().create_item(item_type_id);
                character.get_inventory().add_item(item);
                character.add_message(`+1 ${item.get_name()}`)
                --quantity;
            }
        }
    }
}
let Recipes = [
    Recipe.create().add_input(ItemType.PLANT_FIBER, 2).add_output(ItemType.TWINE, 1),
    Recipe.create().add_input(ItemType.ROCK, 2).add_output(ItemType.ROCK,1).add_output(ItemType.SHARP_ROCK,1),
    Recipe.create().add_input(ItemType.SHARP_ROCK,1).add_input(ItemType.TWINE,1).add_input(ItemType.STICK,1).add_output(ItemType.HATCHET, 1)
];
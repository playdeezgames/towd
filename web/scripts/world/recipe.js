class Recipe {
    constructor(){
        this.inputs= {};
        this.outputs={};
        this.durability_inputs = {};
    }
    static create(){
        return new Recipe();
    }
    set_input(item_type_id, quantity){
        this.inputs[item_type_id] = quantity ?? 1;
        return this;
    }
    set_durability_input(item_type_id, quantity){
        this.durability_inputs[item_type_id] = quantity ?? 1;
        return this;
    }
    set_output(item_type_id, quantity){
        this.outputs[item_type_id] = quantity ?? 1;
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
        let input_durabilities = this.durability_inputs;
        for(let item_type_id in input_durabilities){
            let durability = input_durabilities[item_type_id];
            if(character.get_inventory().get_items_of_type(item_type_id).get_durability() < durability){
                return false;
            }
        }
        return true;
    }
    craft(character){
        if(!this.can_craft(character)){
            return;
        }
        let quantities = {};
        for(let item_type_id in this.outputs){
            quantities[item_type_id] = this.outputs[item_type_id];
        }
        for(let item_type_id in this.inputs){
            if(quantities[item_type_id] == null){
                quantities[item_type_id] = 0;
            }
            quantities[item_type_id] -= this.inputs[item_type_id];
        }
        for(let item_type_id in quantities){
            let quantity = quantities[item_type_id];
            let items_of_type = character.get_inventory().get_items_of_type(item_type_id);
            while(quantity<0){
                let item = items_of_type.pop_item();
                character.add_message(`-1 ${item.get_name()}`)
                item.recycle();
                ++quantity;
            }
            while(quantity>0){
                let item = character.get_world().create_item(item_type_id);
                character.get_inventory().add_item(item);
                character.add_message(`+1 ${item.get_name()}`)
                --quantity;
            }
        }
        let input_durabilities = this.durability_inputs;
        for(let item_type_id in input_durabilities){
            let input_durability = input_durabilities[item_type_id];
            while(input_durability>0){
                let item = character.get_item_of_type(item_type_id);
                character.change_item_durability(item, -1);
                --input_durability;
            }
        }
    }
}
let Recipes = [
    Recipe.create().
        set_input(ItemType.PLANT_FIBER, 2).
        set_output(ItemType.TWINE, 1),
    Recipe.create().
        set_input(ItemType.ROCK, 1).
        set_input(ItemType.HAMMER, 1).
        set_output(ItemType.HAMMER,1).
        set_durability_input(ItemType.HAMMER, 1).
        set_output(ItemType.SHARP_ROCK,1),
    Recipe.create().
        set_input(ItemType.SHARP_ROCK,1).
        set_input(ItemType.TWINE,1).
        set_input(ItemType.STICK,1).
        set_output(ItemType.HATCHET, 1),
    Recipe.create().
        set_input(ItemType.ROCK,1).
        set_input(ItemType.TWINE,1).
        set_input(ItemType.STICK,1).
        set_output(ItemType.HAMMER, 1),
    Recipe.create().
        set_input(ItemType.HATCHET,1).
        set_output(ItemType.HATCHET,1).
        set_durability_input(ItemType.HATCHET, 1).
        set_input(ItemType.HAMMER,1).
        set_output(ItemType.HAMMER,1).
        set_durability_input(ItemType.HAMMER, 1).
        set_input(ItemType.LOG,1).
        set_output(ItemType.PLANK,4)
];
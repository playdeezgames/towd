class Recipe {
    constructor(){
        this.inputs= {};
        this.outputs={};
        this.durability_inputs = {};
        this.precondition = (character) => true;
        this.predicate = (character) => {};
    }
    static create(){
        return new Recipe();
    }
    set_precondition(precondition){
        this.precondition = precondition;
        return this;
    }
    set_predicate(predicate){
        this.predicate = predicate;
        return this;
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
        if(!this.precondition(character)) {
            return false;
        }
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
        this.predicate(character);
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
        set_input(ItemType.STICK, 1).
        set_input(ItemType.HATCHET, 1).
        set_output(ItemType.HATCHET,1).
        set_durability_input(ItemType.HATCHET, 1).
        set_output(ItemType.SHARP_STICK,1),
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
        set_output(ItemType.PLANK,4),
    Recipe.create().
        set_input(ItemType.ROCK, 8).
        set_input(ItemType.STICK, 8).
        set_output(ItemType.COOKING_FIRE, 1).
        set_precondition((character) => { 
            let terrain_type_id = character.get_room_cell().get_terrain_type();
            return terrain_type_id == TerrainType.GRASS || terrain_type_id == TerrainType.DIRT; 
        }).
        set_predicate((character)=> {
            character.remove_item_of_type(ItemType.COOKING_FIRE);
            character.get_room_cell().set_terrain_type(TerrainType.COOKING_FIRE);
        }),
    Recipe.create().
        set_input(ItemType.LOG, 2).
        set_output(ItemType.CHARCOAL, 1).
        set_precondition((character) => { 
            return character.get_room_cell().get_terrain_type() == TerrainType.COOKING_FIRE; 
        }),
    Recipe.create().
        set_input(ItemType.PLANT_FIBER, 1).
        set_input(ItemType.CLAY, 1).
        set_output(ItemType.UNFIRED_BRICK, 1),
    Recipe.create().
        set_input(ItemType.TWINE, 4).
        set_output(ItemType.FISHING_NET, 1),
    Recipe.create().
        set_input(ItemType.UNFIRED_BRICK, 1).
        set_output(ItemType.BRICK, 1).
        set_precondition((character) => { 
            return character.get_room_cell().get_terrain_type() == TerrainType.COOKING_FIRE; 
        }),
    Recipe.create().
        set_input(ItemType.BLADE, 1).
        set_input(ItemType.TWINE, 1).
        set_input(ItemType.STICK, 1).
        set_output(ItemType.KNIFE, 1),
    Recipe.create().
        set_input(ItemType.SHARP_ROCK, 1).
        set_input(ItemType.HAMMER, 1).
        set_durability_input(ItemType.HAMMER, 1).
        set_output(ItemType.HAMMER, 1).
        set_output(ItemType.BLADE, 1),
    Recipe.create().
        set_input(ItemType.KNIFE, 1).
        set_input(ItemType.RAW_FISH, 1).
        set_durability_input(ItemType.KNIFE, 1).
        set_output(ItemType.KNIFE,  1).
        set_output(ItemType.FISH_GUTS, 1).
        set_output(ItemType.FISH_HEAD, 1).
        set_output(ItemType.RAW_FISH_FILET, 1)
];
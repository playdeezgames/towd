let RecipeType = {
    TWINE: "TWINE"
};
Object.freeze(RecipeType);
let RecipeTypes = {};
RecipeTypes[RecipeType.TWINE] = {
    inputs: {},
    outputs: {}
}
RecipeTypes[RecipeType.TWINE].inputs[ItemType.PLANT_FIBER] = 2;
RecipeTypes[RecipeType.TWINE].outputs[ItemType.TWINE] = 1;
Object.freeze(RecipeTypes);
class Recipe {
    static can_craft(character, recipe_type_id){
        let inputs = RecipeTypes[recipe_type_id].inputs;
        for(let item_type_id in inputs){
            let quantity = inputs[item_type_id];
            if(character.get_inventory().get_items_of_type(item_type_id).get_quantity() < quantity){
                return false;
            }
        }
        return true;
    }
    static get_name(recipe_type_id){
        let first = true;
        let result = "";
        let inputs = RecipeTypes[recipe_type_id].inputs;
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
        let outputs = RecipeTypes[recipe_type_id].outputs;
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
    static craft(character, recipe_type_id){
        if(!Recipe.can_craft(character, recipe_type_id)){
            return;
        }
        let inputs = RecipeTypes[recipe_type_id].inputs;
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
        let outputs = RecipeTypes[recipe_type_id].outputs;
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
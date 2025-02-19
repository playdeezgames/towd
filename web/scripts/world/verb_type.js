let VerbType = {
    FORAGE: "FORAGE",
    CRAFT: "CRAFT"
};
Object.freeze(VerbType);
let VerbTypes = {};
VerbTypes[VerbType.FORAGE] = {
    name: "Forage",
    can_perform: (character) => { 
        let terrain_type_id = character.get_room_cell().get_terrain_type();
        return TerrainTypes[terrain_type_id].do_forage != null;
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.FORAGE)){
            character.add_message("You forage.")
            let terrain_type_id = character.get_room_cell().get_terrain_type();
            TerrainTypes[terrain_type_id].do_forage(character);
        }
    }
};
VerbTypes[VerbType.CRAFT] = {
    name: "Craft",
    can_perform: (character) => { 
        for(let recipe_type_id in RecipeType){
            if(Recipe.can_craft(character, recipe_type_id)){
                return true;
            }
        }
        return false;
    },
    perform: (character) => {
        if(character.can_do_verb(VerbType.CRAFT)){
            character.clear_messages();
            character.set_flag(FlagType.CRAFTING, true);
        }
    }
};
Object.freeze(VerbTypes);
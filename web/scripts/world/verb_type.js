let VerbType = {
    FORAGE: "FORAGE",
    CRAFT: "CRAFT",
    CHOP: "CHOP"
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
        for(let recipe of Recipes){
            if(recipe.can_craft(character)){
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
VerbTypes[VerbType.CHOP] = {
    name: "Chop",
    can_perform: (character) => { 
        if(!character.get_inventory().has_items_of_type(ItemType.HATCHET)){
            return false;
        }
        let terrain_type_id = character.get_room_cell().get_terrain_type();
        return TerrainTypes[terrain_type_id].do_chop != null;
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.CHOP)){
            character.add_message("You chop.")
            let terrain_type_id = character.get_room_cell().get_terrain_type();
            TerrainTypes[terrain_type_id].do_chop(character);
        }
    }
};
Object.freeze(VerbTypes);
let VerbType = {
    FORAGE: "FORAGE",
    CRAFT: "CRAFT",
    CHOP: "CHOP",
    DIG: "DIG",
    EAT: "EAT"
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
VerbTypes[VerbType.DIG] = {
    name: "Dig",
    can_perform: (character) => { 
        if(!character.get_inventory().has_items_of_type(ItemType.SHARP_STICK)){
            return false;
        }
        let terrain_type_id = character.get_room_cell().get_terrain_type();
        return TerrainTypes[terrain_type_id].do_dig != null;
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.DIG)){
            character.add_message("You dig.")
            let terrain_type_id = character.get_room_cell().get_terrain_type();
            TerrainTypes[terrain_type_id].do_dig(character);
        }
    }
};
VerbTypes[VerbType.EAT] = {
    name: "Eat",
    can_perform: (character) => { 
        return character.get_inventory().has_items_of_type(ItemType.COOKED_GRUB);
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.EAT)){
            character.add_message("You eat.")
            let item = character.get_item_of_type(ItemType.COOKED_GRUB);
            character.add_message(`-1 ${item.get_name()}`);
            character.remove_item(item);
            let satiety = character.get_statistic(StatisticType.SATIETY);
            let maximum_satiety = character.get_statistic(StatisticType.MAXIMUM_SATIETY);
            let delta = Math.min(item.get_statistic(StatisticType.SATIETY), maximum_satiety - satiety);
            character.set_statistic(StatisticType.SATIETY, satiety + delta);
            character.add_message(`+${delta} satiety`);
        }
    }
};
Object.freeze(VerbTypes);
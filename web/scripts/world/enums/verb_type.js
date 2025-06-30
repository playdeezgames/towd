import TerrainTypes, {TerrainType} from "../room/terrain_type.js";
import { StatisticType } from "./statistic_type.js";
import Recipes from "../item/recipe.js";
import { ItemType } from "../item/item_type.js";
import { FlagType } from "./flag_type.js";
export const VerbType = {
    FORAGE: "FORAGE",
    CRAFT: "CRAFT",
    CHOP: "CHOP",
    DIG: "DIG",
    EAT: "EAT",
    ADD_FUEL: "ADD_FUEL",
    WAIT: "WAIT",
    FISH: "FISH"
};
Object.freeze(VerbType);
export let VerbTypes = {};
VerbTypes[VerbType.FORAGE] = {
    name: "Forage",
    can_perform: (character) => { 
        let terrain_type_id = character.get_room_cell().get_terrain_type();
        if(TerrainTypes[terrain_type_id].do_forage == null){
            return false;
        }
        return character.get_room_cell().get_statistic(StatisticType.FORAGING) > 0;
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.FORAGE)){
            character.add_message("You forage.")
            character.get_room_cell().change_statistic(StatisticType.FORAGING, -1);
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
        if(TerrainTypes[terrain_type_id].do_chop == null){
            return false;
        }
        return character.get_room_cell().get_statistic(StatisticType.CHOPPING) > 0;
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.CHOP)){
            character.add_message("You chop.")
            character.get_room_cell().change_statistic(StatisticType.CHOPPING, -1);
            let terrain_type_id = character.get_room_cell().get_terrain_type();
            TerrainTypes[terrain_type_id].do_chop(character);
        }
    }
};
VerbTypes[VerbType.FISH] = {
    name: "Fish",
    can_perform: (character) => { 
        if(!character.get_inventory().has_items_of_type(ItemType.FISHING_NET)){
            return false;
        }
        let terrain_type_id = character.get_room_cell().get_terrain_type();
        if(TerrainTypes[terrain_type_id].do_fish == null){
            return false;
        }
        return character.get_room_cell().get_statistic(StatisticType.FISHING) > 0;
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.FISH)){
            character.add_message("You fish.")
            character.get_room_cell().change_statistic(StatisticType.FISHING, -1);
            let terrain_type_id = character.get_room_cell().get_terrain_type();
            TerrainTypes[terrain_type_id].do_fish(character);
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
        if(TerrainTypes[terrain_type_id].do_dig == null){
            return false;
        }
        return character.get_room_cell().get_statistic(StatisticType.DIGGING) > 0;
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.DIG)){
            character.add_message("You dig.")
            character.get_room_cell().change_statistic(StatisticType.DIGGING, -1);
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
VerbTypes[VerbType.ADD_FUEL] = {
    name: "Add Fuel",
    can_perform: (character) => { 
        return character.get_inventory().has_items_of_type(ItemType.LOG) && character.get_room_cell().get_terrain_type() === TerrainType.COOKING_FIRE;
    },
    perform: (character) => {
        character.clear_messages();
        if(character.can_do_verb(VerbType.ADD_FUEL)){
            character.add_message("You add fuel.")
            let item = character.get_item_of_type(ItemType.LOG);
            character.add_message(`-1 ${item.get_name()}`);
            let fuel_delta = item.get_statistic(StatisticType.FUEL);
            character.remove_item(item);
            let fuel = character.get_room_cell().change_statistic(StatisticType.FUEL, fuel_delta);
            character.add_message(`+${fuel_delta} Fuel(${fuel})`);
        }
    }
};
VerbTypes[VerbType.WAIT] = {
    name: "Wait",
    can_perform: (_) => {
        return true;
    },
    perform: (character) => {
        if(character.can_do_verb(VerbType.WAIT)){
            character.clear_messages();
            character.add_message("You wait.");
            character.get_world().advance_time(1);
        }
    }
};
Object.freeze(VerbTypes);
export default VerbTypes;
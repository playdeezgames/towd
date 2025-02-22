let TerrainType = {
    GRASS: "GRASS",
    PINE: "PINE",
    ROCK: "ROCK",
    COOKING_FIRE: "COOKING_FIRE",
    DIRT: "DIRT"
}
Object.freeze(TerrainType);
let TerrainTypes = {};
TerrainTypes[TerrainType.GRASS] = {
    img_url: "assets/images/terrain_type_grass.png",
    name: "Grass",
    do_forage: (character) => {
        let item = character.create_item_of_type(ItemType.PLANT_FIBER);
        character.add_message(`You find 1 ${item.get_name()}.`);
        character.get_world().advance_time(1);
    },
    do_dig: (character) => {
        character.get_world().advance_time(1);
        let item = character.create_item_of_type(ItemType.GRUB);
        character.add_message(`You find 1 ${item.get_name()}.`);
        item = character.get_item_of_type(ItemType.SHARP_STICK);
        character.change_item_durability(item, -1)
    },
    initialize: (room_cell) => {
        room_cell.set_statistic(StatisticType.FORAGING, 25);
        room_cell.set_statistic(StatisticType.DIGGING, 25);
    },
    advance_time: (room_cell) => {
        if(room_cell.get_statistic(StatisticType.FORAGING)<=0 && room_cell.get_statistic(StatisticType.DIGGING)<=0){
            room_cell.set_terrain_type(TerrainType.DIRT);
        }
    },
    get_details: (room_cell) => {
        return [
            `Foraging: ${room_cell.get_statistic(StatisticType.FORAGING)}`,
            `Digging: ${room_cell.get_statistic(StatisticType.DIGGING)}`,
        ];
    }
};
TerrainTypes[TerrainType.PINE] = {
    img_url: "assets/images/terrain_type_pine.png",
    name: "Pine Tree",
    do_forage: (character) => {
        character.get_world().advance_time(1);
        let item = character.create_item_of_type(ItemType.STICK);
        character.add_message(`You find 1 ${item.get_name()}.`);
    },
    do_chop: (character) => {
        character.get_world().advance_time(1);
        let item = character.create_item_of_type(ItemType.LOG);
        character.add_message(`You chop 1 ${item.get_name()}.`);
        item = character.get_item_of_type(ItemType.HATCHET);
        character.change_item_durability(item, -1)
    },
    initialize: (room_cell) => {
        room_cell.set_statistic(StatisticType.FORAGING, 25);
        room_cell.set_statistic(StatisticType.CHOPPING, 25);
    },
    advance_time: (room_cell) => {
        if(room_cell.get_statistic(StatisticType.FORAGING)<=0 && room_cell.get_statistic(StatisticType.CHOPPING)<=0){
            room_cell.set_terrain_type(TerrainType.DIRT);
        }
    },
    get_details: (room_cell) => {
        return [
            `Foraging: ${room_cell.get_statistic(StatisticType.FORAGING)}`,
            `Chopping: ${room_cell.get_statistic(StatisticType.CHOPPING)}`,
        ];
    }
};
TerrainTypes[TerrainType.ROCK] = {
    img_url: "assets/images/terrain_type_rock.png",
    name: "Rock",
    do_forage: (character) => {
        character.get_world().advance_time(1);
        let item = character.create_item_of_type(ItemType.ROCK);
        character.add_message(`You find 1 ${item.get_name()}.`);
    },
    initialize: (room_cell) => {
        room_cell.set_statistic(StatisticType.FORAGING, 25);
    },
    advance_time: (room_cell) => {
        if(room_cell.get_statistic(StatisticType.FORAGING)<=0){
            room_cell.set_terrain_type(TerrainType.DIRT);
        }
    },
    get_details: (room_cell) => {
        return [
            `Foraging: ${room_cell.get_statistic(StatisticType.FORAGING)}`
        ];
    }
};
TerrainTypes[TerrainType.COOKING_FIRE] = {
    img_url: "assets/images/terrain_type_cooking_fire.png",
    name: "Cooking Fire",
    initialize: (room_cell) => {
        room_cell.set_statistic(StatisticType.FUEL, 8);
    },
    advance_time: (room_cell) => {
        if(room_cell.get_statistic(StatisticType.FUEL)>0){
            room_cell.change_statistic(StatisticType.FUEL, -1);
        } 
        if(room_cell.get_statistic(StatisticType.FUEL)<=0){
            room_cell.set_terrain_type(TerrainType.DIRT);
        }       
    },
    get_details: (room_cell) => {
        return [
            `Fuel: ${room_cell.get_statistic(StatisticType.FUEL)}`
        ];
    }
};
TerrainTypes[TerrainType.DIRT] = {
    img_url: "assets/images/terrain_type_dirt.png",
    name: "Dirt",
    initialize: (room_cell) => {

    },
    advance_time: (room_cell) => {

    }
};
Object.freeze(TerrainTypes);
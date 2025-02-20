let TerrainType = {
    GRASS: "GRASS",
    PINE: "PINE",
    ROCK: "ROCK"
}
Object.freeze(TerrainType);
let TerrainTypes = {};
TerrainTypes[TerrainType.GRASS] = {
    img_url: "assets/images/terrain_type_grass.png",
    name: "Grass",
    do_forage: (character) => {
        character.apply_hunger(1);
        let item = character.create_item_of_type(ItemType.PLANT_FIBER);
        character.add_message(`You find 1 ${item.get_name()}.`);
    },
    initialize: (room_cell) => {

    }
};
TerrainTypes[TerrainType.PINE] = {
    img_url: "assets/images/terrain_type_pine.png",
    name: "Pine Tree",
    do_forage: (character) => {
        character.apply_hunger(1);
        let item = character.create_item_of_type(ItemType.STICK);
        character.add_message(`You find 1 ${item.get_name()}.`);
    },
    do_chop: (character) => {
        character.apply_hunger(1);
        let item = character.create_item_of_type(ItemType.LOG);
        character.add_message(`You chop 1 ${item.get_name()}.`);
        item = character.get_item_of_type(ItemType.HATCHET);
        let durability = item.get_statistic(StatisticType.DURABILITY);
        --durability;
        item.set_statistic(StatisticType.DURABILITY, durability);
        character.add_message(`-1 ${item.get_name()} durability(${durability}).`);
        if(durability<=0){
            character.add_message(`Yer ${item.get_name()} breaks.`);
            character.remove_item(item);
            item.recycle();
        }
    },
    initialize: (room_cell) => {

    }
};
TerrainTypes[TerrainType.ROCK] = {
    img_url: "assets/images/terrain_type_rock.png",
    name: "Rock",
    do_forage: (character) => {
        character.apply_hunger(1);
        let item = character.create_item_of_type(ItemType.ROCK);
        character.add_message(`You find 1 ${item.get_name()}.`);
    },
    initialize: (room_cell) => {

    }
};
Object.freeze(TerrainTypes);
let TerrainType = {
    GRASS: "GRASS",
    PINE: "PINE",
    ROCK: "ROCK",
    COOKING_FIRE: "COOKING_FIRE"
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
    do_dig: (character) => {
        character.apply_hunger(1);
        let item = character.create_item_of_type(ItemType.GRUB);
        character.add_message(`You find 1 ${item.get_name()}.`);
        item = character.get_item_of_type(ItemType.SHARP_STICK);
        character.change_item_durability(item, -1)
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
        character.change_item_durability(item, -1)
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
TerrainTypes[TerrainType.COOKING_FIRE] = {
    img_url: "assets/images/terrain_type_cooking_fire.png",
    name: "Cooking Fire",
    initialize: (room_cell) => {

    }
};
Object.freeze(TerrainTypes);
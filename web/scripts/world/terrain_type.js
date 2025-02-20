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
        let item = character.get_world().create_item(ItemType.PLANT_FIBER);
        character.get_inventory().add_item(item);
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
        let item = character.get_world().create_item(ItemType.STICK);
        character.get_inventory().add_item(item);
        character.add_message(`You find 1 ${item.get_name()}.`);
    },
    do_chop: (character) => {
        character.apply_hunger(1);
        let item = character.get_world().create_item(ItemType.LOG);
        character.get_inventory().add_item(item);
        character.add_message(`You chop 1 ${item.get_name()}.`);
    },
    initialize: (room_cell) => {

    }
};
TerrainTypes[TerrainType.ROCK] = {
    img_url: "assets/images/terrain_type_rock.png",
    name: "Rock",
    do_forage: (character) => {
        character.apply_hunger(1);
        let item = character.get_world().create_item(ItemType.ROCK);
        character.get_inventory().add_item(item);
        character.add_message(`You find 1 ${item.get_name()}.`);
    },
    initialize: (room_cell) => {

    }
};
Object.freeze(TerrainTypes);
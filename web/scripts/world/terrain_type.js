let TerrainType = {
    EMPTY: "EMPTY"
}
Object.freeze(TerrainType);
let TerrainTypes = {};
TerrainTypes[TerrainType.EMPTY] = {
    img_url: "assets/images/terrain_type_empty.png",
    do_forage: (character) => {
        character.apply_hunger(1);
        character.get_inventory().add_items(ItemType.PLANT_FIBER, 1);
        character.add_message(`You find 1 ${ItemTypes[ItemType.PLANT_FIBER].name}.`);
    }
};
Object.freeze(TerrainTypes);
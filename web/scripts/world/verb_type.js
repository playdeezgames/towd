let VerbType = {
    FORAGE: "FORAGE"
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
Object.freeze(VerbTypes);
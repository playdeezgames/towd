class InPlay {
    static run() {
        Utility.cls();
        Utility.add_paragraph("In Play");
        let world = new World();
        let room = world.get_avatar().get_room();
        let table = document.createElement("table");
        for(let row = 0; row < room.get_rows();++row){
            let table_row = document.createElement("tr");
            for(let column=0;column<room.get_columns();++column){
                let room_cell = room.get_cell(column, row);
                let table_data = document.createElement("td");
                let character = room_cell.get_character();
                if(character!= null) {
                    table_data.innerText = character.get_character_type();
                }else{
                    table_data.innerText = room_cell.get_terrain_type();
                }
                table_row.appendChild(table_data);
            }
            table.appendChild(table_row);
        }
        document.body.appendChild(table);
    }
}
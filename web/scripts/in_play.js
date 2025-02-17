class InPlay {
    static run() {
        Utility.cls();
        Utility.add_paragraph("In Play");
        let world = new World();
        let room = world.get_avatar().get_room();
        InPlay.render_room(room);
        InPlay.render_controls();
    }

    static render_controls() {
        let table = document.createElement("table");
        let first_row = document.createElement("tr");
        let upper_left_cell = document.createElement("td");
        let top_cell = document.createElement("td");
        let upper_right_cell = document.createElement("td");
        first_row.appendChild(upper_left_cell);
        first_row.appendChild(top_cell);
        first_row.appendChild(upper_right_cell);
        let second_row = document.createElement("tr");
        let left_cell = document.createElement("td");
        let middle_cell = document.createElement("td");
        let right_cell = document.createElement("td");
        second_row.appendChild(left_cell);
        second_row.appendChild(middle_cell);
        second_row.appendChild(right_cell);
        let third_row = document.createElement("tr");
        let lower_left_cell = document.createElement("td");
        let bottom_cell = document.createElement("td");
        let lower_right_cell = document.createElement("td");
        third_row.appendChild(lower_left_cell);
        third_row.appendChild(bottom_cell);
        third_row.appendChild(lower_right_cell);
        table.appendChild(first_row);
        table.appendChild(second_row);
        table.appendChild(third_row);
        document.body.appendChild(table);
        Utility.add_button_to(top_cell, "N", InPlay.move_north);
        Utility.add_button_to(bottom_cell, "S", InPlay.move_north);
        Utility.add_button_to(left_cell, "W", InPlay.move_north);
        Utility.add_button_to(right_cell, "E", InPlay.move_north);
    }

    static move_north(){
        let world = new World();
        world.get_avatar().move_north();
        InPlay.run()
    }
    static move_south(){
        let world = new World();
        world.get_avatar().move_south();
        InPlay.run()
    }
    static move_east(){
        let world = new World();
        world.get_avatar().move_east();
        InPlay.run()
    }
    static move_west(){
        let world = new World();
        world.get_avatar().move_west();
        InPlay.run()
    }

    static render_room(room) {
        let table = document.createElement("table");
        for (let row = 0; row < room.get_rows(); ++row) {
            let table_row = document.createElement("tr");
            for (let column = 0; column < room.get_columns(); ++column) {
                let room_cell = room.get_cell(column, row);
                let table_data = document.createElement("td");
                let character = room_cell.get_character();
                if (character != null) {
                    table_data.innerText = character.get_character_type();
                } else {
                    table_data.innerText = room_cell.get_terrain_type();
                }
                table_row.appendChild(table_data);
            }
            table.appendChild(table_row);
        }
        document.body.appendChild(table);
    }
}
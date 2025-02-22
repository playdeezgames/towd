class InPlay {
    static run() {
        Utility.set_command_hook(InPlay.command_hook)
        Utility.cls();
        InPlay.render_room();
        InPlay.render_controls();
        InPlay.render_stats();
        InPlay.render_messages();
        InPlay.render_inventory();
    }
    static command_hook(command){
        if(command == COMMAND_UP){
            InPlay.move_north();
        }else if(command == COMMAND_DOWN){
            InPlay.move_south();
        }else if(command == COMMAND_LEFT){
            InPlay.move_west();
        }else if(command == COMMAND_RIGHT){
            InPlay.move_east();
        }
    }

    static render_messages(){
        World.get_avatar().get_messages().forEach((message)=>{
            Utility.add_paragraph(message);
        });
    }

    static render_inventory(){
        World.get_avatar().get_inventory().get_item_stacks().forEach((item)=>{
            let quantity = item.get_quantity();
            if(quantity>0){
                Utility.add_paragraph(`${item.get_name()}: ${quantity}`)
            }
        })
    }

    static render_stats(){
        let avatar = World.get_avatar();
        Utility.add_paragraph(`Terrain: ${avatar.get_room_cell().get_name()}`);
        Utility.add_paragraph(`Satiety: ${avatar.get_statistic(StatisticType.SATIETY)}/${avatar.get_statistic(StatisticType.MAXIMUM_SATIETY)}`);
        Utility.add_paragraph(`Health: ${avatar.get_statistic(StatisticType.HEALTH)}/${avatar.get_statistic(StatisticType.MAXIMUM_HEALTH)}`);
    }

    static render_controls() {
        Utility.add_button("N", InPlay.move_north);
        Utility.add_break();
        Utility.add_button("W", InPlay.move_west);
        Utility.add_button("E", InPlay.move_east);
        Utility.add_break();
        Utility.add_button("S", InPlay.move_south);
        Utility.add_break();
        let avatar = World.get_avatar();
        for(let key in VerbType) {
            if(avatar.can_do_verb(key)){
                Utility.add_button(VerbTypes[key].name, () => { 
                    avatar.do_verb(key);
                    Neutral.run();
                });
            }
        }
    }

    static with_avatar(predicate){
        let avatar = World.get_avatar();
        predicate(avatar);
        Neutral.run();
    }
    static move_north(){
        InPlay.with_avatar((avatar)=>avatar.move_north());
    }
    static move_south(){
        InPlay.with_avatar((avatar)=>avatar.move_south());
    }
    static move_east(){
        InPlay.with_avatar((avatar)=>avatar.move_east());
    }
    static move_west(){
        InPlay.with_avatar((avatar)=>avatar.move_west());
    }

    static render_room() {
        let world = new World();
        let room = world.get_avatar().get_room();
        for (let row = 0; row < room.get_rows(); ++row) {
            let row_div = Utility.add_div();
            for (let column = 0; column < room.get_columns(); ++column) {
                let room_cell = room.get_room_cell(column, row);
                let character = room_cell.get_character();
                if (character != null) {
                    Utility.add_img_to(row_div, character.get_img_url());
                } else {
                    Utility.add_img_to(row_div, room_cell.get_img_url());
                }
            }
        }
    }
}
class InPlay {
    static run() {
        Utility.cls();
        InPlay.render_room();
        InPlay.render_controls();
        InPlay.render_stats();
        InPlay.render_messages();
        InPlay.render_inventory();
    }

    static render_messages(){
        (new World()).get_avatar().get_messages().forEach((message)=>{
            Utility.add_paragraph(message);
        });
    }

    static render_inventory(){
        (new World()).get_avatar().get_inventory().get_items().forEach((item)=>{
            Utility.add_paragraph(`${item.get_name()}: ${item.get_quantity()}`)
        })
    }

    static render_stats(){
        let avatar = (new World()).get_avatar();
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
        Utility.add_button("Forage", InPlay.forage);
    }

    static with_avatar(predicate){
        let avatar = (new World()).get_avatar();
        predicate(avatar);
        if(avatar.is_dead()) {
            GameOver.run();
        }else{
            InPlay.run();
        }
    }
    static forage(){
        InPlay.with_avatar((avatar)=>avatar.forage());
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
                let room_cell = room.get_cell(column, row);
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
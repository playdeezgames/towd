import CommandHook from "../../utility/command_hook.js";
import ElementStack from "../../utility/element_stack.js";
import World from "../../world/world.js";
import { VerbType, VerbTypes } from "../../world/enums/verb_type.js";
import { StatisticType } from "../../world/enums/statistic_type.js";
import Neutral from "./neutral.js";
export default class InPlay {
    static run() {
        CommandHook.set_command_hook(InPlay.command_hook)
        ElementStack.cls();
        ElementStack.push(ElementStack.add_table());

        ElementStack.push(ElementStack.add_table_row());

        ElementStack.push(ElementStack.add_table_data((element)=>{
            element.rowSpan = 2;
        }));
        InPlay.render_room();
        ElementStack.pop();

        ElementStack.push(ElementStack.add_table_data());
        InPlay.render_controls();
        ElementStack.pop();

        ElementStack.push(ElementStack.add_table_data((element)=>{
            element.rowSpan = 2;
        }));
        InPlay.render_inventory();
        ElementStack.pop();

        ElementStack.pop();
        ElementStack.push(ElementStack.add_table_row());

        ElementStack.push(ElementStack.add_table_data());
        InPlay.render_stats();
        ElementStack.pop();

        ElementStack.pop();

        ElementStack.push(ElementStack.add_table_row());
        ElementStack.push(ElementStack.add_table_data((element)=>{element.colSpan=3}));
        InPlay.render_messages();
        ElementStack.pop();
        ElementStack.pop();
        ElementStack.pop();
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
            ElementStack.add_paragraph(message);
        });
    }

    static render_inventory(){
        ElementStack.add_paragraph("Inventory:")
        World.get_avatar().get_inventory().get_item_stacks().forEach((item)=>{
            let quantity = item.get_quantity();
            if(quantity>0){
                ElementStack.add_paragraph(`${item.get_name()}: ${quantity}`)
            }
        })
    }

    static render_stats(){
        ElementStack.add_paragraph("Statistics:")
        let avatar = World.get_avatar();
        ElementStack.push(ElementStack.add_paragraph(`Terrain: ${avatar.get_room_cell().get_name()}`));
        let report = avatar.get_room_cell().get_details();
        if(report.length>0){
            ElementStack.add_img(World.get_avatar().get_room_cell().get_img_url());
            ElementStack.add_span(`(${report.join(", ")})`)
        }
        ElementStack.pop();
        ElementStack.add_paragraph(`Satiety: ${avatar.get_statistic(StatisticType.SATIETY)}/${avatar.get_statistic(StatisticType.MAXIMUM_SATIETY)}`);
        ElementStack.add_paragraph(`Health: ${avatar.get_statistic(StatisticType.HEALTH)}/${avatar.get_statistic(StatisticType.MAXIMUM_HEALTH)}`);
    }

    static render_controls() {
        ElementStack.add_paragraph("Controls:")

        ElementStack.push(ElementStack.add_table())
        ElementStack.push(ElementStack.add_table_row())
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.pop()
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.add_button("N", InPlay.move_north);
        ElementStack.pop()
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.pop()
        ElementStack.pop()
        ElementStack.push(ElementStack.add_table_row())
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.add_button("W", InPlay.move_west);
        ElementStack.pop()
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.pop()
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.add_button("E", InPlay.move_east);
        ElementStack.pop()
        ElementStack.pop()
        ElementStack.push(ElementStack.add_table_row())
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.pop()
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.add_button("S", InPlay.move_south);
        ElementStack.pop()
        ElementStack.push(ElementStack.add_table_data())
        ElementStack.pop()
        ElementStack.pop()
        ElementStack.pop()

        ElementStack.add_break();
        let avatar = World.get_avatar();
        for(let key in VerbType) {
            if(avatar.can_do_verb(key)){
                ElementStack.add_button(VerbTypes[key].name, () => { 
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
            ElementStack.push(ElementStack.add_div());
            for (let column = 0; column < room.get_columns(); ++column) {
                let room_cell = room.get_room_cell(column, row);
                let character = room_cell.get_character();
                if (character != null) {
                    ElementStack.add_img(character.get_img_url());
                } else {
                    ElementStack.add_img(room_cell.get_img_url());
                }
            }
            ElementStack.pop();
        }
    }
}
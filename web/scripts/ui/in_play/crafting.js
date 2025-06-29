import CommandHook from "../../utility/command_hook.js";
import ElementStack from "../../utility/element_stack.js";
import World from "../../world/world.js";
import Recipes from "../../world/item/recipe.js";
import InPlay from "./in_play.js";
import Neutral from "./neutral.js";
import { FlagType } from "../../world/enums/flag_type.js";
export default class Crafting {
    static run(){
        CommandHook.clear_command_hook();
        ElementStack.cls();
        let character = World.get_avatar();
        for(let recipe of Recipes){
            if(recipe.can_craft(character)){
                ElementStack.add_button(recipe.get_name(), ()=>{
                    character.clear_messages();
                    recipe.craft(character);
                    character.get_world().advance_time(1);
                    Neutral.run();
                });
                ElementStack.add_break();
            }
        }
        ElementStack.add_button("Stop Crafting", () => {
            let avatar = World.get_avatar();
            avatar.clear_messages();
            avatar.set_flag(FlagType.CRAFTING, false);
            Neutral.run();
        })
        InPlay.render_messages();
        InPlay.render_inventory();
    }
}
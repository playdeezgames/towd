import CommandHook from "../../utility/command_hook.js";
import World from "../../world/world.js";
import { FlagType } from "../../world/enums/flag_type.js";
import InPlay from "./in_play.js";
import Crafting from "./crafting.js";
import GameOver from "./game_over.js";
export default class Neutral {
    static run(){
        CommandHook.clear_command_hook();
        let avatar = World.get_avatar();
        if(avatar.is_dead()) {
            GameOver.run();
        }else if(avatar.get_flag(FlagType.CRAFTING)){
            Crafting.run();
        }else{
            InPlay.run();
        }
    }
}
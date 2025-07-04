import {Display} from "../../common/Display.js";
import {SaveSlot} from "../../World/enums/save_slots.js";
import {World} from "../../World/world.js";

export class NeutralState {
    static run(){
        World.save(SaveSlot.AUTO, false);
        let character = World.getAvatar();
        if(character.hasMessages()){
            MessageState.run();
        } else if(character.isDead()){
            GameOverState.run();
        } else if(character.hasFlag(FlagType.INVENTORY)){
            if(character.hasCurrentItemType()){
                ItemStackState.run();
            }else{
                InventoryState.run();
            }
        } else if(character.hasFlag(FlagType.SKILL_MENU)){
            SkillMenuState.run();
        } else if(character.hasFlag(FlagType.MOVE_MENU)){
            MoveMenuState.run();
        } else if(character.hasFlag(FlagType.CRAFT_MENU)){
            CraftMenuState.run();
        } else{
            NavigationState.run();
        }
    }
}
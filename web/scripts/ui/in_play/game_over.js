import CommandHook from "../../utility/command_hook.js";
import ElementStack from "../../utility/element_stack.js";
import Main from "../boilerplate/main.js";
export default class GameOver{
    static run(){
        CommandHook.clear_command_hook();
        ElementStack.cls();
        ElementStack.add_paragraph("Yer dead.");
        ElementStack.add_button("Main Menu", Main.run)
    }
}
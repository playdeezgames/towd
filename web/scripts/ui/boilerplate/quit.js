import CommandHook from '../../utility/command_hook.js'
import ElementStack from '../../utility/element_stack.js';
export default class Quit {
    static run() {
        CommandHook.clear_command_hook();
        ElementStack.cls();
        ElementStack.add_paragraph("Thank you for playing!");
    }
}
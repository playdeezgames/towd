import CommandHook from '../../utility/command_hook.js'
import ElementStack from '../../utility/element_stack.js';
import Main from './main.js'
export default class Settings {
    static run() {
        CommandHook.clear_command_hook();
        ElementStack.cls();
        ElementStack.add_paragraph("TODO: make settings screen");
        ElementStack.add_button("Go Back", Main.run);
    }
}

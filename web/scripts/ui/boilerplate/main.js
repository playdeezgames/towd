import CommandHook from '../../utility/command_hook.js'
import ElementStack from '../../utility/element_stack.js';
import Embark from './embark.js'
import About from './about.js'
import Load from './load.js'
import Settings from './settings.js'
import ConfirmQuit from './confirm_quit.js'
export default class Main {
    static run() {
        CommandHook.hook_keyboard();
        CommandHook.clear_command_hook();
        ElementStack.cls();
        ElementStack.add_button("Embark!", Embark.run);
        ElementStack.add_break();
        ElementStack.add_button("Load...", Load.run);
        ElementStack.add_break();
        ElementStack.add_button("Settings...", Settings.run);
        ElementStack.add_break();
        ElementStack.add_button("About...", About.run);
        ElementStack.add_break();
        ElementStack.add_button("Quit", ConfirmQuit.run);
    }
}
Main.run();
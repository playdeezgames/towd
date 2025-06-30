export const COMMAND_UP = "UP";
export const COMMAND_DOWN = "DOWN";
export const COMMAND_LEFT = "LEFT";
export const COMMAND_RIGHT = "RIGHT";
export default class CommandHook {
    static command_hook = (_) => {};
    static keyboard_hooked = false;
    static command_table = {
        w: COMMAND_UP,
        z: COMMAND_UP,
        a: COMMAND_LEFT,
        q: COMMAND_LEFT,
        s: COMMAND_DOWN,
        d: COMMAND_RIGHT
    };
    static set_command_hook(hook){
        CommandHook.command_hook = hook;
    }
    static clear_command_hook(){
        CommandHook.set_command_hook((_)=>{});
    }
    static hook_keyboard(){
        if(!CommandHook.keyboard_hooked){
            document.body.addEventListener("keypress", (event) => {
                let command = CommandHook.command_table[event.key.toLowerCase()];
                if(command != null){
                    CommandHook.command_hook(command);
                }
            });
        }
    }
}
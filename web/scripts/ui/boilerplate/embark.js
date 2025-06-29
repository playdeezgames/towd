import CommandHook from '../../utility/command_hook.js'
import World from '../../world/world.js'
import Neutral from '../in_play/neutral.js';
export default class Embark {
    static run() {
        CommandHook.clear_command_hook();
        let world = new World();
        world.initialize();
        Neutral.run();
    }
}

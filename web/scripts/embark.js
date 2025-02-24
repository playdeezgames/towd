class Embark {
    static run() {
        CommandHook.clear_command_hook();
        let world = new World();
        world.initialize();
        Neutral.run();
    }
}

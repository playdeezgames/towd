class Embark {
    static run() {
        Utility.clear_command_hook();
        let world = new World();
        world.initialize();
        Neutral.run();
    }
}

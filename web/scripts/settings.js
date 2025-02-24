class Settings {
    static run() {
        CommandHook.clear_command_hook();
        Utility.cls();
        Utility.add_paragraph("TODO: make settings screen");
        Utility.add_button("Go Back", Main.run);
    }
}

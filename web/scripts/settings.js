class Settings {
    static run() {
        CommandHook.clear_command_hook();
        ElementStack.cls();
        ElementStack.add_paragraph("TODO: make settings screen");
        ElementStack.add_button("Go Back", Main.run);
    }
}

class About {
    static run() {
        CommandHook.clear_command_hook();
        Utility.cls();
        Utility.add_paragraph("TODO: about screen");
        Utility.add_button("Go Back", Main.run);
    }
}

class About {
    static run() {
        CommandHook.clear_command_hook();
        ElementStack.cls();
        ElementStack.add_paragraph("TODO: about screen");
        ElementStack.add_button("Go Back", Main.run);
    }
}

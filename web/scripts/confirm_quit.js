class ConfirmQuit {
    static run() {
        CommandHook.clear_command_hook();
        ElementStack.cls();
        ElementStack.add_paragraph("Are you sure you want to quit?");
        ElementStack.add_button("No", Main.run);
        ElementStack.add_break();
        ElementStack.add_button("Yes", Quit.run);
    }
}

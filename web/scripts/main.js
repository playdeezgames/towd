class Main {
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
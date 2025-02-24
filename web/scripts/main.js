class Main {
    static run() {
        CommandHook.hook_keyboard();
        CommandHook.clear_command_hook();
        Utility.cls();
        Utility.add_button("Embark!", Embark.run);
        Utility.add_break();
        Utility.add_button("Load...", Load.run);
        Utility.add_break();
        Utility.add_button("Settings...", Settings.run);
        Utility.add_break();
        Utility.add_button("About...", About.run);
        Utility.add_break();
        Utility.add_button("Quit", ConfirmQuit.run);
    }
}
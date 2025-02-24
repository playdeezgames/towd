class Quit {
    static run() {
        CommandHook.clear_command_hook();
        Utility.cls();
        Utility.add_paragraph("Thank you for playing!");
    }
}
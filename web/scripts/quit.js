class Quit {
    static run() {
        Utility.clear_command_hook();
        Utility.cls();
        Utility.add_paragraph("Thank you for playing!");
    }
}
class GameOver{
    static run(){
        CommandHook.clear_command_hook();
        Utility.cls();
        Utility.add_paragraph("Yer dead.");
        Utility.add_button("Main Menu", Main.run)
    }
}
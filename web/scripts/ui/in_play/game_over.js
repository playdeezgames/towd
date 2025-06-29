class GameOver{
    static run(){
        CommandHook.clear_command_hook();
        ElementStack.cls();
        ElementStack.add_paragraph("Yer dead.");
        ElementStack.add_button("Main Menu", Main.run)
    }
}
class GameOver{
    static run(){
        Utility.cls();
        Utility.add_paragraph("Yer dead.");
        Utility.add_button("Main Menu", Main.run)
    }
}
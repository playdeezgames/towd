class ConfirmQuit {
    static run() {
        Utility.cls();
        Utility.add_paragraph("Are you sure you want to quit?");
        Utility.add_button("No", Main.run);
        Utility.add_break();
        Utility.add_button("Yes", Quit.run);
    }
}

import {Display} from "../../common/Display.js";
import {MainMenu} from "./main_menu.js";
import Main from "./main.js";

export class ConfirmQuit{
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "Are you sure you want to quit?");
        Display.addButton("Yes", ConfirmQuit.confirm);
        Display.addButton("No", ConfirmQuit.cancel)
    }
    static confirm(){
        Main.run();
    }
    static cancel(){
        MainMenu.run();
    }
}

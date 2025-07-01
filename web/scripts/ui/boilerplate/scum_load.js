import {Display} from "../../common/Display.js";
import {MainMenu} from "./main_menu.js";

export class ScumLoad{
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "Scum Load");
        Display.addButton("Cancel", ScumLoad.cancel);
    }
    static cancel(){
        MainMenu.run();
    }
}

import {Display} from "../../common/Display.js";
import {MainMenu} from "./main_menu.js";

export class Load{
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "Load");
        Display.addButton("Cancel", Load.cancel);
    }
    static cancel(){
        MainMenu.run();
    }
}

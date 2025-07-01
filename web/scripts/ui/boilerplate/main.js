import {Display} from "../../common/Display.js";
import {MainMenu} from "./main_menu.js";
export default class Main{
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "TOWD");
        Display.addButton("Ok", MainMenu.run);
    }
}
Main.run();
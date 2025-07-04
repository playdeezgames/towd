import {Display} from "../../common/Display.js";
import {Embark} from "./embark.js";
import {ScumLoad} from "./scum_load.js";
import {Load} from "./load.js";
import {ConfirmQuit} from "./confirm_quit.js";

export class MainMenu{
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "Main Menu:");
        Display.addButton("Embark!", MainMenu.embark);
        Display.addButton("Scum Load", MainMenu.scumLoad);
        Display.addButton("Load...", MainMenu.load);
        Display.addButton("Quit", MainMenu.quit);
    }
    static embark(){
        Embark.run();
    }
    static scumLoad(){
        ScumLoad.run();
    }
    static load(){
        Load.run();
    }
    static quit(){
        ConfirmQuit.run();
    }
}
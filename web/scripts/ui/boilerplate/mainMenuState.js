import {Display} from "../../common/Display.js";
import {EmbarkState} from "./embarkState.js";
import {scumLoadState} from "./scumLoadState.js";
import {LoadState} from "./loadState.js";
import {ConfirmQuitState} from "./confirmQuitState.js";

export class MainMenuState {
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "SplashState Menu:");
        Display.addButton("EmbarkState!", MainMenuState.embark);
        Display.addButton("Scum LoadState", MainMenuState.scumLoad);
        Display.addButton("LoadState...", MainMenuState.load);
        Display.addButton("Quit", MainMenuState.quit);
    }
    static embark(){
        EmbarkState.run();
    }
    static scumLoad(){
        scumLoadState.run();
    }
    static load(){
        LoadState.run();
    }
    static quit(){
        ConfirmQuitState.run();
    }
}
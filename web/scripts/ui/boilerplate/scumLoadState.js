import {Display} from "../../common/Display.js";
import {MainMenuState} from "./mainMenuState.js";

export class scumLoadState {
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "Scum LoadState");
        Display.addButton("Cancel", scumLoadState.cancel);
    }
    static cancel(){
        MainMenuState.run();
    }
}

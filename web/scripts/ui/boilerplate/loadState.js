import {Display} from "../../common/Display.js";
import {MainMenuState} from "./mainMenuState.js";

export class LoadState {
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "LoadState");
        Display.addButton("Cancel", LoadState.cancel);
    }
    static cancel(){
        MainMenuState.run();
    }
}

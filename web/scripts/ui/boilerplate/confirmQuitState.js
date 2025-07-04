import {Display} from "../../common/Display.js";
import {MainMenuState} from "./mainMenuState.js";
import SplashState from "./splashState.js";

export class ConfirmQuitState {
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "Are you sure you want to quit?");
        Display.addButton("Yes", ConfirmQuitState.confirm);
        Display.addButton("No", ConfirmQuitState.cancel)
    }
    static confirm(){
        SplashState.run();
    }
    static cancel(){
        MainMenuState.run();
    }
}

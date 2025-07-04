import {Display} from "../../common/Display.js";
import {MainMenuState} from "./mainMenuState.js";
export default class SplashState {
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "TOWD");
        Display.addButton("Ok", MainMenuState.run);
    }
}
SplashState.run();
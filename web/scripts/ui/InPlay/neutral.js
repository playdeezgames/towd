import {Display} from "../../common/Display.js";

export class Neutral{
    static run(){
        Display.clear();
        Display.addSimpleChild("p", "Neutral");
    }
}
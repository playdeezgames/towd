import {World} from "../../World/world.js";
import {Neutral} from "../InPlay/neutral.js";

export class Embark {
    static run(){
        World.initialize();
        Neutral.run();
    }
}
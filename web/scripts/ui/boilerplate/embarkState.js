import {World} from "../../World/world.js";
import {NeutralState} from "../InPlay/neutralState.js";

export class EmbarkState {
    static run(){
        World.initialize();
        NeutralState.run();
    }
}
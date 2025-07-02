export class WorldData {
    static data = null;
    static clear(){
        WorldData.data = {
            maps:[],
            characters:[],
            mapCells:[],
        };
    }
}
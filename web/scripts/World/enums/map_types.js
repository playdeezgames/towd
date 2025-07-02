import {MapCellType} from "./map_cell_types.js";

export const MapType= {
    NORMAL: "NORMAL"
};
class MapTypeDescriptor{
    static create(){
        return new MapTypeDescriptor();
    }
    setMapCellType(mapCellType){
        this.mapCellType = mapCellType;
        return this;
    }
}
export const MapTypes = {
    [MapType.NORMAL]:
        MapTypeDescriptor.
            create().
            setMapCellType(MapCellType.GRASS),
}
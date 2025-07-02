export const MapCellType = {
    COOKING_FIRE:"COOKING_FIRE",
    DIRT:"DIRT",
    FURNACE:"FURNACE",
    GRASS:"GRASS",
    PINE:"PINE",
    POND:"POND",
    ROCK:"ROCK",
};
class MapCellTypeDescriptor{
    static create(){
        return new MapCellTypeDescriptor();
    }
    setInitialize(initialize){
        this.initialize = initialize;
        return this;
    }
}
export const MapCellTypes={
    [MapCellType.COOKING_FIRE]: MapCellTypeDescriptor.create(),
    [MapCellType.DIRT]: MapCellTypeDescriptor.create(),
    [MapCellType.FURNACE]: MapCellTypeDescriptor.create(),
    [MapCellType.GRASS]: MapCellTypeDescriptor.
        create().setInitialize(()=>{}),
    [MapCellType.PINE]: MapCellTypeDescriptor.create(),
    [MapCellType.POND]: MapCellTypeDescriptor.create(),
    [MapCellType.ROCK]: MapCellTypeDescriptor.create(),
};
let ItemType = {
    PLANT_FIBER: "PLANT_FIBER",
    STICK: "STICK",
    ROCK: "ROCK"
};
Object.freeze(ItemType);
let ItemTypes = {};
ItemTypes[ItemType.PLANT_FIBER] = {
    name: "Plant Fiber"
};
ItemTypes[ItemType.STICK] = {
    name: "Stick"
};
ItemTypes[ItemType.ROCK] = {
    name: "Rock"
};
Object.freeze(ItemTypes);
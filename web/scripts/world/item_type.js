let ItemType = {
    PLANT_FIBER: "PLANT_FIBER",
    STICK: "STICK",
    ROCK: "ROCK",
    TWINE: "TWINE",
    SHARP_ROCK: "SHARP_ROCK"
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
ItemTypes[ItemType.TWINE] = {
    name: "Twine"
};
ItemTypes[ItemType.SHARP_ROCK] = {
    name: "Sharp Rock"
};
Object.freeze(ItemTypes);
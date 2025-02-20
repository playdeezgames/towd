let ItemType = {
    PLANT_FIBER: "PLANT_FIBER",
    STICK: "STICK",
    ROCK: "ROCK",
    TWINE: "TWINE",
    SHARP_ROCK: "SHARP_ROCK",
    HATCHET: "HATCHET",
    LOG: "LOG"
};
Object.freeze(ItemType);
let ItemTypes = {};
ItemTypes[ItemType.PLANT_FIBER] = {
    name: "Plant Fiber",
    initialize: (item) => {}
};
ItemTypes[ItemType.STICK] = {
    name: "Stick",
    initialize: (item) => {}
};
ItemTypes[ItemType.ROCK] = {
    name: "Rock",
    initialize: (item) => {}
};
ItemTypes[ItemType.TWINE] = {
    name: "Twine",
    initialize: (item) => {}
};
ItemTypes[ItemType.SHARP_ROCK] = {
    name: "Sharp Rock",
    initialize: (item) => {}
};
ItemTypes[ItemType.HATCHET] = {
    name: "Hatchet",
    initialize: (item) => {}
};
ItemTypes[ItemType.LOG] = {
    name: "Log",
    initialize: (item) => {}
};
Object.freeze(ItemTypes);
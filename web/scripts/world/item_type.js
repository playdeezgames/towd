let ItemType = {
    PLANT_FIBER: "PLANT_FIBER",
    STICK: "STICK",
    ROCK: "ROCK",
    TWINE: "TWINE",
    SHARP_ROCK: "SHARP_ROCK",
    HATCHET: "HATCHET",
    LOG: "LOG",
    HAMMER: "HAMMER",
    PLANK: "PLANK",
    SHARP_STICK: "SHARP_STICK",
    GRUB: "GRUB",
    COOKING_FIRE: "COOKING_FIRE",
    GRUB_ON_STICK: "GRUB_ON_STICK"
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
    initialize: (item) => {
        item.set_statistic(StatisticType.DURABILITY, 30);
        item.set_statistic(StatisticType.MAXIMUM_DURABILITY, 30);
    }
};
ItemTypes[ItemType.LOG] = {
    name: "Log",
    initialize: (item) => {}
};
ItemTypes[ItemType.HAMMER] = {
    name: "Hammer",
    initialize: (item) => {
        item.set_statistic(StatisticType.DURABILITY, 30);
        item.set_statistic(StatisticType.MAXIMUM_DURABILITY, 30);
    }
};
ItemTypes[ItemType.PLANK] = {
    name: "Plank",
    initialize: (item) => {}
};
ItemTypes[ItemType.SHARP_STICK] = {
    name: "Sharp Stick",
    initialize: (item) => {
        item.set_statistic(StatisticType.DURABILITY, 30);
        item.set_statistic(StatisticType.MAXIMUM_DURABILITY, 30);
    }
};
ItemTypes[ItemType.GRUB] = {
    name: "Grub",
    initialize: (item) => {}
};
ItemTypes[ItemType.COOKING_FIRE] = {
    name: "Cooking Fire",
    initialize: (item) => {}
};
ItemTypes[ItemType.GRUB_ON_STICK] = {
    name: "Grub on a Stick",
    initialize: (item) => {}
};
Object.freeze(ItemTypes);
import {StatisticType} from "../enums/statistic_type.js";

export const ItemType = {
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
    COOKED_GRUB: "COOKED_GRUB",
    CLAY: "CLAY",
    CHARCOAL: "CHARCOAL",
    UNFIRED_BRICK: "UNFIRED_BRICK",
    BRICK: "BRICK",
    FISHING_NET: "FISHING_NET",
    RAW_FISH: "RAW_FISH",
    RAW_FISH_FILET: "RAW_FISH_FILET",
    FISH_HEAD: "FISH_HEAD",
    FISH_GUTS: "FISH_GUTS",
    KNIFE: "KNIFE",
    BLADE: "BLADE",
    COOKED_FISH_FILET: "COOKED_FISH_FILET",
    FURNACE: "FURNACE"
};
Object.freeze(ItemType);
export let ItemTypes = {};
ItemTypes[ItemType.PLANT_FIBER] = {
    name: "Plant Fiber",
    initialize: (_) => {}
};
ItemTypes[ItemType.STICK] = {
    name: "Stick",
    initialize: (_) => {}
};
ItemTypes[ItemType.ROCK] = {
    name: "Rock",
    initialize: (_) => {}
};
ItemTypes[ItemType.TWINE] = {
    name: "Twine",
    initialize: (_) => {}
};
ItemTypes[ItemType.SHARP_ROCK] = {
    name: "Sharp Rock",
    initialize: (_) => {}
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
    initialize: (item) => {
        item.set_statistic(StatisticType.FUEL, 16);
    }
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
    initialize: (_) => {}
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
    initialize: (_) => {}
};
ItemTypes[ItemType.COOKED_GRUB] = {
    name: "Cooked Grub",
    initialize: (item) => {
        item.set_statistic(StatisticType.SATIETY, 5);
    }
};
ItemTypes[ItemType.COOKING_FIRE] = {
    name: "Cooking Fire",
    initialize: (_) => {}
};
ItemTypes[ItemType.FURNACE] = {
    name: "Furnace",
    initialize: (_) => {}
};
ItemTypes[ItemType.CLAY] = {
    name: "Clay",
    initialize: (_) => {}
};
ItemTypes[ItemType.CHARCOAL] = {
    name: "Charcoal",
    initialize: (_) => {}
};
ItemTypes[ItemType.UNFIRED_BRICK] = {
    name: "Unfired Brick",
    initialize: (_) => {}
};
ItemTypes[ItemType.BRICK] = {
    name: "Brick",
    initialize: (_) => {}
};
ItemTypes[ItemType.RAW_FISH] = {
    name: "Raw Fish",
    initialize: (_) => {}
};
ItemTypes[ItemType.RAW_FISH_FILET] = {
    name: "Raw Fish Filet",
    initialize: (_) => {}
};
ItemTypes[ItemType.FISH_HEAD] = {
    name: "Fish Head",
    initialize: (_) => {}
};
ItemTypes[ItemType.FISH_GUTS] = {
    name: "Fish Guts",
    initialize: (_) => {}
};
ItemTypes[ItemType.BLADE] = {
    name: "Blade",
    initialize: (_) => {}
};
ItemTypes[ItemType.COOKED_FISH_FILET] = {
    name: "Cooked Fish Filet",
    initialize: (item) => {
        item.set_statistic(StatisticType.SATIETY, 10);
    }
};
ItemTypes[ItemType.FISHING_NET] = {
    name: "Fishing Net",
    initialize: (item) => {
        item.set_statistic(StatisticType.DURABILITY, 30);
        item.set_statistic(StatisticType.MAXIMUM_DURABILITY, 30);
    }
};
ItemTypes[ItemType.KNIFE] = {
    name: "Knife",
    initialize: (item) => {
        item.set_statistic(StatisticType.DURABILITY, 30);
        item.set_statistic(StatisticType.MAXIMUM_DURABILITY, 30);
    }
};
Object.freeze(ItemTypes);
export default ItemTypes
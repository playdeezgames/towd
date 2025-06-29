const CharacterType = {
    N00B: "N00B"
}
let CharacterTypes = {};
CharacterTypes[CharacterType.N00B] = {
    img_url: "assets/images/character_type_n00b.png",
    initialize: (character) => {
        character.set_statistic(StatisticType.HEALTH, 100);
        character.set_statistic(StatisticType.MAXIMUM_HEALTH, 100);
        character.set_statistic(StatisticType.SATIETY, 100);
        character.set_statistic(StatisticType.MAXIMUM_SATIETY, 100);
    },
    advance_time: (character) => {
        character.apply_hunger(1);
    }
};
Object.freeze(CharacterTypes);
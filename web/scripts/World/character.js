import {WorldData} from "../data/world_data.js";

export class Character{
    constructor(characterId){
        this.characterId = characterId;
    }
    getCharacterData(){
        return WorldData.data.characters[this.characterId];
    }
    addKnownMapCell(mapCell){
        let mapCellId = mapCell.mapCellId;
        let characterData = this.getCharacterData();
        if(!characterData.knownMapCells.includes(mapCellId)){
            characterData.knownMapCells.push(mapCellId);
        }
    }
}
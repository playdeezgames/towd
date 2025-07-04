import {Display} from "../../common/Display.js";
import {SaveSlot} from "../../World/enums/save_slots.js";
import {World} from "../../World/world.js";

export class Neutral{
    static run(){
        World.save(SaveSlot.AUTO, false);
        // Dim character = World.Avatar
        // If character.HasMessages Then
        // ShowState(GameState.Message)
        // ElseIf character.IsDead Then
        // ShowState(GameState.Dead)
        // ElseIf character.HasFlag(FlagType.Inventory) Then
        // If character.CurrentItemType IsNot Nothing Then
        // ShowState(GameState.ItemStack)
        // Else
        // ShowState(GameState.Inventory)
        // End If
        // ElseIf character.HasFlag(FlagType.SkillMenu) Then
        // ShowState(GameState.SkillMenu)
        // ElseIf character.HasFlag(FlagType.MoveMenu) Then
        // ShowState(GameState.MoveMenu)
        // ElseIf character.HasFlag(FlagType.CraftMenu) Then
        // ShowState(GameState.CraftMenu)
        // Else
        // ShowState(GameState.Navigation)
        // End If
    }
}
class Crafting {
    static run(){
        Utility.cls();
        let character = World.get_avatar();
        for(let recipe_type_id in RecipeType){
            if(Recipe.can_craft(character, recipe_type_id)){
                Utility.add_button(Recipe.get_name(recipe_type_id), ()=>{
                    character.clear_messages();
                    Recipe.craft(character, recipe_type_id);
                    Neutral.run();
                });
                Utility.add_break();
            }
        }
        Utility.add_button("Stop Crafting", () => {
            let avatar = World.get_avatar();
            avatar.clear_messages();
            avatar.set_flag(FlagType.CRAFTING, false);
            Neutral.run();
        })
        InPlay.render_messages();
        InPlay.render_inventory();
    }
}
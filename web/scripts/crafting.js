class Crafting {
    static run(){
        Utility.clear_command_hook();
        Utility.cls();
        let character = World.get_avatar();
        for(let recipe of Recipes){
            if(recipe.can_craft(character)){
                Utility.add_button(recipe.get_name(), ()=>{
                    character.clear_messages();
                    recipe.craft(character);
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
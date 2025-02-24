class Neutral {
    static run(){
        CommandHook.clear_command_hook();
        let avatar = World.get_avatar();
        if(avatar.is_dead()) {
            GameOver.run();
        }else if(avatar.get_flag(FlagType.CRAFTING)){
            Crafting.run();
        }else{
            InPlay.run();
        }
    }
}
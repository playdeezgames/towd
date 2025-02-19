class Neutral {
    static run(){
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
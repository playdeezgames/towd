class Neutral {
    static run(){
        let avatar = World.get_avatar();
        if(avatar.is_dead()) {
            GameOver.run();
        }else{
            InPlay.run();
        }
    }
}
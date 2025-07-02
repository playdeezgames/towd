export const CharacterType = {
    N00B: "N00B"
};
class CharacterTypeDescriptor{
    static create(){
        return new CharacterTypeDescriptor();
    }
    setInitialize(initialize){
        this.initialize = initialize;
        return this;
    }
}
export const CharacterTypes= {
    [CharacterType.N00B]:
        CharacterTypeDescriptor.
            create().
            setInitialize(()=>{}),
};
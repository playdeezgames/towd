export default class RNG {
    static generate(table){
        let total = 0;
        for(let key in table){
            total += table[key];
        }
        let generated = RNG.roll(total);
        for(let key in table){
            if(generated<table[key]){
                return key;
            }else{
                generated -= table[key];
            }
        }
        throw "not found";
    }
    static roll(value){
        return Math.floor(Math.random() * value);
    }
}
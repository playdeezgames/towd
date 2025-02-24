class Utility {
    static clear(element) {
        while(element.hasChildNodes()){
            element.removeChild(element.firstChild);
        }
    }
    static element_stack = [];
    static top(){
        if(Utility.element_stack.length>0){
            return Utility.element_stack.at(-1);
        }
        return document.body;
    }
    static push(element){
        Utility.element_stack.push(element);
    }
    static pop(){
        Utility.element_stack.pop();
    }
    static cls() {
        Utility.clear(Utility.top());
    }
    static add_button_to(parent, text, on_click) {
        let button = document.createElement("button");
        button.innerText = text;
        button.addEventListener("click", on_click);
        parent.appendChild(button);
        return button;
    }
    static add_button(text, on_click) {
        return Utility.add_button_to(Utility.top(), text, on_click);
    }
    static add_paragraph_to(parent, text) {
        let p = document.createElement("p");
        p.innerHTML = text;
        parent.appendChild(p);
        return p;
    }
    static add_paragraph(text) {
        return Utility.add_paragraph_to(Utility.top(), text);
    }
    static add_span_to(parent, text) {
        let p = document.createElement("span");
        p.innerHTML = text;
        parent.appendChild(p);
        return p;
    }
    static add_span(text) {
        return Utility.add_span_to(Utility.top(), text);
    }
    static add_break_to(parent) {
        let br = document.createElement("br");
        parent.appendChild(br);
        return br;
    }
    static add_break() {
        return Utility.add_break_to(Utility.top());
    }
    static add_img_to(parent, img_url) {
        let img = document.createElement("img");
        img.src = img_url;
        parent.appendChild(img);
        return img;
    }
    static add_img(img_url){
        return Utility.add_img_to(Utility.top(), img_url);
    }
    static add_div_to(parent){
        let div = document.createElement("div");
        parent.appendChild(div);
        return div;
    }
    static add_div(){
        return Utility.add_div_to(Utility.top());
    }
}
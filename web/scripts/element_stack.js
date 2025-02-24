class ElementStack {
    static clear(element) {
        while(element.hasChildNodes()){
            element.removeChild(element.firstChild);
        }
    }
    static element_stack = [];
    static top(){
        if(ElementStack.element_stack.length>0){
            return ElementStack.element_stack.at(-1);
        }
        return document.body;
    }
    static push(element){
        ElementStack.element_stack.push(element);
    }
    static pop(){
        ElementStack.element_stack.pop();
    }
    static cls() {
        ElementStack.clear(ElementStack.top());
    }
    static add_button_to(parent, text, on_click) {
        let button = document.createElement("button");
        button.innerText = text;
        button.addEventListener("click", on_click);
        parent.appendChild(button);
        return button;
    }
    static add_button(text, on_click) {
        return ElementStack.add_button_to(ElementStack.top(), text, on_click);
    }
    static add_paragraph_to(parent, text) {
        let p = document.createElement("p");
        p.innerHTML = text;
        parent.appendChild(p);
        return p;
    }
    static add_paragraph(text) {
        return ElementStack.add_paragraph_to(ElementStack.top(), text);
    }
    static add_span_to(parent, text) {
        let p = document.createElement("span");
        p.innerHTML = text;
        parent.appendChild(p);
        return p;
    }
    static add_span(text) {
        return ElementStack.add_span_to(ElementStack.top(), text);
    }
    static add_break_to(parent) {
        let br = document.createElement("br");
        parent.appendChild(br);
        return br;
    }
    static add_break() {
        return ElementStack.add_break_to(ElementStack.top());
    }
    static add_img_to(parent, img_url) {
        let img = document.createElement("img");
        img.src = img_url;
        parent.appendChild(img);
        return img;
    }
    static add_img(img_url){
        return ElementStack.add_img_to(ElementStack.top(), img_url);
    }
    static add_div_to(parent){
        let div = document.createElement("div");
        parent.appendChild(div);
        return div;
    }
    static add_div(){
        return ElementStack.add_div_to(ElementStack.top());
    }
}
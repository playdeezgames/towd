class Utility {
    static clear(element) {
        while(element.hasChildNodes()){
            element.removeChild(element.firstChild);
        }
    }
    static cls() {
        Utility.clear(document.body);
    }
    static add_button_to(parent, text, on_click) {
        let button = document.createElement("button");
        button.innerText = text;
        button.addEventListener("click", on_click);
        parent.appendChild(button);
        return button;
    }
    static add_button(text, on_click) {
        return Utility.add_button_to(document.body, text, on_click);
    }
    static add_paragraph_to(parent, text) {
        let p = document.createElement("p");
        p.innerHTML = text;
        parent.appendChild(p);
        return p;
    }
    static add_paragraph(text) {
        return Utility.add_paragraph_to(document.body, text);
    }
    static add_break_to(parent) {
        let br = document.createElement("br");
        parent.appendChild(br);
        return br;
    }
    static add_break() {
        return Utility.add_break_to(document.body);
    }
    static add_img_to(parent, img_url) {
        let img = document.createElement("img");
        img.src = img_url;
        parent.appendChild(img);
        return img;
    }
    static add_img(img_url){
        return Utility.add_img_to(document.body, img_url);
    }
    static add_div_to(parent){
        let div = document.createElement("div");
        parent.appendChild(div);
        return div;
    }
    static add_div(){
        return this.add_div_to(document.body);
    }
}
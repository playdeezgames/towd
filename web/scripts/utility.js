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
        Utility.add_button_to(document.body, text, on_click);
    }
    static add_paragraph_to(parent, text) {
        let p = document.createElement("p");
        p.innerHTML = text;
        parent.appendChild(p);
        return p;
    }
    static add_paragraph(text) {
        this.add_paragraph_to(document.body, text);
    }
    static add_break_to(parent) {
        let br = document.createElement("br");
        parent.appendChild(br);
        return br;
    }
    static add_break() {
        Utility.add_break_to(document.body);
    }
}
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
    static add_button(text, on_click, predicate) {
        let element = ElementStack.add_element_to(ElementStack.top(), "button", predicate)
        element.innerText = text;
        element.addEventListener("click", on_click);
        return element
    }
    static add_paragraph(text, predicate) {
        let element = ElementStack.add_element_to(ElementStack.top(), "p", predicate)
        element.innerText = text;
        return element
    }
    static add_span(text, predicate) {
        let element = ElementStack.add_element_to(ElementStack.top(), "span", predicate)
        element.innerText = text;
        return element
    }
    static add_break(predicate) {
        return ElementStack.add_element_to(ElementStack.top(), "br", predicate)
    }
    static add_img(img_url, predicate){
        let element = ElementStack.add_element_to(ElementStack.top(), "img", predicate);
        element.src = img_url;
        return element;
    }
    static add_div(predicate){
        return ElementStack.add_element_to(ElementStack.top(), "div", predicate)
    }
    static add_element_to(parent, element_type, predicate){
        let element = document.createElement(element_type);
        if(predicate!=null){
            predicate(element);
        }
        parent.appendChild(element);
        return element;
    }
    static add_table(predicate){
        return ElementStack.add_element_to(ElementStack.top(), "table", predicate);
    }
    static add_table_row(predicate){
        return ElementStack.add_element_to(ElementStack.top(), "tr", predicate);
    }
    static add_table_data(predicate){
        return ElementStack.add_element_to(ElementStack.top(), "td", predicate);
    }
}
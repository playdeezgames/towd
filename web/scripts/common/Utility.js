export class Utility{
    static removeChildren(element){
        while(element.firstChild){
            element.removeChild(element.firstChild);
        }
    }
    static addSimpleChild(element, tagName, textContent){
        let child = document.createElement(tagName);
        if(textContent != null){
            child.textContent=textContent;
        }
        element.appendChild(child);
        return child;
    }
}
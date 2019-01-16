function togglePixel(symbol,column,row){
    symbol=unescape(symbol);
    var glyph = font.Glyphs[symbol];
    var line=glyph.Lines[row];
    if(line==null){
        glyph.Lines[row]=[column];
    }else{
        if(glyph.Lines[row].includes(column)){
            var index=glyph.Lines[row].findIndex(function(x){return x==column;});
            glyph.Lines[row].splice(index,1);
        }else{
            glyph.Lines[row].push(column);
        }
    }
    editSymbol(escape(symbol));
}
function decrementWidth(symbol){
    symbol=unescape(symbol);
    var glyph = font.Glyphs[symbol];
    glyph.Width--;
    editSymbol(escape(symbol));
}
function incrementWidth(symbol){
    symbol=unescape(symbol);
    var glyph = font.Glyphs[symbol];
    glyph.Width++;
    editSymbol(escape(symbol));
}
function editSymbol(symbol){
    symbol=unescape(symbol);
    var content = "";
    var glyph = font.Glyphs[symbol];
    content+="<p>Width: "+glyph.Width+"<button onclick=\"decrementWidth('"+escape(symbol)+"')\">-</button><button onclick=\"incrementWidth('"+escape(symbol)+"')\">+</button></p>";
    content+="<table>";
    for(var row=0;row<font.Height;++row){
        content+="<tr>";
        var line = glyph.Lines[row];
        line = line || [];
        for(var column=0;column<glyph.Width;++column){
            content+="<td onclick=\"togglePixel('"+escape(symbol)+"',"+column+","+row+")\" id=\"pixel_"+column+"_"+row+"\" style=\"border: 1px solid #808080;width:32px;height:32px;background-color:";
            if(line.includes(column)){
                content+="#000000";
            }else{
                content+="#FFFFFF";
            }
            content+="\"> </td>";
        }
        content+="</tr>";
    }
    content+="</table>";
    content+="<button onclick=\"listSymbols()\">List</button>";
    document.body.innerHTML=content;
}
function decrementHeight(){
    font.Height--;
    listSymbols();
}
function incrementHeight(){
    font.Height++;
    listSymbols();
}
function loadFont(file){
    var reader = new FileReader();
    reader.onload = function(event) {
        font = JSON.parse(event.target.result);
        listSymbols();
    };    
    reader.readAsText(file);
}
function dropHandler(ev) {
    ev.preventDefault();
    if (ev.dataTransfer.items) {
        for (var i = 0; i < ev.dataTransfer.items.length; i++) {
            if (ev.dataTransfer.items[i].kind === 'file') {
                var file = ev.dataTransfer.items[i].getAsFile();
                loadFont(file);
                break;
            }
        }
    } else {
        for (var i = 0; i < ev.dataTransfer.files.length; i++) {
            var file = ev.dataTransfer.files[i];
            loadFont(file);
            break;
        }
    }
}
function dragOverHandler(ev) {
    ev.preventDefault();
}
function exportFont(){
    var content = JSON.stringify(font);
    var filename = document.getElementById('exportFileName').value;
    
    var blob = new Blob([content], {
     type: "application/json;charset=utf-8"
    });
    
    saveAs(blob, filename);    
}
function listSymbols(){
    var content = "";
    content+="<button id=\"drop_zone\" style=\"padding:20px\" ondrop=\"dropHandler(event);\" ondragover=\"dragOverHandler(event);\">";
    content+="Drop Files Here...";
    content+="</button>";
    content+="<p>Height: "+font.Height+"<button onclick=\"decrementHeight()\">-</button><button onclick=\"incrementHeight()\">+</button></p>";
    content+="<p><button onclick=\"exportFont()\">Export</button><input id=\"exportFileName\" type=\"text\" value=\"font.json\"/></p>"
    content += "<table>";
    content+="<tr>"
    content+="<th>Symbol</th>";
    content+="<th>ASCII</th>";
    content+="<th>Image</th>";
    content+="<th>Actions</th>"
    content+="</tr>"
    for(var symbol in font.Glyphs){
        var glyph = font.Glyphs[symbol];
        content+="<tr>"
        content+="<td>"+symbol+"</td>";
        content+="<td>"+symbol.charCodeAt(0)+"</td>";
        content+="<td>"
        var canvas = document.createElement("canvas");
        canvas.width = font.Glyphs[symbol].Width;
        canvas.height = font.Height;
        var ctx = canvas.getContext("2d");
        ctx.fillStyle="#FFFFFF";
        ctx.fillRect(0,0,canvas.width,canvas.height);
        ctx.fillStyle="#000000";
        for(var row in glyph.Lines){
            var line = glyph.Lines[row];
            for(var index in line){
                var column = line[index];
                ctx.fillRect(column,row,1,1);
            }
        }
        content+="<img src=\""+canvas.toDataURL("image/png")+"\"/>";
        content+="<img width=\""+canvas.width * 8+"\" height=\""+canvas.height*8+"\" src=\""+canvas.toDataURL("image/png")+"\"/>";
        content+="</td>"
        content+="<td>"
        content+="<button onclick=\"editSymbol('"+escape(symbol)+"')\">Edit</button>";
        content+="</td>"
        content+="</tr>"
    }
    content += "</table>";
    document.body.innerHTML=content;
}
function main(){
    listSymbols();
}
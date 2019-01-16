function decrementHeight(){
    font.Height--;
    listSymbols();
}
function incrementHeight(){
    font.Height++;
    listSymbols();
}
function listSymbols(){
    var content = "";
    content+="<button id=\"drop_zone\" style=\"padding:20px\" ondrop=\"dropHandler(event);\" ondragover=\"dragOverHandler(event);\">";
    content+="Drop Files Here...";
    content+="</button>";
    content+="<p>Height: "+font.Height+"<button onclick=\"decrementHeight()\">-</button><button onclick=\"incrementHeight()\">+</button></p>";
    content+="<p><button onclick=\"exportFont()\">Export</button><input id=\"exportFileName\" type=\"text\" value=\"font.json\"/></p>"
    content+="<table class=\"table table-striped\">";
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
        var canvas2 = document.createElement("canvas");
        canvas2.width = font.Glyphs[symbol].Width*8;
        canvas2.height = font.Height*8;
        var ctx = canvas.getContext("2d");
        ctx.fillStyle="#FFFFFF";
        ctx.fillRect(0,0,canvas.width,canvas.height);
        ctx.fillStyle="#000000";
        var ctx2 = canvas2.getContext("2d");
        ctx2.fillStyle="#FFFFFF";
        ctx2.fillRect(0,0,canvas2.width,canvas2.height);
        ctx2.fillStyle="#000000";
        for(var row in glyph.Lines){
            var line = glyph.Lines[row];
            for(var index in line){
                var column = line[index];
                ctx.fillRect(column,row,1,1);
                ctx2.fillRect(column*8,row*8,8,8);
            }
        }
        content+="<img src=\""+canvas.toDataURL("image/png")+"\"/>";
        content+="<img src=\""+canvas2.toDataURL("image/png")+"\"/>";
        content+="</td>"
        content+="<td>"
        content+="<button onclick=\"editSymbol('"+escape(symbol)+"')\">Edit</button>";
        content+="</td>"
        content+="</tr>"
    }
    content += "</table>";
    document.getElementById("content").innerHTML=content;
}
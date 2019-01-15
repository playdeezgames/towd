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
function editSymbol(symbol){
    symbol=unescape(symbol);
    var content = "";
    var glyph = font.Glyphs[symbol];
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
function listSymbols(){
    var content = "";
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
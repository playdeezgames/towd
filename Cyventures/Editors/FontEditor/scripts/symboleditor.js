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
    document.getElementById("content").innerHTML=content;
}

function applyTileHeight(){
    world.TileHeight=document.getElementById("tileHeight").value;
    worldEditor();
}
function applyTileWidth(){
    world.TileWidth=document.getElementById("tileWidth").value;
    worldEditor();
}
function applyAvatar(){
    world.Avatar=document.getElementById("avatar").value;
    worldEditor();
}
function worldEditor(){
    var content = "";
    content+="<button id=\"drop_zone\" style=\"padding:20px\" ondrop=\"dropHandler(event);\" ondragover=\"dragOverHandler(event);\">";
    content+="Drop Files Here...";
    content+="</button>";
    content+="<p><button onclick=\"exportWorld()\">Export</button><input id=\"exportFileName\" type=\"text\" value=\"world.json\"/></p>"
    content+="<table>";
    content+="<tr><td>TileWidth</td><td><input id=\"tileWidth\" type=\"text\" value=\""+world.TileWidth+"\"/><button onclick=\"applyTileWidth()\">Apply</button></td></tr>";
    content+="<tr><td>TileHeight</td><td><input id=\"tileHeight\" type=\"text\" value=\""+world.TileHeight+"\"/><button onclick=\"applyTileHeight()\">Apply</button></td></tr>";
    content+="<tr><td>Avatar</td><td><input id=\"avatar\" type=\"text\" value=\""+world.Avatar+"\"/><button onclick=\"applyAvatar()\">Apply</button></td></tr>";
    content+="</table>";
    document.body.innerHTML=content;
}

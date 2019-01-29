function setPixel(index,column,row){
    var image = bitmapSequence.Items[index];
    var pixelIndex = column + row * image.Width;
    image.Pixels[pixelIndex]=currentColor;
    editImage(index);
}
function resizeImage(image,newWidth,newHeight){
    var newPixels = [];
    while(newPixels.length<newWidth*newHeight){
        newPixels.push(0);
    }
    for(var x=0;x<image.Width && x<newWidth;++x){
        for(var y=0;y<image.Height && x<newHeight;++y){
            newPixels[x+y*newWidth]=image.Pixels[x+y*image.Width];
        }
    }
    image.Width=newWidth;
    image.Height=newHeight;
    image.Pixels=newPixels;
}
function decrementWidth(index){
    var image = bitmapSequence.Items[index];
    if(image.Width>1){
        resizeImage(image,image.Width-1,image.Height);
        editImage(index);
    }
}
function incrementWidth(index){
    var image = bitmapSequence.Items[index];
    resizeImage(image,image.Width+1,image.Height);
    editImage(index);
}
function decrementHeight(index){
    var image = bitmapSequence.Items[index];
    if(image.Height>1){
        resizeImage(image,image.Width,image.Height-1);
        editImage(index);
    }
}
function incrementHeight(index){
    var image = bitmapSequence.Items[index];
    resizeImage(image,image.Width,image.Height+1);
    editImage(index);
}
var currentColor=3;
function setCurrentColor(index,color){
    currentColor=color;
    editImage(index);
}
function editImage(index){
    var content = "";
    var image = bitmapSequence.Items[index];
    content+="<p>Width: "+image.Width+"<button onclick=\"decrementWidth("+index+")\">-</button><button onclick=\"incrementWidth("+index+")\">+</button></p>";
    content+="<p>Height: "+image.Height+"<button onclick=\"decrementHeight("+index+")\">-</button><button onclick=\"incrementHeight("+index+")\">+</button></p>";
    content+="<table>";
    content+="<tr>";
    for(var pixel=0;pixel<4;++pixel){
        content+="<td onclick=\"setCurrentColor("+index+","+pixel+")\" style=\"border: 1px solid ";
        if(pixel==currentColor){
            content+="#FF00FF";
        }else{
            content+="#808080";
        }
        content+=";width:16px;height:16px;background-color:"+palette[pixel]+"\"></td>";
    }
    content+="</tr>";
    content+="</table>";
    content+="<table>";
    for(var row=0;row<image.Height;++row){
        content+="<tr>";
        for(var column=0;column<image.Width;++column){
            content+="<td onclick=\"setPixel("+index+","+column+","+row+")\" id=\"pixel_"+column+"_"+row+"\" style=\"border: 1px solid #808080;width:32px;height:32px;background-color:";
            var pixel = image.Pixels[column+row*image.Width];
            content+=palette[pixel];            
            content+="\"> </td>";
        }
        content+="</tr>";
    }
    content+="</table>";
    content+="<button onclick=\"listImages()\">List</button>";
    document.body.innerHTML=content;
}
function loadBitmapSequence(file){
    var reader = new FileReader();
    reader.onload = function(event) {
        bitmapSequence = JSON.parse(event.target.result);
        listImages();
    };    
    reader.readAsText(file);
}
function dropHandler(ev) {
    ev.preventDefault();
    if (ev.dataTransfer.items) {
        for (var i = 0; i < ev.dataTransfer.items.length; i++) {
            if (ev.dataTransfer.items[i].kind === 'file') {
                var file = ev.dataTransfer.items[i].getAsFile();
                loadBitmapSequence(file);
                break;
            }
        }
    } else {
        for (var i = 0; i < ev.dataTransfer.files.length; i++) {
            var file = ev.dataTransfer.files[i];
            loadBitmapSequence(file);
            break;
        }
    }
}
function dragOverHandler(ev) {
    ev.preventDefault();
}
function exportBitmapSequence(){
    var content = JSON.stringify(bitmapSequence);
    var filename = document.getElementById('exportFileName').value;
    
    var blob = new Blob([content], {
     type: "application/json;charset=utf-8"
    });
    
    saveAs(blob, filename);    
}
function decrementCount(){
    if(bitmapSequence.Count>1){
        bitmapSequence.Count--;
        bitmapSequence.Items.pop();
        listImages();
    }
}
function incrementCount(){
    bitmapSequence.Count++;
    bitmapSequence.Items.push({"Width":1,"Height":1,"Pixels":[0]});
    listImages();
}
var palette = ["#ffffff","#aaaaaa","#555555","#000000"];
function listImages(){
    var content = "";
    content+="<button id=\"drop_zone\" style=\"padding:20px\" ondrop=\"dropHandler(event);\" ondragover=\"dragOverHandler(event);\">";
    content+="Drop Files Here...";
    content+="</button>";
    content+="<p>Count: "+bitmapSequence.Count+"<button onclick=\"decrementCount()\">-</button><button onclick=\"incrementCount()\">+</button></p>";
    content+="<p><button onclick=\"exportBitmapSequence()\">Export</button><input id=\"exportFileName\" type=\"text\" value=\"bitmapsequence.json\"/></p>"
    content+="<table>";
    content+="<tr>"
    content+="<th>Index</th>";
    content+="<th>Image</th>";
    content+="<th>Actions</th>"
    content+="</tr>"
    for(var index in bitmapSequence.Items){
        var image = bitmapSequence.Items[index];
        content+="<tr>"
        content+="<td>"+index+"</td>";
        content+="<td>"
        var canvas = document.createElement("canvas");
        canvas.width = image.Width;
        canvas.height = image.Height;
        var canvas2 = document.createElement("canvas");
        canvas2.width = image.Width*8;
        canvas2.height = image.Height*8;
        var ctx = canvas.getContext("2d");
        var ctx2 = canvas2.getContext("2d");
        for(var x=0;x<image.Width;++x){
            for(var y=0;y<image.Height;++y){
                var pixel = image.Pixels[x+y*image.Width];
                ctx.fillStyle=palette[pixel];
                ctx.fillRect(x,y,1,1);
                ctx2.fillStyle=palette[pixel];
                ctx2.fillRect(x*8,y*8,8,8);
            }
        }
        content+="<img src=\""+canvas.toDataURL("image/png")+"\"/>";
        content+="<img width=\""+canvas.width * 8+"\" height=\""+canvas.height*8+"\" src=\""+canvas2.toDataURL("image/png")+"\"/>";
        content+="</td>"
        content+="<td>"
        content+="<button onclick=\"editImage("+index+")\">Edit</button>";
        content+="</td>"
        content+="</tr>"
    }
    content += "</table>";
    document.body.innerHTML=content;
}
function main(){
    listImages();
}
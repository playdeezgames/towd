function loadWorld(file){
    var reader = new FileReader();
    reader.onload = function(event) {
        world = JSON.parse(event.target.result);
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
                loadWorld(file);
                break;
            }
        }
    } else {
        for (var i = 0; i < ev.dataTransfer.files.length; i++) {
            var file = ev.dataTransfer.files[i];
            loadWorld(file);
            break;
        }
    }
}
function dragOverHandler(ev) {
    ev.preventDefault();
}
function exportWorld(){
    var content = JSON.stringify(world);
    var filename = document.getElementById('exportFileName').value;
    
    var blob = new Blob([content], {
     type: "application/json;charset=utf-8"
    });
    
    saveAs(blob, filename);    
}

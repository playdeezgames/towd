function loadGame(filename) {
    try {
        let saveData = localStorage.getItem(filename)
        if (saveData == null) {
            return null;
        }
        return JSON.parse(saveData).what;
    } catch (ex) {
        console.log(ex);
        return null;
    }
}
function saveExists(filename) {
    try {
        let saveData = localStorage.getItem(filename)
        if (saveData == null) {
            return null;
        }
        return JSON.parse(saveData).when;
    } catch (ex) {
        console.log(ex);
        return null;
    }
}
function saveGame(filename, data) {
    let saveData = { "when": new Date(), "what": data };
    localStorage.setItem(filename, JSON.stringify(saveData));
}
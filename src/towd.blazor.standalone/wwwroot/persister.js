function loadGame(filename) {
    let saveData = localStorage.getItem(filename)
    if (saveData == null) {
        return null;
    }
    return JSON.parse(saveData).what;
}
function saveExists(filename) {
    let saveData = localStorage.getItem(filename)
    if (saveData == null) {
        return null;
    }
    return JSON.parse(saveData).when;
}
function saveGame(filename, data) {
    let saveData = { "when": new Date(), "what": data };
    localStorage.setItem(filename, JSON.stringify(saveData));
}
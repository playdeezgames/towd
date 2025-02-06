local gamestates = require "game.gamestates"
local M = {}
local states = {
    [gamestates.TITLE] = require "scene.states.TITLE"
}
local current = gamestates.TITLE
function M.load()
    for _, state in pairs(states) do
        state.load()
    end
end
function M.update()
    states[current].update()
end
function M.handle_command(command, isrepeat)
    states[current].handle_command(command, isrepeat)
end
return M
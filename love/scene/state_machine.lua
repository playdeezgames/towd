local gamestates = require "game.gamestates"
local M = {}
local states = {
    [gamestates.TITLE] = require "scene.states.TITLE",
    [gamestates.CONFIRM_QUIT] = require "scene.states.CONFIRM_QUIT",
    [gamestates.IN_PLAY] = require "scene.states.IN_PLAY",
    [gamestates.GAME_MENU] = require "scene.states.GAME_MENU"
}
local current = nil
function M.load()
    for _, state in pairs(states) do
        state.load()
        state.set_state = M.set_current
    end
    M.set_current(gamestates.TITLE)
end
function M.update()
    states[current].update()
end
function M.handle_command(command, isrepeat)
    states[current].handle_command(command, isrepeat)
end
local function handle_finish()
    states[current].finish()
end
local function handle_start()
    states[current].start()
end
function M.set_current(state_id)
    if current ~= nil then
        handle_finish()
    end
    current = state_id
    if current ~= nil then
        handle_start()
    end
end
function M.get_current()
    return current
end
return M
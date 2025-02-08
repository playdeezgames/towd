local colors = require "game.colors"
local gfx    = require "gfx.gfx"
local grimoire = require "game.grimoire"
local menu = require "gfx.menu"
local menu_item = require "gfx.menu_item"
local gamestates = require "game.gamestates"
local M = {}
local game_menu = {}
local world = require "world.world"
function M.set_state(_)
    assert(false, "set_state did not get set by the state machine")
end
function M.load()
end
function M.update()
    love.graphics.setBackgroundColor(colors.WHITE)
    love.graphics.clear()
    game_menu:draw()
end
local function handle_continue()
    M.set_state(gamestates.IN_PLAY)
end
local function handle_abandon()
    world.abandon()
    M.set_state(gamestates.TITLE)
end
function M.handle_command(command, isrepeat)
    game_menu:handle_command(command, isrepeat)
end
function M.start()
    game_menu = menu.new("Game Menu",{
        menu_item.new("Continue Game", handle_continue),
        menu_item.new("Abandon Game", handle_abandon)
    },handle_continue, 0, 0, grimoire.VIEW_WIDTH, grimoire.VIEW_HEIGHT, gfx.get_font(grimoire.FONT5X7))
end
function M.finish()
end
return M
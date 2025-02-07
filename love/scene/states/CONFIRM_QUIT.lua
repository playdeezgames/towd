local colors = require "game.colors"
local gfx    = require "gfx.gfx"
local grimoire = require "game.grimoire"
local menu = require "gfx.menu"
local menu_item = require "gfx.menu_item"
local gamestates = require "game.gamestates"
local M = {}
local confirm_menu = {}
function M.set_state(_)
    assert(false, "set_state did not get set by the state machine")
end
function M.load()
end
function M.update()
    love.graphics.setBackgroundColor(colors.WHITE)
    love.graphics.clear()
    confirm_menu:draw()
end
function M.handle_command(command, isrepeat)
    confirm_menu:handle_command(command, isrepeat)
end
local function handle_no()
    M.set_state(gamestates.TITLE)
end
local function handle_hell_no()
    confirm_menu:set_current_item_index(1)
end
function M.start()
    confirm_menu = menu.new("Confirm quit?",{
        menu_item.new("No", handle_no),
        menu_item.new("Hell No", handle_hell_no)
    },handle_no, 0, 0, grimoire.VIEW_WIDTH, grimoire.VIEW_HEIGHT, gfx.get_font(grimoire.FONT5X7))
end
function M.finish()
end
return M
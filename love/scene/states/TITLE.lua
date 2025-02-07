local colors = require "game.colors"
local gfx    = require "gfx.gfx"
local grimoire = require "game.grimoire"
local menu = require "gfx.menu"
local menu_item = require "gfx.menu_item"
local M = {}
local main_menu = {}
function M.set_state(_)
    assert(false, "set_state did not get set by the state machine")
end
function M.load()
    main_menu = menu.new("Main Menu",{
        menu_item.new("Embark!", {}),
        menu_item.new("Quit", {})
    },0,0,grimoire.VIEW_WIDTH,grimoire.VIEW_HEIGHT,gfx.get_font(grimoire.FONT3X5))
end
function M.update()
    love.graphics.setBackgroundColor(colors.WHITE)
    love.graphics.clear()
    main_menu:draw()
end
function M.handle_command(command, isrepeat)
    if command == grimoire.COMMAND_UP then
        main_menu:previous_item()
    elseif command == grimoire.COMMAND_DOWN then
        main_menu:next_item()
    end
end
function M.start()
end
function M.finish()
end
return M
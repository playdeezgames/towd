local colors = require "game.colors"
local gfx    = require "gfx.gfx"
local grimoire = require "game.grimoire"
local menu = require "gfx.menu"
local menu_item = require "gfx.menu_item"
local gamestates = require "game.gamestates"
local world_initializer = require "world.initializers.world"
local M = {}
local main_menu = {}
function M.set_state(_)
    assert(false, "set_state did not get set by the state machine")
end
function M.load()
end
function M.update()
    love.graphics.setBackgroundColor(colors.WHITE)
    love.graphics.clear()
    main_menu:draw()
end
local function handle_embark()
    world_initializer.initialize()
    M.set_state(gamestates.IN_PLAY)
end
local function handle_quit()
    M.set_state(gamestates.CONFIRM_QUIT)
end
function M.handle_command(command, isrepeat)
    main_menu:handle_command(command, isrepeat)
end
function M.start()
    main_menu = menu.new("Main Menu",{
        menu_item.new("Embark!", handle_embark),
        menu_item.new("Quit", handle_quit)
    },handle_quit, 0, 0, grimoire.VIEW_WIDTH, grimoire.VIEW_HEIGHT, gfx.get_font(grimoire.FONT5X7))
end
function M.finish()
end
return M
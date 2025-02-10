local stage = require "scene.stage"
local states = require "game.states"
local splash_state = require "game.splash_state"
local commands = require "game.commands"
local gfx = require "gfx.gfx"
local hues= require "game.hues"
local fonts = require "game.fonts"
local images= require "game.images"
local main_menu_state = require "game.main_menu_state"
local M = {}
local command_table = {
    ["return"] = commands.BLUE,
    down = commands.DOWN,
    space = commands.GREEN,
    left = commands.LEFT,
    escape = commands.RED,
    right = commands.RIGHT,
    up = commands.UP,
    tab = commands.YELLOW
}
function M.new()
    local instance = stage.new()
    instance.states = {
        [states.SPLASH] = splash_state.new(instance),
        [states.MAIN_MENU] = main_menu_state.new(instance)
    }
    function instance:get_current_state()
        if instance.state == nil then
            return nil
        end
        return instance.states[instance.state]
    end
    function instance:set_state(state)
        if instance:get_current_state() ~= nil then
            instance:get_current_state():finish()
        end
        instance.state = state
        if instance:get_current_state() ~= nil then
            instance:get_current_state():start()
        end
    end
    function instance:on_draw()
        gfx.clear(hues.BLACK)
    end
    function instance:on_keypressed(key, scancode, isrepeat)
        local which = command_table[key]
        if which ~= nil then
            instance:get_current_state():handle_command(which)
        end
    end
    function instance:on_load()
        fonts.load()
        images.load()
    end
    instance:set_state(states.SPLASH)
    return instance
end
return M
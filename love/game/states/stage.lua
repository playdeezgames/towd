local stage = require "scene.stage"
local states = require "game.states.states"
local splash_state = require "game.states.splash_state"
local commands = require "game.commands"
local gfx = require "gfx.gfx"
local hues= require "game.gfx.hues"
local fonts = require "game.gfx.fonts"
local images= require "game.gfx.images"
local main_menu_state = require "game.states.main_menu_state"
local sources         = require "game.sfx.sources"
local confirm_quit_state = require "game.states.confirm_quit_state"
local source_id          = require "game.sfx.source_id"
local sfx                = require "sfx.sfx"
local options_state      = require "game.states.options_state"
local decal              = require "ui.decal"
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
local button_table = {
    a=commands.GREEN,
    b=commands.RED,
    x=commands.BLUE,
    y=commands.YELLOW,
    dpup=commands.UP,
    dpdown=commands.DOWN,
    dpleft=commands.LEFT,
    dpright=commands.RIGHT
}
function M.new()
    local instance = stage.new()
    instance.states = {
        [states.SPLASH] = splash_state.new(instance),
        [states.MAIN_MENU] = main_menu_state.new(instance),
        [states.CONFIRM_QUIT] = confirm_quit_state.new(instance),
        [states.OPTIONS] = options_state.new(instance)
    }
    decal.new(
        instance,
        {
            image = love.graphics.newImage("assets/images/back_button.png"),
            x = 64,
            y = 64,
            origin_x = 32,
            origin_y = 32,
            hue = {0.5,1,1}
        })
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
    function instance:legacy_on_draw()
        gfx.clear(hues.BLACK)
        gfx.update_scale()
    end
    function instance:on_keypressed(key, scancode, isrepeat)
        local which = command_table[key]
        if which ~= nil then
            self:get_current_state():handle_command(which)
        end
    end
    function instance:on_gamepadpressed(joystick, button)
        local which = button_table[button]
        if which ~= nil then
            self:get_current_state():handle_command(which)
        end
    end
    function instance:on_load()
        fonts.load()
        images.load()
        sfx.load()
        sources.load()
        sources[source_id.MINOR_SONG]:loop()
    end
    instance:set_state(states.SPLASH)
    return instance
end
return M
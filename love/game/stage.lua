local stage = require "scene.stage"
local states = require "game.states"
local splash_state = require "game.splash_state"
local main_menu_state = require "game.main_menu_state"
local M = {}
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
        love.graphics.clear()
    end
    instance:set_state(states.SPLASH)
    return instance
end
return M
local picker_state = require "game.picker_state"
local states       = require "game.states"
local hues         = require "gfx.hues"
local game         = require "game.game"
local M = {}

function M.new(parent)
    local YES_TEXT = "Yes"
    local NO_TEXT = "No"
    local CONFIRM_TEXT = "Are you sure you want to quit?"
    local instance = picker_state.new(parent, CONFIRM_TEXT, hues.RED, {
        NO_TEXT,
        YES_TEXT
    })
    function instance:on_menu_item(menu_item)
        if menu_item == NO_TEXT then
            self:get_parent():set_state(states.MAIN_MENU)
        else
            game.quit()
        end
    end
    function instance:on_cancel()
        self:get_parent():set_state(states.MAIN_MENU)
    end
    return instance
end

return M
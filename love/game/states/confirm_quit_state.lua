local picker_state = require "game.states.picker_state"
local image_id     = require "game.gfx.image_id"
local font_id      = require "game.gfx.font_id"
local states       = require "game.states.states"
local game         = require "game.game"
local M = {}
function M.new(parent)
    local NO_TEXT = "No"
    local YES_TEXT = "Yes"
    local instance = picker_state.new(parent, image_id.SPLASH, font_id.M6X11PLUS_48, "Are you sure you want to quit?", {NO_TEXT, YES_TEXT})
    function instance:on_menu_item(menu_item)
        local item_text = menu_item:get_text()
        if item_text == YES_TEXT then
            game.quit()
        else
            self:get_parent():set_state(states.MAIN_MENU)
        end
    end
    function instance:on_cancel()
        self:get_parent():set_state(states.MAIN_MENU)
    end
    return instance
end
return M
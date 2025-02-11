local picker_state = require "game.picker_state"
local image_id     = require "game.image_id"
local font_id      = require "game.font_id"
local states       = require "game.states"
local M = {}
function M.new(parent)
    local EMBARK_TEXT = "Embark!"
    local LOAD_TEXT = "Load..."
    local OPTIONS_TEXT = "Options..."
    local ABOUT_TEXT = "About..."
    local QUIT_TEXT = "Quit"
    local instance = picker_state.new(parent, image_id.SPLASH, font_id.M6X11PLUS_48, "Main Menu:", {EMBARK_TEXT, LOAD_TEXT, OPTIONS_TEXT, ABOUT_TEXT, QUIT_TEXT})
    function instance:on_menu_item(menu_item)
        local item_text = menu_item:get_text()
        if item_text == QUIT_TEXT then
            self:get_parent():set_state(states.CONFIRM_QUIT)
        end
    end
    function instance:on_cancel()
        self:get_parent():set_state(states.CONFIRM_QUIT)
    end
    return instance
end
return M